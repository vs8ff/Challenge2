using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace BestPrice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetBestPrice/{itemName}")]
        public String BestPrice(String itemName)
        {

            String bestPrice = "";
            String avgPrice = "";

            //Declare Connection by passing the connection string from the web config file
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MercadoDb"].ConnectionString);

            //Open the connection
            conn.Open();

          

            //Declare the sql command
            SqlCommand getPriceCmd = new SqlCommand("Select min(Price),avg(Price) from FarmerItems where itemName ='" + itemName + "'",conn);

            SqlDataReader reader = getPriceCmd.ExecuteReader();

            while (reader.Read())
            {
                bestPrice = reader[0].ToString();
                avgPrice = reader[1].ToString();

            }
            reader.Close();
            //close the connection
            conn.Close();

            return bestPrice+"  " +avgPrice;
        }

    }

}
