using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.UserAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.UserAggregate
{
    public class User : AggregateRoot<UserId>
    {
        public string Email { get; private set; } = null!;
        public string Password { get; private set; } = null!;
        public string Name { get; private set; } = null!;

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdateDateTime { get; private set; }

        private User(string name, string email, string password, UserId? userId = null)
            : base(userId ?? UserId.CreateUnique())
        {
            Email = email;
            Password = password;
            Name = name;
        }
    }
}
