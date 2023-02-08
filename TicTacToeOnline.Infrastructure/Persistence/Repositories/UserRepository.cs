using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.UserAggregate;
using TicTacToeOnline.Domain.UserAggregate.ValueObjects;

namespace TicTacToeOnline.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TicTacToeOnlineDbContext _dbContext;

        public UserRepository(TicTacToeOnlineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public async Task<User?> GetUserById(UserId userId)
        {
            return await _dbContext.User.SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetFirstWhere(Expression<Func<User, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.User.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.User.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task Add(User user)
        {
           await _dbContext.User.AddAsync(user);
        }

        public async Task UpdateAsync(User entity)
        {
            await Task.CompletedTask;

            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
