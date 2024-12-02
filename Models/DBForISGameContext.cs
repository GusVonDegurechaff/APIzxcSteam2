using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Models;

public partial class DBForISGameContext : DbContext
{
    public DBForISGameContext()
    {
    }

    public DBForISGameContext(DbContextOptions<DBForISGameContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Statistic> Statistics { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserGame> UserGames { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS01;Database=DBforISGame;TrustServerCertificate=True;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasKey(e => e.FriendshipId).HasName("PK__Friends__BC802BCF826EA37E");

            entity.Property(e => e.FriendshipId).HasColumnName("friendship_id");
            entity.Property(e => e.UserId1).HasColumnName("user_id_1");
            entity.Property(e => e.UserId2).HasColumnName("user_id_2");

            entity.HasOne(d => d.UserId1Navigation).WithMany(p => p.Friends)
                .HasForeignKey(d => d.UserId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Friends_Users");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Games__FFE11FCF7CB1688C");

            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.CoverImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cover_image");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Developer)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("developer");
            entity.Property(e => e.GameName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("game_name");
            entity.Property(e => e.Genre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("genre");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.ToTable("Profile");

            entity.Property(e => e.ProfileId).HasColumnName("profile_id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.ProfileName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("profile_name");
            entity.Property(e => e.StatId).HasColumnName("stat_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Game).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_Games");

            entity.HasOne(d => d.User).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_Users");
        });

        modelBuilder.Entity<Statistic>(entity =>
        {
            entity.HasKey(e => e.StatId);

            entity.ToTable("Statistic");

            entity.Property(e => e.StatId).HasColumnName("stat_id");
            entity.Property(e => e.HoursInGame)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hoursInGame");
            entity.Property(e => e.Lvl)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lvl");
            entity.Property(e => e.ProfileId).HasColumnName("profile_id");
            entity.Property(e => e.Rank)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rank");

            entity.HasOne(d => d.Profile).WithMany(p => p.Statistics)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Statistic_Profile");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FBF60CD48");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E616484759438").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC572C8A6ADC8").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.LastLogin).HasColumnName("last_login");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.ProfilePicture)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("profile_picture");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("registration_date");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserGame>(entity =>
        {
            entity.HasKey(e => e.UserGameId).HasName("PK__UserGame__7EF077049E4C1313");

            entity.HasIndex(e => new { e.UserId, e.GameId }, "UQ__UserGame__564026F2A7D7146C").IsUnique();

            entity.Property(e => e.UserGameId).HasColumnName("user_game_id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Game).WithMany(p => p.UserGames)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserGames__game___48CFD27E");

            entity.HasOne(d => d.User).WithMany(p => p.UserGames)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserGames__user___49C3F6B7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
