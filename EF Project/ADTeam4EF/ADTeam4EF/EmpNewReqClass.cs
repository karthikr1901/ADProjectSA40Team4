using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADTeam4EF
{
    public class EmpNewReqClass
    {
        public string Description;
        public int RequestedQty;
        public string RequestedItem;
        public string UnitOfMeasurement;

        public EmpNewReqClass(string description, int requestedqty, string requesteditem, string unitofmeasurement)
        {
            Description = description;
            RequestedQty = requestedqty;
            RequestedItem = requesteditem;
            UnitOfMeasurement = unitofmeasurement;
        }
        public string description
        {
            get { return this.Description; }
            set { this.Description = value; }
        }
        public int requestedqty
        {
            get { return this.RequestedQty; }
            set { this.RequestedQty = value; }
        }

        public string requesteditem
        {
            get { return this.RequestedItem; }
            set { this.RequestedItem = value; }
        }

        public string unitofmeasurement
        {
            get { return this.UnitOfMeasurement; }
            set { this.UnitOfMeasurement = value; }
        }
    }
}
