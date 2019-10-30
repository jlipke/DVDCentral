<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="DVDCentral.UI.WebForm.admin" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="form-group mt-2 ml-5">
    <div class="table">
        <table style="width:50%">
        <tr>
            <td><asp:Button ID="btnMovies" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Movies" OnClick="btnMovies_Click"/></td>
            <td><asp:Button ID="btnOrders" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Orders" OnClick="btnOrders_Click"/></td>
            <td><asp:Button ID="btnRatings" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Ratings" OnClick="btnRatings_Click" /></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnDirectors" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Directors" OnClick="btnDirectors_Click" /></td>
            <td><asp:Button ID="btnGenres" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Genres" OnClick="btnGenres_Click" /></td>
        </tr>

        </table>    
            <br />

        
        </div>
    </div>
    
    
</asp:Content>
