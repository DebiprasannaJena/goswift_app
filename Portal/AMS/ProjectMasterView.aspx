<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectMasterView.aspx.cs"
    Inherits="SingleWindow_ProjectMasterView" %>

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
    <style>
        .modal-body iFrame
        {
            height: 350px;
        }
    </style>
    <script language="javascript" type="text/javascript">

        pageHeader = "View Project Master"
        indicate = "yes"
        backMe = "yes"
        printMe = "yes"

        window.onload = function () {
            //            configButton();
            //            configTab();
            configTitleBar()
        }

        function pageLoad() {
            var frm_hit = 1000;
            var location_detl_ftr = '<button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>';

            $('.PDetails').click(function () {
                var url = $(this).attr('data');
                var location_detl_body_cnt = 'ProjectDetailsView.aspx?ID=' + url + '';
                var headertxt = 'Project Details'
                openPageModalnHdr(location_detl_body_cnt, location_detl_ftr, frm_hit, headertxt);
            });

            $('.Financial').click(function () {
                var url = $(this).attr('data');
                var location_detl_body_cnt = 'FinancialPerformanceView.aspx?ID=' + url + '';
                var headertxt = 'Financial Details'
                openPageModalnHdr(location_detl_body_cnt, location_detl_ftr, frm_hit, headertxt);
            });

            $('.Proposal').click(function () {
                var url = $(this).attr('data');
                var location_detl_body_cnt = 'ProposalMasterView.aspx?ID=' + url + '';
                var headertxt = 'Proposal Details'
                openPageModalnHdr(location_detl_body_cnt, location_detl_ftr, frm_hit, headertxt);
            });
            $('.Decision').click(function () {
                var url = $(this).attr('data');
                var location_detl_body_cnt = 'SLFCDecisionView.aspx?ID=' + url + '';
                var headertxt = 'Decision Point Details'
                openPageModalnHdr(location_detl_body_cnt, location_detl_ftr, frm_hit, headertxt);
            });
            $('.Project').click(function () {
                var url = $(this).attr('data');
                var location_detl_body_cnt = 'ProjectView.aspx?ID=' + url + '';
                var headertxt = 'Decision Point Details'
                openPageModalnHdr(location_detl_body_cnt, location_detl_ftr, frm_hit, headertxt);
            });

            $('.FinDoc').click(function () {
                var url = $(this).attr('data');
                var location_detl_body_cnt = 'FinancialDetailShow.aspx?ID=' + url + '';
                var headertxt = 'Financial Documents'
                openPageModalnHdr(location_detl_body_cnt, location_detl_ftr, frm_hit, headertxt);
            });

        }

        function openPageModalnHdr(page, footer, frm_hit, header) {
            $('#pageModal .modal-body').html("<iframe width='100%' height='" + frm_hit + "px' src='" + page + "' frameborder='0' scrolling='yes'></iframe>");
            $('#pageModal .modal-footer').html(footer);
            if (footer == "") { $('#pageModal .modal-footer').remove(); }
            $('#pageModal').modal();
        }

    </script>
    <style type="text/css">
        a
        {
            text-decoration: none !important;
        }
    </style>
