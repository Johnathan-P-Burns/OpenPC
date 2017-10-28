//Team Medjed
//Johnathan Burns, Ethan Spangler, Michael Xie
//Volhacks 2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPC_Database_Management
{
    public class Building :BaseClass
    {
        public Building() { }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<Room> Rooms { get; set;}
    }
}
