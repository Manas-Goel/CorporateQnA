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
    [Route("api/answers")]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet("all")]
        public IEnumerable<QuestionAnswers> GetAllAnswers(int questionId)
        {
            return _answerService.GetAllAnswersByQuestionId(questionId);
        }

        [HttpGet("{id}")]
        public Answer GetAnswer(int id)
        {
            return _answerService.GetAnswerById(id);
        }

        [HttpPost]
        public void AddAnswer(Answer answer)
        {
            _answerService.AddAnswer(answer);
            return;
        }

        [HttpGet("getratings")]
        public AnswerRating GetAnswerRating(int answerId, string userId)
        {
            AnswerRating rating = new AnswerRating()
            {
                AnswerId = answerId,
                UserId = userId
            };


            return _answerService.GetAnswerRating(rating);
        }

        [HttpGet("rate")]
        public AnswerRating GiveAnswerRating(int answerId, string userId, bool liked, bool disliked)
        {
            AnswerRating rating = new AnswerRating()
            {
                AnswerId = answerId,
                UserId = userId,
                Liked = liked,
                Disliked = disliked
            };


            return _answerService.GiveAnswerRating(rating);
        }

        [HttpGet("markbest")]
        public void MarkBestAnswer(int answerId, int questionId, bool isBest)
        {
            _answerService.MarkBestAnswer(answerId, questionId, isBest);
        }
    }
}
