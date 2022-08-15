using AntimalnikAPI.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AntimalnikAPI.DAL
{
    public class AntimalnikDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<IdentityUserClaim<string>> AspNetUserClaims { get; set; }

        public AntimalnikDbContext(DbContextOptions<AntimalnikDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => base.OnConfiguring(optionsBuilder);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
              .HasOne(post => post.Creator)
              .WithMany(creator => creator.Posts)
              .HasForeignKey(post => post.CreatorId);

            modelBuilder.Entity<Message>()
                .HasOne(message => message.Sender)
                .WithMany(sender => sender.SentMessages)
                .HasForeignKey(message => message.SenderId);
        }
    }
}
