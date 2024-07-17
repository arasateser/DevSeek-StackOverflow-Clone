namespace DevSeek.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        // The user who posted the question
        public User Author { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Vote> Votes { get; set; } = new List<Vote>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }

}