using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.BL.Models;

namespace DVDCentral.UI.WebForm
{
    public partial class ManageGenres : System.Web.UI.Page
    {
        List<Genre> genres;
        Genre genre;

        protected void Page_Load(object sender, EventArgs e)
        {
            // If not postback, (here for the first time, load from database)

            if (!IsPostBack)
            {
                // Call the correct load method in the BL
                genres = new List<Genre>();
                genres = GenreManager.Load();
                Rebind();

                // Put into session
                Session["genres"] = genres;
            }
            else
            {
                // Load from session
                genres = (List<Genre>)Session["genres"];
            }
        }

        private void Rebind()
        {
            // Bind the list to the dropdownlist
            ddlGenres.DataSource = null;
            ddlGenres.DataSource = genres;

            //Designate the field that will be displayed
            ddlGenres.DataTextField = "Description";   // if nothing binds, its typically because "Description" is spelled wrong

            // Designate the field that is the primary key or the unique identifier
            ddlGenres.DataValueField = "Id";

            // Refresh the binding
            ddlGenres.DataBind();

            txtDescription.Text = string.Empty;
        }

        protected void ddlGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the genre that is selected.
            genre = genres[ddlGenres.SelectedIndex];

            // Display the description
            txtDescription.Text = genre.Description;

        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                genre = new Genre();
                // Get the typed description from the screen
                genre.Description = txtDescription.Text;

                // Add to the database
                GenreManager.Insert(genre);

                // Add to the list
                genres.Add(genre);

                Rebind();

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the one the user has selected
                int index = ddlGenres.SelectedIndex;
                genre = genres[ddlGenres.SelectedIndex];

                // Get the typed description from the screen
                genre.Description = txtDescription.Text;

                // Add to the database
                GenreManager.Update(genre);

                // Update to the list
                genres[ddlGenres.SelectedIndex] = genre;

                Rebind();

                ddlGenres.SelectedIndex = index;
                ddlGenres_SelectedIndexChanged(sender, e);

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the one the user has selected

                genre = genres[ddlGenres.SelectedIndex];


                // Add to the database
                GenreManager.Delete(genre.Id);

                // Remove it from the list
                genres.Remove(genre);

                Rebind();

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }
    }
}