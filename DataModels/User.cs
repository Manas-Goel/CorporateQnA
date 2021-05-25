namespace Db
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string JobProfile { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string ProfileImageUrl { get; set; }

        public int QuestionsAsked { get; set; }
        public int QuestionsAnswered { get; set; }
        public int QuestionsSolved { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
