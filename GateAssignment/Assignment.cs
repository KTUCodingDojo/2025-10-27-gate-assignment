using NodaTime;

namespace GateAssignment
{
    public sealed record Assignment(
        string FlightId,
        string GateId,
        Interval TimeWindow // identical to Flight.TimeWindow
    );
}
