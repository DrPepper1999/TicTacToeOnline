using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;

namespace TicTacToeOnline.Application.Rooms.Commands.DeleteRoom
{
    public record DeleteRoomCommand(Guid id) : IRequest<ErrorOr<Unit>>;
}
