using Models;
using System.Collections.Generic;

namespace Services.Integrate
{
    public interface IQuestionService
    {
        public void AddQuestion(Question question);
        public IEnumerable<UserQuestions> GetQuestionsByUserId(string userId);
        public Question GetQuestionById(int questionId);
        public QuestionUpvote GetUpvoteInfo(QuestionUpvote upvoteInfo);
        public void UpvoteQuestion(QuestionUpvote upvoteInfo);
        public void IncreaseQuestionViews(int questionId, string userId);
        public IEnumerable<UserQuestions> SearchQuestions(string keyword, int categoryId, int searchCriteria, int searchTime, string userId);
    }
}
