using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTeam4EF
{
    public class ApproveAdjustmentMobile
    {
        public string description;
        public int balance;
        public string uom;
        public decimal unitPrice;
        public decimal amount;
        public decimal totalPrice;
        public string remark;

        public ApproveAdjustmentMobile(string description, int balance, string uom, decimal unitPrice, decimal amount, decimal totalPrice, string remark)
        {
            this.description = description;
            this.balance = balance;
            this.uom = uom;
            this.unitPrice = unitPrice;
            this.amount = amount;
            this.totalPrice = totalPrice;
            this.remark = remark;
        }

    }



}
