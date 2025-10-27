using NodaTime;

namespace GateAssignment
{
    public sealed class Flight
    {
        public Flight(string id, Interval timeWindow, AircraftType aircraftType, OriginType originType, bool requiresJetway)
        {
            Id = id;
            TimeWindow = timeWindow;
            AircraftType = aircraftType;
            OriginType = originType;
            RequiresJetway = requiresJetway;
        }

        public string Id { get; set; }
        public Interval TimeWindow { get; set; }
        public AircraftType AircraftType { get; set; }
        public OriginType OriginType { get; set; }
        public bool RequiresJetway { get; set; }


    }
}