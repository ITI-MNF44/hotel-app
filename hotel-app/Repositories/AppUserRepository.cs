using hotel_app.Models;

namespace hotel_app.Repositories
{
    public class AppUserRepository : GeneralRepository<ApplicationUser>, IAppUserRepository
    {
        HotelDbContext appContext;
        public AppUserRepository(HotelDbContext DbContext) : base(DbContext)
        {
            appContext = DbContext;
        }

        public ApplicationUser GetAppUserById(string id)
        {
           return appContext.Users.FirstOrDefault(u => u.Id == id);
        }


    }
}
