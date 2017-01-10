using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADTeam4EF
{
    public class EditRequest
    {
        ADProjectSA40Team4Entities ad = new ADProjectSA40Team4Entities();
        public dynamic DropReqNo(int emp)
        {
            var dd = (from drn in ad.Requests where drn.RequestStatus == "Pending" && drn.RequestByEmployeeID == emp select drn).ToList();
            if (dd.Count() > 0)
            {
                return dd;
            }
            else
            {
                return null;
            }
        }

        public dynamic HistDropReqNo(int emp)
        {
            var ddqw = (from drnqw in ad.Requests where (drnqw.RequestStatus == "Approved" || drnqw.RequestStatus == "Alloted" || drnqw.RequestStatus == "Rejected" || drnqw.RequestStatus == "Disbursed") && drnqw.RequestByEmployeeID == emp select drnqw).ToList();
            if (ddqw.Count() > 0)
            {
                return ddqw;
            }
            else
            {
                return null;
            }
        }
        public List<Request> HistLabelGen(int tran)
        {
            var gg = (from ffg in ad.Requests where ffg.RequestID == tran select ffg).ToList();
            List<Request> l = new List<Request>();
            foreach (var t in gg)
                l.Add(new Request() { RequestStatus = t.RequestStatus, Remark = t.Remark});
            return l;
        }
        public dynamic HistGridViewGen(int tran)
        {
            var gg = (from ffg in ad.RequestDetails join fgj in ad.Items on ffg.RequestedItem equals fgj.ItemID where ffg.RequestID == tran select new { ffg.RequestedItem, ffg.RequestID, ffg.RequestedQty, fgj.Description, ffg.ReceivedQty, fgj.UnitOfMeasurement }).ToList();
            return gg;
        }
        public dynamic GridViewGen(int tran)
        {
            var gg = (from ffg in ad.RequestDetails join fgj in ad.Items on ffg.RequestedItem equals fgj.ItemID where ffg.RequestID == tran select new { ffg.RequestedItem, ffg.RequestID, ffg.RequestedQty, fgj.Description, fgj.UnitOfMeasurement }).ToList();
            return gg;
        }

        public void UpdReqStat(int tran)
        {
            Request trt = ad.Requests.Single(tid => tid.RequestID == tran);
            trt.RequestStatus = "EDIT";
            ad.SaveChanges();
        }

        public void UpdReqStatAfterEdit(int tran)
        {
            string appr = System.DateTime.Now.ToShortDateString();
            Request trt = ad.Requests.Single(tid => tid.RequestID == tran);
            trt.RequestStatus = "Pending";
            trt.RequestDate = System.DateTime.Now.Date;
            ad.SaveChanges();
        }

        public dynamic DropCategory()
        {
            var dropcat = (from dc1 in ad.Categories select dc1).ToList();
            return dropcat;
        }


        public void AddItem(int req, string itm, int qty)
        {
            var checkitm = (from citm in ad.RequestDetails
                            where citm.RequestID == req && citm.RequestedItem == itm
                            select citm).ToList();
            if (checkitm.Count > 0)
            {
                checkitm[0].RequestedQty = qty;
                ad.SaveChanges();
            }
            else
            {
                RequestDetail reqd = new RequestDetail
                {
                    RequestID = req,
                    RequestedItem = itm,
                    RequestedQty = qty
                };


                ad.RequestDetails.Add(reqd);

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

        public dynamic DropItem(int catid)
        {
            var dropitm = (from di1 in ad.Items where di1.CategoryID == catid select new { di1.Description, di1.ItemID }).ToList();
            return dropitm;
        }

        public dynamic UnitMeasure(string itmid)
        {
            var unitms = (from u1 in ad.Items where u1.ItemID == itmid select u1.UnitOfMeasurement).ToList();
            return unitms;

        }

        public bool DeleteItem(string IID, int RTID)
        {
            using (var ctx = new ADProjectSA40Team4Entities())
            {
                var x = (from y in ctx.RequestDetails
                         where y.RequestedItem == IID && y.RequestID == RTID
                         select y).FirstOrDefault();
                ctx.RequestDetails.Remove(x);
                ctx.SaveChanges();
                var xe = (from y in ctx.RequestDetails
                          where y.RequestID == RTID
                          select y).ToList();
                if (xe.Count < 1)
                {
                    var x1 = (from y1 in ctx.Requests
                              where y1.RequestID == RTID
                              select y1).FirstOrDefault();
                    ctx.Requests.Remove(x1);
                    ctx.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public void DeleteReqNo(int tran)
        {
            using (var ctxt = new ADProjectSA40Team4Entities())
            {
                lk:
                    var x = (from y in ctxt.RequestDetails
                             where y.RequestID == tran
                             select y).FirstOrDefault();
                    if (x != null)
                    {
                        ctxt.RequestDetails.Remove(x);
                        ctxt.SaveChanges();
                        goto lk;
                    }
                    var x1 = (from y1 in ctxt.Requests
                              where y1.RequestID == tran
                              select y1).FirstOrDefault();
                    ctxt.Requests.Remove(x1);
                    ctxt.SaveChanges();
                }
            
        }

        public void ChangeSt(int empid)
        {
            zs:
            var x1 = (from y1 in ad.Requests
                      where y1.RequestStatus == "EDIT" && y1.RequestByEmployeeID == empid
                      select y1);
            if (x1.ToList().Count > 0)
            {
                Request req = x1.First();
                req.RequestStatus = "Pending";
                ad.SaveChanges();
                goto zs;
            }
        }
    }
}
