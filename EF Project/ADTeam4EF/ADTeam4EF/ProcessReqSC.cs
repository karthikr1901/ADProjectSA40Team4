using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ADTeam4EF
{
    public class ProcessReqSC
    {
        ADProjectSA40Team4Entities ad = new ADProjectSA40Team4Entities();
        public dynamic ProcessReqGrid(int av)
        {
            DateTime date1, date2, date3, date4, current1, current2;
            date1 = date2 = date3 = date4 = current1 = System.DateTime.Now;
            current2 = System.DateTime.Now.Date.AddHours(9).AddMinutes(45);
            DayOfWeek f1 = current1.DayOfWeek;
            string day = Convert.ToString(f1);
            if (day == "Monday")
            {
                if (current1 <= current2)
                {
                    date1 = current1.AddDays(-12).Date.AddHours(17);
                }
                else
                { date1 = current1.AddDays(-5).Date.AddHours(17); }
            }
            else
            {
                switch (day)
                {
                    case "Sunday": date1 = current1.AddDays(-11).Date.AddHours(17); break;
                    case "Tuesday": date1 = current1.AddDays(-6).Date.AddHours(17); break;
                    case "Wednesday": date1 = current1.AddDays(-7).Date.AddHours(17); break;
                    case "Thursday": date1 = current1.AddDays(-8).Date.AddHours(17); break;
                    case "Friday": date1 = current1.AddDays(-9).Date.AddHours(17); break;
                    case "Saturday": date1 = current1.AddDays(-10).Date.AddHours(17); break;
                }
            }

            date2 = date1.AddDays(5).Date.AddHours(9).AddMinutes(45);
            date3 = date1.AddDays(7).Date.AddHours(17);
            date4 = date1.AddDays(12).Date.AddHours(9).AddMinutes(45);
            Allocate(date1, date3);
            return GridDisp(av);
        }

        public dynamic DropItem()
        {
            var dropitm = (from di1 in ad.Items select new { di1.Description, di1.ItemID }).ToList();
            return dropitm;
        }


        public List<ProcessReqMobile> MobileReq(string itemid, string damageqty, string eid1)
        {
            int req = NewRaiseAdj(Int32.Parse(eid1));
            RaiseAdjDetails(itemid, Int32.Parse(damageqty), req);
            DamageAllocate(itemid, Int32.Parse(damageqty));
            return MobileGrid();
        }

        public int NewRaiseAdj(int eid1)
        {
            int req = 0;
            var genAdjNo = (from gadrn1 in ad.Adjustments where gadrn1.AdjustmentStatus == "NEW" && gadrn1.AdjustedByEmployeeID == eid1 select gadrn1).ToList();
            if (genAdjNo.Count > 0)
                req = genAdjNo[0].AdjustmentID;
            else
            {
                Adjustment areq = new Adjustment
                {
                    AdjustmentStatus = "NEW",
                    AdjustedByEmployeeID = eid1
                };

                ad.Adjustments.Add(areq);

                try
                {
                    ad.SaveChanges();
                }
                catch (Exception tye)
                {
                    Console.WriteLine(tye);

                }
                var raisead = (from graise1 in ad.Adjustments where graise1.AdjustmentStatus == "NEW" && graise1.AdjustedByEmployeeID == eid1 select graise1).ToList();
                req = raisead[0].AdjustmentID;
            }
            return req;
        }

        public void RaiseAdjDetails(string itemid, int damageqty, int req)
        {
            var raisead = (from graise1 in ad.AdjustmentDetails where graise1.AdjustmentID == req && graise1.ItemID == itemid select graise1).ToList();
            if (raisead.Count > 0)
            {
                raisead[0].Quantity += damageqty;
                ad.SaveChanges();
            }
            else
            {
                AdjustmentDetail adjd = new AdjustmentDetail
                {
                    AdjustmentID = req,
                    ItemID = itemid,
                    AdjustmentRemark = "Broken Items",
                    Quantity = damageqty
                };


                ad.AdjustmentDetails.Add(adjd);

                try
                {
                    ad.SaveChanges();
                }
                catch (Exception tye)
                {
                    Console.WriteLine(tye);

                }
            }

        }

        public void UpdAdjNo(int eid1)
        {
            decimal ptotal = 0;
            var raisead = (from graise1 in ad.Adjustments where graise1.AdjustmentStatus == "NEW" && graise1.AdjustedByEmployeeID == eid1 select graise1).ToList();
            int req = raisead[0].AdjustmentID;
            var cost = (from costadj in ad.AdjustmentDetails join price in ad.SupplyItems on costadj.ItemID equals price.ItemID where costadj.AdjustmentID == req && price.Priority == 1 select new { costadj.ItemID, costadj.Quantity, price.Price}).ToList();
            foreach (var pricetotal in cost)
                ptotal += (decimal)(pricetotal.Quantity * pricetotal.Price);

            Adjustment updadj = (from c in ad.Adjustments where c.AdjustmentID == req select c).SingleOrDefault(); //ad.Requests.Single(rid => rid.RequestID == reqtid);
            updadj.AdjustmentStatus = "Pending";
            updadj.TotalPrice = ptotal;
            updadj.RequestAdjustmentDate = System.DateTime.Now.Date;

            try
            {
                ad.SaveChanges();
                //sendmail(reqtid);
            }
            catch (Exception tye)
            {
                Console.WriteLine(tye);
            }
        }


        public void DamageAllocate(string itemid, int damageqty)
        {
            var italloc = (from itall in ad.Items where itall.ItemID == itemid select itall).ToList();

            if (italloc[0].Balance > damageqty)
            {
                italloc[0].Balance = italloc[0].Balance - damageqty;
                damageqty = 0;
                var all12 = from a12 in ad.Items where a12.ItemID == itemid select a12;
                Item it12 = all12.First();
                it12.Balance = italloc[0].Balance;
                ad.SaveChanges();
            }
            else if (italloc[0].Balance > 0)
            {
                damageqty = damageqty - (int)italloc[0].Balance;
                italloc[0].Balance = 0;
                var all12 = from a12 in ad.Items where a12.ItemID == itemid select a12;
                Item it12 = all12.First();
                it12.Balance = italloc[0].Balance;
                ad.SaveChanges();

                var allocat = (from a in (from alloc in ad.Requests where alloc.RequestStatus == "Alloted" select new { alloc.RequestID, alloc.ApproveDate }) orderby a.ApproveDate select a.RequestID);
                int[] s = allocat.ToArray();
                for (int y = s.Length - 1; y >= 0; y--)
                {
                    if (damageqty == 0) break;
                    int sos = s[y];
                    var allocat1 = (from alloc1 in ad.RequestDetails where alloc1.RequestID == sos && alloc1.RequestedItem == itemid select alloc1).ToList();
                    if (allocat1.Count > 0)
                    {
                        if (allocat1[0].ReceivedQty > damageqty)
                        {
                            allocat1[0].ReceivedQty = allocat1[0].ReceivedQty - damageqty;
                            damageqty = 0;
                            var all11 = from a11 in ad.RequestDetails where a11.RequestID == sos && a11.RequestedItem == itemid select a11;
                            RequestDetail rd = all11.First();
                            rd.ReceivedQty = allocat1[0].ReceivedQty;
                            ad.SaveChanges();
                        }
                        else
                        {
                            damageqty = damageqty - (int)allocat1[0].ReceivedQty;
                            allocat1[0].ReceivedQty = 0;
                            var all11 = from a11 in ad.RequestDetails where a11.RequestID == sos && a11.RequestedItem == itemid select a11;
                            RequestDetail rd = all11.First();
                            rd.ReceivedQty = allocat1[0].ReceivedQty;
                            ad.SaveChanges();
                        }
                    }

                }
            }
            else
            {

                var allocat = (from a in (from alloc in ad.Requests where alloc.RequestStatus == "Alloted" select new { alloc.RequestID, alloc.ApproveDate }) orderby a.ApproveDate select a.RequestID);
                int[] s = allocat.ToArray();
                for (int y = s.Length - 1; y >= 0; y--)
                {
                    if (damageqty == 0) break;
                    int sos = s[y];
                    var allocat1 = (from alloc1 in ad.RequestDetails where alloc1.RequestID == sos && alloc1.RequestedItem == itemid select alloc1).ToList();
                    if (allocat1.Count > 0)
                    {
                        if (allocat1[0].ReceivedQty > damageqty)
                        {
                            allocat1[0].ReceivedQty = allocat1[0].ReceivedQty - damageqty;
                            damageqty = 0;
                            var all11 = from a11 in ad.RequestDetails where a11.RequestID == sos && a11.RequestedItem == itemid select a11;
                            RequestDetail rd = all11.First();
                            rd.ReceivedQty = allocat1[0].ReceivedQty;
                            ad.SaveChanges();
                        }
                        else
                        {
                            damageqty = damageqty - (int)allocat1[0].ReceivedQty;
                            allocat1[0].ReceivedQty = 0;
                            var all11 = from a11 in ad.RequestDetails where a11.RequestID == sos && a11.RequestedItem == itemid select a11;
                            RequestDetail rd = all11.First();
                            rd.ReceivedQty = allocat1[0].ReceivedQty;
                            ad.SaveChanges();
                        }
                    }

                }
            }
        }


        public List<ProcessReqMobile> MobileGrid()
        {
            var eq = (from tq in ad.Requests where tq.RequestStatus == "Alloted" select tq.RequestID);
            int[] ssq = eq.ToArray();

            var qw1 = (from aa1 in
                           ((from t1 in ad.Requests
                             join d1 in ad.RequestDetails on t1.RequestID equals d1.RequestID
                             join c1 in ad.Items on d1.RequestedItem equals c1.ItemID
                             where ssq.Contains((int)(d1.RequestID))
                             select new { d1.RequestedItem, c1.Description, c1.Balance, c1.UnitOfMeasurement, d1.RequestedQty, d1.ReceivedQty }).ToList())
                       group aa1 by new { aa1.RequestedItem, aa1.Balance, aa1.UnitOfMeasurement, aa1.Description } into gg1
                       select new { gg1.Key.RequestedItem, gg1.Key.Description, gg1.Key.Balance, gg1.Key.UnitOfMeasurement, TNeeded = (int?)gg1.Sum(p1 => p1.RequestedQty), TAlloted = (int?)gg1.Sum(pw1 => pw1.ReceivedQty) }).ToList();

            var uu = from io in qw1 orderby io.Description select new { io.RequestedItem, io.Description, io.Balance, io.UnitOfMeasurement, io.TNeeded, io.TAlloted };


            var hfh = (from kklk in uu select kklk).ToList();

            ProcessReqMobile hh;
            List<ProcessReqMobile> hhList = new List<ProcessReqMobile>();
            foreach (var i in hfh)
            {
                hh = new ProcessReqMobile(i.RequestedItem, i.Description, (int)i.Balance, i.UnitOfMeasurement, (int)i.TNeeded, (int)i.TAlloted);
                hhList.Add(hh);
            }
            //hh = uu;

            return hhList;
        }

        public dynamic GridDisp(int av)
        {
            DateTime date1, date2, date3, date4, current1, current2;
            date1 = date2 = date3 = date4 = current1 = System.DateTime.Now;
            current2 = System.DateTime.Now.Date.AddHours(9).AddMinutes(45);
            DayOfWeek f1 = current1.DayOfWeek;
            string day = Convert.ToString(f1);
            if (day == "Monday")
            {
                if (current1 <= current2)
                {
                    date1 = current1.AddDays(-12).Date.AddHours(17);
                }
                else
                { date1 = current1.AddDays(-5).Date.AddHours(17); }
            }
            else
            {
                switch (day)
                {
                    case "Sunday": date1 = current1.AddDays(-11).Date.AddHours(17); break;
                    case "Tuesday": date1 = current1.AddDays(-6).Date.AddHours(17); break;
                    case "Wednesday": date1 = current1.AddDays(-7).Date.AddHours(17); break;
                    case "Thursday": date1 = current1.AddDays(-8).Date.AddHours(17); break;
                    case "Friday": date1 = current1.AddDays(-9).Date.AddHours(17); break;
                    case "Saturday": date1 = current1.AddDays(-10).Date.AddHours(17); break;
                }
            }

            date2 = date1.AddDays(5).Date.AddHours(9).AddMinutes(45);
            date3 = date1.AddDays(7).Date.AddHours(17);
            date4 = date1.AddDays(12).Date.AddHours(9).AddMinutes(45);
            DateTime tnow = System.DateTime.Now;

            string statushard = "";
            var eq = (from tq in ad.Requests where tq.RequestStatus == statushard select tq.RequestID);
            if (av == 1)
            {
                statushard = "Approved ";
                eq = (from tq in ad.Requests where tq.RequestStatus == statushard && tq.ApproveDate > date1 && tq.ApproveDate <= date3 select tq.RequestID);
            }
            else
            {
                statushard = "Alloted";
                eq = (from tq in ad.Requests where tq.RequestStatus == statushard select tq.RequestID);
            }
            int[] ssq = eq.ToArray();

            var qw1 = (from aa1 in
                           ((from t1 in ad.Requests
                             join d1 in ad.RequestDetails on t1.RequestID equals d1.RequestID
                             join c1 in ad.Items on d1.RequestedItem equals c1.ItemID
                             where ssq.Contains((int)(d1.RequestID))
                             select new { d1.RequestedItem, c1.Description, c1.Balance, c1.UnitOfMeasurement, d1.RequestedQty, d1.ReceivedQty }).ToList())
                       group aa1 by new { aa1.RequestedItem, aa1.Balance, aa1.UnitOfMeasurement, aa1.Description } into gg1
                       select new { gg1.Key.RequestedItem, gg1.Key.Description, gg1.Key.Balance, gg1.Key.UnitOfMeasurement, TNeeded = (System.Int32?)gg1.Sum(p1 => p1.RequestedQty), TAlloted = (System.Int32?)gg1.Sum(pw1 => pw1.ReceivedQty) }).ToList();



            var qw = (from aa in
                          ((from t in ad.Requests
                            join d in ad.RequestDetails on t.RequestID equals d.RequestID
                            join c in ad.Items on d.RequestedItem equals c.ItemID
                            where ssq.Contains((int)(d.RequestID))
                            select new { d.RequestedItem, c.Description, c.Balance, c.UnitOfMeasurement, t.RequestByDepartmentID, d.RequestedQty, d.ReceivedQty }).ToList())
                      group aa by new { aa.RequestedItem, aa.Balance, aa.UnitOfMeasurement, aa.Description, aa.RequestByDepartmentID } into gg
                      select new
                      {
                          gg.Key.RequestedItem,
                          gg.Key.Description,
                          gg.Key.Balance,
                          gg.Key.UnitOfMeasurement,
                          gg.Key.RequestByDepartmentID,
                          Needed = (System.Int32?)gg.Sum(p => p.RequestedQty),
                          Alloted = (System.Int32?)gg.Sum(pw => pw.ReceivedQty)
                      }).ToList();




            var three = from d1 in qw
                        join d2 in qw1 on new { d1.RequestedItem, d1.Description, d1.Balance, d1.UnitOfMeasurement }
                        equals new { d2.RequestedItem, d2.Description, d2.Balance, d2.UnitOfMeasurement } into j
                        from d2 in j.DefaultIfEmpty()
                        select new
                        {
                            d1.RequestedItem,
                            d1.Description,
                            d1.Balance,
                            d1.UnitOfMeasurement,
                            d2.TNeeded,
                            d2.TAlloted,
                            d1.RequestByDepartmentID,
                            d1.Needed,
                            d1.Alloted

                        };



            var uu = from io in three orderby io.Description select new { io.RequestedItem, io.Description, io.Balance, io.UnitOfMeasurement, io.TNeeded, io.TAlloted, io.RequestByDepartmentID, io.Needed, io.Alloted };


            var hfh = (from kklk in uu select kklk).ToList();


            return hfh;
        }


        public void Allocate(DateTime date1, DateTime date3)
        {
           
            var allocat = (from a in (from alloc in ad.Requests where alloc.RequestStatus == "Approved" && alloc.ApproveDate > date1 && alloc.ApproveDate <= date3 select new { alloc.RequestID, alloc.ApproveDate }) orderby a.ApproveDate select a.RequestID);
            if (allocat.Count() > 0)
            {
                int[] s = allocat.ToArray();
                for (int y = 0; y < s.Length; y++)
                {
                    int sos = s[y];
                    var allocat1 = (from alloc1 in ad.RequestDetails where alloc1.RequestID == sos select alloc1).ToList();

                    foreach (var t in allocat1)
                    {
                        var italloc = (from itall in ad.Items where itall.ItemID == t.RequestedItem select itall.Balance).ToList();
                        if (Convert.ToInt32(italloc[0]) > t.RequestedQty)
                        {
                            t.ReceivedQty = t.RequestedQty;
                            italloc[0] = Convert.ToInt32(italloc[0]) - t.RequestedQty;
                        }
                        else
                        {
                            t.ReceivedQty = Convert.ToInt32(italloc[0]);
                            italloc[0] = Convert.ToInt32(italloc[0]) - t.ReceivedQty;
                        }
                        var all11 = from a11 in ad.RequestDetails where a11.RequestID == sos && a11.RequestedItem == t.RequestedItem select a11;
                        RequestDetail rd = all11.First();
                        rd.ReceivedQty = t.ReceivedQty;
                        ad.SaveChanges();
                        var all12 = from a12 in ad.Items where a12.ItemID == t.RequestedItem select a12;
                        Item it12 = all12.First();
                        it12.Balance = italloc[0];
                        ad.SaveChanges();
                        var all15 = from a11 in ad.Requests where a11.RequestID == sos select a11;
                        Request rd15 = all15.First();
                        rd15.RequestStatus = "Alloted";
                        ad.SaveChanges();
                    }
                }
            }
        }

        public bool OutstandingAllocate()
        {           
            var outall = (from outal in ad.Requests where outal.RequestStatus == "Outstanding" select outal.RequestID);
            if (outall.Count() > 0)
            {
                 var outall1 = (from outal1 in ad.RequestDetails
                           join outal2 in ad.Requests on outal1.RequestID equals outal2.RequestID
                           join outitm in ad.Items on outal1.RequestedItem equals outitm.ItemID
                           where outal2.RequestStatus == "Outstanding"
                           select new { outal1.RequestedItem , outal1.RequestedQty, outitm.Balance }).ToList();
                 foreach (var t in outall1)
                 {
                     if (((int)t.Balance - (int)t.RequestedQty) < 0)
                         return false;
                 }
                int[] soutal = outall.ToArray();
                for (int y = 0; y < soutal.Length; y++)
                {
                    int soso = soutal[y];
                    var allocat1 = (from alloc1 in ad.RequestDetails where alloc1.RequestID == soso select alloc1).ToList();

                    foreach (var t in allocat1)
                    {
                        var italloc = (from itall in ad.Items where itall.ItemID == t.RequestedItem select itall.Balance).ToList();
                        t.ReceivedQty = t.RequestedQty;
                        italloc[0] = Convert.ToInt32(italloc[0]) - t.RequestedQty;
                        var all11 = from a11 in ad.RequestDetails where a11.RequestID == soso && a11.RequestedItem == t.RequestedItem select a11;
                        RequestDetail rd = all11.First();
                        rd.ReceivedQty = t.ReceivedQty;
                        ad.SaveChanges();
                        var all12 = from a12 in ad.Items where a12.ItemID == t.RequestedItem select a12;
                        Item it12 = all12.First();
                        it12.Balance = italloc[0];
                        ad.SaveChanges();
                        var all15 = from a11 in ad.Requests where a11.RequestID == soso select a11;
                        Request rd15 = all15.First();
                        rd15.RequestStatus = "Alloted";
                        ad.SaveChanges();
                    }
                }
                return true;
            }
            else
                return false;
        }

        public bool GetOutstandingAllocate()
        {
            var outall1 = (from outal1 in ad.RequestDetails
                           join outal2 in ad.Requests on outal1.RequestID equals outal2.RequestID
                           join outitm in ad.Items on outal1.RequestedItem equals outitm.ItemID
                           where outal2.RequestStatus == "Outstanding"
                           select new { outal1.RequestedItem, outal1.RequestedQty, outitm.Balance }).ToList();
            if (outall1.Count > 0)
            {
                foreach (var t in outall1)
                {
                    if (((int)t.Balance - (int)t.RequestedQty) < 0)
                        return false;
                }
                return true;
            }
            else return false;
        }

        public int GetCount()
        {
            var et = GridDisp(1);
            int ascount = et.Count;
            return ascount;
        }
    }
}