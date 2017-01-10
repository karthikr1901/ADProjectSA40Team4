<%@ Page Title="Raise Adjustment Voucher" Language="C#" MasterPageFile="~/StoreClerk.master" AutoEventWireup="true" CodeFile="RaiseAdjustmentVoucher.aspx.cs" Inherits="RaiseAdjustmentVoucher" %>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-body">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container">
            <h2 style="text-align: center">Raise Adjustment Voucher Form</h2>
            <h4 id="currentRep" class="text-primary" runat="server" style="text-align: center"></h4>
        </div>
        <div class="assignRepClass">
            <div class="col-md-4">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="text-align: center">Raise Adjustment Voucher</div>
                    <div class="panel-body">
                        <p id="txtPending" runat="server" class="text-info">There are some pending requests</p>
                        <div id="adjustmentform" runat="server">
                            <div class="form-group">
                                <label class="control-label" for="disabledInput">Category:</label>
                                <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label id="lblDescription" class="control-label" runat="server" for="disabledInput">Item Description: </label>
                                <asp:DropDownList ID="ddlItemID" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label id="lblQty" class="control-label" runat="server" for="disabledInput">Quantity: </label>
                                <asp:TextBox ID="txtQty" class="form-control" placeholder="Enter Number" runat="server"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="txtFliterQty" runat="server" Enabled="true" FilterType="Numbers" TargetControlID="txtQty" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter valid quantity" ControlToValidate="txtQty" CssClass="text-danger"></asp:RequiredFieldValidator>
                                <%--                            <asp:TextBox ID="txtQty" class="control-label" runat="server" ></asp:TextBox>--%>
                            </div>
                            <div class="form-group">
                                <label id="lblRemark" class="control-label" runat="server" for="disabledInput">Remark: </label>
                                <asp:DropDownList ID="ddlRemark" CssClass="form-control" runat="server">
                                    <asp:ListItem>Broken Items</asp:ListItem>
                                    <asp:ListItem>Free gift in offer pack</asp:ListItem>
                                    <asp:ListItem>Wrong Item</asp:ListItem>
                                    <asp:ListItem>Special gift</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-actions">
                                <asp:Button ID="btnAdd" CssClass="btn btn-success btn-sm" runat="server" Text="ADD" OnClick="btnAdd_Click" />
                            </div>
                            <asp:Label ID="lblMessage" CssClass="text-danger" runat="server" Text="Label" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <div class="gridHolder">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CssClass="table table-hover table-striped gridViewTable" Font-Size="Small" OnRowDeleting="GridView1_RowDeleting">
                        <%-- OnRowCommand="GridView1_RowCommand" >--%>
                        <Columns>
                            <asp:BoundField DataField="ItemID" HeaderText="Item ID" Visible="False" />
                            <asp:BoundField DataField="Description" HeaderText="Item Description" />
                            <asp:BoundField DataField="UnitOfMeasurement" HeaderText="Unit Of Measure" />

                            <asp:BoundField DataField="Quantity" HeaderText="Adjusted Quantity" />

                            <asp:BoundField DataField="Price" HeaderText="Unit Price ($)" />
                            <asp:BoundField DataField="Amount" HeaderText="Amount ($)" />

                            <asp:BoundField DataField="AdjustmentRemark" HeaderText="Remark" />

                            <asp:ButtonField CommandName="Delete" HeaderText="Row Delete" Text="Delete" ButtonType="Link" />
                        </Columns>
                    </asp:GridView>

                    <div class="form-actions">
                        <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    </div>
                    <div class="form-group">
                        <label id="lblResult" class="control-label" runat="server" visible="false"></label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

