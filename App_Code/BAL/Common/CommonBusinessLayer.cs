using EntityLayer.Common;
using DataAcessLayer.Common;
using System;

namespace BusinessLogicLayer.Common
{
    public class CommonBusinessLayer : ICommonBusinessLayer
    {
        CommonDataLayer objDataAccess = new CommonDataLayer();
        public string ManageFeedback(Feedback objFeedback)
        {
            return objDataAccess.ManageFeedback(objFeedback);
        }


        public System.Collections.Generic.List<Feedback> ViewFeedback(Feedback objFeedback)
        {
            return objDataAccess.ViewFeedback(objFeedback);
        }


        public System.Collections.Generic.List<Gallery> ViewGallery(Gallery objGallery)
        {
            return objDataAccess.ViewGallery(objGallery);
        }


        public string ManageGallery(Gallery objGallery)
        {
            return objDataAccess.ManageGallery(objGallery);
        }


        public Gallery EditGallery(int ImageId)
        {
            return objDataAccess.EditGallery(ImageId);
        }


        public string DeleteGalleryData(Gallery objGallery)
        {
            return objDataAccess.DeleteGalleryData(objGallery);
        }

        public object ViewFeedback(object objServiceEntity)
        {
            throw new NotImplementedException();
        }
    }
}
