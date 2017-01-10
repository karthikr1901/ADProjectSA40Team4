using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTeam4EF
{
    public class InventoryStatusController
    {
        ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();
        public List<DisplayLowLevelStock_View> displayLowLevelStock()
        {
            try
            {
                var lowStock = from items in ctx.DisplayLowLevelStock_View
                               select items;
                return lowStock.ToList<DisplayLowLevelStock_View>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
