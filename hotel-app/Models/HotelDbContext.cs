using hotel_app.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hotel_app.Models
{
	public class dbContext:IdentityDbContext<ApplicationUser>
	{
        //Ctor
        public dbContext():base()
        {
            
        }
		//inject context
		public dbContext(DbContextOptions<dbContext> options) : base(options)
		{

		}

		//DB sets
		public DbSet<Food> Foods { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<GuestRoom> GuestsRooms { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelCategory> HotelsCategories { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomCategory> RoomsCategories { get; set; }

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.
		//		UseSqlServer("Data Source=.;Initial Catalog=HotelDB;Integrated Security=True;Encrypt=False");
		//	base.OnConfiguring(optionsBuilder);
		//}


	}
}
