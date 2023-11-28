<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Webheader.ascx.vb" Inherits="includes_header" %>

<link href="css/font-awesome.min.css" rel="stylesheet">
<link href="css/bootstrap.min.css" rel="stylesheet">
<link href="css/open-sans.css" rel="stylesheet">
<link href="css/simple.min.css" rel="stylesheet">
<link href="css/jquerysctipttop.css">
<link href="css/custum.css" media="all" rel="stylesheet">
<script src="js/jquery.min.js"></script>
<script src="js/bootstrap.min.js"></script>
<script src="js/highcharts.js"></script>
<script src="js/exporting.js"></script>
<script src="js/jquery.marquee.js"></script>

<div class="top-header" >
	<div class="container">
		<div class="row">
			<div class="col-sm-5 col-md-5">
				<a href="index.aspx"><img src="images/Grs-logo.png" alt="logo" title="logo"/></a>
			</div>
			<div class="col-sm-7 col-md-5 col-lg-4 col-sm-offset-0 col-lg-offset-3 col-md-offset-2">
				<div class="col-sm-6 col-md-6 tollnobox">
						<i class="fa fa-volume-control-phone icon" aria-hidden="true"></i>
						<div class="tollno">Toll free no:<span>1967/1800</span></div>
					
				</div>
				<div class="col-sm-6 col-md-6">
					<div class="pull-right"><img src="images/e-bitaran-logo.png" alt="logo" title="logo"/></div>
				</div>
		</div>
		</div>
	</div>	
</div>



<div>
<nav class="navbar navbar-default">
 <div class="container">
  <div class="container-fluid">
    <!-- Brand and toggle get grouped for better mobile display -->
    <div class="navbar-header">
      <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
        <span class="sr-only">Toggle navigation</span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </button>
   
    </div>

    <!-- Collect the nav links, forms, and other content for toggling -->
    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
      <ul class="nav navbar-nav">
     <%--       <li class="dropdown">
          <a href="#" class="dropdown-toggle active" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">About us <span class="caret"></span></a>
<ul class="dropdown-menu">
            <li><a href="about_us.aspx">FSCW Dept</a></li>
            <li><a href="#">Another action</a></li>
          
          </ul>
     </li>--%>
       <li ><a href="index.aspx" style="font-size:23px;"><i class="fa fa-home" aria-hidden="true"></i> </a></li>
         <li><a href="about_us.aspx">About us </a></li>
        <li><a href="How_it_Works.aspx">Registation Process</a></li>
         <li><a href="#">Who's who</a></li>
          <li><a href="Contact_Us.aspx">Contact us</a></li>
      </ul>

   



   

		<ul class="pull-right">
       
      
			<div class="login"><a href="Login.aspx" target="_blank">Login</a></div>
		</ul>
          <ul class="pull-right1"> 
   <div class="odial">
  <%-- <a class="english" style="color:Black;" onclick="lnkBeng_Click">A</a>--%>
    <asp:LinkButton ID="lnkBOdia"
             runat="server" class="odia" ><img src="images/odia.png" /></asp:LinkButton>
   </div>
   </ul>
        <ul class="pull-right1"> 
    <div class="odial">
    <asp:LinkButton ID="lnkBeng"
             runat="server" class="english" >A</asp:LinkButton>
     <%--<a class="odia" onclick="lnkBOdia_Click"><img src="images/odia.png"/></a>--%>
    </div>    
   </ul>
 

    </div><!-- /.navbar-collapse -->
  </div><!-- /.container-fluid -->
  </nav>
</div>
		

<style>
.alphaa{font-size:16px;position:inherit;right: 165px;z-index:1000;top: 109px;/* border-right: 1px solid #fbfbfb; */}
.alphaa a {padding: 4px 4px;border: 1px solid #b7b7b7;text-align:center;color: #000;font-weight: bold;background: #fbfbfb;width: 30px;height: 30px;display: inline-block;}
.alphaa a img { padding:0px; margin:0px; margin-bottom:10px;}
.alphaa a:hover { text-decoration:none; background:#fff;}


.odial {
    background-color: #fbfbfb;border: 1px solid #b7b7b7;
    color: #fff;
    padding: 4px 25px 5px 10px;
    margin-top:10px;
    border-radius: 4px;
   text-align:center;
   font-weight:bold;
   color:#000;
}
  .pull-right1 {
    float: right!important;
    width: 50px;
}
</style>