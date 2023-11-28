<%-- File Name             :   SingleWindow/FinanceDetailsView.aspx
 Description           :   To View Finance Details to Accountant
 Created by            :   Surya Prakash Barik
 Created on            :   30-OCT-2017
 Modification History  :
       <CR no.>                      <Date>                    <Modified by>                <Modification Summary>'                                                          
********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinancialDetailsView.aspx.cs"
    Inherits="SingleWindow_FinancialDetailsView" %>

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

        //wPageHeader="View Official Holidays"
        pageHeader = "Financial Details View"

        indicate = "yes"
        backMe = "yes"
        printMe = "yes"

        window.onload = function () {
            //            configButton();
            //            configTab();
            configTitleBar()
        }

        function pageLoad() {
            var frm_hit = 500;
            var location_detl_ftr = '<button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>';

            $('.Financial').click(function () {
                var url = $(this).attr('data');
                var location_detl_body_cnt = 'FinancialDetailShow.aspx?ID=' + url + '';
                var headertxt = 'Financial Details'
                openPageModalnHdr(location_detl_body_cnt, location_detl_ftr, frm_hit, headertxt);
            });

        }

        function CModal(id, headText, cmt, cnt) {
            $('#SendModal').modal();
            $('#myModal').hide();
            $('#SendModal .modal-title').text(headText);
            $('#hdnId').val(id);
            $('#txtComment').val(cmt);
            $('#lblCnt').text(cnt);

        }

        function openPageModalnHdr(page, footer, frm_hit, header) {
            $('#pageModal .modal-body').html("<iframe width='100%' height='" + frm_hit + "px' src='" + page + "' frameborder='0' scrolling='yes'></iframe>");
            $('#pageModal .modal-footer').html(footer);
            if (footer == "") { $('#pageModal .modal-footer').remove(); }
            $('#pageModal').modal();
        }

        function Validation() {
            if (!blankFieldValidation('txtComment', ' Comment')) { return false; }
            if (!WhiteSpaceValidation1st('txtComment', 'Comment')) { return false; }
        }

        function TextCounter(ctlTxtName, lblCouter, numTextSize) {
            var txtName = document.getElementById(ctlTxtName).value;
            var txtNameLength = txtName.length;
            if (parseInt(txtNameLength) > parseInt(numTextSize)) {
                var txtMaxTextSize = txtProjectDesc.substr(0, numTextSize)
                document.getElementById(ctlTxtName).value = txtMaxTextSize;
                alert("Entered Text Exceeds '" + numTextSize + "' Characters.");
                document.getElementById(lblCouter).innerHTML = 0;
                return false;
            }
            else {
                document.getElementById(lblCouter).innerHTML = parseInt(numTextSize) - parseInt(txtNameLength);
                return true;
            }
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
                    Financial Details</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>AMS</a></li><li><a>Financial Details</a></li>
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
                                        <asp:GridView ID="grdProjmst" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                                            DataKeyNames="INTPROJCTID,FINDOC" OnPageIndexChanging="grdProjmst_PageIndexChanging"
                                            OnRowCommand="grdProjmst_RowCommand" OnRowDataBound="grdProjmst_RowDataBound"
                                            class="table table-bordered table-hover">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(grdProjmst.PageIndex * grdProjmst.PageSize) + (grdProjmst.Rows.Count + 1)%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="VCHPROJCT_NAME" HeaderText="Company Name">
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
                                                <asp:TemplateField HeaderText="Details">
                                                    <ItemTemplate>
                                                        <a href="../AMS/ProjectDetails.aspx?ID=<%# Eval("INTPROJCTID") %>" title="Details"
                                                            target="_blank"><i class="fa fa-bars  "></i></a>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Financial Document">
                                                    <ItemTemplate>
                                                        <a href="javascript:void(0)" title="Details" class="Financial" data='<%# Eval("INTPROJCTID") %>'>
                                                            <i class="fa fa-university "></i></a>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="180px" CssClass="noPrint" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnReturn" class="btn btn-sm btn-danger" Text="Comment"
                                                            runat="server" data='<%# Eval("INTPROJCTID") %>' CommandName="Comment" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'>     
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="80px" CssClass="noPrint" />
                                                    <ItemStyle CssClass="noPrint" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                            <PagerSettings Mode="NumericFirstLast" NextPageText="Next" FirstPageText="First"
                                                LastPageText="Last" PreviousPageText="Prev" Position="Bottom" />
                                        </asp:GridView>
                                        <asp:Label ID="lblMessage" runat="server" Text="No Record(s) Found!!!" Visible="false"
                                            CssClass="lblMessage norecord" />
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
        <!-- Modal -->
        <!-- Large Modal Window Panel -->
        <div class="modal fade bs-example-modal-lg" id="pageModal" tabindex="-1" role="dialog"
            aria-labelledby="myModalLabel" aria-hidden="true" style="width: 900px;">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title" id="myModalLabel">
                            Financial Documents
                        </h4>
                    </div>
                    <div class="modal-body">
                    </div>
                    <div class="modal-footer">
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="SendModal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">
                            Modal title</h4>
                    </div>
                    <div class="modal-body">
                        <table id="tblSend">
                            <tr>
                                <td width="125px">
                                    Comment <span class="text-danger">*</span>
                                </td>
                                <td width="170px">
                                    <asp:TextBox runat="server" ID="txtComment" MaxLength="500" CssClass="form-control"
                                        Width="400px" TextMode="MultiLine" Rows="3" onkeyup="return TextCounter('txtComment','lblCnt',500)"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FEtxtComment" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,custom"
                                        ValidChars=" /.,()&-:_/?%" TargetControlID="txtComment" InvalidChars="'*|">
                                    </cc1:FilteredTextBoxExtender>
                                     <small class="text-danger">Maximum <span class="mandatory">
                                        <asp:Label ID="lblCnt" runat="server" Text="500"></asp:Label>
                                    </span>&nbsp;characters</small>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <table border="0" cellspacing="0" cellpadding="0" class="addTable">
                            <tr>
                                <td width="110">
                                </td>
                                <td width="15">
                                </td>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-md btn-success"
                                        OnClientClick="return Validation();" OnClick="btnSubmit_Click" />
                                    <button type="button" class="btn btn-md btn-danger" data-dismiss="modal">
                                        Close</button>
                                    <asp:HiddenField ID="hdnId" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        </form>
    </div>
</body>
</html>
