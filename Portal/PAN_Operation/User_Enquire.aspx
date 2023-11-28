<%--'*******************************************************************************************************************
' File Name         : User_Enquiry.aspx
' Description       : Enquiry User Details
' Created by        : Sushant Jena
' Created On        : 17-Nov-2018
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="User_Enquire.aspx.cs" Inherits="Portal_PAN_Operation_User_Enquire" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script src="../js/decimalrstr.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';



        $(document).ready(function () {

            $('#ContentPlaceHolder1_Btn_Search').click(function () {
                if ($('#ContentPlaceHolder1_Txt_MobileNo').val() !== '') {
                    if ($('#ContentPlaceHolder1_Txt_MobileNo').val().length <= 9) {
                        jAlert('<strong>Enter Valid Mobile No !!</strong>', projname);
                        $("#popup_ok").click(function () { $("#ContentPlaceHolder1_Txt_MobileNo").focus(); });
                        return false;
                    }
                }
                if ($('#ContentPlaceHolder1_Txt_Email_Id').val() == '' && $('#ContentPlaceHolder1_Txt_PAN').val() == '' && $('#ContentPlaceHolder1_Txt_Unit_Name').val() == '' && $('#ContentPlaceHolder1_Txt_SWS_Id').val() == '' && $('#ContentPlaceHolder1_Txt_MobileNo').val() == '') {
                    jAlert('<strong>Please Enter Any One Field !!</strong>', projname);
                    $("#popup_ok").click(function () { $("#ContentPlaceHolder1_Txt_Email_Id").focus(); });
                    return false;
                }
            });
        });

        function inputLimiter(e, allow) {
            var AllowableCharacters = '';

            if (allow == 'NameCharacters') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'NameCharactersAndNumbers') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'GSTINDET') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZ';
            }

            if (allow == 'OtherSpecify') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'Profit') {
                AllowableCharacters = '1234567890-.';
            }
            if (allow == 'Numbers') {
                AllowableCharacters = '1234567890';
            }
            if (allow == 'Decimal') {
                AllowableCharacters = '1234567890.';
            }
            if (allow == 'Email') {
                AllowableCharacters = '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@@._-';
            }
            if (allow == 'Address') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!"#$%&()*+,-./:;<=>?@[\]^_`{|}~';
            }
            if (allow == 'DateFormat') {
                AllowableCharacters = '1234567890/-';
            }
            if (allow == 'NumbersSSN') {
                AllowableCharacters = '1234567890-';
            }
            if (allow == 'RawMetrial') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!"#$%&()*+,-./:;<=>?@[\]^_`{|}~';
            }
            var k;
            k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
            if (k != 13 && k != 8 && k != 0) {
                if ((e.ctrlKey == false) && (e.altKey == false)) {
                    return (AllowableCharacters.indexOf(String.fromCharCode(k)) != -1);
                }
                else {
                    return true;
                }
            }
            else {
                return true;
            }
        }
        function checkvalidation() {
            //debugger;
            if (blankFieldValidation('ContentPlaceHolder1_Txt_New_Pwd', 'New password', projname) == false) {
                return false;
            }
            if (!checkPassword()) {
                return false;
            }
            if (blankFieldValidation('ContentPlaceHolder1_Txt_Confirm_Pwd', 'Confirm password', projname) == false) {
                return false;
            }
            if (document.getElementById("ContentPlaceHolder1_Txt_New_Pwd").value != document.getElementById("ContentPlaceHolder1_Txt_Confirm_Pwd").value) {
                debugger;
                jAlert('<strong>New password and Confirm password does not match !</strong>', 'SWP');
                document.getElementById("ContentPlaceHolder1_Txt_New_Pwd").value = "";
                document.getElementById("ContentPlaceHolder1_Txt_Confirm_Pwd").value = "";
                document.getElementById("ContentPlaceHolder1_Txt_New_Pwd").focus();
                return false;
            }
        }

        function checkPassword() {
            var Txt_New_Pwd = document.getElementById("ContentPlaceHolder1_Txt_New_Pwd");
            var pwdVal = Txt_New_Pwd.value;
            var illegalChars = /[\W_]g/;
            var re = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,14}$/
            if (!re.test(pwdVal)) {
                jAlert("<strong>Password must contain atleast one uppercase letter,one lowercase letter,one number and one special character and length must be between 8-14 characters !</strong>", projname);
                Txt_New_Pwd.focus();
                return false;
            }
            else {
                return true;
            }
        }
      

    </script>
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.6;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 900px;
            height: 550px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-tachometer"></i>
            </div>
            <div class="header-title">
                <h1>
                    User Enquire</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Manage User</a></li><li><a>User Enquire</a></li></ul>
            </div>
        </div>
        <!-- Main content -->
        <div class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-body">
                            <div class="search-sec">
                                <div class="form-group">
                                    <label class="col-sm-2">
                                        Email Id</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_Email_Id" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-2">
                                        PAN</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_PAN" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2">
                                        Unit Name</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_Unit_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-2">
                                        Investor SWS Id
                                    </label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_SWS_Id" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>

                                 <div class="form-group">
                                    <label class="col-sm-2">
                                        Mobile No</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_MobileNo" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-6">
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Button ID="Btn_Search" runat="server" Text="Search" CssClass="btn btn-success"
                                            OnClick="Btn_Search_Click" ToolTip="Click here to view investor(s) !!" />
                                        <asp:Button ID="Btn_Reset" runat="server" Text="Reset" CssClass="btn btn-danger"
                                            OnClick="Btn_Reset_Click" ToolTip="Click here to reset above fields !!" />
                                        <asp:Button ID="Btn_Rejected_User" runat="server" Text="View Rejected User" CssClass="btn btn-rating"
                                            OnClick="Btn_Rejected_User_Click" ToolTip="Click here to view rejected investor(s) !!" />
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover"
                                    AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SlNo">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbkl_SlNo" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Name">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkBtn_Inv_Name" runat="server" ToolTip="Click here to view details !!"
                                                    OnClick="LnkBtn_Inv_Name_Click" Text='<%# Eval("VCH_INV_NAME")%>'></asp:LinkButton>
                                                <asp:HiddenField ID="Hid_Unique_Id" runat="server" Value='<%# Eval("VCH_UNIQUEID") %>' />
                                                <asp:HiddenField ID="Hid_Investor_Id" runat="server" Value='<%# Eval("INT_INVESTOR_ID") %>' />
                                                <asp:HiddenField ID="Hid_Regd_Source" runat="server" Value='<%# Eval("VCH_REGD_SOURCE") %>' />
                                                <asp:HiddenField ID="Hid_Investor_UserId" runat="server" Value='<%# Eval("VCH_INV_USERID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Other Details">
                                            <ItemTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td width="28%">
                                                            New User Id
                                                        </td>
                                                        <td width="2%">
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Lbl_New_User_Id" Text='<%# Eval("VCH_INV_USERID") %>'
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Old User Id
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Lbl_Old_User_Id" Text='<%# Eval("VCH_INV_USERID_BK") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            PAN
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Lbl_PAN" Text='<%# Eval("VCH_PAN") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Email Id
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Lbl_Email_Id" Text='<%# Eval("VCH_EMAIL") %>' ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top;">
                                                            Address
                                                        </td>
                                                        <td style="vertical-align: top;">
                                                            :
                                                        </td>
                                                        <td style="vertical-align: top;">
                                                            <asp:Label runat="server" ID="Lbl_Address" Text='<%# Eval("VCH_ADDRESS") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transaction Details">
                                            <ItemTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td width="60%">Total Proposal
                                                        </td>
                                                        <td width="2%">:&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Lbl_Total_Proposal" Text='<%# Eval("INT_PROPOSAL_COUNT") %>'
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Total Service
                                                        </td>
                                                        <td>:&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Lbl_Total_Service" Text='<%# Eval("INT_SERVICE_COUNT") %>'
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Total Incentive
                                                        </td>
                                                        <td>:&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Lbl_Total_Incentive" Text='<%# Eval("INT_INCENTIVE_COUNT") %>'
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Total Grievance
                                                        </td>
                                                        <td>:&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Lbl_Total_Grievance" Text='<%# Eval("INT_GRIEVANCE_COUNT") %>'
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <%--<tr>
                                                        <td colspan="3">&nbsp;
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:LinkButton ID="LnkBtn_View_Transaction" runat="server" OnClick="LnkBtn_View_Transaction_Click"
                                                                ToolTip="Click here to view Transation Details !" CssClass="btn-link">View Details</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Status" Text='<%# Eval("VCH_APPROVAL_STATUS") %>'
                                                    Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reset Password">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkBtn_Reset_Pwd" runat="server" OnClick="LnkBtn_Reset_Pwd_Click"
                                                    OnClientClick="return confirm('Are you sure to reset password !!');" CssClass="btn btn-danger"
                                                    ToolTip="Click Here to Reset Password !!" Width="130px">Reset Password</asp:LinkButton>
                                                <br />
                                                <br />
                                                <asp:LinkButton ID="LnkBtn_Verify_OTP" runat="server" OnClick="LnkBtn_Verify_OTP_Click"
                                                    CssClass="btn btn-success" ToolTip="Click Here to Verify OTP  !!" Width="130px">Verify OTP</asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No records found !!
                                    </EmptyDataTemplate>
                                    <EmptyDataRowStyle Font-Italic="true" ForeColor="Red" />
                                </asp:GridView>
                                <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover"
                                    AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SlNo">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbkl_SlNo" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Name">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkBtn_Inv_Name_Reject" runat="server" ToolTip="Click here to view details !!"
                                                    OnClick="LnkBtn_Inv_Name_Reject_Click" Text='<%# Eval("VCH_INV_NAME")%>'></asp:LinkButton>
                                                <asp:HiddenField ID="Hid_Unique_Id" runat="server" Value='<%# Eval("VCH_UNIQUEID") %>' />
                                                <asp:HiddenField ID="Hid_Investor_Id" runat="server" Value='<%# Eval("INT_INVESTOR_ID") %>' />
                                                <asp:HiddenField ID="Hid_Investor_UserId" runat="server" Value='<%# Eval("VCH_INV_USERID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email Id">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Email_Id" Text='<%# Eval("VCH_EMAIL") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Other Details">
                                            <ItemTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td width="28%">
                                                            User Id
                                                        </td>
                                                        <td width="2%">
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Lbl_New_User_Id" Text='<%# Eval("VCH_INV_USERID") %>'
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            PAN
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Lbl_PAN" Text='<%# Eval("VCH_PAN") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top;">
                                                            Address
                                                        </td>
                                                        <td style="vertical-align: top;">
                                                            :
                                                        </td>
                                                        <td style="vertical-align: top;">
                                                            <asp:Label runat="server" ID="Lbl_Address" Text='<%# Eval("VCH_ADDRESS") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rejection Cause">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Rejection_Cause" Text='<%# Eval("VCH_REJECTION_CAUSE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rejection Date">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Rejection_Date" Text='<%#  Eval("DTM_REJECTION_DATE", "{0:dd-MMM-yyyy hh:mm:ss tt}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="12%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No records found !!
                                    </EmptyDataTemplate>
                                    <EmptyDataRowStyle Font-Italic="true" ForeColor="Red" />
                                </asp:GridView>
                            </div>
                            <asp:HiddenField ID="Hid_Pop" runat="server" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                                TargetControlID="Hid_Pop" BackgroundCssClass="modalBackground" CancelControlID="Btn_Cancel">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel1" runat="server" CssClass="modalfade" Style="display: none;">
                                <div id="undertakingipr2015">
                                    <div class="modal-dialog modal-lg">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-header bg-purpul">
                                                <h4 class="modal-title">
                                                    <span style="font-weight: bold; color: Maroon;">Reset Password</span></h4>
                                            </div>
                                            <div class="modal-body">
                                                <h4 style="font-weight: 600; color: Blue;">
                                                    Unit Details.</h4>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-2">
                                                            Unit Name</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Lbl_Investor_Name_ResetPwd" runat="server" CssClass="form-control-static"
                                                                Font-Bold="true"></asp:Label>
                                                            <asp:HiddenField ID="Hid_Unique_Id_ResetPwd" runat="server" />
                                                            <asp:HiddenField ID="Hid_Investor_Id_ResetPwd" runat="server" />
                                                        </div>
                                                        <label class="col-sm-2">
                                                            User Id</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Lbl_User_Id_ResetPwd" runat="server" CssClass="form-control-static"
                                                                Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-2">
                                                            PAN</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Lbl_PAN_ResetPwd" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <label class="col-sm-2">
                                                            Email Id</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Lbl_Email_Id_ResetPwd" runat="server" CssClass="form-control-static"
                                                                Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-2">
                                                            New Password</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="Txt_New_Pwd" runat="server" CssClass="form-control" MaxLength="50"
                                                                TextMode="Password"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-2">
                                                            Confirm Password</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="Txt_Confirm_Pwd" runat="server" CssClass="form-control" MaxLength="50"
                                                                TextMode="Password"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <asp:Button ID="Btn_Submit" runat="server" Text="Submit" OnClick="Btn_Submit_Click"
                                                            class="btn btn-success" ToolTip="Click Here to Proceed !!" OnClientClick="return checkvalidation();" />
                                                        <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" class="btn btn-danger" ToolTip="Click Here to Cancel !!" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:HiddenField ID="Hid_Pop_2" runat="server" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel2"
                                TargetControlID="Hid_Pop_2" BackgroundCssClass="modalBackground" CancelControlID="Btn_OTP_Cancel">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel2" runat="server" CssClass="modalfade" Style="display: none;">
                                <div id="Div1">
                                    <div class="modal-dialog modal-lg">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-header bg-purpul">
                                                <h4 class="modal-title">
                                                    <span style="font-weight: bold; color: Green;">Verify and Update OTP</span>
                                                </h4>
                                            </div>
                                            <div class="modal-body">
                                                <h4 style="font-weight: 600; color: Blue;">
                                                    Unit Details.</h4>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-2">
                                                            Unit Name</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Lbl_Investor_Name_OTP" runat="server" CssClass="form-control-static"
                                                                Font-Bold="true"></asp:Label>
                                                            <asp:HiddenField ID="Hid_Investor_Id_OTP" runat="server" />
                                                        </div>
                                                        <label class="col-sm-2">
                                                            User Id</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Lbl_User_Id_OTP" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-2">
                                                            PAN</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Lbl_PAN_OTP" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <label class="col-sm-2">
                                                            Email Id</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Lbl_Email_Id_OTP" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-2">
                                                            OTP</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Lbl_OTP" runat="server" Font-Bold="true" ForeColor="Red" CssClass="form-control-static"></asp:Label>
                                                        </div>
                                                        <label class="col-sm-2">
                                                            OTP Status</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Lbl_OTP_Status" runat="server" Font-Bold="true" ForeColor="Green"
                                                                CssClass="form-control-static"></asp:Label>
                                                            <asp:HiddenField ID="Hid_OTP_Status" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-2">
                                                            OTP Time
                                                        </label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Lbl_OTP_Time" runat="server" CssClass="form-control-static"></asp:Label>
                                                        </div>
                                                        <label class="col-sm-2">
                                                            Application Status</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Lbl_Application_Status" runat="server" Font-Bold="true" ForeColor="Green"
                                                                CssClass="form-control-static"></asp:Label>
                                                            <asp:HiddenField ID="Hid_Application_Status" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <asp:Button ID="Btn_OTP_Update" runat="server" Text="Update" OnClick="Btn_OTP_Update_Click"
                                                            class="btn btn-success" ToolTip="Click Here to Proceed !!" OnClientClick="return confirm('Are you sure to update OTP ?')" />
                                                        <asp:Button ID="Btn_OTP_Cancel" runat="server" Text="Cancel" class="btn btn-danger"
                                                            ToolTip="Click Here to Cancel !!" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
