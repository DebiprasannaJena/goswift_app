<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PlainHeader.ascx.cs" Inherits="includes_PlainHeader" %>
<script>
    $(function () {
        $(document).attr("title", "GO-SWIFT | Single Window Portal | Department Of Industries, Govt. of Odisha");
    });        
</script>
<div runat="server" visible="false" id="divScrollingText" style="color: orangered;
    font-size: large;">
</div>
<section class="topbar">
    <div class="container">
    <div class="row">     
      <%--  <div class="helpline">
            <i class="fa fa-phone"></i><h3>Toll Free Helpline - <span>1800 345 7157</span><small>(Timing 10.00 AM to 6.00 PM on working days)</small></h3>
            <h4><i class="fa fa-envelope"></i>support[dot]investodisha[at]nic[dot]in</h4>
        </div>
        --%>

        <div style="background-color:#333;height:50px;width:100%;">&nbsp;</div>

   


      <%-- <ul class="nav navbar-nav nav-links">
            <li><a class="scrdr" href="http://www.nvda-project.org/" target="_blank"><i class="fa fa-fax"></i></a></li>
            <li><a href="#" class="font-plus">T+</a></li>
            <li><a href="#" class="font-normal active">T</a></li>
            <li><a href="#" class="font-minus">T-</a></li>
            <li><a href="https://www.youtube.com/watch?v=jqXi-AhpDg0&t=0s&index=2&list=PLxlD_G5mrgNItIPSMkbNSj-wDKCCR8MiH" target="_blank">Walkthrough Videos</a></li>            
            <li class="dropdown">
                <a class="dropdown-toggle" data-toggle="dropdown" href="#">User Manual<span class="caret"></span></a>
                <ul class="dropdown-menu" style="min-width: 93px !important;background-color:black;">                                    
                    <li><a href="#" style="position:static;">REGISTRATION</a></li>   
                    <li><a href="images/UserManual.pdf" target="_blank" style="position:static;">PEAL</a></li>  
                </ul>
            </li>
          
             <li><a href="~/../contactus.aspx" target="_blank">Contact Us</a></li>
        </ul>--%>
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
<div style="background-color: #ed3338; height: 40px; width: 100%;">
    &nbsp;</div>
