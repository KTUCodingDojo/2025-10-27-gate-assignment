using GateAssignment.Tests.Builders;
using NodaTime;

namespace GateAssignment.Tests
{
    public class GateAssignmentServiceTests
    {
        private readonly GateAssignmentService _service = new();
        private readonly FlightBuilder _flightBuilder;
        private readonly GateBuilder _gateBuilder;

        public GateAssignmentServiceTests()
        {
            _flightBuilder = new FlightBuilder();
            _gateBuilder = new GateBuilder();
        }

        [Fact]
        public void Given_NoAvailableGates_When_AssigningGate_Then_ReturnsNull()
        {
            // Arrange
            var flight = _flightBuilder.Build();
            var gates = new List<Gate>();

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void Given_DomesticFlight_When_AssigningToDomesticGate_Then_ReturnsAssignment()
        {
            // Arrange
            var flight = _flightBuilder
                .WithOriginType(OriginType.Domestic)
                .Build();

            var gate = _gateBuilder
                .IsDomestic(true)
                .Build();

            var gates = new List<Gate> { gate };

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            var expected = new Assignment(flight.Id, gate.Id, flight.TimeWindow);
            result.Should().Be(expected);
        }

        [Fact]
        public void Given_InternationalFlight_When_AssigningToInternationalGate_Then_ReturnsAssignment()
        {
            // Arrange
            var flight = _flightBuilder
                .WithOriginType(OriginType.International)
                .Build();

            var gate = _gateBuilder
                .IsDomestic(false)
                .Build();

            var gates = new List<Gate> { gate };

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            var expected = new Assignment(flight.Id, gate.Id, flight.TimeWindow);
            result.Should().Be(expected);
        }

        [Fact]
        public void Given_DomesticFlight_When_AssigningToInternationalGate_Then_ReturnsNull()
        {
            // Arrange
            var flight = _flightBuilder
                .WithOriginType(OriginType.Domestic)
                .Build();

            var gate = _gateBuilder
                .IsDomestic(false)
                .Build();

            var gates = new List<Gate> { gate };

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void Given_InternationalFlight_When_AssigningToDomesticGate_Then_ReturnsNull()
        {
            // Arrange
            var flight = _flightBuilder
                .WithOriginType(OriginType.International)
                .Build();

            var gate = _gateBuilder
                .IsDomestic(true)
                .Build();

            var gates = new List<Gate> { gate };

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void Given_WideBodyAircraft_When_AssigningToSupportingGate_Then_ReturnsAssignment()
        {
            // Arrange
            var flight = _flightBuilder
                .WithAircraftType(AircraftType.Wide)
                .Build();

            var gate = _gateBuilder
                .SupportsWidebody(true)
                .Build();

            var gates = new List<Gate> { gate };

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            var expected = new Assignment(flight.Id, gate.Id, flight.TimeWindow);
            result.Should().Be(expected);
        }

        [Fact]
        public void Given_NarrowBodyAircraft_When_AssigningToNonWidebodyGate_Then_ReturnsAssignment()
        {
            // Arrange
            var flight = _flightBuilder
                .WithAircraftType(AircraftType.Narrow)
                .Build();

            var gate = _gateBuilder
                .SupportsWidebody(false)
                .Build();

            var gates = new List<Gate> { gate };

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            var expected = new Assignment(flight.Id, gate.Id, flight.TimeWindow);
            result.Should().Be(expected);
        }

        [Fact]
        public void Given_WideBodyAircraft_When_AssigningToNonWidebodyGate_Then_ReturnsNull()
        {
            // Arrange
            var flight = _flightBuilder
                .WithAircraftType(AircraftType.Wide)
                .Build();

            var gate = _gateBuilder
                .SupportsWidebody(false)
                .Build();

            var gates = new List<Gate> { gate };

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void Given_Flight_When_AssigningWithNullGates_Then_ThrowsArgumentNullException()
        {
            // Arrange
            var flight = _flightBuilder.Build();

            // Act & Assert
            _service
                .Invoking(s => s.AssignGate(flight, null))
                .Should()
                .Throw<ArgumentNullException>()
                .WithParameterName("gates");
        }

        [Fact]
        public void Given_NullFlight_When_AssigningGate_Then_ThrowsArgumentNullException()
        {
            // Arrange
            var gates = new List<Gate> { _gateBuilder.Build() };

            // Act & Assert
            _service
                .Invoking(s => s.AssignGate(null, gates))
                .Should()
                .Throw<ArgumentNullException>()
                .WithParameterName("flight");
        }


    }
}