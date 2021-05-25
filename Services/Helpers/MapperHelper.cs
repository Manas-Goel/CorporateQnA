using AutoMapper;
using CorporateQnA.Models;
using Models;
using System.Collections.Generic;

namespace Services.Helpers
{
    public static class MapperHelper
    {
        private static IMapper Mapper;

        public static TDest MapTo<TDest>(this object src)
        {
            if (Mapper == null) 
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Db.User, User>();
                    cfg.CreateMap<Db.Answer, Answer>();
                    cfg.CreateMap<Db.AnswerRating, AnswerRating>();
                    cfg.CreateMap<Db.Category, Category>();
                    cfg.CreateMap<Db.Question, Question>();
                    cfg.CreateMap<Db.QuestionAnswers, QuestionAnswers>();
                    cfg.CreateMap<Db.QuestionUpvote, QuestionUpvote>();
                    cfg.CreateMap<Db.UserQuestions, UserQuestions>();
                    cfg.CreateMap<Db.UserRating, UserRating>();
                });
                Mapper = config.CreateMapper();
            }

            return Mapper.Map<TDest>(src);
        }

    }
}