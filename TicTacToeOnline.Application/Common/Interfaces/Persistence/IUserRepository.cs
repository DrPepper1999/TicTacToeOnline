using System.Linq.Expressions;
using TicTacToeOnline.Domain.UserAggregate;
using TicTacToeOnline.Domain.UserAggregate.ValueObjects;

namespace TicTacToeOnline.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository : IRepository<User, UserId>
    {
        public Task<User?> GetUserById(UserId userId);
        Task<User?> GetFirstWhere(Expression<Func<User, bool>> predicate,
            CancellationToken cancellationToken = default);
        public Task<User?> GetUserByEmail(string email);
        public Task Add(User user);
        public Task UpdateAsync(User entity);
    }
}
