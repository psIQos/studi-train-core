using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudiTrain.Models;

namespace StudiTrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly PostgresContext _db;

        public ValuesController(ControllerSetup setup)
        {
            if (setup.ConnectionString == null)
                return;
            var optionsBuilder = new DbContextOptionsBuilder<PostgresContext>();
            optionsBuilder.UseNpgsql(setup.ConnectionString);
            _db = new PostgresContext(optionsBuilder.Options);
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IDictionary> Get()
        {
            return Ok(Environment.GetEnvironmentVariables());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Questions> Get(int id)
        {
            return Ok(_db.Questions.First());
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
