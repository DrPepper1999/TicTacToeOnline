using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeOnline.Infrastructure.Persistence.Constants
{
    public class TableNames
    {
        internal const string Members = nameof(Members);

        internal const string Gatherings = nameof(Gatherings);

        internal const string Invitations = nameof(Invitations);

        internal const string Attendees = nameof(Attendees);

        internal const string OutboxMessages = nameof(OutboxMessages);
    }
}
