using hotel_app.Models;
using hotel_app.Repositories;
using hotel_app.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace hotel_app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //

            //built-in services need to be registered
            //register DBcontext
            builder.Services.AddDbContext<HotelDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("HotelConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });


            //Register Identity Service (userManager -roleMnager- SigninManager)
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                Options =>
                {
                    Options.Password.RequireNonAlphanumeric = false;
                    Options.Password.RequiredLength = 8;


                })
                .AddEntityFrameworkStores<HotelDbContext>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                    .AddDefaultTokenProviders();


            // Register custom services
            builder.Services.AddScoped<IGuestRepository, GuestRepository>();
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
            builder.Services.AddScoped<IHotelRepository, HotelRepository>();
            builder.Services.AddScoped<IFoodRepository, FoodRepository>();
            builder.Services.AddScoped<IRoomCategoryRepository, RoomCategoryRepository>();
            builder.Services.AddScoped<IHotelCategoryRepository, HotelCategoryRepository>();
            builder.Services.AddScoped<IFoodCategoryRepository, FoodCategoryRepository>();
            builder.Services.AddScoped<IGuestRoomRepository, GuestRoomRepository>();
            builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();

            //services 
            builder.Services.AddScoped<IHotelService, HotelService>();
            builder.Services.AddScoped<IGuestService, GuestService>();
            builder.Services.AddScoped<IGeneralRepository<Hotel>, GeneralRepository<Hotel>>();
            builder.Services.AddScoped<IHotelService, HotelService>();
            builder.Services.AddScoped<IHotelCategoryService, HotelCategoryService>();

            builder.Services.AddScoped<IRoomService, RoomService>();
            builder.Services.AddScoped<IFoodService, FoodService>();
            builder.Services.AddScoped<IAppUserService, AppUserService>();

            builder.Services.AddHttpClient();


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
