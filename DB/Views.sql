CREATE VIEW ApproveRejectRequest_View AS
SELECT r.RequestID,i.Description, rd.RequestedQty, i.UnitOfMeasurement
FROM Item i, RequestDetail rd, Request r
WHERE i.ItemID = rd.RequestedItem AND
rd.RequestID = r.RequestID


Create VIEW DisplayLowLevelStock_View AS
SELECT i.ItemID,c.CategoryName, i.Description, c.Location,i.Balance, (i.Balance+ISNULL(po.OrderQty,0)) As [Balance after reorder],i.ReorderLevel, 
i.ReorderQuantity,i.SuggestedQuantity,i.UnitOfMeasurement
FROM Category c,Item i left join PurchaseOrderDetail po on i.ItemID = po.ItemID
WHERE i.CategoryID=c.CategoryID 
AND (i.Balance+ISNULL(po.OrderQty,0))<i.ReorderLevel


select * from Item
where ItemID = 'C001'

CREATE VIEW RaiseAdjustmentVoucher_View AS
SELECT  i.Description, ad.Quantity, i.UnitOfMeasurement, si.Price, a.TotalPrice, ad.AdjustmentRemark
FROM Item i, AdjustmentDetail ad, SupplyItem si, Adjustment a
WHERE si.ItemID = i.ItemID AND i.ItemID = ad.ItemID AND a.AdjustmentID = ad.AdjustmentID


CREATE VIEW RequestHistory_View AS
SELECT r.RequestID, i.Description, rd.RequestedQty,rd.ReceivedQty, i.UnitOfMeasurement
FROM Request r, RequestDetail rd, Item i
WHERE rd.RequestID = r.RequestID AND rd.RequestedItem = i.ItemID


CREATE VIEW RequestReport_View AS
SELECT r.RequestID, d.DepartmentName, c.CategoryName, SUM(s.Price) AS [Total Price], SUM(rd.RequestedQty) AS [Total Quantity], r.RequestDate
FROM Request r, Department d, RequestDetail rd, Category c, Item i, SupplyItem s
WHERE d.DepartmentID = r.RequestByDepartmentID AND r.RequestID= rd.RequestID AND rd.RequestedItem = i.ItemID AND s.ItemID= i.ItemID AND i.CategoryID =c.CategoryID AND s.Priority =1
GROUP BY r.RequestID, d.DepartmentName,c.CategoryName,r.RequestDate



CREATE VIEW DISTINCTEMPLOYEE_VIEW AS
SELECT DISTINCT(e.EmployeeName)
FROM Employee e, Request r
WHERE r.RequestByEmployeeID = e.EmployeeID
 
CREATE VIEW ReorderReport_View AS
SELECT i.ItemID, c.CategoryName, i.Description, SUM(pod.OrderQty) AS TotalQty, si.Price ,i.UnitOfMeasurement,po.OrderDate
FROM Item i, PurchaseOrder po, PurchaseOrderDetail pod, Category c, SupplyItem si
WHERE i.ItemID = pod.ItemID AND pod.PurchaseOrderID = po.PurchaseOrderID AND c.CategoryID=i.CategoryID AND si.ItemID=i.ItemID AND si.Priority=1
Group By i.ItemID, c.CategoryName, i.Description,i.UnitOfMeasurement,po.PurchaseOrderID, si.Price,po.OrderDate,po.ExpectedDeliveryDate