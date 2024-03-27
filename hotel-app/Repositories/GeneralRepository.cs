
using hotel_app.Models;

namespace hotel_app.Repositories
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : class
    {
        HotelDbContext hotelDbContext;

        public GeneralRepository(HotelDbContext DbContext)
        {
            hotelDbContext = DbContext;
        }
        public void Delete(int id)
        {
            T obj =  GetById(id);
            hotelDbContext.Remove(obj);
        }
        public List<T> GetAll()
        {
            return hotelDbContext.Set<T>().ToList();
        }
        public T GetById(int id)
        {
            return hotelDbContext.Set<T>().Find(id);
        }
        public void Insert(T obj)
        {
            hotelDbContext.Set<T>().Add(obj);
        }
        public void Update(T obj)
        {
            hotelDbContext.Set<T>().Update(obj);
        }
        public int Save()
        {
             return hotelDbContext.SaveChanges();
        }
    }
}
