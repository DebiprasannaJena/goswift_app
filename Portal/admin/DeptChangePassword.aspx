<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="DeptChangePassword.aspx.cs" Inherits="Portal_admin_DeptChangePassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="~/Portal/Console/TopHeader.ascx" TagName="Topheader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <title></title>
    <script language="javascript" type="text/javascript" src="~/Portal/Console/scripts/Validator.js"></script>
    <script type="text/javascript" src="~/Portal/Console/scripts/md5.js"></script>
    <script src="../../js/WebValidation.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function validateChgPwd() {
            if (blankFieldValidation('ContentPlaceHolder1_txtOldPwd', 'Old password', projname) == false) {
                return false;
            }
            if (blankFieldValidation('ContentPlaceHolder1_txtNewPwd', 'New password', projname) == false) {
                return false;
            }
            if (document.getElementById("ContentPlaceHolder1_txtNewPwd").value.length < 8) {
                jAlert('<strong>Your password must be at least 8 characters long !</strong>', projname);
                document.getElementById("ContentPlaceHolder1_txtNewPwd").focus();
                return false;
            }
            if (document.getElementById("ContentPlaceHolder1_txtOldPwd").value == document.getElementById("ContentPlaceHolder1_txtNewPwd").value) {
                jAlert('<strong>New password cannot be same as old password !</strong>', projname);
                document.getElementById("ContentPlaceHolder1_txtNewPwd").value = "";
                document.getElementById("ContentPlaceHolder1_txtRetypPwd").value = "";
                document.getElementById("ContentPlaceHolder1_txtNewPwd").focus();
                return false;
            }
            if (!checkPassword()) {
                return false;
            }
            if (blankFieldValidation('ContentPlaceHolder1_txtRetypPwd', 'Confirm password', projname) == false) {
                document.getElementById("ContentPlaceHolder1_txtRetypPwd").focus();
                return false;
            }
            if (document.getElementById("ContentPlaceHolder1_txtNewPwd").value != document.getElementById("ContentPlaceHolder1_txtRetypPwd").value) {
                jAlert('<strong>New password and Confirm password does not match !</strong>', projname);
                document.getElementById("ContentPlaceHolder1_txtNewPwd").value = "";
                document.getElementById("ContentPlaceHolder1_txtRetypPwd").value = "";
                document.getElementById("ContentPlaceHolder1_txtNewPwd").focus();
                return false;
            }

            //            var str2;
            //            var slt;

            //            slt = randomtext();
            //            document.getElementById("hidSlt").value = slt;
            //            str2 = hex_md5(document.getElementById("txtOldPwd").value).toUpperCase() + slt;
            //            document.getElementById("txtOldPwd").value = hex_md5(str2).toUpperCase();
        }
        function randomtext() {
            var the_number = Math.floor(Math.random() * 500);
            return (the_number)
        }
        function checkPassword() {
            var txtNewPwd = document.getElementById("ContentPlaceHolder1_txtNewPwd");
            var pwdVal = txtNewPwd.value;
            var illegalChars = /[\W_]g/;
            var re = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/
            if (!re.test(pwdVal)) {
                jAlert("<strong>The password must contain at least one uppercase letter,one lowercase letter,one numeral and one special character.</strong>", 'GOSWIFT');
                txtNewPwd.focus();
                return false;
            }
            else {
                return true;
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
        <div class="header-icon">
            <i class="fa fa-lock"></i>
        </div>
        <div class="header-title">
            <h1>
                Change Password</h1>
            <ul class="breadcrumb">
                <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                <li><a>Change Password</a></li></ul>
        </div>
        </section>
        <!-- Main content -->
        <section class="content">
        <div class="row">
            <!-- Form controls -->
            <div class="col-sm-12">
                <div class="panel panel-bd lobidrag">
                    <div class="panel-heading">
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2">
                                    Old Password</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:TextBox ID="txtOldPwd" runat="server" TextMode="Password" CssClass="form-control"
                                        ToolTip="Enter old password here !" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                    <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtOldPwd"
                                        FilterMode="ValidChars" FilterType="UppercaseLetters,Numbers,Custom,LowercaseLetters"
                                        ValidChars="@_-">
                                    </cc2:FilteredTextBoxExtender>
                                    <span class="mandetory">*</span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2">
                                    New Password</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password" CssClass="form-control"
                                        MaxLength="50" ToolTip="Password must have 1 digit,1 special character,1 upper & lower case letter and cannot contain [%$#!()'<>] chars"  AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                    <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtNewPwd"
                                        FilterMode="ValidChars" FilterType="UppercaseLetters,Numbers,Custom,LowercaseLetters"
                                        ValidChars="@_-">
                                    </cc2:FilteredTextBoxExtender>
                                    <span class="mandetory">*</span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2">
                                    Confirm Password</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:TextBox ID="txtRetypPwd" runat="server" TextMode="Password" MaxLength="50" CssClass="form-control"
                                        ToolTip="Retype above password !"  AutoCompleteType="None" autocomplete="Off"></asp:TextBox>
                                    <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtRetypPwd"
                                        FilterMode="ValidChars" FilterType="UppercaseLetters,Numbers,Custom,LowercaseLetters"
                                        ValidChars="@_-">
                                    </cc2:FilteredTextBoxExtender>
                                    <span class="mandetory">*</span>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div style="display: inline" class="nav">
                        </div>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success"
                            TabIndex="5" OnClick="btnSubmit_Click" OnClientClick="return validateChgPwd();" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-warning"
                            TabIndex="5" OnClick="btnReset_Click" />
                        <asp:Label ID="lblMsg" runat="server" Text="" Style="color: Red"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
         </section>
    </div>
</asp:Content>
