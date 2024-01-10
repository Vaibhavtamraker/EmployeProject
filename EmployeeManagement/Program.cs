using EmployeeManagement.Models.Repositories.Abstract;
using EmployeeManagement.Models.Repositories.Implementation;
using Microsoft.AspNetCore.Identity;
namespace EmployeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IUserAuthentication, UserAuthentication>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IHRService, HRService>();
            builder.Services.AddScoped<IAdminService, AdminService>();

            var app = builder.Build();
            

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=UserAuthentication}/{action=Login}/{id?}");

            app.Run();
        }
    }
}