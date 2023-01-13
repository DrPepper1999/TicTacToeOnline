using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeOnline.Domain.Entities;

namespace TicTacToeOnline.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        public User? GetUserByEmail(string email);
        public void Add(User user);
    }
}
