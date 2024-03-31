using hotel_app.Models;
using System.Linq.Expressions;

namespace hotel_app.Services
{
    public interface IAppUserService
    {
        public int GetCount(Expression<Func<ApplicationUser, bool>> condition);
        public ApplicationUser GetUserById(string id);

    }
}
