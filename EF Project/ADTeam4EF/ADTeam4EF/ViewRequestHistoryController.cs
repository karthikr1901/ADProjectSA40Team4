using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
namespace ADTeam4EF
{
    public class ViewRequestHistoryController
    {
        ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();

        public List<DISTINCTEMPLOYEE_VIEW> getEmp(string departmentid)
        {
            try
            {
                var emp = from e in ctx.DISTINCTEMPLOYEE_VIEW
                          select e;
                List<DISTINCTEMPLOYEE_VIEW> empList = emp.ToList();
                if (empList.Count() > 0)
                {
                    return empList;
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
        public List<Employee> getRequestedEmployee(string departmentid)
        {
            try
            {
                var emp = from employee in ctx.Employees
                          join req in ctx.Requests on employee.EmployeeID equals req.RequestByEmployeeID
                          where req.RequestByDepartmentID == departmentid
                          select employee;
                List<Employee> employeeList = emp.ToList();
                if (employeeList.Count() > 0)
                {
                    return employeeList;
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
     
        public List<Request> populateRequestNo(string employeeName, DateTime fromDate, DateTime toDate)
        {
            try
            {
                List<Request> requestList = new List<Request>();
                var employee = from emp in ctx.Employees
                               where emp.EmployeeName == employeeName
                               select emp;
                ADTeam4EF.Employee e = employee.First();
                var reqno = from req in ctx.Requests
                            where req.RequestDate >= fromDate && req.RequestDate <= toDate && req.RequestByEmployeeID == e.EmployeeID
                            select req;
                List<Request> reqList = reqno.ToList<Request>();
                if (reqList.Count > 0)
                {
                    return reqList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<RequestHistoryObj> getRequestHistory(int requestno)
        {
            try
            {
                var request = from req in ctx.RequestHistory_View
                              where req.RequestID == requestno
                              select new RequestHistoryObj { Description = req.Description, RequestedQty = (int)req.RequestedQty, ReceivedQty=(int)req.ReceivedQty, Unitofmeasurement = req.UnitOfMeasurement };
                List<ADTeam4EF.RequestHistoryObj> obj = request.ToList<RequestHistoryObj>();
                if (obj.Count() > 0)
                {
                    return obj;
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
        public Request getRequestStatus(int requestno)
        {
            try
            {
                var req = from r in ctx.Requests
                          where r.RequestID == requestno
                          select r;
                Request request = req.FirstOrDefault();
                return request;
            }
            catch (Exception ex)
            {
                throw ex;
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
                               select new
                               {
                                   i.Description,
                                   ad.Quantity,
                                   i.UnitOfMeasurement,
                                   si.Price,
                                   Amount = (ad.Quantity * si.Price),
                                   adjust.TotalPrice,
                                   ad.AdjustmentRemark
                               }).ToList();

                return adjList;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                throw e;
            }

        }

    }
}
