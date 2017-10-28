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
    public class Computer
    {
        public Computer() { }
        public string ID { get; set; }
        public string Name { get; set; }
        public int MemorySize { get; set; }
        public bool PrintAvailable { get; set; }
        public bool InUse { get; set; }
        public ProcessorInfo Processor { get; set; }
        public List<string> Apps { get; set; }
        public OS OperatingSystem { get; set; }
    }
}
