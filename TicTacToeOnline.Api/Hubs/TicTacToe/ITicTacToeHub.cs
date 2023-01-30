using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeOnline.Contracts.Player;
using TicTacToeOnline.Contracts.Room;

namespace TicTacToeOnline.Api.Hubs.TicTacToe
{
    public interface ITicTacToeHub
    {
        public Task setPlayer(PlayerResponse playerResponse);

        public Task setRoom(RoomResponse roomResponse);

        public Task setConnectionInfo(bool isSuccessConnection, string[]? errors);
    }
}
