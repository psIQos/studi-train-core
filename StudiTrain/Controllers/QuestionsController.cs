using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudiTrain.Entities;
using StudiTrain.Models.Database;
using StudiTrain.Services;
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

        [HttpGet]
        public ActionResult<IEnumerable<Question>> Get([FromQuery] int? category)
        {
            var questions = DbConn.Questions.Include(q => q.AnswersMc);
            if (category == null)
                return Ok(questions
                    .OrderBy(q => q.Number)
                    .Select(q => new Question(q))
                );

            return Ok(questions
                .Where(q => q.Category == category)
                .OrderBy(q => q.Number)
                .Select(q => new Question(q))
            );
        }

        [HttpGet("count")]
        public ActionResult<int> GetCount([FromQuery] int? category)
        {
            var questions = DbConn.Questions.Include(q => q.AnswersMc);
            if (category == null) return Ok(questions.Count());

            return Ok(questions.Count(q => q.Category == category));
        }

        [HttpGet("{id}")]
        public ActionResult<Question> GetOne(int id)
        {
            var questionDb = DbConn.Questions
                .Include(q => q.AnswersMc)
                .FirstOrDefault(q => q.Id == id);
            if (questionDb == null)
                return NoContent();
            var question = new Question(questionDb);
            return question;
        }

        [HttpPost]
        public ActionResult<int> PostOne([FromBody] Question questionInput, [FromQuery] int? category)
        {
            // if no category is specified one will be created
            if (category == null)
            {
                var newCategory = new Categories
                {
                    Name = Guid.NewGuid().ToString(),
                    Comment = "created on import"
                };
                DbConn.Categories.Add(newCategory);
                DbConn.SaveChanges();
                category = newCategory.Id;
            }
            // if the specified category can't be found it's wrong
            else if (DbConn.Categories.Find(category) == null)
            {
                return BadRequest();
            }

            var userId = Services.UserService.GetUserFromToken(HttpContext).Id;
            var question = new Questions(questionInput, userId, category);
            DbConn.Questions.Add(question);
            DbConn.SaveChanges();
            return question.Id;
        }

        [Route("import")]
        [HttpPost]
        public ActionResult<IEnumerable<int>> PostMany([FromBody] IEnumerable<Question> questionsInput,
            [FromRoute] int? category)
        {
            // if no category is specified one will be created
            if (category == null)
            {
                var newCategory = new Categories
                {
                    Name = Guid.NewGuid().ToString(),
                    Comment = "created on import"
                };
                DbConn.Categories.Add(newCategory);
                DbConn.SaveChanges();
                category = newCategory.Id;
            }
            // if the specified category can't be found it's wrong
            else if (DbConn.Categories.Find(category) == null)
            {
                return BadRequest();
            }

            var userId = Services.UserService.GetUserFromToken(HttpContext).Id;
            var questions = questionsInput
                .Select((questionInput, index) => new Questions(questionInput, userId, category, index)).ToList();
            DbConn.AddRange(questions);
            DbConn.SaveChanges();
            return Ok(questions.Select(q => q.Id).OrderBy(id => id));
        }
    }
}