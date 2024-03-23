using hotel_app.Models;
using hotel_app.Repositories;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hotel_app.Controllers
{
    public class HotelController : Controller
    {
        //ask
        dbContext mycontext;
        IWebHostEnvironment myEnvironment;
        UserManager<ApplicationUser> usermanager;
        //repo
        private readonly IHotelCategoryRepository _hotelCategoryRepository;
        public HotelController(dbContext context, IWebHostEnvironment hostEnvironment,
            UserManager<ApplicationUser> usermanagerlogin, IHotelCategoryRepository hotelCategoryRepository)
        {
            mycontext = context;
            myEnvironment = hostEnvironment;
            this.usermanager = usermanagerlogin;
            _hotelCategoryRepository= hotelCategoryRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        //1-open registeration form 
        [HttpGet]
        public IActionResult UserHotelRegister()
        {
            var categories = _hotelCategoryRepository.GetAll()
                                             .Select(category => new SelectListItem
                                             {
                                                 Value = category.Id.ToString(),
                                                 Text = category.Name
                                             })
                                             .ToList();

            ViewBag.CategoryList = categories;
            return View();
        }
        //2-save to db
        [HttpPost]
        public async Task<IActionResult> UserHotelRegister(RegisterUserViewModel hoteluservm)
        {
            //first insert to user table
            if (ModelState.IsValid == true)
            {
                //user first
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = hoteluservm.UserName,
                    Email = hoteluservm.Email,
                    PasswordHash = hoteluservm.Password
                };
                IdentityResult userCreationResult = await usermanager.CreateAsync(user); //hashed
                if (userCreationResult.Succeeded)
                {
                    //get userid
                    string userId = user.Id;
                    //image part
                    string filename = "";
                    if (hoteluservm.photo != null && hoteluservm.photo.Length > 0)
                    {
                        filename = Guid.NewGuid().ToString() + "_" + hoteluservm.photo.FileName;
                        string filepath = Path.Combine(myEnvironment.WebRootPath, "images", filename);

                        using (var stream = new FileStream(filepath, FileMode.Create))
                        {
                            await hoteluservm.photo.CopyToAsync(stream);
                        }
                    }
                    //save the rest of other data 
                    Hotel hotel = new Hotel()
                    {
                        Name = hoteluservm.Name,
                        Description = hoteluservm.Description,
                        Country = hoteluservm.Country,
                        City = hoteluservm.City,
                        Address = hoteluservm.Address,
                        StarRating = hoteluservm.StarRating,
                        Category = hoteluservm.Category,
                        CreatedDate = DateTime.Now,
                        UserId = userId,
                        Image=filename
                    };
                    //save
                    mycontext.Hotels.Add(hotel);
                    await mycontext.SaveChangesAsync();
                    return RedirectToAction("Index");

                }
                else
                {
                    foreach (var item in userCreationResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            else
            {
                return View("UserHotelRegister", hoteluservm);
            }
            return View();
        }

    }
}
