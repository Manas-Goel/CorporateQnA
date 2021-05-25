using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Integrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateQnA.Controllers
{
        [ApiController]
        [Route("api/questions")]
        public class QuestionController : ControllerBase
        {
            private readonly IQuestionService _questionService;

            public QuestionController(IQuestionService questionService)
            {
                _questionService = questionService;
            }

            [HttpGet("{id}")]
            public Question GetQuestionById(int id)
            {
                return _questionService.GetQuestionById(id);
            }

            [HttpGet("user")]
            public IEnumerable<UserQuestions> GetQuestionsByUserId(string userId)
            {
                return _questionService.GetQuestionsByUserId(userId);
            }

            [HttpGet("view")]
            public void ViewQuestion(int questionId, string userId)
            {
                _questionService.IncreaseQuestionViews(questionId, userId);
                return;
            }

            [HttpGet("getupvoteinfo")]
            public QuestionUpvote GetUpvoteInfo(string userId, int questionId)
            {
                var upvoteInfo = new QuestionUpvote()
                {
                    UserId = userId,
                    QuestionId = questionId,
                };
                return _questionService.GetUpvoteInfo(upvoteInfo);
            }

            [HttpGet("upvotequestion")]
            public bool UpvoteQuestion(string userId, int questionId, bool upvote)
            {
                var upvoteInfo = new QuestionUpvote()
                {
                    UserId = userId,
                    QuestionId = questionId,
                    Upvote = upvote
                };
                _questionService.UpvoteQuestion(upvoteInfo);
                return true;
            }

            [HttpPost]
            public bool AddQuestion(Question question)
            {
                _questionService.AddQuestion(question);
                return true;
            }

            [HttpGet("search")]
            public IEnumerable<UserQuestions> SearchQuestions(string keyword, int categoryId, int searchCriteria, int searchTime, string userId)
            {
                return _questionService.SearchQuestions(keyword, categoryId, searchCriteria, searchTime, userId);
            }
        }
}
