using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace chatroom.Models;

public partial class Chatroom
{
    public int RoomId { get; set; }

    [Required]
    [MaxLength(25, ErrorMessage = "Chat name cannot be longer than 25 characters.")]
    [MinLength(2, ErrorMessage = "Chat name must be at least 2 characters.")]
    public string RoomName { get; set; } = null!;
    
    [MaxLength(100, ErrorMessage = "Description cannot be longer than 100 characters.")]
    public string? Description { get; set; }

    public int? CreatedBy { get; set; }
    
    [Required]
    public virtual ICollection<ChatroomMember> ChatroomMembers { get; set; } = new List<ChatroomMember>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
