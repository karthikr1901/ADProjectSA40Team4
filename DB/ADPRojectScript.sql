CREATE DATABASE ADProjectSA40Team4

USE ADProjectSA40Team4
CREATE TABLE Supplier(
SupplierID int Identity(1,1) not null,
SupplierName nvarchar(50) not null,
SupplierContactNo nvarchar(11) not null,
SupplierEmail nvarchar(50),
SupplierAddress nvarchar(100)
CONSTRAINT pk_SupplierID PRIMARY KEY (SupplierID)
)

CREATE TABLE Role(
RoleID int Identity(1,1) not null,
RoleName nvarchar(50) not null
CONSTRAINT pk_RoleID PRIMARY KEY (RoleID)
)

CREATE TABLE Employee(
EmployeeID int Identity(1,1) not null,
EmployeeName nvarchar(50) not null,
EmployeePassword nvarchar(100) not null,
EmployeeContactNo nvarchar(11) not null,
EmployeeEmail nvarchar(50),
EmployeeAddress nvarchar(100),
RoleID int,
DepartmentID nvarchar(10),
CONSTRAINT pk_EmployeeID PRIMARY KEY (EmployeeID),
CONSTRAINT fk_EmpRoleID FOREIGN KEY (RoleID) REFERENCES Role(RoleID) ON DELETE NO ACTION
)

CREATE TABLE DelicatedInfo (
DelicatedInfoID int Identity(1,1) not null,
EmployeeID int,
fromDate Datetime,
toDate Datetime,
CONSTRAINT pk_DelicatedInfoID PRIMARY KEY (DelicatedInfoID),
CONSTRAINT fk_DInfoEmployeeID FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID) ON DELETE NO ACTION
)

CREATE TABLE Control(
Prefix nvarchar(3) not null UNIQUE,
Length nvarchar(10) not null,
LastUpdateDate Datetime
CONSTRAINT ck_PrefixLength PRIMARY KEY (Prefix,Length)
)



CREATE TABLE CollectionPoint(
CollectionPointID int IDENTITY(1,1) not null,
Place nvarchar(100) not null,
Time nvarchar(20) not null,
InCharge int,
CONSTRAINT pk_CollectionPointID PRIMARY KEY (CollectionPointID),
CONSTRAINT fk_InCharge FOREIGN KEY (InCharge) REFERENCES Employee(EmployeeID) ON DELETE NO ACTION
)


CREATE TABLE Department(
DepartmentID nvarchar(10) unique not null,
DepartmentName nvarchar(50) not null,
CollectionPointID int,
CONSTRAINT pk_DepartmentID PRIMARY KEY (DepartmentID),
CONSTRAINT fk_DeptCollectionPointID FOREIGN KEY (CollectionPointID) REFERENCES CollectionPoint(CollectionPointID) ON DELETE NO ACTION
)

CREATE TABLE Category(
CategoryID int Identity(1,1) not null,
CategoryName nvarchar(50) not null,
Location nvarchar(15),
CONSTRAINT pK_CategoryID PRIMARY KEY (CategoryID)
)

CREATE TABLE Item(
ItemID nvarchar(10) unique not null,
Description nvarchar(150),
ReorderLevel int not null,
ReorderQuantity int not null,
UnitOfMeasurement nvarchar(10) not null,
Balance int,
SuggestedQuantity int,
CategoryID int,
CONSTRAINT pk_ItemID PRIMARY KEY (ItemID),
CONSTRAINT fk_ItemCategoryID FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID) ON DELETE NO ACTION
)



CREATE TABLE SupplyItem(
SupplierID int not null,
ItemID nvarchar(10) not null,
Price decimal not null,
Priority int not null,
CONSTRAINT ck_SupplierIDItemID PRIMARY KEY (SupplierID,ItemID),
CONSTRAINT fk_SupplierID FOREIGN KEY (SupplierID) REFERENCES Supplier (SupplierID) ON DELETE NO ACTION,
CONSTRAINT fk_SupplyItemID FOREIGN KEY (ItemID) REFERENCES Item (ItemID) ON DELETE NO ACTION
)

