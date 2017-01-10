<%@ Page Title="Inventory Status Report" Language="C#" MasterPageFile="~/StoreClerk.master" AutoEventWireup="true" CodeFile="InventoryStatusReport.aspx.cs" Inherits="InventoryStatusReport" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="container">
        <div class="container-body">
            <h2 style="text-align: center">Inventory Status Report</h2>
            <h4 id="currentRep" class="text-primary" runat="server" style="text-align: center"></h4>
        </div>
        </div>
        <div class="gridHolder">
            <div class="form-group">
                <asp:GridView ID="GridInventoryReport" CssClass="table table-striped table-hover" runat="server" AutoGenerateColumns="False" PageSize="15" AllowPaging="True" OnPageIndexChanging="GridInventoryReport_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="ItemID" HeaderText="Item Code" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Location" HeaderText="Location" />
                        <asp:BoundField DataField="UnitOfMeasurement" HeaderText="Unit of Measurement" />
                        <asp:BoundField DataField="Balance" HeaderText="Quality on Hand" />
                        <asp:BoundField DataField="ReorderLevel" HeaderText="Reorder Level" />
                    </Columns>
                    <PagerStyle HorizontalAlign="Right" CssClass="pager" />
                </asp:GridView>
            </div>
        </div>
</asp:Content>

