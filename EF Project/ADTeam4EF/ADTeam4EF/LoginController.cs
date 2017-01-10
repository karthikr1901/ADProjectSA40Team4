using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Transactions;
namespace ADTeam4EF
{
    public class LoginController
    {
        protected string errorMsg = string.Empty;
        ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();

        public Employee authenticateUser(string email, string hashedPassword)
        {
            try
            {
                var q = from emp in ctx.Employees
                        join r in ctx.Roles on emp.RoleID equals r.RoleID
                        where emp.EmployeeEmail == email && emp.EmployeePassword == hashedPassword
                        select emp;
                ADTeam4EF.Employee employee = q.FirstOrDefault();
                if (employee != null)
                {
                    return employee;
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
		
		 public Employee GetEmployeeIDByEmployeeName(string EmployeeName)
        {
            try
            {
                var q = from emp in ctx.Employees
                        where emp.EmployeeName == EmployeeName
                        select emp;

                ADTeam4EF.Employee employee = q.First();

                return employee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
        public DelicatedInfo checkDelegate(int employeeid, DateTime today)
        {
            try
            {
                var dele = from del in ctx.DelicatedInfoes
                           join emp in ctx.Employees on del.EmployeeID equals emp.EmployeeID
                           where del.EmployeeID == employeeid
                           where del.fromDate <= today && del.toDate >= today
                           select del;
                ADTeam4EF.DelicatedInfo delicatedInfo = dele.FirstOrDefault();
                if (delicatedInfo != null)
                {
                    return delicatedInfo;
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

        public string GetSHA1HashData(string data)
        {
            //create new instance of md5
            SHA1 sha1 = SHA1.Create();

            //convert the input text to array of bytes
            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));

            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }
            // return hexadecimal string
            return returnValue.ToString();
        }
        public void updateExpiredDelegatedEmployee()
        {
            try
            {
                var emp = from e in ctx.Employees
                          join r in ctx.Roles on e.RoleID equals r.RoleID
                          where e.RoleID == 3
                          select e;
                ADTeam4EF.Employee employee = emp.FirstOrDefault();
                if (employee != null)
                {
                    var de = from del in ctx.DelicatedInfoes
                             join e in ctx.Employees on del.EmployeeID equals e.EmployeeID
                             where e.EmployeeID == employee.EmployeeID
                             select del;
                    ADTeam4EF.DelicatedInfo d = de.FirstOrDefault();
                    if (d != null)
                    {
                        if (System.DateTime.Today > d.toDate)
                        {
                            using (TransactionScope ts = new TransactionScope()){
                                employee.RoleID = 4;
                                ctx.DelicatedInfoes.Remove(d);
                                ctx.SaveChanges();
                                ts.Complete();
                            }
                        }               
                    }
                }
   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
