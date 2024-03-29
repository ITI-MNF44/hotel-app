using hotel_app.Models;

namespace hotel_app.Repositories
{
    public interface IAppUserRepository: IGeneralRepository<ApplicationUser>
    {
       public ApplicationUser GetAppUserById(string id);
    }
}
