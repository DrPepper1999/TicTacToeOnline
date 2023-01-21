using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.RoomAggregate;

namespace TicTacToeOnline.Infrastructure.Persistence.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly TicTacToeOnlineDbContext _dbContext;

        public RoomRepository(TicTacToeOnlineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Room room)
        {
            _dbContext.Add(room);

            _dbContext.SaveChanges();
        }
    }
}
