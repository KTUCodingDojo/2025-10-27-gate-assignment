using NodaTime;

namespace GateAssignment.Tests.Builders
{
    public class GateBuilder
    {
        private string _id = "G1";
        private bool _supportsWidebody = false;
        private bool _isDomestic = true;
        private bool _hasJetway = true;
        private IReadOnlyList<Interval> _availabilityWindows;

        public GateBuilder()
        {
            var start = Instant.FromUtc(2025, 10, 27, 1, 0);
            var end = Instant.FromUtc(2025, 10, 27, 2, 0);
            _availabilityWindows = new List<Interval> { new Interval(start, end) };
        }

        public GateBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public GateBuilder SupportsWidebody(bool supportsWidebody)
        {
            _supportsWidebody = supportsWidebody;
            return this;
        }

        public GateBuilder IsDomestic(bool isDomestic)
        {
            _isDomestic = isDomestic;
            return this;
        }

        public GateBuilder HasJetway(bool hasJetway)
        {
            _hasJetway = hasJetway;
            return this;
        }

        public GateBuilder WithAvailabilityWindows(IReadOnlyList<Interval> availabilityWindows)
        {
            _availabilityWindows = availabilityWindows;
            return this;
        }

        public Gate Build()
        {
            return new Gate(_id, _supportsWidebody, _isDomestic, _hasJetway, _availabilityWindows);
        }
    }
}