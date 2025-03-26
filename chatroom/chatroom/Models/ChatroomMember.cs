using System;
using System.Collections.Generic;

namespace chatroom.Models;

public partial class ChatroomMember
{
    public int MembershipId { get; set; }

    public int UserId { get; set; }

    public int RoomId { get; set; }

    public bool? IsModerator { get; set; }

    public virtual Chatroom Room { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
