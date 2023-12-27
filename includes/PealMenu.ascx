<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PealMenu.ascx.cs" Inherits="includes_PealMenu" %>
<script>
    $(function () {
        $(document).attr("title", "GO-SWIFT | Single Window Portal | Department Of Industries, Govt. of Odisha");
    });
</script>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <li class="menunswsredirect" id="nswsmenu" runat="server">
        <asp:LinkButton ID="LinkBtnNswsRedirect" runat="server" OnClick="LinkBtnNswsRedirect_Click" ><i class="fa fa-tachometer"></i>Go 
            to<br /> NSWS</asp:LinkButton>
    </li>
</ul>

<asp:LinkButton ID="LnkBtnTarget1" runat="server"></asp:LinkButton>
<cc1:ModalPopupExtender id="ServiceModalPopup" behaviorid="mpe1" runat="server" popupcontrolid="pnlPopup"
    TargetControlID="LnkBtnTarget1" BackgroundCssClass="modalBackground" CancelControlID="LinkPopupclose">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none; width: 600px; height: 330px;"
    ToolTip="Important Notes">
    <div class="mhead">
        <asp:LinkButton ID="LinkPopupclose" runat="server" OnClick="LinkPopupclose_Click"><i class="fa fa-close"></i></asp:LinkButton>
        <%--<span class="glyphicon glyphicon-alert"></span>--%>
        <h4 class="modal-title">Important Notes</h4>
    </div>
    <div class="modal-body">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12">
                        <ol style="padding-left: 20px;">
                            <li style="padding-bottom: 7px;">If you are an <span style="font-weight: bolder; color: red;">Industrial</span> unit (As defined by Govt of India from time to time either Large or MSME industrial unit) then click on the <span style="color: #5cb85c; font-weight: bolder;">Industrial User Registration </span>button.</li>
                           
                        </ol>
                    </div>
                </div>
                <div class="mFooter">
                    <div class="row">
                        <div class="col-sm-6 text-left">
                            <asp:Button ID="BtnYes" runat="server" Text="Yes" CssClass="btn btn-success" OnClick="BtnYes_Click" />
                        </div>
                        <div class="col-sm-6">
                            <asp:Button ID="BtnNo" runat="server" Text="No" CssClass="btn btn-primary" OnClick="BtnNo_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
</asp:Panel>

<asp:LinkButton ID="LnkBtnTarget2" runat="server"></asp:LinkButton>
<cc1:ModalPopupExtender id="InformationModalpopup" behaviorid="mpe2" runat="server" popupcontrolid="Popuppanel"
   BackgroundCssClass="modalBackground" CancelControlID="LinkBtnPopupclose" TargetControlID="LnkBtnTarget2" >
</cc1:ModalPopupExtender>

<asp:Panel ID="Popuppanel" runat="server" CssClass="modalPopup" Style="display: none; width: 350px; height: 230px;"
    ToolTip="Important Notes">
    <div class="mhead">
        <asp:LinkButton ID="LinkBtnPopupclose" runat="server" OnClick="LinkBtnPopupclose_Click"><i class="fa fa-close"></i></asp:LinkButton>
        <h4 class="modal-title">Alert </h4>
    </div>
    <div class="modal-body">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div class="row">
                    <div class="col-sm-12">
                        <label runat="server" id="lbl_message"></label>

                    </div>
                    <div class="modal-footer">
                        <div class="row">

                            <div class="col-sm-6 text-left">
                                <asp:Button ID="Btn_ok" runat="server" Text="Ok" CssClass="btn btn-primary" OnClick="Btn_ok_Click" />
                            </div>

                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
     
</asp:Panel>

<asp:LinkButton ID="LnkBtnTarget3" runat="server"></asp:LinkButton>
<cc1:modalpopupextender id="InformationModalpopup2" behaviorid="mpe3" runat="server" popupcontrolid="pnlPopup2"
    TargetControlID="LnkBtnTarget3" backgroundcssclass="modalBackground" CancelControlID="Popupclose">
</cc1:modalpopupextender>
<asp:Panel ID="pnlPopup2" runat="server" CssClass="modalPopup" Style="display: none; width: 600px; height: 330px;"
    ToolTip="Important Notes">
    <div class="mhead">
        <asp:LinkButton ID="Popupclose" runat="server" OnClick="Popupclose_Click"><i class="fa fa-close"></i></asp:LinkButton>
        <%--<span class="glyphicon glyphicon-alert"></span>--%>
        <h4 class="modal-title">Important Notes</h4>
    </div>
    <div class="modal-body">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12">
                        <ol style="padding-left: 20px;">
                            <li style="padding-bottom: 7px;">Your CIN ,entity type was not updated in GOSWIFT database ,If you need to update then you click yes button or go to edit profile.</li>
                           
                        </ol>
                    </div>
                </div>
                <div class="mFooter">
                    <div class="row">
                        <div class="col-sm-6 text-left">
                            <asp:Button ID="Btn_yes" runat="server" Text="Yes" CssClass="btn btn-success" OnClick="Btn_yes_Click" />
                        </div>
                        <div class="col-sm-6">
                            <asp:Button ID="Btn_no" runat="server" Text="No" CssClass="btn btn-primary" OnClick="Btn_no_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
</asp:Panel>
