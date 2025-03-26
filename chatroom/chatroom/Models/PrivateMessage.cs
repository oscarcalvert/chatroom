using System;
using System.Collections.Generic;

namespace chatroom.Models;

public partial class PrivateMessage
{
    public long MessageId { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string MessageContent { get; set; } = null!;

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
