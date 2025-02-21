using MegaMarket.Repository;
using MegaMarket.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Service;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(option => 
                {
                    option.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
                }
            );

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option => 
                {
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequireDigit = false;
                    option.Password.RequireUppercase = false;
                }
            )
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<IVendorService,VendorService>();
            builder.Services.AddScoped<IProductService,ProductService>();
            builder.Services.AddScoped<IProductRepository,ProductRepository>();
            builder.Services.AddScoped<IVendorRepository,VendorRepository>();
            builder.Services.AddScoped<ISellerRepository,SellerRepository>();
            builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
            builder.Services.AddScoped<ISellerService,SellerService>();


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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
