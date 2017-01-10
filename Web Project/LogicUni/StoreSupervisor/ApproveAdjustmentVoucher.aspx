<%@ Page Title="Approve Adjustment Voucher" Language="C#" MasterPageFile="~/StoreSupervisor.master" AutoEventWireup="true" CodeFile="ApproveAdjustmentVoucher.aspx.cs" Inherits="ApproveAdjustmentVoucher" %>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-body">
        <div class="container">
            <h2 style="text-align: center">Adjustment Voucher</h2>
            <h3 id="noAdjustment" class="text-primary" runat="server" style="text-align: center">There is no pending adjustments</h3>
        </div>
        <div class="assignRepClass">
            <div id="voucherbox" runat="server">
                <div class="col-md-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center">Approve Adjustment Voucher</div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="control-label" for="disabledInput">Voucher No:</label>
                                <asp:DropDownList ID="ddlVoucherNo" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlVoucherNo_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label id="lblResult" class="control-label" runat="server" visible="false"></label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <asp:GridView ID="gdvAdjustmentDetail" CssClass="table table-hover table-striped gridViewTable" runat="server" AllowPaging="True" AutoGenerateColumns="False" Font-Size="small">
                    <Columns>
                        <asp:BoundField DataField="Description" HeaderText="Item Description" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity Adjusted" />
                        <asp:BoundField DataField="UnitOfMeasurement" HeaderText="Unit Of Measurement" />
                        <asp:BoundField DataField="Price" HeaderText="Unit Price(S$)" />
                        <asp:BoundField DataField="Amount"  HeaderText="Amount(S$)" />
                        <asp:BoundField DataField="AdjustmentRemark" HeaderText="Remark" />
                    </Columns>
                </asp:GridView>
                <div class="form-actions">
                    <asp:Button ID="btnApprove" CssClass="btn btn-success btn-sm" runat="server" Text="Approve" OnClick="btnApprove_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

