<%@ Page Title="Approve Reject Request" Language="C#" MasterPageFile="~/DepartmentHeadHome.master" AutoEventWireup="true" CodeFile="ApproveRejectRequest.aspx.cs" Inherits="ApproveRejectRequest" %>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-body">
        <div class="container">
            <h2 style="text-align: center">Stationery Requisition Form</h2>
            <h3 style="text-align: center" id="noRequest" class="text-primary" runat="server">There is no pending requests</h3>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        </div>
        <div class="assignRepClass">
            <div class="requestform">
                <div id="RequestForm" runat="server">
                    <div class="col-md-4">
                        <div class="panel panel-primary">
                            <div class="panel-heading" style="text-align: center">Pending Requests</div>
                            <div class="panel-body">
                                <div class="form-group">
                                    <label id="lblRequestEmp" class="control-label" for="disabledInput" runat="server">Requests:</label>
                                    <div class="form-actions">
                                        <asp:DropDownList ID="ddlRequest" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRequest_SelectedIndexChanged"></asp:DropDownList>
                                        <p id="lblEmployeeStatus" class="text-danger" runat="server"></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="gridHolder">
                    <div class="col-md-8">
                        <asp:GridView ID="PreviewGridView" CssClass="table table-striped table-hover" runat="server" AutoGenerateColumns="False" AllowPaging="true" Font-Size="Small">
                            <Columns>
                                <asp:BoundField HeaderText="Description" DataField="Description" />
                                <asp:BoundField HeaderText="Requested Qty" DataField="Quantity" />
                                <asp:BoundField HeaderText="Unit of Measurement" DataField="UnitOfMeasurement" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-md-8 col-md-offset-4">
                        <asp:TextBox ID="txtReason" placeholder="Reason" CssClass="form-control" runat="server" TextMode="MultiLine" ReadOnly="false" AutoPostBack="false" Height="40px"></asp:TextBox>
                    </div>
                    <div class="col-md-8 col-md-offset-4">
                        <div class="approverejectbutton">
                            <asp:Button ID="btnReject" CssClass="btn btn-danger btn-sm" runat="server" Text="Reject" OnClick="btnReject_Click" />
                            <asp:Button ID="btnApprove" CssClass="btn btn-success btn-sm" runat="server" Text="Approve" OnClick="btnApprove_Click" />
                        </div>
                        <div class="col-md-8">
                            <p runat="server" id="lblResult" class="text-danger"></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
