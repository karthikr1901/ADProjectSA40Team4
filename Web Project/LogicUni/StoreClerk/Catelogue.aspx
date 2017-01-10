<%@ Page Title="Catelogue" Language="C#" MasterPageFile="~/StoreClerk.master" AutoEventWireup="true" CodeFile="Catelogue.aspx.cs" Inherits="Catelogue" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-catelogue">
        <div class="container">
            <h2 style="text-align: center">Catalogue</h2>
        </div>
        <div class="assignRepClass">
            <div class="col-lg-4 col-lg-offset-4">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="text-align: center">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        Add New Item
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="col-lg-4 control-label">Category:</label>
                            <%--<asp:TextBox ID="txtCategory" runat="server"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-4 control-label">Description:</label>
                            <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-4 control-label">Reorder Level:</label>
                            <asp:TextBox ID="txtReorderLevel" CssClass="form-control" runat="server"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="txtFilterReorderLevel" runat="server" Enabled="true" FilterType="Numbers" TargetControlID="txtReorderLevel" />
                        </div>
                        <div class="form-group">
                            <label class="col-lg-4 control-label">Reorder Qty:</label>
                            <asp:TextBox ID="txtReorderQty" CssClass="form-control" runat="server"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="txtFliterReorderQty" runat="server" Enabled="true" FilterType="Numbers" TargetControlID="txtReorderQty" />
                        </div>
                        <div class="form-group">
                            <label class="col-lg-4 control-label">Unit of Measure:</label>
                            <asp:TextBox ID="txtUOM" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-4 control-label">Balance:</label>
                            <asp:TextBox ID="txtBalance" CssClass="form-control" runat="server"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="txtFliterBalance" runat="server" Enabled="true" FilterType="Numbers" TargetControlID="txtBalance" />
                        </div>
                        <div class="form-actions">
                            <asp:Button ID="btnAdd" Style="text-align: center" CssClass="btn btn-success btn-block" runat="server" Text="AddItem" OnClick="btnAdd_Click" />
                            <br />
                            <p id="lblCatelogueStatus" class="text-danger" runat="server"></p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">

                    <asp:GridView ID="CatagoryGridView" CssClass="table table-striped table-hover" AllowPaging="true" PageSize="20" DataKeyNames="ItemID,CategoryName,Description" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="CatagoryGridView_SelectedIndexChanged" OnRowDeleting="CatagoryGridView_RowDeleting" OnRowEditing="CatagoryGridView_RowEditing" OnRowCancelingEdit="CatagoryGridView_RowCancelingEdit" OnRowUpdating="CatagoryGridView_RowUpdating" CellPadding="1" HorizontalAlign="Left" OnPageIndexChanging="CatagoryGridView_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="Item Number" DataField="ItemID" ReadOnly="true" />
                            <asp:BoundField HeaderText="Category" DataField="CategoryName" ReadOnly="true" />
                            <asp:BoundField HeaderText="Description" DataField="Description" ReadOnly="true" />
                            <asp:BoundField HeaderText="Reorder Level" DataField="ReorderLevel" />
                            <asp:BoundField HeaderText="Reorder Qty" DataField="ReorderQuantity" />
                            <asp:BoundField HeaderText="Unit of Measurement" DataField="UnitOfMeasurement" />
                            <asp:BoundField HeaderText="Balance" DataField="Balance" />
                            <asp:CommandField ShowEditButton="true" />
                            <asp:CommandField ShowDeleteButton="true" />
                        </Columns>
                    <PagerStyle HorizontalAlign="Right" CssClass="pager" />
                    </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
