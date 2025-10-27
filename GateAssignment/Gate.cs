using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace GateAssignment
{
    public sealed class Gate
    {
        public Gate(string id, bool supportsWidebody, bool isDomestic, bool hasJetway, IReadOnlyList<Interval> availabilityWindows)
        {
            Id = id;
            SupportsWidebody = supportsWidebody;
            IsDomestic = isDomestic;
            HasJetway = hasJetway;
            AvailabilityWindows = availabilityWindows;
        }

        public string Id { get; set; }
        public bool SupportsWidebody { get; set; }
        public bool IsDomestic { get; set; }
        public bool HasJetway { get; set; }
        public IReadOnlyList<Interval> AvailabilityWindows { get; set; }
    }
}
