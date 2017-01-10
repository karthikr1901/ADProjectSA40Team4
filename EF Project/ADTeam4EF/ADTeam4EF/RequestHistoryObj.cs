using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTeam4EF
{
    public class RequestHistoryObj
    {
        public string Description { get; set; }
        public int RequestedQty { get; set; }
        public int ReceivedQty { get; set; }
        public string Unitofmeasurement { get; set; }

    }
}
