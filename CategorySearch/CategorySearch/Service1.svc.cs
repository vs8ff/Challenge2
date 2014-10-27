using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace CategorySearch
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Service1 : IService1
    {

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SearchProductsByCategory/{categoryName}")]
        public String[] SearchProductsByCategory(String categoryName)
        {

                String[] productNames;
                //Declare Connection by passing the connection string from the web config file
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MercadoDb"].ConnectionString);

                //Open the connection
                conn.Open();

                SqlCommand getCount = new SqlCommand("Select count(distinct ItemName) from FarmerItems where Category ='" + categoryName + "'", conn);

                int count = Convert.ToInt16(getCount.ExecuteScalar().ToString());

                productNames = new String[count];

                //Declare the sql command
                SqlCommand getProductCmd = new SqlCommand("Select distinct ItemName from FarmerItems where Category  ='" + categoryName + "'", conn);

                SqlDataReader reader = getProductCmd.ExecuteReader();

                int i = 0;
                while (reader.Read())
                {
                    productNames[i++] = reader[0].ToString();
                }


                reader.Close();
                //close the connection
                conn.Close();

            return productNames;
        }
    }
}
