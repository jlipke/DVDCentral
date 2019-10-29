<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="DVDCentral.UI.WebForm.admin" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="header rounded-top">
        <h3>Admin Panel</h3>

        <div class="form-group mt-2 ml-5">
        <asp:Button ID="btnMovies" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Movies" OnClick="btnMovies_Click"/>
        <asp:Button ID="btnOrders" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Orders" OnClick="btnOrders_Click"/>
        <asp:Button ID="btnRatings" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Ratings" OnClick="btnRatings_Click"/>

        <asp:Button ID="btnDirectors" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Directors" OnClick="btnDirectors_Click"/>
        <asp:Button ID="btnGenres" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Genres" OnClick="btnGenres_Click"/>

        

        </div>
    </div>
    
    
</asp:Content>
