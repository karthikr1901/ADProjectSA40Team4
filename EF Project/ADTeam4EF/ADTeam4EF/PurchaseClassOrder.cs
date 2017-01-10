using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADTeam4EF
{
    public class PurchaseClassOrder
    {
        public string description;
        public string itemid;
        public int balance;
        public int reorderlevel;
        public int reorderqty;
        public int suggestedqty;
        public string suppliername;
        public int supplierid;
        public decimal price;
        public decimal totalcost;

        //public PurchaseClassOrder()
        //{
        //}

        public PurchaseClassOrder(string ItemID, string Description, int Balance, int ReorderLevel, int ReorderQty, int SuggestedQty, string SupplierName, int SupplierID, decimal Price,decimal TotalCost)
        {
            description = Description;
            itemid = ItemID;
            balance = Balance;
            reorderlevel = ReorderLevel;
            reorderqty = ReorderQty;
            suggestedqty = SuggestedQty;
            suppliername = SupplierName;
            supplierid = SupplierID;
            price = Price;
            totalcost = TotalCost;
        }
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
        public string ItemID
        {
            get { return this.itemid; }
            set { this.itemid = value; }
        }
        public int Balance
        {
            get { return this.balance; }
            set { this.balance = value; }
        }
        public int ReorderLevel
        {
            get { return this.reorderlevel; }
            set { this.reorderlevel = value; }
        }
        public int ReorderQty
        {
            get { return this.reorderqty; }
            set { this.reorderqty = value; }
        }
        public int SuggestedQty
        {
            get { return this.suggestedqty; }
            set { this.suggestedqty = value; }
        }

        public string SupplierName
        {
            get { return this.suppliername; }
            set { this.suppliername = value; }
        }

        public int SupplierID
        {
            get { return this.supplierid; }
            set { this.supplierid = value; }
        }

        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        public decimal TotalCost
        {
            get { return this.totalcost; }
            set { this.totalcost = value; }
        }
    }
}
