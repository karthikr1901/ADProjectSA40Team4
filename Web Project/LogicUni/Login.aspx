<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" MasterPageFile="~/Login.master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="LoginFormGroup">
        <div class="col-sm-4 col-sm-offset-4">
            <div class="form-group">
                <h3 style="text-align: center">Login to Stationery Store</h3>
            </div>
            <div class="form-group">
                <label class="sr-only" for="InputEmail">Email address</label>
                <input type="email" class="form-control" id="txtEmail" placeholder="Email address" required="required" runat="server" autofocus="autofocus">
            </div>
            <div class="form-group">
                <label class="sr-only" for="InputPassword">Password</label>
                <input type="password" class="form-control" id="txtPassword" placeholder="Password" required runat="server">
            </div>
            <div class="checkbox">
                <label>
                    <asp:CheckBox ID="chkRememberMe" runat="server" />
                    Keep me Logged in
                </label>
            </div>
            <div class="form-group">
                <asp:Button ID="btnSubmitLogin" Text="Submit" CssClass="btn btn-success btn-block" runat="server" OnClick="btnSubmitLogin_Click" />
            </div>
            <div>
                <asp:Label ID="lblLoginStatus" runat="server" Text="." Visible="false" Style="text-align: center" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>


