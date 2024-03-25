using hotel_app.Models;

namespace hotel_app.Repositories
{
    public interface IHotelCategoryRepository: IGeneralRepository<HotelCategory>
    {
<<<<<<< Updated upstream
        //don't write abstract, Interface methods are implicitly abstract, no need to dfine methods in IGenral, 
        //just add new ones
      
=======
        // add abstract method
        List<HotelCategory> GetAllCategories();
>>>>>>> Stashed changes
    }
}
