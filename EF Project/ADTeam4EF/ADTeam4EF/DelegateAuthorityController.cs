using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ADTeam4EF
{
    public class DelegateAuthorityController
    {
        ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();
        public List<Employee> populateEmployeeList(string departmentid)
        {
            try
            {
                List<Employee> employeeList = new List<Employee>();
                employeeList = (from emp in ctx.Employees
                                join r in ctx.Roles on emp.RoleID equals r.RoleID
                                where r.RoleID == 4 && emp.DepartmentID == departmentid
                                select emp).ToList<Employee>();
                return employeeList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public CancellationOfAuthorityObject GetDelegatedInfo(string departmentid)
        {
            try
            {
                var d = (from del in ctx.DelicatedInfoes
                        join e in ctx.Employees on del.EmployeeID equals e.EmployeeID
                        where e.DepartmentID == departmentid && e.RoleID == 3 && (DateTime.Today >= del.fromDate || DateTime.Today <= del.toDate)
                         select new { del.DelicatedInfoID, del.EmployeeID }).FirstOrDefault();


                var employee = (from emp in ctx.Employees
                               where emp.EmployeeID == d.EmployeeID
                               select emp).Single();


                ADTeam4EF.CancellationOfAuthorityObject ca = new ADTeam4EF.CancellationOfAuthorityObject();
                ca.DelicatedInfoID = d.DelicatedInfoID;
                ca.EmployeeName = employee.EmployeeName;

                return ca;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public String DeleteDelecatedInfo(string DelicatedInfoID)
        {
            try
            {
                var d = ctx.DelicatedInfoes.Find(int.Parse(DelicatedInfoID));

                var e = (from emp in ctx.Employees
                         where emp.EmployeeID == d.EmployeeID
                         select emp).Single();

                e.RoleID = 3;
                  
                var delete = ctx.DelicatedInfoes.Remove(d);

                ctx.SaveChanges();

                return "SUCCESS";
            }
            catch (Exception e)
            {
                return "FAIL";
            }
        }

        //public bool delegateAuthority(string employeeName, DateTime fromDate, DateTime toDate)
        //{
        //    try
        //    {
        //        var employee = from emp in ctx.Employees
        //                       join r in ctx.Roles on emp.RoleID equals r.RoleID
        //                       where emp.EmployeeName == employeeName
        //                       select emp;
        //        ADTeam4EF.Employee e = employee.First();
        //        using (TransactionScope ts = new TransactionScope())
        //        {
        //            e.RoleID = 3;
        //            ctx.SaveChanges();
        //            ADTeam4EF.DelicatedInfo delicateInfo = new ADTeam4EF.DelicatedInfo();
        //            delicateInfo.EmployeeID = e.EmployeeID;
        //            delicateInfo.fromDate = fromDate;
        //            delicateInfo.toDate = toDate;
        //            ctx.DelicatedInfoes.Add(delicateInfo);
        //            ctx.SaveChanges();
        //            ts.Complete();
        //            return true;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public bool delegateAuthority(string employeeName, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var employee = from emp in ctx.Employees
                               join r in ctx.Roles on emp.RoleID equals r.RoleID
                               where emp.EmployeeName == employeeName
                               select emp;
                ADTeam4EF.Employee e = employee.First();
                using (TransactionScope ts = new TransactionScope())
                {
                    e.RoleID = 3;
                    ctx.SaveChanges();
                    ADTeam4EF.DelicatedInfo delicateInfo = new ADTeam4EF.DelicatedInfo();
                    delicateInfo.EmployeeID = e.EmployeeID;
                    delicateInfo.fromDate = fromDate;
                    delicateInfo.toDate = toDate;
                    ctx.DelicatedInfoes.Add(delicateInfo);
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

        public bool delegateAuthorityMobile(int employeeID, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var employee = from emp in ctx.Employees
                               join r in ctx.Roles on emp.RoleID equals r.RoleID
                               where emp.EmployeeID == employeeID
                               select emp;
                ADTeam4EF.Employee e = employee.First();
                using (TransactionScope ts = new TransactionScope())
                {
                    e.RoleID = 3;
                    ctx.SaveChanges();
                    ADTeam4EF.DelicatedInfo delicateInfo = new ADTeam4EF.DelicatedInfo();
                    delicateInfo.EmployeeID = employeeID;
                    delicateInfo.fromDate = fromDate;
                    delicateInfo.toDate = toDate;
                    ctx.DelicatedInfoes.Add(delicateInfo);
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

        //public bool delegateAuthority(string employeeName, string fromDate, string toDate)
        //{
        //    try
        //    {
        //        var employee = from emp in ctx.Employees
        //                       join r in ctx.Roles on emp.RoleID equals r.RoleID
        //                       where emp.EmployeeName == employeeName
        //                       select emp;
        //        ADTeam4EF.Employee e = employee.First();
        //        using (TransactionScope ts = new TransactionScope())
        //        {
        //            e.RoleID = 3;
        //            ctx.SaveChanges();
        //            ADTeam4EF.DelicatedInfo delicateInfo = new ADTeam4EF.DelicatedInfo();
        //            delicateInfo.EmployeeID = e.EmployeeID;
        //            delicateInfo.fromDate = Convert.ToDateTime(fromDate.Replace("-","/"));
        //            delicateInfo.toDate = Convert.ToDateTime(toDate.Replace("-","/"));
        //            ctx.DelicatedInfoes.Add(delicateInfo);
        //            ctx.SaveChanges();
        //            ts.Complete();
        //            return true;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        return false;
        //    }
        //}
        public Employee populateAuthority(string departmentid)
        {
            try
            {
                var emp = from employee in ctx.Employees
                          join rol in ctx.Roles on employee.RoleID equals rol.RoleID
                          where rol.RoleID == 3
                          select employee;
                ADTeam4EF.Employee empl = emp.FirstOrDefault();
                if (empl != null)
                {
                    return empl;
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
        public bool cancelAuthority()
        {
            try
            {
                var emp = from employee in ctx.Employees
                          join rol in ctx.Roles on employee.RoleID equals rol.RoleID
                          where rol.RoleID == 3
                          select employee;
                ADTeam4EF.Employee e = emp.First();
                var delegateInfo = from del in ctx.DelicatedInfoes
                                   where del.EmployeeID == e.EmployeeID
                                   select del;
                ADTeam4EF.DelicatedInfo d = delegateInfo.First();
                using (TransactionScope ts = new TransactionScope())
                {
                    e.RoleID = 4;
                    //ctx.SaveChanges();
                    ctx.DelicatedInfoes.Remove(d);
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
        public void sendmail(string departmentid, string type)
        {
            try
            {
                var emp = from e in ctx.Employees
                          where e.DepartmentID == departmentid && e.RoleID == 3
                          select e;
                Employee employee = emp.FirstOrDefault();
                //string mail = "smsmarartt@gmail.com";
                var delicatedInfo = (from c in ctx.DelicatedInfoes join d in ctx.Employees on c.EmployeeID equals d.EmployeeID 
                                     where c.EmployeeID ==  employee.EmployeeID select c).SingleOrDefault();
                
                string to = employee.EmployeeEmail;
                string body = string.Empty;
                string sub = string.Empty;
                if (type == "delegate")
                {
                     sub = "You have been deletegated as a temporary department head";
                     body = "From: " + delicatedInfo.fromDate + "<br/> To: " + delicatedInfo.toDate;
                }
                else if (type == "cancel")
                {
                     sub = "You have been canceled as a temporary department head";
                     body = "You have been canceled as a temporary department head starting from today: " + System.DateTime.Today;
                }
               
              
                NetworkCredential loginInfo = new NetworkCredential("smsmarartt@gmail.com", "ttraramsms1");
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("smsmarartt@gmail.com");
                msg.To.Add(new MailAddress(to));
                msg.Subject = sub;
                msg.Body = body;
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
    }
}
