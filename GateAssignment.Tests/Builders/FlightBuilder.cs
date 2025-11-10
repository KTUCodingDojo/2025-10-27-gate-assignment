using NodaTime;

namespace GateAssignment.Tests.Builders
{
    public class FlightBuilder
    {
        private string _id = "01";
        private Interval _timeWindow;
        private AircraftType _aircraftType = AircraftType.Narrow;
        private OriginType _originType = OriginType.Domestic;
        private bool _requiresJetway = true;

        public FlightBuilder()
        {
            var start = Instant.FromUtc(2025, 10, 27, 1, 0);
            var end = Instant.FromUtc(2025, 10, 27, 2, 0);
            _timeWindow = new Interval(start, end);
        }

        public FlightBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public FlightBuilder WithTimeWindow(Interval timeWindow)
        {
            _timeWindow = timeWindow;
            return this;
        }

        public FlightBuilder WithAircraftType(AircraftType aircraftType)
        {
            _aircraftType = aircraftType;
            return this;
        }

        public FlightBuilder WithOriginType(OriginType originType)
        {
            _originType = originType;
            return this;
        }

        public FlightBuilder RequiringJetway(bool requiresJetway)
        {
            _requiresJetway = requiresJetway;
            return this;
        }

        public Flight Build()
        {
            return new Flight(_id, _timeWindow, _aircraftType, _originType, _requiresJetway);
        }
    }
}