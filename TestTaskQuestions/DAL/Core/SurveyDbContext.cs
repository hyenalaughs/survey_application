using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TestTaskQuestions.Models;

namespace TestTaskQuestions.DAL.Core
{
    public class SurveyDbContext : DbContext
    {
        public SurveyDbContext(DbContextOptions<SurveyDbContext> options)
        : base(options) { }

        public DbSet<Survey> Surveys => Set<Survey>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Answer> Answers => Set<Answer>();
        public DbSet<Interwiew> Interviews => Set<Interwiew>();
        public DbSet<Result> Results => Set<Result>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.ToTable("surveys");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Title).HasColumnName("title").IsRequired();
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.CreatedAt).HasColumnName("createdat");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("questions");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.SurveyId).HasColumnName("surveyid");
                entity.Property(e => e.Text).HasColumnName("text").IsRequired();
                entity.Property(e => e.OrderNumber).HasColumnName("ordernumber");

                entity.HasIndex(q => new { q.SurveyId, q.OrderNumber }).IsUnique();
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("answers");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.QuestionId).HasColumnName("questionid");
                entity.Property(e => e.Text).HasColumnName("text").IsRequired();
            });

            modelBuilder.Entity<Interwiew>(entity =>
            {
                entity.ToTable("interviews");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.SurveyId).HasColumnName("surveyid");
                entity.Property(e => e.StartedAt).HasColumnName("startedat");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.ToTable("results");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.InterviewId).HasColumnName("interviewid");
                entity.Property(e => e.QuestionId).HasColumnName("questionid");
                entity.Property(e => e.AnswerId).HasColumnName("answerid");
                entity.Property(e => e.AnsweredAt).HasColumnName("answeredat");

                entity.HasIndex(r => new { r.InterviewId, r.QuestionId }).IsUnique();
            });
        }
    }
}
