<%@ Page Title="Raise Purchase Order" Language="C#" MasterPageFile="~/StoreClerk.master" AutoEventWireup="true" CodeFile="RaisePurchaseOrder.aspx.cs" Inherits="RaisePurchaseOrder" %>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-body">
        <div class="container">
            <h2 style="text-align: center">Raise Purchase Order Form</h2>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <h3 id="noLowLevel" class="text-primary" runat="server" style="text-align: center" visible="false">There is no low level stocks</h3>
        </div>
        <div class="assignRepClass">
            <div class="col-md-8">
                <div class="form-group">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped gridViewTable" PageSize="8" Font-Size="small" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Item ID">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblNo" runat="server" Text='<% #Eval("itemid")%>' OnClick="lblNo_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="description" HeaderText="Description" />
                            <asp:BoundField DataField="balance" HeaderText="Balance" />
                            <asp:BoundField DataField="reorderlevel" HeaderText="Reorder Level" />
                            <asp:BoundField DataField="reorderqty" HeaderText="Reorder Quantity" />
                            <asp:BoundField DataField="suggestedqty" HeaderText="Suggested Quantity" />
                            <asp:BoundField DataField="suppliername" HeaderText="Supplier Name" />
                            <asp:BoundField DataField="price" HeaderText="Price" />
                            <asp:BoundField DataField="totalcost" HeaderText="Total Cost" />
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" CssClass="pager" />
                    </asp:GridView>
                </div>
                <div class="form-group" style="text-align:center">
                    <asp:TextBox ID="txtDate" runat="server" placeholder="Expected Delivery"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-success btn-sm" Text="Raise Order" OnClick="Button1_Click" />
                    <ajaxToolkit:CalendarExtender ID="CalFromDate" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy" />

                </div>
                <div>
                     <asp:RegularExpressionValidator ID="regexValidatorFrom" runat="server" ErrorMessage="Please Enter Valid Date!" ControlToValidate="txtDate" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Date is required!" ControlToValidate="txtDate"></asp:RequiredFieldValidator><br />                
                </div>
            </div>
            <br />

            <div class="col-md-4">
                <div class="panel panel-primary" id="pv" runat="server">
                    <div class="panel-heading" style="text-align: center">Details</div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="control-label" for="disabledInput">
                                <asp:Label ID="Label3" runat="server" Text="Item ID :"></asp:Label>
                            </label>
                            <asp:Label ID="lblItemID" runat="server"></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label4" runat="server" Text="Item Description :"></asp:Label>
                            <asp:Label ID="lblDescript" runat="server"></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label5" runat="server" Text="No. of Suppliers :"></asp:Label>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="Radio-Group" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="form-group">
                            <label id="Label2" class="control-label" runat="server" for="disabledInput">
                                <asp:Label ID="Label6" runat="server" Text="Supplier 1 :"></asp:Label>
                            </label>
                            <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
                            <asp:DropDownList ID="DropDownList1" CssClass="form-control" Height="30px" Font-Size="XX-Small" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="Number1" runat="server" Height="30px" Width="200px" CssClass="form-actions" Font-Size="XX-Small" TextMode="Number"></asp:TextBox>
                            <asp:Label ID="lblsgqty1" runat="server" Font-Size="XX-Small"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label id="Label7" class="control-label" runat="server" for="disabledInput">
                                <asp:Label ID="Label8" runat="server" Text="Supplier 2 :"></asp:Label>
                                <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label>
                            </label>
                            <asp:DropDownList ID="DropDownList2" CssClass="form-control" Height="30px" Font-Size="XX-Small"  runat="server">
                            </asp:DropDownList>               
                        </div>
                        <div class="form-group">
                             <asp:TextBox ID="Number2" runat="server"  Height="30px" Width="200px" CssClass="form-actions" Font-Size="XX-Small" TextMode="Number"></asp:TextBox>
                            <asp:Label ID="lblsgqty2" runat="server" Font-Size="XX-Small"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label id="Label10" class="control-label" runat="server" for="disabledInput">
                                <asp:Label ID="Label11" runat="server" Text="Supplier 3:"></asp:Label>
                                <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label>
                            </label>  
                        </div>
                        <div class="form-group">
                              <asp:TextBox ID="Number3" runat="server" Height="30px" Width="200px" CssClass="form-actions" Font-Size="XX-Small" TextMode="Number"></asp:TextBox>
                            <asp:Label ID="lblsgqty3" runat="server" Font-Size="XX-Small"></asp:Label>
                        </div>
                        <div class="form-group" style="text-align:center">
                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" OnClick="btnClose_Click" />
                            <asp:Button ID="Button2" runat="server" Text="Save" CssClass="btn btn-success btn-sm" OnClick="Button2_Click" />
                        </div>
                        <div class="form-group">
                            <label id="lblResult" class="control-label" runat="server" visible="false"></label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

