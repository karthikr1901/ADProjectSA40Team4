using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ADTeam4EF;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{
    [OperationContract]
    [WebGet(UriTemplate = "/GetAuthenticateUser/{email}/{hashedPassword}", ResponseFormat = WebMessageFormat.Json)]
    WCF_Employee GetAuthenticateUser(string email, string hashedPassword);

    [OperationContract]
    [WebGet(UriTemplate = "/GetRepresentative/{DepartmentID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_Employee GetRepresentative(string DepartmentID);

    [OperationContract]
    [WebGet(UriTemplate = "/appointNewRepresentativeMobile/{EmployeeID}/{DepartmentID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_RMessage appointNewRepresentativeMobile(String EmployeeID, String DepartmentID);

    [OperationContract]
    [WebGet(UriTemplate = "/MobileGenReqNoFirst/{EmployeeID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_RequestID MobileGenReqNoFirst(String EmployeeID);

    [OperationContract]
    [WebGet(UriTemplate = "/MobileGenReqNoSecond/{EmployeeID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_RequestID MobileGenReqNoSecond(String EmployeeID);

    [OperationContract]
    [WebGet(UriTemplate = "/DeleteItem/{ItemID}/{RequestID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_RMessage DeleteItem(String ItemID, String RequestID);

    [OperationContract]
    [WebGet(UriTemplate = "/MobileSaveReqNo/{RequestID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_RMessage MobileSaveReqNo(String RequestID);

    [OperationContract]
    [WebGet(UriTemplate = "/GridMobViewEmpNewReq/{RequestID}", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_RequestItemsNew> GridMobViewEmpNewReq(String RequestID);
    
    [OperationContract]
    [WebGet(UriTemplate = "/MobileGetDetails/{ItemID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_RequestItems MobileGetDetails(String ItemID);

    [OperationContract]
    [WebGet(UriTemplate = "/MobileAddItem/{RequestID}/{ItemID}/{Quantity}", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_RequestItems> MobileAddItem(String RequestID, String ItemID, String Quantity);

    //[OperationContract]
    //[WebGet(UriTemplate = "/GetAll", ResponseFormat = WebMessageFormat.Json)]
    //List<WCF_Employee> GetAll();

    [OperationContract]
    [WebGet(UriTemplate = "/GetEmployeeNameList/{DepartmentID}", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_Employee> GetEmployeeNameList(String DepartmentID);

    [OperationContract]
    [WebGet(UriTemplate = "/GetEmployeeNameListForAppointment/{DepartmentID}", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_Employee> GetEmployeeNameListForAppointment(String DepartmentID);


    [OperationContract]
    [WebGet(UriTemplate = "/GetRequestIDByEmployeeID/{EmployeeID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_RequestID GetRequestIDByEmployeeID(string EmployeeID);

    [OperationContract]
    [WebGet(UriTemplate = "/GetEmployeeIDByEmployeeName/{EmployeeName}", ResponseFormat = WebMessageFormat.Json)]
    WCF_Employee GetEmployeeIDByEmployeeName(string EmployeeName);

    [OperationContract]
    [WebGet(UriTemplate = "/GetRequestItems/{RequestID}", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_RequestItems> GetRequestItems(string RequestID);

    [OperationContract]
    [WebGet(UriTemplate = "/updateRequestStatusToApprove/{RequestID}/{Status}/{ApprovedByEmployeeID}/{Remark}", ResponseFormat = WebMessageFormat.Json)]
    WCF_RMessage updateRequestStatusToApprove(string RequestID, string Status, string ApprovedByEmployeeID, string Remark);

    [OperationContract]
    [WebGet(UriTemplate = "/delegateAuthorityMobile/{EmployeeID}/{FromDate}/{ToDate}", ResponseFormat = WebMessageFormat.Json)]
    WCF_RMessage delegateAuthorityMobile(string EmployeeID, string FromDate, string ToDate);

    [OperationContract]
    [WebGet(UriTemplate = "/GetDelegatedInfo/{DepartmentID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_DelicatedInfo GetDelegatedInfo(String DepartmentID);

    [OperationContract]
    [WebGet(UriTemplate = "/DeleteDelegatedInfo/{DelicatedInfoID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_RMessage DeleteDelegatedInfo(String DelicatedInfoID);

    [OperationContract]
    [WebGet(UriTemplate = "/GetRequests", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_ProcessReq> GetRequests();

    [OperationContract]
    [WebGet(UriTemplate = "/GetUpdateRequests/{itemid}/{damageqty}/{eid1}", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_ProcessReq> GetUpdateRequests(string itemid, string damageqty, string eid1);

    [OperationContract]
    [WebGet(UriTemplate = "/GetCollectionPoints/{empids}", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_CollectionPoints> GetCollectionPoints(string empids);

    [OperationContract]
    [WebGet(UriTemplate = "/GetDeptRepListForCollectionPoint/{cptds}", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_Employee> GetDeptRepListForCollectionPoint(string cptds);

    [OperationContract]
    [WebGet(UriTemplate = "/DisburseDepartGrid/{deptid}", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_DisbursementInfo> DisburseDepartGrid(string deptid);

    //[OperationContract]
    //[WebInvoke(UriTemplate = "/updateDisburse/{deptid}", Method = "POST",
    //    ResponseFormat = WebMessageFormat.Json)]
    //int updateDisburse(string deptid);

    [OperationContract]
    [WebGet(UriTemplate = "/updateDisburse/{deptid}/{empID}", ResponseFormat = WebMessageFormat.Json)]
    int updateDisburse(string deptid,string empID);

    [OperationContract]
    [WebGet(UriTemplate = "/getLowLevelStockQty", ResponseFormat = WebMessageFormat.Json)]
    int getLowLevelStockQty();

    [OperationContract]
    [WebGet(UriTemplate = "/DisplayLowLevelStock", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_DisplayLowLevelStock_View> DisplayLowLevelStock();

    [OperationContract]
    [WebGet(UriTemplate = "/getVoucherNumberMobile/{roleID}", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_AdjVoucherNumber> getVoucherNumberMobile(string roleID);

    [OperationContract]
    [WebGet(UriTemplate = "/getAdjustmentListMobile/{adjID}", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_ApproveAdjustmentMobile> getAdjustmentListMobile(string adjID);

    [OperationContract]
    [WebGet(UriTemplate = "/approvedAdjustmentListMobile/{AdjustID}/{empID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_RMessage approvedAdjustmentListMobile(string AdjustID, string empID);

    [OperationContract]
    [WebGet(UriTemplate = "/MgetPurchaseOrderID", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_PurchaseOrder> MgetPurchaseOrderID();

    [OperationContract]
    [WebGet(UriTemplate = "/MgetPurchaseOrderListbyPOid/{POID}", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_PurchaseOrderDetail> MgetPurchaseOrderListbyPOid(string POID);

    [OperationContract]
    [WebGet(UriTemplate = "/checkItemForReceivePO/{POID1}/{itemid}", ResponseFormat = WebMessageFormat.Json)]
    string checkItemForReceivePO(string POID1, string itemid);

    [OperationContract]
    [WebGet(UriTemplate = "/MupdatePurchaseOrderDetail/{POID}/{itemID}/{qty1}/{remark}", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_PurchaseOrderDetail2> MupdatePurchaseOrderDetail(string POID, string itemID, string qty1, string remark);

   // string POID1, string itemID, string qty1, string remark

    [OperationContract]
    [WebGet(UriTemplate = "/MupdatePurchaseOrder/{POID1}/{empid1}/{DVNo}", ResponseFormat = WebMessageFormat.Json)]
    string MupdatePurchaseOrder(string POID1, string empid1, string DVNo);


    //[OperationContract]
    //string GetData(int value);

    //[OperationContract]
    //CompositeType GetDataUsingDataContract(CompositeType composite);

    //// TODO: Add your service operations here
}

#region WCF_DelicatedInfo

[DataContract]
public class WCF_DelicatedInfo
{
    [DataMember]
    public int delicatedInfoID;

    [DataMember]
    public string employeeName;

    public WCF_DelicatedInfo(int DelicatedInfoID, string EmployeeName)
    {
        this.delicatedInfoID = DelicatedInfoID;
        this.employeeName = EmployeeName;
    }
}

#endregion

#region WCF_PurchaseOrder

[DataContract]
public class WCF_PurchaseOrder
{

    [DataMember]
    public int PurchaseOrderID;

    [DataMember]
    public string Description;

    [DataMember]
    public int SupplierID;

    [DataMember]
    public int EmployeeID;

    [DataMember]
    public string OrderDate;

    [DataMember]
    public string ExpectedDeliveryDate;

    public WCF_PurchaseOrder(int PurchaseOrderID, string Description, int SupplierID, int EmployeeID, string OrderDate, string ExpectedDeliveryDate)
    {
        this.PurchaseOrderID = PurchaseOrderID;
        this.Description = Description;
        this.SupplierID = SupplierID;
        this.EmployeeID = EmployeeID;
        this.OrderDate = OrderDate;
        this.ExpectedDeliveryDate = ExpectedDeliveryDate;
    }

    //public WCF_PurchaseOrder(int PurchaseOrderID, string PurchaseStatus, int SupplierID, int EmployeeID, int ReceivedByEmployeeID, string DeliverVoucherNo, string OrderDate, string ExpectedDeliveryDate, string ReceivedDate)
    //{
    //    this.PurchaseOrderID = PurchaseOrderID;
    //    this.PurchaseStatus = PurchaseStatus;
    //    this.SupplierID = SupplierID;
    //    this.EmployeeID = EmployeeID;
    //    this.ReceivedByEmployeeID = ReceivedByEmployeeID;
    //    this.DeliverVoucherNo = DeliverVoucherNo;
    //    this.OrderDate = OrderDate;
    //    this.ExpectedDeliveryDate = ExpectedDeliveryDate;
    //    this.ReceivedDate = ReceivedDate;
    //}
}

#endregion

#region WCF_PurchaseOrderDetail

[DataContract]
public class WCF_PurchaseOrderDetail
{

    [DataMember]
    public string ItemID;

    [DataMember]
    public string Description;

    [DataMember]
    public string UOM;

    [DataMember]
    public int OrderQty;

    [DataMember]
    public string OrderDate;

    [DataMember]
    public string ExpectedDeliveryDate;

    public WCF_PurchaseOrderDetail(string ItemID, string Description, string UOM, int OrderQty, string OrderDate, string ExpectedDeliveryDate)
    {
        this.ItemID = ItemID;
        this.Description = Description;
        this.UOM = UOM;
        this.OrderQty = OrderQty;
        this.OrderDate = OrderDate;
        this.ExpectedDeliveryDate = ExpectedDeliveryDate;
    }

    //public WCF_PurchaseOrder(int PurchaseOrderID, string PurchaseStatus, int SupplierID, int EmployeeID, int ReceivedByEmployeeID, string DeliverVoucherNo, string OrderDate, string ExpectedDeliveryDate, string ReceivedDate)
    //{
    //    this.PurchaseOrderID = PurchaseOrderID;
    //    this.PurchaseStatus = PurchaseStatus;
    //    this.SupplierID = SupplierID;
    //    this.EmployeeID = EmployeeID;
    //    this.ReceivedByEmployeeID = ReceivedByEmployeeID;
    //    this.DeliverVoucherNo = DeliverVoucherNo;
    //    this.OrderDate = OrderDate;
    //    this.ExpectedDeliveryDate = ExpectedDeliveryDate;
    //    this.ReceivedDate = ReceivedDate;
    //}
}

#endregion

#region WCF_PurchaseOrderDetail2

[DataContract]
public class WCF_PurchaseOrderDetail2
{

    [DataMember]
    public string ItemID;

    [DataMember]
    public string Description;

    [DataMember]
    public string UOM;

    [DataMember]
    public int OrderQty;



    [DataMember]
    public string OrderDate;

    [DataMember]
    public string ExpectedDeliveryDate;

    [DataMember]
    public int receivedQty;

    public WCF_PurchaseOrderDetail2(string ItemID, string Description, string UOM, int OrderQty, string OrderDate, string ExpectedDeliveryDate, int receivedQty)
    {
        this.ItemID = ItemID;
        this.Description = Description;
        this.UOM = UOM;
        this.OrderQty = OrderQty;
       
        this.OrderDate = OrderDate;
        this.ExpectedDeliveryDate = ExpectedDeliveryDate;
        this.receivedQty = receivedQty;
    }

    //public WCF_PurchaseOrder(int PurchaseOrderID, string PurchaseStatus, int SupplierID, int EmployeeID, int ReceivedByEmployeeID, string DeliverVoucherNo, string OrderDate, string ExpectedDeliveryDate, string ReceivedDate)
    //{
    //    this.PurchaseOrderID = PurchaseOrderID;
    //    this.PurchaseStatus = PurchaseStatus;
    //    this.SupplierID = SupplierID;
    //    this.EmployeeID = EmployeeID;
    //    this.ReceivedByEmployeeID = ReceivedByEmployeeID;
    //    this.DeliverVoucherNo = DeliverVoucherNo;
    //    this.OrderDate = OrderDate;
    //    this.ExpectedDeliveryDate = ExpectedDeliveryDate;
    //    this.ReceivedDate = ReceivedDate;
    //}
}

#endregion

#region WCF_VoucherNumber

[DataContract]
public class WCF_AdjVoucherNumber
{
    [DataMember]
    public string voucherNumber;

    public WCF_AdjVoucherNumber(string voucherNumber)
    {
        this.voucherNumber = voucherNumber;
    }
}


#endregion

#region WCF_RMessage

[DataContract]
public class WCF_RMessage
{
    [DataMember]
    public string RMessage;

    public WCF_RMessage(string RMessage)
    {
        this.RMessage = RMessage;
    }
}
#endregion

#region DisplayLowLevelStock_View
[DataContract]
public class WCF_DisplayLowLevelStock_View
{
    [DataMember]
    public string ItemID;

    [DataMember]
    public string CategoryName;

    [DataMember]
    public string Description;

    [DataMember]
    public int Balance;

    [DataMember]
    public int ReorderLevel;

    [DataMember]
    public int ReorderQuantity;

    [DataMember]
    public int SuggestedQuantity;

    [DataMember]
    public string UnitOfMeasurement;

    public WCF_DisplayLowLevelStock_View(string ItemID, string CategoryName, string Description, int Balance, int ReorderLevel, int ReorderQuantity, int SuggestedQuantity, string UnitOfMeasurement)
    {
        this.ItemID = ItemID;
        this.CategoryName = CategoryName;
        this.Description = Description;
        this.Balance = Balance;
        this.ReorderLevel = ReorderLevel;
        this.ReorderQuantity = ReorderQuantity;
        this.SuggestedQuantity = SuggestedQuantity;
        this.UnitOfMeasurement = UnitOfMeasurement;
    }
}

#endregion

#region WCF_ApproveAdjustmentMobile

[DataContract]
public class WCF_ApproveAdjustmentMobile
{

    [DataMember]
    public string Description;

    [DataMember]
    public int Balance;

    [DataMember]
    public string UOM;

    [DataMember]
    public decimal UnitOfPrice;

    [DataMember]
    public decimal Amount;

    [DataMember]
    public decimal TotalPrice;

    [DataMember]
    public string Remark;

    public WCF_ApproveAdjustmentMobile(string Description, int Balance, string UOM, decimal UnitOfPrice, decimal Amount, decimal TotalPrice, string Remark)
    {
        this.Description = Description;
        this.Balance = Balance;
        this.UOM = UOM;
        this.UnitOfPrice = UnitOfPrice;
        this.Amount = Amount;
        this.TotalPrice = TotalPrice;
        this.Remark = Remark;
    }
}

#endregion

#region WCF_RequestItems

[DataContract]
public class WCF_RequestItems
{
    [DataMember]
    public string Description;

    [DataMember]
    public int Quantity;

    [DataMember]
    public string UnitOfMeasurement;

    public WCF_RequestItems(string Description, int Quantity, string UnitOfMeasurement)
    {
        this.Description = Description;
        this.Quantity = Quantity;
        this.UnitOfMeasurement = UnitOfMeasurement;
    }
}


#endregion

#region WCF_RequestItems

[DataContract]
public class WCF_RequestItemsNew
{
    [DataMember]
    public string ItemNo;

    [DataMember]
    public string Description;

    [DataMember]
    public int Quantity;

    [DataMember]
    public string UnitOfMeasurement;

    public WCF_RequestItemsNew(string ItemNo, string Description, int Quantity, string UnitOfMeasurement)
    {
        this.ItemNo = ItemNo;
        this.Description = Description;
        this.Quantity = Quantity;
        this.UnitOfMeasurement = UnitOfMeasurement;
    }
}


#endregion

#region WCF_RequestID

[DataContract]
public class WCF_RequestID
{
    [DataMember]
    public string RequestID;

    public WCF_RequestID(string RequestID)
    {
        this.RequestID = RequestID;
    }
}

#endregion

#region WCF_CollectionPoints
[DataContract]
public class WCF_CollectionPoints
{
    [DataMember]
    public int CollectionPointID;

    [DataMember]
    public string Place;

    [DataMember]
    public string Time;

    [DataMember]
    public string InCharge;

    public WCF_CollectionPoints(int CollectionPointID, string Place, string Time, string InCharge)
    {
        this.CollectionPointID = CollectionPointID;
        this.Place = Place;
        this.Time = Time;
        this.InCharge = InCharge;
    }
}

#endregion

#region WCF_Request

[DataContract]
public class WCF_Request
{
    [DataMember]
    public int RequestID;

    [DataMember]
    public string RequestStatus;

    [DataMember]
    public int RequestByEmployeeID;

    [DataMember]
    public string RequestByDepartmentID;

    [DataMember]
    public int ApprovedByEmployeeID;

    [DataMember]
    public int ProcessedByEmployeeID;

    [DataMember]
    public int ReceivedByEmployeeID;

    [DataMember]
    public DateTime RequestDate;

    [DataMember]
    public DateTime ApproveDate;

    [DataMember]
    public DateTime ReceivedDate;

    [DataMember]
    public string Remark;

    public WCF_Request(int RequestID, string RequestStatus, int RequestByEmployeeID, string RequestByDepartmentID, int ApprovedByEmployeeID, int ProcessedByEmployeeID, int ReceivedByEmployeeID, DateTime RequestDate, DateTime ApproveDate, DateTime ReceivedDate, string Remark)
    {
        this.RequestID = RequestID;
        this.RequestStatus = RequestStatus;
        this.RequestByEmployeeID = RequestByEmployeeID;
        this.RequestByDepartmentID = RequestByDepartmentID;

        this.ApprovedByEmployeeID = ApprovedByEmployeeID;
        this.ProcessedByEmployeeID = ProcessedByEmployeeID;

        this.ProcessedByEmployeeID = ReceivedByEmployeeID;
        this.RequestDate = RequestDate;
        this.ApproveDate = ApproveDate;
        this.ReceivedDate = ReceivedDate;
        this.Remark = Remark;
    }
}

#endregion

#region WCF_DisbursementInfo

[DataContract]
public class WCF_DisbursementInfo
{
    [DataMember]
    public string description;

    [DataMember]
    public int requestedqty;

    [DataMember]
    public int recievedqty;

    [DataMember]
    public int outstandingqty;

    [DataMember]
    public string UnitOfMeasurement;

    public WCF_DisbursementInfo(string description, int requestedqty, int recievedqty, int outstandingqty, string UnitOfMeasurement)
    {

        this.description = description;
        this.requestedqty = requestedqty;
        this.recievedqty = recievedqty;
        this.outstandingqty = outstandingqty;
        this.UnitOfMeasurement = UnitOfMeasurement;
    }
}

#endregion

#region WCF_Employee
[DataContract]
public class WCF_Employee
{
    [DataMember]
    public int EmployeeID;

    [DataMember]
    public string EmployeeName;

    [DataMember]
    public string EmployeePassword;

    [DataMember]
    public string EmployeeContactNo;

    [DataMember]
    public string EmployeeEmail;

    [DataMember]
    public string EmployeeAddress;

    [DataMember]
    public int RoleID;

    [DataMember]
    public string DepartmentID;

    public WCF_Employee(int employeeID, string employeeName, string employeePassword, string employeeContactNo, string employeeEmail, string employeeAddress, int roleID, string departmentID)
    {
        this.EmployeeID = employeeID;
        this.EmployeeName = employeeName;
        this.EmployeePassword = employeePassword;
        this.EmployeeContactNo = employeeContactNo;

        this.EmployeeEmail = employeeEmail;
        this.EmployeeAddress = employeeAddress;
        this.RoleID = roleID;
        this.DepartmentID = departmentID;
    }

    public WCF_Employee(string departmentID, string employeeName ,string employeeEmail)
    {
        this.EmployeeName = employeeName;
        this.DepartmentID = departmentID;
        this.EmployeeEmail = employeeEmail;
    }
}

#endregion

#region WCF_ProcessReq
[DataContract]
public class WCF_ProcessReq
{
    [DataMember]
    public string itemid;

    [DataMember]
    public string description;

    [DataMember]
    public int balance;

    [DataMember]
    public string uom;

    [DataMember]
    public int tneeded;

    [DataMember]
    public int talloted;

    public WCF_ProcessReq(string itemid, string description, int balance, string uom, int tneeded, int talloted)
    {
        this.itemid = itemid;
        this.description = description;
        this.balance = balance;
        this.uom = uom;
        this.tneeded = tneeded;
        this.talloted = talloted;
    }

#endregion

#region CompositeType
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
	    bool boolValue = true;
	    string stringValue = "Hello ";

	    [DataMember]
	    public bool BoolValue
	    {
		    get { return boolValue; }
		    set { boolValue = value; }
	    }

	    [DataMember]
	    public string StringValue
	    {
		    get { return stringValue; }
		    set { stringValue = value; }
	    }
    }

    #endregion

}


