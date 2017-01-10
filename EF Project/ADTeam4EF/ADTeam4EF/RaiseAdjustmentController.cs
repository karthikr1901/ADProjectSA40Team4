using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ADTeam4EF
{
    public class RaiseAdjustmentController
    {
        ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();
        public List<Category> getCategories()
        {
            try
            {
                var category = from Cat in ctx.Categories
                               select Cat;
                return category.ToList<Category>();
            }
            catch
            {
                return null;
            }
        }

        public int getCategoryID(string categoryName)
        {
            try
            {
                var catID = from Cat in ctx.Categories
                            where Cat.CategoryName == categoryName
                            select Cat.CategoryID;
                return catID.First();
            }
            catch
            {
                return 0;
            }
        }

        public List<Item> getItemsByCategoryId(int catID)
        {
            try
            {
                var description = from itemDes in ctx.Items
                                  join cate in ctx.Categories on itemDes.CategoryID equals cate.CategoryID
                                  where itemDes.CategoryID == catID
                                  select itemDes;
                List<Item> i = description.ToList<Item>();
                if (i.Count() > 0)
                {
                    return i;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public decimal populatePrice(string itemID)
        {
            try
            {
                var price = from s in ctx.SupplyItems
                            where s.ItemID == itemID
                            select s.Price;
                return price.First();
            }
            catch
            {
                return 0;
            }
        }




        public DataTable addItemtoAdjustmentDetail(string ItemID)
        {
            var adjDetailList = (from i in ctx.Items
                                 join si in ctx.SupplyItems on i.ItemID equals si.ItemID
                                 where i.ItemID == ItemID
                                 select new { i.Description, i.UnitOfMeasurement, si.Price }).ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("Description");
            dt.Columns.Add("UOM");
            dt.Columns.Add("Price");
            //DataRow dr;

            foreach (var t in adjDetailList)
            {
                dt.Rows.Add(t.Description, t.UnitOfMeasurement, t.Price);
            }

            return dt;
        }

        public bool insertAdjustmentItem(Adjustment adjHeader, ICollection<AdjustmentDetail> adjItems)
        {
            try
            {
                ctx.Adjustments.Add(adjHeader);
                foreach (AdjustmentDetail adjItem in adjItems)
                {
                    ctx.AdjustmentDetails.Add(adjItem);
                }
                ctx.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Boolean addDataToAdjustment(DataTable dt, string userName, decimal tp)
        {
            bool result = false;

            var userID = from emp in ctx.Employees
                         where emp.EmployeeName == userName
                         select emp.EmployeeID;

            using (TransactionScope ts = new TransactionScope())
            {
                Adjustment adjustment = new Adjustment();
                adjustment.AdjustmentStatus = "Pending";
                adjustment.AdjustedByEmployeeID = userID.First();
                adjustment.ApprovedByEmployeeID = null;
                adjustment.RequestAdjustmentDate = DateTime.Now;
                adjustment.ApproveAdjustmentDate = null;
                adjustment.TotalPrice = tp;
                ctx.Adjustments.Add(adjustment);

                foreach (DataRow dr in dt.Rows)
                {
                    AdjustmentDetail adjustmentDetail = new AdjustmentDetail();
                    int qty = Convert.ToInt32(dr["Quantity"]);
                    string itemID = dr["ItemID"].ToString();
                    string remark = dr["AdjustmentRemark"].ToString();
                    adjustmentDetail.ItemID = itemID;
                    adjustmentDetail.AdjustmentID = adjustment.AdjustmentID;
                    adjustmentDetail.Quantity = qty;
                    adjustmentDetail.AdjustmentRemark = remark;
                    ctx.AdjustmentDetails.Add(adjustmentDetail);

                    if ((remark == "Free gift in offer pack") || (remark == "Special gift"))
                    {
                        addItemQty(itemID, qty);
                    }
                    else
                    {
                        result = reduceItemQty(itemID, qty);

                    }
                }


                if (result == true)
                {
                    ctx.SaveChanges();
                    ts.Complete();
                    return result;
                }
                else
                {
                    return result;
                }

            }
        }

        public Boolean reduceItemQty(string itemID, int quantity)
        {

            var itemData = from i in ctx.Items
                           where i.ItemID == itemID
                           select i;
            Item item = itemData.First();
            if (quantity <= item.Balance)
            {
                item.Balance -= quantity;
                ctx.SaveChanges();
                return true;
            }
            return false;
        }
        public void addItemQty(string itemID, int quantity)
        {
            var itemData = from i in ctx.Items
                           where i.ItemID == itemID
                           select i;
            Item item = itemData.First();
            item.Balance += quantity;
            ctx.SaveChanges();
        }

        public int checkAdjustCount(int empID)
        {
            var adjNewAdjust = (from ad in ctx.Adjustments
                                where ad.AdjustmentStatus == "NEW" && ad.AdjustedByEmployeeID == empID
                                select ad).Count();

            return adjNewAdjust;

        }

        //New Method start

        public DataTable addNewItemtoGrid(int adjustID)
        {
            var adj = from adD in ctx.AdjustmentDetails
                      join ad in ctx.Adjustments on adD.AdjustmentID equals ad.AdjustmentID
                      join i in ctx.Items on adD.ItemID equals i.ItemID
                      join si in ctx.SupplyItems on i.ItemID equals si.ItemID
                      where ad.AdjustmentID == adjustID && si.Priority == 1
                      select new { i.Description, adD.Quantity, i.UnitOfMeasurement, si.Price, Amount = (adD.Quantity * si.Price), adD.AdjustmentRemark };


            DataTable dt = new DataTable();
            dt.Columns.Add("Description");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("UnitOfMeasurement");
            dt.Columns.Add("Price");
            dt.Columns.Add("Amount");
            dt.Columns.Add("AdjustmentRemark");
            foreach (var t in adj)
            {
                dt.Rows.Add(t.Description, t.Quantity, t.UnitOfMeasurement, t.Price, t.Amount, t.AdjustmentRemark);
            }



            return dt;

        }

        public int getAdjustID(int empID)
        {
            var ad = from adj in ctx.Adjustments
                     where adj.AdjustmentStatus == "NEW" && adj.AdjustedByEmployeeID == empID
                     select adj.AdjustmentID;

            return ad.FirstOrDefault();
        }


        public Boolean updateDataToAdjustment(int adjID, DataTable dt)
        {
            var AdjList = from adj in ctx.Adjustments
                          where adj.AdjustmentID == adjID
                          select adj;
            decimal tPrice = 0;

            foreach (DataRow dr in dt.Rows)
            {
                tPrice += Convert.ToDecimal(dr["Amount"].ToString());
            }

            try
            {
                Adjustment adjustment = AdjList.FirstOrDefault();
                adjustment.AdjustmentStatus = "Pending";
                adjustment.RequestAdjustmentDate = DateTime.Now;
                adjustment.TotalPrice = tPrice;
                ctx.SaveChanges();

                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                throw e;
            }


        }
        public int checkQuantity(string itemID, int quantity)
        {
            try
            {
                var balance = from i in ctx.Items
                              where i.ItemID == itemID
                              select i.Balance;
                int b = Convert.ToInt32(balance.First());
                return b;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
