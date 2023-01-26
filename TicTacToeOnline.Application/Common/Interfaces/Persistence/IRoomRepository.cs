using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeOnline.Domain.RoomAggregate;

namespace TicTacToeOnline.Application.Common.Interfaces.Persistence
{
    public interface IRoomRepository
    {
        Task AddAsync(Room room, CancellationToken cancellationToken = default);

        Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task DeleteAsync(Room room);
    }
}
