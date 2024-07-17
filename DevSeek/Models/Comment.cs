namespace DevSeek.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        // The user who posted the comment
        public User Author { get; set; }

        public Question Question { get; set; }
        public List<Vote> Votes { get; set; } = new List<Vote>();
    }



}