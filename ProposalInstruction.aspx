<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProposalInstruction.aspx.cs"
    Inherits="ProposalInstruction" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script>

        $(document).ready(function () {
            $('.menuproposal').addClass('active');
        });
   
    </script>
    <style type="text/css">
        .guidelines
        {
            display: table;
            width: 100%;
            min-height: 200px;
            text-align: center;
            background: #eee;
            margin-bottom: 10px;
        }
        .guidelines p
        {
            display: table-cell;
            vertical-align: middle;
            font-size: 18px;
            letter-spacing: 1px;
        }
        .guidelinesdetails
        {
        }
        .guidelinesdetails h4
        {
            margin-top: 0px;
            font-weight: 600;
        }
        .instructiondiv
        {
            padding: 20px 40px;
        }
        .instructiondiv h2
        {
            color: #b1020a;
        }
        .minheight300
        {
            min-height: 300px;
        }
        .minheight300 p
        {
            font-size: 15px;
            line-height: 24px;
        }
    </style>
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
                    <div class="form-sec">
                        <div class="form-header">
                            <h2>
                                Proposal Guideline
                            </h2>
                        </div>
                        <div class="form-body ">
                            <div class="guidelinesdetails">
                                <div class="form-group ">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="minheight300">
                                                <%--<h4>
                                                Guideline</h4>--%>
                                                <div id="divabout" runat="server" class="instructiondiv">
                                                    <p class="text-justify">
                                                        All Industrial Units being set up in the state are encouraged to apply for Project
                                                        Evaluation including Allotment of Land (PEAL) Form to get the in-principle approval
                                                        of the concerned government departments and support of Single Window Authority.
                                                        However, existing industries and new industries already in possession of requisite
                                                        land may choose to apply for department services directly without filling the PEAL
                                                        form.</p>
                                                    <p>
                                                        PEAL Application and Single Window approval is mandatory in case land is required
                                                        from IDCO.</p>
                                                    <p>
                                                        For further queries please contact<b class="text-danger"> support.investodisha@nic.in
                                                        </b>
                                                    </p>
                                                </div>
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
                        <div class="col-sm-12 text-center margin-top10">
                            <asp:Button ID="btnApply" runat="server" Text="Proceed to PEAL Form" CssClass="btn btn-success"
                                OnClick="btnApply_Click" ToolTip="Click Here to Proceed into PEAL Form." />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
