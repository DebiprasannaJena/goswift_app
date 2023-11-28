<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NodalOfficerView.aspx.cs" Inherits="SingleWindow_NodalOfficerView" %>


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
    <script language="javascript" type="text/javascript">
        pageHeader = "Manage Stakeholders"
        indicate = "yes"
        window.onload = function () {
            configTab();
             configButton();
            configTitleBar();
        }        

//        $(document).ready(function () {
//           
//        });
//        function pageLoad() {
//            groupTable($('#grdOfficers tr:has(td)'), 1, 5);
//            $('#grdOfficers .deleted').remove();
//        }
    </script>
</head>
<body  class="hold-transition sidebar-mini">
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
            <ucLeftMenu:leftMenu ID="LeftPannel1" runat="server" />               
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
                    Manage Stakeholders</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>AMS</a></li><li><a>Manage Officers</a></li>
                </ul>
            </div>
        </section>

          

         <section class="content">
          <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-bd lobidisable">
                          <div class="panel-heading">
                <div class="btn-group buttonlist">
                    <a class="btn btn-add " href="NodalOfficerAdd.aspx"><i class="fa fa-plus"></i>Add
                    </a>
                </div>
                <div class="btn-group buttonlist">
                    <a class="btn btn-add active" href="NodalOfficerView.aspx"><i class="fa fa-file"></i>View
                    </a>
                </div>
            </div>
                            <div class="panel-body">

                                 <div id="divpaging" style="float: right;" runat="server">
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="height: 20px" align="right">
                                                        <span id="spanPaging" runat="server">
                                                            <asp:LinkButton ID="lbtnAll" CssClass="more" runat="server" Text="All" OnClick="lbtnAll_Click"></asp:LinkButton>&nbsp;
                                                            <asp:Label CssClass="tdDataNormal" ID="lblPaging" runat="server"></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                        <div class="viewTable" id="viewTable">
                                            <asp:GridView ID="grdOfficers" runat="server" Width="100%" AllowPaging="True" DataKeyNames="intType"
                                                AutoGenerateColumns="False" OnPageIndexChanging="grdOfficers_PageIndexChanging" OnDataBound="OnDataBound"
                                                OnRowCommand="grdOfficers_RowCommand" class="table table-bordered table-hover">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsl" runat="server" Text='<%#(grdOfficers.PageIndex * grdOfficers.PageSize) + (grdOfficers.Rows.Count + 1)%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="40" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="OType" HeaderText="Officers Type">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Fullname" HeaderText="Officers Name">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DesigName" HeaderText="Designation">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                       <ItemStyle Width="50px" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnEdit" CssClass="btn btn-sm btn-success" runat="server"
                                                                Title="Edit Profile" CommandName="E" CommandArgument='<%# Eval("intType") %>'>
                                                                <i class="fa fa-pencil-square-o"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" CssClass="noPrint" />
                                                        <ItemStyle CssClass="noPrint" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pagination-grid no-print" />
                                                <PagerSettings Mode="NumericFirstLast" NextPageText="Next" FirstPageText="First"
                                                    LastPageText="Last" PreviousPageText="Prev" Position="Bottom" />
                                            </asp:GridView>
                                            <asp:Label ID="lblMessage" runat="server" Text="No Record(s) Found!!!" Visible="false"
                                                CssClass="lblMessage" />
                                        </div>
                            </div>
                       </div>
                     </div>
                    </div>
                                
         </section>
         </div>
          <ucfooter:footer ID="footer2" runat="server" />

    <%--<div id="midbg">
        <div id="midArea">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td id="LeftMenu" valign="top">
                        <!--Left Menu-->
                        <ucLeftMenu:leftMenu ID="leftMenu1" runat="server" />
                    </td>
                    <td valign="top">
                  
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <div class="Container">
                                        <div class="title" id="title">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div id="myButton">
                                        </div>
                               
                                   
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <!--Footer-->
        <ucfooter:footer ID="footer1" runat="server" />
    </div>--%>
    </form>
    </div>
</body>
</html>

