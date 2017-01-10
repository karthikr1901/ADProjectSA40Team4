using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace ADTeam4EF
{
    public class PurchaseControlOrder
    {
        ADProjectSA40Team4Entities ad = new ADProjectSA40Team4Entities();
        public DataTable PageLoadGrid()
        {
            var pglgrid = (from pglg in ad.Items join pgp in ad.SupplyItems on pglg.ItemID equals pgp.ItemID
                           join gpg in ad.Suppliers on pgp.SupplierID equals gpg.SupplierID
                           where (int)(pglg.Balance) < (int)(pglg.ReorderLevel) && pgp.Priority == 1 select new
                           {
                               pglg.Description, pglg.ItemID, pglg.Balance, pglg.ReorderLevel, pglg.ReorderQuantity,
                               pgp.Price, gpg.SupplierName, gpg.SupplierID
                           }).ToList();

            var typo = (from aa in ((from rdd in ad.RequestDetails join rr in ad.Requests 
                        on rdd.RequestID equals rr.RequestID
                        where rr.RequestStatus == "Outstanding" select rdd).ToList())group aa by new { aa.RequestedItem} into gg
                      select new
                      {
                          gg.Key.RequestedItem,
                          Quantity = (System.Int32?)gg.Sum(p => p.RequestedQty)
                      }).ToList();

            var typo2 = (from pod in  ad.PurchaseOrderDetails join po in ad.PurchaseOrders
                         on pod.PurchaseOrderID equals po.PurchaseOrderID
                         where po.PurchaseStatus == "Pending" select pod).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("itemid"); dt.Columns.Add("description"); dt.Columns.Add("balance"); dt.Columns.Add("reorderlevel");
            dt.Columns.Add("reorderqty"); dt.Columns.Add("suggestedqty"); dt.Columns.Add("suppliername");
            dt.Columns.Add("supplierid"); dt.Columns.Add("price"); dt.Columns.Add("totalcost");
            DataRow dr;
            foreach (var tpglg in pglgrid)
            {
                dr = dt.NewRow();
                int bal = (int)tpglg.Balance;
                int rl = (int)tpglg.ReorderLevel;
                int rq = (int)tpglg.ReorderQuantity;
                int sqty = 0;
                if (rl - bal > rq)
                    sqty = rl - bal;
                else
                    sqty = rq;
                foreach (var tc1 in typo)
                {
                    if (tc1.RequestedItem == tpglg.ItemID)
                        sqty += (int)tc1.Quantity;
                }
                foreach (var tc2 in typo2)
                {
                    if (tc2.ItemID == tpglg.ItemID)
                        if (((int)tc2.OrderQty - sqty) >= 0)
                            sqty = 0;
                }

                if (sqty == 0)
                { }
                else
                {
                    decimal pcost = (decimal)(sqty * tpglg.Price);
                    dr["itemid"] = tpglg.ItemID; dr["description"] = tpglg.Description; dr["balance"] = (int)(tpglg.Balance);
                    dr["reorderlevel"] = (int)(tpglg.ReorderLevel); dr["reorderqty"] = (int)(tpglg.ReorderQuantity); dr["suggestedqty"] = (int)(sqty);
                    dr["suppliername"] = tpglg.SupplierName; dr["supplierid"] = (int)(tpglg.SupplierID); dr["price"] = tpglg.Price; dr["totalcost"] = pcost;
                    dt.Rows.Add(dr);
                }
            }
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        public List<PurchaseClassOrder> ItemOne(string itemd)        
        {
            var pglgrid = (from pglg in ad.Items
                           join pgp in ad.SupplyItems on
                               pglg.ItemID equals pgp.ItemID
                           join gpg in ad.Suppliers on
                               pgp.SupplierID equals gpg.SupplierID
                           where pglg.ItemID == itemd
                           select new
                           {
                               pglg.Description,
                               pglg.ItemID,
                               pglg.Balance,
                               pglg.ReorderLevel,
                               pglg.ReorderQuantity,
                               pgp.Price,
                               gpg.SupplierName,
                               gpg.SupplierID
                           }).ToList();

            
            List<PurchaseClassOrder> lpco = new List<PurchaseClassOrder>();
            PurchaseClassOrder pco;
            foreach (var tpglg in pglgrid)
            {
                int sqty = (int)((tpglg.ReorderLevel - tpglg.Balance) + tpglg.ReorderQuantity);
                decimal pcost = (decimal)(sqty * tpglg.Price);
                pco = new PurchaseClassOrder(tpglg.ItemID, tpglg.Description, (int)(tpglg.Balance), (int)(tpglg.ReorderLevel), (int)(tpglg.ReorderQuantity), (int)(sqty), tpglg.SupplierName, (int)(tpglg.SupplierID), tpglg.Price, pcost);
                
                lpco.Add(pco);
            }
            
            return lpco;
        }


        public DataTable ItemOnePriority(string itemd, string supp)
        {
            int suppid = Convert.ToInt32(supp);
            var pglgrid = (from pglg in ad.Items
                           join pgp in ad.SupplyItems on
                               pglg.ItemID equals pgp.ItemID
                           join gpg in ad.Suppliers on
                               pgp.SupplierID equals gpg.SupplierID
                           where pglg.ItemID == itemd && gpg.SupplierID == suppid
                           select new
                           {
                               pglg.Description,
                               pglg.ItemID,
                               pglg.Balance,
                               pglg.ReorderLevel,
                               pglg.ReorderQuantity,
                               pgp.Price,
                               gpg.SupplierName,
                               gpg.SupplierID
                           }).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("itemid"); dt.Columns.Add("description"); dt.Columns.Add("balance"); dt.Columns.Add("reorderlevel");
            dt.Columns.Add("reorderqty"); dt.Columns.Add("suggestedqty"); dt.Columns.Add("suppliername");
            dt.Columns.Add("supplierid"); dt.Columns.Add("price"); dt.Columns.Add("totalcost");
            DataRow dr;
            foreach (var tpglg in pglgrid)
            {
                dr = dt.NewRow();
                int sqty = (int)((tpglg.ReorderLevel - tpglg.Balance) + tpglg.ReorderQuantity);
                decimal pcost = (decimal)(sqty * tpglg.Price);               
                dr["itemid"] = tpglg.ItemID; dr["description"] = tpglg.Description; dr["balance"] = (int)(tpglg.Balance);
                dr["reorderlevel"] = (int)(tpglg.ReorderLevel); dr["reorderqty"] = (int)(tpglg.ReorderQuantity); dr["suggestedqty"] = (int)(sqty); dr["suppliername"] = tpglg.SupplierName; dr["supplierid"] = (int)(tpglg.SupplierID); dr["price"] = tpglg.Price; dr["totalcost"] = pcost;
                dt.Rows.Add(dr);
            }

            return dt;
        }
        public void RaisePurchaseControlOrder(DataTable dt2, int employeid, DateTime date)
        {
            int req = 0;
            DataTable dt = new DataTable();
            dt = dt2;
            int supid = Convert.ToInt32(dt.Rows[0]["supplierid"]);
            PurchaseOrder poentry = new PurchaseOrder
            {
                PurchaseStatus = "NEW",
                SupplierID = supid,
                EmployeeID = employeid
            };
            ad.PurchaseOrders.Add(poentry);
            ad.SaveChanges();            
            for (int f = 0; f < dt.Rows.Count; f++)
            {
            like:
                var genReqNo = (from grn1 in ad.PurchaseOrders where grn1.PurchaseStatus == "NEW" && grn1.SupplierID == supid && grn1.EmployeeID == employeid select grn1).ToList();
                if (genReqNo.Count > 0)
                {
                    req = genReqNo[0].PurchaseOrderID;
                    if (Convert.ToInt32(dt.Rows[f]["supplierid"]) == supid)
                    {
                        PurchaseOrderDetail reqd = new PurchaseOrderDetail
                        {
                            PurchaseOrderID = req,
                            ItemID = Convert.ToString(dt.Rows[f]["itemid"]),
                            OrderQty = Convert.ToInt32(dt.Rows[f]["suggestedqty"]),
                            ReceivedQty = 0
                        };


                        ad.PurchaseOrderDetails.Add(reqd);

                        try
                        {
                            ad.SaveChanges();
                        }
                        catch (Exception tye)
                        {
                            Console.WriteLine(tye);

                        }
                        if((f+1)<dt.Rows.Count)
                            supid = Convert.ToInt32(dt.Rows[f+1]["supplierid"]);
                    }
                }
                else
                {
                    PurchaseOrder poentry2 = new PurchaseOrder
                    {
                        PurchaseStatus = "NEW",
                        SupplierID = supid,
                        EmployeeID = employeid
                    };
                    ad.PurchaseOrders.Add(poentry2);
                    ad.SaveChanges();
                    goto like;
                }
            }
            like2:
            var genReqNo2 = (from grn1 in ad.PurchaseOrders where grn1.PurchaseStatus == "NEW" && grn1.EmployeeID == employeid select grn1).ToList();
            if (genReqNo2.Count > 0)
            {
                int reqtid = genReqNo2[0].PurchaseOrderID;
                PurchaseOrder addreq = (from c in ad.PurchaseOrders where c.PurchaseOrderID == reqtid select c).SingleOrDefault(); //ad.Requests.Single(rid => rid.RequestID == reqtid);
                addreq.PurchaseStatus = "Pending";
                addreq.OrderDate = System.DateTime.Now.Date;
                addreq.ExpectedDeliveryDate = date;
                try
                {
                    ad.SaveChanges();
                    //sendmail(reqtid);
                }
                catch (Exception tye)
                {
                    Console.WriteLine(tye);

                }
                goto like2;
            }
        }
        
    }
}
