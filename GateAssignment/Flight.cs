using NodaTime;

namespace GateAssignment
{
    public sealed record Flight(
        string Id,
        Interval TimeWindow,
        AircraftType AircraftType,
        OriginType OriginType,
        bool RequiresJetway
    );
}