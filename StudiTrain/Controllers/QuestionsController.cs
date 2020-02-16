﻿using System;
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
        public ActionResult<IEnumerable<Question>> Get([FromQuery] int? category)
        {
            var questions = DbConn.Questions.Include(q => q.AnswersMc);
            if (category == null) return Ok(questions.Select(q => new Question(q)));

            return Ok(questions.Where(q => q.Category == category).Select(q => new Question(q)));
        }

        [HttpGet("count")]
        public ActionResult<int> GetCount([FromQuery] int? category)
        {
            var questions = DbConn.Questions.Include(q => q.AnswersMc);
            if (category == null) return Ok(questions.Count());

            return Ok(questions.Count(q => q.Category == category));
        }

        // GET api/values/5
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

        // POST api/values
        [HttpPost]
        public ActionResult<int> PostOne([FromBody] Question questionInput)
        {
            var question = new Questions(questionInput);
            DbConn.Questions.Add(question);
            DbConn.SaveChanges();
            return question.Id;
        }

        [Route("import")]
        [HttpPost]
        public ActionResult<IEnumerable<int>> PostMany([FromBody] IEnumerable<Question> questionsInput, [FromRoute] int? category)
        {
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
            else if (DbConn.Categories.Find(category) == null)
            {
                return BadRequest();
            }
            var questions = questionsInput.Select(questionInput => new Questions(questionInput, category)).ToList();
            DbConn.Questions.AddRange(questions);
            DbConn.SaveChanges();
            return Ok(questions.Select(q => q.Id).OrderBy(id => id));
        }
    }
}