using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
namespace ADTeam4EF
{
    public class ChangeCollectionPt
    {
        ADProjectSA40Team4Entities ad = new ADProjectSA40Team4Entities();

        public List<CollectionPoint> CurrentCollectionPt(string dep)
        {
            List<CollectionPoint> ccpt = (from h in ad.Departments join a in ad.CollectionPoints on h.CollectionPointID equals a.CollectionPointID where h.DepartmentID == dep select a).ToList();
            return ccpt;
        }

        public List<CollectionPoint> DropCollectionPt()
        {
            List<CollectionPoint> dropcpt = (from empl in ad.CollectionPoints select empl).ToList();
            return dropcpt;
        }


        public void UpdateCollectionPt(string dep, int trancptid)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Department dcpt = (from ftg in ad.Departments where ftg.DepartmentID == dep select ftg).SingleOrDefault();
                dcpt.CollectionPointID = trancptid;
                ad.SaveChanges();
                ts.Complete();
            }
        }

        public string DisplayTime(int cptid)
        {
            string rtime = (from tim in ad.CollectionPoints where tim.CollectionPointID == cptid select tim.Time).FirstOrDefault().ToString();
            return rtime;
        }
    }
}
