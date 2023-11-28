using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.CMS
{
    public class CMSDetails
    {
        public string StrAction { get; set; }
        public int IntMenuId { get; set; }
        public int IntCmsId { get; set; }
        public string StrMenuName { get; set; }
        public string StrContent { get; set; }
        public int IntCreatedBy { get; set; }
        public int bitDeletedFlag { get; set; }
        public string StrDescription { get; set; }
        public string Strusername { get; set; }
        public string Strmail { get; set; }
        public string Strmobileno { get; set; }
        public string Strcompanyname { get; set; }
        public string Strtype { get; set; }
        public string Strheading { get; set; }
        public string Strimg { get; set; }
        public string strAttachment { get; set; }
        public string strpath { get; set; }
        public int IntServiceId { get; set; }
        public string StrServicename { get; set; }
        public string StrFromDate { get; set; }         // Added by Dharmasis sahoo
        public string StrToDate { get; set; }           // Added by Dharmasis sahoo
        public string StrDate { get; set; }           // Added by Dharmasis sahoo


        //added by nibedita behera for website proposal details
        public string Received { get; set; }
        public string Approved { get; set; }
        public string TotCapital { get; set; }
        public string TotEmpProp { get; set; }


        public string actioncode { get; set; }
        public string Template { get; set; }
        public string pagename { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string keyword { get; set; }
        public int pageid { get; set; }
        public string Glink { get; set; }
        public string Plink { get; set; }
        public string PageContent { get; set; }
        public int Glinkid { get; set; }
        public int Plinkid { get; set; }
        public int Templateid { get; set; }
        public int Temptid { get; set; }
        public int ContentId { get; set; }
        public string Sinppet { get; set; }
        public string Authorname { get; set; }
        public string dtmCreatedOn { get; set; }
        public int intWindowType { get; set; }
        public int intLinkType { get; set; }
        public string vchURL { get; set; }
        public int intPageType { get; set; }
        public string vchUseFulinkName { get; set; }
        public string vchUseImageURL { get; set; }
        public int intlinkId { get; set; }

        public string strContent1 { get; set; }
        public string strContent2 { get; set; }
        public string strContent3 { get; set; }
        public string strContent4 { get; set; }

        //added by Radhika Patri for website proposal details
        public string strHdrUrl { get; set; }
        public string strHdrMenues { get; set; }
        public int intHeaderId { get; set; }

        //added by Radhika Patri for website proposal details
        public string strFtrUrl { get; set; }
        public string strFtrMenues { get; set; }
        public int intFooterId { get; set; }


        //added by ABT for show the window type and pagetype show in viewLink page on cms
        public string viewPageType { get; set; }
        public string viewWindowType { get; set; }
        public string vchModalId { get; set; }


    }
    public class TemplateDetails
    {
        public string actioncode { get; set; }
        public int TemplateId { get; set; }
        public string DivName { get; set; }
        public int ContentType { get; set; }
        public int IntPLinkId { get; set; }

        public string strContent1 { get; set; }
        public string strContent2 { get; set; }
        public string strContent3 { get; set; }
        public string strContent4 { get; set; }
        public string strContent5 { get; set; }
        public string strContent6 { get; set; }
        public string strContent7 { get; set; }
        public string strContent8 { get; set; }
        public string strContent9 { get; set; }
        public string strContent10 { get; set; }
        public string strPageName { get; set; }
    
    }


}
