using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Modals;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RandomNumberController : ControllerBase
    {

        private readonly ILogger<RandomNumberController> _logger;

        public RandomNumberController(ILogger<RandomNumberController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<RandomNumber> Get()
        {
            yield return new RandomNumber(10, 1, 100);
        }


        //GET List of Random Numbers
        [HttpGet("GenerateRandom")]
        public IActionResult GenerateRandom(int? numberQP, int? minQP, int? maxQP)
        {
            RandomNumberResult result = new RandomNumberResult();
            try
            {

                StringValues numberString, minString, maxString;
                if (!(Request.Query.TryGetValue("numberQP", out numberString) && Request.Query.TryGetValue("minQP", out minString) && Request.Query.TryGetValue("maxQP", out maxString)))
                    throw new Exception("Missing parameter in query string.");
                else
                {
                    if (!(string.IsNullOrEmpty(numberString.ToString()) && string.IsNullOrEmpty(minString.ToString()) && string.IsNullOrEmpty(maxString.ToString())))
                    {
                        int number = 0, min = 0, max = 0;
                        if (Int32.TryParse(numberString, out number) && Int32.TryParse(minString, out min) && Int32.TryParse(maxString, out max))
                        {
                            RandomNumber rng = new RandomNumber(number, min, max);
                            result.randomNumberList = rng.RandomNumbers;
                        }
                        else
                            throw new Exception("Invalid integer in query string.");
                    }
                    else
                        throw new Exception("Invalid value in query string.");
                }

                
            }
            catch (Exception ex)
            {
                result.randomNumberList = new int[] { };
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
