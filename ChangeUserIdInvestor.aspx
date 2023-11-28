<%--'*******************************************************************************************************************
' File Name         : ChangeUserIdInvestor.aspx
' Description       : To create alias name for user id
' Created by        : Sushant Jena
' Created On        : 14-Sep-2018
' Modification History:

' <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeUserIdInvestor.aspx.cs"
    Inherits="ChangeUserIdInvestor" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script src="js/WebValidation.js" type="text/javascript"></script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        /*--------------------------------------------------------------*/

        function Validate() {

            if (blankFieldValidation('Txt_Alternate_User_Id', 'Alternative user name', projname) == false) {
                return false;
            }
            if ($('#Txt_Confirm_Alternate_User_Id').val() == "") {
                jAlert('<strong>Please confirm your alternative user name !!</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_Confirm_Alternate_User_Id").focus(); });
                return false;
            }

            if (document.getElementById("Txt_Alternate_User_Id").value != document.getElementById("Txt_Confirm_Alternate_User_Id").value) {
                document.getElementById("Txt_Confirm_Alternate_User_Id").focus();
                jAlert("<strong>Confirm user name should be same as alternative user name !</strong>", projname);
                return false;
            }
        }       

    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="registration-div investors-bg">
            <div id="exTab1">
                <div class="investrs-tab">
                    <div class="row">
                        <div class="col-sm-12">
                            <uc4:investoemenu ID="ineste" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="1a">
                        <div class="form-sec">
                            <div class="form-header">
                                <a href="EditInvestorProfile.aspx" title="Edit Profile" class="pull-right proposalbtn ">
                                    Edit Profile</a> <a href="ChangeUserIdInvestor.aspx" title="Create Alternate User Name"
                                        class="pull-right proposalbtn active">Create Alternate User Name</a>
                                <h2>
                                    Create Alternative User Name</h2>
                            </div>
                            <div class="form-body">
                                <div class="form-group">
                                    <label class="col-sm-2">
                                        Unit Name</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:Label ID="Lbl_Unit_Name" runat="server" CssClass="form-control-static"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Label ID="Lbl_Warning_Msg" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2">
                                        Email Id</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:Label ID="Lbl_Email_Id" runat="server" CssClass="form-control-static"></asp:Label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2">
                                        Mobile Number</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:Label ID="Lbl_Mobile_Number" runat="server" CssClass="form-control-static"></asp:Label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2">
                                        User Id</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:Label ID="Lbl_User_Id" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2">
                                        Alternate User Name</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_Alternate_User_Id" runat="server" CssClass="form-control" MaxLength="30"
                                            AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                            TargetControlID="Txt_Alternate_User_Id" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                            InvalidChars="&quot;'<>&; ">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="form-group" id="divConfirmAlias" runat="server">
                                    <label class="col-sm-2">
                                        Confirm User Name</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_Confirm_Alternate_User_Id" runat="server" CssClass="form-control"
                                            TextMode="Password" MaxLength="30"></asp:TextBox>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>
                            <div class="form-footer" id="divBtnSubmit" runat="server">
                                <div class="row">
                                    <div class="col-sm-10 col-sm-offset-2 ">
                                        <asp:Button ID="Btn_Submit" CssClass="btn  btn-success" runat="server" Text="Submit"
                                            TabIndex="5" OnClientClick="return Validate();" OnClick="Btn_Submit_Click" />
                                        <asp:Label ID="Lbl_Msg" runat="server" Text="" Style="color: Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
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
