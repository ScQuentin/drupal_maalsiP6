using Drupal.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Drupal.Infrastructure.Database;

public class DrupalDbContext : DbContext
{
    public DrupalDbContext(DbContextOptions<DrupalDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<QuestionEntity> Questions { get; set; }
    public DbSet<AnswerEntity> Answers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration de la table Users
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.Id);

        });

        // Configuration de la table Questions
        modelBuilder.Entity<QuestionEntity>(entity =>
        {
            entity.ToTable("Questions");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Wording)
            .IsRequired()
            .HasMaxLength(500)
            .IsUnicode(true);
        });

        // Configuration de la table Answers (Many-to-Many)
        modelBuilder.Entity<AnswerEntity>(entity =>
        {
            entity.ToTable("Answers");

            // Clé composite (UserId + QuestionId)
            entity.HasKey(e => new { e.UserId, e.QuestionId });

            // Conversion de l'enum en string dans la DB
            entity.Property(e => e.Answer)
                .HasConversion<string>()
                .IsRequired();

            // Relations Many-to-Many
            entity.HasOne(e => e.User)
                .WithMany(u => u.Answers)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}