using NodaTime;

namespace GateAssignment
{
    public class GateAssignmentService
    {
        public GateAssignmentService() { }

        public Assignment? AssignGate(Flight flight, IEnumerable<Gate> gates)
        {
            if (flight == null)
                throw new ArgumentNullException(nameof(flight));
            if (gates == null)
                throw new ArgumentNullException(nameof(gates));

            foreach (Gate gate in gates)
            {
                if (IsGateSuitableForFlight(gate, flight))
                {
                    return new Assignment(flight.Id, gate.Id, flight.TimeWindow);
                }
            }

            return null;
        }

        private bool IsGateSuitableForFlight(Gate gate, Flight flight)
        {
            return IsGateSuitableForAircraft(gate, flight)
                && IsGateSuitableForOrigin(gate, flight)
                && DoesGateMeetJetwayRequirements(gate, flight)
                && IsGateAvailableInTimeWindow(gate, flight.TimeWindow);
        }

        private bool IsGateSuitableForAircraft(Gate gate, Flight flight)
        {
            return flight.AircraftType != AircraftType.Wide || gate.SupportsWidebody;
        }

        private bool IsGateSuitableForOrigin(Gate gate, Flight flight)
        {
            return (flight.OriginType == OriginType.Domestic && gate.IsDomestic) ||
                   (flight.OriginType == OriginType.International && !gate.IsDomestic);
        }

        private bool DoesGateMeetJetwayRequirements(Gate gate, Flight flight)
        {
            return !flight.RequiresJetway || gate.HasJetway;
        }

        private bool IsGateAvailableInTimeWindow(Gate gate, Interval timeWindow)
        {
            return gate.AvailabilityWindows.Any(window => 
                (window.Start <= timeWindow.Start && window.End >= timeWindow.End));
        }
    }
}
