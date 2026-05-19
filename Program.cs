using HocTiengTrung.Data;
using Microsoft.EntityFrameworkCore;

namespace HocTiengTrung
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // CHO PHÉP CH?Y TRÊN SERVER RENDER
            builder.WebHost.UseUrls("http://0.0.0.0:10000");

            // MVC
            builder.Services.AddControllersWithViews();

            // SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration
                    .GetConnectionString("KetNoiMacDinh")
                ));

            var app = builder.Build();

            // Configure pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            // Có th? gi? ho?c comment n?u b? redirect l?i
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Routing
            app.MapControllerRoute(
                name: "default",
                pattern:
                "{controller=Home}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}