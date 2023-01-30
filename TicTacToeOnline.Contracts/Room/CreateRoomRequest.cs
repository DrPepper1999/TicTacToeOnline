using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeOnline.Contracts.Room
{
    public record CreateRoomRequest(
        string Name,
        string? Password,
        int PlayersForStart,
        int MapSize
    );
}
