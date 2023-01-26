using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
