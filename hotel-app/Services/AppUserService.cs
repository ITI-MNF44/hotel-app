using hotel_app.Models;
using hotel_app.Repositories;
using System.Linq.Expressions;

namespace hotel_app.Services
{
    public class AppUserService: IAppUserService
    {

        IAppUserRepository appUserRepo;

        public AppUserService(IAppUserRepository _appUserRepo) {
            appUserRepo = _appUserRepo;
        }
        public int GetCount(Expression<Func<ApplicationUser, bool>> condition)
        {
            return appUserRepo.GetEntityCount(condition);
        }

        public ApplicationUser GetUserById(string id)
        {
            return appUserRepo.GetAppUserById(id);
        }

    }
}
