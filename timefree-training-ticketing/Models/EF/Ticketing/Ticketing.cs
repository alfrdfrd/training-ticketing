using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace timefree_training_ticketing.Models.EF.Ticketing;

public partial class Ticketing : DbContext
{
   

    public Ticketing(DbContextOptions<Ticketing> options)
        : base(options)
    {
    }

    public virtual DbSet<order> order { get; set; }

    public virtual DbSet<ticket> ticket { get; set; }

    public virtual DbSet<user> user { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<order>(entity =>
        {
            entity.HasKey(e => e.guid).HasName("PK__order__497F6CB4C4F04505");

            entity.ToTable("order", "ticketing-system");

            entity.Property(e => e.guid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.created_by_ip)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.date_created).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.modified_by_ip)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.ticket).WithMany(p => p.order)
                .HasForeignKey(d => d.ticket_guid)
                .HasConstraintName("FK_order_ticket");

            entity.HasOne(d => d.user).WithMany(p => p.order)
                .HasForeignKey(d => d.user_guid)
                .HasConstraintName("FK_order_user");
        });

        modelBuilder.Entity<ticket>(entity =>
        {
            entity.HasKey(e => e.guid).HasName("PK__ticket__497F6CB4E07C527F");

            entity.ToTable("ticket", "ticketing-system");

            entity.Property(e => e.guid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.created_by_ip)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.date_created).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.event_name).HasMaxLength(200);
            entity.Property(e => e.location).HasMaxLength(200);
            entity.Property(e => e.modified_by_ip)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ticket_type).HasMaxLength(20);
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.guid).HasName("PK__users__497F6CB44F81FB5C");

            entity.ToTable("user", "ticketing-system");

            entity.Property(e => e.guid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.created_by_ip)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.date_created).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.first_name).HasMaxLength(100);
            entity.Property(e => e.last_name).HasMaxLength(100);
            entity.Property(e => e.modified_by_ip)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
