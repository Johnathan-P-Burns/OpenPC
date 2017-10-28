using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPC_Database_Management
{
    class School
    {
        string SchoolName { get; set; }
        double Longitude { get; set; }
        double Latitude { get; set; }
        List<Building> Buildings { get; set; }
    }
}
