using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
namespace ADTeam4EF
{
    public class StoreClerkHomeController
    {
        ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();
        public List<DisplayLowLevelStock_View> displayLowLevelStock()
        {
            try
            {
                var lowStock = from items in ctx.DisplayLowLevelStock_View
                               select items;
                if (lowStock.Count() > 0)
                {
                    return lowStock.ToList<DisplayLowLevelStock_View>();
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
		
		
		public int displayLowLevelStockQty()
        {
            try
            {
                var lowStock = from items in ctx.DisplayLowLevelStock_View
                               select items;
                return lowStock.ToList<DisplayLowLevelStock_View>().Count;
            }
            catch
            {
                return 0;
            }
        }
        public bool updateSuggestedQty(string itemid, DateTime todayDate)
        {
            try
            {
                var lowStock = from items in ctx.Items
                               join reqDetail in ctx.RequestDetails on items.ItemID equals reqDetail.RequestedItem
                               join req in ctx.Requests on reqDetail.RequestID equals req.RequestID
                               where reqDetail.RequestedItem == itemid && req.RequestDate <= todayDate
                               select items;
                var count = lowStock.Count();
                if (count > 20)
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        var item = from items in ctx.Items
                                   where items.ItemID == itemid
                                   select items;
                        ADTeam4EF.Item i = item.First();
                        i.SuggestedQuantity = i.ReorderQuantity * 2;
                        ctx.SaveChanges();
                        ts.Complete();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
