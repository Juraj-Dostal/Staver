using ClientStaver.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace ClientStaver
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // Vlastne
            builder.Services.AddTransient<HttpClientHandler>(provider =>
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                return handler;
            });

            builder.Services.AddHttpClient<ApiService>(client =>
            {
                var baseUrl = /*builder.Configuration["ApiSettings:BaseUrl"] ?? */Environment.GetEnvironmentVariable("API_BASE_URL");
                client.BaseAddress = new Uri(baseUrl);
            })
            .ConfigurePrimaryHttpMessageHandler(provider =>
            {
                return provider.GetRequiredService<HttpClientHandler>();
            });

            var keysFolder = new DirectoryInfo("/home/app/.aspnet/dataProtection-Keys/clientStaver");

            builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(keysFolder)
                .SetApplicationName("ClientStaver");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
           
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
