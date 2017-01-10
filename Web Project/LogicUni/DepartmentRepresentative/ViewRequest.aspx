<%@ Page Title="View Request" Language="C#" MasterPageFile="~/DepartmentRepresentative.master" AutoEventWireup="true" CodeFile="ViewRequest.aspx.cs" Inherits="EmpHistory" %>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-body">
        <div class="container">
            <h2 style="text-align: center">Requisition History</h2>
            <h3 id="noRequest" class="text-primary" runat="server" style="text-align: center">There is no request at this moment</h3>
        </div>
        <div class="assignRepClass">
            <div id="RequestForm" runat="server">
                <div class="col-md-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center">View Request</div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="control-label" for="disabledInput">
                                    <asp:Label ID="Label2" runat="server" Text="Request Number :"></asp:Label>
                                </label>
                                <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="Button1" CssClass="btn btn-success btn-block" runat="server" Text="SELECT" OnClick="Button1_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <label id="Label3" class="text-info" runat="server" text="Status :" style="float: left"></label>
                <label id="Label5" runat="server" class="text-info" text="Reason :" style="float: right"></label>

                <asp:GridView runat="server" ID="GridView1" HeaderStyle-CssClass="table_header" CssClass="table table-hover table-striped gridViewTable" AllowPaging="True" Font-Size="small" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                    DataKeyNames="RequestedItem">
                    <Columns>
                        <asp:BoundField DataField="RequestedItem" HeaderText="ItemID" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"></asp:BoundField>
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="RequestedQty" HeaderText="Quantity" />
                        <asp:BoundField DataField="UnitOfMeasurement" HeaderText="Unit of Measurement" />
                        <asp:BoundField DataField="ReceivedQty" HeaderText="Recieved Quantity" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

