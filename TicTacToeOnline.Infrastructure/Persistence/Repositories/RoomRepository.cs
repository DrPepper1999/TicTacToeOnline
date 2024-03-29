﻿using Microsoft.EntityFrameworkCore;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.RoomAggregate;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;

namespace TicTacToeOnline.Infrastructure.Persistence.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly TicTacToeOnlineDbContext _dbContext;

        public RoomRepository(TicTacToeOnlineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public async Task AddAsync(Room room, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(room, cancellationToken);
        }

        public async Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Rooms
                .FirstOrDefaultAsync(x => x.Id == RoomId.Create(id), cancellationToken);

            return entity;
        }

        public async Task UpdateAsync(Room room, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(room).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Room room)
        {
            await Task.CompletedTask;

            _dbContext.Rooms.Remove(room);
        }
    }
}
