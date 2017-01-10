using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTeam4EF
{
    public class ReceivedOrderController
    {
        ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();
        public List<PurchaseOrder> getPurchaseOrderID()
        {
            try
            {
                var POList = from PO in ctx.PurchaseOrders
                          where PO.PurchaseStatus == "Pending"
                          select PO;
                List<PurchaseOrder> PList = POList.ToList<PurchaseOrder>();
                if(PList.Count()>0)
                {
                    return PList;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public string getSupplierbyPOID(int POID)
        {
            try
            {
                var supplier = from sup in ctx.Suppliers
                               join PO in ctx.PurchaseOrders on sup.SupplierID equals PO.SupplierID
                               where PO.PurchaseOrderID == POID
                               select sup.SupplierName;
                return supplier.First();
            }
            catch
            {
                return null;
            }
        }

        public DateTime getOrderDate(int POID)
        {
            try
            {
                var date = from PO in ctx.PurchaseOrders
                           where PO.PurchaseOrderID == POID
                           select PO.OrderDate;
                return (DateTime)date.First();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                throw e;
            }
        }

        public dynamic getPurchaseOrderListbyPOid(int POID)
        {
            try
            {
                var POList = (from PO in ctx.PurchaseOrders
                              join PODetail in ctx.PurchaseOrderDetails on PO.PurchaseOrderID equals PODetail.PurchaseOrderID
                              join i in ctx.Items on PODetail.ItemID equals i.ItemID
                              where PO.PurchaseOrderID== POID
                              select new ReceivedOrderObj{ ItemID =i.ItemID,Description= i.Description,UnitOfMeasurement= i.UnitOfMeasurement,OrderQty= (int)PODetail.OrderQty, ReceivedQty =(int)PODetail.OrderQty,OrderDate= (DateTime)PO.OrderDate,ExpectedDeliveryDate= (DateTime)PO.ExpectedDeliveryDate }).ToList();

                              
                return POList;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                throw e;
            }

        }
        public DataTable getListDataTable(int POID)
        {
            try
            {
                var POList = (from PO in ctx.PurchaseOrders
                              join PODetail in ctx.PurchaseOrderDetails on PO.PurchaseOrderID equals PODetail.PurchaseOrderID
                              join i in ctx.Items on PODetail.ItemID equals i.ItemID
                              where PO.PurchaseOrderID == POID
                              select new ReceivedOrderObj { ItemID = i.ItemID, Description = i.Description, UnitOfMeasurement = i.UnitOfMeasurement, OrderQty = (int)PODetail.OrderQty, ReceivedQty = (int)PODetail.OrderQty, OrderDate = (DateTime)PO.OrderDate, ExpectedDeliveryDate = (DateTime)PO.ExpectedDeliveryDate }).ToList();
                DataTable dt = new DataTable();
                dt.Columns.Add("ItemID");
                dt.Columns.Add("Description");
                dt.Columns.Add("OrderQty");
                dt.Columns.Add("ReceivedQty");
                dt.Columns.Add("UnitOfMeasurement");
                dt.Columns.Add("Remark");
                foreach (var t in POList)
                {
                    dt.Rows.Add(t.ItemID, t.Description,  t.OrderQty, t.ReceivedQty, t.UnitOfMeasurement);
                }
                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                throw e;
            }

        }

        public Boolean updatePurchaseOrder(int POID, string userName, string DVNo, DataTable dt)
        {
            try
            {
                var POList = from POrd in ctx.PurchaseOrders
                              where POrd.PurchaseOrderID == POID
                              select POrd;
                var userID = from emp in ctx.Employees
                             where emp.EmployeeName == userName
                             select emp.EmployeeID;
                
                if (POList.Count() > 0)
                {
                    PurchaseOrder PO = POList.FirstOrDefault();
                    PO.PurchaseStatus = "Updated";
                    PO.ReceivedByEmployeeID = userID.FirstOrDefault();
                    PO.ReceivedDate = DateTime.Now;
                    PO.DeliverVoucherNo = DVNo;

                    ctx.SaveChanges();

                    foreach (DataRow dr in dt.Rows)
                    {
                        string itemID= dr["ItemID"].ToString();
                        PurchaseOrderDetail purchaseOrderDetail = updatePurchaseOrderDetail(POID, itemID);
                        int qty = Convert.ToInt16(dr["ReceivedQty"]);
                        string remark = dr["Remark"].ToString();
                        purchaseOrderDetail.ReceivedQty = qty;
                        purchaseOrderDetail.Remark = remark;
                        ctx.SaveChanges();

                        addItemQty(itemID, qty);
                    }


                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                throw e;
            }

        }

        public PurchaseOrderDetail updatePurchaseOrderDetail(int POID, string itemID)
        {
            try
            {
                var PODetail = from POD in ctx.PurchaseOrderDetails
                               join PO in ctx.PurchaseOrders on POD.PurchaseOrderID equals PO.PurchaseOrderID
                               where PO.PurchaseOrderID == POID && POD.ItemID == itemID
                               select POD;

                return PODetail.First();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                throw e;
            }
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

        public List<PurchaseOrder> MgetPurchaseOrderID()
        {
            List<PurchaseOrder> l = new List<PurchaseOrder>();
            l = getPurchaseOrderID();
            return l;
        }

        public List<MobGetPurchaseOrderClass> MgetPurchaseOrderListbyPOid(string POID1)
        {
            int POID = Convert.ToInt32(POID1);
            try
            {
                var POList = (from PO in ctx.PurchaseOrders
                              join PODetail in ctx.PurchaseOrderDetails on PO.PurchaseOrderID equals PODetail.PurchaseOrderID
                              join i in ctx.Items on PODetail.ItemID equals i.ItemID
                              where PO.PurchaseOrderID == POID
                              select new { i.ItemID, i.Description, i.UnitOfMeasurement, PODetail.OrderQty, PO.OrderDate, PO.ExpectedDeliveryDate }).ToList();
                MobGetPurchaseOrderClass mg;
                List<MobGetPurchaseOrderClass> lmg = new List<MobGetPurchaseOrderClass>();
                foreach (var t in POList)
                {
                    mg = new MobGetPurchaseOrderClass((string)t.ItemID, (string)t.Description, (string)t.UnitOfMeasurement, (int)t.OrderQty, t.OrderDate.ToString(), t.ExpectedDeliveryDate.ToString());
                    lmg.Add(mg);
                }

                return lmg;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                throw e;
            }

        }

        public string McheckItem(string POID1, string itemid)
        {
            int POID = Convert.ToInt32(POID1);
            var POList = (from PODetail in ctx.PurchaseOrderDetails
                          join PO in ctx.PurchaseOrders on PODetail.PurchaseOrderID equals PO.PurchaseOrderID
                          where PODetail.PurchaseOrderID == POID && PODetail.ItemID == itemid
                          select PODetail).ToList();
            if (POList.Count > 0)
                return "true";
            else return "false";
        }

        public List<MobGetPurchaseOrderClass2> MupdatePurchaseOrderDetail(string POID1, string itemID, string qty1, string remark)
        {
            int POID = Convert.ToInt32(POID1);
            int qty = Convert.ToInt32(qty1);
            try
            {
                PurchaseOrderDetail purchaseOrderDetail = updatePurchaseOrderDetail(POID, itemID);
                purchaseOrderDetail.ReceivedQty = qty;
                purchaseOrderDetail.Remark = remark;
                ctx.SaveChanges();
                addItemQty(itemID, qty);
                var POList = (from PO in ctx.PurchaseOrders
                              join PODetail in ctx.PurchaseOrderDetails on PO.PurchaseOrderID equals PODetail.PurchaseOrderID
                              join i in ctx.Items on PODetail.ItemID equals i.ItemID
                              where PO.PurchaseOrderID == POID
                              select new { i.ItemID, i.Description, i.UnitOfMeasurement, PODetail.OrderQty, PO.OrderDate, PO.ExpectedDeliveryDate, PODetail.ReceivedQty }).ToList();
                MobGetPurchaseOrderClass2 mg;
                List<MobGetPurchaseOrderClass2> lmg = new List<MobGetPurchaseOrderClass2>();
                foreach (var t in POList)
                {
                    mg = new MobGetPurchaseOrderClass2((string)t.ItemID, (string)t.Description, (string)t.UnitOfMeasurement, (int)t.OrderQty, t.OrderDate.ToString(), t.ExpectedDeliveryDate.ToString(), (int)t.ReceivedQty);
                    lmg.Add(mg);
                }

                return lmg;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                throw e;
            }
        }

        public string MupdatePurchaseOrder(string POID1, string empid1, string DVNo)
        {
            int POID = Convert.ToInt32(POID1);
            int empid = Convert.ToInt32(empid1);
            try
            {
                var POList = from POrd in ctx.PurchaseOrders
                             where POrd.PurchaseOrderID == POID
                             select POrd;

                if (POList.Count() > 0)
                {
                    PurchaseOrder PO = POList.FirstOrDefault();
                    PO.PurchaseStatus = "Updated";
                    PO.ReceivedByEmployeeID = empid;
                    PO.ReceivedDate = DateTime.Now;
                    PO.DeliverVoucherNo = DVNo;

                    ctx.SaveChanges();


                    return "true";
                }
                else
                {
                    return "false";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                throw e;
            }
        }

    }
}
