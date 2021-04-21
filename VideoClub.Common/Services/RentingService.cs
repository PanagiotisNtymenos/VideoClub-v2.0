﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Core.Entities;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Data;

namespace VideoClub.Common.Services
{
    public class RentingService : IRentingService
    {
        private readonly VideoClubDbContext _context;

        public RentingService(VideoClubDbContext context)
        {
            _context = context;
        }

        // Get
        public async Task<List<Renting>> GetAllActiveRentings()
        {
            return await _context.Rentings.Where(r => r.IsActive).Include(r => r.Copy).Include(r => r.User).ToListAsync();
        }

        public async Task<Renting> GetRentingById(int rentingId)
        {
            return await _context.Rentings.Where(r => r.Id == rentingId).Include(c => c.Copy).FirstAsync();
        }


        // Add
        public async Task AddRenting(Renting renting)
        {
            _context.Rentings.Add(renting);
            _context.Copies.Attach(renting.Copy);
            renting.Copy.IsAvailable = false;
            _context.Entry(renting.Copy).Property(c => c.IsAvailable).IsModified = true;

            await _context.SaveChangesAsync();
        }


        // Delete
        public async Task DeleteRenting(Renting renting)
        {
            _context.Rentings.Attach(renting);
            _context.Entry(renting).Property(r => r.IsActive).IsModified = true;
            _context.Entry(renting).Property(r => r.ReturnNotes).IsModified = true;
            _context.Entry(renting).Property(r => r.ReturnDate).IsModified = true;

            _context.Copies.Attach(renting.Copy);
            _context.Entry(renting.Copy).Property(c => c.IsAvailable).IsModified = true;

            await _context.SaveChangesAsync();
        }
    }
}
