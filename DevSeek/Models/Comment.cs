namespace DevSeek.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public ApplicationUser UserId { get; set; }
        public Question QuestionId { get; set; }
    }
}