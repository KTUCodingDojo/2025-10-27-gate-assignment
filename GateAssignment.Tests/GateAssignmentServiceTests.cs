using GateAssignment;
using NodaTime;

namespace GateAssignment.Tests
{
    public class GateAssignmentServiceTests
    {
        private readonly GateAssignmentService _service = new();

        [Fact]
        public void AssignGate_NoAvailableGates_ReturnsNull()
        {
            // Arrange
            Instant start = Instant.FromUtc(2025, 10, 27, 1, 0); 
            Instant end = Instant.FromUtc(2025, 10, 27, 2, 0);
            Interval timeWindow = new Interval(start, end);
            Flight flight = new Flight("01", timeWindow, AircraftType.Narrow, OriginType.Domestic, true);
            List<Gate> gates = new List<Gate> {};

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            result.Should().BeNull();
        }
    }
}