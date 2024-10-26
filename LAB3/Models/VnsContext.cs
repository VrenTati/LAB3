using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LAB3.Models;

public partial class VnsContext : DbContext
{
    public VnsContext()
    {
    }

    public VnsContext(DbContextOptions<VnsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccessControl> AccessControls { get; set; }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Glossary> Glossaries { get; set; }

    public virtual DbSet<Lecturer> Lecturers { get; set; }

    public virtual DbSet<Resource> Resources { get; set; }

    public virtual DbSet<ResourceProperty> ResourceProperties { get; set; }

    public virtual DbSet<Subunit> Subunits { get; set; }

    public virtual DbSet<Survey> Surveys { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<Upload> Uploads { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=vns;Username=postgres;Password=2281");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessControl>(entity =>
        {
            entity.HasKey(e => e.AccessId).HasName("access_control_pkey");

            entity.ToTable("access_control");

            entity.Property(e => e.AccessId).HasColumnName("access_id");
            entity.Property(e => e.AccessEnd)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("access_end");
            entity.Property(e => e.AccessStart)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("access_start");
            entity.Property(e => e.ResourceId).HasColumnName("resource_id");
            entity.Property(e => e.Restricted)
                .HasDefaultValue(false)
                .HasColumnName("restricted");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasColumnName("role");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Resource).WithMany(p => p.AccessControls)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("access_control_resource_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.AccessControls)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("access_control_user_id_fkey");
        });

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("assignments_pkey");

            entity.ToTable("assignments");

            entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Course).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("assignments_course_id_fkey");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("courses_pkey");

            entity.ToTable("courses");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CourseName)
                .HasMaxLength(255)
                .HasColumnName("course_name");
            entity.Property(e => e.LecturerId).HasColumnName("lecturer_id");

            entity.HasOne(d => d.Lecturer).WithMany(p => p.Courses)
                .HasForeignKey(d => d.LecturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("courses_lecturer_id_fkey");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("feedback_pkey");

            entity.ToTable("feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.DateProvided)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_provided");
            entity.Property(e => e.FeedbackText).HasColumnName("feedback_text");
            entity.Property(e => e.ProvidedBy).HasColumnName("provided_by");

            entity.HasOne(d => d.Course).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("feedback_course_id_fkey");

            entity.HasOne(d => d.ProvidedByNavigation).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ProvidedBy)
                .HasConstraintName("feedback_provided_by_fkey");
        });

        modelBuilder.Entity<Glossary>(entity =>
        {
            entity.HasKey(e => e.GlossaryId).HasName("glossaries_pkey");

            entity.ToTable("glossaries");

            entity.Property(e => e.GlossaryId).HasColumnName("glossary_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Definition).HasColumnName("definition");
            entity.Property(e => e.Term)
                .HasMaxLength(255)
                .HasColumnName("term");

            entity.HasOne(d => d.Course).WithMany(p => p.Glossaries)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("glossaries_course_id_fkey");
        });

        modelBuilder.Entity<Lecturer>(entity =>
        {
            entity.HasKey(e => e.LecturerId).HasName("lecturers_pkey");

            entity.ToTable("lecturers");

            entity.HasIndex(e => e.Email, "lecturers_email_key").IsUnique();

            entity.Property(e => e.LecturerId).HasColumnName("lecturer_id");
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .HasColumnName("department");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.LecturerName)
                .HasMaxLength(255)
                .HasColumnName("lecturer_name");
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.ResourceId).HasName("resources_pkey");

            entity.ToTable("resources");

            entity.Property(e => e.ResourceId).HasColumnName("resource_id");
            entity.Property(e => e.Alignment)
                .HasMaxLength(20)
                .HasColumnName("alignment");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ResourceType)
                .HasMaxLength(50)
                .HasColumnName("resource_type");
            entity.Property(e => e.Tags).HasColumnName("tags");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Tooltip).HasColumnName("tooltip");

            entity.HasOne(d => d.Course).WithMany(p => p.Resources)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("resources_course_id_fkey");
        });

        modelBuilder.Entity<ResourceProperty>(entity =>
        {
            entity.HasKey(e => e.PropertyId).HasName("resource_properties_pkey");

            entity.ToTable("resource_properties");

            entity.Property(e => e.PropertyId).HasColumnName("property_id");
            entity.Property(e => e.ResourceId).HasColumnName("resource_id");
            entity.Property(e => e.SettingType)
                .HasMaxLength(50)
                .HasColumnName("setting_type");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.Visibility)
                .HasDefaultValue(true)
                .HasColumnName("visibility");

            entity.HasOne(d => d.Resource).WithMany(p => p.ResourceProperties)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("resource_properties_resource_id_fkey");
        });

        modelBuilder.Entity<Subunit>(entity =>
        {
            entity.HasKey(e => e.SubunitId).HasName("subunits_pkey");

            entity.ToTable("subunits");

            entity.Property(e => e.SubunitId).HasColumnName("subunit_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Subtitle)
                .HasMaxLength(255)
                .HasColumnName("subtitle");
            entity.Property(e => e.TextContent).HasColumnName("text_content");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Course).WithMany(p => p.Subunits)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subunits_course_id_fkey");
        });

        modelBuilder.Entity<Survey>(entity =>
        {
            entity.HasKey(e => e.SurveyId).HasName("surveys_pkey");

            entity.ToTable("surveys");

            entity.Property(e => e.SurveyId).HasColumnName("survey_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.SurveyTitle)
                .HasMaxLength(255)
                .HasColumnName("survey_title");

            entity.HasOne(d => d.Course).WithMany(p => p.Surveys)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("surveys_course_id_fkey");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("topics_pkey");

            entity.ToTable("topics");

            entity.Property(e => e.TopicId).HasColumnName("topic_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.TopicName)
                .HasMaxLength(255)
                .HasColumnName("topic_name");

            entity.HasOne(d => d.Course).WithMany(p => p.Topics)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("topics_course_id_fkey");
        });

        modelBuilder.Entity<Upload>(entity =>
        {
            entity.HasKey(e => e.UploadId).HasName("uploads_pkey");

            entity.ToTable("uploads");

            entity.Property(e => e.UploadId).HasColumnName("upload_id");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .HasColumnName("file_path");
            entity.Property(e => e.ResourceId).HasColumnName("resource_id");
            entity.Property(e => e.UploadDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("upload_date");
            entity.Property(e => e.UploadedBy).HasColumnName("uploaded_by");

            entity.HasOne(d => d.Resource).WithMany(p => p.Uploads)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("uploads_resource_id_fkey");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.Uploads)
                .HasForeignKey(d => d.UploadedBy)
                .HasConstraintName("uploads_uploaded_by_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
