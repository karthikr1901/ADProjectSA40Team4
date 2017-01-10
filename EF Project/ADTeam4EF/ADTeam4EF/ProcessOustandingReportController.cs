using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ADTeam4EF
{
    public class ProcessOustandingReportController
    {
        ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();
                
        public DataTable getOutstandingDataForGrid()
        {
            var grid = (from req in ctx.Requests
                        join reqDetail in ctx.RequestDetails on req.RequestID equals reqDetail.RequestID
                        join i in ctx.Items on reqDetail.RequestedItem equals i.ItemID
                        join dep in ctx.Departments on req.RequestByDepartmentID equals dep.DepartmentID
                        where req.RequestStatus == "Outstanding"
                        select new { req.RequestID, i.ItemID, i.Description, dep.DepartmentName, reqDetail.RequestedQty, reqDetail.ReceivedQty, i.UnitOfMeasurement }).ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("RequestID");
            dt.Columns.Add("ItemID");
            dt.Columns.Add("Description");
            dt.Columns.Add("DepartmentName");
            dt.Columns.Add("UnitOfMeasurement");
            dt.Columns.Add("RequestedQty");
            dt.Columns.Add("ReceivedQty");
            //DataRow dr;

            foreach (var t in grid)
            {
                dt.Rows.Add(t.RequestID, t.ItemID, t.Description, t.DepartmentName, t.UnitOfMeasurement, t.RequestedQty,t.ReceivedQty);
            }

            return dt;
        }

        

        
        public void DeleteOutstanding(int reqID, string itemID)
        {
            var rqDetail = from reqDetail in ctx.RequestDetails
                           where reqDetail.RequestID == reqID && reqDetail.RequestedItem == itemID
                           select reqDetail;

            RequestDetail reqD = rqDetail.FirstOrDefault();
            
            if(reqD != null)
            {
                ctx.RequestDetails.Remove(reqD);
                ctx.SaveChanges();
            }

            
        }

        public void deleteRequest(int reqID)
        {
            var req = from r in ctx.Requests
                      where r.RequestID == reqID
                      select r;

            Request request = req.FirstOrDefault();

            if (request != null)
            {
                ctx.Requests.Remove(request);
                ctx.SaveChanges();
            }
        }

        public int getReqDetailCount(int reqID)
        {
            var req = (from rd in ctx.RequestDetails
                      where rd.RequestID == reqID
                      select rd).Count();

            return req;
        }

        public bool OutstandingAllocate(int empID)
        {
            var outall = (from outal in ctx.Requests where outal.RequestStatus == "Outstanding" select outal.RequestID);
            if (outall.Count() > 0)
            {
                var outall1 = (from outal1 in ctx.RequestDetails
                               join outal2 in ctx.Requests on outal1.RequestID equals outal2.RequestID
                               join outitm in ctx.Items on outal1.RequestedItem equals outitm.ItemID
                               where outal2.RequestStatus == "Outstanding"
                               select new { outal1.RequestedItem, outal1.RequestedQty, outitm.Balance }).ToList();
                foreach (var t in outall1)
                {
                    if (((int)t.Balance - (int)t.RequestedQty) < 0)
                        return false;
                }
                int[] soutal = outall.ToArray();
                for (int y = 0; y < soutal.Length; y++)
                {
                    int soso = soutal[y];
                    var allocat1 = (from alloc1 in ctx.RequestDetails where alloc1.RequestID == soso select alloc1).ToList();

                    foreach (var t in allocat1)
                    {
                        var italloc = (from itall in ctx.Items where itall.ItemID == t.RequestedItem select itall.Balance).ToList();
                        t.ReceivedQty = t.RequestedQty;
                        italloc[0] = Convert.ToInt32(italloc[0]) - t.RequestedQty;
                        var all11 = from a11 in ctx.RequestDetails where a11.RequestID == soso && a11.RequestedItem == t.RequestedItem select a11;
                        RequestDetail rd = all11.First();
                        rd.ReceivedQty = t.ReceivedQty;
                        ctx.SaveChanges();
                        var all12 = from a12 in ctx.Items where a12.ItemID == t.RequestedItem select a12;
                        Item it12 = all12.First();
                        it12.Balance = italloc[0];
                        ctx.SaveChanges();
                        var all15 = from a11 in ctx.Requests where a11.RequestID == soso select a11;
                        Request rd15 = all15.First();
                        rd15.RequestStatus = "Alloted";
                        rd15.ProcessedByEmployeeID = empID;
                        ctx.SaveChanges();
                    }
                }
                return true;
            }
            else
                return false;
        }

    }
    }

