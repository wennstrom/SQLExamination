using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbtentamenow
{
    public class Vehicle
    {
        public Vehicle (string regPlate, int location, string vehicleType, DateTime arrival)
        {
            this.RegPlate = regPlate;
            this.Location = location;
            this.VehicleType = vehicleType;
            this.Arrival = arrival;
        }
        public string RegPlate { get;  }
        public int Location { get; }
        public string VehicleType { get; }
        public DateTime Arrival { get; }
    }
}
