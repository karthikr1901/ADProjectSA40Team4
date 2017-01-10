using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTeam4EF
{
    public class ProductRequisitionController
    {
        ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();
        public bool updateOutstandingRequisition()
        {
            try
            {
                int oldRequestID = 0;
                int newRequestID = 0;
            L: var requestDetails = from reqDetails in ctx.RequestDetails
                                    join request in ctx.Requests on reqDetails.RequestID equals request.RequestID
                                    where request.RequestStatus == "Approved" && reqDetails.ReceivedQty != reqDetails.RequestedQty
                                    select reqDetails;
                if (requestDetails != null)
                {
                    RequestDetail oldRequestDetail = requestDetails.First();
                    var req = from request in ctx.Requests
                              where request.RequestID == oldRequestDetail.RequestID
                              select request;
                    Request oldRequest = req.First();
                    if (oldRequestDetail.RequestID != oldRequestID)
                    {
                        if (newRequestID != 0)
                        {
                            var u = from x in ctx.Requests
                                    where x.RequestID == newRequestID
                                    select x;
                            Request update = u.First();
                            update.RequestStatus = "Outstanding";
                            ctx.SaveChanges();
                        }
                        ADTeam4EF.Request newRequest = new ADTeam4EF.Request();
                        newRequest.RequestStatus = "Outstanding Edit";
                        newRequest.RequestByEmployeeID = oldRequest.RequestByEmployeeID;
                        newRequest.RequestByDepartmentID = oldRequest.RequestByDepartmentID;
                        newRequest.ApprovedByEmployeeID = oldRequest.ApprovedByEmployeeID;
                        newRequest.RequestDate = System.DateTime.Today;
                        oldRequestID = (int)oldRequestDetail.RequestID;
                        ctx.Requests.Add(newRequest);
                        ctx.SaveChanges();
                        var nR = from n in ctx.Requests
                                 where n.RequestStatus == "Outstanding Edit" && n.RequestByEmployeeID == oldRequest.RequestByEmployeeID
                                 select n;
                        newRequestID = nR.First().RequestID;
                    }
                    ADTeam4EF.RequestDetail newRequestDetails = new ADTeam4EF.RequestDetail();
                    newRequestDetails.RequestID = newRequestID;
                    newRequestDetails.RequestedItem = oldRequestDetail.RequestedItem;
                    newRequestDetails.RequestedQty = oldRequestDetail.RequestedQty - oldRequestDetail.ReceivedQty;
                    ctx.RequestDetails.Add(newRequestDetails);
                    ctx.SaveChanges();
                    goto L;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
