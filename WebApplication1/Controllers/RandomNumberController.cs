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
            yield return new RandomNumber(10, 1, 100); // Generate 100 random numbers between 1 and 100
        }


        //GET List of Random Numbers
        [HttpGet("GenerateRandom")]
        public IActionResult GenerateRandom(int? numberQP, int? minQP, int? maxQP)
        {
            // Create a new results variable to store the results
            RandomNumberResult result = new RandomNumberResult();
            try
            {
                // StringValues to hold the parameter values, they will be parsed to ints later
                StringValues numberString, minString, maxString;

                // Verifying that each parameter is a valid type (string) and there is a value present. Output the vlaue to the StringValue parameter
                if (!(Request.Query.TryGetValue("numberQP", out numberString) && Request.Query.TryGetValue("minQP", out minString) && Request.Query.TryGetValue("maxQP", out maxString)))
                    throw new Exception("Missing parameter in query string.");
                else
                {
                    // Verifying that the StringValues doesnt contain blank spaces or is empty
                    if (!(string.IsNullOrEmpty(numberString.ToString()) && string.IsNullOrEmpty(minString.ToString()) && string.IsNullOrEmpty(maxString.ToString())))
                    {
                        int number = 0, min = 0, max = 0;

                        // Verifying the StringValues are correctly parsed into integers and outputted to ints
                        if (Int32.TryParse(numberString, out number) && Int32.TryParse(minString, out min) && Int32.TryParse(maxString, out max))
                        {

                            // If all of our data checks passed, we use the parameters to generate a new random list of numbers
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
                // Catching this exception so we make an empty array for our result and return a badRequest 
                result.randomNumberList = new int[] { };
                return BadRequest(result);
            }
            // if no exceptions are seen we return a good request with the random list we generated
            return Ok(result);
        }
    }
}
