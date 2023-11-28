<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="Application_includes_header" %>
   <script>
       $(function () {
           $(document).attr("title", "GO-SWIFT | Single Window Portal | Department Of Industries, Govt. of Odisha");
       });
</script>


<section class="topbar">
    <div class="container">
        <ul>
            <li><a href="#" class="active">Font Size</a></li>
            <li><a href="#" class="font-plus">T+</a></li>
            <li><a href="#" class="font-normal">T</a></li>
            <li><a href="#" class="font-minus">T-</a></li>
            <li><a href="#">Walkthrough Videos</a></li>
            <li><a href="#">User Manual</a></li>
            <li><a href="#">Contact Us</a></li>
        </ul>
    </div>
</section>



<section class="header">
    <div class="container">
        <div class="row">
        <div class="col-sm-6">
            <h1 class="logo">
               <img src="newimages/govtlogo.png" />
               <img src="newimages/gologo.png" />
            </h1>
        </div>
        <div class="col-sm-6">
            <div class="searchbar">
                <div class="input-group">
                    <input type="text" class="form-control" name="email" placeholder="Email">
                    <span class="input-group-addon"><i class="fa fa-search"></i></span>
                  </div>
            </div>
        </div>
        </div>
    </div>
</section>



<section class="menubar">
    <div class="container">
        
        <nav class="navbar navbar-default">
          
            <div class="navbar-header">
              <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>                        
              </button>
             
            </div>
            <div class="collapse navbar-collapse" id="myNavbar">
              <ul class="nav navbar-nav nav-links">
                <li><a href="#" class="active">Home</a></li>
                <li><a href="#">At A Glance</a></li>
                <li><a href="#">Online Services</a></li>
                <li><a href="#">Department</a></li>
                <li><a href="#">Doing Business in Odisha</a></li>
                <li><a href="#">Faqs</a></li>
              </ul>
              
            </div>
          
        </nav>

        <%--<ul class="links">
            <li><a href="#" class="active">Home</a></li>
            <li><a href="#">At A Glance</a></li>
            <li><a href="#">Online Services</a></li>
            <li><a href="#">Department</a></li>
            <li><a href="#">Doing Business in Odisha</a></li>
            <li><a href="#">Faqs</a></li>
        </ul>--%>
        <ul class="implinks">
            <li><a href="#"><i class="fa fa-home"></i><label>Department Login</label></a></li>
            <li><a href="#"><i class="fa fa-lock"></i><label>Investor Login</label></a></li>
        </ul>


    </div>
</section>













<header>
   <div class="show-table" style="display:none;"> 
   <div class="top-header">
    <div class="navbar-header">
       <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse"> <span class="sr-only">Toggle navigation</span> <i class="fa fa-bars"></i></button>
      <a class="navbar-brand" href="Default.aspx" title="Single Window Portal"><h1><img src="images/Logo2.png"  alt="Government of Odisha"/><img src="images/logo.png"  alt="Go Swift"/></h1></a>
     
     <%-- <p>Single window Portal</p>--%>
       </div>
      <div class="tophdr-rightdiv">
    
     <div class="header-investorDetails     pull-right" id="userDetails" runat="server">
  <ul style="margin-bottom:0;">
  
   <li class="list-box user-admin dropdown">
        <div class="userbox">
      <div class="userimage"> <img src="images/usersquare.png" alt="user img"/> <%--<span class="caret"></span>--%></div>
      
     
      <div class="userdetails">
   <label>Welcome </label>
   <br />
   <asp:Label ID="lblUserName" runat="server"></asp:Label>
    </div>
   <div class="clearfix"></div>
   </div>
    <a href="LogOut.aspx" class="rightpnl-btn" title="Logout" data-toggle="tooltip" data-placement="bottom"><i class="fa fa-sign-out"></i></a>
  <a href="ChangePasswordInvestor.aspx" class="rightpnl-btn" title="Change Password" data-toggle="tooltip" data-placement="bottom"><i class="fa fa-key"></i></a>
  <a href="EditInvestorProfile.aspx" class="rightpnl-btn" title="Edit Profile" data-toggle="tooltip" data-placement="bottom"><i class="fa fa-pencil-square-o"></i>
  </a>
 
   
    </li>
    </ul>
   </div>
      <ul class="top-menu" style="display:none">
      <li><a href="UserManual.aspx" title="User Manual">User Manual</a></li>
      <li  ><a href="contactus.aspx?id=4" title="Contact Us"  >Contact Us</a></li>
    <li id="invlogin" runat="server"><a href="inestorlogin.aspx" class="loginbtn" title="Investor Login"><i class="fa fa-user"></i> Investor Login</a></li>
    <li id="lidept" runat="server"><a href="Portal/default.aspx" target="_blank" class="loginbtn" title="Department Login"><i class="fa fa-sign-in"></i> Department Login</a></li>
    </ul>
 
