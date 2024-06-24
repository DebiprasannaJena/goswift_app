<%@ Control Language="C#" AutoEventWireup="true" CodeFile="webheader.ascx.cs" Inherits="Application_includes_header" %>
<script type="text/javascript">
    $(function () {
        $(document).attr("title", "GO-SWIFT | Single Window Portal | Department Of Industries, Govt. of Odisha");
        //document.addEventListener('contextmenu', function(e) {
        //   e.preventDefault();
        //});
        //document.onkeydown = function(e) {
        //      if(event.keyCode == 123) {
        //         return false;
        //    }    
        //    else if ((event.ctrlKey && event.shiftKey && event.keyCode == 73) || (event.ctrlKey && event.shiftKey && event.keyCode == 74)) {
        //          return false;
        //    }
        //}
    });
</script>
<div runat="server" visible="false" id="divScrollingText" style="color: orangered; font-size: large;">
</div>
<section class="topbar">
    <div class="container">
        <div class="row" style="width: 1172px;">
            <div class="helpline">
                <i class="fa fa-phone"></i>
                <h3>Toll Free Helpline - <span>1800 345 7157</span><br />
                    Help Desk Contact No  -<span>91-8895889513</span>
                    <small>(Timing 10.00 AM to 6.00 PM on working days)</small></h3>
                <h4><i class="fa fa-envelope"></i>support[dot]investodisha[at]nic[dot]in</h4>
            </div>

            <ul class="nav navbar-nav nav-links">
                <li><a class="scrdr" href="http://www.nvda-project.org/" target="_blank"><i class="fa fa-fax"></i></a></li>
                <li><a href="#" class="font-plus">T+</a></li>
                <li><a href="#" class="font-normal active">T</a></li>
                <li><a href="#" class="font-minus">T-</a></li>
               <%-- <li><a href="https://www.youtube.com/watch?v=jqXi-AhpDg0&t=0s&index=2&list=PLxlD_G5mrgNItIPSMkbNSj-wDKCCR8MiH" target="_blank">Walkthrough Videos</a></li>--%>
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">User Manual<span class="caret"></span></a>
                    <ul class="dropdown-menu" style="min-width: 93px !important; background-color: black;">
                        <li><a href="#" style="position: static;">REGISTRATION</a></li>
                        <li><a href="images/UserManual.pdf" target="_blank" style="position: static;">PEAL</a></li>
                    </ul>
                </li>
                <%--<li><a href="https://investodisha.gov.in/goswift/contactus.aspx?enc=UgkSQR+edDnLp3fXL8+9Bw==" target="_blank">Contact Us</a></li>--%>
                <li><a href="../contactus.aspx" target="_blank">Contact Us</a></li>
            </ul>
        </div>
    </div>
</section>
<section class="header">
    <div class="container">
        <div class="row">
            <div class="col-sm-6">
                <h1 class="logo">
                    <img src="images/govtlogo.png" />
                    <img src="images/gologo.png" />
                </h1>
            </div>
            <div class="col-sm-6">
                <%--<div class="searchbar">
                <div class="input-group">
                    <input type="text" class="form-control" name="email" placeholder="Search Term">
                    <span class="input-group-addon"><i class="fa fa-search"></i></span>
                  </div>
            </div>--%>
            </div>
        </div>
    </div>
</section>
<section class="menubar">
    <div class="container">
        <div class="row">
            <nav class="navbar navbar-default">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>

                </div>
                <div class="collapse navbar-collapse" runat="server" id="myNavbar">
                </div>

                <ul class="implinks">
                    <li>
                        <a href="javascript:void(0)"><i class="fa fa-sign-in"></i>
                            <label>Login</label></a>
                        <ul>
                            <li><a href="Login.aspx" title="Investor Login">Investor Login</a></li>
                            <li><a href="portal" target="_blank" title="Department Login">Department Login</a></li>
                        </ul>
                    </li>

                </ul>

                <div class="clearfix"></div>
            </nav>
        </div>

         <%--Used for CMS Modal (Dynamically added at Server Side)--%>
        <div id="divModal" runat="server"></div>

    </div>
</section>
