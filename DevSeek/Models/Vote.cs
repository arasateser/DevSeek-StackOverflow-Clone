namespace DevSeek.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public bool IsUpvote { get; set; }
        public User User { get; set; }
        public Question Question { get; set; }
        public Comment Comment { get; set; }
    }
}