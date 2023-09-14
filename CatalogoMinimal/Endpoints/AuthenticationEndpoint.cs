using CatalogoMinimal.Models;
using CatalogoMinimal.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CatalogoMinimal.Endpoints;

public static class AuthenticationEndpoint
{
    public static void MapAutenticationEndpoint(this WebApplication app)
    {
        app.MapPost("/login", [AllowAnonymous] (User user, ITokenService tokenService) =>
        {
            if (user == null)
                return Results.BadRequest("Login Inválido");

            if (user.Username == "teste" && user.Password == "1q2w3e4r")
            {
                var tokenString = tokenService.GenerateToken
                (
                    app.Configuration["Jwt:Key"],
                    app.Configuration["Jwt:Issuer"],
                    app.Configuration["Jwt:Audience"],
                    user
                );
                return Results.Ok(new { token = tokenString });
            }
            else
            {
                return Results.BadRequest("Login Inválido");
            }
        }).Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK)
            .WithName("Login")
            .WithTags("/autenticacao");
    }
}
