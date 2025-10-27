using GateAssignment;
using NodaTime;

namespace GateAssignment.Tests
{
    public class GateAssignmentServiceTests
    {
        private readonly GateAssignmentService _service = new();
        private readonly Flight flight;
        private readonly Gate gate;
        public GateAssignmentServiceTests()
        {
            Instant start = Instant.FromUtc(2025, 10, 27, 1, 0);
            Instant end = Instant.FromUtc(2025, 10, 27, 2, 0);
            Interval timeWindow = new Interval(start, end);
            flight = new Flight("01", timeWindow, AircraftType.Narrow, OriginType.Domestic, true);
            gate = new Gate("G1", false, true, true, new List<Interval> { timeWindow });

        }

        [Fact]
        public void AssignGate_NoAvailableGates_ReturnsNull()
        {
            // Arrange

            List<Gate> gates = new List<Gate> {};

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            result.Should().BeNull();
        }
        [Fact]
        public void AssignedGate_Domestic_Flight_Domestic_Gate_Returns_Gate()
        {
            // Arrange
            flight.OriginType = OriginType.Domestic;
            gate.IsDomestic = true;

            List<Gate> gates = new List<Gate> { };
            gates.Add(gate);

            // Act
            
            var result = _service.AssignGate(flight, gates);

            // Assert
            Assignment expected = new Assignment(flight.Id, gate.Id, flight.TimeWindow);
            result.Should().Be(expected);
        }

        [Fact]
        public void AssignedGate_International_Flight_International_Gate_Returns_Gate()
        {
            // Arrange
            flight.OriginType = OriginType.International;
            gate.IsDomestic = false;

            List<Gate> gates = new List<Gate> { };
            gates.Add(gate);

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            Assignment expected = new Assignment(flight.Id, gate.Id, flight.TimeWindow);
            result.Should().Be(expected);
        }

        [Fact]
        public void AssignedGate_Domestic_Flight_International_Gate_Returns_Null()
        {
            // Arrange
            flight.OriginType = OriginType.Domestic;
            gate.IsDomestic = false;

            List<Gate> gates = new List<Gate> { };
            gates.Add(gate);

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            result.Should().BeNull();
        }
        [Fact]
        public void AssignedGate_International_Flight_Domestic_Gate_Returns_Null()
        {
            // Arrange
            flight.OriginType = OriginType.International;
            gate.IsDomestic = true;

            List<Gate> gates = new List<Gate> { };
            gates.Add(gate);

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void AssignedGate_Flight_WideAircraft_Gate_SupportsWideBody_Assigns()
        {
            // Arrange
            flight.AircraftType = AircraftType.Wide;
            gate.SupportsWidebody = true;

            List<Gate> gates = new List<Gate> { };
            gates.Add(gate);

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            Assignment expected = new Assignment(flight.Id, gate.Id, flight.TimeWindow);
            result.Should().Be(expected);
        }
        [Fact]
        public void AssignedGate_Flight_NarrowAircraft_Gate_DoesNotSupportsWideBody_Assigns()
        {
            // Arrange
            flight.AircraftType = AircraftType.Narrow;
            gate.SupportsWidebody = false;

            List<Gate> gates = new List<Gate> { };
            gates.Add(gate);

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            Assignment expected = new Assignment(flight.Id, gate.Id, flight.TimeWindow);
            result.Should().Be(expected);
        }
        [Fact]
        public void AssignedGate_Flight_WideAircraft_Gate_DoesNotSupportsWideBody_ReturnsNull()
        {
            // Arrange
            flight.AircraftType = AircraftType.Wide;
            gate.SupportsWidebody = false;

            List<Gate> gates = new List<Gate> { };
            gates.Add(gate);

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            result.Should().BeNull();
        }


    }
}