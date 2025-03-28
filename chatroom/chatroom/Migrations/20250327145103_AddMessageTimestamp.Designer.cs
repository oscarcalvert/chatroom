﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using chatroom.Models;

#nullable disable

namespace chatroom.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20250327145103_AddMessageTimestamp")]
    partial class AddMessageTimestamp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("chatroom.Models.Chatroom", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RoomID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomId"));

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RoomId")
                        .HasName("PK__Chatroom__328639197A5AA326");

                    b.HasIndex("CreatedBy");

                    b.ToTable("Chatrooms");
                });

            modelBuilder.Entity("chatroom.Models.ChatroomMember", b =>
                {
                    b.Property<int>("MembershipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MembershipID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MembershipId"));

                    b.Property<bool?>("IsModerator")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("RoomId")
                        .HasColumnType("int")
                        .HasColumnName("RoomID");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("MembershipId")
                        .HasName("PK__Chatroom__92A7859924BDD4FD");

                    b.HasIndex("RoomId");

                    b.HasIndex(new[] { "UserId", "RoomId" }, "UQ__Chatroom__94A0AF3CA46232C7")
                        .IsUnique();

                    b.ToTable("ChatroomMembers");
                });

            modelBuilder.Entity("chatroom.Models.Message", b =>
                {
                    b.Property<long>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("MessageID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MessageId"));

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int")
                        .HasColumnName("RoomID");

                    b.Property<int>("SenderId")
                        .HasColumnType("int")
                        .HasColumnName("SenderID");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("MessageId")
                        .HasName("PK__Messages__C87C037C668F28A9");

                    b.HasIndex("RoomId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("chatroom.Models.PrivateMessage", b =>
                {
                    b.Property<long>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("MessageID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MessageId"));

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int")
                        .HasColumnName("ReceiverID");

                    b.Property<int>("SenderId")
                        .HasColumnType("int")
                        .HasColumnName("SenderID");

                    b.HasKey("MessageId")
                        .HasName("PK__PrivateM__C87C037C63C5DC32");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("PrivateMessages");
                });

            modelBuilder.Entity("chatroom.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("Offline");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId")
                        .HasName("PK__Users__1788CCACB3F40C03");

                    b.HasIndex(new[] { "Username" }, "UQ__Users__536C85E48EEC60A0")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("chatroom.Models.Chatroom", b =>
                {
                    b.HasOne("chatroom.Models.User", "CreatedByNavigation")
                        .WithMany("Chatrooms")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK__Chatrooms__Creat__286302EC");

                    b.Navigation("CreatedByNavigation");
                });

            modelBuilder.Entity("chatroom.Models.ChatroomMember", b =>
                {
                    b.HasOne("chatroom.Models.Chatroom", "Room")
                        .WithMany("ChatroomMembers")
                        .HasForeignKey("RoomId")
                        .IsRequired()
                        .HasConstraintName("FK__ChatroomM__RoomI__31EC6D26");

                    b.HasOne("chatroom.Models.User", "User")
                        .WithMany("ChatroomMembers")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__ChatroomM__UserI__30F848ED");

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("chatroom.Models.Message", b =>
                {
                    b.HasOne("chatroom.Models.Chatroom", "Room")
                        .WithMany("Messages")
                        .HasForeignKey("RoomId")
                        .IsRequired()
                        .HasConstraintName("FK__Messages__RoomID__2B3F6F97");

                    b.HasOne("chatroom.Models.User", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId")
                        .IsRequired()
                        .HasConstraintName("FK__Messages__Sender__2C3393D0");

                    b.Navigation("Room");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("chatroom.Models.PrivateMessage", b =>
                {
                    b.HasOne("chatroom.Models.User", "Receiver")
                        .WithMany("PrivateMessageReceivers")
                        .HasForeignKey("ReceiverId")
                        .IsRequired()
                        .HasConstraintName("FK__PrivateMe__Recei__35BCFE0A");

                    b.HasOne("chatroom.Models.User", "Sender")
                        .WithMany("PrivateMessageSenders")
                        .HasForeignKey("SenderId")
                        .IsRequired()
                        .HasConstraintName("FK__PrivateMe__Sende__34C8D9D1");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("chatroom.Models.Chatroom", b =>
                {
                    b.Navigation("ChatroomMembers");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("chatroom.Models.User", b =>
                {
                    b.Navigation("ChatroomMembers");

                    b.Navigation("Chatrooms");

                    b.Navigation("Messages");

                    b.Navigation("PrivateMessageReceivers");

                    b.Navigation("PrivateMessageSenders");
                });
#pragma warning restore 612, 618
        }
    }
}
