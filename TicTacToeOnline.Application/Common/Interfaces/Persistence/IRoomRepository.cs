using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeOnline.Domain.RoomAggregate;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;

namespace TicTacToeOnline.Application.Common.Interfaces.Persistence
{
    public interface IRoomRepository : IRepository<Room, RoomId>
    {
        Task AddAsync(Room room, CancellationToken cancellationToken = default);

        Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task DeleteAsync(Room room);
    }
}
