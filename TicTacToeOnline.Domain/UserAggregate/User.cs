using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.DomainEvents;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.UserAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.UserAggregate
{
    public class User : AggregateRoot<UserId>
    {
        public string Name { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string Password { get; private set; } = null!;
        public string? RefreshToken { get; private set; } = null;
        public DateTime TokenCreated { get; private set; }
        public DateTime TokenExpires { get; private set; }

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdateDateTime { get; private set; }

        private User(string name, string email, string password, UserId? userId = null)
            : base(userId ?? UserId.CreateUnique())
        {
            Email = email;
            Password = password;
            Name = name;
        }

        public static User Create(string name, string email, string password)
        {
            var user = new User(name, email, password);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(Guid.NewGuid(), user.Id, user.Name));

            return user;
        }

        public void SetRefreshToken(string refreshToken, DateTime tokenExpires)
        {
            RefreshToken = refreshToken;
            TokenCreated = DateTime.UtcNow;
            TokenExpires = tokenExpires;
            RaiseDomainEvent(new RefreshTokenSetDomainEvent(Guid.NewGuid(), this));
        }

#pragma warning disable CS8618
        private User()
        {
        }
#pragma warning disable CS8618
    }
}
