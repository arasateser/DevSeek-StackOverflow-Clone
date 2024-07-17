namespace DevSeek.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public User Author { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Vote> Votes { get; set; }
        public List<Tag> Tags { get; set; }
    }
}