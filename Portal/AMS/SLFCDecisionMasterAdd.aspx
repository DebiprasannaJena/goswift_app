<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SLFCDecisionMasterAdd.aspx.cs"
    Inherits="SingleWindow_SLFCDecisionMasterAdd" %>

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
        pageHeader = "Conditions for SLSWCA Proposals"
        indicate = "yes"
        window.onload = function () {
            configTab();
            configButton();
            configTitleBar();
        }

        $(document).ready(function () {
            $("#divSector").hide();

            $("input[name='CType']").click(function () {
                if ($("#rbtAll").is(":checked")) {
                    $("#divSector").hide();
                } else {
                    $("#divSector").show();
                }
            });

        });

        function Validation() {
            if (!blankFieldValidation('txtBrief', ' Terms & Conditions')) { return false; }
            if (!WhiteSpaceValidation1st('txtBrief', ' Terms & Conditions ')) { return false; }
        }
    </script>
</head>
<body class="hold-transition sidebar-mini">
    <div class="wrapper">
        <form id="form1" runat="server" defaultbutton="btnSubmit" defaultfocus="txtProjNm">
        <!--Header-->
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <ucheader:header ID="header1" runat="server" />
        <aside class="main-sidebar">
            <!-- sidebar -->
            <div class="sidebar">
               <!-- sidebar menu -->           
           <ucLeftMenu:leftMenu ID="leftMenu1" runat="server" />
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
                         SLFC Checklist</h1>
                    <ul class="breadcrumb">
                        <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                        <li><a>AMS</a></li><li><a> SLFC Checklist</a></li>
                    </ul>
                </div>
            </section>
             


            <section class="content">
               <div class="row">
                    <div class="col-md-12">
                       <div class="panel panel-bd lobidisable">
                <div class="panel-heading">
                    <div class="btn-group buttonlist">
                        <a class="btn btn-add active" href="SLFCDecisionMasterAdd.aspx"><i class="fa fa-plus"></i>Add
                             </a>
                    </div>
                    <div class="btn-group buttonlist">
                        <a class="btn btn-add " href="ViewSLFCDecisionMaster.aspx"><i class="fa fa-file"></i>Active
                             </a>
                    </div>
                     <div class="btn-group buttonlist">
                        <a class="btn btn-add " href="ViewInactiveSLFCDecisions.aspx"><i class="fa fa-file"></i>Inactive
                             </a>
                    </div>
                </div>
          
                        <div class="panel panel-bd lobidrag">
                            <div class="panel-body">
                               <div class="form-group row">
                                    <label class="col-sm-3">Condition Type</label> <!--<span class="mandatory">*</span>-->
                                        <div class="col-sm-4"><span class="colon">:</span>
                                                    <label class="radio-inline ">
                                                            <asp:RadioButton ID="rbtAll" Text="All" Checked="true" GroupName="CType" runat="server" />
                                                    </label>
                                                    <label class="radio-inline ">
                                                        <asp:RadioButton ID="rbtSector" Text="Sector Specific" GroupName="CType" runat="server" />
                                                    </label>
                                      </div>
                                  </div>
                                  <div class="form-group row" id="divSector">                                                               <div class="col-sm-5 col-sm-offset-3">
                                                        <asp:DropDownList ID="ddlSector" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        </asp:DropDownList>
                                  </div>
                                  </div>
                                  <div class="form-group row">
                                    <label class="col-sm-3">Terms & Conditions<span class="mandatory">*</span></label>
                                        <div class="col-sm-5"><span class="colon">:</span>     
                                        <asp:TextBox ID="txtBrief" runat="server" CssClass="form-control" MaxLength="500" TextMode="MultiLine"
                                            TabIndex="1" Rows="4" onkeyup="return TextCounter('txtBrief','lblBrief',500)"
                                            ondrop="return false;" />
                                        <cc1:FilteredTextBoxExtender ID="FEBrief" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                            TargetControlID="txtBrief" InvalidChars="!<>%'*&" ValidChars=" .(),-/\">
                                        </cc1:FilteredTextBoxExtender>
                                       
                                        <small class="text-danger">Maximum <span class="mandatory">
                                            <asp:Label ID="lblBrief" runat="server" Text="500"></asp:Label>
                                        </span>&nbsp;characters</small>
                                        <asp:HiddenField ID="hdnKey" runat="server" Value="0" />
                                        </div>
                                 </div>
                                 <div class="row">
                                 <div class="col-sm-4 col-sm-offset-3"> <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-success"
                                    OnClientClick="return Validation();" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_Click" /></div>      
                              </div>  
                        </div>
                    </div>
                    </div>
                </div>
            </section>
        </div>
          <ucfooter:footer ID="footer1" runat="server" />
        <!-- Modal -->
        </form>
    </div>
</body>
</html>
