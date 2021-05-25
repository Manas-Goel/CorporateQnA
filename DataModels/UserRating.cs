using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db
{
    public class UserRating
    {
        public int Id { get; set; }
        public string UserBeingRatedId { get; set; }
        public string UserGivingRatingId { get; set; }
        public bool Liked { get; set; }
        public bool DisLiked { get; set; }
    }
}
