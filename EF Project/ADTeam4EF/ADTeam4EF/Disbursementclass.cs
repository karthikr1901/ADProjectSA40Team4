using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADTeam4EF
{
    public class Disbursementclass
    {
        public string description;
        public int requestedqty;
        public int recievedqty;
        public int outstandingqty;
        public string unitofmeasurement;

        public Disbursementclass(string Description, int RequestedQty, int RecievedQty, int OutstandingQty, string UnitOfMeasurement)
        {
            description = Description;
            requestedqty = RequestedQty;
            recievedqty = RecievedQty;
            outstandingqty = OutstandingQty;
            unitofmeasurement = UnitOfMeasurement;
        }
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
        public int RequestedQty
        {
            get { return this.requestedqty; }
            set { this.requestedqty = value; }
        }
        public int RecievedQty
        {
            get { return this.recievedqty; }
            set { this.recievedqty = value; }
        }
        public int OutstandingQty
        {
            get { return this.outstandingqty; }
            set { this.outstandingqty = value; }
        }

        public string UnitOfMeasurement
        {
            get { return this.unitofmeasurement; }
            set { this.unitofmeasurement = value; }
        }
    }
}
