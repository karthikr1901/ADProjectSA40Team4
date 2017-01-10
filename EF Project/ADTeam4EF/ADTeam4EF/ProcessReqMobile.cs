using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADTeam4EF
{
    public class ProcessReqMobile
    {
        public string itemid;
        public string description;
        public int balance;
        public string uom;
        public int tneeded;
        public int talloted;

        public ProcessReqMobile(string ItemID, string Description, int Balance, string UnitOfMeasurement, int TNeeded, int TAlloted)
        {
            itemid = ItemID;
            description = Description;
            balance = Balance;
            uom = UnitOfMeasurement;
            tneeded = TNeeded;
            talloted = TAlloted;
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
        public int Balance
        {
            get { return this.balance; }
            set { this.balance = value; }
        }
        public string UnitOfMeasurement
        {
            get { return this.uom; }
            set { this.uom = value; }
        }
        public int TNeeded
        {
            get { return this.tneeded; }
            set { this.tneeded = value; }
        }
        public int TAlloted
        {
            get { return this.talloted; }
            set { this.talloted = value; }
        }


    }
}
