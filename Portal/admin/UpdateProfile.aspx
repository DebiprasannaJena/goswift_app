<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="UpdateProfile.aspx.cs" Inherits="Portal_admin_UpdateProfile" %>

<%@ Register Src="~/Portal/Console/TopHeader.ascx" TagName="Topheader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <title></title>
    <script language="javascript" type="text/javascript" src="~/Portal/Console/scripts/Validator.js"></script>
    <script src="../../js/WebValidation.js" type="text/javascript"></script>
    <script type="text/javascript" src="~/Portal/Console/scripts/md5.js"></script>
    <script language="javascript" type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function validation() {

            if (blankFieldValidation('ContentPlaceHolder1_txtEmail', 'Email Id', projname) == false) {
                return false;
            }
            if (EmailValidation('ContentPlaceHolder1_txtEmail', 'email address', projname) == false) {
                return false;
            }
            if (blankFieldValidation('ContentPlaceHolder1_txtMobile', 'Mobile No', projname) == false) {
                return false;
            }

            var val = ($("#ContentPlaceHolder1_txtMobile").val());
            if (($("#ContentPlaceHolder1_txtMobile").val()).substring(0, 1) == '0') {
                jAlert('<strong>Mobile number should not be start with zero !</strong>');
                $("#ContentPlaceHolder1_txtMobile").val('');
                $("#ContentPlaceHolder1_txtMobile").focus();
                return false;
            }
            if ($("#ContentPlaceHolder1_txtMobile").val().length != 10) {
                jAlert('<strong>Mobile number should be 10 digits !</strong>');
                $("#ContentPlaceHolder1_txtMobile").focus();
                return false;
            }
        }

        /////-------------------------------------------------------------------------

        function numeralsOnly(evt, tt) {
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : ((evt.which) ? evt.which : 0));
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                //alert("Enter Numerals only in this Field!");
                //tt.value="";              
                return false;
            }
            return true;
        }

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-lock"></i>
            </div>
            <div class="header-title">
                <h1>
                    Update Profile</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Update Profile</a></li></ul>
            </div>
        </div>
        <!-- Main content -->
        <div class="content">
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
                                        Email Id</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" AutoCompleteType="None"
                                            autocomplete="off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                            TargetControlID="txtEmail" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            ValidChars="_.@">
                                        </cc1:FilteredTextBoxExtender>
                                        <span class="mandetory">*</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        MobileNo</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="10"
                                            onkeypress="return numeralsOnly(event,this);" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            TargetControlID="txtMobile" FilterMode="ValidChars" FilterType="Numbers" ValidChars="0-9">
                                        </cc1:FilteredTextBoxExtender>
                                        <span class="mandetory">*</span>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div style="display: inline" class="nav">
                            </div>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success"
                                OnClientClick="return validation();" TabIndex="5" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-warning"
                                TabIndex="5" OnClick="btnReset_Click" />
                            <asp:Label ID="lblMsg" runat="server" Text="" Style="color: Red"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
