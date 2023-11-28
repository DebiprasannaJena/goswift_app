using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using EntityLayer.Common;

namespace BusinessLogicLayer.Common
{
     [ServiceContract]
   public interface ICommonBusinessLayer
    {
         [OperationContract]
         string ManageFeedback(Feedback objFeedback);

         [OperationContract]
         List<Feedback> ViewFeedback(Feedback objFeedback);

         [OperationContract]
         List<Gallery> ViewGallery(Gallery objGallery);

         [OperationContract]
         string ManageGallery(Gallery objGallery);

         [OperationContract]
         Gallery EditGallery(int ImageId);

         [OperationContract]
         string DeleteGalleryData(Gallery objGallery);
    }
}
