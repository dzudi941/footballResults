using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTEC.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HTEC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        // GET: api/Results
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Results/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Results
        [HttpPost]
        public void Post(List<ResultViewModel> results)
        {
            var aa = results;
        }

        // PUT: api/Results/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("PostResults")]
        public void PostResults(List<ResultViewModel> results)
        {
            var aa = results;
        }
    }
}
