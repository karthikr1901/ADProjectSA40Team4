using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADTeam4EF
{
    public class EmpNotify
    {
        ADProjectSA40Team4Entities ad = new ADProjectSA40Team4Entities();
        public dynamic Notify(int empid)
        {
            var note = (from note1 in ad.Requests 
                        where note1.RequestByEmployeeID == empid &&
                        (note1.RequestStatus == "Approved" || note1.RequestStatus == "Rejected" || note1.RequestStatus == "Alloted" ) 
                        select note1).ToList();
            
            List<Request> lr = new List<Request>();
            
            foreach (var t in note)
            {
                lr.Add(t);
            }
            return lr;
        }
    }
}
