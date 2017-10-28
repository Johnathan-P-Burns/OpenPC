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
    public class Room :BaseClass
    {
        public Room() { }
        public string Name { get; set; }
        public int Floor { get; set; }
        public List<Computer> Computers { get; set; }
    }
}
