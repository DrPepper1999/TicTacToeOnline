﻿using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using MapsterMapper;
using MediatR;
using TicTacToeOnline.Application.Players.Queries.GetPlayer;
using TicTacToeOnline.Application.Rooms.Commands.CreateRoom;
using TicTacToeOnline.Contracts.Player;
using TicTacToeOnline.Domain.UserAggregate.ValueObjects;
using TicTacToeOnline.Contracts.Room;
using static TicTacToeOnline.Domain.Common.Errors.Errors.Authentication;
using TicTacToeOnline.Application.Games.Commands.MakeMove;
using TicTacToeOnline.Application.Players.Commands.AppendConnection;
using TicTacToeOnline.Domain.Common.ValueObjects;

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

        public override Task OnDisconnectedAsync(Exception? exception)
        {

            return base.OnDisconnectedAsync(exception);
        }

        public async Task CreateRoomAuth(CreateRoomRequest request)
        {
            // if user authenticated
            var userIdentity = (Context.User!.Identity as ClaimsIdentity);
            if ((userIdentity?.IsAuthenticated ?? false) is false)
            {
                await Clients.Caller.setConnectionInfo(false,
                    new[] { InvalidCredentials.Description });
                return;
            }
            var userId = GetUserId(userIdentity);

            // TODO убрать Player так как через UserId можно узнать id player
            var getPlayerQuery = new GetPlayerQuery(x =>
                x.UserId! == userId);

            var playerResult = await _mediator.Send(getPlayerQuery);

            if (playerResult.IsError)
            {
                await Clients.Caller.setConnectionInfo(false,
                    playerResult.Errors.Select(x => x.Description).ToArray());
                return;
            }
            playerResult.Value.AppendConnection(Context.ConnectionId);
            // TODO проверить что просле добавления нет ошибки
            await _mediator.Send(new AppendConnectionCommand(playerResult.Value.Id.Value, Context.ConnectionId));
            await Clients.Caller.setPlayer(_mapper.Map<PlayerResponse>(playerResult.Value));

            var createRoomCommand = _mapper
               .Map<CreateRoomCommand>((request, playerResult.Value.Id.Value));
            var createRoomResult = await _mediator.Send(createRoomCommand);

            if (createRoomResult.IsError)
            {
                await Clients.Caller.setConnectionInfo(false,
                    createRoomResult.Errors.Select(x => x.Description).ToArray());
                return;
            }

            await Clients.Caller.setRoom(_mapper.Map<RoomResponse>(createRoomResult.Value));

            await Groups
                .AddToGroupAsync(Context.ConnectionId, createRoomResult.Value.Id.Value.ToString());

            await Clients.Caller.setConnectionInfo(true, null);
        }

        public async Task CreateRoomGuest(CreatePlayerRequest createPlayerRequest,
            CreateRoomRequest createRoomHubRequest)
        {
            await Task.Delay(100);
        }

        public async Task JoinRoomAuth()
        {

        }

        private static UserId GetUserId(ClaimsIdentity userIdentity)
        {
            return UserId.Create(Guid.Parse(userIdentity!
                .FindFirst(ClaimTypes.NameIdentifier)!.Value));
        }

        public async Task MakeMove(MoveRequest request)
        {
            var connection = Context.ConnectionId;

            var command = _mapper.Map<MakeMoveCommand>(request);

            var makeMoveResult = await _mediator.Send(command);

            if (makeMoveResult.IsError)
            {

            }

            var gameId = makeMoveResult.Value.GameId.Value.ToString();
            await Clients.Group(gameId).makeMove(_mapper.Map<MoveResponse>(makeMoveResult.Value));
        }
    }
}
