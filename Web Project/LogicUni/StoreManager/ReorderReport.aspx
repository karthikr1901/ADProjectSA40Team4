<%@ Page Title="Reorder Report" Language="C#" MasterPageFile="~/StoreManager.master" AutoEventWireup="true" CodeFile="ReorderReport.aspx.cs" Inherits="StoreSupervisor_ReorderReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-body">
        <div class="container">
            <h2 style="text-align: center">Generate Reorder Report</h2>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        </div>
        <div class="panel panel-primary">
            <div class="panel-heading" style="text-align: center">Reorder Report Preferences</div>
            <div class="panel-body">
                <div class="form-group">
                    <div class="col-sm-2 col-sm-offset-3">
                        <label class="control-label">Category:</label>
                        <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-sm-offset-3">
                        <label class="control-label">Type:</label>
                        <asp:RadioButton ID="rdQty" runat="server" CssClass="radio" GroupName="typeGroup" Text="Quantity" />
                        <asp:RadioButton ID="rdPrice" runat="server" CssClass="radio" GroupName="typeGroup" Text="Price" />
                    </div>
                    <div class="col-sm-2 col-sm-offset-3">
                        <label class="control-label">First Month:</label>
                        <asp:TextBox ID="txtFirst" CssClass="form-control" runat="server" ReadOnly="false" AutoPostBack="false"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regexValidatorFrom" runat="server" ErrorMessage="Please Enter Valid Date!" ControlToValidate="txtFirst" ValidationExpression="^((0[1-9])|(1[0-2]))\/(\d{4})$"></asp:RegularExpressionValidator>
                        <ajaxToolkit:CalendarExtender ID="CalFromDate" runat="server" TargetControlID="txtFirst" Format="MM/yyyy" ClearTime="True" DefaultView="Months"></ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Date is required!" ControlToValidate="txtFirst"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-2 col-sm-offset-3">
                        <label class="control-label">Second Month:</label>
                        <asp:TextBox ID="txtSecond" CssClass="form-control" runat="server" ReadOnly="false" AutoPostBack="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Date is required!" ControlToValidate="txtSecond"></asp:RequiredFieldValidator>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSecond" Format="MM/yyyy"></ajaxToolkit:CalendarExtender>
                        <asp:RegularExpressionValidator ID="regexValidatorTo" runat="server" ErrorMessage="Please Enter Valid Date!" ControlToValidate="txtSecond" ValidationExpression="^((0[1-9])|(1[0-2]))\/(\d{4})$"></asp:RegularExpressionValidator>
                        <ajaxToolkit:CalendarExtender ID="CalToDate" runat="server" TargetControlID="txtSecond" Format="MM/yyyy" ClearTime="True" DefaultView="Months"></ajaxToolkit:CalendarExtender>
                    </div>
                    <div class="col-sm-2 col-sm-offset-3">
                        <label class="control-label">Third Month:</label>
                        <asp:TextBox ID="txtThird" CssClass="form-control" runat="server" ReadOnly="false" AutoPostBack="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Date is required!" ControlToValidate="txtThird"></asp:RequiredFieldValidator>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtThird" Format="MM/yyyy"></ajaxToolkit:CalendarExtender>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter Valid Date!" ControlToValidate="txtThird" ValidationExpression="^((0[1-9])|(1[0-2]))\/(\d{4})$"></asp:RegularExpressionValidator>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtThird" Format="MM/yyyy" ClearTime="True" DefaultView="Months"></ajaxToolkit:CalendarExtender>
                    </div>
                    <div class="col-sm-2 col-sm-offset-3">
                        <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-success btn-md" Text="Generate" OnClick="btnGenerate_Click" />
                        <p class="text-danger" runat="server" id="lblStatus"></p>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">

                    <asp:GridView ID="RequestGridView" CssClass="table table-striped table-hover" runat="server" AutoGenerateColumns="False" CellPadding="1" HorizontalAlign="Left">
                        <Columns>
                            <asp:BoundField HeaderText="Category" DataField="CategoryName" ReadOnly="true" />
                            <asp:BoundField HeaderText="ItemCode" DataField="ItemID" ReadOnly="true" />
                            <asp:BoundField HeaderText="First Month" DataField="FirstMonth" ReadOnly="true" />
                            <asp:BoundField HeaderText="Second Month" DataField="SecondMonth" />
                            <asp:BoundField HeaderText="Third Month" DataField="ThirdMonth" />
                        </Columns>
                    </asp:GridView>
            </div>
        <div>
            <CR:CrystalReportViewer ID="RequestReportViewer" CssClass="ReportViewer" runat="server" AutoDataBind="true" HasToggleGroupTreeButton="false" BorderStyle="None" EnableDatabaseLogonPrompt="False" HasToggleParameterPanelButton="False" Height="50px" ToolPanelView="None" Width="350px" />
        </div>
    </div>

</asp:Content>
