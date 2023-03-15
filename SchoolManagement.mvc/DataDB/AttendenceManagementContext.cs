using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SchoolManagement.mvc.DataDB
{
    public partial class AttendenceManagementContext : DbContext
    {
        public AttendenceManagementContext()
        {
        }

        public AttendenceManagementContext(DbContextOptions<AttendenceManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AttendenceDetail> AttendenceDetails { get; set; } = null!;
        public virtual DbSet<AttendenceTable> AttendenceTables { get; set; } = null!;
        public virtual DbSet<CourseOfferedTable> CourseOfferedTables { get; set; } = null!;
        public virtual DbSet<CourseTable> CourseTables { get; set; } = null!;
        public virtual DbSet<RoomTable> RoomTables { get; set; } = null!;
        public virtual DbSet<StudentEnrollmentTable> StudentEnrollmentTables { get; set; } = null!;
        public virtual DbSet<StudentTable> StudentTables { get; set; } = null!;
        public virtual DbSet<TeacherAllocationTable> TeacherAllocationTables { get; set; } = null!;
        public virtual DbSet<TeacherTable> TeacherTables { get; set; } = null!;
        public virtual DbSet<TimeSlot> TimeSlots { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-DN9MBNB;Database=Attendence Management;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttendenceDetail>(entity =>
            {
                entity.HasKey(e => e.AdId);

                entity.ToTable("Attendence Details");

                entity.Property(e => e.AdId).HasColumnName("AD_ID");

                entity.Property(e => e.AmId)
                    .HasColumnName("AM_ID")
                    .HasComment("Attendence ID");

                entity.Property(e => e.IsPresent).HasColumnName("Is Present");

                entity.Property(e => e.StudentId).HasColumnName("Student_ID");

                entity.HasOne(d => d.Am)
                    .WithMany(p => p.AttendenceDetails)
                    .HasForeignKey(d => d.AmId)
                    .HasConstraintName("FK_Attendence Details_Attendence Table");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.AttendenceDetails)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Attendence Details_Student Table");
            });

            modelBuilder.Entity<AttendenceTable>(entity =>
            {
                entity.HasKey(e => e.AmId)
                    .HasName("PK_Attendence Tabl");

                entity.ToTable("Attendence Table");

                entity.Property(e => e.AmId).HasColumnName("AM_ID");

                entity.Property(e => e.CoId)
                    .HasColumnName("CO ID")
                    .HasComment("Course Offered ID");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasComment("DD/MM/YYYY");

                entity.HasOne(d => d.Co)
                    .WithMany(p => p.AttendenceTables)
                    .HasForeignKey(d => d.CoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attendence Table_Course Offered Table");
            });

            modelBuilder.Entity<CourseOfferedTable>(entity =>
            {
                entity.HasKey(e => e.CoId);

                entity.ToTable("Course Offered Table");

                entity.Property(e => e.CoId).HasColumnName("CO_ID");

                entity.Property(e => e.CourseId).HasColumnName("Course_ID");

                entity.Property(e => e.TsId)
                    .HasColumnName("TS_ID")
                    .HasComment("Time Slot ID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseOfferedTables)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Course Offered Table_Course Table");
            });

            modelBuilder.Entity<CourseTable>(entity =>
            {
                entity.HasKey(e => e.CourseId);

                entity.ToTable("Course Table");

                entity.Property(e => e.CourseId).HasColumnName("Course_ID");

                entity.Property(e => e.CourseName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Course Name");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Short Name");
            });

            modelBuilder.Entity<RoomTable>(entity =>
            {
                entity.HasKey(e => e.RoomId);

                entity.ToTable("Room Table");

                entity.Property(e => e.RoomId).HasColumnName("Room_ID");

                entity.Property(e => e.RoomName)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Room Name");
            });

            modelBuilder.Entity<StudentEnrollmentTable>(entity =>
            {
                entity.HasKey(e => e.SeId);

                entity.ToTable("Student Enrollment Table");

                entity.Property(e => e.SeId).HasColumnName("SE_ID");

                entity.Property(e => e.CoId)
                    .HasColumnName("CO_ID")
                    .HasComment("Course Offer ID");

                entity.Property(e => e.StuId)
                    .HasColumnName("Stu_ID")
                    .HasComment("Student Id");

                entity.HasOne(d => d.Co)
                    .WithMany(p => p.StudentEnrollmentTables)
                    .HasForeignKey(d => d.CoId)
                    .HasConstraintName("FK_Student Enrollment Table_Student Table");

                entity.HasOne(d => d.Stu)
                    .WithMany(p => p.StudentEnrollmentTables)
                    .HasForeignKey(d => d.StuId)
                    .HasConstraintName("FK_Student Enrollment Table_Student Table1");
            });

            modelBuilder.Entity<StudentTable>(entity =>
            {
                entity.HasKey(e => e.StudentRegId);

                entity.ToTable("Student Table");

                entity.Property(e => e.StudentRegId).HasColumnName("Student_RegID");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("Date Of Birth");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Mobile_No");

                entity.Property(e => e.StudentName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Student Name");
            });

            modelBuilder.Entity<TeacherAllocationTable>(entity =>
            {
                entity.HasKey(e => e.TaId);

                entity.ToTable("Teacher Allocation Table");

                entity.Property(e => e.TaId).HasColumnName("TA_ID");

                entity.Property(e => e.CoId)
                    .HasColumnName("CO_ID")
                    .HasComment("Course Offered ID");

                entity.Property(e => e.TId)
                    .HasColumnName("T_ID")
                    .HasComment("Teacher ID");

                entity.HasOne(d => d.Co)
                    .WithMany(p => p.TeacherAllocationTables)
                    .HasForeignKey(d => d.CoId)
                    .HasConstraintName("FK_Teacher Allocation Table_Course Offered Table");

                entity.HasOne(d => d.TIdNavigation)
                    .WithMany(p => p.TeacherAllocationTables)
                    .HasForeignKey(d => d.TId)
                    .HasConstraintName("FK_Teacher Allocation Table_Teacher Table");
            });

            modelBuilder.Entity<TeacherTable>(entity =>
            {
                entity.HasKey(e => e.TeacherId);

                entity.ToTable("Teacher Table");

                entity.Property(e => e.TeacherId).HasColumnName("Teacher_ID");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("Date of Birth")
                    .HasComment("DD/MM/YYYY");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.JoiningDate)
                    .HasColumnType("date")
                    .HasColumnName("Joining Date")
                    .HasComment("DD/MM/YYYY");

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Mobile No");

                entity.Property(e => e.TeacherName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Teacher Name");
            });

            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.HasKey(e => e.TimeId);

                entity.ToTable("Time Slot");

                entity.Property(e => e.TimeId).HasColumnName("Time_ID");

                entity.Property(e => e.FromTime).HasColumnName("From Time");

                entity.Property(e => e.RoomId).HasColumnName("Room ID");

                entity.Property(e => e.ToTime).HasColumnName("To Time");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.TimeSlots)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_Time Slot_Room Table");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
