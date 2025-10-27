using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace GateAssignment
{
    public sealed record Gate(
        string Id,
        bool SupportsWidebody,
        bool IsDomestic,
        bool HasJetway,
        IReadOnlyList<Interval> AvailabilityWindows
    );
}
