using Models;
using Services.Helpers;
using Services.Integrate;
using System;
using System.Collections.Generic;

namespace Services
{
    public class AnswerService:IAnswerService
    {
        private readonly Database Database;

        public AnswerService()
        {
            Database = new Database("CorporateQnADatabase", "SqlServer");
        }

        public IEnumerable<QuestionAnswers> GetAllAnswersByQuestionId(int questionId)
        {
            return Database.Query<Db.QuestionAnswers>(
                "SELECT * FROM viewQuestionAnswers WHERE QuestionId = @0 " +
                "ORDER BY CreatedOn",
                questionId
                ).MapTo<IEnumerable<QuestionAnswers>>();
        }

        public Answer GetAnswerById(int answerId)
        {
            return Database.SingleOrDefault<Db.Answer>(
                "SELECT * FROM Answers WHERE Answers.Id = @0",
                answerId
                ).MapTo<Answer>();
        }

        public void AddAnswer(Answer answer)
        {
            int count = Database.SingleOrDefault<int>(
                "SELECT COUNT(*) FROM Answers " +
                "WHERE QuestionId=@0 AND UserId=@1",
                answer.QuestionId,answer.UserId);

            try 
            {
                Database.BeginTransaction();

                if (count == 0) 
                {
                    Database.Execute("UPDATE UserDetails " +
                        "SET QuestionsAnswered = QuestionsAnswered + 1 " +
                        "WHERE Id = @0", answer.UserId);
                }

                Database.Execute("INSERT INTO Answers(Description,Likes,Dislikes,QuestionId," +
                    "UserId,IsBestSolution,CreatedOn) " +
                    "VALUES(@0, 0, 0, @1," +
                    " @2, @3, @4)",
                    answer.Description, answer.QuestionId, answer.UserId, answer.IsBestSolution, answer.CreatedOn);

                Database.Execute("UPDATE Questions " +
                    "SET TotalAnswers = TotalAnswers + 1 " +
                    "WHERE Id = @0", answer.QuestionId);

                Database.CommitTransaction();
            }
            catch(Exception e)
            {
                Database.RollbackTransaction();
            }
        }

        public AnswerRating GetAnswerRating(AnswerRating rate)
        {
            return Database.SingleOrDefault<Db.AnswerRating>(
                "SELECT * FROM AnswerRatings " +
                "WHERE AnswerId = @0 and UserId = @1",
                rate.AnswerId, rate.UserId).MapTo<AnswerRating>();
        }

        public AnswerRating GiveAnswerRating(AnswerRating rate)
        {
            var previousRating = GetAnswerRating(rate);

            if (previousRating == null)
            {
                RateNewAnswer(rate);
            }
            else
            {
                RateOldAnswer(rate,previousRating);
            }

            return rate;
        }

        public void MarkBestAnswer(int answerId, int questionId, bool isBest)
        {
            string userId = GetAnswerUserId(answerId, questionId);
            int count = Database.SingleOrDefault<int>("SELECT COUNT(*) FROM Answers " +
                "WHERE QuestionId=@0 AND IsBestSolution=1 AND UserId=@1",
                questionId, userId);

            try
            {
                Database.BeginTransaction();

                if (isBest)
                {
                    if (count == 0)
                    {
                        Database.Execute("UPDATE UserDetails " +
                            "SET QuestionsSolved = QuestionsSolved + 1 " +
                            "WHERE Id = @0", userId);
                    }
                }
                else
                {
                    if (count == 1)
                    {
                        Database.Execute("UPDATE UserDetails " +
                            "SET QuestionsSolved = QuestionsSolved - 1 " +
                            "WHERE Id = @0", userId);
                    }
                }

                Database.Execute("UPDATE Answers " +
                    "SET IsBestSolution = @0 " +
                    "WHERE Id = @1 AND QuestionId = @2", isBest, answerId, questionId);

                count = Database.SingleOrDefault<int>("SELECT COUNT(*) FROM Answers " +
                    "WHERE QuestionId=@0 AND IsBestSolution=1", questionId);

                if (count > 0)
                {
                    Database.Execute("UPDATE Questions " +
                        "SET IsResolved = 1 " +
                        "WHERE Id = @0", questionId);
                }
                else
                {
                    Database.Execute("UPDATE Questions " +
                        "SET IsResolved = 0 " +
                        "WHERE Id = @0", questionId);
                }

                Database.CommitTransaction();
            }
            catch(Exception e)
            {
                Database.RollbackTransaction();
            }
        }

        private void RateNewAnswer(AnswerRating rate)
        {
            try
            {
                Database.BeginTransaction();
                if (rate.Liked && !rate.Disliked)
                {
                    Database.Execute("UPDATE Answers " +
                        "SET Likes = Likes + 1 " +
                        "WHERE Id = @0", rate.AnswerId);
                }
                else if (!rate.Liked && rate.Disliked)
                {
                    Database.Execute("UPDATE Answers " +
                        "SET Dislikes = Dislikes + 1 " +
                        "WHERE Id = @0", rate.AnswerId);
                }

                Database.Execute("INSERT INTO AnswerRatings(AnswerId,UserId,Liked,Disliked) " +
                        "VALUES(@0, @1, @2, @3)",
                        rate.AnswerId, rate.UserId, rate.Liked, rate.Disliked);

                Database.CommitTransaction();
            }
            catch(Exception e)
            {
                Database.RollbackTransaction();
            }
        }

        private void RateOldAnswer(AnswerRating rate,AnswerRating previousRating)
        {
            try
            {
                Database.BeginTransaction();
                if(rate.Liked && !rate.Disliked)
                {
                    if (previousRating.Disliked)
                    {
                        Database.Execute("UPDATE Answers " +
                            "SET Likes = Likes + 1, Dislikes = Dislikes - 1 " +
                            "WHERE Id = @0",rate.AnswerId);
                    }
                    else
                    {
                        Database.Execute("UPDATE Answers " +
                            "SET Likes = Likes + 1 " +
                            "WHERE Id = @0", rate.AnswerId);
                    }
                }
                else if(!rate.Liked && rate.Disliked)
                {
                    if (previousRating.Liked)
                    {
                        Database.Execute("UPDATE Answers " +
                            "SET Likes = Likes - 1, Dislikes = Dislikes + 1 " +
                            "WHERE Id = @0", rate.AnswerId);
                    }
                    else
                    {
                        Database.Execute("UPDATE Answers " +
                            "SET Dislikes = Dislikes + 1 " +
                            "WHERE Id = @0", rate.AnswerId);
                    }
                }
                else
                {
                    if (previousRating.Liked)
                    {
                        Database.Execute("UPDATE Answers " +
                            "SET Likes = Likes - 1 " +
                            "WHERE Id = @0", rate.AnswerId);
                    }
                    else
                    {
                        Database.Execute("UPDATE Answers " +
                            "SET Dislikes = Dislikes - 1 " +
                            "WHERE Id = @0", rate.AnswerId);
                    }
                }

                Database.Execute("UPDATE AnswerRatings " +
                    "SET Liked = @0, Disliked = @1 " +
                    "WHERE AnswerId = @2 AND UserId = @3",
                    rate.Liked, rate.Disliked, rate.AnswerId, rate.UserId);

                Database.CommitTransaction();
            }
            catch(Exception e)
            {
                Database.RollbackTransaction();
            }
        }

        private string GetAnswerUserId(int answerId, int questionId)
        {
            return Database.SingleOrDefault<string>(
                "SELECT UserId FROM Answers " +
                "WHERE QuestionId=@0 AND Id=@1",
                questionId, answerId);
        }
    }
}
