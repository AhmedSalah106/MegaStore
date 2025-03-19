using MegaMarket.Repository;
using MegaMarket.Service;
using MegaStore.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
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

            builder.Services.AddSignalR();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option => 
                {
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequireDigit = false;
                    option.Password.RequireUppercase = false;
                }
            )
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<IVendorService,VendorService>();
            builder.Services.AddScoped<IProductService,MegaMarket.Service.ProductService>();
            builder.Services.AddScoped<IProductRepository,ProductRepository>();
            builder.Services.AddScoped<IVendorRepository,VendorRepository>();
            builder.Services.AddScoped<ISellerRepository,SellerRepository>();
            builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
            builder.Services.AddScoped<ISellerService,SellerService>();

            builder.Services.AddSession(option =>
            {
                option.IOTimeout = TimeSpan.FromMinutes(5);
                option.IdleTimeout = TimeSpan.FromMinutes(5);
                option.Cookie.HttpOnly = true;

            });
            builder.Services.AddDistributedMemoryCache();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

            app.UseAuthorization();

            app.MapHub<ProductHub>("/ProductH");


            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
