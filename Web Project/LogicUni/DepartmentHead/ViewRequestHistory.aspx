<%@ Page Title="View Request History" Language="C#" MasterPageFile="~/DepartmentHeadHome.master" AutoEventWireup="true" CodeFile="ViewRequestHistory.aspx.cs" Inherits="ViewRequestHistory" %>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-body">
        <div class="container">
            <h2 style="text-align: center">View Request History</h2>
            <h3 style="text-align:center" class="text-primary" id="noRequest" runat="server">There is no pending requests</h3>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        </div>
        <div class="assignRepClass" id ="requestbox" runat="server">
                <div class="col-md-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center">Request History Preference</div>
                        <div class="panel-body">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Employee Name:</label>
                                    <asp:DropDownList ID="ddlEmployeeName" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlEmployeeName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">From Date:</label>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Date is required!" ControlToValidate="txtFromDate"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regexValidatorFrom" runat="server" ErrorMessage="Please Enter Valid Date!" ControlToValidate="txtFromDate" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                    <ajaxToolkit:CalendarExtender ID="CalFromDate" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy" />
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">To Date:</label>
                                    <asp:TextBox ID="txtToDate" runat="server" AutoPostBack="True" OnTextChanged="txtToDate_TextChanged" CssClass="form-control" CausesValidation="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Date is required!" ControlToValidate="txtToDate"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regexValidatorTo" runat="server" ErrorMessage="Please Enter Valid Date!" ControlToValidate="txtToDate" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                    <ajaxToolkit:CalendarExtender ID="CalToDate" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" />
                                </div>
                                <div class="form-actions">
                                    <p class="text-danger" runat="server" id="lblStatus"></p>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Request No:</label>
                                    <asp:DropDownList ID="ddlRequestNo" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <p id="status" class="text-danger" runat="server"></p>
                                <asp:Button ID="btnPreview" CssClass="btn btn-success btn-block" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="gridHolder">
                <div class="col-md-8">
                    <p id="lblRequestNo" runat="server" class="text-info" style="float: right"></p>
                    <p id="lbRequestStatus" runat="server" class="text-info" style="float: left"></p>
                    <asp:GridView ID="PreviewGridView" CssClass="table table-striped table-hover" runat="server" AutoGenerateColumns="False" AllowPaging="true" Font-Size="Small">
                        <Columns>
                            <asp:BoundField HeaderText="Description" DataField="Description" />
                            <asp:BoundField HeaderText="Requested Qty" DataField="RequestedQty" />
                            <asp:BoundField HeaderText="Received Qty" DataField="ReceivedQty" />
                            <asp:BoundField HeaderText="Unit of Measurement" DataField="UnitOfMeasurement" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

    </div>

</asp:Content>
