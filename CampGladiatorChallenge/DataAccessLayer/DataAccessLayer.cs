using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using CampGladiatorChallenge.Controllers;
using Microsoft.Extensions.Configuration;

namespace CampGladiatorChallenge
{
    public class DataAccessLayer : IDisposable
    {
        private bool disposedValue;
        private static IConfiguration _config;
        public DataAccessLayer(IConfiguration configuration)
        {
            _config = configuration;
        }
        
        private TrainerDataAccessLayer _trainerDataAccessLayer;
        public TrainerDataAccessLayer TrainerData
        {
            get
            {
                if (_trainerDataAccessLayer == null)
                {
                    _trainerDataAccessLayer = new TrainerDataAccessLayer();

                }
                return _trainerDataAccessLayer;
            }
        }

        public class TrainerDataAccessLayer {
            // I'd use commvault or something to store these, but in the interest of time I'm sending an appsettings with this info. 
            private static BasicAWSCredentials _credentials = new BasicAWSCredentials(_config.GetSection("AWSKeys")["AccessKey"], _config.GetSection("AWSKeys")["SecretKey"]);
            private static AmazonDynamoDBConfig _awsconfig = new AmazonDynamoDBConfig()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USWest2
            };
            private static AmazonDynamoDBClient _client = new AmazonDynamoDBClient(_credentials, _awsconfig);
            private static string _tableName = "Trainers";


            public async Task<GetItemResponse> GetTrainerFromDB(string trainerId)
            {
                return await _client.GetItemAsync(CreateDynamoGetItemRequestFromSingleAttribute("id", trainerId));
            }
            
            public async Task<(HttpStatusCode, string)> PutNewTrainerInDB(Trainer input)
            {
                try
                {
                    string id = Guid.NewGuid().ToString();
                    Dictionary<string, AttributeValue> trainer = new Dictionary<string, AttributeValue>
                        {
                            {"id", new AttributeValue { S = id} },
                            {"email", new AttributeValue { S = input.email} },
                            {"phone", new AttributeValue { N = input.phone.ToString()} },
                            {"first_name", new AttributeValue { S = input.first_name} },
                            {"last_name", new AttributeValue { S = input.last_name} }
                        };

                    PutItemResponse response = await _client.PutItemAsync(_tableName, trainer);
                    return (response.HttpStatusCode, id);
                }
                catch
                {
                    return (HttpStatusCode.InternalServerError, "no id, record not created");
                }

            }
            private static GetItemRequest CreateDynamoGetItemRequestFromSingleAttribute(string attributeName, string value)
            {
                // with more time, I'd overload this function or try a parse test on a string value, and make this function fulfill its name by allowing requests with different types.
                // Could even be expanded to accept multiple attributes depending on client needs.
                Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>
                    {
                        {
                            attributeName, new AttributeValue { S = value.ToString() }
                        },
                    };

                return new GetItemRequest
                {
                    TableName = _tableName,
                    Key = key,
                };
            }
        }
        // below is just boilerplate to add disposal, ran out of time. 
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Datalayer()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }
    }
}