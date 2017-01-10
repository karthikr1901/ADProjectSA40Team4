using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTeam4EF
{
    public class ReorderReportController

    {
        ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();

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
        public List<ReorderReport_View> getFirstMonth(string categoryName, DateTime ff, DateTime fl)
        {
            try
            {

                var firstMom = (from f in ctx.ReorderReport_View
                                where f.OrderDate >= ff && f.OrderDate <= fl && f.CategoryName == categoryName
                                select f).ToList();
                List<ReorderReport_View> firstList = firstMom;
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
        public List<ReorderReport_View> getSecondMonth(string categoryName, DateTime sf, DateTime sl)
        {
            try
            {
                var second = from f in ctx.ReorderReport_View
                             where f.OrderDate >= sf && f.OrderDate <= sl && f.CategoryName == categoryName
                             select f;
                List<ReorderReport_View> secondList = second.ToList();
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
        public List<ReorderReport_View> getThirdMonth(string categoryName, DateTime tf, DateTime tl)
        {
            try
            {
                var third = from f in ctx.ReorderReport_View
                            where f.OrderDate >= tf && f.OrderDate <= tl && f.CategoryName == categoryName
                            select f;
                List<ReorderReport_View> thirdList = third.ToList();
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
