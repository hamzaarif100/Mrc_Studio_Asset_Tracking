using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication1.Pages.Assets
{
    public class IndexModel : PageModel
    {
        public List<AssetInfo> listAssets = new List<AssetInfo>();
        public void OnGet()
        {
            try 
            {
                String connectionString = "Data Source=ASUS-LAPTOP;Initial Catalog=AssetTracking;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Assets";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AssetInfo assetInfo = new AssetInfo();
                                assetInfo.id = reader.GetString(0);
                                assetInfo.assetName = reader.GetString(1);
                                assetInfo.assetTag = reader.GetString(2);
                                assetInfo.usageTag = reader.GetString(3);
                                assetInfo.location = reader.GetString(4);
                                assetInfo.userName = reader.GetString(5);

                                listAssets.Add(assetInfo);
                            }
                        }
                    }    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class AssetInfo
    {
        public String id;
        public String assetName;
        public String assetTag;
        public String usageTag;
        public String location;
        public String userName;
    }
}
