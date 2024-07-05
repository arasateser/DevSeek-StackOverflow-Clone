namespace DevSeek.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public bool IsUpvote { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Question QuestionId { get; set; }
        public Comment Comment { get; set; }
    }
}