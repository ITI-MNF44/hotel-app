namespace hotel_app.Repositories
{
    public interface IGeneralRepository<T>
    {
        
        public List<T> GetAll();
        public void Insert(T obj);
        public void Update(T obj);
        public void Delete(int id);
        public T GetById(int id);
        public int Save();
    }
}
