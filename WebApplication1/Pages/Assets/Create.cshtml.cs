using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication1.Pages.Assets
{
    public class CreateModel : PageModel
    {
        public AssetInfo assetInfo = new AssetInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            assetInfo.id = Request.Form["id"];
            assetInfo.assetName = Request.Form["assetName"];
            assetInfo.assetTag = Request.Form["assetTag"];
            assetInfo.usageTag = Request.Form["usageTag"];
            assetInfo.location = Request.Form["location"];
            assetInfo.userName = Request.Form["firstName"];

            if (assetInfo.id.Length == 0 || assetInfo.assetName.Length == 0 ||
                assetInfo.usageTag.Length == 0)
            {
                errorMessage = "All the fields are required!";
                return;
            }

            //save the new asset into database

            try 
            {
                String connectionString = "Data Source=ASUS-LAPTOP;Initial Catalog=AssetTracking;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Assets " +
                                 "(id, assetName, assetTag, usageTag, location, userFirstName) VALUES " +
                                 "(@id, @assetName, @assetTag, @usageTag, @location, @userFirstName);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", assetInfo.id);
                        command.Parameters.AddWithValue("@assetName", assetInfo.assetName);
                        command.Parameters.AddWithValue("@assetTag", assetInfo.assetTag);
                        command.Parameters.AddWithValue("@usageTag", assetInfo.usageTag);
                        command.Parameters.AddWithValue("@location", assetInfo.location);
                        command.Parameters.AddWithValue("@userFirstName", assetInfo.userName);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            assetInfo.id = "";
            assetInfo.assetName = "";
            assetInfo.assetTag = "";
            assetInfo.usageTag = "";
            assetInfo.location = "";
            assetInfo.userName = "";
            successMessage = "New Asset Added Successfully!"; 

            Response.Redirect("/Assets/Index");
        }
    }
}
