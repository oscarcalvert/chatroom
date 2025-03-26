using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using chatroom.Models;

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
                dbcontext.Users.FirstOrDefault(u => u.UserId == uid);
            }
            base.OnActionExecuting(context);
        }
    }
}
