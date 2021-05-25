using CorporateQnA.Models;
using System;

namespace Models
{
    public class UserQuestions
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int UpVotes { get; set; }
        public int Views { get; set; }
        public int TotalAnswers { get; set; }
        public bool IsResolved { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
