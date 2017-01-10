<%@ Page Title="Delegate Authority" MasterPageFile="~/DepartmentHeadHome.master" Language="C#" AutoEventWireup="true" CodeFile="DelegateAuthority.aspx.cs" Inherits="DelegateAuthority" %>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-body">
        <div class="container">
            <h2 style="text-align: center">Delegate Authority Form</h2>
        </div>
        <div class="assignRepClass">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="col-lg-4 col-lg-offset-4">
                <div class="assign" id="assignbox" runat="server">
                    <div class="form-group">
                        <label class="col-lg-4 control-label">Employee Name:</label>
                        <asp:DropDownList ID="ddlEmployeeName" CssClass="form-control" runat="server"></asp:DropDownList>
                        <br />
                    </div>
                    <div class="form-group">
                        <label class="col-lg-4 control-label">From Date:</label>
                        <asp:TextBox ID="txtFromDate" CssClass="form-control" runat="server" ReadOnly="false" AutoPostBack="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Date is required!" ControlToValidate="txtFromDate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexValidatorFrom" runat="server" ErrorMessage="Please Enter Valid Date!" ControlToValidate="txtFromDate" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        <ajaxToolkit:CalendarExtender ID="CalFromDate" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy" ClearTime="True"></ajaxToolkit:CalendarExtender>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-4 control-label">To Date:</label>
                        <asp:TextBox ID="txtToDate" CssClass="form-control" runat="server" ReadOnly="false" AutoPostBack="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Date is required!" ControlToValidate="txtToDate"></asp:RequiredFieldValidator>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                        <asp:RegularExpressionValidator ID="regexValidatorTo" runat="server" ErrorMessage="Please Enter Valid Date!" ControlToValidate="txtToDate" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        <ajaxToolkit:CalendarExtender ID="CalToDate" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" ClearTime="True"></ajaxToolkit:CalendarExtender>
                    </div>
                    <div class="form-actions">
                        <asp:Button ID="btnDSubmit" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        <br />
                        <p class="text-danger" runat="server" id="lblStatus"></p>
                        <br />
                        <br />
                    </div>
                </div>
                <div class="cancelbox" id="cancelbox" runat="server">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center">Cancellation of Authority</div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label id="lblCancelEmp" class="control-label" for="disabledInput" runat="server">Employee Name:</label>
                                <div class="form-actions">
                                    <asp:Button ID="btnCancel" CssClass="btn btn-success btn-sm" runat="server" Text="Cancel Authority" OnClick="btnCancel_Click" />
                                    <p class="text-danger" runat="server" id="lblCancelStatus"></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
