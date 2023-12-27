﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublishProjectView.aspx.cs"
    Inherits="SingleWindow_PublishProjectView" %>

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

        pageHeader = "Proposal form for SLSWCA"
        indicate = "yes"
        backMe = "yes"
        printMe = "yes"
        window.onload = function () {
            configButton();
            configTab();
            configTitleBar()
            CheckUncheckGrid();
        }

    </script>
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            $("#btnArchieve").click(function (e) {
                totChecked = $('.RowCheck input[type="checkbox"]:checked').length;
                if (totChecked == 0) {
                    alert('Select Record(s) Want To Archive');
                    e.preventDefault();
                    return;
                }
                if (!ConfirmDelete('Are you sure you want to Archive ?')) {
                    return false;
                }
            });
            var frm_hit = 500;
            var location_detl_ftr = '<button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>';
            $('.CDetails').click(function () {
                var url = $(this).attr('data');
                var location_detl_body_cnt = 'ViewCommentHistory.aspx?ID=' + url + '';
                var headertxt = 'Comment Details'
                openPageModalnHdr(location_detl_body_cnt, location_detl_ftr, frm_hit, headertxt);
            });
        }

        function openPageModalnHdr(page, footer, frm_hit, header) {
            $('#pageModal .modal-body').html("<iframe width='100%' height='" + frm_hit + "px' src='" + page + "' frameborder='0' scrolling='yes'></iframe>");
            $('#pageModal .modal-footer').html(footer);
            if (footer == "") { $('#pageModal .modal-footer').remove(); }
            $('#pageModal').modal();
        }

        function Model(id, headText, PName) {
            $('#myModal').modal();
            $('#SendModal').hide();
            $('#myModal .modal-title').text(headText);
            $('#hdnId').val(id);
            $('#hdName').val(PName);
        }

        function CModal(id, headText, PName) {
            $('#SendModal').modal();
            $('#myModal').hide();
            $('#SendModal .modal-title').text(headText);
            $('#hdnId').val(id);
            $('#hdName').val(PName);
        }

        function Validation() {
            if (!blankFieldValidation('txtRemark', ' Remarks')) { return false; }
            if (!WhiteSpaceValidation1st('txtRemark', 'Remarks')) { return false; }

        }

        function CValidation() {
            if ($('#cblMember :checkbox:checked').length <= 0) {
                alert('Select SLFC Member');
                return false;
            };
            if (!blankFieldValidation('txtClarification', ' Clarification')) { return false; }
            if (!WhiteSpaceValidation1st('txtClarification', 'Clarification')) { return false; }

        }
    </script>
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
            <ucLeftMenu:leftMenu ID="LeftMenu1" runat="server" />               
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
                    Proposal form for SLSWCA</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>AMS</a></li><li><a>Proposal for SLSWCA</a></li>
                </ul>
            </div>
        </section>
            <section class="content">
                <div class="row">
                   <div class="col-md-12">
                 <div class="panel panel-bd lobidisable">
                <div class="panel-heading">
                    <div class="btn-group buttonlist">
                        <a class="btn btn-add " href="PublishProjectAdd.aspx"><i class="fa fa-plus"></i>Add
                        </a>
                    </div>
                    <div class="btn-group buttonlist">
                        <a class="btn btn-add active" href="PublishProjectView.aspx"><i class="fa fa-file"></i>View
                        </a>
                    </div>
                    <div class="btn-group buttonlist">
                        <a class="btn btn-add " href="archiveAgendaForm.aspx"><i class="fa fa-file"></i>Archieve
                        </a>
                    </div>
                    <div class="btn-group buttonlist">
                        <a class="btn btn-add " href="DeferedProjectList.aspx"><i class="fa fa-file"></i>Reject
                        </a>
                    </div>
                </div>
          
                     
                            <div class="panel panel-bd lobidrag">
                                <div class="panel-body">
                                <asp:UpdatePanel ID="updPanel1" runat="server">
                                <ContentTemplate>
                                    <div id="divpaging" runat="server">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
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
                                        <asp:GridView ID="grdProjmst" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="grdProjmst_PageIndexChanging"
                                         DataKeyNames="INTPROJCTID,INTREOPENID" OnRowCommand="grdProjmst_RowCommand" OnRowDataBound="grdProjmst_RowDataBound" 
                                         class="table table-bordered table-hover">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox runat="server" ID="chkSelectAll" ToolTip="Check All" class="noPrint" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="chkSelectSingle" ToolTip="Check" class="noPrint RowCheck" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(grdProjmst.PageIndex * grdProjmst.PageSize) + (grdProjmst.Rows.Count + 1)%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="30px" />
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
                                                <asp:BoundField DataField="ENVCATEGORY" HeaderText="Environment Category">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                    <asp:TemplateField HeaderText="View Comment History">
                                                    <ItemTemplate>
                                                        <a href="javascript:void(0)" title="Details" class="CDetails" data='<%# Eval("INTPROJCTID") %>'>
                                                            <i class="fa fa-comment"></i></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Print Form">
                                                    <ItemTemplate>
                                                        <a href="Mom.aspx?ID=<%# Eval("INTPROJCTID") %>" title="Print" target="_blank"><i
                                                            class="fa fa-print"></i></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reopen" >
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnReopen" class="btn btn-sm btn-danger" Text="Reopen" runat="server"
                                                            data='<%# Eval("INTPROJCTID") %>' CommandName="Reopen" OnClientClick="return confirm(' Are you sure to Re-Open this Published Project!')"
                                                              CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'>     
                                                     <%--     <i class="fa fa-hand-o-up "></i>    --%>                                                       
                                                        </asp:Button>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                            <PagerSettings Mode="NumericFirstLast" NextPageText="Next" FirstPageText="First"
                                                LastPageText="Last" PreviousPageText="Prev" Position="Bottom" />
                                        </asp:GridView>
                                        <asp:Button ID="btnArchieve" runat="server" Text="Archive" CssClass="btn btn-success"
                                            OnClientClick="return Validation();" TabIndex="1" OnClick="btnArchieve_Click" />
                                        <asp:Label ID="lblMessage" runat="server" Text="No Record(s) Found!!!" Visible="false"
                                            CssClass="lblMessage norecord" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                       <asp:PostBackTrigger ControlID="grdProjmst" />
                                 </Triggers>
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
        <!-- Large Modal Window Panel -->
        <div class="modal fade bs-example-modal-lg" id="pageModal" tabindex="-1" role="dialog"
            aria-labelledby="myModalLabel" aria-hidden="true" style="width: 700px;">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title" id="myModalLabel">
                            Comments of SLFC Members
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