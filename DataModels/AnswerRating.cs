using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db
{
    public class AnswerRating
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public string UserId { get; set; }
        public bool Liked { get; set; }
        public bool Disliked { get; set; }
    }
}
