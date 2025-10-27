using GateAssignment;

namespace GateAssignment.Tests
{
    public class GateAssignmentServiceTests
    {
        private readonly GateAssignmentService _service = new();

        [Fact]
        public void AssignGate_NoAvailableGates_ReturnsNull()
        {
            // Arrange
            Flight flight = null;
            List<Gate> gates = new List<Gate> {};

            // Act
            var result = _service.AssignGate(flight, gates);

            // Assert
            result.Should().BeNull();
        }
    }
}