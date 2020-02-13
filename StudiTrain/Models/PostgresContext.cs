using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudiTrain.Models
{
    public partial class PostgresContext : DbContext
    {
        public PostgresContext()
        {
        }

        public PostgresContext(DbContextOptions<PostgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnswersMc> AnswersMc { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<AnswersMc>(entity =>
            {
                entity.HasKey(e => new { e.QId, e.Number })
                    .HasName("answers_mc_pkey");

                entity.ToTable("answers_mc");

                entity.Property(e => e.QId).HasColumnName("q_id");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.Correct).HasColumnName("correct");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Q)
                    .WithMany(p => p.AnswersMc)
                    .HasForeignKey(d => d.QId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("answers_mc_q_id_fkey");
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.ToTable("questions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Complete)
                    .HasColumnName("complete")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.QuestionText)
                    .IsRequired()
                    .HasColumnName("question_text")
                    .HasColumnType("character varying");

                entity.Property(e => e.QuestionTitle)
                    .HasColumnName("question_title")
                    .HasColumnType("character varying");

                entity.Property(e => e.QuestionType).HasColumnName("question_type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
