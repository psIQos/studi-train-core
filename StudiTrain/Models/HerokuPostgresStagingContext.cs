using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudiTrain.Models
{
    public partial class HerokuPostgresStagingContext : DbContext
    {
        public HerokuPostgresStagingContext()
        {
        }

        public HerokuPostgresStagingContext(DbContextOptions<HerokuPostgresStagingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnswersMc> AnswersMc { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=ec2-54-246-90-10.eu-west-1.compute.amazonaws.com;Database=d7au6sj4t97jm9;Username=upwrknlermayjr;Password=2656247f7be30b3be6665486160de4b761f6240a5df735347121be4d53c09450;SslMode=Require;Trust Server Certificate=True");
            }
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
