using Models;
using Services.Helpers;
using Services.Integrate;
using System;
using System.Collections.Generic;

namespace Services
{
    public class QuestionService:IQuestionService
    {
        private readonly Database Database;

        public QuestionService()
        {
            Database = new Database("CorporateQnADatabase", "SqlServer");
        }

        public void AddQuestion(Question question)
        {
            try
            {
                Database.BeginTransaction();

                Database.Execute("INSERT INTO Questions(Title,Description,CategoryId,UserId,CreatedOn,Upvotes,Views) " +
                    "VALUES(@0, @1, @2, @3, @4, 0, 0)",
                    question.Title,question.Description,question.CategoryId,question.UserId,DateTime.Now);

                Database.Execute("Update Categories " +
                    "Set QuestionsTagged = QuestionsTagged + 1 " +
                    "Where Id = @0", question.CategoryId);

                Database.Execute("Update UserDetails " +
                    "Set QuestionsAsked = QuestionsAsked + 1 " +
                    "Where Id = @0",question.UserId);

                Database.CommitTransaction();
            }
            catch(Exception e)
            {
                Database.RollbackTransaction();
            }
        }

        public IEnumerable<Question> GetQuestions()
        {
            return Database.Query<Db.Question>("SELECT * FROM Questions")
                .MapTo<IEnumerable<Question>>();
        }

        public IEnumerable<UserQuestions> GetQuestionsByUserId(string userId)
        {
            return Database.Query<Db.UserQuestions>(
                "SELECT * FROM viewUserQuestions Where UserId=@0", userId)
                .MapTo<IEnumerable<UserQuestions>>();
        }

        public Question GetQuestionById(int questionId)
        {
            return Database.SingleOrDefault<Db.Question>("SELECT * FROM Questions Where Id=@0", questionId)
                .MapTo<Question>();
        }

        public void IncreaseQuestionViews(int questionId, string userId)
        {
            int count = Database.SingleOrDefault<int>("SELECT COUNT(*) FROM QuestionViews " +
                "WHERE QuestionId=@0 AND UserId=@1",
                questionId, userId);

            if (count == 0)
            {
                try
                {
                    Database.BeginTransaction();

                    Database.Execute("UPDATE Questions " +
                    "SET Views = Views + 1 " +
                    "WHERE Id = @0", questionId);

                    Database.Execute("INSERT INTO QuestionViews(QuestionId,UserId) " +
                        "VALUES(@0, @1)", questionId, userId);

                    Database.CommitTransaction();
                }
                catch(Exception e)
                {
                    Database.RollbackTransaction();
                }
            }
        }

        public void UpvoteQuestion(QuestionUpvote upvoteInfo)
        {
            var upvote = GetUpvoteInfo(upvoteInfo);

            if (upvote == null)
            {
                UpvoteNewQuestion(upvoteInfo);
            }
            else
            {
                UpvoteOldQuestion(upvoteInfo);
            }
        }

        public QuestionUpvote GetUpvoteInfo(QuestionUpvote upvoteInfo)
        {
            return Database.SingleOrDefault<Db.QuestionUpvote>(
                "SELECT * FROM QuestionUpvotes WHERE QuestionId=@0 and UserId=@1",
                upvoteInfo.QuestionId, upvoteInfo.UserId
                ).MapTo<QuestionUpvote>();
        }

        public IEnumerable<UserQuestions> SearchQuestions(string keyword, int categoryId, int searchCriteria, int searchTime, string userId)
        {
            var sql = $"SELECT * FROM viewUserQuestions" +
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

            return Database.Query<UserQuestions>(sql);
        }

        private void UpvoteNewQuestion(QuestionUpvote upvote)
        {
            try
            {
                Database.BeginTransaction();

                Database.Execute("INSERT INTO QuestionUpvotes(QuestionId,UserId,Upvote) " +
                    "VALUES(@0, @1, @2)",
                    upvote.QuestionId, upvote.UserId, upvote.Upvote);

                Database.Execute("UPDATE Questions " +
                    "SET UpVotes = UpVotes + 1 " +
                    "WHERE Id = @0", upvote.QuestionId);

                Database.CommitTransaction();
            }
            catch(Exception e)
            {
                Database.RollbackTransaction();
            }
        }
        private void UpvoteOldQuestion(QuestionUpvote upvote)
        {
            try
            {
                Database.BeginTransaction();

                Database.Execute("UPDATE QuestionUpvotes " +
                    "SET Upvote = @0 " +
                    "WHERE QuestionId = @1 AND UserId = @2",
                    upvote.Upvote, upvote.QuestionId, upvote.UserId);

                if (upvote.Upvote)
                {
                    Database.Execute("UPDATE Questions " +
                        "SET UpVotes = UpVotes + 1 " +
                        "WHERE Id = @0", upvote.QuestionId);
                }
                else
                {
                    Database.Execute("UPDATE Questions " +
                        "SET UpVotes = UpVotes - 1 " +
                        "WHERE Id = @0", upvote.QuestionId);
                }

                Database.CommitTransaction();
            }
            catch(Exception e)
            {
                Database.RollbackTransaction();
            }
        }
    }
}
