using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using chatroom.Models;
using Microsoft.EntityFrameworkCore;

namespace chatroom.Controllers
{
    public class BaseController : Controller
    {

        private DBContext dbcontext = new DBContext();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var uid = HttpContext.Session.GetInt32("UID");
            if (uid != null)
            {
                var user = dbcontext.Users.FirstOrDefault(u => u.UserId == uid);
                var chats = dbcontext.Chatrooms
                    .Include(c => c.ChatroomMembers)
                    .Where(c => c.ChatroomMembers.Any(cm => cm.UserId == uid))
                    .ToList();
                ViewBag.Chats = chats;
                ViewBag.User = user;
            }
            base.OnActionExecuting(context);
        }
    }
}
