using Catalog.Models;
using Catalog.Services;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Endpoints
{
    public static class AuthenticationEndpointsExtensions
    {
        public static void MapAuthenticationEndpoints(this WebApplication app)
        {
            app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
            {
                if (null == userModel)
                {
                    return Results.BadRequest("Invalid login!");
                }

                if (userModel.Name == "user-test" && userModel.Password == "password123")
                {
                    var tokenString = tokenService.GenerateToken(
                        app.Configuration["Jwt:Key"],
                        app.Configuration["Jwt:Issuer"],
                        app.Configuration["Jwt:Audience"],
                        userModel);

                    return Results.Ok(new { Token = tokenString });
                }
                else
                {
                    return Results.BadRequest("Invalid login!");
                }
            })
             .Produces(StatusCodes.Status400BadRequest)
             .Produces(StatusCodes.Status200OK)
             .WithName("Login")
             .WithTags("Athentication");
        }
    }
}
