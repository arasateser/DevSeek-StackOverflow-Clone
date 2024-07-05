using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DevSeek.Models;
//using System;
namespace DevSeek.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<QuestionTag> QuestionTags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure primary keys, foreign keys, and relationships

            // Configure QuestionTag as a join entity between Question and Tag
            modelBuilder.Entity<QuestionTag>()
                .HasKey(qt => new { qt.QuestionId, qt.TagId }); // Composite primary key for QuestionTag

            modelBuilder.Entity<QuestionTag>()
                .HasOne(qt => qt.Question)
                .WithMany(q => q.QuestionTags)
                .HasForeignKey(qt => qt.QuestionId); // Relationship: Question has many QuestionTags

            modelBuilder.Entity<QuestionTag>()
                .HasOne(qt => qt.Tag)
                .WithMany(t => t.QuestionTags)
                .HasForeignKey(qt => qt.TagId); // Relationship: Tag has many QuestionTags

            // Configure User relationships
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Questions)
                .WithOne(q => q.UserId)
                .HasForeignKey(q => q.UserId); // Relationship: ApplicationUser has many Questions

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.UserId)
                .HasForeignKey(c => c.UserId); // Relationship: ApplicationUser has many Comments

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Votes)
                .WithOne(v => v.User)
                .HasForeignKey(v => v.UserId); // Relationship: ApplicationUser has many Votes

            // Configure Question relationships
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Comments)
                .WithOne(c => c.QuestionId)
                .HasForeignKey(c => c.QuestionId); // Relationship: Question has many Comments

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Votes)
                .WithOne(v => v.QuestionId)
                .HasForeignKey(v => v.QuestionId); // Relationship: Question has many Votes

            modelBuilder.Entity<Question>()
                .HasMany(q => q.QuestionTags)
                .WithOne(qt => qt.Question)
                .HasForeignKey(qt => qt.QuestionId); // Relationship: Question has many QuestionTags

            // Configure Tag relationships
            modelBuilder.Entity<Tag>()
                .HasMany(t => t.QuestionTags)
                .WithOne(qt => qt.Tag)
                .HasForeignKey(qt => qt.TagId); // Relationship: Tag has many QuestionTags
        }
    }
}