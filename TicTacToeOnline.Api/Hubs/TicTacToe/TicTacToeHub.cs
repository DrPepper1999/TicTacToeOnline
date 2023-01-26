using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using TicTacToeOnline.Contracts.Player;
using TicTacToeOnline.Contracts.Room;
using MediatR;
using TicTacToeOnline.Application.Players.Commands.CreatePlayer;

namespace TicTacToeOnline.Api.Hubs.TicTacToe
{
    public class TicTacToeHub : Hub<ITicTacToeHub>
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public bool IsAuthenticated { get; set; } = false;
        public TicTacToeHub(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public override Task OnConnectedAsync()
        {
            IsAuthenticated = Context.User?.Identity?.IsAuthenticated ?? false;

            if (!IsAuthenticated)
            {
                return base.OnConnectedAsync();
            }



            return base.OnConnectedAsync();
        }

        public async Task CreateRoom(CreatePlayerRequest createPlayerRequest, CreateRoomRequest createRoomRequest)
        {
            var createPlayerCommand = _mapper.Map<CreatePlayerCommand>(createPlayerRequest);

            var createPlayerResult = await _mediator.Send(createPlayerCommand);

            if (createPlayerResult.IsError)
            {

            }

            var createRoomCommand = _mapper
                .Map<CreatePlayerCommand>((createRoomRequest, createPlayerResult.Value.Id));

            var createRoomResult = await _mediator.Send(createRoomCommand);

            if (createRoomResult.IsError)
            {

            }

            //Clients.Caller
        }
    }
}
