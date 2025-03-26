using chatroom.Models;
using Microsoft.AspNetCore.Mvc;

namespace chatroom.Controllers;

public class ChatController:BaseController
{
    private DBContext _db = new DBContext();

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
}