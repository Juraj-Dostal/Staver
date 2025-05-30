using ApiStaver.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiStaver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<StatServerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("StatServerContext")));

            builder.Services.AddControllers();

            var keysFolder = new DirectoryInfo("/home/app/.aspnet/dataProtection-Keys/apiStaver");

            builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(keysFolder)
                .SetApplicationName("ApiStaver");

            builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "ApiStaver", // Napr. URL tvojho API
                        ValidAudience = "http://localhost:8070", // Napr. klienti API
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureSecretKey1234567890123456"))
                    };
                });

            builder.Services.AddAuthorization();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
