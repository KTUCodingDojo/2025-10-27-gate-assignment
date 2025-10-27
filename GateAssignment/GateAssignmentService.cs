using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GateAssignment
{
    public class GateAssignmentService
    {

        public GateAssignmentService() { }
        public Assignment? AssignGate(Flight flight, IEnumerable<Gate> gates)
        {

             foreach (Gate gate in gates)
                {

                if (flight.AircraftType == AircraftType.Wide && !gate.SupportsWidebody)
                {
                    continue;
                }
                 if (flight.OriginType == OriginType.Domestic && gate.IsDomestic)
                {
                    return new Assignment(flight.Id, gate.Id, flight.TimeWindow);
                }
                else if (flight.OriginType == OriginType.International && !gate.IsDomestic)
                {
                    return new Assignment(flight.Id, gate.Id, flight.TimeWindow);
                }







            }
            //----------------


            return null;
        }
    }
}
