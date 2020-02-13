using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudiTrain.Models;
using StudiTrain.Setup;

namespace StudiTrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly PostgresContext _db;

        public QuestionController(ControllerSetup setup)
        {
            if (setup.ConnectionString == null)
                throw new InvalidOperationException("Connection string has to be set by environment variables.");
            var optionsBuilder = new DbContextOptionsBuilder<PostgresContext>();
            optionsBuilder.UseNpgsql(setup.ConnectionString);
            _db = new PostgresContext(optionsBuilder.Options);
        }
        // GET api/values
        [HttpGet]
        public ActionResult<List<Questions>> Get()
        {
            return _db.Questions.Include(q => q.AnswersMc).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Questions> Get(int id)
        {
            var result = _db.Questions.Where(q => q.Id == id)
                .Include(q => q.AnswersMc);
            if (result.Any())
                return result.First();
            return NoContent();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Questions question)
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
