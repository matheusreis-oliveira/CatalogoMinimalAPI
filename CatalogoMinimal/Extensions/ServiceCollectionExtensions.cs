using CatalogoMinimal.Data.Context;
using CatalogoMinimal.Services;
using CatalogoMinimal.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CatalogoMinimal.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureSwagger();
    }

    public static void ConfigureSwagger(this IServiceCollection service)
    {
        service.AddEndpointsApiExplorer();
        service.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalogo API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. " +
                              "Enter 'Bearer'[space]key. Example: \"Bearer 12345abcdef\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                      new OpenApiSecurityScheme
                      {
                          Reference = new OpenApiReference
                          {
                              Type = ReferenceType.SecurityScheme,
                              Id = "Bearer"
                          }
                      },
                     new string[] {}
                }
            });
        });
    }

    public static void AddPersistence(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));



        builder.Services.AddSingleton<ITokenService>(new TokenService());
    }

    public static void AddAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

        builder.Services.AddAuthorization();
    }

}
