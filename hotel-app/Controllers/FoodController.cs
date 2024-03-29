using hotel_app.Models;
using hotel_app.Services;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hotel_app.Controllers
{
    public class FoodController : Controller
    {
        IFoodService _foodService;
        IHotelService _hotelService;
        public FoodController(IFoodService foodService, IHotelService hotelService)
        {
            _foodService = foodService;
            _hotelService = hotelService;

        }

        public async Task<IActionResult> Index()
        {
            var currentHotel = await _hotelService.GetCurrentHotel();
            List<Food> Food = _foodService.GetHotelFoods(currentHotel.Id, "Category", "Hotel");

            return View("AllFood", Food);
        }

        [Authorize]
        public IActionResult Add()
        {
            FoodViewModel foodCategories = new FoodViewModel()
            {
                Categories = _foodService.GetFoodCategories(),
            };
            return View("AddFood", foodCategories);
        }

        [HttpPost]
        public async Task<IActionResult> Save(FoodViewModel food)
        {
            if (ModelState.IsValid)
            {
                var hotel = await _hotelService.GetCurrentHotel();
                food.HotelID = hotel.Id;
                food.CreatedDate = DateTime.Now;

                _foodService.Insert(FoodViewModelMapper.MapToFood(food));
                _foodService.Save();

                return RedirectToAction("Index");
            }

            food.Categories = _foodService.GetFoodCategories();
            return View("AddFood", food);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            Food food =  _foodService.GetById(id, "Hotel", "Category");
            FoodViewModel foodViewModel = FoodViewModelMapper.MapToFoodViewModel(food);
            foodViewModel.Categories = _foodService.GetFoodCategories();

            return View("Edit", foodViewModel);
        }

        [HttpPost]
        public IActionResult Update(FoodViewModel foodViewModel)
        {
            if(ModelState.IsValid)
            {
                Food oldFood = _foodService.GetById(foodViewModel.Id, "Hotel", "Category");
                foodViewModel.CreatedDate = oldFood.CreatedDate;
                foodViewModel.HotelID = oldFood.HotelID;

                _foodService.Update(FoodViewModelMapper.MapToFood(foodViewModel));
                _foodService.Save();

                return RedirectToAction("Index");
            }
            foodViewModel.Categories = _foodService.GetFoodCategories();
            return View("Edit", foodViewModel);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            Food food = _foodService.GetById(id, "Hotel", "Category");
            FoodViewModel foodViewModel = FoodViewModelMapper.MapToFoodViewModel(food);

            return View("Delete", foodViewModel);
        }
        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            var Food = _foodService.GetById(id);
            Food.isDeleted = true;

            _foodService.Update(Food);
            _foodService.Save();

            return RedirectToAction("Index");
        }

    }
}
