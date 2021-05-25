using Models;
using Services.Helpers;
using Services.Integrate;
using System;
using System.Collections.Generic;

namespace Services
{
    public class QuestionService:IQuestionService
    {
        public void AddQuestion(Question question)
        {
            SqlHelper.Execute(
                "Execute spAddQuestion @0,@1,@2,@3,@4",
                question.Title,question.Description,question.CategoryId,question.UserId,DateTime.Now);
        }

        public IEnumerable<Question> GetQuestions()
        {
            return SqlHelper.Query<Db.Question>("SELECT * FROM Questions")
                .MapTo<IEnumerable<Question>>();
        }

        public IEnumerable<UserQuestions> GetQuestionsByUserId(string userId)
        {
            return SqlHelper.Query<Db.UserQuestions>(
                "SELECT * FROM vWQuestionsByUserId Where UserId=@0",userId)
                .MapTo<IEnumerable<UserQuestions>>();
        }

        public Question GetQuestionById(int questionId)
        {
            return SqlHelper.SingleOrDefault<Db.Question>("SELECT * FROM Questions Where Id=@0", questionId)
                .MapTo<Question>();
        }

        public void IncreaseQuestionViews(int questionId, string userId)
        {
            SqlHelper.Execute(
                "Execute spIncreaseQuestionViews @0,@1",
                questionId, userId 
                );
        }

        public void UpvoteQuestion(QuestionUpvote upvoteInfo)
        {
            var upvote = GetUpvoteInfo(upvoteInfo);

            if (upvote == null)
            {
                SqlHelper.Execute(
                    "Execute spUpvoteNewQuestion @0,@1,@2",
                    upvoteInfo.QuestionId, upvoteInfo.UserId, upvoteInfo.Upvote
                    );
            }
            else
            {
                SqlHelper.Execute(
                    "Execute spUpvoteOldQuestion @0,@1,@2",
                    upvoteInfo.QuestionId, upvoteInfo.UserId, upvoteInfo.Upvote
                    );
            }
        }

        public QuestionUpvote GetUpvoteInfo(QuestionUpvote upvoteInfo)
        {
            return SqlHelper.SingleOrDefault<Db.QuestionUpvote>(
                "SELECT * FROM QuestionUpvotes WHERE QuestionId=@0 and UserId=@1",
                upvoteInfo.QuestionId, upvoteInfo.UserId
                ).MapTo<QuestionUpvote>();
        }

        public IEnumerable<UserQuestions> SearchQuestions(string keyword, int categoryId, int searchCriteria, int searchTime, string userId)
        {
            var sql = $"SELECT * FROM vWQuestionsByUserId" +
                $" WHERE Title LIKE '%{keyword}%'";

            if (categoryId != 0)
            {
                sql += $" AND CategoryId={categoryId}";
            }
            if (searchCriteria == 1)
            {
                sql += $" AND UserId='{userId} '";
            }
            else if (searchCriteria == 2)
            {
                sql += $" AND Questions.Id IN (SELECT DISTINCT QuestionId FROM Answers WHERE UserId = '{userId}')";
            }
            else if (searchCriteria == 3)
            {
                sql += " AND IsResolved=1";
            }
            else if (searchCriteria == 4)
            {
                sql += " AND IsResolved=0";
            }
            if (searchTime != 0)
            {
                sql += $" AND CreatedOn >= DATEADD(day,-{searchTime}, GETDATE())";
            }

            return SqlHelper.Query<UserQuestions>(sql);
        }
    }
}
