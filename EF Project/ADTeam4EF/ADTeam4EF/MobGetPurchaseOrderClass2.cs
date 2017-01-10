using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADTeam4EF
{
    public class MobGetPurchaseOrderClass2
    {
        public string itemid;
        public string description;
        public string unitofmeasurement;
        public int orderQty;
        public int receivedQty;
        public string orderDate;
        public string expectedDeliveryDate;

        public MobGetPurchaseOrderClass2(string ItemID, string Description, string UnitOfMeasurement, int OrderQty, string OrderDate, string ExpectedDeliveryDate, int ReceivedQty)
        {
            itemid = ItemID;
            description = Description;
            unitofmeasurement = UnitOfMeasurement;
            orderQty = OrderQty;
            orderDate = OrderDate;
            expectedDeliveryDate = ExpectedDeliveryDate;
            receivedQty = ReceivedQty;
        }

        public string ItemID
        {
            get { return this.itemid; }
            set { this.itemid = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public string UnitOfMeasurement
        {
            get { return this.unitofmeasurement; }
            set { this.unitofmeasurement = value; }
        }

        public int OrderQty
        {
            get { return this.orderQty; }
            set { this.orderQty = value; }
        }

        public int ReceivedQty
        {
            get { return this.receivedQty; }
            set { this.receivedQty = value; }
        }

        public string OrderDate
        {
            get { return this.orderDate; }
            set { this.orderDate = value; }
        }

        public string ExpectedDeliveryDate
        {
            get { return this.expectedDeliveryDate; }
            set { this.expectedDeliveryDate = value; }
        }
    }
}
