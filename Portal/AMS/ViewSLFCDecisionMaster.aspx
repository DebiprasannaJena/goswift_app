<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewSLFCDecisionMaster.aspx.cs"
    Inherits="SingleWindow_ViewSLFCDecisionMaster" %>

<%@ Register Src="~/Include/IncludeScript.ascx" TagName="IncludeScript" TagPrefix="ucIncludeScript" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Include/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/includes/Leftmenupanel.ascx" TagName="leftMenu" TagPrefix="ucLeftMenu" %>
<%@ Register Src="~/include/AMSfooter.ascx" TagName="footer" TagPrefix="ucfooter" %>
<!DOCTYPE html >
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
    <script language="javascript" type="text/javascript">

        pageHeader = "Conditions for SLSWCA Proposals"
        indicate = "yes"
        window.onload = function () {
            configTab();
            configButton();
            configTitleBar();
        }

        function SelectAllCheckboxesSpecific(spanChk) {
            var IsChecked = spanChk.checked;
            var Chk = spanChk;
            Parent = document.getElementById('grdViwComments');
            var items = Parent.getElementsByTagName('input');
            for (i = 0; i < items.length; i++) {
                if (items[i].id != Chk && items[i].type == "checkbox") {
                    if (items[i].checked != IsChecked) {
                        items[i].click();
                    }
                }
            }
        }
        function Validation() {
            if (!ConfirmAction('btnSubmit', 'Are you sure want to Inactive ?')) {
                return false;
            }
        }
    
    </script>
</head>
<body class="hold-transition sidebar-mini">
    <div class="wrapper">
        <form id="form1" runat="server">
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
                        Conditions for SLSWCA</h1>
                    <ul class="breadcrumb">
                        <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                        <li><a>AMS</a></li><li><a>Conditions for SLSWCA</a></li>
                    </ul>
                </div>
            </section>
            <section class="content">
               <div class="row">
                    <div class="col-md-12">
                      <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="SLFCDecisionMasterAdd.aspx"><i class="fa fa-plus"></i>
                                    Add </a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add active" href="ViewSLFCDecisionMaster.aspx"><i class="fa fa-file"></i>
                                    Active </a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="ViewInactiveSLFCDecisions.aspx"><i class="fa fa-file">
                                </i>Inactive </a>
                            </div>
                        </div>
                    
                        <div class="panel panel-bd lobidrag">
                            <div class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div id="divPaging" runat="server" class="topPagnibh" align="right">
                                                        <asp:LinkButton ID="lbtnAll" CssClass="more" runat="server" Text="All" OnClick="lbtnAll_Click"
                                                            Visible="false"></asp:LinkButton>
                                                        <asp:Label CssClass="tdDataNormal" ID="lblPaging" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="table-responsive" id="printArea">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                            <tr>
                                                                <td>
                                                                    <div class="viewTable" id="viewTable">
                                                                        <asp:GridView ID="grdViwComments" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                            AllowSorting="true" PageIndex="10" DataKeyNames="bitDeletedFlag,ChecklistId"
                                                                            OnRowDataBound="grdViwComments_RowDataBound"
                                                                               class="table table-bordered table-hover" OnRowCommand="grdViwComments_RowCommand">
                                                                            <RowStyle CssClass="tdData2" />
                                                                     
                                                                            <Columns>
                                                                                <asp:TemplateField ItemStyle-CssClass="NOPRINT" HeaderStyle-CssClass="NOPRINT">
                                                                                    <HeaderTemplate>
                                                                                        <input name="cbAll" value="cbAll" type="checkbox" onclick="javascript:SelectAllCheckboxesSpecific(this);" />
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSel" runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Sl#" HeaderStyle-Width="10px">
                                                                                    <ItemTemplate>
                                                                                        <%#Container.DataItemIndex + 1%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField HeaderText="Term and Condition " DataField="ChecklistPoint" HeaderStyle-HorizontalAlign="Left"
                                                                                    NullDisplayText="--" />
                                                                                <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>                              
                                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-sm btn-success" title="Take Action" 
                                                                                        CommandName="Edit" CommandArgument='<%# Eval("ChecklistId") %>' OnClientClick="return confirm('Do You Want to Edit?')">
                                                                                         <i class="fa fa-pencil-square-o"></i></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle Width="60px" CssClass="noPrint" />
                                                                                    <ItemStyle CssClass="noPrint" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                            <asp:HiddenField ID="hdnDeptId" runat="server" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <table border="0" cellspacing="0" cellpadding="0" class="addTable">
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Button ID="btnSubmit" runat="server" Text="Make Inactive" CssClass="btn btn-primary"
                                                                            OnClientClick="return Validation();" OnClick="btnSubmit_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
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
