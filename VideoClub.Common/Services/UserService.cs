using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VideoClub.Core.Entities;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Data;

namespace VideoClub.Common.Services
{
    public class UserService : IUserService
    {
        private readonly VideoClubDbContext _context;

        public UserService(VideoClubDbContext context)
        {
            _context = context;
        }

        public User GetUserByUserName(string username)
        {
            return _context.Users
                .Where(u => u.UserName == username)
                .FirstOrDefault();
        }

        public async Task<List<string>> GetUserNameByQuery(string q)
        {
            return await _context.Users
                .Where(m => m.UserName.StartsWith(q))
                .Select(m => m.UserName)
                .ToListAsync();
        }

        public async Task<List<User>> GetAllCustomers()
        {
            return await _context.Users
                .Where(u => u.Roles.Any(r => r.RoleId == "2"))
                .Include(r => r.Rentings)
                .ToListAsync();
        }
    }
}
