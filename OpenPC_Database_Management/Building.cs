using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPC_Database_Management
{
    class Building:BaseClass
    {
        string Name { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
        List<Room> Rooms { get; set;}
    }
}
