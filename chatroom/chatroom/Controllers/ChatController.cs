using chatroom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace chatroom.Controllers;

public class ChatController : BaseController
{
    private DBContext _db = new DBContext();

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(ChatroomViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = ViewBag.User;

            if (model.Username == user.Username)
            {
                ModelState.AddModelError("Username", "Username cannot be your own");
                return View(model);
            }

            var otherUser = _db.Users.FirstOrDefault(u => u.Username == model.Username);
            if (otherUser == null)
            {
                ModelState.AddModelError("Username", "User not found");
                return View(model);
            }

            Chatroom chatroom = new Chatroom
            {
                RoomName = model.RoomName,
                Description = model.Description,
                CreatedBy = user.UserId
            };
            _db.Chatrooms.Add(chatroom);
            _db.SaveChanges();
            
            
            ChatroomMember creator = new ChatroomMember
            {
                UserId = user.UserId,
                RoomId = chatroom.RoomId,
                IsModerator = true
            };
            _db.ChatroomMembers.Add(creator);
            ChatroomMember chatroomMember = new ChatroomMember
            {
                UserId = otherUser.UserId,
                RoomId = chatroom.RoomId,
                IsModerator = false
            };
            _db.ChatroomMembers.Add(chatroomMember);

            _db.SaveChanges();
            Console.WriteLine("Created chatroom");
            return Redirect("/Home");
        }
        return View(model);
    }

    public IActionResult View(int? id)
    {
        if (id == null)
        {
            Console.WriteLine("No chat provided");
            return Redirect("/Home");
        }
        var chat = _db.Chatrooms.FirstOrDefault(c => c.RoomId == id);

        ViewBag.Chat = chat;
        return View();
    }
}