<%--'*******************************************************************************************************************
' File Name         : Proposals.aspx
' Description       : Show the list of all approved and pending proposals.
' Created by        : AMit Sahoo
' Created On        : 30th June 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DraftedProposals.aspx.cs"
    Inherits="DraftedProposals" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {
            $('.menuproposal').addClass('active');
        });
    </script>
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        
        .modalPopup
        {
            background-color: #fbfbfb;
            border: 3px solid #b93607;
            margin: 0px;
        }
        .modalPopup .mhead
        {
            padding: 5px 15px;
            border-bottom: 1px solid #ccc;
            background: #b93607;
            color: #fff;
        }
        .modalPopup .mhead h4
        {
            display: inline-block;
        }
        .modalPopup .mhead a
        {
            float: right;
            color: #fff;
            text-decoration: none;
        }
        .modalPopup .mbody
        {
            padding: 30px 15px;
        }
        .radiodiv
        {
            padding: 10px 0px 20px;
        }
        .Confdiv
        {
            padding: 25px 120px 20px;
        }
        
        #PanelIdco h4
        {
            font-size: 17px;
            font-weight: bold;
            padding-bottom: 12px;
        }
        .radio-inline label
        {
            display: inline-block;
            padding-right: 20px;
            padding-left: 12px;
        }
        .reglogin
        {
            padding: 25px;
        }
        .reglogin p
        {
            text-align: justify;
        }
        .reglogin a
        {
            color: #0088cc;
            text-decoration: none;
        }
        .reglogin a:hover
        {
            color: #159f45;
        }
        .popBox
        {
            position: absolute;
            -webkit-box-shadow: 0px 2px 7px 0px rgba(50, 50, 50, 0.65);
            -moz-box-shadow: 0px 2px 7px 0px rgba(50, 50, 50, 0.65);
            box-shadow: 0px 2px 7px 0px rgba(50, 50, 50, 0.65);
            background: #fffdef;
            padding: 8px;
            border: 1px solid #ddd;
            width: 93%;
            left: 15px;
            font-size: 14px !important;
        }
        #pop-up
        {
            top: 120px;
        }
        #pop-up1
        {
            top: 120px;
        }
        #pop-up2
        {
            top: 100px;
        }
        .row
        {
            margin-left: -15px;
            margin-right: 0;
        }
        .navbar-inverse
        {
            background-color: none !important;
            border-color: none !important;
        }
        .portlet-sec
        {
            margin: 16px 15px 8px;
            padding: 5px;
            border-radius: 2px;
        }
        .portlet-sec h3
        {
            text-transform: uppercase;
            font-size: 20px;
        }
        .portlet-sec h3 span
        {
            font-weight: 600;
            color: #ac2d00;
            padding: 0px 4px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="registration-div investors-bg">
            <div class="">
                <div id="exTab1">
                    <div class="investrs-tab">
                        <uc4:investoemenu ID="ineste" runat="server" />
                    </div>
                    <div class="tab-content clearfix">
                        <div class="tab-pane active" id="1a">
                            <div class="form-sec">
                                <div class="form-header">
                                    <%-- <a href="PEAL/PromoterDetails.aspx" title="Submit Proposal" class="pull-right proposalbtn">
                                    Submit Proposal</a>--%>
                                    <%--<span class="pull-right"><strong>Last Updated on : </strong><asp:Label ID="lblLastUpdatedon" runat="server" Text=""></asp:Label></span>--%>
                                    <a href="DraftedProposals.aspx" title="Draft Proposals" class="pull-right proposalbtn ">
                                        Draft Proposals</a> <a href="ProposalInstruction.aspx" title="Create Proposal" class="pull-right proposalbtn active">
                                            Create Proposal (PEAL)</a> <a href="Proposals.aspx" title="View Proposal" class="pull-right proposalbtn active">
                                                View Proposal</a>
                                    <h2>
                                        Draft Proposals</h2>
                                </div>
                                <div class="form-body minheight350">
                                    <div class="form-group">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvDraftProposal" runat="server" CssClass="table table-bordered"
                                                AllowPaging="true" PageSize="10" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                                                OnPageIndexChanging="gvDraftProposal_PageIndexChanging" CellPadding="4" GridLines="None"
                                                DataKeyNames="strProposalNo" OnRowDataBound="gvDraftProposal_RowDataBound">
                                                <AlternatingRowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Proposal No.">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hypLink" runat="server" Text='<%# Eval("strProposalNo") %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name Of Industries/Enterprises">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("strActionTakenBY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Investor's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("strActionToBeTakenBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Industry Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("strQuerytype") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Last Updated on">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("dtmCreatedOn") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Draft" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hypEdit" CssClass="btn btn-primary" runat="server">Continue</asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pagination-grid no-print" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--POPPUP FOR UPDATE INFO--%>
                        <asp:HiddenField ID="hdnIndId" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpe" runat="server"
                            PopupControlID="pnlProfile" TargetControlID="hdnIndId" BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnlProfile" runat="server" CssClass="modalPopup" Style="display: none;
                            width: 800px; height: 385px">
                            <div class="mhead">
                                <asp:LinkButton ID="Linkclose" runat="server" OnClick="Linkclose_Click"><i class="fa fa-close" onclick='return check()'></i></asp:LinkButton>
                                <h4>
                                    Update Information</h4>
                            </div>
                            <div class="mbody">
                                <div class="form-section">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <label>
                                                Name of the Contact Person
                                            </label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtContactPersn" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Name"
                                                ControlToValidate="txtContactPersn" ValidationGroup="a" ForeColor="Red" SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Name."
                                                ControlToValidate="txtContactPersn" ValidationExpression="^[a-zA-Z ]+$" ValidationGroup="a"
                                                ForeColor="Red" SetFocusOnError="true" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <label>
                                                E-Mail address of Contact Person
                                            </label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Email"
                                                ControlToValidate="txtEmailId" ValidationGroup="a" ForeColor="Red" SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                TargetControlID="txtEmailId" FilterMode="ValidChars" ValidChars="@._-" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ID="validateEmail" runat="server" ErrorMessage="Invalid email."
                                                ControlToValidate="txtEmailId" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
                                                ValidationGroup="a" ForeColor="Red" SetFocusOnError="true" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <label>
                                                Mobile Number of Contact Person
                                            </label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Mobile Number"
                                                ControlToValidate="txtMobileNo" ValidationGroup="a" ForeColor="Red" SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                TargetControlID="txtMobileNo" InvalidChars="!<>%">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <br />
                                    <div style="text-align: center; margin-right: 183px;">
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success"
                                            ValidationGroup="a" OnClick="btnUpdate_Click" />
                                        <asp:Button ID="btnHide" runat="server" Text="Skip" CssClass="btn btn-danger" OnClick="btnHide_Click" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
