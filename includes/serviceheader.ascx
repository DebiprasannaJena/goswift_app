<%@ Control Language="C#" AutoEventWireup="true" CodeFile="serviceheader.ascx.cs" Inherits="includes_serviceheader" %>
<header >
  <div class="top-header usertop-header">
  <div class="container">
    <div class="navbar-header">
       <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse"> <span class="sr-only">Toggle navigation</span> <i class="fa fa-bars"></i></button>
      <a class="navbar-brand" href="InvesterProfile.aspx"><img src="../images/Logo2.png"  alt=""/><img src="../images/Logo.png"  alt=""/></a> </div>
   <div class="header-investorDetails pull-right">
  
  
   <li class="list-box user-admin dropdown">
      <div class="userdetails">
   <label><small>Welcome </small> </label>
   <br />
   <asp:Label ID="lblUserName" runat="server">Sandeep Sharma</asp:Label>
 
   </div>
      <a id="drop4" href="javascript:void(0)" role="button" class="dropdown-toggle" data-toggle="dropdown" aria-expanded="false"> 
      <div class="userimage"> <img src="../images/user.png" alt="user img"/> <span class="caret"></span></div>
      </a>
      <ul class="dropdown-menu sm">
<%--        <li class="dropdown-content"> <a href="javascript:void(0)"><i class="fa fa-eye"></i>&nbsp;Change Password</a><a href="javascript:void(0)"><i class="fa fa-sign-out"></i>&nbsp;&nbsp;Logout</a></li>
--%>     
        <li class="dropdown-content"> <a href="ChangePasswordInvestor.aspx"><i class="fa fa-eye"></i>&nbsp;Change Password</a>
       <a href="LogOut.aspx"><i class="fa fa-sign-out"></i>&nbsp;&nbsp;Logout</a></li>



 </ul>
    </li>
   </div>




   </div>
 
    <div class="clearfix"></div>
  </div>


  <!--// Menu //--> 

</header>