using System;
using System.Collections.Generic;

namespace chatroom.Models;

public partial class Message
{
    public long MessageId { get; set; }

    public int RoomId { get; set; }

    public int SenderId { get; set; }

    public DateTime Timestamp { get; set; }

    public string MessageContent { get; set; } = null!;

    public virtual Chatroom Room { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
