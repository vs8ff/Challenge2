/*
 *  This Rest Service is used to Store the Farmer Reviews and Get the all the Farmer Reviews
 *  
 *  Author Name: Bharat Viswanadham
 *  
 */
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace FarmerReview
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Service1 : IService1
    {
        /*
         * This Rest Service is used to Store the Farmer Reviews 
         * Input is customerName, farmerName, rating, review
         */ 
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PutFarmerReview/{cName}/{fName}/{rating}/{review}")]
        public String PutFarmerReview(String cName, String fName, String rating, String review)
        {
            String result = "";
            
            try{

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MercadoDb"].ConnectionString);

            conn.Open();

            int rate = Convert.ToInt16(rating);
            SqlCommand putFarmerReviewCmd = new SqlCommand("Insert into farmerReview(customerName,farmerName,rating,comments) values ('"
                                                            + cName + "','" + fName + "'," + rate + ",'" + review + "')", conn);

            int rowsaffected = putFarmerReviewCmd.ExecuteNonQuery();

            if (rowsaffected == 1)
                result = "Success";
            else
                result = "Failure";


            putFarmerReviewCmd.Dispose();

            conn.Close();

            }

            catch(Exception e)
            {
                e.GetBaseException();
                Console.WriteLine(e.Message);
            }


            return result;
            /*
             putFarmerReviewCmd = new SqlCommand("select * from farmerReview", conn);

            SqlDataReader reader = putFarmerReviewCmd.ExecuteReader();

            String c="", f="", r="", re="";

            while (reader.Read())
            {
                c = reader[0].ToString();
                f = reader[1].ToString();
                r = reader[2].ToString();
                re = reader[3].ToString();
            }


            String result = c + f + r + re;

            return result;
             */
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFarmerReview/{fName}")]
        public FarmerReviewList GetFarmerReview(String fName)
        {
            FarmerReviewList fReviews = new FarmerReviewList();
            fReviews.reviewList = new List<FarmerReview>();

            
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MercadoDb"].ConnectionString);

                conn.Open();

                SqlCommand getReviewcountCmd = new SqlCommand("select distinct count(*) from farmerReview where farmerName = '" + fName + "'", conn);

                int reviewCount = Convert.ToInt16(getReviewcountCmd.ExecuteScalar().ToString());

                conn.Close();

                FarmerReview[] reviewResults = new FarmerReview[reviewCount];

                conn.Open();

                SqlCommand getReviewsCmd = new SqlCommand("select distinct * from farmerReview where farmerName ='" + fName + "'", conn);

                SqlDataReader reader = getReviewsCmd.ExecuteReader();


                int i = 0;
                while (reader.Read())
                {
                    reviewResults[i] = new FarmerReview();
                    reviewResults[i].cName = reader["customerName"].ToString();
                    reviewResults[i].fName = reader["farmerName"].ToString();
                    reviewResults[i].rating = Convert.ToInt32(reader["rating"].ToString());
                    reviewResults[i].review = reader["comments"].ToString();
                    fReviews.reviewList.Add(reviewResults[i]);
                    i++;
                }
            }
            catch(Exception e)
            {
                e.GetBaseException();
                Console.WriteLine(e.Message);
            }
                return fReviews;
        }



        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFarmers")]
        public String[] GetFarmers()
        {
            String[] farmerNames ={""};

            try{
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MercadoDb"].ConnectionString);

                conn.Open();

                SqlCommand getReviewcountCmd = new SqlCommand("select count (username) from farmerregistration", conn);

                int reviewCount = Convert.ToInt16(getReviewcountCmd.ExecuteScalar().ToString());

                conn.Close();

               farmerNames = new String[reviewCount];

               conn.Open();

               SqlCommand getReviewsCmd = new SqlCommand("select username from farmerregistration", conn);

                SqlDataReader reader = getReviewsCmd.ExecuteReader();


                int i = 0;
                while (reader.Read())
                {
                    farmerNames[i++] = reader[0].ToString();
                }
                conn.Close();
                return farmerNames;
            }
            catch(Exception e)
            {
                e.GetBaseException();
                Console.WriteLine(e.Message);
            }
            return farmerNames;
        }

    }
}
