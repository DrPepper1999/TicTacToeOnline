using TicTacToeOnline.Domain.UserAggregate;

namespace TicTacToeOnline.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        public User? GetUserByEmail(string email);
        public void Add(User user);
    }
}
