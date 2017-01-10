using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Transactions;

namespace ADTeam4EF
{
    public class CatalogueController
    {

        ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();

        public List<Category> populateCategory()
        {
            try
            {
                List<Category> categoryList = new List<Category>();
                var category = from cat in ctx.Categories
                               select cat;
                categoryList = category.ToList<Category>();
                return categoryList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable getAllCategory()
        {
            try
            {
                var category = (from item in ctx.Items
                                join cate in ctx.Categories on item.CategoryID equals cate.CategoryID
                                select new { item.ItemID, item.Category.CategoryName, item.Description, item.ReorderLevel, item.ReorderQuantity, item.UnitOfMeasurement, item.Balance }).ToList();
                DataTable dt = new DataTable();
                dt.Columns.Add("ItemID");
                dt.Columns.Add("CategoryName");
                dt.Columns.Add("Description");
                dt.Columns.Add("ReorderLevel");
                dt.Columns.Add("ReorderQuantity");
                dt.Columns.Add("UnitOfMeasurement");
                dt.Columns.Add("Balance");
                //DataRow dr;

                foreach (var t in category)
                {
                    dt.Rows.Add(t.ItemID, t.CategoryName, t.Description, t.ReorderLevel, t.ReorderQuantity, t.UnitOfMeasurement, t.Balance);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string getLastRow(string category)
        {
            try
            {
                var ca = from cate in ctx.Categories
                         where cate.CategoryName == category
                         select cate;
                Category c = ca.First();
                var item = from it in ctx.Items
                           where it.CategoryID == c.CategoryID
                           orderby it.ItemID descending
                           select it;
                Item i = item.First();
                if (i != null)
                {
                    return i.ItemID.ToString();
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
        public bool insertAllCategory(string itemNo, string category, string description, int reorderLevel, int reorderQty, string unitOfMeasurement, int balance)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var ca = from cate in ctx.Categories
                             where cate.CategoryName == category
                             select cate;
                    Category c = ca.First();
                    ADTeam4EF.Item item = new ADTeam4EF.Item();
                    item.ItemID = itemNo;
                    item.CategoryID = c.CategoryID;
                    item.Description = description;
                    item.ReorderLevel = reorderLevel;
                    item.ReorderQuantity = reorderQty;
                    item.UnitOfMeasurement = unitOfMeasurement;
                    item.Balance = balance;
                    item.SuggestedQuantity = reorderQty;
                    ctx.Items.Add(item);
                    ctx.SaveChanges();
                    ts.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }

        }

        public bool deleteItem(string itemid)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var i = from item in ctx.Items
                            where item.ItemID == itemid
                            select item;
                    ADTeam4EF.Item it = i.First();
                    if (it != null)
                    {
                        ctx.Items.Remove(it);
                        ctx.SaveChanges();
                        ts.Complete();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        public bool checkCategory(string category)
        {
            try
            {
                var cat = from c in ctx.Categories
                          where c.CategoryName == category
                          select c;
                ADTeam4EF.Category cate = cat.FirstOrDefault();
                if (cate != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool updateItem(string itemNo, string category, string description, int reorderLevel, int reorderQty, string unitOfMeasurement, int balance)
        {
            try
            {
                var cat = from c in ctx.Categories
                          where c.CategoryName == category
                          select c;
                ADTeam4EF.Category cate = cat.First();
                var item = from i in ctx.Items
                           where i.ItemID == itemNo
                           select i;
                ADTeam4EF.Item it = item.First();
                if (item != null)
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        it.Description = description;
                        it.ReorderLevel = reorderLevel;
                        it.ReorderQuantity = reorderQty;
                        it.UnitOfMeasurement = unitOfMeasurement;
                        it.Balance = balance;
                        it.CategoryID = cate.CategoryID;
                        ctx.SaveChanges();
                        ts.Complete();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }


    }


}
