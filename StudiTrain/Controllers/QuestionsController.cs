using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudiTrain.Models;
using StudiTrain.Models.Database;
using StudiTrain.Setup;

namespace StudiTrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly PostgresContext _db;

        public QuestionsController(AppSettings settings)
        {
            _db = new PostgresContext(settings.ControllerSetup.ConnectionString);
        }
        // GET api/values
        [Authorize]
        [HttpGet]
        public ActionResult<DbSet<Questions>> Get()
        {
            return _db.Questions;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Questions> Get(int id)
        {
            var result = _db.Questions.Where(q => q.Id == id);
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
