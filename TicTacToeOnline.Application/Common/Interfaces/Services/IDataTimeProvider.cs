﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeOnline.Application.Common.Interfaces.Services
{
    public interface IDataTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
