<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PealMenu.ascx.cs" Inherits="includes_PealMenu" %>
<script>
    $(function () {
        $(document).attr("title", "GO-SWIFT | Single Window Portal | Department Of Industries, Govt. of Odisha");
    });
</script>
<ul class="nav nav-pills">
    <li class="menuproposal"><a href="../Proposals.aspx"><i class="fa fa-briefcase"></i>Proposals</a></li>
    <li class="menuservices"><a href="../DepartmentClearance.aspx"><i class="fa fa-wrench"></i>Services</a></li>
    <li class="menugrievance"><a href="../Grievance.aspx"><i class="fa fa-question-circle"></i>Grievance</a></li>
    <li class="menuPc"><a href="../pcViewPage.aspx"><i class="fa fa-certificate"></i>Production Certificate</a></li>
    <li class="menuincentive othermenuli"><a href="../incentives/incentiveoffered.aspx"><i class="fa fa-money"></i>Incentive</a></li>
    <li class="menumanage othermenuli" runat="server" id="managemenu"><a href="javascript:void(0);"><i class="fa fa-money"></i>Manage</a>
        <div class="othermenus">
            <ul>
                <li><a href="../InvestorPortal/User_Management.aspx">User Management</a></li>
                <li><a href="../InvestorPortal/User_Creation.aspx">User Creation</a></li>
                <li><a href="../InvestorPortal/Drafted_User.aspx">Drafted User</a></li>
                <li><a href="../InvestorPortal/Set_User_Permission.aspx">Application Permission</a></li>
            </ul>
        </div>
    </li>
    <li class="othermenulist othermenuli" runat="server" id="othermenulist"><a href="javascript:void(0);"><i class="fa fa-th"></i>Other Applications</a>
        <div class="othermenus">
            <ul id="OtherApps" runat="server">
            </ul>
        </div>
    </li>
    <li class="menudashboard"><a href="../InvesterDashboard.aspx"><i class="fa fa-tachometer"></i>Dashboard</a></li>
</ul>
