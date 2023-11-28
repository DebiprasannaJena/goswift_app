<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NonIndustryWebHeader.ascx.cs" Inherits="includes_NonIndustryWebHeader" %>
<header class="form-hdr">
    <div class="show-table">
        <div class="top-header usertop-header">
            <%--<div class="investorheader">--%>
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse"><span class="sr-only">Toggle navigation</span> <i class="fa fa-bars"></i></button>
                <a class="navbar-brand" href="../Default.aspx" title="Single Window Portal">
                    <h1>
                        <img src="../images/Logo2.png" alt="Government of Odisha" /><img src="../images/Logo.png" alt="GO Swift" /></h1>
                </a>

                <%--    <p>Single window Portal</p>--%>
            </div>
            <div class="tophdr-rightdiv">

                <div class="header-investorDetails pull-right" id="userDetails" runat="server">
                    <ul>
                        <li class="list-box user-admin dropdown">
                            <div class="userbox">
                                <div class="userimage">
                                    <img src="../images/usersquare.png" alt="user img" />
                                </div>
                                <div class="userdetails">
                                    <label>Welcome</label>
                                    <span class="username">
                                        <asp:Label ID="lblUserName" runat="server"></asp:Label></span>
                                </div>
                            </div>
                            <a href="../LogOut.aspx" class="rightpnl-btn" title="Logout" data-toggle="tooltip" data-placement="bottom"><i class="fa fa-sign-out"></i></a>
                            <a href="../ChangePasswordNonInvestor.aspx" class="rightpnl-btn" title="Change Password" data-toggle="tooltip" data-placement="bottom"><i class="fa fa-key"></i></a>
                            <a href="../EditNonInvestorProfile.aspx" class="rightpnl-btn" title="Edit Profile" data-toggle="tooltip" data-placement="bottom"><i class="fa fa-pencil-square-o"></i></a>
                        </li>
                    </ul>
                </div>
                <ul class="top-menu">
                    <li><a href="../UserManual.aspx" target="_blank" title="User Manual">User Manual</a></li>
                </ul>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
    <div class="clearfix"></div>
    <!--// Menu //-->
</header>
