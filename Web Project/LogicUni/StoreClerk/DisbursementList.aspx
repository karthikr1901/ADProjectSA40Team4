<%@ Page Title="Disbursement List" Language="C#" MasterPageFile="~/StoreClerk.master" AutoEventWireup="true" CodeFile="DisbursementList.aspx.cs" Inherits="DisbursementList" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-body">
        <div class="container">
            <h3 style="text-align: center">Disbursement List</h3>
        </div>
        <div class="assignRepClass">
            <div class="col-md-4">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="text-align: center">Disbursement List</div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="control-label" for="disabledInput">Department:</label>
                            <asp:DropDownList ID="ddlDepartment" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-block" OnClick="btnSearch_Click1" Text="Search" />
                        </div>
                        <div class="form-group">
                            <label id="lblResult" class="text-danger" runat="server" visible="false"></label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <div class="form-group">
                    <label id="lblCollectpoint" runat="server" style="float: left" class="text-info"></label>
                    <label id="lblRep" runat="server" style="float: right" class="text-info"></label>
                    <asp:GridView ID="GridView1" CssClass="table table-striped table-hover" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="description" HeaderText="Description" />
                            <asp:BoundField DataField="requestedqty" HeaderText="RequestedQty" />
                            <asp:BoundField DataField="recievedqty" HeaderText="ReceivedQty" />
                            <asp:BoundField DataField="outstandingqty" HeaderText="OutstandingQty" />
                            <asp:BoundField DataField="unitofmeasurement" HeaderText="UnitOfMeasurement" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
