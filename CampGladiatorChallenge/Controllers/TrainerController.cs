using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Configuration;

namespace CampGladiatorChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainerController : BaseController

    {
        private const string genericResponseForProd = "There was an issue with supplied credentials, data, or connection to the database";
        public TrainerController(IConfiguration config, DataAccessLayer dataLayer ) : base(config, dataLayer)
        {
        }
        [HttpPost("upsertTrainer")]
        public async Task<ActionResult> UpsertTrainer([FromForm] Trainer input)
        {
            try
            {
                // if id is not present, we just want to insert a new row - if we were making this a true upsert we'd run an update of existing row an ID was supplied
                (HttpStatusCode status, string id) responseAndId = await _dataLayer.TrainerData.PutNewTrainerInDB(input);
                if (responseAndId.status == HttpStatusCode.OK)
                {
                    return Ok("Record inserted with ID:" + responseAndId.id);
                }
            }
            catch (Exception ex)
            {
                // we want to swallow the error instead of reporting the problem over the network for greater security, but can leave this here while debugging.
                // return BadRequest(ex);
            }

            return BadRequest(genericResponseForProd);
            
        }

        [HttpGet("{trainerId}")]
        public async Task<ActionResult<Trainer>> GetTrainer(string trainerId)
        {
            try { 
                GetItemResponse response = await _dataLayer.TrainerData.GetTrainerFromDB(trainerId);
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    return Ok(new Trainer(response));
                }
            }
            catch (Exception ex)
            {
                // we want to swallow the error instead of reporting the problem over the network for greater security, but can leave this here while debugging.
                // return BadRequest(ex);
            }
            return BadRequest(genericResponseForProd);
        }
    }
}



