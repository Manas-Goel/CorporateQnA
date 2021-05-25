using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Integrate
{
    public interface IAnswerService
    {
        public IEnumerable<QuestionAnswers> GetAllAnswersByQuestionId(int questionId);
        public void AddAnswer(Answer answer);
        public AnswerRating GetAnswerRating(AnswerRating rate);
        public AnswerRating GiveAnswerRating(AnswerRating rate);
        public Answer GetAnswerById(int answerId);
        public void MarkBestAnswer(int answerId, int questionId, bool isBest);
    }
}
