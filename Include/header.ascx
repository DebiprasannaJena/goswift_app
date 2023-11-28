<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="includes_header" %>
<script type="text/javascript" language="JavaScript">
    // window.history.forward();
    function startclock() {
        var thetime = new Date();
        var nhours = thetime.getHours();
        var nmins = thetime.getMinutes();
        var nsecn = thetime.getSeconds();
        var nday = thetime.getDay();
        var nmonth = thetime.getMonth();
        var ntoday = thetime.getDate();
        var nyear = thetime.getYear();
        var AorP = " ";

        if (nhours >= 12)
            AorP = "PM";
        else
            AorP = "AM";

        if (nhours >= 13)
            nhours -= 12;

        if (nhours == 0)
            nhours = 12;

        if (nsecn < 10)
            nsecn = "0" + nsecn;

        if (nmins < 10)
            nmins = "0" + nmins;

        if (nday == 0)
            nday = "Sunday";
        if (nday == 1)
            nday = "Monday";
        if (nday == 2)
            nday = "Tuesday";
        if (nday == 3)
            nday = "Wednesday";
        if (nday == 4)
            nday = "Thursday";
        if (nday == 5)
            nday = "Friday";
        if (nday == 6)
            nday = "Saturday";

        nmonth += 1;

        if (nyear <= 99)
            nyear = "19" + nyear;

        if ((nyear > 99) && (nyear < 2000))
            nyear += 1900;
        var monthnameShort = new Array("", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec")
        var monthnameFull = new Array("", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December")

        document.getElementById('clock').innerHTML = "" + nday + ", " + monthnameFull[nmonth] + " " + ntoday + ", " + nyear + "  &nbsp;" + nhours + ": " + nmins + " " + AorP + "";

        setTimeout('startclock()', 1000);

    }
</script>
<title></title>
 
<%--<div class="header">
    <div class="navbar navbar-inverse">
        <div class="navbar-inner">
            <div class="container">
                <div class="brand">
                <a href="../Dashboard/Dashboard.aspx?linkm=<%=Request.QueryString["linkm"] %>&linkn=<%=Request.QueryString["linkn"] %>&ranNum=<%=Session["RandomNo"]%>" class="navbar-brand"><span class="navbar-logo">
                         <img src="../img/CICG-Logo.png" alt="Agenda Monitoring System"
                        title="Agenda Monitoring System"
                        border="0" align="absmiddle" style="float: left; height: 60px;" /></span> </a>
                   
                    <h4>
                        Agenda Monitoring System</h4>
                </div>
                <div class="nav-collapse collapse">
                    <ul class="nav navbar-nav pull-right">
                        <li class="dropdown" style="margin-top: 15px; text-align: right; padding-right: 20px;">
                            Welcome <strong>
                                <asp:Label ID="lblName" runat="server"></asp:Label></strong><br />
                            <asp:Label ID="lblDesig" runat="server"></asp:Label>
                            <asp:Label ID="lblLoc" runat="server"></asp:Label>
                            <div id="clock">
                            </div>
                            <script language="javascript" type="Text/javascript">
                                startclock();
                            </script>
                        </li>
                        <li class="dropdown"><a href="#" data-toggle="dropdown">
                            <div class="menu_class">
                                Settings <b class="caret"></b>
                            </div>
                        </a>
                            <ul class="dropdown-menu">
                                <%--<li><a href="#"><i class="icon-user"></i>&nbsp;Admin Console</a> </li>--%>
                   <%--             <li><a href="../Dashboard/changePassword.aspx?linkm=1&linkn=0&ranNum=<%=Session["RandomNo"]%>">
                                    <i class="icon-lock"></i>&nbsp;Change Password</a></li>
                                <% if (Session["adminstat"].ToString() == "super")
                                   { %>
                                <li><a href="../Console/AdminDefault.aspx?dwXb=<%=Session["RandomNo"]%>">
                                    <img src="../img/console.gif" alt="Dashboard" border="0" align="absmiddle" />Admin
                                    Console</a></li><% }%>
                                <li><a href="../Logout.aspx"><i class="icon-off"></i>&nbsp;Logout</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</div>--%>
<div id="preloader">
         <div id="status"></div>
      </div>
         
         <header class="main-header">
         <!--====== Logo ======-->
            <a href="../Dashboard/Default.aspx" class="logo">
              
               <span class="logo-mini">
               <img src="../images/portallogo.png" alt="Logo" />
               </span>
               <span class="logo-lg">
               <img src="../images/portallogo.png" alt="Logo"/>
               <img src="../images/logo.png" alt="Logo"/>
               </span>
            </a>
            <!--====== Logo ======-->
            <!--====== Header Navbar ======-->
            <nav class="navbar navbar-static-top">
               <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
                  <!-- Sidebar toggle button-->
                  <span class="sr-only">Toggle navigation</span>
                  <span class="pe-7s-angle-left-circle"></span>
               </a>
               <!-- searchbar-->
               
             <div class="navbar-custom-menu">
                  <ul  class="nav navbar-nav">
                  
                   <%--  <!-- Messages -->
                     <li class="dropdown messages-menu">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <i class="fa fa-envelope-o"></i>
                        <span class="label label-success">4</span>
                        </a>
                        <ul class="dropdown-menu">
                           <li>
                              <ul class="menu">
                                 <li>
                                    <!-- start message -->
                                    <a href="#" class="border-gray">
                                       <div class="pull-left">
                                          <img src="../images/admin.png" class="img-circle" alt="User Image">
                                       </div>
                                       <h4>Ronaldo</h4>
                                       <p>Please oreder 10 pices of kits..</p>
                                       <span class="badge badge-success badge-massege"><small>15 hours ago</small>
                                       </span>
                                    </a>
                                 </li>
                                 <li>
                                    <a href="#" class="border-gray">
                                       <div class="pull-left">
                                          <img src="../images/admin.png" class="img-circle" alt="User Image">
                                       </div>
                                       <h4>Leo messi</h4>
                                       <p>Please oreder 10 pices of Sheos..</p>
                                       <span class="badge badge-info badge-massege"><small>6 days ago</small>
                                       </span>   
                                    </a>
                                 </li>
                                 <li>
                                    <a href="#" class="border-gray">
                                       <div class="pull-left" >
                                          <img src="../images/admin.png" class="img-circle" alt="User Image">
                                       </div>
                                       <h4>Modric</h4>
                                       <p>Please oreder 6 pices of bats..</p>
                                       <span class="badge badge-info badge-massege"><small>1 hour ago</small>
                                       </span>
                                    </a>
                                 </li>
                                 <li>
                                    <a href="#" class="border-gray">
                                       <div class="pull-left">
                                          <img src="../images/admin.png" class="img-circle" alt="User Image">
                                       </div>
                                       <h4>Salman</h4>
                                       <p>Hello i want 4 uefa footballs</p>
                                       <span class="badge badge-danger badge-massege">
                                       <small>6 days ago</small>
                                       </span>  
                                    </a>
                                 </li>
                                 <li>
                                    <a href="#" class="border-gray">
                                       <div class="pull-left">
                                          <img src="../images/admin.png" class="img-circle" alt="User Image">
                                       </div>
                                       <h4>Sergio Ramos</h4>
                                       <p>Hello i want 9 uefa footballs</p>
                                       <span class="badge badge-info badge-massege"><small>5 hours ago</small>
                                       </span>
                                    </a>
                                 </li>
                              </ul>
                           </li>
                        </ul>
                     </li>
                     <!-- Notifications -->
                     <li class="dropdown notifications-menu">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <i class="fa fa-bell-o"></i>
                        <span class="label label-warning">7</span>
                        </a>
                        <ul class="dropdown-menu">
                           <li>
                              <ul class="menu">
                                 <li>
                                    <a href="#" class="border-gray">
                                    <i class="fa fa-dot-circle-o color-green"></i>Change Your font style</a>
                                 </li>
                                 <li><a href="#" class="border-gray">
                                    <i class="fa fa-dot-circle-o color-red"></i>
                                    check the system ststus..</a>
                                 </li>
                                 <li><a href="#" class="border-gray">
                                    <i class="fa fa-dot-circle-o color-yellow"></i>
                                    Add more admin...</a>
                                 </li>
                                 <li><a href="#" class="border-gray">
                                    <i class="fa fa-dot-circle-o color-violet"></i> Add more clients and order</a>
                                 </li>
                                 <li><a href="#" class="border-gray">
                                    <i class="fa fa-dot-circle-o color-yellow"></i>
                                    Add more admin...</a>
                                 </li>
                                 <li><a href="#" class="border-gray">
                                    <i class="fa fa-dot-circle-o color-violet"></i> Add more clients and order</a>
                                 </li>
                              </ul>
                           </li>
                        </ul>
                     </li>
                     <!-- Tasks -->
                     <li class="dropdown tasks-menu">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <i class="fa fa-wpforms"></i>
                        <span class="label label-danger">6</span>
                        </a>
                        <ul class="dropdown-menu">
                           <li>
                              <ul class="menu">
                                 <li>
                                    <!-- Task item -->
                                    <a href="#" class="border-gray">
                                       <span class="label label-success pull-right">50%</span>
                                       <h3><i class="fa fa-check-circle"></i> Theme color should be change</h3>
                                    </a>
                                 </li>
                                 <!-- end task item -->
                                 <li>
                                    <!-- Task item -->
                                    <a href="#" class="border-gray">
                                       <span class="label label-warning pull-right">90%</span>
                                       <h3><i class="fa fa-check-circle"></i> Fix Error and bugs</h3>
                                    </a>
                                 </li>
                                 <!-- end task item -->
                                 <li>
                                    <!-- Task item -->
                                    <a href="#" class="border-gray">
                                       <span class="label label-danger pull-right">80%</span>
                                       <h3><i class="fa fa-check-circle"></i> Sidebar color change</h3>
                                    </a>
                                 </li>
                                 <!-- end task item -->
                                 <li>
                                    <!-- Task item -->
                                    <a href="#" class="border-gray">
                                       <span class="label label-info pull-right">30%</span>   
                                       <h3><i class="fa fa-check-circle"></i> font-family should be change</h3>
                                    </a>
                                 </li>
                                 <li>
                                    <!-- Task item -->
                                    <a href="#"  class="border-gray">
                                       <span class="label label-success pull-right">60%</span>
                                       <h3><i class="fa fa-check-circle"></i> Fix the database Error</h3>
                                    </a>
                                 </li>
                                 <li>
                                    <!-- Task item -->
                                    <a href="#"  class="border-gray">
                                       <span class="label label-info pull-right">20%</span>
                                       <h3><i class="fa fa-check-circle"></i> data table data missing</h3>
                                    </a>
                                 </li>
                                 <!-- end task item -->
                              </ul>
                           </li>
                        </ul>
                     </li>--%>
                   
                     <!-- user -->
                     <li class="dropdown dropdown-user">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <img src="../images/admin.png" height="45" alt=""><span><asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>&nbsp;<i class="fa fa-th"></i></span></a>
                        <ul class="dropdown-menu" >
                         <%--  <li>
                              <a href="profile.html">
                              <i class="fa fa-user"></i> Edit Profile</a>
                           </li>--%>
                           <li><a href="../admin/DeptChangePassword.aspx"><i class="fa fa-inbox"></i> Change Password</a></li>
                            <li id="adminconsolelink" runat="server"><a href="../Console/AdminDefault.aspx?dwXb=<%=Session["RandomNo"]%>"><i class="fa fa-inbox" ></i>Admin Console</a></li>
                           <li><a href="../LogOut.aspx">
                              <i class="fa fa-sign-out"></i> Signout</a>
                           </li>
                        </ul>
                     </li>
                  </ul>
               </div>
            </nav>
              <!--====== Header Navbar ======-->
         </header>
         <!-- =============================================== -->
        
      
<div class="clearfix"></div>
