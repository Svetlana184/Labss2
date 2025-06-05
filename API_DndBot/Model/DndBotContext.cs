using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_DndBot.Model;

public partial class DndBotContext : DbContext
{
    public DndBotContext()
    {
    }

    public DndBotContext(DbContextOptions<DndBotContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Fact> Facts { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameImage> GameImages { get; set; }

    public virtual DbSet<GeneratorLocation> GeneratorLocations { get; set; }

    public virtual DbSet<GeneratorVibe> GeneratorVibes { get; set; }

    public virtual DbSet<LocationImage> LocationImages { get; set; }

    public virtual DbSet<Rule> Rules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=GOBLINSCOMP3;Initial Catalog=DndBot;User ID=sa;Password=1234;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fact>(entity =>
        {
            entity.HasKey(e => e.IdFact);

            entity.ToTable("Fact");

            entity.Property(e => e.DescriptionFact).HasColumnType("ntext");
            entity.Property(e => e.ImageFact).HasColumnType("image");
            entity.Property(e => e.NameFact).HasMaxLength(50);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.IdGame);

            entity.ToTable("Game");

            entity.Property(e => e.DescriptionGame).HasColumnType("ntext");
            entity.Property(e => e.Duration).HasMaxLength(50);
            entity.Property(e => e.FromWho).HasMaxLength(50);
            entity.Property(e => e.Genre).HasColumnType("ntext");
            entity.Property(e => e.NameGame).HasMaxLength(50);
            entity.Property(e => e.Setting).HasMaxLength(100);
            entity.Property(e => e.System).HasMaxLength(50);
            entity.Property(e => e.Vibes).HasColumnType("ntext");
        });

        modelBuilder.Entity<GameImage>(entity =>
        {
            entity.HasKey(e => e.IdGameImage);

            entity.ToTable("GameImage");

            entity.Property(e => e.NameImage).HasMaxLength(100);
            entity.Property(e => e.Sourse).HasColumnType("image");

            entity.HasOne(d => d.IdGameNavigation).WithMany(p => p.GameImages)
                .HasForeignKey(d => d.IdGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GameImage_Game");
        });

        modelBuilder.Entity<GeneratorLocation>(entity =>
        {
            entity.HasKey(e => e.IdLocation);

            entity.ToTable("GeneratorLocation");

            entity.Property(e => e.DescriptionLocation).HasColumnType("ntext");
            entity.Property(e => e.FromWho).HasMaxLength(50);
            entity.Property(e => e.NameLocation).HasMaxLength(200);
            entity.Property(e => e.Setting).HasMaxLength(100);
        });

        modelBuilder.Entity<GeneratorVibe>(entity =>
        {
            entity.HasKey(e => e.IdVibes);

            entity.Property(e => e.FromWho).HasMaxLength(50);
            entity.Property(e => e.ImageVibes).HasColumnType("image");
            entity.Property(e => e.NameVibes).HasMaxLength(50);
            entity.Property(e => e.TextVibes).HasColumnType("ntext");
        });

        modelBuilder.Entity<LocationImage>(entity =>
        {
            entity.HasKey(e => e.IdLocationImage);

            entity.ToTable("LocationImage");

            entity.Property(e => e.NameImage).HasMaxLength(100);
            entity.Property(e => e.Source).HasColumnType("image");

            entity.HasOne(d => d.IdLocationNavigation).WithMany(p => p.LocationImages)
                .HasForeignKey(d => d.IdLocation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LocationImage_GeneratorLocation");
        });

        modelBuilder.Entity<Rule>(entity =>
        {
            entity.HasKey(e => e.IdRule);

            entity.ToTable("Rule");

            entity.Property(e => e.Link).HasMaxLength(300);
            entity.Property(e => e.NameRule).HasMaxLength(50);
            entity.Property(e => e.Source).HasColumnType("image");
            entity.Property(e => e.TypeOfRule).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
