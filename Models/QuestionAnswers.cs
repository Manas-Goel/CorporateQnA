using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class QuestionAnswers
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int QuestionId { get; set; }
        public string UserId { get; set; }
        public bool IsBestSolution { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