</head>
<body class="hold-transition sidebar-mini">
    <div class="wrapper">
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <ucheader:header ID="header1" runat="server" />
        <aside class="main-sidebar">
            <!-- sidebar -->
            <div class="sidebar">
                <!-- sidebar menu -->
                <ucLeftMenu:leftMenu ID="leftMenu2" runat="server" />
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
                        View Project Master</h1>
                    <ul class="breadcrumb">
                        <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                        <li><a>AMS</a></li><li><a>Edit/View Project</a></li>
                    </ul>
                </div>
            </section>
            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-bd lobidrag">
                            <div class="panel-body">
                                <asp:UpdatePanel ID="updPanel1" runat="server">
                                    <ContentTemplate>
                                        <div id="divpaging" runat="server">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <p style="margin-top: 2px; margin-bottom: 0px;">
                                                            <span style="background: #ff4348; height: 10px; width: 10px; display: inline-block;
                                                                margin-right: 5px;"></span>
                                                            <label style="display: inline-block;">
                                                                Mark indicates reopened agenda</label></p>
                                                    </td>
                                                    <td width="50px">
                                                        &nbsp;
                                                    </td>
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
                                            <asp:GridView ID="grdProjmst" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                                                class="table table-bordered table-hover" OnRowCommand="grdProjmst_RowCommand"
                                                DataKeyNames="ProposalId,DetailsId,FinanceId,INTSTATUS,DecisionId,FinDoc,REOPENSTATUS"
                                                OnRowDataBound="grdProjmst_RowDataBound" OnPageIndexChanging="grdProjmst_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsl" runat="server" Text='<%#(grdProjmst.PageIndex * grdProjmst.PageSize) + (grdProjmst.Rows.Count + 1)%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company Name">
                                                        <ItemTemplate>
                                                            <a href="javascript:void(0)" title="Click For Details" class="Project" data='<%# Eval("INTPROJCTID") %>'>
                                                                <%# Eval("VCHPROJCT_NAME")%></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="VCHPROJCT_LOCATION" HeaderText="Location" Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VCHPRODUCT" HeaderText="Product" Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VCHSECTOR" HeaderText="Sector">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CategoryId" HeaderText="Category">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Proposal">
                                                        <ItemTemplate>
                                                            <a href="javascript:void(0)" title="Details" class="Proposal" data='<%# Eval("INTPROJCTID") %>'>
                                                                <i class="fa fa-file-text-o "></i></a>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="70px" CssClass="noPrint" />
                                                        <FooterStyle />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Financial">
                                                        <ItemTemplate>
                                                            <a href="javascript:void(0)" title="Details" class="Financial" data='<%# Eval("INTPROJCTID") %>'>
                                                                <i class="fa fa-university "></i></a>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="70px" CssClass="noPrint" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Financial Documents">
                                                        <ItemTemplate>
                                                            <a href="javascript:void(0)" title="Financial Documents" class="FinDoc" data='<%# Eval("INTPROJCTID") %>'>
                                                                <i class="fa fa-file-text-o"></i></a>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="70px" CssClass="noPrint" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Details">
                                                        <ItemTemplate>
                                                            <a href="javascript:void(0)" title="Details" class="PDetails" data='<%# Eval("INTPROJCTID") %>'>
                                                                <i class="fa fa-list-alt  "></i></a>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="70px" CssClass="noPrint" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Decision">
                                                        <ItemTemplate>
                                                            <a href="javascript:void(0)" title="Details" class="Decision " data='<%# Eval("INTPROJCTID") %>'>
                                                                <i class="fa fa-list-alt  "></i></a>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="70px" CssClass="noPrint" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnEdit" CssClass="btn btn-sm btn-success" runat="server"
                                                                CommandName="E" OnClientClick="return confirm(' Are you sure want to Edit the selected Item!')"
                                                                CommandArgument='<%# Convert.ToString(Eval("INTPROJCTID")) %>'>
                                                                <i class="fa fa-pencil-square-o"></i>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnDelete" CssClass="btn btn-sm btn-danger" runat="server"
                                                                CommandName="D" OnClientClick="return confirm('Do you want to delete this record?');"
                                                                CommandArgument='<%#Eval("INTPROJCTID")%>'>
                                                                <i class="fa fa-trash-o"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="90px" CssClass="noPrint" />
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
        <ucfooter:footer ID="footer1" runat="server" />
        <!-- Large Modal Window Panel -->
        <div class="modal fade bs-example-modal-lg" id="pageModal" tabindex="-1" role="dialog"
            aria-labelledby="myModalLabel" aria-hidden="true" style="width: 900px;">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title" id="myModalLabel">
                            Project Details
                        </h4>
                    </div>
                    <div class="modal-body">
                    </div>
                    <div class="modal-footer">
                    </div>
                </div>
            </div>
        </div>
        </form>
    </div>
</body>
</html>
