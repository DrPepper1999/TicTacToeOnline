using FluentValidation;

namespace TicTacToeOnline.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.MaxPlayers)
                .NotEmpty()
                .InclusiveBetween(2, 8);
            RuleFor(x => x.MapSize)
                .NotEmpty()
                .InclusiveBetween(3, 12);
            RuleFor(x => x.TeamCount)
                .NotEmpty()
                .InclusiveBetween(2, 4);
            RuleFor(x => x.PlayerId).NotEmpty();
        }
    }
}
