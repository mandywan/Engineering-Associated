using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AeDirectory.Models
{
/**
* If anyone wants to change this class, please discord @Ricky
* But the recommendation is that inheriting this class and making a new one by any change. 
*/
    public partial class AEV2Context : DbContext
    {
        public AEV2Context()
        {
        }

        public AEV2Context(DbContextOptions<AEV2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyOfficeGroup> CompanyOfficeGroups { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // need to be changed if the DB server is changed
                optionsBuilder.UseSqlServer(
                    "Server=35.182.69.53,1433;Database=AEV2;User Id=backend;Password=CPSC319cpsc319;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.SkillCategoryId)
                    .HasName("pkSkillCategory");

                entity.ToTable("Category");

                entity.Property(e => e.SkillCategoryId)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SortValue)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompanyCode)
                    .HasName("pkLocationCompany");

                entity.ToTable("Company");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CompanyOfficeGroup>(entity =>
            {
                entity.HasKey(e => new {e.CompanyCode, e.OfficeCode, e.GroupCode})
                    .HasName("pkLocationGroup");

                entity.ToTable("CompanyOfficeGroup");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.OfficeCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.GroupCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.CompanyOfficeGroups)
                    .HasForeignKey(d => new {d.CompanyCode, d.OfficeCode})
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COMPANYO_REFERENCE_OFFICE");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeNumber)
                    .HasName("PK_EMPLOYEE");

                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeNumber).ValueGeneratedOnAdd();

                entity.Property(e => e.CompanyCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmploymentType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.GroupCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.HireDate).HasColumnType("date");

                entity.Property(e => e.IsContractor).HasColumnName("isContractor");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocationId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OfficeCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.PhotoUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TerminationDate).HasColumnType("date");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WorkCell)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.WorkPhone)
                    .HasMaxLength(24)
                    .IsUnicode(false);
                
                entity.Property(e => e.Bio)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                
                entity.Property(e => e.ExtraInfo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.YearsPriorExperience).HasColumnType("numeric(3, 1)");

                entity.HasOne(d => d.CompanyCodeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYEE_REFERENCE_COMPANY");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYEE_REFERENCE_LOCATION");

                entity.HasOne(d => d.SupervisorEmployeeNumberNavigation)
                    .WithMany(p => p.InverseSupervisorEmployeeNumberNavigation)
                    .HasForeignKey(d => d.SupervisorEmployeeNumber)
                    .HasConstraintName("FK_EMPLOYEE_REFERENCE_SUPERVISOR_EMPLOYEE");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => new {d.CompanyCode, d.OfficeCode})
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYEE_REFERENCE_OFFICE");

                entity.HasOne(d => d.CompanyOfficeGroup)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => new {d.CompanyCode, d.OfficeCode, d.GroupCode})
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYEE_REFERENCE_COMPANYO");
            });

            modelBuilder.Entity<EmployeeSkill>(entity =>
            {
                entity.HasKey(e => new {e.EmployeeNumber, e.SkillId})
                    .HasName("pkEmployeeSkills");

                entity.Property(e => e.SkillId)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.EmployeeSkills)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYEE_REFERENCE_EMPLOYEE");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.EmployeeSkills)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYEE_REFERENCE_SKILL");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.LocationId)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SortValue)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.HasKey(e => new {e.CompanyCode, e.OfficeCode})
                    .HasName("pkLocationOffice");

                entity.ToTable("Office");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.OfficeCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.CompanyCodeNavigation)
                    .WithMany(p => p.Offices)
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OFFICE_REFERENCE_COMPANY");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("Skill");

                entity.Property(e => e.SkillId)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SkillCategoryId)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.SortValue)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.SkillCategory)
                    .WithMany(p => p.Skills)
                    .HasForeignKey(d => d.SkillCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SKILL_REFERENCE_CATEGORY");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}