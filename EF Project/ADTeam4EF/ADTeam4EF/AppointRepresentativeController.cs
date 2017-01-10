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
    public class AppointRepresentativeController
    {
        protected string msg = string.Empty;
        ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();
        public Employee getRepresentative(string departmentid)
        {
            try
            {
                var employee = from e in ctx.Employees
                               join r in ctx.Roles on e.RoleID equals r.RoleID
                               where e.DepartmentID == departmentid && r.RoleID == 2
                               select e;
                ADTeam4EF.Employee emp = employee.First();
                return emp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Employee> getEmployeeList(string departmentid)
        {
            try
            {
                var employee = from e in ctx.Employees
                               join r in ctx.Roles on e.RoleID equals r.RoleID
                               where e.DepartmentID == departmentid && r.RoleID != 1 && r.RoleID != 2
                               select e;
                return employee.ToList<Employee>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
		public Employee getRepresentativeMobile(string departmentid)
        {
            try
            {
                var employee = from e in ctx.Employees
                               //join r in ctx.Roles on e.RoleID equals r.RoleID
                               where e.DepartmentID == departmentid && e.RoleID == 2
                               select e;

                ADTeam4EF.Employee emp = employee.FirstOrDefault();
                return emp;
            }
            catch
            {
                return null;
            }
        }
		
        public bool appointNewRepresentative(string employeeName, string departmentid)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var employee = from e in ctx.Employees
                                   join r in ctx.Roles on e.RoleID equals r.RoleID
                                   where e.EmployeeName == employeeName
                                   select e;
                    ADTeam4EF.Employee emp = employee.First();
                    ADTeam4EF.Employee previousEmp = getRepresentative(departmentid);
                    previousEmp.RoleID = 4;
                    emp.RoleID = 2;
                    sendmail(departmentid, previousEmp.EmployeeEmail, emp.EmployeeEmail);
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

        public String appointNewRepresentativeMobile(string employeeID, string departmentid)
        {
            try
            {
                int eID = Convert.ToInt16(employeeID);
                using (TransactionScope ts = new TransactionScope())
                {
                    var employee = from e in ctx.Employees
                                   //join r in ctx.Roles on e.RoleID equals r.RoleID
                                   where e.EmployeeID == eID
                                   select e;

                    ADTeam4EF.Employee emp = employee.First();

                    emp.RoleID = 2;
                    //ctx.SaveChanges();
                    ADTeam4EF.Employee previousEmp = getRepresentative(departmentid);
                    previousEmp.RoleID = 4;
                    ctx.SaveChanges();
                    ts.Complete();
                    return "SUCCESS";
                }
            }
            catch
            {
                return "FAIL";
            }
        }

        public void sendmail(string departmentid, string p_email, string n_email)
        {
            try
            {
                string to1 = p_email;
                string to2 = n_email;
                string body1 = string.Empty;
                string sub1 = string.Empty;
                sub1 = "You have been appointed as a department representative";
                body1 = "You have been appointed as a department representative starting from: " + System.DateTime.Today;
                string sub2 = "You have been canceled as a temporary department representative";
                string body2 = "You have been canceled as a temporary department representative starting from today: " + System.DateTime.Today;
                NetworkCredential loginInfo = new NetworkCredential("smsmarartt@gmail.com", "ttraramsms1");
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("smsmarartt@gmail.com");
                msg.To.Add(new MailAddress(to1));
                msg.Subject = sub1;
                msg.Body = body1;
                msg.IsBodyHtml = true;

                MailMessage msg2 = new MailMessage();
                msg2.From = new MailAddress("smsmarartt@gmail.com");
                msg2.To.Add(new MailAddress(to2));
                msg2.Subject = sub2;
                msg2.Body = body2;
                msg2.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                client.Host = "lynx.iss.nus.edu.sg";
                client.Port = 25;
                client.EnableSsl = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = loginInfo;
                client.Send(msg);
                client.Send(msg2);

            }
            catch (Exception ex)
            {

                Console.Write("Exception in sendEmail:" + ex.Message);
            }


        }
        
    }
}
