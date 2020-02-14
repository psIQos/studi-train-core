using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public ActionResult<DbSet<Questions>> Get()
        {
            return DbConn.Questions;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Questions> Get(int id)
        {
            var result = DbConn.Questions.Where(q => q.Id == id);
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