CREATE TABLE Request (
RequestID int Identity(1,1) not null,
RequestStatus nvarchar(15),
RequestByEmployeeID int,
RequestByDepartmentID nvarchar(10),
ApprovedByEmployeeID int,
ProcessedByEmployeeID int,
ReceivedByEmployeeID int,
RequestDate Datetime,
ApproveDate Datetime,
ReceivedDate Datetime,
Remark nvarchar(150)
CONSTRAINT pk_RequestID PRIMARY KEY (RequestID),
CONSTRAINT fk_ReqRequestByEmployeeID FOREIGN KEY (RequestByEmployeeID) REFERENCES Employee (EmployeeID) ON DELETE NO ACTION,
CONSTRAINT fk_ReqRequestByDepartmentID FOREIGN KEY (RequestByDepartmentID) REFERENCES Department (DepartmentID) ON DELETE NO ACTION,
CONSTRAINT fk_ReqApprovedByEmployeeID FOREIGN KEY (ApprovedByEmployeeID) REFERENCES Employee (EmployeeID) ON DELETE NO ACTION,
CONSTRAINT fk_ReqProcessedByEmployeeID FOREIGN KEY (ProcessedByEmployeeID) REFERENCES Employee (EmployeeID) ON DELETE NO ACTION,
CONSTRAINT fk_ReqReceivedByEmployeeID FOREIGN KEY (ReceivedByEmployeeID) REFERENCES Employee (EmployeeID) ON DELETE NO ACTION
)

CREATE TABLE RequestDetail(
RequestDetailID int Identity(1,1) not null,
RequestID int,
RequestedItem nvarchar(10),
RequestedQty int,
ReceivedQty int
CONSTRAINT pk_RequestDetailID PRIMARY KEY (RequestDetailID)
CONSTRAINT fk_ReqDetailRequestID FOREIGN KEY (RequestID) REFERENCES Request (RequestID) ON DELETE NO ACTION,
CONSTRAINT fk_ReqDetailRequestedItem FOREIGN KEY (RequestedItem) REFERENCES Item (ItemID) ON DELETE NO ACTION
)

CREATE TABLE Adjustment (
AdjustmentID int Identity(1,1) not null,
AdjustmentStatus nvarchar(15),
AdjustedByEmployeeID int,
ApprovedByEmployeeID int,
RequestAdjustmentDate Datetime,
ApproveAdjustmentDate Datetime,
TotalPrice decimal
CONSTRAINT pk_AdjustmentID PRIMARY KEY (AdjustmentID),
CONSTRAINT fk_AdjustedByEmployeeID FOREIGN KEY (AdjustedByEmployeeID) REFERENCES Employee(EmployeeID) ON DELETE NO ACTION,
CONSTRAINT fk_ApprovedByEmployeeID FOREIGN KEY (ApprovedByEmployeeID) REFERENCES Employee(EmployeeID) ON DELETE NO ACTION
)

CREATE TABLE AdjustmentDetail (
AdjustmentDetailID int Identity(1,1) not null,
ItemID nvarchar(10),
AdjustmentID int,
Quantity int,
AdjustmentRemark nvarchar(150),
CONSTRAINT pk_AdjustmentDetailID PRIMARY KEY (AdjustmentDetailID),
CONSTRAINT fk_AdjDetailItemID FOREIGN KEY (ItemID) REFERENCES Item (ItemID) ON DELETE NO ACTION,
CONSTRAINT fk_AdjDetailAdjustmentID FOREIGN KEY (AdjustmentID) REFERENCES Adjustment (AdjustmentID) ON DELETE NO ACTION
)

CREATE TABLE PurchaseOrder (
PurchaseOrderID int Identity(1,1) not null,
PurchaseStatus nvarchar(15),
SupplierID int,
EmployeeID int,
ReceivedByEmployeeID int,
DeliverVoucherNo nvarchar(10),
OrderDate Datetime,
ExpectedDeliveryDate Datetime,
ReceivedDate Datetime
CONSTRAINT pk_PurchaseOrderID PRIMARY KEY (PurchaseOrderID),
CONSTRAINT fk_POSupplierID FOREIGN KEY (SupplierID) REFERENCES Supplier (SupplierID),
CONSTRAINT fk_POEmployeeID FOREIGN KEY (EMPLOYEEID) REFERENCES Employee (EmployeeID)
)

CREATE TABLE PurchaseOrderDetail(
PurchaseOrderDetailID int Identity(1,1) not null,
PurchaseOrderID int,
ItemID nvarchar(10),
OrderQty int,
ReceivedQty int,
Remark nvarchar(100)
CONSTRAINT pk_PurchaseOrderDetailID PRIMARY KEY (PurchaseOrderDetailID),
CONSTRAINT fk_PODItemID FOREIGN KEY (ItemID) REFERENCES Item(ItemID),
CONSTRAINT fk_PurchaseOrderID FOREIGN KEY (PurchaseOrderID) REFERENCES PurchaseOrder (PurchaseOrderID)
)
