namespace DevSeek.Models
{

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Admin" or "User"

        // List of questions posted by the user
        public List<Question> Questions { get; set; } = new List<Question>();

        // List of comments posted by the user
        public List<Comment> Comments { get; set; } = new List<Comment>();
        // List of votes cast by the user
        public List<Vote> Votes { get; set; } = new List<Vote>();
    }

}