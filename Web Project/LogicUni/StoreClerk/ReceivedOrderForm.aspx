<%@ Page Title="Received Order Form" Language="C#" MasterPageFile="~/StoreClerk.master" AutoEventWireup="true" CodeFile="ReceivedOrderForm.aspx.cs" Inherits="ReceivedOrderForm" %>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-body">
        <div class="container">
            <h2 style="text-align: center">Received Order Form</h2>
            <h3 id="noOrder" class="text-primary" runat="server" style="text-align: center">There is no pending orders now</h3>
        </div>
        <div class="assignRepClass">
            <div class="col-md-4">
                <div id="ReceiveForm" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center">Receive Order Items</div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label id="lblPONoDes" class="control-label" runat="server" for="disabledInput">Order Number:</label>
                                <asp:DropDownList ID="ddlOrderNo" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlOrderNo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label id="lblDONo" class="control-label" runat="server" for="disabledInput">Delivery Order No:</label>
                                <asp:TextBox type="text" id="txtDeliveryOrdNo" class="form-control" placeholder="Enter Delivery Voucher No" runat="server" OnTextChanged="txtDeliveryOrdNo_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqField" CssClass="text-danger" runat="server" ErrorMessage="Please fill the Delivery Voucher No" ControlToValidate="txtDeliveryOrdNo"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label id="lblSupplierDes" class="control-label" runat="server" for="disabledInput">Supplier Name:</label>
                                <asp:Label ID="lblSupplierName" class="control-label" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="form-group">
                                <label id="lblOrdDateDec" class="control-label" runat="server" for="disabledInput">Order Date:</label>
                                <asp:Label ID="lblOrdDate" class="control-label" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="form-group" style="text-align:center">
                                <asp:Button ID="btnShow" CssClass="btn btn-success btn-sm" runat="server" Text="Preview" OnClick="btnShow_Click" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <asp:GridView ID="GdvReceivedOrder" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped gridViewTable" Font-Size="Small" DataKeyNames="ItemID,Description,UnitOfMeasurement,OrderQty" OnRowEditing="GdvReceivedOrder_RowEditing" OnRowCancelingEdit="GdvReceivedOrder_RowCancelingEdit" OnRowUpdating="GdvReceivedOrder_RowUpdating">
                    <Columns>
                        <asp:BoundField DataField="ItemID" HeaderText="Item ID" ReadOnly="true" />
                        <asp:BoundField DataField="Description" HeaderText="Item Description" ReadOnly="true" />
                        <asp:BoundField DataField="UnitOfMeasurement" HeaderText="Unit Of Measure" ReadOnly="true" />
                        <asp:BoundField DataField="OrderQty" HeaderText="Order Quantity" ReadOnly="true" />
                        <asp:BoundField DataField="ReceivedQty" HeaderText="Received Quantity" />
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" MaxLength="100" Height="40px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="true" />
                    </Columns>
                </asp:GridView>
                <div class="form-actions">
                    <asp:Button ID="btnSave" CssClass="btn btn-success btn-sm" runat="server" Text="Save" OnClick="btnSave_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

