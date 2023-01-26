using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeOnline.Domain.PlayerAggregate;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;

namespace TicTacToeOnline.Application.Common.Interfaces.Persistence
{
    public interface IPlayerRepository : IRepository<Player, PlayerId>
    {
        Task AddAsync(Player player, CancellationToken cancellationToken = default);

        Task<Player?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task DeleteAsync(Player player);
    }
}
