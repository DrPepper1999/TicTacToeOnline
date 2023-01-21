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
        void Add(Room room);
    }
}
