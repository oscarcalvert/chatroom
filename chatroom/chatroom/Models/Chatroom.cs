using System;
using System.Collections.Generic;

namespace chatroom.Models;

public partial class Chatroom
{
    public int RoomId { get; set; }

    public string RoomName { get; set; } = null!;

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public virtual ICollection<ChatroomMember> ChatroomMembers { get; set; } = new List<ChatroomMember>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
