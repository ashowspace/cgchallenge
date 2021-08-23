using Amazon.DynamoDBv2.Model;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CampGladiatorChallenge.Controllers
{
    public class Trainer
    {
        public string ?id { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }

        [JsonConstructor]
        public Trainer() { }
            
        public Trainer(GetItemResponse response)
            {
                id = "trainer-id-" + response.Item["id"].S;
                email = response.Item["email"].S;
                phone = Regex.Replace(response.Item["phone"].N, @"(\d{3})(\d{3})(\d{4})", "($1)-$2-$3");
                first_name = response.Item["first_name"].S;
                last_name = response.Item["last_name"].S;
            }
        }

}
    
