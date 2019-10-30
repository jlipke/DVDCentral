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
    public partial class ManageRatings : System.Web.UI.Page
    {
        List<Rating> ratings;
        Rating rating;

        protected void Page_Load(object sender, EventArgs e)
        {
            // If not postback, (here for the first time, load from database)

            if (!IsPostBack)
            {
                // Call the correct load method in the BL
                ratings = new List<Rating>();
                ratings = RatingManager.Load();
                Rebind();

                // Put into session
                Session["ratings"] = ratings;
            }
            else
            {
                // Load from session
                ratings = (List<Rating>)Session["ratings"];
            }
        }

        private void Rebind()
        {
            // Bind the list to the dropdownlist
            ddlRatings.DataSource = null;
            ddlRatings.DataSource = ratings;

            //Designate the field that will be displayed
            ddlRatings.DataTextField = "Description";   // if nothing binds, its typically because "Description" is spelled wrong

            // Designate the field that is the primary key or the unique identifier
            ddlRatings.DataValueField = "Id";

            // Refresh the binding
            ddlRatings.DataBind();

            txtDescription.Text = string.Empty;
        }

        protected void ddlRatings_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the rating that is selected.
            rating = ratings[ddlRatings.SelectedIndex];

            // Display the description
            txtDescription.Text = rating.Description;
            
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                rating = new Rating();
                // Get the typed description from the screen
                rating.Description = txtDescription.Text;

                // Add to the database
                RatingManager.Insert(rating);

                // Add to the list
                ratings.Add(rating);

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
                int index = ddlRatings.SelectedIndex;
                rating = ratings[ddlRatings.SelectedIndex];

                // Get the typed description from the screen
                rating.Description = txtDescription.Text;

                // Add to the database
                RatingManager.Update(rating);

                // Update to the list
                ratings[ddlRatings.SelectedIndex] = rating;

                Rebind();

                ddlRatings.SelectedIndex = index;
                ddlRatings_SelectedIndexChanged(sender, e);

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

                rating = ratings[ddlRatings.SelectedIndex];


                // Add to the database
                RatingManager.Delete(rating.Id);

                // Remove it from the list
                ratings.Remove(rating);

                Rebind();

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }
    }
}