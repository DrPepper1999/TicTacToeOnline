using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using TicTacToeOnline.Application.Common.Extensions;
using TicTacToeOnline.Domain.RoomAggregate.Enums;

namespace TicTacToeOnline.Infrastructure.Persistence.Common.Conversion
{
    public class MapConverter : ValueConverter<Mark[,], string>
    {
        public MapConverter()
            : base(
                m => JsonSerializer
                    .Serialize(m.ConvertToString(), (JsonSerializerOptions)null!),
                m => m.ConvertStringFieldToMap())
        { }
    }
}
