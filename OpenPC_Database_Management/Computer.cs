using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPC_Database_Management
{
    class Computer
    {
        string ID { get; set; }
        string Name { get; set; }
        int MemorySize { get; set; }
        bool PrintAvailable { get; set; }
        ProcessorInfo Processor { get; set; }
        List<Application> Apps { get; set; }
        List<Feature> Features { get; set; }
        OS OperatingSystem { get; set; }
    }
}
