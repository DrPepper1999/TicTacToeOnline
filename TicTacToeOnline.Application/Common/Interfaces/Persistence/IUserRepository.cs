using TicTacToeOnline.Domain.UserAggregate;
using TicTacToeOnline.Domain.UserAggregate.ValueObjects;

namespace TicTacToeOnline.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository : IRepository<User, UserId>
    {
        public Task<User?> GetUserByEmail(string email);
        public Task Add(User user);
    }
}
