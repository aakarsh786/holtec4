using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace holtec4.Models
{
    public partial class holtec4Context : DbContext
    {
        public holtec4Context()
        {
        }

        public holtec4Context(DbContextOptions<holtec4Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Alluserdatum> Alluserdata { get; set; } = null!;
        public virtual DbSet<Applicant> Applicants { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Signup> Signups { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=HA-NB29\\SQLEXPRESS;Database=holtec4;Integrated Security=True;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alluserdatum>(entity =>
            {
                entity.HasKey(e => e.Alluserid)
                    .HasName("PK__alluserd__D8720BA69549ACE5");

                entity.ToTable("alluserdata");

                entity.Property(e => e.Alluserid)
                    .ValueGeneratedNever()
                    .HasColumnName("alluserid");

                entity.Property(e => e.Aiml).HasColumnName("aiml");

                entity.Property(e => e.Angular).HasColumnName("angular");

                entity.Property(e => e.Autocad).HasColumnName("autocad");

                entity.Property(e => e.Bachelors)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("bachelors");

                entity.Property(e => e.BachelorsMarks)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("bachelors_marks");

                entity.Property(e => e.BachelorsYear).HasColumnName("bachelors_year");

                entity.Property(e => e.C).HasColumnName("c#");

                entity.Property(e => e.ClassX)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("class_x");

                entity.Property(e => e.ClassXii)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("class_xii");

                entity.Property(e => e.Company1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("company_1");

                entity.Property(e => e.Company2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("company_2");

                entity.Property(e => e.Company3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("company_3");

                entity.Property(e => e.Company4)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("company_4");

                entity.Property(e => e.Company5)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("company_5");

                entity.Property(e => e.ComputerGraphics).HasColumnName("computer_graphics");

                entity.Property(e => e.Cplusplus).HasColumnName("cplusplus");

                entity.Property(e => e.Dateofbirth)
                    .HasColumnType("datetime")
                    .HasColumnName("dateofbirth");

                entity.Property(e => e.Diploma)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("diploma");

                entity.Property(e => e.DiplomaMarks)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("diploma_marks");

                entity.Property(e => e.DiplomaYear).HasColumnName("diploma_year");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("firstname");

                entity.Property(e => e.Flutter).HasColumnName("flutter");

                entity.Property(e => e.Html).HasColumnName("html");

                entity.Property(e => e.Java).HasColumnName("java");

                entity.Property(e => e.Javascript).HasColumnName("javascript");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lastname");

                entity.Property(e => e.Masters)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("masters");

                entity.Property(e => e.MastersMarks)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("masters_marks");

                entity.Property(e => e.MastersYear).HasColumnName("masters_year");

                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");

                entity.Property(e => e.Profilepic).HasColumnName("profilepic");

                entity.Property(e => e.Python).HasColumnName("python");

                entity.Property(e => e.React).HasColumnName("react");

                entity.Property(e => e.Resume).HasColumnName("resume");

                entity.Property(e => e.Role1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("role_1");

                entity.Property(e => e.Role2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("role_2");

                entity.Property(e => e.Role3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("role_3");

                entity.Property(e => e.Role4)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("role_4");

                entity.Property(e => e.Role5)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("role_5");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Userrole)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("userrole");

                entity.Property(e => e.XMarks)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("x_marks");

                entity.Property(e => e.XYear).HasColumnName("x_year");

                entity.Property(e => e.XiiMarks)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("xii_marks");

                entity.Property(e => e.XiiYear).HasColumnName("xii_year");

                entity.Property(e => e.Years1).HasColumnName("years_1");

                entity.Property(e => e.Years2).HasColumnName("years_2");

                entity.Property(e => e.Years3).HasColumnName("years_3");

                entity.Property(e => e.Years4).HasColumnName("years_4");

                entity.Property(e => e.Years5).HasColumnName("years_5");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Alluserdata)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__alluserda__useri__3E52440B");
            });

            modelBuilder.Entity<Applicant>(entity =>
            {
                entity.ToTable("applicants");

                entity.Property(e => e.ApplicantId).HasColumnName("applicant_id");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Applicants)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK__applicant__job_i__440B1D61");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Applicants)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__applicant__useri__44FF419A");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.Property(e => e.DepartmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("department_id");

                entity.Property(e => e.DepartmentManager)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("department_manager");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("department_name");

                entity.Property(e => e.ManagerEmployeeId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("manager_employee_id");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("jobs");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.Deadline)
                    .HasColumnType("datetime")
                    .HasColumnName("deadline");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Experience).HasColumnName("experience");

                entity.Property(e => e.JobDescription)
                    .IsUnicode(false)
                    .HasColumnName("job_description");

                entity.Property(e => e.JobRequirements)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("job_requirements");

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("job_title");

                entity.Property(e => e.JobType)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("job_type");

                entity.Property(e => e.Location)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.Salary)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("salary");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__jobs__department__3B75D760");
            });

            modelBuilder.Entity<Signup>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("PK__signup__CBA1B257E3536085");

                entity.ToTable("signup");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
