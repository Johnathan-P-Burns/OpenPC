using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPC_Database_Management
{
    class Room:BaseClass
    {
        string Name { get; set; }
        int Floor { get; set; }
        List<Computer> Computers { get; set; }
    }
}
