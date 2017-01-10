<%@ Page Title="Process Requisition" Language="C#" MasterPageFile="~/StoreClerk.master" AutoEventWireup="true" CodeFile="ProcessRequisition.aspx.cs" Inherits="ProcessRequisition" %>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-body">
        <div class="container">
            <h2 style="text-align: center">Process Requisition Form</h2>
            <h3 id="noEdit" class="text-primary" runat="server" style="text-align: center">There is no pending request</h3>
        </div>
        <div class="assignRepClass">

            <div class="gridHolder">
                <asp:GridView runat="server" ID="GridView1" CssClass="table table-hover" AllowPaging="True" Font-Size="small" AutoGenerateColumns="False"
                    DataKeyNames="RequestedItem" OnPreRender="GridView1_PreRender">
                    <Columns>
                        <asp:BoundField DataField="RequestedItem" HeaderText="ItemID"></asp:BoundField>
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Balance" HeaderText="Balance" />
                        <asp:BoundField DataField="UnitOfMeasurement" HeaderText="Unit of Measurement" />
                        <asp:BoundField DataField="TNeeded" HeaderText="Total Needed" />
                        <asp:BoundField DataField="TAlloted" HeaderText="Total Alloted" />
                        <asp:BoundField DataField="RequestByDepartmentID" HeaderText="Department ID" />
                        <asp:BoundField DataField="Needed" HeaderText="Requested Quantity" />
                        <asp:BoundField DataField="Alloted" HeaderText="Alloted Quantity" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="form-actions">
            <asp:Button ID="btnAllocate" CssClass="btn btn-success btn-sm" runat="server" Text="Allocate" OnClick="Button2_Click" Height="30px" Width="75px" />
        </div>
    </div>
</asp:Content>

