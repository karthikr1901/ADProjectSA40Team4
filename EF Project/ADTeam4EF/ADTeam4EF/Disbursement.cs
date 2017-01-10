using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ADTeam4EF
{    
    public class Disbursement
    {
        ADProjectSA40Team4Entities ad = new ADProjectSA40Team4Entities();
        public dynamic DisburseGrid()
        {

            return null;
        }

        public List<CollectionPoint> CollectRadio(string empids)
        {
            int empid = Convert.ToInt32(empids);
            var cp = (from cp1 in ad.CollectionPoints where cp1.InCharge == empid select cp1).ToList();
            List<CollectionPoint> lcp = new List<CollectionPoint>();
            foreach (var t in cp)
                lcp.Add(t);
            return lcp;
        }

        public List<CollectionPoint> PlaceLab(string empids, string cids)
        {
            int empid = Convert.ToInt32(empids);
            int cid = Convert.ToInt32(cids);
            var cp = (from cp1 in ad.CollectionPoints where cp1.CollectionPointID == cid && cp1.InCharge == empid select cp1).ToList();
            List<CollectionPoint> lcp = new List<CollectionPoint>();
            foreach (var t in cp)
                lcp.Add(t);
            return lcp;
        }

        public List<Employee> DisburseGrid(string cptds)
        {
            int cptd = Convert.ToInt32(cptds);
            var dgrid = (from dg in ad.Departments
                        join emm in ad.Employees on dg.DepartmentID equals emm.DepartmentID
                        join odd in ad.Requests on dg.DepartmentID equals odd.RequestByDepartmentID
						 join ith in ad.Roles on emm.RoleID equals ith.RoleID
                        where dg.CollectionPointID == cptd && ith.RoleName == "Department Representative" && odd.RequestStatus == "Alloted"
                        select new {emm.DepartmentID, emm.EmployeeName , emm.EmployeeEmail}).Distinct().ToList();
            List<Employee> l = new List<Employee>();
            foreach(var t in dgrid)
                l.Add(new Employee() { DepartmentID = t.DepartmentID, EmployeeName = t.EmployeeName, EmployeeEmail = t.EmployeeEmail });
            return l;
        }


        public List<String> DisburseDepartLabel(string deptid)
        {
            var ddptl = (from dg in ad.Departments
                         join emm in ad.Employees on dg.DepartmentID equals emm.DepartmentID
                         join emem in ad.CollectionPoints on dg.CollectionPointID equals emem.CollectionPointID
                         join ith in ad.Roles on emm.RoleID equals ith.RoleID
                         where dg.DepartmentID == deptid && ith.RoleName == "Department Representative"
                         select new {dg.DepartmentName , emm.EmployeeName ,emem.Place,emem.Time}).ToList();
            //List<String> lobj = new List<String>();
            if (ddptl.Count > 0)
            {
                List<String> lobj = new List<String> { ddptl[0].DepartmentName, ddptl[0].EmployeeName, ddptl[0].Place, ddptl[0].Time };
                return lobj;
            }
            else
                return null;
        }

        public List<Disbursementclass> DisburseDepartGrid(string deptid)
        {
            var ddptg = (from odr in ad.Requests
                         join odrd in ad.RequestDetails on odr.RequestID equals odrd.RequestID
                         join itm in ad.Items on odrd.RequestedItem equals itm.ItemID
                         where odr.RequestStatus == "Alloted" && odr.RequestByDepartmentID == deptid
                         select new { itm.Description, odrd.RequestedQty, odrd.ReceivedQty, itm.UnitOfMeasurement }).ToList();
            Disbursementclass hh;
            List<Disbursementclass> hhList = new List<Disbursementclass>();
            foreach (var i in ddptg)
            {
                int a = (int)i.RequestedQty - (int)i.ReceivedQty;
                hh = new Disbursementclass(i.Description, (int)i.RequestedQty, (int)i.ReceivedQty, a, i.UnitOfMeasurement);
                hhList.Add(hh);
            }
            return hhList;
        }

        public int updateDisburse(string deptid, string empid1)
        {
			int empid = Convert.ToInt32(empid1);
            var ddptg = (from odr in ad.Requests
                         where odr.RequestStatus == "Alloted" && odr.RequestByDepartmentID == deptid
                         select odr).ToList();
            foreach (var qin in ddptg)
            {
                var outstand = (from ot in ad.RequestDetails where ot.RequestID == qin.RequestID && ot.RequestedQty != ot.ReceivedQty select ot).ToList();
                if (outstand.Count > 0)
                {
                    Request reqout = new Request
                    {
                        RequestStatus = "NEW",
                        RequestByDepartmentID = qin.RequestByDepartmentID,
                        RequestByEmployeeID = empid
                    };

                    ad.Requests.Add(reqout);
                    try
                    {
                        ad.SaveChanges();
                    }
                    catch (Exception tye)
                    {
                        Console.WriteLine(tye);

                    }
                    var genReqNo = (from grn1 in ad.Requests where grn1.RequestStatus == "NEW" && grn1.RequestByEmployeeID == empid && grn1.RequestByDepartmentID == qin.RequestByDepartmentID select grn1).ToList();
                    int reqnoutstand = genReqNo[0].RequestID;
                    EmpNewRequest outstandnewreq = new EmpNewRequest();
                    foreach (var outstandvar in outstand)
                    {
                        int qtyoutstand = (int)(outstandvar.RequestedQty - outstandvar.ReceivedQty);
                        outstandnewreq.AddItem(reqnoutstand, outstandvar.RequestedItem, qtyoutstand);
                    }
                    outstandnewreq.AddOutStandingReqNo(reqnoutstand);
                }

                var all12 = from a12 in ad.Requests where a12.RequestID == qin.RequestID select a12;
                Request req = all12.First();
                req.RequestStatus = "Disbursed";
                req.ReceivedDate = System.DateTime.Now.Date;

                try
                {
                    ad.SaveChanges();
                }
                catch (Exception tye)
                {
                    Console.WriteLine(tye);
                    return 0;                   
                }
            }
            return 1;
        }
    }
}
