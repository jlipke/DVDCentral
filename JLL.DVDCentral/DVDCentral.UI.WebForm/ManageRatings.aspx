<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageRatings.aspx.cs" Inherits="DVDCentral.UI.WebForm.ManageRatings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="header rounded-top">

        <h3>Manage Ratings</h3>
    </div>
    
    <div class="blue-border">
        <div class="form-row ml-2 mt-2">
        <br />
        <a href="/admin.aspx">Back to Admin Page</a> 
        <br />
        </div>
    <div class="form-row ml-2 mt-2">
        <div class="control-label col-md-2"> 
           <asp:Label ID="lblRatingsPick" runat="server" Text="Ratings:"></asp:Label></div>
        <div class="control-label col-md-3">
            <asp:DropDownList ID="ddlRatings"
                runat="server"
                AutoPostBack="true" OnSelectedIndexChanged="ddlRatings_SelectedIndexChanged">
            </asp:DropDownList> 
        </div>
    </div>

    <div class="form-row ml-2 mt-2">
        <div class="control-label col-md-2"> 
           <asp:Label ID="Label1" runat="server" Text="Description:"></asp:Label></div>
        <div class="control-label col-md-3">
            <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
        </div>
    </div>

    <br />

    <div class="form-group mt-2 ml-5">
        <asp:Button ID="btnInsert" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Insert" OnClick="btnInsert_Click"/>
        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Update" OnClick="btnUpdate_Click"/>
        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Delete" OnClick="btnDelete_Click"/>

    </div>
</div>


</asp:Content>
