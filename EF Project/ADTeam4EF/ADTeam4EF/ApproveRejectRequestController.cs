using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
namespace ADTeam4EF
{
    public class ApproveRejectRequestController
    {
        ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();
        public List<Request> getRequestID(string departmentid)
        {
            try
            {
                var request = from req in ctx.Requests
                              join emp in ctx.Employees on req.RequestByEmployeeID equals emp.EmployeeID
                              where req.RequestByDepartmentID == departmentid && req.RequestStatus == "Pending"
                              select req;

                //var employee = from emp in ctx.Employees
                //               join req in ctx.Requests on emp.EmployeeID equals req.RequestByEmployeeID      
                //               where req.RequestStatus == "Pending" && emp.DepartmentID == departmentid
                //               select emp;
                if (request.Count() > 0)
                {
                    return request.ToList<Request>();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 public List<Employee> getEmployeeNamesforApproveRejectRequest(string departmentid)
        {
            try
            {
                var employee = from emp in ctx.Employees
                               join req in ctx.Requests on emp.EmployeeID equals req.RequestByEmployeeID
                               where req.RequestStatus == "Pending" && emp.DepartmentID == departmentid
                               select emp;
                return employee.ToList<Employee>();
            
            }
            catch
            {
                return null;
            }              
        }

        public List<Employee> getEmployeeNameList(String DepartmentID)
        {
            try
            {
                //var employee = from emp in ctx.Employees
                //               join req in ctx.Requests on emp.EmployeeID equals req.ApprovedByEmployeeID
                //               where req.RequestStatus != "APPROVED"    
                //               select emp;

                //var employee = from emp in ctx.Employees
                //               let ces = from ce in ctx.Requests
                //                        select ce.ApprovedByEmployeeID
                //               where ces.Contains(emp.EmployeeID)
                //               select emp;

                //var employee = from emp in ctx.Employees
                //               where !(from req in ctx.Requests
                //                    where req.ApprovedByEmployeeID != null)

                //                    .Contains(emp.EmployeeID)
                //                    select emp;

                var employee = from emp in ctx.Employees
                               where emp.DepartmentID == DepartmentID && 
                               ctx.Requests.All(r => r.ApprovedByEmployeeID != emp.EmployeeID)
                               select emp;
              
                return employee.ToList<Employee>();

            }
            catch
            {
                return null;
            }
        }
        public Employee getEmployeeInfo(string employeename)
        {
            try
            {
                var employee = from emp in ctx.Employees
                               where emp.EmployeeName == employeename
                               select emp;
                ADTeam4EF.Employee e = employee.First();
                return e;
            }
            catch
            {
                return null;
            }
        }
        public Request getRequestIDByEmployeeID(int employeeID)
        {
            try
            {
                var request = from req in ctx.Requests
                              where req.RequestByEmployeeID == employeeID
                              select req;
                ADTeam4EF.Request r = request.FirstOrDefault();
                if (r != null)
                {
                    return r;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //USING VIEW AND SELECTING ONLY PARTICULAR COLUMN OF THE VIEW 
        public List<ApproveRejectRequestObject> getRequestItems(int requestID)
        {
            try
            {
                var item = from items in ctx.ApproveRejectRequest_View
                           where items.RequestID == requestID
                           select new ApproveRejectRequestObject { Description = items.Description, Quantity = (int)items.RequestedQty, UnitOfMeasurement = items.UnitOfMeasurement };
                if (item.Count() > 0)
                {
                    return item.ToList<ApproveRejectRequestObject>();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
		public String getRequestIDByEmployeeID(string employeeID)
        {
            try
            {
                int eID = 0;
                eID = Convert.ToInt16(employeeID);
                var request = from req in ctx.Requests
                              where req.RequestByEmployeeID == eID
                              select req;

                ADTeam4EF.Request r = request.First();

                return r.RequestID.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String updateRequestStatusToApprove(int RequestID, string Status, int ApprovedByEmployeeID, string Remark)
        {
            try
            {
                Request r = (from req in ctx.Requests
                             where req.RequestID == RequestID
                             select req).First();
                r.RequestStatus = Status;
                r.ApprovedByEmployeeID = ApprovedByEmployeeID;
                r.ApproveDate = DateTime.Now;
                r.Remark = Remark;
                ctx.SaveChanges();
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        //USING VIEW AND SELECTING ONLY PARTICULAR COLUMN OF THE VIEW 
        public List<ApproveRejectRequestObject> getRequestItem(int requestID)
        {
            try
            {
                var item = from items in ctx.ApproveRejectRequest_View
                           where items.RequestID == requestID
                           select new ApproveRejectRequestObject { Description = items.Description, Quantity = (int)items.RequestedQty, UnitOfMeasurement = items.UnitOfMeasurement };
                return item.ToList<ApproveRejectRequestObject>();
            }
            catch
            {
                return null;
            }
        }

        public List<ApproveRejectRequestObject> getRequestItems(string requestID)
        {
            try
            {
                int rID = 0;
                rID = Convert.ToInt16(requestID);
                var item = from items in ctx.ApproveRejectRequest_View
                           where items.RequestID == rID
                           select new ApproveRejectRequestObject { Description = items.Description, Quantity = (int)items.RequestedQty, UnitOfMeasurement = items.UnitOfMeasurement };
                return item.ToList<ApproveRejectRequestObject>();
            }
            catch
            {
                return null;
            }
        }

        public bool approveRequest(int requestID)
        {
            try
            {
                var request = from req in ctx.Requests
                              where req.RequestID == requestID
                              select req;
                ADTeam4EF.Request r = request.First();
                using (TransactionScope ts = new TransactionScope())
                {
                    r.RequestStatus = "Approved";
                    ctx.SaveChanges();
                    ts.Complete();
                    return true;
                }   
            }
            catch
            {
                return false;
            }
        }
       
    }
}
