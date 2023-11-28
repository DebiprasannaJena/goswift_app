<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublishProjectAdd.aspx.cs"
    Inherits="SingleWindow_PublishProjectAdd" %>

<%@ Register Src="~/Include/IncludeScript.ascx" TagName="IncludeScript" TagPrefix="ucIncludeScript" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Include/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/includes/Leftmenupanel.ascx" TagName="leftMenu" TagPrefix="ucLeftMenu" %>
<%@ Register Src="~/include/AMSfooter.ascx" TagName="footer" TagPrefix="ucfooter" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>SWP</title>
    <script src="../../js/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../js/jQuery.alert.js" type="text/javascript"></script>
    <link href="../../PortalCSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/lobipanel.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/flash.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/pe-icon-7-stroke.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/themify-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/emojionearea.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/stylecrm.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <!-- End Styles
         =====================================================================-->
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../../js/custom.js" type="text/javascript"></script>
    <script src="../../js/Validator.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        //wPageHeader="View Official Holidays"
        pageHeader = "Proposal form for SLSWCA"

        indicate = "yes"
        backMe = "yes"
        printMe = "yes"

        window.onload = function () {
            //            configTitleBar()

        }

        function pageLoad() {
            var frm_hit = 500;
            var location_detl_ftr = '<button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>';

            $('.CDetails').click(function () {
                var url = $(this).attr('data');
                var location_detl_body_cnt = 'ViewComments.aspx?ID=' + url + '';
                var headertxt = 'Comment Details'
                openPageModalnHdr(location_detl_body_cnt, location_detl_ftr, frm_hit, headertxt);
            });

            $('.CMDetails').click(function () {
                var url = $(this).attr('data');
                var location_detl_body_cnt = 'ViewCmdComments.aspx?ID=' + url + '';
                var headertxt = 'CMD Comment Details'
                openPageModalnHdr(location_detl_body_cnt, location_detl_ftr, frm_hit, headertxt);
            });
            $('.ActionDetails').click(function () {
                var url = $(this).attr('data');
                var location_detl_body_cnt = 'ViewActionDetails.aspx?ID=' + url + '';
                var headertxt = 'Action Details'
                openPageModalnHdrAction(location_detl_body_cnt, location_detl_ftr, frm_hit, headertxt);
            });
            var frm_hit1 = 600;
            var location_detl_ftr1 = '<button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>';
            $('.Comment').click(function () {
                var url = $(this).attr('cmt');
                var location_detl_body_cnt1 = 'ViewClarification.aspx?ID=' + url + '';
                var headertxt = 'Edit Members Comment'
                openCModalnHdr(location_detl_body_cnt1, location_detl_ftr1, frm_hit1, headertxt);
            });


        }

        function openPageModalnHdrAction(page, footer, frm_hit, header) {
            $('#pageModalAction .modal-body').html("<iframe width='100%' height='" + frm_hit + "px' src='" + page + "' frameborder='0' scrolling='yes'></iframe>");
            $('#pageModalAction .modal-footer').html(footer);
            if (footer == "") { $('#pageModal .modal-footer').remove(); }
            $('#pageModalAction').modal();
            $('#myModal').hide();
        }

        function openCModalnHdr(page, footer, frm_hit, header) {
            $('#pageModal .modal-body').html("<iframe width='100%' height='" + frm_hit + "px' src='" + page + "' frameborder='0' scrolling='yes'></iframe>");
            $('#pageModal .modal-footer').html(footer);
            if (footer == "") { $('#pageModal .modal-footer').remove(); }
            $('#pageModal').modal();
            $('#myModal').hide();
            $('#SendModal').hide();
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

        //        function CModal(id, headText, PName) {
        //            $('#SendModal').modal();
        //            $('#myModal').hide();
        //            $('#SendModal .modal-title').text(headText);
        //            $('#hdnId').val(id);
        //            $('#hdName').val(PName);
        //        }

        function Validation() {

            if (!blankFieldValidation('txtRemark', ' Remarks')) { return false; }
            if (!WhiteSpaceValidation1st('txtRemark', 'Remarks')) { return false; }
        }

        function CValidation() {
            debugger;
            if ($('#cblMember :checkbox:checked').length <= 0) {
                alert('Select SLFC Member');
                return false;
            };
            if (!blankFieldValidation('txtClarification', ' Clarification')) { return false; }
            if (!WhiteSpaceValidation1st('txtClarification', 'Clarification')) { return false; }

        }
        $(window).load(function () {
            $('.customclose').click(function () {
                //alert('hii');
                $('#pnlPopup').hide();
            })

        })
           
    </script>
    <style>
        .show-data input[type="checkbox"]
        {
            float: left !important;
            margin-right: 10px !important;
            margin-top: 3px !important;
        }
        
        .modal
        {
            display: none;
        }
        .modalPopup
        {
            background-color: rgba(0,0,0,0.51);
            height: 100%;
            width: 100%;
            z-index: 9;
            text-align: center;
        }
        .custom-modal
        {
            width: 50%;
            margin: 5% auto;
            background-color: #FFF;
        }
        .custom-modal td
        {
            padding: 6px;
        }
    </style>
</head>
<body class="hold-transition sidebar-mini">
    <div class="wrapper">
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
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
                                    <a class="btn btn-add active" href="PublishProjectAdd.aspx"><i class="fa fa-plus"></i>
                                        Add </a>
                                </div>
                                <div class="btn-group buttonlist">
                                    <a class="btn btn-add " href="PublishProjectView.aspx"><i class="fa fa-file"></i>View
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
                            <div class="panel-body">
                                <asp:UpdatePanel ID="updPanel1" runat="server">
                                    <ContentTemplate>
                                        <div id="divpaging" runat="server">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <p style="margin-top: 2px; margin-bottom: 0px;">
                                                            <span class="legend-box red-bg"></span>
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
                                            <asp:GridView ID="grdProjmst" runat="server" Width="100%" AllowPaging="true" PageSize="10"
                                                AutoGenerateColumns="False" OnRowCommand="grdProjmst_OnRowCommand" DataKeyNames="INTSTATUS,INTPROJCTID,NOOFMEMBER,NOOFCOMMENT,NOOFDAYS,Decission,VCHPROJCT_NAME,REOPENSTATUS"
                                                OnRowDataBound="grdProjmst_RowDataBound" class="table table-bordered table-hover"
                                                OnPageIndexChanging="grdProjmst_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsl" runat="server" Text='<%#(grdProjmst.PageIndex * grdProjmst.PageSize) + (grdProjmst.Rows.Count + 1)%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project Name">
                                                        <ItemTemplate>
                                                            <a href="AgendaPrint.aspx?ID=<%# Eval("INTPROJCTID") %>" title="Details" target="_blank">
                                                                <%# Eval("VCHPROJCT_NAME")%></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="VCHPROJCT_LOCATION" HeaderText="Location" Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VCHSECTOR" HeaderText="Sector">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ECATEGORY" HeaderText="Category">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PTYPE" HeaderText="Type">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Comment Details">
                                                        <ItemTemplate>
                                                            <a href="javascript:void(0)" title="SLFC Members Comment" class="CDetails" data='<%# Eval("INTPROJCTID") %>'>
                                                                <i class="fa fa-comment"></i></a>&nbsp;&nbsp;
                                                            <asp:HyperLink ID="hlEdit" class="Comment" runat="server" cmt='<%# Eval("INTPROJCTID") %>'
                                                                ToolTip="Edit Comments">
                                                        <i class="fa fa-pencil-square-o"></i>
                                                      
                                                            </asp:HyperLink>
                                                            &nbsp;&nbsp; <a href="javascript:void(0)" id="CMDCmnt" title="CMD Comment" class="CMDetails"
                                                                data='<%# Eval("INTPROJCTID") %>' runat="server"><i class="fa fa-comment"></i>
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Clarification">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnSend" class="btn btn-mini btn-danger" runat="server" data='<%# Eval("INTPROJCTID") %>'
                                                                CommandName="Send" Text="Send" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'>                                                                
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Forward">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnForward" class="btn btn-mini btn-danger" Text="Forward" runat="server"
                                                                data='<%# Eval("INTPROJCTID") %>' CommandName="Forward" OnClientClick="return confirm(' Are you sure want to forward the Project to CMD, IPICOL!')"
                                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'>                                                               
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Publish">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnPublish" class="btn btn-mini btn-danger" Text="Publish" runat="server"
                                                                data='<%# Eval("INTPROJCTID") %>' CommandName="Publish" OnClientClick="return confirm(' Are you sure want to Publish the Project!')"
                                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'>                                                               
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reject">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnReturn" class="btn btn-mini btn-danger" Text="Reject" runat="server"
                                                                data='<%# Eval("INTPROJCTID") %>' CommandName="Reject" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'>     
                                                           <%-- <i class="fa fa-hand-o-up "></i>    --%>                                                       
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Details">
                                                        <ItemTemplate>
                                                            <%--<asp:LinkButton ID="ActionDtls" class="ActionDetails" runat="server" title="Action Details"
                                                            data='<%# Eval("INTPROJCTID") %>'>     
                                                            <i class="fa fa-hand-o-up"></i>                                                           
                                                        </asp:LinkButton>--%>
                                                            <a href="javascript:void(0)" id="ActionDtls" title="Action Details" class="ActionDetails"
                                                                data='<%# Eval("INTPROJCTID") %>' runat="server"><i class="fa fa-list"></i></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CMDRemark" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnCMDRmk" runat="server" Value='<%#Eval("CMDRemark") %>' />
                                                        </ItemTemplate>
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
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="grdProjmst" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
        <%--<div id="midArea">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td id="LeftMenu" valign="top">
                        <!--Left Menu-->
                        <ucLeftMenu:leftMenu ID="LeftPannel1" runat="server" />
                    </td>
                    <td valign="top">
                       
                        <div class="Container">
                            <div class="title" id="title">
                            </div>
                            <div class="clear">
                            </div>
                            <div id="myButton">
                            </div>
                          
                            <div id="MyTab">
                            </div>
                            
                        </div>
                    </td>
                </tr>
            </table>
        </div>--%>
        <ucfooter:footer ID="footer1" runat="server" />
        <!-- Modal -->
        <!-- Large Modal Window Panel -->
        <div class="modal fade bs-example-modal-lg" id="pageModal" tabindex="-1" role="dialog"
            aria-labelledby="myModalLabel" aria-hidden="true" style="width: 800px;">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title" id="myModalLabel">
                            Feedback Details
                        </h4>
                    </div>
                    <div class="modal-body">
                    </div>
                    <div class="modal-footer">
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal -->
        <script src="../js/bootstrap-datepicker.js" type="text/javascript"></script>
        <div class="modal fade" id="myModal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">
                            Modal title</h4>
                    </div>
                    <div class="modal-body">
                        <table>
                            <tr>
                                <td width="125px">
                                    Remarks
                                </td>
                                <td width="5px">
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRemark" runat="server" MaxLength="100" Width="300px" TextMode="MultiLine"
                                        Rows="4" onkeypress="return TextCounter('txtRemark','Label1',100)" />
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,custom"
                                        ValidChars="#$^&*()_+[]{}?:;|'\\\,./~`-= " TargetControlID="txtRemark" InvalidChars="!<>%">
                                    </cc1:FilteredTextBoxExtender>
                                    <span class="mandatory">&nbsp;*</span> Maximum <span class="mandatory">
                                        <asp:Label ID="Label1" runat="server" Text="100"></asp:Label>
                                    </span>&nbsp;characters
                                    <asp:HiddenField ID="hdName" runat="server" />
                                    <asp:HiddenField ID="hdnId" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <table border="0" cellspacing="0" cellpadding="0" class="addTable">
                            <tr>
                                <td width="110">
                                </td>
                                <td width="10">
                                </td>
                                <td>
                                    <%--    <asp:Button ID="btnAccept" runat="server" Text="Accept" CssClass="btn btn-success"
                                    OnClientClick="return Validation();" OnClick="btnAccept_Click" />--%>
                                    <asp:Button ID="btnReturn" runat="server" Text="Submit" CssClass="btn btn-success"
                                        OnClientClick="return Validation();" OnClick="btnReturn_Click" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                        Close</button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
        <%--<div class="modal fade" id="SendModal" >
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
                                <td>
                                    SLFC Member
                                </td>
                                <td width="5px">
                                    :
                                </td>
                                <td>
                                    <div class="viewTable show-data" id="Div1">
                                        <asp:CheckBoxList ID="cblMember" runat="server">
                                        </asp:CheckBoxList>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td width="125px">
                                    Clarification
                                </td>
                                <td width="5px">
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtClarification" runat="server" MaxLength="500" Width="300px" TextMode="MultiLine"
                                        Rows="4" onkeypress="return TextCounter('txtClarification','lblText',500)" />
                                    <cc1:FilteredTextBoxExtender ID="FEClarification" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,custom"
                                        ValidChars="#$^&*()_+[]{}?:;|'\\\,./~`-= " TargetControlID="txtClarification"
                                        InvalidChars="!<>%">
                                    </cc1:FilteredTextBoxExtender>
                                    <span class="mandatory">&nbsp;*</span> Maximum <span class="mandatory">
                                        <asp:Label ID="lblText" runat="server" Text="500"></asp:Label>
                                    </span>&nbsp;characters
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <table border="0" cellspacing="0" cellpadding="0" class="addTable">
                            <tr>
                                <td width="110">
                                </td>
                                <td width="10">
                                </td>
                                <td>
                                    <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-success" OnClick="btnSend_Click"
                                        OnClientClick="return CValidation();" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                        Close</button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>--%>
        <asp:UpdatePanel ID="update11" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" OnClientClick="return ShowModalPopup()" />
                <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
                    PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                    <div class="custom-modal">
                        <div class="modal-body">
                            <table id="tblSend" width="100%">
                                <tr>
                                    <td>
                                        SLFC Member
                                    </td>
                                    <td width="5px">
                                        :
                                    </td>
                                    <td>
                                        <div class="viewTable show-data" id="Div1">
                                            <asp:CheckBoxList ID="cblMember" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="125px">
                                        Clarification <span class="text-danger">*</span>
                                    </td>
                                    <td width="5px">
                                        :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtClarification" runat="server" MaxLength="500" Width="300px" TextMode="MultiLine"
                                            Rows="4" onkeypress="return TextCounter('txtClarification','lblText',500);" />
                                        <cc1:FilteredTextBoxExtender ID="FEClarification" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,custom"
                                            ValidChars="#$^&*()_+[]{}?:;|'\\\,./~`-= " TargetControlID="txtClarification"
                                            InvalidChars="!<>%">
                                        </cc1:FilteredTextBoxExtender>
                                        <br>
                                        <small class="text-danger">Maximum <span class="mandatory">
                                            <asp:Label ID="lblText" runat="server" Text="500"></asp:Label>
                                        </span>&nbsp;characters</small>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <div class=" text-center">
                                <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-success" OnClick="btnSend_Click"
                                    OnClientClick="return CValidation();" />
                                <button type="button" class="btn btn-default customclose">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSend" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="modal fade" id="pageModalAction" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title" id="H1">
                            Action Details
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
