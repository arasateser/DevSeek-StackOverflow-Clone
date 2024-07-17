namespace DevSeek.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public User Author { get; set; }
        public Question Question { get; set; }
        public List<Vote> Votes { get; set; }
    }

}