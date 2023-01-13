using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeOnline.Application.Common.Interfaces.Services;

namespace TicTacToeOnline.Infrastructure.Services
{
    public class DataTimeProvider : IDataTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
