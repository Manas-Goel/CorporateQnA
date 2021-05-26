using AutoMapper;
using CorporateQnA.Models;
using Models;

namespace Services.Profiles
{
    public class CorporateQnAProfile:Profile
    {
        public CorporateQnAProfile() 
        {
            CreateMap<Db.User, User>();
            CreateMap<Db.Answer, Answer>();
            CreateMap<Db.AnswerRating, AnswerRating>();
            CreateMap<Db.Category, Category>();
            CreateMap<Db.Question, Question>();
            CreateMap<Db.QuestionAnswers, QuestionAnswers>();
            CreateMap<Db.QuestionUpvote, QuestionUpvote>();
            CreateMap<Db.UserQuestions, UserQuestions>();
            CreateMap<Db.UserRating, UserRating>();
        }
    }
}
