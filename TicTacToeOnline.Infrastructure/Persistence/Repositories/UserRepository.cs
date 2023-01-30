using Microsoft.EntityFrameworkCore;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.UserAggregate;

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

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.User.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task Add(User user)
        {
           await _dbContext.User.AddAsync(user);
        }
    }
}
