using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace chatroom.Models;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chatroom> Chatrooms { get; set; }

    public virtual DbSet<ChatroomMember> ChatroomMembers { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<PrivateMessage> PrivateMessages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=chatroomDB;Trusted_Connection=True;Integrated Security=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chatroom>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Chatroom__328639197A5AA326");

            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.RoomName).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Chatrooms)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Chatrooms__Creat__286302EC");
        });

        modelBuilder.Entity<ChatroomMember>(entity =>
        {
            entity.HasKey(e => e.MembershipId).HasName("PK__Chatroom__92A7859924BDD4FD");

            entity.HasIndex(e => new { e.UserId, e.RoomId }, "UQ__Chatroom__94A0AF3CA46232C7").IsUnique();

            entity.Property(e => e.MembershipId).HasColumnName("MembershipID");
            entity.Property(e => e.IsModerator).HasDefaultValue(false);
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Room).WithMany(p => p.ChatroomMembers)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChatroomM__RoomI__31EC6D26");

            entity.HasOne(d => d.User).WithMany(p => p.ChatroomMembers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChatroomM__UserI__30F848ED");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C037C668F28A9");

            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.SenderId).HasColumnName("SenderID");

            entity.HasOne(d => d.Room).WithMany(p => p.Messages)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__RoomID__2B3F6F97");

            entity.HasOne(d => d.Sender).WithMany(p => p.Messages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__Sender__2C3393D0");
        });

        modelBuilder.Entity<PrivateMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__PrivateM__C87C037C63C5DC32");

            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");
            entity.Property(e => e.SenderId).HasColumnName("SenderID");

            entity.HasOne(d => d.Receiver).WithMany(p => p.PrivateMessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PrivateMe__Recei__35BCFE0A");

            entity.HasOne(d => d.Sender).WithMany(p => p.PrivateMessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PrivateMe__Sende__34C8D9D1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACB3F40C03");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E48EEC60A0").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.PasswordHash).HasMaxLength(128);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Offline");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
