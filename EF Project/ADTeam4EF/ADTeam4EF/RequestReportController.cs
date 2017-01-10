using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADTeam4EF;
namespace ADTeam4EF
{
    public class RequestReportController
    {
        ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();
        public List<Department> getAllDepartment()
        {
            try
            {
                var department = from depart in ctx.Departments
                                select depart;
                List<Department> departList = department.ToList();
                return departList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Category> getAllCategory()
        {
            try
            {
                var category = from cat in ctx.Categories
                                 select cat;
                List<Category> catList = category.ToList();
                return catList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        public List<RequestReport_View> getFirstMonth(string categoryName, DateTime ff, DateTime fl)
        {
            try
            {

                var firstMom = (from f in ctx.RequestReport_View
                            where f.RequestDate >= ff && f.RequestDate <= fl && f.CategoryName == categoryName
                            select f).ToList();
                List<RequestReport_View> firstList = firstMom;
                if (firstList.Count() > 0)
                {
                    return firstList;
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
        public List<RequestReport_View> getSecondMonth(string categoryName, DateTime sf, DateTime sl)
        {
            try
            {
                var second = from f in ctx.RequestReport_View
                             where f.RequestDate >= sf && f.RequestDate <= sl && f.CategoryName == categoryName
                             select f;
                List<RequestReport_View> secondList = second.ToList();
                if (secondList.Count() > 0)
                {
                    return secondList;
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
        public List<RequestReport_View> getThirdMonth(string categoryName, DateTime tf, DateTime tl)
        {
            try
            {
                var third = from f in ctx.RequestReport_View
                            where f.RequestDate >= tf && f.RequestDate <= tl && f.CategoryName == categoryName
                            select f;
                List<RequestReport_View> thirdList = third.ToList();
                if (thirdList.Count() > 0)
                {
                    return thirdList;
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
        public List<RequestReport_View> getFirstMonthOneDepartmentAllCategory(string department, DateTime ff, DateTime fl)
        {
            try
            {

                var firstMom = (from f in ctx.RequestReport_View
                                where f.RequestDate >= ff && f.RequestDate <= fl && f.DepartmentName == department
                                select f).ToList();
                List<RequestReport_View> firstList = firstMom;
                if (firstList.Count() > 0)
                {
                    return firstList;
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
        public List<RequestReport_View> getSecondMonthOneDepartmentAllCategory(string department, DateTime sf, DateTime sl)
        {
            try
            {
                var second = from f in ctx.RequestReport_View
                             where f.RequestDate >= sf && f.RequestDate <= sl && f.DepartmentName == department
                             select f;
                List<RequestReport_View> secondList = second.ToList();
                if (secondList.Count() > 0)
                {
                    return secondList;
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
        public List<RequestReport_View> getThirdMonthOneDepartmentAllCategory(string department, DateTime tf, DateTime tl)
        {
            try
            {
                var third = from f in ctx.RequestReport_View
                            where f.RequestDate >= tf && f.RequestDate <= tl && f.DepartmentName == department
                            select f;
                List<RequestReport_View> thirdList = third.ToList();
                if (thirdList.Count() > 0)
                {
                    return thirdList;
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
        public List<RequestReport_View> getFirstMonthOneDepartmentOneCategory(string department,string categoryName, DateTime ff, DateTime fl)
        {
            try
            {

                var firstMom = (from f in ctx.RequestReport_View
                                where f.RequestDate >= ff && f.RequestDate <= fl && f.CategoryName == categoryName && f.DepartmentName == department
                                select f).ToList();
                List<RequestReport_View> firstList = firstMom;
                if (firstList.Count() > 0)
                {
                    return firstList;
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
        public List<RequestReport_View> getSecondMonthOneDepartmentOneCategory(string department,string categoryName, DateTime sf, DateTime sl)
        {
            try
            {
                var second = from f in ctx.RequestReport_View
                             where f.RequestDate >= sf && f.RequestDate <= sl && f.CategoryName == categoryName && f.DepartmentName == department
                             select f;
                List<RequestReport_View> secondList = second.ToList();
                if (secondList.Count() > 0)
                {
                    return secondList;
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
        public List<RequestReport_View> getThirdMonthOneDepartmentOneCategory(string department,string categoryName, DateTime tf, DateTime tl)
        {
            try
            {
                var third = from f in ctx.RequestReport_View
                            where f.RequestDate >= tf && f.RequestDate <= tl && f.CategoryName == categoryName && f.DepartmentName == department
                            select f;
                List<RequestReport_View> thirdList = third.ToList();
                if (thirdList.Count() > 0)
                {
                    return thirdList;
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
    }
}
