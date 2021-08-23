using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CampGladiatorChallenge
{
    public class BaseController : Controller
    {
        protected readonly DataAccessLayer _dataLayer;
        protected readonly IConfiguration _config;

        public BaseController(IConfiguration config, DataAccessLayer dataLayer)
        {
            _dataLayer = dataLayer;
            _config = config;
        }
    }
}
