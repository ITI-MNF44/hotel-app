﻿using hotel_app.Models;

namespace hotel_app.Repositories
{
    public class HotelRepository : GeneralRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(HotelDbContext DbContext) : base(DbContext)
        {
        }

        public void test_func()
        {
            throw new NotImplementedException();
        }
    }
}