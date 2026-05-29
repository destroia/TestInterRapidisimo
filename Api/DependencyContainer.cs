using Api.Middleware;
using Business;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api;

public static class DependencyContainer
{
    private static string MyAllowSpecificOrigins = "CorsPolicy";
    public static IServiceCollection AddApiDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAplicationDependency(configuration);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors(options =>
        {
            options.AddPolicy(
            name: MyAllowSpecificOrigins,
            builder =>
            {
                builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
            });
        });

        var jwtSettings = configuration.GetSection("JwtAdmin");
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
                };
            });
        return services;
    }

    public static WebApplication AddApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() || 1 == 1)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapOpenApi();
        }
        app.UseMiddleware<MiddlewareGlobalExceptionHandler>();
        app.UseCors(MyAllowSpecificOrigins);
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}

