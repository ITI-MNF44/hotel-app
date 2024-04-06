using hotel_app.Models;
using hotel_app.ViewModels;

namespace hotel_app.Services
{
    public static class FoodViewModelMapper
    {
        public static Food MapToFood(FoodViewModel foodViewModel)
        {
            if (foodViewModel == null)
                return null;

            return new Food() { 
                Id = foodViewModel.Id,
                Name = foodViewModel.Name,
                CategoryId = foodViewModel.CategoryId,
                Description = foodViewModel?.Description,
                HotelID = foodViewModel.HotelID,
                PricePerPerson= foodViewModel.PricePerPerson,
                CreatedDate= foodViewModel.CreatedDate,
                isDeleted = foodViewModel.isDeleted
            };

        }

        public static FoodViewModel MapToFoodViewModel(Food food)
        {
            if (food == null)
                return null;

            return new FoodViewModel()
            {
                Id = food.Id,
                Name = food.Name,
                CategoryId = food.CategoryId,
                Category = food.Category.Name,
                Description = food?.Description,
                HotelID = food.HotelID,
                Hotel = food.Hotel.Name,
                PricePerPerson = food.PricePerPerson,
                CreatedDate = food.CreatedDate,
                isDeleted = food.isDeleted
            };

        }
    }
}
