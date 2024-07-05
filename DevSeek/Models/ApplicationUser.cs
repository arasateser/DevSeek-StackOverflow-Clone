using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DevSeek.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}