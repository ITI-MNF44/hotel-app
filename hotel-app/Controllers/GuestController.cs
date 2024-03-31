using hotel_app.Models;
using hotel_app.Services;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Text.Json;

namespace hotel_app.Controllers
{
    public class Country
    {
        public string CommonName { get; set; }
    }
    public class GuestController : Controller
    {
        //ask
        UserManager<ApplicationUser> usermanager;
        SignInManager<ApplicationUser> signInManager;
        IGuestService GuestService;
        private readonly IHttpClientFactory _clientFactory;

        public GuestController(UserManager<ApplicationUser> usermanagerlogin,
            SignInManager<ApplicationUser> _signInManager, IGuestService _GuestService, IHttpClientFactory clientFactory)
        {
            usermanager = usermanagerlogin;
            signInManager = _signInManager;
            GuestService = _GuestService;
            _clientFactory = clientFactory;
        }
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> Index()
        {
            Guest guest = await GuestService.GetCurrentGuest();
            return Content("hello from guest index : " + guest.FirstName + " " + guest.LastName);
        }

        public IActionResult Login()
        {
            return View("GuestLoginView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginVIewModel guestVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser AppUser = await usermanager.FindByNameAsync(guestVM.Username);
                if (AppUser != null)
                {
                    bool found = await usermanager.CheckPasswordAsync(AppUser, guestVM.Passwrod);
                    if (found)
                    {
                        await signInManager.SignInAsync(AppUser, guestVM.RememberMe);
                        List<Claim> Claims = new List<Claim>();
                        Claims.Add(new Claim(ClaimTypes.NameIdentifier, AppUser.Id));
                        await usermanager.AddToRoleAsync(AppUser, "Guest");
                        await signInManager.SignInWithClaimsAsync(AppUser, guestVM.RememberMe, Claims);
                        return RedirectToAction("index", "home");
                    }

                }
                ModelState.AddModelError(string.Empty, "Username or password is incorrect");

            }
            return View("GuestLoginView", guestVM);
        }

        public async Task<IActionResult> Register()
        {
            var guestVM = new RegisterGuestViewModel();

            try
            {
                // Call the REST countries API to get the list of countries
                var client = _clientFactory.CreateClient();
                HttpResponseMessage response = await client.GetAsync("https://restcountries.com/v3.1/all");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse the JSON response using JsonDocument
                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    var root = doc.RootElement;
                    var countries = root.EnumerateArray();

                    // Extract country names
                    List<string> countryNames = new List<string>();
                    foreach (var country in countries)
                    {
                        string name = country.GetProperty("name").GetProperty("common").GetString();
                        countryNames.Add(name);
                    }

                    countryNames.Sort();

                    // Set the SelectList to the view model
                    guestVM.Countries = countryNames;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log the error)
                // You can set a default SelectList or handle it as per your requirement
                Console.WriteLine($"Error fetching countries: {ex.Message}");
                guestVM.Countries = new List<string>();
            }

            // Pass the view model to the view
            return View("GuestRegisterView", guestVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterGuestViewModel guestVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser AppUser = GuestService.MapGuestVmToAppUser(guestVM);
                IdentityResult result = await usermanager.CreateAsync(AppUser, guestVM.Password);
                if (result.Succeeded)
                {
                    await usermanager.AddToRoleAsync(AppUser, "Guest");
                    List<Claim> Claims = new List<Claim>();
                    Claims.Add(new Claim(ClaimTypes.NameIdentifier, AppUser.Id));
                    await signInManager.SignInWithClaimsAsync(AppUser, true, Claims);
                    Guest guest = GuestService.MapGuestVmToGuest(guestVM);
                    guest.UserId = AppUser.Id;
                    GuestService.InsertGuest(guest);
                    GuestService.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View("GuestRegisterView", guestVM);


            }
            return View("GuestRegisterView", guestVM);
        }

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return Content("guest SignedOut");
        }

   
        public IActionResult ReservationsHistory(int guestId)
        {
            var model = GuestService.getGuestReservations(guestId);
            return View("GuestReservationsHistory", model);
        }
    }
}