<div class="clearfix"></div>
    </div>
    <div class="clearfix"></div>
      </div>
       </div>
    <div class="clearfix"></div>
  <div class="navbar navbar-default " role="navigation" style="display:none">
    <div class="collapse navbar-collapse">
  
      <ul class="nav navbar-nav">
        <li class="homelink"><a href="Default.aspx" title="Home">Home</a></li>
        <li class="aboutlink"><a href="aboutus.aspx?id=1" title="At a Glance">At a Glance</a></li>

     <li class="service"> <a href="#" class="dropdown-toggle" title="Services" data-toggle="dropdown">Services <b class="caret"></b></a>
          <ul class="dropdown-menu multi-level">
       <%--     <li class="plSWAvailable"><a href="inestorlogin.aspx?id=2" title="Single Window Available" >Avail Services</a></li>--%>
            <li class="plSWClearance"><a href="SWClearance.aspx" title="Single Window Clearance" >Single Window Clearance</a></li>
            <li class="plCIFramework"><a href="CIFramework.aspx" title="Central Inspection Framework">Central Inspection Framework</a></li>
            <li class="plGOIplus"><a href="GOIPlus.aspx" title="GO iPLUS">GO<b class="text-danger">i</b>PLUS</a></li>
            <li class="plApaa"><a href="APAA.aspx" title="APAA">APAA</a></li>
            <li class="plGRedressal"><a href="GRedressal.aspx" title="Grievance Redressal">Grievance Redressal</a></li>
           
          </ul>
        </li>
        <li class="department"> <a href="#"  class="dropdown-toggle" title="Departments" data-toggle="dropdown">Departments <b class="caret"></b></a>
          <ul class="dropdown-menu multi-level" id="uldeparmentid" runat="server">
           <%--  <li class="plLesi"><a href="LabourESI.aspx" title="Labour & ESI" >Labour &amp; ESI</a></li>
             <li class="plDIndustries"><a href="http://industries.odisha.gov.in/" target="_blank" title="Department of Industries" >Department of Industries</a></li>
             <li class="plOPCBoard"><a href="http://ospcboard.org/" target="_blank" title="Odisha Pollution Control Board" >Odisha Pollution Control Board</a></li>
             <li class="plCCTax"><a href="https://odishatax.gov.in/" target="_blank" title="Commissionerate Of Commercial Tax" >Commissionerate of Commercial Tax</a></li>--%>
          </ul>
        </li> 
          <li class="dbodisha"> <a href="#"  class="dropdown-toggle" title="Doing Business in Odisha" data-toggle="dropdown">Doing Business in Odisha <b class="caret"></b></a>
          <ul class="dropdown-menu multi-level">
            <li class="plEDBusiness"><a href="EDBusiness.aspx" title="Ease of Doing Business">Ease of Doing Business</a></li>
            <li class="dropdown-submenu"><a href="javascript:void(0);" title="Policy Framework">Policy Framework</a>
                <ul class="dropdown-menu">
                  <li><a href="http://investodisha.gov.in/industrial-policy" target="_blank" title="Industrial Policy, 2015">Industrial Policy, 2015</a></li>
                  <li><a href="http://investodisha.gov.in/download/OdishaPolicy-Ecosystem-Key-Features.pdf" target="_blank" title="Policy and Incentive Framework">Policy &amp; Incentive Framework</a></li>
                  <li class="dropdown-submenu"><a href="javascript:void(0);" title="Sectoral Policies">Sectoral Policies</a>
                    <ul class="dropdown-menu">
                      <li><a href="http://investodisha.gov.in/apparel-policy" target="_blank" title="Apparel Policy">Apparel Policy</a></li>
                      <li><a href="http://investodisha.gov.in/biotechnology-policy" class="screenReader" title="Biotechnology Policy 2016">Biotechnology Policy 2016</a></li>
                      <li><a href="http://investodisha.gov.in/health-care-investment-promotion-policy" target="_blank" title="Health Care Investment Promotion Policy 2016">Health Care Investment Promotion Policy 2016</a></li>
                      <li><a href="http://investodisha.gov.in/ICT-policy" target="_blank" title="ICT Policy, 2014">ICT Policy, 2014</a></li>
                      <li><a href="http://investodisha.gov.in/odisha-fisheries-policy" target="_blank" title="Odisha Fisheries Policy 2015">Odisha Fisheries Policy 2015</a></li>
                      <li><a href="http://investodisha.gov.in/odisha-food-processing-policy" target="_blank" title="Odisha Food Processing Policy, 2016">Odisha Food Processing Policy, 2016</a></li>
                      <li><a href="http://investodisha.gov.in/tourism-policy" target="_blank" title="Odisha Tourism Policy, 2016">Odisha Tourism Policy, 2016</a></li>
                      <li><a href="http://investodisha.gov.in/pharmaceuticals-policy" target="_blank" title="Pharmaceuticals Policy 2016">Pharmaceuticals Policy 2016</a></li>
                      <li><a href="http://investodisha.gov.in/renewable-energy-policy" target="_blank" title="Renewable Energy Policy 2016">Renewable Energy Policy 2016</a></li>
                    </ul>
                  </li>
                  <li class="dropdown-submenu"><a href="javascript:void(0);" title="Other Policies">Other Policies</a>
                    <ul class="dropdown-menu">
                      <li><a href="http://investodisha.gov.in/odisha-MSME-policy" target="_blank" title="Odisha MSME Policy, 2016">Odisha MSME Policy, 2016</a></li>
                      <li><a href="http://investodisha.gov.in/policy-for-special-economic-zones" target="_blank" title="Policy for Special Economic Zones, 2015">Policy for Special Economic Zones, 2015</a></li>
                      <li><a href="http://investodisha.gov.in/startup-policy" target="_blank" title="Startup Policy 2016">Startup Policy 2016</a></li>
                    </ul>
                  </li>
                </ul>            
            </li>
            <li><a href="External.aspx?id=1" target="_blank" title="Investor's Guide">Investor's Guide</a></li>
            <li><a href="http://investodisha.gov.in/eodb/investment-facilitation-cell" target="_blank" title="Investment Facilitation Cell">Investment Facilitation Cell</a></li>
             <li class="plLIProjects"><a href="InvestibleProjects.aspx" title="List of Investible Projects">List of Investible Projects</a></li>  
             <li class="LIstatetaxs"><a href="StateTaxes.aspx" title="List of State Taxes">List of State Taxes</a></li>
          </ul>
        </li> 
         <li class="incentives"> <a href="#"  class="dropdown-toggle" title="Incentives" data-toggle="dropdown">Incentives <b class="caret"></b></a>
          <ul class="dropdown-menu multi-level">
          
            <li class="plIFramework"><a href="Download/OdishaPolicy-Ecosystem-Key-Features.pdf" target="_blank" title="Incentive Framework" >Incentive Framework</a></li>
            <li class="plOGuidelines"><a href="OperationalGuidelines.aspx" title="Operational Guidelines" >Operational Guidelines</a></li>
          </ul>
        </li> 
        
        <li class="eventlink"><a href="allEvents.aspx?id=5" title="Acts & Rules">Acts &amp; Rules</a></li>
        
         <li class="faqlink"><a href="faq.aspx?id=2" title="FAQ">FAQ</a></li>
      
        <li id="liDashBoardId" runat="server" style="display:none" class="dashboard" ><a href="InvesterDashboard.aspx">Dashboard</a></li>
       
        
      </ul>
    </div>
    <!--/.nav-collapse -->
    <div class="clearfix"></div>
    </div>

  <!--// Menu //--> 
  
</header>
