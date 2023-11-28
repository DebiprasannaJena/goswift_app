<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePasswordNonInvestor.aspx.cs" Inherits="ChangePasswordNonInvestor" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/NonIndustryHeader.ascx" TagName="NonIndustryHeader" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/NonIndustryMenu.ascx" TagName="NonIndustryMenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%--<script src="Scripts/Validator.js" type="text/javascript"></script>--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="js/WebValidation.js" type="text/javascript"></script>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function validation() {          

            if (blankFieldValidation('Txt_Old_Pwd', 'Old password', 'SWP') == false) {
                return false;
            }

            if (blankFieldValidation('Txt_New_Pwd', 'New password', 'SWP') == false) {
                return false;
            }

            if (document.getElementById("Txt_Old_Pwd").value == document.getElementById("Txt_New_Pwd").value) {               
                jAlert('<strong>New password cannot be same as old password !</strong>', 'SWP');
                document.getElementById("Txt_New_Pwd").value = "";
                document.getElementById("Txt_Retype_Pwd").value = "";
                document.getElementById("Txt_New_Pwd").focus();
                return false;
            }

            if (!checkPassword()) {
                return false;
            }

            if (blankFieldValidation('Txt_Retype_Pwd', 'Retype password', 'SWP') == false) {
                document.getElementById("Txt_New_Pwd").focus();
                return false;
            }

            if (document.getElementById("Txt_New_Pwd").value != document.getElementById("Txt_Retype_Pwd").value) {
                debugger;
                jAlert('<strong>New password and Retype password does not match !</strong>', 'SWP');
                document.getElementById("Txt_New_Pwd").value = "";
                document.getElementById("Txt_Retype_Pwd").value = "";
                document.getElementById("Txt_New_Pwd").focus();
                return false;
            }

            function checkPassword() {
                var Txt_New_Pwd = document.getElementById("Txt_New_Pwd");
                var pwdVal = Txt_New_Pwd.value;
                var illegalChars = /[\W_]g/;
                var re = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,14}$/
                if (!re.test(pwdVal)) {
                    jAlert("<strong>Password must contain atleast one uppercase letter,one lowercase letter,one number and one special character and length must be between 8-14 characters !</strong>", 'SWP');
                    Txt_New_Pwd.focus();
                    return false;
                }
                else {
                    return true;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:NonIndustryHeader ID="header" runat="server" />
    <div class="container wrapper">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="registration-div investors-bg">
            <div id="exTab1">
                <div class="investrs-tab">
                    <div class="row">
                        <div class="col-sm-12">
                            <uc4:NonIndustryMenu ID="ineste" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="1a">
                        <div class="form-sec">
                            <div class="form-header">
                                <h2>
                                    Change Password</h2>
                            </div>
                            <div class="form-body">
                                <div class="form-group">
                                    <label class="col-sm-2">
                                        Old Password</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_Old_Pwd" autocomplete="off" runat="server" CssClass="form-control"
                                            onPaste="return false" TextMode="Password" MaxLength="14"></asp:TextBox>
                                        <span class="mandetory">*</span></div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2">
                                        New Password</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_New_Pwd" autocomplete="off" runat="server" TextMode="Password"
                                            onPaste="return false" CssClass="form-control" onblur="blockspecialchar_first(this);"
                                            MaxLength="14" ToolTip="Password must have 1 digit,1 special character,1 upper & lower case letter and cannot contain [%$#!()'<>] chars"></asp:TextBox>
                                        <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="Txt_New_Pwd"
                                            FilterMode="ValidChars" FilterType="UppercaseLetters,Numbers,Custom,LowercaseLetters"
                                            ValidChars="@_-">
                                        </cc2:FilteredTextBoxExtender>
                                        <span class="mandetory">*</span></div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2">
                                        Retype Password</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_Retype_Pwd" autocomplete="off" runat="server" onPaste="return false"
                                            TextMode="Password" CssClass="form-control" onblur="blockspecialchar_first(this);"
                                            MaxLength="14"></asp:TextBox>
                                        <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="Txt_Retype_Pwd"
                                            FilterMode="ValidChars" FilterType="UppercaseLetters,Numbers,Custom,LowercaseLetters"
                                            ValidChars="@_-">
                                        </cc2:FilteredTextBoxExtender>
                                        <span class="mandetory">*</span>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>
                            <div class="form-footer">
                                <div class="row">
                                    <div class="col-sm-10 col-sm-offset-2 ">
                                        <asp:Button ID="Btn_Submit" CssClass="btn  btn-success" runat="server" Text="Submit"
                                            TabIndex="5" OnClientClick="return validation();" OnClick="Btn_Submit_Click" />
                                        <asp:Button ID="Btn_Reset" runat="server" CssClass="btn  btn-danger" Text="Reset"
                                            TabIndex="5" OnClick="Btn_Reset_Click" />
                                        <asp:Label ID="lblMsg" runat="server" Text="" Style="color: Red"></asp:Label>
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