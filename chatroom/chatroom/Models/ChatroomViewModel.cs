using System.ComponentModel.DataAnnotations;

namespace chatroom.Models;

public class ChatroomViewModel:Chatroom
{
    [Required]
    public string Username { get; set; }
}