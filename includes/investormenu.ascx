<%@ Control Language="C#" AutoEventWireup="true" CodeFile="investormenu.ascx.cs"
    Inherits="includes_investormenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<ul class="nav nav-pills">
    <li class="menuproposal"><a href="Proposals.aspx"><i class="fa fa-briefcase"></i>Proposals</a></li>
    <li class="menuservices"><a href="DepartmentClearance.aspx"><i class="fa fa-wrench"></i>Services</a></li>
    <li class="menugrievance"><a href="Grievance.aspx"><i class="fa fa-question-circle"></i>Grievance</a></li>
    <li class="menuPc"><a href="pcViewPage.aspx"><i class="fa fa-certificate"></i>Production Certificate</a></li>
    <li class="menuincentive othermenuli"><a href="incentives/incentiveoffered.aspx"><i class="fa fa-money"></i>Incentive</a></li>
    <li class="menumanage othermenuli" runat="server" id="managemenu"><a href="javascript:void(0);"><i class="fa fa-money"></i>Manage</a>
        <div class="othermenus">
            <ul>
                <li><a href="InvestorPortal/User_Management.aspx">User Management</a></li>
                <li><a href="InvestorPortal/User_Creation.aspx">User Creation</a></li>
                <li><a href="InvestorPortal/Drafted_User.aspx">Drafted User</a></li>
                <li><a href="InvestorPortal/Set_User_Permission.aspx">Application Permission</a></li>
            </ul>
        </div>
    </li>
    <li class="othermenulist othermenuli" runat="server" id="othermenulist"><a href="javascript:void(0);">
        <i class="fa fa-th"></i>Other Applications</a>
        <div class="othermenus">
            <ul id="OtherApps" runat="server">
            </ul>
        </div>
    </li>
    <li class="menudashboard" runat="server" id="dashboardmenu"><a href="InvesterDashboard.aspx"><i class="fa fa-tachometer"></i>Dashboard</a> </li>
    <li class="menunswsredirect" id="nswsmenu" runat="server">
        <asp:LinkButton ID="LnkBtnNswsRedirect" runat="server" OnClick="LnkBtnNswsRedirect_Click"><i class="fa fa-tachometer"></i>NSWS</asp:LinkButton>
    </li>
</ul>


<%--Modal Popup Section for Consent--%>

<asp:LinkButton ID="LnkBtnConsentTarget" runat="server"></asp:LinkButton>
<cc1:ModalPopupExtender ID="ModalPopupConsent" BehaviorID="mpe1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="PanelConsent"
    TargetControlID="LnkBtnConsentTarget" CancelControlID="LnkBtnConsentPopupClose">
</cc1:ModalPopupExtender>
<asp:Panel ID="PanelConsent" runat="server" CssClass="modalPopup" Style="display: none; width: 550px; height: 250px;"
    ToolTip="Important Notes">
    <div class="mhead">
        <asp:LinkButton ID="LnkBtnConsentPopupClose" runat="server" OnClick="LnkBtnConsentPopupClose_Click"><i class="fa fa-close"></i></asp:LinkButton>
        <span class="glyphicon glyphicon-alert"></span>
        <h4 class="modal-title">Important Notes</h4>
    </div>
    <div class="modal-body">
        <asp:UpdatePanel ID="UpdatePanelConsent" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12">
                        <ol style="padding-left: 20px;">
                            <li style="padding-bottom: 7px;">By clicking <span style="font-weight: bolder; color: red;">Yes</span>, you consent to being redirected to an external website. Please note that you will be subject to the terms and conditions, privacy policy, and other policies of the external website. Do you wish to proceed?"</li>
                        </ol>
                    </div>
                </div>
                <div class="mFooter">
                    <div class="row">
                        <div class="col-sm-12 text-right">
                            <asp:Button ID="BtnConsentYes" runat="server" Text="Yes" CssClass="btn btn-success" OnClick="BtnConsentYes_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="BtnConsentNo" runat="server" Text="No" CssClass="btn btn-primary" OnClick="BtnConsentNo_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Panel>


<%--Modal Popup Section for Alert Messages--%>

<asp:LinkButton ID="LnkBtnAlertTarget" runat="server"></asp:LinkButton>
<cc1:ModalPopupExtender ID="ModalPopupAlert" BehaviorID="mpe2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="PanelAlert"
    TargetControlID="LnkBtnAlertTarget" CancelControlID="LnkBtnAlertPopupClose">
</cc1:ModalPopupExtender>
<asp:Panel ID="PanelAlert" runat="server" CssClass="modalPopup" Style="display: none; width: 350px; height: 230px;"
    ToolTip="Important Notes">
    <div class="mhead">
        <asp:LinkButton ID="LnkBtnAlertPopupClose" runat="server" OnClick="LnkBtnAlertPopupClose_Click"><i class="fa fa-close"></i></asp:LinkButton>
        <h4 class="modal-title">Alert</h4>
    </div>
    <div class="modal-body">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12">
                        <label runat="server" id="LblMsg"></label>
                    </div>
                    <div class="modal-footer">
                        <div class="row">
                            <div class="col-sm-6 text-left">
                                <asp:Button ID="BtnAlertOk" runat="server" Text="Ok" CssClass="btn btn-primary" OnClick="BtnAlertOk_Click" />
                            </div>
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Panel>

<%--Modal Popup Section for Validation--%>

<asp:LinkButton ID="LnkBtnValidationTarget" runat="server"></asp:LinkButton>
<cc1:ModalPopupExtender ID="ModalPopupValidation" BehaviorID="mpe3" runat="server" BackgroundCssClass="modalBackground" PopupControlID="PanelValidation"
    TargetControlID="LnkBtnValidationTarget" CancelControlID="LnkBtnValidationPopupClose">
</cc1:ModalPopupExtender>
<asp:Panel ID="PanelValidation" runat="server" CssClass="modalPopup" Style="display: none; width: 600px; height: 330px;"
    ToolTip="Important Notes">
    <div class="mhead">
        <asp:LinkButton ID="LnkBtnValidationPopupClose" runat="server" OnClick="LnkBtnValidationPopupClose_Click"><i class="fa fa-close"></i></asp:LinkButton>
        <span class="glyphicon glyphicon-alert"></span>
        <h4 class="modal-title">Important Notes</h4>
    </div>
    <div class="modal-body">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12">
                        To access NSWS services, you are required to furnish the following information.                                    
                    </div>
                    <div class="col-sm-12">
                        <ol style="padding-left: 20px;">
                            <li style="padding-bottom: 7px;">Entity Type</li>
                            <li style="padding-bottom: 7px;">Postal Address</li>
                            <li style="padding-bottom: 7px;">Registartion Address</li>
                            <li style="padding-bottom: 7px;">CIN (If the Entity type is Incorporated Company)</li>
                            <li style="padding-bottom: 7px;">LLPIN (If the Entity type is Limited Liability Partnership)</li>
                        </ol>
                    </div>
                    <div class="col-sm-12">
                        Click on the <span style="font-weight: bolder; color: red;">Yes</span> button to update the aforementioned details, or proceed to the <span style="font-weight: bolder; color: red;">Edit Profile</span> page for updating your information.         
                    </div>
                </div>
                <div class="mFooter">
                    <div class="row">
                        <div class="col-sm-12 text-right">
                            <asp:Button ID="BtnValidationYes" runat="server" Text="Yes" CssClass="btn btn-success" OnClick="BtnValidationYes_Click" />
                            <asp:Button ID="BtnValidationNo" runat="server" Text="No" CssClass="btn btn-primary" OnClick="BtnValidationNo_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Panel>
