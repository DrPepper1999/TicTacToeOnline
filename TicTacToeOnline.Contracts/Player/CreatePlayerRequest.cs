﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeOnline.Contracts.Player
{
    public record CreatePlayerRequest(string Name, string ConnectionId);
}