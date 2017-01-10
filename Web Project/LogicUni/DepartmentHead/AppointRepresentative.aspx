<%@ Page Title="Appoint Representative" Language="C#" MasterPageFile="~/DepartmentHeadHome.master" AutoEventWireup="true" CodeFile="AppointRepresentative.aspx.cs" Inherits="AppointRepresentative" %>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="container-body">
            <h2 style="text-align: center">Appoint Representative Form</h2>
            <h4 id="currentRep" class="text-primary" runat="server" style="text-align: center"></h4>
        </div>
        <div class="assignRepClass">
            <div class="col-lg-4 col-lg-offset-4">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="text-align: center">Assign New Representative</div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="control-label" for="disabledInput">Employee:</label>
                            <asp:DropDownList ID="ddlEmployeeName" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="form-actions">
                            <asp:Button ID="btnCancel" CssClass="btn btn-danger btn-sm" runat="server" Text="Cancel" />
                            <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
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
