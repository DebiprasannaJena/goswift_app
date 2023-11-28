<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgendaFormView.aspx.cs" Inherits="SingleWindow_AgendaFormView" %>

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

        pageHeader = "Forward Project"

        indicate = "yes"
        backMe = "yes"
        printMe = "yes"

        window.onload = function () {
            configButton();
            configTab();
            configTitleBar()
        }

    </script>
</head>
<body class="hold-transition sidebar-mini">
    <div class="wrapper">
        <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
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
                    Agenda Form</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>AMS</a></li><li><a>Agenda Form</a></li>
                </ul>
            </div>
        </section>
            <section class="content">
           <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-bd lobidrag">
                            <div class="panel-body">
                                <div class="row">
                                <div class="col-sm-12">                                
                                 <asp:UpdatePanel ID="updPanel1" runat="server">
                                <ContentTemplate>
                                    <div id="divpaging" runat="server">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                               <td>
                                                    <p style="margin-top:2px; margin-bottom:0px;"><span style="background:#f00; height:10px; width:10px; display:inline-block; margin-right:5px;"></span><label style="display:inline-block;">Mark indicates reopened agenda</label></p>
                                                </td>
                                                <td >&nbsp;</td>
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
                                        <asp:GridView ID="grdProjmst" runat="server" Width="100%" AllowPaging="true" 
                                            PageSize="10" DataKeyNames="REOPENSTATUS" class="table table-bordered table-hover"
                                           OnPageIndexChanging="grdProjmst_PageIndexChanging" 
                                            AutoGenerateColumns="False" onrowdatabound="grdProjmst_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#"  ItemStyle-Width="20">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(grdProjmst.PageIndex * grdProjmst.PageSize) + (grdProjmst.Rows.Count + 1)%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="VCHPROJCT_NAME" HeaderText="Project Name">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="VCHPROJCT_LOCATION" HeaderText="Location" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="VCHSECTOR" HeaderText="Sector">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                               <%-- <asp:BoundField DataField="VCHCAPACITY" HeaderText="Capacity">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="ENVCATEGORY" HeaderText="Category">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Details">
                                                   <ItemStyle Width="70px" />
                                                    <ItemTemplate>
                                                        <a href="Mom.aspx?ID=<%# Eval("INTPROJCTID") %>" class="btn btn-sm btn-info" title="Details" target="_blank"><i
                                                            class="fa fa-print "></i></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                               
                                            </Columns>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                            <PagerSettings Mode="NumericFirstLast" NextPageText="Next" FirstPageText="First"
                                                LastPageText="Last" PreviousPageText="Prev" Position="Bottom" />
                                        </asp:GridView>
                                        <asp:Label ID="lblMessage" runat="server" Text="No Record(s) Found!!!" Visible="false"
                                            CssClass="lblMessage" />
                                    </div>
                                </ContentTemplate>
                             </asp:UpdatePanel>
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
