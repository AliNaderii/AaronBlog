namespace Aaron.Middlewares
{
    public class EnforceCredentialsChangeMiddleware
    {
        private readonly RequestDelegate _next;
        public EnforceCredentialsChangeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var user = context.User;
            if(user.Identity != null && user.Identity.IsAuthenticated && user.IsInRole("Admin")) {
                var isDefualtClaim = user.FindFirst("IsDefault");

                if(isDefualtClaim != null && bool.TryParse(isDefualtClaim.Value, out var isDefualt) && isDefualt )
                {
                    var path = context.Request.Path.Value?.ToLower();
                    if (!path.StartsWith("/admin/account/changepassword"))
                    {
                        context.Response.Redirect("/admin/account/changepassword");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
