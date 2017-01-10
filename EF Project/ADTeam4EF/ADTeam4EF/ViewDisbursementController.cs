using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTeam4EF
{
    public class ViewDisbursementController
    {
        ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();
        //public List<ViewDisburmentList> ViewDisburmentList(string place, DateTime approvedate)
        //{
        //    try
        //    {
        //        List<ViewDisburmentList> cList = new List<ViewDisburmentList>();
        //        var q = (from u in ctx.ViewDisburmentLists
        //                 where u.Place == place && u.ApproveDate == approvedate
        //                 select u);

        //        //cList = q.ToList<ViewDisburmentList>();

        //        if (cList != null)
        //        {
        //            foreach (ViewDisburmentList b in q)
        //            {
        //                cList.Add(b);
        //            }
        //            return cList;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex.InnerException;
        //    }
        //}

        public List<Department> GetAllDepartment(int empid)
        {
            try
            {
                List<Department> EmpColl = (from empc in ctx.Departments join cpt in ctx.CollectionPoints on 
                                                empc.CollectionPointID equals cpt.CollectionPointID 
                                            where cpt.InCharge == empid select empc).ToList();
               
                return EmpColl;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

          public string GetCollectionPoint(int id)
        {
            try
            {
              
                var q = from x in ctx.CollectionPoints
                        where x.CollectionPointID  == id
                        select x.Place ;                

                return q.First().ToString ();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

    }
}
