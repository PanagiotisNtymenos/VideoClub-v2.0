using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Core.Entities;

namespace VideoClub.Core.Interfaces
{
    public interface IUserService
    {
        // Get
        User GetUserByUserName(string username);
        Task<List<string>> GetUserNameByQuery(string q);
    }
}
