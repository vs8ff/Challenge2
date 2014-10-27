using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FarmerReview
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        String PutFarmerReview(String cName, String fName, String rating, String review);

       [OperationContract]
       FarmerReviewList GetFarmerReview(String fName);

       [OperationContract]
       String[] GetFarmers();

    }

    [DataContract]
    public class FarmerReview 
    {  
        [DataMember]
        public String fName { get; set; }
        [DataMember]
        public String cName { get; set; }
        [DataMember]
        public int rating { get; set; }
        [DataMember]
        public String review { get; set; }
    }

    [DataContract]
    public class FarmerReviewList
    {
        [DataMember]
        public List<FarmerReview> reviewList { get; set; }
    }

}
