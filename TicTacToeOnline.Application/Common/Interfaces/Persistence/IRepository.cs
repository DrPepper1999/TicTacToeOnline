using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeOnline.Domain.Common.Models;

namespace TicTacToeOnline.Application.Common.Interfaces.Persistence
{
    public interface IRepository<T, TId>
        where T : AggregateRoot<TId>
        where TId : ValueObject
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
