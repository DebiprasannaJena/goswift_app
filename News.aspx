<%@ Page Language="C#" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="allEvents" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">

 <head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
     <title>SWP(Single Window Portal)</title>
  
</head>
<style>
.rightnews ul li.active a {
   color:#f00
}
</style>
<body>
    <form id="form1" runat="server">
     <div class="container">
    <uc2:header ID="header" runat="server" />
   <div class="navigatorheader-div eventheadernav">
        <div class="">
        <div class="col-sm-12">
        <%--h2><i class="fa fa-newspaper-o" aria-hidden="true"></i> Latest News / Events</h2>--%>
         <ul class="breadcrumb"><li><a href="Default.aspx" title="Home page"><i class="fa fa-home"></i></a></li><li>Latest News</li></ul>
         </div>
         <div class="clearfix"></div>
         </div>
    </div>
    <div class="registration-div">
        <div class="">
         <div class="news-grids">
						<div class="col-md-8 news-grid-left" id="divabout" runat="server">
							  <ul>
                               <asp:Repeater ID="RepNews" runat="server">
                        <ItemTemplate>                           
                                <li><div class="news-grid-left1">
									<img src="CMSImageGallery/<%#Eval("VCH_IMAGE")%>"  alt="" class="img-responsive" />
									</div>
									<div class="news-grid-right1">
										<h4><a href="#"><%#Eval("VCH_HEADING")%></a></h4>
										<h5><i class="fa fa-calendar"></i> Published on <span> <%#Eval("Publish_ON")%></span></h5>
										<p> <%#Eval("VCH_CONTENT")%></p>
									</div>
									<div class="clearfix"> </div>
                                </a></li>
                        </ItemTemplate>
                    </asp:Repeater>	</ul>						
						</div>
					<div class="col-md-4" >
                   <div class="rightpanel">
    <div class="rightnews bglight-gray ">
                    <h2>
                        All News 
                    </h2> 
                     <ul class="newsul"  id="ultdnews" runat="server">
                     <asp:Repeater ID="repsidenews" runat="server" DataMember="INT_ID" 
                          OnItemDataBound="repsidenews_OnItemDataBound"   onitemcommand="repsidenews_ItemCommand" >
                     
                        <ItemTemplate>
                           
                                <li id="liNews" runat="server"><a href="News.aspx?annid=<%#Eval("INT_ID")%>"><span>
                                    <img src= "CMSImageGallery/<%#Eval("VCH_IMAGE")%>" class="img-sec" alt="newsimg"  /></span> <%#Eval("VCH_HEADING")%>
                                    <div class="clearfix">
                                    </div>
                                </a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                <%--      <li class="active"><a href="#"><span>
                                    <img src="CMSImageGallery/TO_IW_1501218184.jpg" class="img-sec" alt="newsimg"></span> Foreign tourist arrivals to grow 18% in 2017
                                    <div class="clearfix">
                                    </div>
                                </a></li>
                        
                          
                                <li><a href="#"><span>
                                    <img src="CMSImageGallery/TO_IW_1501218184.jpg" class="img-sec" alt="newsimg"></span> Odisha showcases investment potential at investors’ meet
                                    <div class="clearfix">
                                    </div>
                                </a></li>
                         <li><a href="#"><span>
                                    <img src="CMSImageGallery/TO_IW_1501218184.jpg" class="img-sec" alt="newsimg"></span> Foreign tourist arrivals to grow 18% in 2017
                                    <div class="clearfix">
                                    </div>
                                </a></li>
                        
                          
                                <li><a href="#"><span>
                                    <img src="CMSImageGallery/TO_IW_1501218184.jpg" class="img-sec" alt="newsimg"></span> Odisha showcases investment potential at investors’ meet
                                    <div class="clearfix">
                                    </div>
                                </a></li>
                          
                                <li><a href="#"><span>
                                    <img src="CMSImageGallery/TO_IW_1503551804.jpg" class="img-sec" alt="newsimg"></span> Odisha Govt, FICCI join hands to promote culture, tourism
                                    <div class="clearfix">
                                    </div>
                                </a></li>
                        
                          
                                <li><a href="#"><span>
                                    <img src="CMSImageGallery/TO_IW_1503902161.jpg" class="img-sec" alt="newsimg"></span> In Odisha, approved investments show tilt to emerging sectors
                                    <div class="clearfix">
                                    </div>
                                </a></li>
                        
                          
                                <li><a href="#"><span>
                                    <img src="CMSImageGallery/TO_IW_1504073847.jpg" class="img-sec" alt="newsimg"></span> Odisha receives Rs 466 crore of investment proposals
                                    <div class="clearfix">
                                    </div>
                                </a></li>
                        
                          
                                <li><a href="#"><span>
                                    <img src="CMSImageGallery/g3.jpg" class="img-sec" alt="newsimg"></span> Mexicos oil giant is in uncharted waters
                                    <div class="clearfix">
                                    </div>
                                </a></li>
                        
                          
                                <li><a href="#"><span>
                                    <img src="CMSImageGallery/The INTERN (1).jpg" class="img-sec" alt="newsimg"></span> Etiam imperdiet  volutpat libero eu tristique. Aenean, rutrum felis in...
                                    <div class="clearfix">
                                    </div>
                                </a></li>--%>
                      </ul>
                </div>
               
   <a class="infowizard" href="javascript:void(0)" title="Info Wizard">Info Wizard &nbsp;<i class="fa fa-angle-right"></i></a>
   </div>
                    </div>
						<div class="clearfix"> </div>
					</div>
        </div>
    </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>