using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudiTrain.Entities;
using StudiTrain.Models.Database;
using StudiTrain.Setup;

namespace StudiTrain.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : StudiTrainController
    {
        public QuestionsController(IAppSettings settings) : base(settings)
        {
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<Questions>> Get()
        {
            return DbConn.Questions.Include(q => q.AnswersMc).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Questions> Get(int id)
        {
            var question = DbConn.Questions.FirstOrDefault(q => q.Id == id);
            if (question == null)
                return NoContent();
            return question;
        }

        // POST api/values
        [HttpPost]
        public void PostOne([FromBody] Question questionInput)
        {
            var question = new Questions(questionInput);
            DbConn.Questions.Add(question);
            DbConn.SaveChanges();
        }

        [Route("many")]
        [HttpPost]
        public void PostMany([FromBody] IEnumerable<Question> questionsInput)
        {
            foreach (var questionInput in questionsInput)
            {
                var question = new Questions(questionInput);
                DbConn.Questions.Add(question);
            }
            DbConn.SaveChanges();
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