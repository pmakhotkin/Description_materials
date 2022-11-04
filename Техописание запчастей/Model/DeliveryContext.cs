using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Техописание_запчастей.Model
{
    public partial class DeliveryContext : DbContext
    {
        public DeliveryContext()
        {
        }

        public DeliveryContext(DbContextOptions<DeliveryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SparePart> SpareParts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(ConfigurationManager.ConnectionStrings["MySql"].ConnectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.27-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<SparePart>(entity =>
            {
                entity.HasKey(e => e.Material)
                    .HasName("PRIMARY");

                entity.ToTable("spare_parts");

                entity.Property(e => e.Material)
                    .HasMaxLength(50)
                    .HasColumnName("material");

                entity.Property(e => e.CheckComment).HasColumnName("check_comment");

                entity.Property(e => e.Comment)
                    .HasMaxLength(200)
                    .HasColumnName("comment");

                entity.Property(e => e.DateCheckCom)
                    .HasColumnType("datetime")
                    .HasColumnName("date_check_com");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.Division)
                    .HasMaxLength(10)
                    .HasColumnName("division");

                entity.Property(e => e.GrossVolume)
                    .HasMaxLength(20)
                    .HasColumnName("Gross_Volume");

                entity.Property(e => e.GrossWgt)
                    .HasMaxLength(20)
                    .HasColumnName("Gross_Wgt");

                entity.Property(e => e.Height).HasMaxLength(20);

                entity.Property(e => e.Length).HasMaxLength(20);

                entity.Property(e => e.NetHeight)
                    .HasMaxLength(20)
                    .HasColumnName("Net_Height");

                entity.Property(e => e.NetLength)
                    .HasMaxLength(20)
                    .HasColumnName("Net_Length");

                entity.Property(e => e.NetWgt)
                    .HasMaxLength(20)
                    .HasColumnName("Net_Wgt");

                entity.Property(e => e.NetWidth)
                    .HasMaxLength(20)
                    .HasColumnName("Net_Width");

                entity.Property(e => e.Photo)
                    .HasMaxLength(255)
                    .HasColumnName("photo");

                entity.Property(e => e.Photo1)
                    .HasMaxLength(255)
                    .HasColumnName("photo1");

                entity.Property(e => e.RusDescription)
                    .HasMaxLength(1000)
                    .HasColumnName("rus_description");

                entity.Property(e => e.SyncDate).HasColumnType("datetime");

                entity.Property(e => e.Tnved)
                    .HasMaxLength(20)
                    .HasColumnName("TNVED");

                entity.Property(e => e.TnvedDescription)
                    .HasMaxLength(200)
                    .HasColumnName("TNVED_Description");

                entity.Property(e => e.Unity)
                    .HasMaxLength(255)
                    .HasColumnName("unity");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .HasColumnName("user_name");

                entity.Property(e => e.Width).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
