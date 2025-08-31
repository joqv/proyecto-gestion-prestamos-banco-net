namespace GestionPrestamoBancoWeb.Middleware
{
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Ignorar solo el login
            var path = context.Request.Path.Value?.ToLower();

            if (!path.Contains("/login"))
            {
                var token = context.Session.GetString("JWToken");

                if (string.IsNullOrEmpty(token))
                {
                    context.Response.Redirect("/Login/LoginUsuario");
                    return;
                }
            }

            await _next(context);
        }
    }
}
