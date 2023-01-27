﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.Common.Errors;
using TicTacToeOnline.Domain.DomainEvents;
using TicTacToeOnline.Domain.PlayerAggregate;

namespace TicTacToeOnline.Application.Players.Events
{
    public class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly IPlayerRepository _playerRepository;

        public UserCreatedDomainEventHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var player = Player.Create(notification.UserId, notification.Name);

            await _playerRepository.AddAsync(player, cancellationToken);

            await _playerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
