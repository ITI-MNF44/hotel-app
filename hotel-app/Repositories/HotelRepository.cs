using hotel_app.Models;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace hotel_app.Repositories
{
    public class HotelRepository : GeneralRepository<Hotel>, IHotelRepository
    {
<<<<<<< Updated upstream
        public HotelRepository(dbContext DbContext) : base(DbContext)
=======
        //ask
        UserManager<ApplicationUser> usermanager;
        IWebHostEnvironment myEnvironment;
        IGeneralRepository<Hotel> _repository;
        //inject
        public HotelRepository(HotelDbContext dbContext, UserManager<ApplicationUser> _usermanager,
            IWebHostEnvironment hostEnvironment, IGeneralRepository<Hotel> repository) : base(dbContext)
>>>>>>> Stashed changes
        {
            usermanager = _usermanager;
            myEnvironment = hostEnvironment;
            _repository = repository;
        }

        public async Task RegisterInsert(RegisterUserViewModel hotelvm)
        {
            //first add to user table
            //first add to user table
            ApplicationUser user = new ApplicationUser()
            {
                UserName = hotelvm.UserName,
                Email = hotelvm.Email 
            };
            IdentityResult userCreationResult = await usermanager.CreateAsync(user,hotelvm.Password); //hashed done
            if (userCreationResult.Succeeded)
            {
                //get userid
                string userId = user.Id;
                string filename = string.Empty;
                //image part
                if (hotelvm.Image != null)
                {
                    string Uploader = Path.Combine(myEnvironment.WebRootPath, "images");
                    filename = Guid.NewGuid().ToString() + "_" + hotelvm.Image.FileName;
                    string filepath = Path.Combine(Uploader, filename);
                    // Copy image file
                    hotelvm.Image.CopyTo(new FileStream(filepath, FileMode.Create));
                }
                //save the rest of other data 
                Hotel hotel = new Hotel()
                {
                    Name = hotelvm.Name,
                    Description = hotelvm.Description,
                    Country = hotelvm.Country,
                    City = hotelvm.City,
                    Address = hotelvm.Address,
                    StarRating = hotelvm.StarRating,
                    Category = hotelvm.Category,
                    CreatedDate = DateTime.Now,
                    UserId = userId,
                    Image = filename
                };
                //save
                // Add the hotel entity to the context
                _repository.Insert(hotel);
                _repository.Save();

            }
        }
    }
}