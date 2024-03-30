using hotel_app.Models;
using hotel_app.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace hotel_app.Controllers
{
    public class TestController : Controller
    {
        private readonly IGuestRepository guestRepository;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TestController(IGuestRepository _guestRepository, RoleManager<IdentityRole> roleManager)
        {
            guestRepository = _guestRepository;
            _roleManager = roleManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            string res = guestRepository.GetAll().Count().ToString();
            return Content("hello from TestController: num of guest = "+res);
        }


       

        // Action method to add the "Hotel" role
        public async Task<IActionResult> AddHotelRole()
        {
            // Check if the "Hotel" role exists
            if (!await _roleManager.RoleExistsAsync("Hotel"))
            {
                // If the role doesn't exist, create it
                var role = new IdentityRole("Hotel");
                var result = await _roleManager.CreateAsync(role);

                // Check if the role creation was successful
                if (result.Succeeded)
                {
                    // Role created successfully
                    return Ok("Guest role created successfully.");
                }
                else
                {
                    // Role creation failed
                    // You can handle the error as per your application's requirements
                    return StatusCode(500, "Failed to create Guest role.");
                }
            }
            else
            {
                // Role already exists
                return Conflict("Guest role already exists.");
            }
        }
    }
}
