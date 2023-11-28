<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SLFCDecisionAdd.aspx.cs"
    Inherits="SingleWindow_SLFCDecisionAdd" %>

<%@ Register Src="~/Include/IncludeScript.ascx" TagName="IncludeScript" TagPrefix="ucIncludeScript" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Include/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/includes/Leftmenupanel.ascx" TagName="leftMenu" TagPrefix="ucLeftMenu" %>
<%@ Register Src="~/include/AMSfooter.ascx" TagName="footer" TagPrefix="ucfooter" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>SWP</title>
    <!-- Favicon and touch icons -->
    <link rel="shortcut icon" href="../images/favicon.ico" type="image/x-icon">
    <!-- Start Styles
         =====================================================================-->
    <script src="../../js/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../js/jQuery.alert.js" type="text/javascript"></script>
    <link href="../css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="../../PortalCSS/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/lobipanel.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/flash.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/pe-icon-7-stroke.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/themify-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/emojionearea.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/monthly.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/stylecrm.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/jquery.timepicker.css" rel="stylesheet" type="text/css" />
    <!-- End Styles
         =====================================================================-->
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery.timepicker.js" type="text/javascript"></script>
       <script src="../../js/custom.js" type="text/javascript"></script>
    <script src="../../js/Validator.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        pageHeader = "Manage Decision Details"
        indicate = "yes"
        window.onload = function () {
            configTab();
            configButton();
            configTitleBar();
        }
    </script>
</head>
<body class="hold-transition sidebar-mini">
    <div class="wrapper">
        <form id="form1" runat="server" defaultbutton="btnSubmit" defaultfocus="ddlProject">
        <!--Header-->
        <script language="javascript" type="text/javascript">

            $(document).ready(function () {
                $("#btnSubmit").click(function (e) {

                    if (!DropDownValidation('ddlProject', 'Project Name')) { return false; }
                    if (!blankFieldValidation('txtDecision', ' Decision of SLFC ')) { return false; }
                    if (!WhiteSpaceValidation1st('txtDecision', ' Decision of SLFC ')) { return false; }
                    if ($('#cbDecision :checkbox:checked').length <= 0) {
                        alert('Select SLFC Term and Condition');
                        e.preventDefault();
                        return;
                    };
                    if (!ConfirmAction('btnSubmit', 'Are you sure want to Submit ?', 'Are you sure want to Update ?')) {
                        return false;
                    }
                });
            });


        </script>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <ucheader:header ID="header1" runat="server" />
        <aside class="main-sidebar">
            <!-- sidebar -->
            <div class="sidebar">
                <!-- sidebar menu -->           
            <ucLeftMenu:leftMenu ID="LeftMenu2" runat="server" />               
            </div>
            <!-- /.sidebar -->
     </aside>
        <div class="content-wrapper">
            <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    Manage Decision Details</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>AMS</a></li><li><a>SLFC Decission</a></li>
                </ul>
            </div>
        </section>
            <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-bd lobidrag">
                        <div class="panel-body">
                            <div class="form-group row">
                                    <label class="col-sm-2">   Project Name</label>
                                    <div class="col-sm-4"><span class="colon">:</span>
                                        <asp:DropDownList ID="ddlProject" runat="server"  CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnId" Value="0" runat="server" />
                                        <span class="mandetory">*</span>
                                    </div>
                             </div>
                             <div class="form-group row">
                                <label class="col-sm-2">Decision of SLFC</label>
                                <div class="col-sm-4"><span class="colon">:</span>
                                    <asp:TextBox ID="txtDecision" runat="server" CssClass="form-control"  TextMode="MultiLine"
                                        TabIndex="5" Rows="3" onkeypress="return TextCounter('txtDecision','lblFinDetail',250)"
                                        ondrop="return false;" /> <span class="mandetory">*</span>       
                                    <cc1:FilteredTextBoxExtender ID="Decision" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,custom"
                                        TargetControlID="txtDecision" InvalidChars="!<>%'*&" ValidChars=" .()/,:">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:HiddenField ID="hdnKey" runat="server" Value="0" /> 
                                </div>
                             </div>
                            <div class="form-group row">
                                <label class="col-sm-2">Term and Condition</label>
                                <div class="col-sm-10"><span class="colon">:</span>
                                    <div class="viewTable show-data" id="viewTable">
                                        <asp:CheckBoxList ID="cbDecision" runat="server" cssClass="table table-bordered table-striped">
                                        </asp:CheckBoxList>
                                        <asp:Label ID="lblMessage" runat="server" Text="No Record(s) Found!!!" Visible="false"
                                            CssClass="lblMessage" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4 col-sm-offset-3"><asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success"
                                    OnClick="btnSubmit_Click" TabIndex="15" />
                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_Click"
                                    TabIndex="16" />
                                </div>      
                             </div>  
                        </div>
                    </div>
                </div>
            </div>
         </section>
        </div>
        <ucfooter:footer ID="footer1" runat="server" />
        </form>
    </div>
</body>
</html>
