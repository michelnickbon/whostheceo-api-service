using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiService.Models
{
    public partial class MainDBContext : DbContext
    {
        public MainDBContext()
        {
        }

        public MainDBContext(DbContextOptions<MainDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ceo> Ceo { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<History> History { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-QK4HUCR;Database=MainDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ceo>(entity =>
            {
                entity.Property(e => e.CeoId)
                    .HasColumnName("ceo_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActiveFrom)
                    .HasColumnName("active_from")
                    .HasColumnType("date");

                entity.Property(e => e.ActiveTo)
                    .HasColumnName("active_to")
                    .HasColumnType("date");

                entity.Property(e => e.FullName)
                    .HasColumnName("full_name")
                    .HasMaxLength(50);

                entity.Property(e => e.ImageRef).HasColumnName("image_ref");

                entity.Property(e => e.ShortBio).HasColumnName("short_bio");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyId)
                    .HasColumnName("company_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CeoId).HasColumnName("ceo_id");

                entity.Property(e => e.CompanyDescription)
                    .IsRequired()
                    .HasColumnName("company_description")
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .HasColumnName("company_name")
                    .HasMaxLength(50);

                entity.Property(e => e.CompanyWebsite)
                    .HasColumnName("company_website")
                    .IsUnicode(false);

                entity.HasOne(d => d.Ceo)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.CeoId)
                    .HasConstraintName("FK_Company_Ceo");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.Property(e => e.HistoryId)
                    .HasColumnName("history_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.LoggedAt)
                    .HasColumnName("logged_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.History)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_History_Company");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
