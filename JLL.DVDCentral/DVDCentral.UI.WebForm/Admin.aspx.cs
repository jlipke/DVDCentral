using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DVDCentral.UI.WebForm
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnMovies_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ManageMovies.aspx");
        }

        protected void btnOrders_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ManageOrders.aspx");
        }

        protected void btnRatings_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ManageRatings.aspx");
        }

        protected void btnDirectors_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ManageDirectors.aspx");
        }

        protected void btnGenres_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ManageGenres.aspx");
        }
    }
}