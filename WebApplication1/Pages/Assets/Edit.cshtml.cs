using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication1.Pages.Assets
{
    public class EditModel : PageModel
    {
        public AssetInfo assetInfo = new AssetInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=ASUS-LAPTOP;Initial Catalog=AssetTracking;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Assets WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            { 
                                assetInfo.id = reader.GetString(0);
                                assetInfo.assetName = reader.GetString(1);
                                assetInfo.assetTag = reader.GetString(2);
                                assetInfo.usageTag = reader.GetString(3);
                                assetInfo.location = reader.GetString(4);
                                assetInfo.userFirstName = reader.GetString(5);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {



            assetInfo.id = Request.Form["id"];
            assetInfo.assetName = Request.Form["assetName"];
            assetInfo.assetTag = Request.Form["assetTag"];
            assetInfo.usageTag = Request.Form["usageTag"];
            assetInfo.location = Request.Form["location"];
            assetInfo.userFirstName = Request.Form["userFirstName"];
            try
            {
                String connectionString = "Data Source=ASUS-LAPTOP;Initial Catalog=AssetTracking;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "UPDATE Assets " +
                                 "SET AssetName=@assetName, AssetTag=@assetTag, UsageTag=@usageTag, Location=@location, UserFirstName=@userFirstName " +
                                 "WHERE ID=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        
                        command.Parameters.AddWithValue("@assetName", assetInfo.assetName);
                        command.Parameters.AddWithValue("@assetTag", assetInfo.assetTag);
                        command.Parameters.AddWithValue("@usageTag", assetInfo.usageTag);
                        command.Parameters.AddWithValue("@location", assetInfo.location);
                        command.Parameters.AddWithValue("@userFirstName", assetInfo.userFirstName);
                        command.Parameters.AddWithValue("@id", assetInfo.id);
                        
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Assets/Index");
        }
    }
}
