<%@ Page Title="Change Collection Point" Language="C#" MasterPageFile="~/DepartmentRepresentative.master" AutoEventWireup="true" CodeFile="CollectionPoint.aspx.cs" Inherits="DRCollectionPt" %>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-body">
        <div class="container">
            <h2 style="text-align: center">Change of Collection Point</h2>
            <h4 id="currentCollectionPoint" class="text-primary" runat="server" style="text-align: center"></h4>
        </div>
        <div class="assignRepClass">
            <div class="col-md-4 col-md-offset-4">
                <div class="panel panel-primary" id="pv" runat="server">
                    <div class="panel-heading" style="text-align: center">Request History Preference</div>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:Label ID="lblchcoll" class="control-label" runat="server" Text="Collection Points : "></asp:Label>
                            <asp:DropDownList ID="DropDownList2" CssClass="form-control" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:Label ID="lblTime"  class="control-label" runat="server"></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="Button2" runat="server" Text="Save" CssClass="btn btn-success btn-block" OnClick="Button2_Click" />
                        </div>
                        <div class="form-group">
                        </div>
                        <div class="form-actions">
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
