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
    public partial class ManageDirectors : System.Web.UI.Page
    {
        List<Director> directors;
        Director director;

        protected void Page_Load(object sender, EventArgs e)
        {
            // If not postback, (here for the first time, load from database)

            if (!IsPostBack)
            {
                // Call the correct load method in the BL
                directors = new List<Director>();
                directors = DirectorManager.Load();
                Rebind();

                // Put into session
                Session["directors"] = directors;
            }
            else
            {
                // Load from session
                directors = (List<Director>)Session["directors"];
            }
        }

        private void Rebind()
        {
            // Bind the list to the dropdownlist
            ddlDirectors.DataSource = null;
            ddlDirectors.DataSource = directors;

            //Designate the field that will be displayed
            ddlDirectors.DataTextField = "FirstName";   
           
            // Designate the field that is the primary key or the unique identifier
            ddlDirectors.DataValueField = "Id";

            // Refresh the binding
            ddlDirectors.DataBind();

            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
        }

        protected void ddlDirectors_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the director that is selected.
            director = directors[ddlDirectors.SelectedIndex];

            // Display the First and last name
            txtFirstName.Text = director.FirstName;
            txtLastName.Text = director.LastName;

        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                director = new Director();
                // Get the typed description from the screen
                director.FirstName = txtFirstName.Text;
                director.LastName = txtLastName.Text;

                // Add to the database
                DirectorManager.Insert(director);

                // Add to the list
                directors.Add(director);

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
                int index = ddlDirectors.SelectedIndex;
                director = directors[ddlDirectors.SelectedIndex];

                // Get the typed description from the screen
                director.FirstName = txtFirstName.Text;
                director.LastName = txtLastName.Text;

                // Add to the database
                DirectorManager.Update(director);

                // Update to the list
                directors[ddlDirectors.SelectedIndex] = director;

                Rebind();

                ddlDirectors.SelectedIndex = index;
                ddlDirectors_SelectedIndexChanged(sender, e);

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

                director = directors[ddlDirectors.SelectedIndex];


                // Add to the database
                DirectorManager.Delete(director.Id);

                // Remove it from the list
                directors.Remove(director);

                Rebind();

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }
    }
}