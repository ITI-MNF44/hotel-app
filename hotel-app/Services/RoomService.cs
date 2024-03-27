﻿using hotel_app.Models;
using hotel_app.Repositories;
using Microsoft.EntityFrameworkCore;

namespace hotel_app.Services
{
    public class RoomService: IRoomService
    {
        readonly IRoomRepository _roomRepository;
        readonly IRoomCategoryRepository _roomCategoryRepository;
        public RoomService(IRoomRepository roomRepository,
        IRoomCategoryRepository roomCategoryRepository) 
        {
            _roomRepository = roomRepository;
            _roomCategoryRepository = roomCategoryRepository;
        }

        public Room GetById(int id)
        {
            return _roomRepository.GetById(id);
        }

        public List<Room> HotelRooms(int id)
        {
            return _roomRepository.HotelRooms(id);
        }

        public List<RoomCategory> RoomCategories()
        {
            return _roomCategoryRepository.GetAll();
        }

        public void Insert(Room room)
        {
            _roomRepository.Insert(room);
        }

        public void Save()
        {
            _roomRepository.Save();
        }

        public Room GetById(int id, params string[] include)
        {
           return _roomRepository.GetById(id, include);
        }

        public void Update(Room room)
        {
            _roomRepository.Update(room);
        }
        public void Delete(int id)
        {
            _roomRepository.Delete(id);
        }

        public List<Room> AllAvailableRooms()
        {
            return _roomRepository.AllAvailableRooms();
        }
    }
}
