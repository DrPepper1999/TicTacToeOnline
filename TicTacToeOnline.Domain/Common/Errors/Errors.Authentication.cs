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
        public static class Authentication
        {
            public static Error InvalidCredentials => Error.Validation(
                code: "Auth.InvalidCred",
                description:"Invalid credentials");

            public static Error TokenExpired => Error.Failure(
                code: "Auth.TokenExpired",
                description: "Token Expired");
        }
    }
}
