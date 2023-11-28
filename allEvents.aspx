<%@ Page Language="C#" AutoEventWireup="true" CodeFile="allEvents.aspx.cs" Inherits="allEvents" %>

<!DOCTYPE html>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<html>
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet"  />
     <title>SWP(Single Window Portal)</title>
    <script>
        $(document).ready(function () {
            $('.eventlink').addClass('active');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="navigatorheader-div eventheadernav">
            <div class="">
                <div class="col-sm-12">
                    <%--h2><i class="fa fa-newspaper-o" aria-hidden="true"></i> Latest News / Events</h2>--%>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx" title="Home page"><i class="fa fa-home"></i></a></li>
                        <li>Acts &amp; Rule</li></ul>
                </div>
                <div class="clearfix">
                </div>
            </div>
        </div>
        <div class="registration-div">
            <div class="col-md-12 news-grid-left">
                <div class="aboutcontent-sec">
                    <div class="news-grids" id="divabout" runat="server">
                        <%--<ul>
								<li>
									<div class="news-grid-left1">
									<img src="images/g3.jpg"  alt=" " class="img-responsive" />
									</div>
									<div class="news-grid-right1">
										<h4><a href="#">Mexico's oil giant is in uncharted waters</a></h4>
										<h5><i class="fa fa-calendar"></i> Published on <span>20.07.2017</span></h5>
										<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
									</div>
									<div class="clearfix"> </div>
								</li>
								<li>
									<div class="news-grid-left1">
									<img src="images/g3.jpg"  alt=" " class="img-responsive" />
									</div>
									<div class="news-grid-right1">
										<h4><a href="#">Mexico's oil giant is in uncharted waters</a></h4>
										<h5><i class="fa fa-calendar"></i> Published on <span>20.07.2017</span></h5>
										<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
									</div>
									<div class="clearfix"> </div>
								</li>
							<li>
									<div class="news-grid-left1">
									<img src="images/g3.jpg"  alt=" " class="img-responsive" />
									</div>
									<div class="news-grid-right1">
										<h4><a href="#">Mexico's oil giant is in uncharted waters</a></h4>
										<h5><i class="fa fa-calendar"></i> Published on <span>20.07.2017</span></h5>
										<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
									</div>
									<div class="clearfix"> </div>
								</li>
						<li>
									<div class="news-grid-left1">
									<img src="images/g3.jpg"  alt=" " class="img-responsive" />
									</div>
									<div class="news-grid-right1">
										<h4><a href="#">Mexico's oil giant is in uncharted waters</a></h4>
										<h5><i class="fa fa-calendar"></i> Published on <span>20.07.2017</span></h5>
										<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
									</div>
									<div class="clearfix"> </div>
								</li>
                                <li>
									<div class="news-grid-left1">
									<img src="images/g3.jpg"  alt=" " class="img-responsive" />
									</div>
									<div class="news-grid-right1">
										<h4><a href="#">Mexico's oil giant is in uncharted waters</a></h4>
										<h5><i class="fa fa-calendar"></i> Published on <span>20.07.2017</span></h5>
										<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
									</div>
									<div class="clearfix"> </div>
								</li>
							</ul>--%>
                    </div>
                </div>
            </div>
            <div class="clearfix">
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
