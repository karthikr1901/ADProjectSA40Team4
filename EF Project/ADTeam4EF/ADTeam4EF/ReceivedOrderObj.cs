using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTeam4EF
{
    public class ReceivedOrderObj
    {
        public string ItemID { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasurement { get; set; }
        public int OrderQty { get; set; }
        public int ReceivedQty { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
    }
}
