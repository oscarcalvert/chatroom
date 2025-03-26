namespace chatroom.Middleware;

public class SessionCheck
{
    private readonly RequestDelegate _next;

    public SessionCheck(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path;
        
        if (
            path.StartsWithSegments("/Styles") ||
            path.StartsWithSegments("/Scripts") ||
            path.StartsWithSegments("/Media"))
        {
            await _next(context);
            return;
        }

        var uid = context.Session.GetInt32("UID");

        if (uid == null && !context.Request.Path.StartsWithSegments("/Auth"))
        {
            context.Response.Redirect("/Auth/Login");
            return;
        } else if (
                    uid != null && 
                   context.Request.Path.StartsWithSegments("/Auth") && 
                   !context.Request.Path.StartsWithSegments("/Auth/Logout") && 
                   !context.Request.Path.StartsWithSegments("/Auth/Delete")
                   )
        {
            context.Response.Redirect("/Home");
            return;
        }
        
        await _next(context);
    }
}