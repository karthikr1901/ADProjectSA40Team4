<%@ Page Title="Process Outstanding Request" Language="C#" MasterPageFile="~/StoreClerk.master" AutoEventWireup="true" CodeFile="ProcessOutstandingRequest.aspx.cs" Inherits="ProcessOutstandingRequest" %>


<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-body">
        <div class="container">
            <h2 style="text-align: center">Process Outstanding Request</h2>
            <h3 id="noOutstanding" class="text-primary" runat="server" style="text-align: center">There is no outstanding items</h3>
        </div>
        <div class="assignRepClass">
            <asp:GridView ID="gdvOutstanding" CssClass="table table-hover table-striped gridViewTable" runat="server" AllowPaging="True" AutoGenerateColumns="False" Font-Size="Small" OnRowDeleting="gdvOutstanding_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="RequestID" HeaderText="Request ID" Visible="False" />
                    <asp:BoundField DataField="RequestedItem" HeaderText="Requested Item" Visible="False" />
                    <asp:BoundField DataField="Description" HeaderText="Item Description" />
                    <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />
                    <asp:BoundField DataField="UnitOfMeasurement" HeaderText="Unit Of Measurement" />
                    <asp:BoundField DataField="RequestedQty" HeaderText="Outstanding Quantity" />
                    <asp:BoundField DataField="ReceivedQty" HeaderText="Received Quantity" />
                    <asp:ButtonField CommandName="Delete" HeaderText="Row Delete" Text="Delete" ButtonType="Link" />
                </Columns>
            </asp:GridView>
            <div class="form-actions">
                <asp:Button ID="btnAllocate" CssClass="btn btn-success btn-sm" runat="server" Text="Allocate" OnClick="btnAllot_Click" />
            </div>
            <div class="form-group" style="text-align:center">
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
            </div>
        </div>
        </div>
</asp:Content>
