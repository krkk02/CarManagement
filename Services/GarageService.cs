using CarManagement.Data;
using CarManagement.Data.Entities;
using CarManagement.Dtos;
using CarManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarManagement.Services
{
    public class GarageService : IGarageService
    {
        private readonly AppDbContext _context;
        public GarageService(AppDbContext context)
        {
            _context = context;
        }

        public List<GarageDto> GetAllGarages(string? locationFilter)
        {
            var query = _context.Garages.AsQueryable();
            if (!string.IsNullOrEmpty(locationFilter))
                query = query.Where(w => w.City == locationFilter);

            return query
                .Select(w => new GarageDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    Location = w.Location,
                    City = w.City,
                    Capacity = w.Capacity
                })
                .ToList();
        }

        public GarageDto? GetGarageById(int id)
        {
            var w = _context.Garages.Find(id);
            if (w == null) return null;
            return new GarageDto
            {
                Id = w.Id,
                Name = w.Name,
                Location = w.Location,
                City = w.City,
                Capacity = w.Capacity
            };
        }

        public GarageDto? CreateGarage(GarageDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) ||
                string.IsNullOrWhiteSpace(dto.City) ||
                dto.Capacity < 0)
                return null;

            var entity = new Garage
            {
                Name = dto.Name,
                City = dto.City,
                Capacity = dto.Capacity,
                Location = dto.Location,
            };
            _context.Garages.Add(entity);
            _context.SaveChanges();

            dto.Id = entity.Id;
            return dto;
        }

        public GarageDto? UpdateGarage(int id, GarageDto dto)
        {
            var entity = _context.Garages.Find(id);
            if (entity == null) return null;

            if (string.IsNullOrWhiteSpace(dto.Name) ||
                string.IsNullOrWhiteSpace(dto.City) ||
                dto.Capacity < 0)
                return null;

            entity.Name = dto.Name;
            entity.Location = dto.Location;
            entity.City = dto.City;
            entity.Capacity = dto.Capacity;

            _context.SaveChanges();

            return new GarageDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Location = entity.Location,
                City = entity.City,
                Capacity = entity.Capacity
            };
        }

        public bool DeleteGarage(int id)
        {
            var entity = _context.Garages.Find(id);
            if (entity == null) return false;

            _context.Garages.Remove(entity);
            _context.SaveChanges();
            return true;
        }
    }
}
