using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudiTrain.Models.Database
{
    public partial class PostgresContext : DbContext
    {
        public PostgresContext() { }

        public PostgresContext(string connString) : this(ConstructOptions(connString)) { }

        public PostgresContext(DbContextOptions<PostgresContext> options)
            : base(options) { }

        private static DbContextOptions<PostgresContext> ConstructOptions(string connString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostgresContext>();
            optionsBuilder.UseNpgsql(connString);
            return optionsBuilder.Options;
        }

        public virtual DbSet<AnswersMc> AnswersMc { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<GroupsToPermissions> GroupsToPermissions { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersToGroups> UsersToGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

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

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories");

                entity.HasIndex(e => e.Name)
                    .HasName("categories_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ancestor).HasColumnName("ancestor");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("character varying");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.AncestorNavigation)
                    .WithMany(p => p.InverseAncestorNavigation)
                    .HasForeignKey(d => d.Ancestor)
                    .HasConstraintName("categories_ancestor_fkey");
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.ToTable("groups");

                entity.HasIndex(e => e.Name)
                    .HasName("groups_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<GroupsToPermissions>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.PermissionId })
                    .HasName("groups_to_permissions_pkey");

                entity.ToTable("groups_to_permissions");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupsToPermissions)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("groups_to_permissions_group_id_fkey");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.GroupsToPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("groups_to_permissions_permission_id_fkey");
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.ToTable("permissions");

                entity.HasIndex(e => e.Name)
                    .HasName("permissions_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.ToTable("posts");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.TagId).HasColumnName("tag_id");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("posts_question_id_fkey");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("posts_tag_id_fkey");
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.ToTable("questions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category).HasColumnName("category");

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

                entity.Property(e => e.Tag).HasColumnName("tag");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("questions_category_fkey");

                entity.HasOne(d => d.TagNavigation)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.Tag)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("questions_tag_fkey");
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.ToTable("tags");

                entity.HasIndex(e => e.Name)
                    .HasName("tags_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Username)
                    .HasName("users_username_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("character varying");

                entity.Property(e => e.Passhash)
                    .IsRequired()
                    .HasColumnName("passhash")
                    .HasColumnType("character varying");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<UsersToGroups>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.GroupId })
                    .HasName("users_to_groups_pkey");

                entity.ToTable("users_to_groups");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UsersToGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_to_groups_group_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersToGroups)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("users_to_groups_user_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
