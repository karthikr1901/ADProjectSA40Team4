﻿<%@ Page Title="Edit Request" Language="C#" MasterPageFile="~/DepartmentRepresentative.master" AutoEventWireup="true" CodeFile="EditRequisition.aspx.cs" Inherits="EditRequisition" %>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-body">
        <div class="container">
            <h2 style="text-align: center">Edit Requisition Form</h2>
            <h3 id="noEdit" class="text-primary" runat="server" style="text-align: center">There is no request to edit</h3>
        </div>
        <div class="assignRepClass">
            <div class="col-md-4">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div id="editForm" runat="server">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center">Edit Request</div>
                        <div class="panel-body">
                            <div class="form-group">
                                &nbsp;<label class="control-label" for="disabledInput"><asp:Label ID="Label2" runat="server" Text="Request Number :"></asp:Label>
                                </label>
                                <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="Button1" CssClass="btn btn-success btn-block" runat="server" Text="EDIT" OnClick="Button1_Click" />
                            </div>
                            <div class="form-group">
                                <label id="lblCategory" class="control-label" runat="server" for="disabledInput">Category:</label>
                                <asp:DropDownList ID="DropDownList2" CssClass="form-control" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label id="lblDescription" class="control-label" runat="server" for="disabledInput">Item Description: </label>
                                <label class="control-label" for="disabledInput"></label>
                                <asp:DropDownList ID="DropDownList3" CssClass="form-control" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                            </div>
                            <div class="form-group">
                                <label id="lblQty" class="control-label" runat="server" for="disabledInput">Quantity: </label>
                                <asp:TextBox id="TextBox1" class="form-control" placeholder="Enter Number" runat="server" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="filter" runat="server" TargetControlID="TextBox1" FilterType="Numbers" ValidChars="0123456789" />
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </div>
                            <div class="form-actions">
                                <asp:Button ID="Button3" CssClass="btn btn-success btn-block" runat="server" Text="ADD" OnClick="Button3_Click" />
                            </div>
                            <div class="form-group">
                                <label id="lblResult" class="control-label" runat="server" visible="false"></label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-8">
            <asp:GridView runat="server" ID="GridView1" HeaderStyle-CssClass="table_header" CssClass="table table-hover table-striped gridViewTable" AllowPaging="True" Font-Size="small" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                DataKeyNames="RequestedItem" OnRowDeleting="GridView1_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="RequestedItem" HeaderText="ItemID" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"></asp:BoundField>
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="RequestedQty" HeaderText="Quantity" />
                    <asp:BoundField DataField="UnitOfMeasurement" HeaderText="Unit of Measurement" />
                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                </Columns>
            </asp:GridView>
            <div class="form-actions">
                <asp:Button ID="Button5" CssClass="btn btn-danger btn-sm" runat="server" Text="Delete" OnClick="Button5_Click" />
                <asp:Button ID="Button4" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" OnClick="Button4_Click" />
            </div>
        </div>
    </div>
</asp:Content>

