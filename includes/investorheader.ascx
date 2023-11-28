<%@ Control Language="C#" AutoEventWireup="true" CodeFile="investorheader.ascx.cs"
    Inherits="includes_investorheader" %>
<script>
    $(function () {
        $(document).attr("title", "GO-SWIFT | Single Window Portal | Department Of Industries, Govt. of Odisha");

        ////Right Key Disable
        //document.addEventListener('contextmenu', function(e) {
        //       e.preventDefault();
        //    });
        //    document.onkeydown = function(e) {
        //          if(event.keyCode == 123) {
        //             return false;
        //          }  
        //        else if ((event.ctrlKey && event.shiftKey && event.keyCode == 73) || (event.ctrlKey && event.shiftKey && event.keyCode == 74)) {
        //              return false;
        //        }
        //    }
    });
</script>
<header class="form-hdr">
    <div class="show-table">
        <div class="top-header usertop-header">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse"><span class="sr-only">Toggle navigation</span> <i class="fa fa-bars"></i></button>
                <a class="navbar-brand" href="Default.aspx" title="Single Window Portal">
                    <h1>
                        <img src="images/Logo2.png" alt="Government of Odisha" /><img src="images/Logo.png" alt="GO Swift" /></h1>
                </a>
                <div class="clearfix"></div>
            </div>
            <div class="tophdr-rightdiv">
                <div class="header-investorDetails pull-right">
                    <ul style="margin-bottom: 0;">
                        <li class="list-box user-admin dropdown">
                            <div class="userbox">
                                <div class="userimage">
                                    <img src="images/usersquare.png" alt="user img" />
                                </div>
                                <div class="userdetails  pull-right">
                                    <label>Welcome </label>
                                    <br />
                                    <asp:Label ID="LblUserName" runat="server"></asp:Label>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <a href="LogOut.aspx" class="rightpnl-btn" title="Logout" data-toggle="tooltip" data-placement="bottom"><i class="fa fa-sign-out"></i></a>
                            <a href="ChangePasswordInvestor.aspx" class="rightpnl-btn" title="Change Password" data-toggle="tooltip" data-placement="bottom"><i class="fa fa-key"></i></a>
                            <a href="EditInvestorProfile.aspx" class="rightpnl-btn" title="Edit Profile" data-toggle="tooltip" data-placement="bottom"><i class="fa fa-pencil-square-o"></i></a>
                        </li>
                    </ul>
                </div>
                <ul class="top-menu">
                    <li><a href="UserManual.aspx" target="_blank" title="User Manual">User Manual</a></li>
                </ul>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>     
</header>
