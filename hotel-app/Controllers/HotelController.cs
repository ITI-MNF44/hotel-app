using hotel_app.Models;
using hotel_app.Repositories;
<<<<<<< Updated upstream
=======
using hotel_app.Services;
>>>>>>> Stashed changes
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hotel_app.Controllers
{
    public class HotelController : Controller
    {
        //ask
<<<<<<< Updated upstream
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
=======
        HotelDbContext mycontext;
        UserManager<ApplicationUser> usermanager;
        SignInManager<ApplicationUser> signInManager;
        IHotelCategoryService _categoryService;
        IHotelService _hotelService;
        //Ctor,inject
        public HotelController(HotelDbContext context,
            UserManager<ApplicationUser> usermanagerlogin,IHotelCategoryService categoryService, SignInManager<ApplicationUser> _signInManager,
            IHotelService hotelService) 
        {
            mycontext = context;
            usermanager = usermanagerlogin;
            _categoryService = categoryService;
            _hotelService = hotelService;
            signInManager = _signInManager;
>>>>>>> Stashed changes
        }

        public IActionResult Index()
        {
            return View();
        }
        //1-open registeration form 
        [HttpGet]
        public IActionResult UserHotelRegister()
        {
<<<<<<< Updated upstream
            var categories = _hotelCategoryRepository.GetAll()
                                             .Select(category => new SelectListItem
                                             {
                                                 Value = category.Id.ToString(),
                                                 Text = category.Name
                                             })
                                             .ToList();

            ViewBag.CategoryList = categories;
            return View();
=======
            var categories = _categoryService.GetAllCategories();
            var vm = new RegisterUserViewModel
            {
                Categories = categories
            };
            return View(vm);
>>>>>>> Stashed changes
        }
        //2-save to db
        [HttpPost]
        public async Task<IActionResult> UserHotelRegister(RegisterUserViewModel hotelvm)
        {
<<<<<<< Updated upstream
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
=======
            // Continue with form submission logic
            if (ModelState.IsValid )
            {
                await _hotelService.RegisterInsert(hotelvm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var categories = _categoryService.GetAllCategories();
                hotelvm.Categories = categories;
                return View("UserHotelRegister", hotelvm);
>>>>>>> Stashed changes
            }
        }
<<<<<<< Updated upstream

=======
        //
		public IActionResult Login()
		{
			return View("HotelLogin");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(UserLoginVIewModel hotelVM)
		{
            if (ModelState.IsValid)
            {
                ApplicationUser AppUser = await usermanager.FindByNameAsync(hotelVM.Username);
                if (AppUser != null)
                {
                    bool Found = hotelVM.Passwrod.Equals(AppUser.PasswordHash);
                    if (Found)
                    {
                        await signInManager.SignInAsync(AppUser, hotelVM.RememberMe);
                        return RedirectToAction("index", "home");
                    }
                    
                }
            }
            return RedirectToAction("HotelLogin");
		}

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return Content("SignedOut");
        }
>>>>>>> Stashed changes
    }
}
