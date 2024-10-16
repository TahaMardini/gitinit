using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class TokenMiddleware
{
    private readonly RequestDelegate _next;

    public TokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/Users/Login"))
        {
            await _next(context);
            return;
        }

        var token = context.Session.GetString("JWToken");

        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");

            if (roleClaim != null)
            {
                var identity = new ClaimsIdentity();

                var nameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name");

                if (nameClaim != null)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Name, nameClaim.Value));
                }
                else
                {
                    Console.WriteLine("Unique name claim not found in token.");
                }

                identity.AddClaim(new Claim(ClaimTypes.Role, roleClaim.Value));
                context.User = new ClaimsPrincipal(identity);
            }
        }
        else
        {
            context.Response.Redirect("/Users/Login");
            return;
        }

        await _next(context);
    }
}
