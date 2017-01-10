using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTeam4EF
{
    public class ApproveAdjustmentController
    {
        ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();
        public List<Adjustment> getVoucherNumber(string userName)
        {
            var adj = new List<Adjustment>();
            var rID = from emp in ctx.Employees
                         where emp.EmployeeName == userName
                         select emp.RoleID;
            int roleID = (int)rID.First();
            try
            {
                if (roleID == 7)
                {
                     adj = (from adjust in ctx.Adjustments
                              where adjust.AdjustmentStatus == "Pending" && adjust.TotalPrice < 250
                              select adjust).ToList<Adjustment>();
                   
                }
                else if (roleID == 5)
                {
                     adj = (from adjust in ctx.Adjustments
                              where adjust.AdjustmentStatus == "Pending" && adjust.TotalPrice >= 250
                              select adjust).ToList<Adjustment>();
                
                }
                if (adj.Count() > 0)
                {
                    return adj;

                }
                else
                {
                    return null;
                }
            }

            //try
            //{
            //    var adj = from adjust in ctx.Adjustments
            //              where adjust.AdjustmentStatus == "Pending"
            //              select adjust;
            //    return adj.ToList<Adjustment>();
            //}
            catch
            {
                return null;
            }
        }

        public List<AdjVoucherNumber> getVoucherNumberMobile(string roleID)
        {
            try
            {
                if (Int16.Parse(roleID) == 7)
                {
                    var adj = (from adjust in ctx.Adjustments
                              where adjust.AdjustmentStatus == "Pending" && adjust.TotalPrice < 250
                              select new {adjust.AdjustmentID }).ToList();
                    List<AdjVoucherNumber> lobj = new List<AdjVoucherNumber>();
                    foreach (var t in adj)
                        lobj.Add(new AdjVoucherNumber(t.AdjustmentID.ToString()));
                    return lobj;

                }
                else 
                {
                    var adj = (from adjust in ctx.Adjustments
                              where adjust.AdjustmentStatus == "Pending" && adjust.TotalPrice >= 250
                              select new { adjust.AdjustmentID }).ToList();
                    List<AdjVoucherNumber> lobj = new List<AdjVoucherNumber>();
                    foreach (var t in adj)
                        lobj.Add(new AdjVoucherNumber(t.AdjustmentID.ToString()));
                    return lobj;
                }
            }
            catch
            {
                return null;
            }
        }
		

        public dynamic getAdjustmentList(int AdjustID)
        {
            try
            {
                var adjList = (from adjust in ctx.Adjustments
                               join ad in ctx.AdjustmentDetails on adjust.AdjustmentID equals ad.AdjustmentID
                               join i in ctx.Items on ad.ItemID equals i.ItemID
                               join si in ctx.SupplyItems on i.ItemID equals si.ItemID
                               where adjust.AdjustmentID == AdjustID && si.Priority == 1
                               select new { i.Description, ad.Quantity, i.UnitOfMeasurement, si.Price, Amount = (ad.Quantity * si.Price), adjust.TotalPrice, ad.AdjustmentRemark }).ToList();

                return adjList;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                throw e;
            }
        }

        public List<ApproveAdjustmentMobile> getAdjustmentListMobile(string AdjustID)
        {
            int adjID = Int32.Parse(AdjustID);

            var adjList = (from adjust in ctx.Adjustments
                           join ad in ctx.AdjustmentDetails on adjust.AdjustmentID equals ad.AdjustmentID
                           join i in ctx.Items on ad.ItemID equals i.ItemID
                           join si in ctx.SupplyItems on i.ItemID equals si.ItemID
                           where adjust.AdjustmentID == adjID && si.Priority == 1 
                           select new { i.ItemID, ad.Quantity, i.UnitOfMeasurement, si.Price, Amount = (ad.Quantity * si.Price), adjust.TotalPrice, ad.AdjustmentRemark }).ToList();

            
            ApproveAdjustmentMobile AppAdjMob;
            List<ApproveAdjustmentMobile> AppAdjMobList = new List<ApproveAdjustmentMobile>();
            foreach (var i in adjList)
            {
                //public ApproveAdjustmentMobile(string description, int balance, string uom, decimal unitPrice, decimal amount, decimal totalPrice, string remark)
                AppAdjMob = new ApproveAdjustmentMobile(i.ItemID, (int)i.Quantity, i.UnitOfMeasurement, (decimal)i.Price, (decimal)i.Amount, (decimal)i.TotalPrice, i.AdjustmentRemark);
                AppAdjMobList.Add(AppAdjMob);
            }
            //hh = uu;

            return AppAdjMobList;
        }

        public Boolean approvedAdjustmentList(int AdjustID, string userName)
        {
            try
            {
                var adjList = from adjust in ctx.Adjustments
                              where adjust.AdjustmentID == AdjustID
                              select adjust;
                var userID = from emp in ctx.Employees
                             where emp.EmployeeName == userName
                             select emp.EmployeeID;
                if (adjList.Count() > 0)
                {
                    Adjustment adj = adjList.First();
                    adj.AdjustmentStatus = "Approved";
                    adj.ApprovedByEmployeeID = userID.First();
                    adj.ApproveAdjustmentDate = DateTime.Now;
                    ctx.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                throw e;
            }

        }

        public String approvedAdjustmentListMobile(string AdjustID, string empID)
        {
            String ret = "";
            int adjID = Int16.Parse(AdjustID);
            int rID = Int16.Parse(empID);
            try
            {
                var adjList = from adjust in ctx.Adjustments
                              where adjust.AdjustmentID == adjID
                              select adjust;
                //var userID = from emp in ctx.Employees
                //             where emp.EmployeeName == userName
                //             select emp.EmployeeID;
                if (adjList.Count() > 0)
                {
                    Adjustment adj = adjList.First();
                    adj.AdjustmentStatus = "Approved";
                    adj.ApprovedByEmployeeID = rID;
                    adj.ApproveAdjustmentDate = DateTime.Now;
                    ctx.SaveChanges();
                    return "SUCCESS";
                }
                else
                {
                    return "FAIL";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                throw e;
            }

        }


    }
}
