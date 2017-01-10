using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ADTeam4EF;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    LoginController loginController;
    ApproveRejectRequestController approveRejectRequestController;
    DelegateAuthorityController delegateAuthorityController;
    ProcessReqSC processReqController;
    Disbursement disbursementController;
    StoreClerkHomeController storeClerkHomeController;
    ApproveAdjustmentController approveAdjustmentController;
    AppointRepresentativeController appointRepresentativeController;
    EmpNewRequest empNewRequest;
    ReceivedOrderController receivedOrderController;

    //public List<WCF_Employee> GetAll()
    //{
    //    loginController = new LoginController();
    //    List<WCF_Employee> eWCFList = new List<WCF_Employee>();
    //    List<Employee> eList = loginController.GetAll();

    //    foreach (Employee emp in eList)
    //    {
    //        eWCFList.Add(new WCF_Employee((int)emp.EmployeeID, emp.EmployeeName, emp.EmployeePassword, emp.EmployeeContactNo, emp.EmployeeEmail, emp.EmployeeAddress, (int)emp.RoleID, emp.DepartmentID));
    //    }

    //    return eWCFList;
    //}

    public WCF_Employee GetAuthenticateUser(string email, string hashedPassword)
    {
        loginController = new LoginController();
        String encryptedPassword = loginController.GetSHA1HashData(hashedPassword);
        Employee emp = loginController.authenticateUser(email, encryptedPassword);

        return new WCF_Employee((int)emp.EmployeeID, emp.EmployeeName, emp.EmployeePassword, emp.EmployeeContactNo, emp.EmployeeEmail, emp.EmployeeAddress, (int)emp.RoleID, emp.DepartmentID);
    }

    public WCF_Employee GetRepresentative(string DepartmentID)
    {
        appointRepresentativeController = new AppointRepresentativeController();

        Employee emp = appointRepresentativeController.getRepresentative(DepartmentID);

        return new WCF_Employee((int)emp.EmployeeID, emp.EmployeeName, emp.EmployeePassword, emp.EmployeeContactNo, emp.EmployeeEmail, emp.EmployeeAddress, (int)emp.RoleID, emp.DepartmentID);
    }

    public WCF_RMessage appointNewRepresentativeMobile(String EmployeeID, String DepartmentID)
    {
        appointRepresentativeController = new AppointRepresentativeController();
        WCF_RMessage req = new WCF_RMessage(appointRepresentativeController.appointNewRepresentativeMobile(EmployeeID, DepartmentID));

        return req;
    }

    public WCF_RequestID MobileGenReqNoFirst(String EmployeeID)
    {
        empNewRequest = new EmpNewRequest();
        empNewRequest.MobileGenReqNo(int.Parse(EmployeeID));
        WCF_RequestID req = new WCF_RequestID("SUCCESS");
        return req;
    }

    public WCF_RequestID MobileGenReqNoSecond(String EmployeeID)
    {
        empNewRequest = new EmpNewRequest();
        WCF_RequestID req = new WCF_RequestID(empNewRequest.MobileGetReqNo(EmployeeID));
        return req;
    }

    public WCF_RequestItems MobileGetDetails(String ItemID)
    {
        empNewRequest = new EmpNewRequest();
        Item item = empNewRequest.MobileGetDetails(ItemID);
        WCF_RequestItems req = new WCF_RequestItems(item.Description,0 , item.UnitOfMeasurement);
        return req;
    }

    public List<WCF_RequestItems> MobileAddItem(String RequestID, String ItemID, String Quantity)
    {
        empNewRequest = new EmpNewRequest();
        List<WCF_RequestItems> eWCFList = new List<WCF_RequestItems>();
        List<EmpNewReqClass> eList = empNewRequest.MobileAddItem(RequestID, ItemID, int.Parse(Quantity));

        foreach (EmpNewReqClass emp in eList)
        {
            eWCFList.Add(new WCF_RequestItems(emp.Description, (int)emp.RequestedQty, emp.UnitOfMeasurement));
        }

        return eWCFList;
    }

    public WCF_RMessage DeleteItem(String ItemID, String RequestID)
    {
        empNewRequest = new EmpNewRequest();
        WCF_RMessage req = new WCF_RMessage(empNewRequest.DeleteItem(ItemID, int.Parse(RequestID)));
        return req;
    }

    public WCF_RMessage MobileSaveReqNo(String RequestID)
    {
        empNewRequest = new EmpNewRequest();
        WCF_RMessage req = new WCF_RMessage(empNewRequest.MobileSaveReqNo(RequestID));
        return req;
    }

    public List<WCF_RequestItemsNew> GridMobViewEmpNewReq(String RequestID)
    {
        empNewRequest = new EmpNewRequest();
        List<WCF_RequestItemsNew> eWCFList = new List<WCF_RequestItemsNew>();
        List<EmpNewReqClass> eList = empNewRequest.GridMobViewEmpNewReq(int.Parse(RequestID));

        foreach (EmpNewReqClass emp in eList)
        {
            eWCFList.Add(new WCF_RequestItemsNew(emp.RequestedItem, emp.Description, (int)emp.RequestedQty, emp.UnitOfMeasurement));
        }

        return eWCFList;
    }

    public List<WCF_Employee> GetEmployeeNameList(String DepartmentID)
    {
        approveRejectRequestController = new ApproveRejectRequestController();
        List<WCF_Employee> eWCFList = new List<WCF_Employee>();
        List<Employee> eList = approveRejectRequestController.getEmployeeNameList(DepartmentID);

        foreach (Employee emp in eList)
        {
            eWCFList.Add(new WCF_Employee((int)emp.EmployeeID, emp.EmployeeName, emp.EmployeePassword, emp.EmployeeContactNo, emp.EmployeeEmail, emp.EmployeeAddress, (int)emp.RoleID, emp.DepartmentID));
        }
        return eWCFList;
    }

    public List<WCF_Employee> GetEmployeeNameListForAppointment(String DepartmentID)
    {
        appointRepresentativeController = new AppointRepresentativeController();
        List<WCF_Employee> eWCFList = new List<WCF_Employee>();
        List<Employee> eList = appointRepresentativeController.getEmployeeList(DepartmentID);

        foreach (Employee emp in eList)
        {
            eWCFList.Add(new WCF_Employee((int)emp.EmployeeID, emp.EmployeeName, emp.EmployeePassword, emp.EmployeeContactNo, emp.EmployeeEmail, emp.EmployeeAddress, (int)emp.RoleID, emp.DepartmentID));
        }
        return eWCFList;
    }


    public WCF_RequestID GetRequestIDByEmployeeID(String EmployeeID)
    {
        approveRejectRequestController = new ApproveRejectRequestController();
        WCF_RequestID req = new WCF_RequestID(approveRejectRequestController.getRequestIDByEmployeeID(EmployeeID));
            
        return req;
    }

    public WCF_RMessage updateRequestStatusToApprove(String RequestID, String Status, String ApprovedByEmployeeID, String Remark)
    {
        approveRejectRequestController = new ApproveRejectRequestController();
        WCF_RMessage req = new WCF_RMessage(approveRejectRequestController.updateRequestStatusToApprove(int.Parse(RequestID), Status, int.Parse(ApprovedByEmployeeID), Remark));

        return req;

    }

    public WCF_DelicatedInfo GetDelegatedInfo(String DepartmentID)
    {
        delegateAuthorityController = new DelegateAuthorityController();
        CancellationOfAuthorityObject d = delegateAuthorityController.GetDelegatedInfo(DepartmentID);

        WCF_DelicatedInfo wcf_q = new WCF_DelicatedInfo(d.DelicatedInfoID,d.EmployeeName);
     
        return wcf_q;

    }

    public WCF_RMessage DeleteDelegatedInfo(String DelicatedInfoID)
    {
        delegateAuthorityController = new DelegateAuthorityController();
        WCF_RMessage req = new WCF_RMessage(delegateAuthorityController.DeleteDelecatedInfo(DelicatedInfoID));

        return req;
    }

    public WCF_RMessage delegateAuthorityMobile(String EmployeeID, String FromDate, String ToDate)
    {
        delegateAuthorityController = new DelegateAuthorityController();
        DateTime FDate;
        DateTime TDate;

        FromDate = FromDate.ToString().Trim().Replace("-", "/");
        ToDate = ToDate.ToString().Trim().Replace("-", "/");

        FDate = Convert.ToDateTime(FromDate);
        TDate = Convert.ToDateTime(ToDate);

        bool ret = delegateAuthorityController.delegateAuthorityMobile(int.Parse(EmployeeID), FDate, TDate);

        WCF_RMessage req;
        if (ret == true)
            req = new WCF_RMessage("SUCCESS");
        else
            req = new WCF_RMessage("FAIL");
        return req;
    }

    public WCF_Employee GetEmployeeIDByEmployeeName(string EmployeeName)
    {
        loginController = new LoginController();
       
        Employee emp = loginController.GetEmployeeIDByEmployeeName(EmployeeName);

        return new WCF_Employee((int)emp.EmployeeID, emp.EmployeeName, emp.EmployeePassword, emp.EmployeeContactNo, emp.EmployeeEmail, emp.EmployeeAddress, (int)emp.RoleID, emp.DepartmentID);
    }

    public List<WCF_RequestItems> GetRequestItems(string RequestID)
    {
        approveRejectRequestController = new ApproveRejectRequestController();
        List<WCF_RequestItems> rWCFList = new List<WCF_RequestItems>();
        List<ApproveRejectRequestObject> rList = approveRejectRequestController.getRequestItems(RequestID);

        foreach (ApproveRejectRequestObject req in rList)
        {
            rWCFList.Add(new WCF_RequestItems(req.Description, (int)req.Quantity, req.UnitOfMeasurement));
        }
        return rWCFList;
    }

    public List<WCF_ProcessReq> GetRequests()
    {

        processReqController = new ProcessReqSC();
        List<WCF_ProcessReq> wcf_reqList = new List<WCF_ProcessReq>();
        WCF_ProcessReq req;
        List<ProcessReqMobile> hhList = processReqController.MobileGrid();

        foreach (ProcessReqMobile i in hhList)
        {
            req = new WCF_ProcessReq(i.itemid, i.description, (int)i.balance, i.uom, (int)i.tneeded, (int)i.talloted);
            wcf_reqList.Add(req);
        }
        return wcf_reqList;
    }

    public List<WCF_ProcessReq> GetUpdateRequests(string itemid, string damageqty, string empID)
    {

        processReqController = new ProcessReqSC();
        List<WCF_ProcessReq> wcf_reqList = new List<WCF_ProcessReq>();
        WCF_ProcessReq req;

        List<ProcessReqMobile> hhList = processReqController.MobileReq(itemid, damageqty, empID);

        foreach (ProcessReqMobile i in hhList)
        {
            req = new WCF_ProcessReq(i.itemid, i.description, (int)i.balance, i.uom, (int)i.tneeded, (int)i.talloted);
            wcf_reqList.Add(req);
        }
        return wcf_reqList;
    }

    public List<WCF_CollectionPoints> GetCollectionPoints(string empids)
    {
        disbursementController = new Disbursement();
        List<WCF_CollectionPoints> cWCFList = new List<WCF_CollectionPoints>();
        WCF_CollectionPoints point;
        List<CollectionPoint> cList = disbursementController.CollectRadio(empids);
        foreach (CollectionPoint c in cList)
        {
            point = new WCF_CollectionPoints((int)c.CollectionPointID, c.Place, c.Time.ToString(), c.InCharge.ToString());
            cWCFList.Add(point);
        }
        return cWCFList;
    }

    public List<WCF_Employee> GetDeptRepListForCollectionPoint(string cptds)
    {

            disbursementController = new Disbursement();
            List<WCF_Employee> eWCFList = new List<WCF_Employee>();
            WCF_Employee e;
            List<Employee> eList = disbursementController.DisburseGrid(cptds);
            foreach (Employee emp in eList)
            {
                e = new WCF_Employee(emp.DepartmentID, emp.EmployeeName ,emp.EmployeeEmail);
                eWCFList.Add(e);
            }
            return eWCFList;
    }

    public List<WCF_DisbursementInfo> DisburseDepartGrid(string deptid)
    {

        disbursementController = new Disbursement();
        List<WCF_DisbursementInfo> dWCFList = new List<WCF_DisbursementInfo>();
        WCF_DisbursementInfo dinfo;
        List<Disbursementclass> dList = disbursementController.DisburseDepartGrid(deptid);
        foreach (Disbursementclass d in dList)
        {
            dinfo = new WCF_DisbursementInfo(d.description,(int)d.requestedqty,(int)d.recievedqty,(int)d.outstandingqty,d.UnitOfMeasurement);
            dWCFList.Add(dinfo);
        }
        return dWCFList;
    }

    public int updateDisburse(string deptid,string empID)
    {
        disbursementController = new Disbursement();
        return disbursementController.updateDisburse(deptid, empID);
    }

    public int getLowLevelStockQty()
    {
        storeClerkHomeController = new StoreClerkHomeController();
        return storeClerkHomeController.displayLowLevelStockQty();
    }

    public List<WCF_DisplayLowLevelStock_View> DisplayLowLevelStock()
    {
        storeClerkHomeController = new StoreClerkHomeController();
        List<WCF_DisplayLowLevelStock_View> dWCFList = new List<WCF_DisplayLowLevelStock_View>();
        WCF_DisplayLowLevelStock_View dinfo;
        List<DisplayLowLevelStock_View> dList = storeClerkHomeController.displayLowLevelStock();
        foreach (DisplayLowLevelStock_View d in dList)
        {
            dinfo = new WCF_DisplayLowLevelStock_View(d.ItemID, d.CategoryName, d.Description, (int)d.Balance, (int)d.ReorderLevel, (int)d.ReorderQuantity, (int)d.SuggestedQuantity,d.UnitOfMeasurement);
            dWCFList.Add(dinfo);
        }
        return dWCFList;
    }

    public List<WCF_AdjVoucherNumber> getVoucherNumberMobile(string roleID)
    {
        approveAdjustmentController = new ApproveAdjustmentController();
        List<WCF_AdjVoucherNumber> vWCFList = new List<WCF_AdjVoucherNumber>();
        WCF_AdjVoucherNumber wcfAdjNo;
        List<AdjVoucherNumber> vouList = approveAdjustmentController.getVoucherNumberMobile(roleID);
        foreach (AdjVoucherNumber vouNo in vouList)
        {
            wcfAdjNo = new WCF_AdjVoucherNumber(vouNo.voucherNumber);
            vWCFList.Add(wcfAdjNo);
        }
        return vWCFList;
    }

    public List<WCF_ApproveAdjustmentMobile> getAdjustmentListMobile(string adjID)
    {

        approveAdjustmentController = new ApproveAdjustmentController();
        List<WCF_ApproveAdjustmentMobile> adjWCFList = new List<WCF_ApproveAdjustmentMobile>();
        WCF_ApproveAdjustmentMobile adjInfo;
        List<ApproveAdjustmentMobile> adjList = approveAdjustmentController.getAdjustmentListMobile(adjID);
        foreach (ApproveAdjustmentMobile d in adjList)
        {
            adjInfo = new WCF_ApproveAdjustmentMobile(d.description, (int)d.balance, d.uom, (decimal)d.unitPrice, (decimal)d.amount, (decimal)d.totalPrice, d.remark);
            adjWCFList.Add(adjInfo);
        }
        return adjWCFList;
    }

    public WCF_RMessage approvedAdjustmentListMobile(string AdjustID, string empID)
    {
        approveAdjustmentController = new ApproveAdjustmentController();
        return new WCF_RMessage(approveAdjustmentController.approvedAdjustmentListMobile(AdjustID, empID).ToString());
    }

    public List<WCF_PurchaseOrder> MgetPurchaseOrderID()
    {

        receivedOrderController = new ReceivedOrderController();
        List<WCF_PurchaseOrder> poWCFList = new List<WCF_PurchaseOrder>();
        WCF_PurchaseOrder poInfo;
        List<PurchaseOrder> poList = receivedOrderController.MgetPurchaseOrderID();
        foreach (PurchaseOrder po in poList)
        {
            String test = po.ReceivedDate.ToString();
            //poInfo = new WCF_PurchaseOrder(po.PurchaseOrderID, po.PurchaseStatus,(int) po.SupplierID, (int)po.EmployeeID, (int)po.ReceivedByEmployeeID, po.DeliverVoucherNo, po.OrderDate.ToString(), po.ExpectedDeliveryDate.ToString(), po.ReceivedDate.ToString());
            poInfo = new WCF_PurchaseOrder(po.PurchaseOrderID, po.PurchaseStatus, (int)po.SupplierID, (int)po.EmployeeID, po.OrderDate.ToString(), po.ExpectedDeliveryDate.ToString());
            poWCFList.Add(poInfo);
        }
        return poWCFList;
    }

    public List<WCF_PurchaseOrderDetail> MgetPurchaseOrderListbyPOid(string POID)
    {

        receivedOrderController = new ReceivedOrderController();
        List<WCF_PurchaseOrderDetail> poWCFList = new List<WCF_PurchaseOrderDetail>();
        WCF_PurchaseOrderDetail poInfo;
        List<MobGetPurchaseOrderClass> poList = receivedOrderController.MgetPurchaseOrderListbyPOid(POID);
        foreach (MobGetPurchaseOrderClass po in poList)
        {
            //poInfo = new WCF_PurchaseOrder(po.PurchaseOrderID, po.PurchaseStatus,(int) po.SupplierID, (int)po.EmployeeID, (int)po.ReceivedByEmployeeID, po.DeliverVoucherNo, po.OrderDate.ToString(), po.ExpectedDeliveryDate.ToString(), po.ReceivedDate.ToString());
            poInfo = new WCF_PurchaseOrderDetail(po.ItemID, po.Description, po.UnitOfMeasurement, (int)po.OrderQty, po.OrderDate, po.ExpectedDeliveryDate);
            poWCFList.Add(poInfo);
        }
        return poWCFList;
    }

    public string checkItemForReceivePO(string POID1, string itemid)
    {
        receivedOrderController = new ReceivedOrderController();
        return receivedOrderController.McheckItem( POID1,  itemid);
    }

    public List<WCF_PurchaseOrderDetail2> MupdatePurchaseOrderDetail(string POID, string itemID, string qty1, string remark)
    {

        receivedOrderController = new ReceivedOrderController();
        List<WCF_PurchaseOrderDetail2> poWCFList = new List<WCF_PurchaseOrderDetail2>();
        WCF_PurchaseOrderDetail2 poInfo;
        List<MobGetPurchaseOrderClass2> poList = receivedOrderController.MupdatePurchaseOrderDetail(POID, itemID, qty1, remark);
        foreach (MobGetPurchaseOrderClass2 po in poList)
        {
            //poInfo = new WCF_PurchaseOrder(po.PurchaseOrderID, po.PurchaseStatus,(int) po.SupplierID, (int)po.EmployeeID, (int)po.ReceivedByEmployeeID, po.DeliverVoucherNo, po.OrderDate.ToString(), po.ExpectedDeliveryDate.ToString(), po.ReceivedDate.ToString());
            poInfo = new WCF_PurchaseOrderDetail2(po.ItemID, po.Description, po.UnitOfMeasurement, (int)po.OrderQty,  po.OrderDate, po.ExpectedDeliveryDate,(int)po.ReceivedQty);
            poWCFList.Add(poInfo);
        }
        return poWCFList;
    }

    public string MupdatePurchaseOrder(string POID1, string empid1, string DVNo)
    {
        receivedOrderController = new ReceivedOrderController();
        return receivedOrderController.MupdatePurchaseOrder(POID1, empid1, DVNo);
    }

    //public string GetData(int value)
    //{
    //    return string.Format("You entered: {0}", value);
    //}

    //public CompositeType GetDataUsingDataContract(CompositeType composite)
    //{
    //    if (composite == null)
    //    {
    //        throw new ArgumentNullException("composite");
    //    }
    //    if (composite.BoolValue)
    //    {
    //        composite.StringValue += "Suffix";
    //    }
    //    return composite;
    //}

}
