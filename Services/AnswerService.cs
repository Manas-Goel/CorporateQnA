using Models;
using Services.Helpers;
using Services.Integrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AnswerService:IAnswerService
    {
        public IEnumerable<QuestionAnswers> GetAllAnswersByQuestionId(int questionId)
        {
            return SqlHelper.Query<Db.QuestionAnswers>(
                "Execute spGetAllAnswersByQuestionId @0",
                questionId
                ).MapTo<IEnumerable<QuestionAnswers>>();
        }

        public Answer GetAnswerById(int answerId)
        {
            return SqlHelper.SingleOrDefault<Db.Answer>(
                "SELECT * FROM Answers WHERE Answers.Id = @0",
                answerId
                ).MapTo<Answer>();
        }

        public void AddAnswer(Answer answer)
        {
            SqlHelper.Execute(
                "Execute spAddAnswer @0,@1,@2,@3,@4",
                answer.QuestionId,answer.UserId,answer.Description,answer.IsBestSolution,answer.CreatedOn
                );
        }

        public AnswerRating GetAnswerRating(AnswerRating rate)
        {
            return SqlHelper.SingleOrDefault<Db.AnswerRating>(
                "SELECT * FROM AnswerRatings WHERE AnswerId = @0 and UserId = @1",
                rate.AnswerId, rate.UserId).MapTo<AnswerRating>();
        }

        public AnswerRating GiveAnswerRating(AnswerRating rate)
        {
            var rating = GetAnswerRating(rate);

            if (rating == null)
            {
                SqlHelper.Execute(
                    "Execute spRateNewAnswer @0,@1,@2,@3",
                    rate.Liked,rate.Disliked,rate.AnswerId,rate.UserId
                    );
            }
            else
            {
                SqlHelper.Execute(
                    "Execute spRateOldAnswer @0,@1,@2,@3,@4,@5",
                    rate.Liked, rate.Disliked, rating.Liked, rating.Disliked, rate.AnswerId, rate.UserId
                    );
            }

            return rate;
        }

        public void MarkBestAnswer(int answerId, int questionId, bool isBest)
        {

            SqlHelper.Execute(
                "Execute spMarkBestAnswer @0,@1,@2",
                questionId, answerId, isBest
                );
        }
    }
}
