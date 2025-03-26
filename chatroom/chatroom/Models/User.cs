using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace chatroom.Models;

public partial class User
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    public string PasswordHash { get; set; } = null!;

    public string? Status { get; set; }

    public virtual ICollection<ChatroomMember> ChatroomMembers { get; set; } = new List<ChatroomMember>();

    public virtual ICollection<Chatroom> Chatrooms { get; set; } = new List<Chatroom>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<PrivateMessage> PrivateMessageReceivers { get; set; } = new List<PrivateMessage>();

    public virtual ICollection<PrivateMessage> PrivateMessageSenders { get; set; } = new List<PrivateMessage>();
}
