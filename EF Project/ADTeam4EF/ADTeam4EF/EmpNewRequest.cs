using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ADTeam4EF
{
    public class EmpNewRequest
    {
        ADProjectSA40Team4Entities ad = new ADProjectSA40Team4Entities();

        public List<EmpNewReqClass> GridMobViewEmpNewReq(int reqno)
        {
            EmpNewReqClass enrcl;// = new EmpNewReqClass();
            List<EmpNewReqClass> enrc = new List<EmpNewReqClass>();
            var newarc = (from g1 in ad.RequestDetails join g2 in ad.Items on g1.RequestedItem equals g2.ItemID where g1.RequestID == reqno select new { g2.Description, g1.RequestedQty, g1.RequestedItem, g2.UnitOfMeasurement }).ToList();
            foreach(var t in newarc)
                enrc.Add(enrcl = new EmpNewReqClass((string)t.Description, (int)t.RequestedQty, (string)t.RequestedItem, (string)t.UnitOfMeasurement));
            return enrc;            
        }

        public dynamic GridViewEmpNewReq(int reqno)
        {
            var newarc = (from g1 in ad.RequestDetails join g2 in ad.Items on g1.RequestedItem equals g2.ItemID where g1.RequestID == reqno select new { g2.Description, g1.RequestedQty, g1.RequestedItem, g2.UnitOfMeasurement }).ToList();
            return newarc;
        }

        public dynamic GetReqNo(string dptid1, int eid1)
        {
            var genReqNo = (from grn1 in ad.Requests where grn1.RequestStatus == "NEW" && grn1.RequestByEmployeeID == eid1 select grn1).ToList();
            if (genReqNo.Count > 0)
            {
                return genReqNo;
            }
            else
            {
                Request req = new Request
                {
                    RequestStatus = "NEW",
                    RequestByDepartmentID = dptid1,
                    RequestByEmployeeID = eid1
                };

                ad.Requests.Add(req);

                try
                {
                    ad.SaveChanges();
                }
                catch (Exception tye)
                {
                    Console.WriteLine(tye);

                }

                var genReqNo1 = (from grn1 in ad.Requests where grn1.RequestStatus == "NEW" && grn1.RequestByEmployeeID == eid1 select grn1).ToList();
                return genReqNo1;
            }
        }

        public void ReqNo(int eid1)
        {
            var genReqNo = (from grn1 in ad.Requests where grn1.RequestStatus == "NEW" && grn1.RequestByEmployeeID == eid1 select grn1).ToList();
            if (genReqNo.Count > 0)
            {
                int reqno = genReqNo[0].RequestID;
                using (var ctxt = new ADProjectSA40Team4Entities())
                {
                lk:
                    var x = (from y in ctxt.RequestDetails
                             where y.RequestID == reqno
                             select y).FirstOrDefault();
                    if (x != null)
                    {
                        ctxt.RequestDetails.Remove(x);
                        ctxt.SaveChanges();
                        goto lk;
                    }
                    var x1 = (from y1 in ctxt.Requests
                             where y1.RequestID == reqno
                             select y1).FirstOrDefault();
                    ctxt.Requests.Remove(x1);
                    ctxt.SaveChanges();
                }
            }
        }

        public dynamic DropCategory()
        {            
                var dropcat = (from dc1 in ad.Categories select new {dc1.CategoryName,dc1.CategoryID}).ToList();
                return dropcat;            
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

        public void AddItem(int req, string itm, int qty)
        {
            var checkitm = (from citm in ad.RequestDetails
                            where citm.RequestID == req && citm.RequestedItem == itm
                            select citm).ToList();
            if (checkitm.Count > 0) 
            {
                checkitm[0].RequestedQty = qty;
                checkitm[0].ReceivedQty = 0;
                ad.SaveChanges();
            }
            else
            {
                RequestDetail reqd = new RequestDetail
                {
                    RequestID = req,
                    RequestedItem = itm,
                    RequestedQty = qty,
                    ReceivedQty = 0
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

        public void AddReqNo(int reqtid)
        {
            Request addreq = (from c in ad.Requests where c.RequestID == reqtid select c).SingleOrDefault(); //ad.Requests.Single(rid => rid.RequestID == reqtid);
            addreq.RequestStatus = "Pending";
            addreq.RequestDate = System.DateTime.Now;
            var f = (from d in ad.Requests where d.RequestID == reqtid select d).ToList();
            try
            {
                ad.SaveChanges();
                sendmail(reqtid,(string)f[0].RequestByDepartmentID,(int)f[0].RequestByEmployeeID);
            }
            catch (Exception tye)
            {
                Console.WriteLine(tye);

            }
        }

        public void AddOutStandingReqNo(int reqtid)
        {
            Request addreq = (from c in ad.Requests where c.RequestID == reqtid select c).SingleOrDefault(); //ad.Requests.Single(rid => rid.RequestID == reqtid);
            addreq.RequestStatus = "Outstanding";
            addreq.ApproveDate = addreq.RequestDate = System.DateTime.Now.Date;
            
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

        public string DeleteItem(string IID, int RTID)
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
                    return "true";
                }
                else
                    return "false";
            }
        }

        public void sendmail(int reqtid, string departid, int empid)
        {
            try
            {
                //string mail = "smsmarartt@gmail.com";
                var addreq1 = (from c in ad.Requests where c.RequestID == reqtid select c).SingleOrDefault();
                var rdadd1 = (from c1 in ad.RequestDetails join s1 in ad.Items on c1.RequestedItem equals s1.ItemID where c1.RequestID == reqtid select new { c1.RequestedItem, s1.Description, c1.RequestedQty }).ToList();
                var empid1 = (from c2 in ad.Employees where c2.DepartmentID == departid && c2.RoleID == 1 select c2).ToList();
                var name1 = (from c3 in ad.Employees where c3.EmployeeID == empid select c3).ToList();
                string to = empid1[0].EmployeeEmail;
                string sub = "You have request from " + name1[0].EmployeeName + " (Employee ID: " + empid + ")";
                string body = "Order request number :" + reqtid + "<br/> Requested on :" + addreq1.RequestDate + "<br/>";
                int ccc = 0;
                string body1 = body + @"<table cellpadding=""10"" border=""1""><tbody><th>ItemID</th><th>Description</th><th>Requested Quantity</th>";
                cplus:
                if (ccc < rdadd1.Count())
                {
                    body1 += "<tr><td>"+rdadd1[ccc].RequestedItem + "</td><td>" + rdadd1[ccc].Description + "</td><td>" + rdadd1[ccc].RequestedQty + "</td></tr>";
                    ccc++;
                    goto cplus;
                }
                body1 += "</tbody></table>";



                NetworkCredential loginInfo = new NetworkCredential("smsmarartt@gmail.com","ttraramsms1");
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("smsmarartt@gmail.com");
                msg.To.Add(new MailAddress(to));
                msg.Subject = sub;
                msg.Body = body1;
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                client.Host = "lynx.iss.nus.edu.sg";
                client.Port = 25;
                client.EnableSsl = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = loginInfo;
                client.Send(msg);
            }
            catch (Exception ex)
            {
       
                Console.Write("Exception in sendEmail:" + ex.Message);
            }
            

        }


        public void MobileGenReqNo(int empid)  //call this no 1 pass employee id
        {
            ReqNo(empid);
            //string genReqNo = Convert.ToString(MobileGetReqNo(empid));
            //return genReqNo;
        }

        public string MobileGetReqNo(string empid1)
        {
            int empid = Convert.ToInt32(empid1);
            var getcheck = (from grn1 in ad.Requests where grn1.RequestStatus == "NEW" && grn1.RequestByEmployeeID == empid select grn1.RequestID).ToList();
            if (getcheck.Count > 0)
            {
                string ReqNo = Convert.ToString(getcheck.Single());
                return ReqNo;
            }
            else
            {
                string deptid = (from deptemp in ad.Employees where deptemp.EmployeeID == empid select deptemp.DepartmentID).Single();
                Request req = new Request
                {
                    RequestStatus = "NEW",
                    RequestByDepartmentID = deptid,
                    RequestByEmployeeID = empid
                };

                ad.Requests.Add(req);

                try
                {
                    ad.SaveChanges();
                }
                catch (Exception tye)
                {
                    Console.WriteLine(tye);

                }

                int genReqNo = (from grn1 in ad.Requests where grn1.RequestStatus == "NEW" && grn1.RequestByEmployeeID == empid select grn1.RequestID).Single();
                string genReqNo1 = Convert.ToString(genReqNo);
                return genReqNo1;
            }
        }

        public Item MobileGetDetails(string itemid)    //call this no 2 pass item id
        {
            Item a = new Item();
            a = (from getdet in ad.Items where getdet.ItemID == itemid select getdet).FirstOrDefault();
            return a;
        }

        public List<EmpNewReqClass> MobileAddItem(string req1, string itm, int qty)  //call this no 3 pass request no, item description, quantity
        {
            int req = Convert.ToInt32(req1);
            //string itm = (from itmiddes in ad.Items where itmiddes.Description == itmdes select itmiddes.ItemID).Single();
            var checkitm = (from citm in ad.RequestDetails
                            where citm.RequestID == req && citm.RequestedItem == itm
                            select citm).ToList();
            if (checkitm.Count > 0)
            {
                checkitm[0].RequestedQty = qty;
                checkitm[0].ReceivedQty = 0;
                ad.SaveChanges();
            }
            else
            {
                RequestDetail reqd = new RequestDetail
                {
                    RequestID = req,
                    RequestedItem = itm,
                    RequestedQty = qty,
                    ReceivedQty =0
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
            List<EmpNewReqClass> dd = new List<EmpNewReqClass>();
            dd = GridMobViewEmpNewReq(req);
            return dd;
        }

        public string MobileSaveReqNo(string reqt)   //call this no 4 pass request no
        {
            int reqtid = Convert.ToInt32(reqt);
            Request addreq = (from c in ad.Requests where c.RequestID == reqtid select c).SingleOrDefault(); //ad.Requests.Single(rid => rid.RequestID == reqtid);
            addreq.RequestStatus = "Pending";
            addreq.RequestDate = System.DateTime.Now.Date;
            var f = (from d in ad.Requests where d.RequestID == reqtid select d).ToList();

            try
            {
                ad.SaveChanges();
                sendmail(reqtid, (string)f[0].RequestByDepartmentID, (int)f[0].RequestByEmployeeID);
                return "SUCCESS";
            }
            catch (Exception tye)
            {
                Console.WriteLine(tye);
                return "FAIL";
            }
        }
    }
}
