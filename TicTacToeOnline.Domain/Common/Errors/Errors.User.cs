using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;

namespace TicTacToeOnline.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error DuplicateEmail => 
                Error.Conflict(code: "User.DuplicateEmail", description: "Email is already is use");

        }
    }
}
