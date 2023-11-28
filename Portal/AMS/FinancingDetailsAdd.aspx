<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinancingDetailsAdd.aspx.cs"
    Inherits="SingleWindow_FinancingDetailsAdd" %>

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
    <script src="../../js/Validator.js" type="text/javascript"></script>
    <script src="../js/jquery.timepicker.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        pageHeader = "Manage Project Details"
        indicate = "yes"
        window.onload = function () {
            configTitleBar();
        }
        function Load() {
            SetVisible();
        }
        function pageLoad() {

            $(function () {
                $("[id*=grvFinDtls] [id*=BtnAddMoreFin]").click(function () {
                    var row = $(this).closest("tr");
                    var ddlFin = row.find("[id*=ddlFinDtls]");
                    var txtFinAmnt = row.find("[id*=txtFinAmnt]");
                    var message = "";
                    if (ddlFin.val() == "0" && ($.trim(txtFinAmnt.val()) != "")) {
                        message += "Please select Financing Description";
                        ddlFin.focus();
                    }
                    if ($.trim(txtFinAmnt.val()) == "" && ddlFin.val() != "0") {
                        message += "Please enter Amount.\n";
                        txtFinAmnt.focus();
                    }
                    if ($.trim(txtFinAmnt.val()) == "" && ddlFin.val() == "0") {
                        message += "Please give both Description and Amount\n";
                        ddlFin.focus();
                    }
                    if (message != "") {
                        alert(message);
                        return false;
                    }
                    return true;
                });
            });
        }
    </script>
    <style>
        .radiobtn
        {
            padding: 0px;
            margin-top: -3px;
        }
        .radiobtn label
        {
            float: right;
            margin-left: 10px;
            margin-top: 4px;
        }
        .radiobtn input[type="radio"], input[type="checkbox"]
        {
            margin-top: 7px;
        }
        .img-btn
        {
            border-width: 0px;
            padding: 8px 15px;
            border: 1px solid #c1c1c1 !important;
            margin-bottom: 5px;
            width: 7px !important;
        }
        
        
        .steps
        {
            padding-left: 0;
            list-style: none;
            font-size: 12px;
            line-height: 1;
            margin: 0 auto 20px;
            border-radius: 3px;
            width: 72%;
        }
        
        .steps strong
        {
            font-size: 14px;
            display: block;
            line-height: 1.4;
        }
        .steps h4
        {
            line-height: 24px;
            font-size: 16px;
            margin: 4px;
        }
        .steps > li
        {
            position: relative;
            display: block;
            width: auto;
        }
        .steps > li a
        {
            padding: 6px 20px 6px 44px;
            display: block;
            color: #000;
        }
        .steps > li
        {
            display: inline-block;
        }
        .steps .past
        {
            color: #fff;
            background: #ff8184;
        }
        .steps .present
        {
            color: #000;
            background: #ed3237;
        }
        .steps .present a
        {
            color: #FFF;
        }
        .steps .future
        {
            color: #777;
            background: #efefef;
        }
        .steps .past a
        {
            color: #fff !important;
            display: block;
        }
        .steps li > span:after, .steps li > span:before
        {
            content: "";
            display: block;
            width: 0px;
            height: 0px;
            position: absolute;
            top: 0;
            left: -6px;
            border: solid transparent;
            border-left-color: #f0f0f0;
            border-width: 22px;
        }
        
        .steps li > span:after
        {
            top: -5px;
            z-index: 1;
            border-left-color: white;
            border-width: 27px;
        }
        
        .steps li > span:before
        {
            z-index: 2;
        }
        
        .steps li.past + li > span:before
        {
            border-left-color: #ff8184;
        }
        .steps li.present + li > span:before
        {
            border-left-color: #ed3237;
        }
        .steps li.future + li > span:before
        {
            border-left-color: #efefef;
        }
        
        .steps li:first-child > span:after, .steps li:first-child > span:before
        {
            display: none;
        }
        
        /* Arrows at start and end */
        .steps li:first-child i, .steps li:last-child i
        {
            display: block;
            position: absolute;
            top: 0;
            left: -6;
            border: solid transparent;
            border-left-color: white;
            border-width: 22px;
        }
        
        .steps li:last-child i
        {
            left: auto;
            right: -25px;
            border-left-color: transparent;
            border-top-color: white;
            border-bottom-color: white;
        }
    </style>
</head>
<body class="hold-transition sidebar-mini">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $('#btnSubmit').click(function (e) {
                debugger;
                var counts = '<%=grvFinDtls.Rows.Count %>'
                var counts = $("[id*=grvFinDtls] tr").length;
                var $ddlFinDtls = $("#<%= grvFinDtls.ClientID %> input[id*='ddlFinDtls']:selected").val();
                var $txtFinAmnt = $('#<%=grvFinDtls.ClientID %>').find('input:text[id$="txtFinAmnt"]').val();
                if (counts < 3) {
                    if (($txtFinAmnt == undefined) && ($ddlFinDtls == undefined)) {
                        alert("Please add at least one Financing Details");
                        return false;
                    }
                }
                //                if (!ConfirmAction('btnSubmit', 'Are you sure want to Save in draft ?', 'Are you sure want to Update ?')) {
                //                    return false;
                //                }
            });
        });


        //  function Validation() { 
        //  debugger;      
        //            var rows = <%= grvFinDtls.Rows.Count %>;
        //            var rows = $("[id*=grvFinDtls] tr").length;
        //            if (rows < 3) {
        //                debugger;
        //                alert("Please add at least one Financing Details");
        //                $('#ddlFin').focus();
        //                return false;
        //            }


        //            if (!ConfirmAction('btnSubmit', 'Are you sure want to Save in draft ?', 'Are you sure want to Update ?')) {
        //                return false;
        //            }        
        //        }

        //        
       

    </script>
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
                        Add Proposal</h1>
                    <ul class="breadcrumb">
                        <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                        <li><a>AMS</a></li><li><a>Create Project</a></li>
                    </ul>
                </div>
            </section>
            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-bd lobidrag">
                            <div class="panel-body">
                                <ul class="steps">
                                    <li class="past"><span>
                                        <h4>
                                            <a href="ProjectMasterAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                Step-1</a></h4>
                                    </span><i></i></li>
                                    <li class="past"><span>
                                        <h4>
                                            <a href="ProposalMasterAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                Step-2</a></h4>
                                    </span><i></i></li>
                                    <li class="past"><span>
                                        <h4>
                                            <a href="ProjectDetailsAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                Step-3</a></h4>
                                    </span><i></i></li>
                                    <li class="present"><span>
                                        <h4>
                                            <a href="FinancingDetailsAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                Step-4</a></h4>
                                    </span><i></i></li>
                                    <li class="future"><span>
                                        <h4>
                                            <a href="FinancialPerformanceAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                Step-5</a></h4>
                                    </span><i></i></li>
                                    <li class="future"><span>
                                        <h4>
                                            <a href="FinancialDocumentAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                Step-6</a></h4>
                                    </span><i></i></li>
                                </ul>
                                <div class="clearfix">
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">
                                        Financing Details
                                        <br />
                                        (Rs in crores) <span class="mandatory">*</span></label>
                                    <div class="col-sm-7">
                                        <span class="colon">:</span>
                                        <asp:UpdatePanel ID="UpdatePanelFin" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="grvFinDtls" runat="server" AutoGenerateColumns="False" ShowHeader="true"
                                                    border="0" CssClass="table table-bordered" ShowFooter="false" OnRowDataBound="grvFinDtls_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl#" HeaderStyle-Width="30px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="30px"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description" HeaderStyle-Width="300px">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlFinDtls" runat="server" Width="214px" TabIndex="8" class="form-control">
                                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:Label ID="lblFinDescription" runat="server" Text='<% #Eval("FinDescription") %>' />--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="300px"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtFinAmnt" runat="server" MaxLength="10" Width="140px" TabIndex="8"
                                                                    Text='<% #Eval("FinAmount") %>' class="form-control" Style="text-align: right" />
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers,Custom"
                                                                    ValidChars=".-" TargetControlID="txtFinAmnt">
                                                                </cc1:FilteredTextBoxExtender>
                                                                <%--<asp:Label ID="lblFinAmount" runat="server" Text='<% #Eval("FinAmount") %>' />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="% of project">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPercentage" runat="server" MaxLength="100" Width="100px" TabIndex="1"
                                                                    Text='<% #Eval("Percentage") %>' class="form-control" Style="text-align: right" />
                                                                <cc1:FilteredTextBoxExtender ID="FEPercentage" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                                                    TargetControlID="txtPercentage" InvalidChars="!<>%'*&" ValidChars=" .(),-">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="30px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnDeleteFinDtls" runat="server" ImageUrl="~/images/delete_btn.gif"
                                                                    ToolTip="Click To Delete !" OnClick="imgbtnDeleteFinDtls_Click" />
                                                                <asp:Button ID="BtnAddMoreFin" runat="server" Text="Addmore" TabIndex="8" CssClass="btn btn-success"
                                                                    OnClick="BtnAddMoreFin_Click" />
                                                                <%--<asp:HiddenField ID="hdnFilename" runat="server" Value='<%#Eval("FinDescription") %>' />--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="30px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <%--   <FooterTemplate>
                                                                                    
                                                                                    </FooterTemplate>--%>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">
                                        Financing Description</label>
                                    <div class="col-sm-7">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtFinDescription" runat="server" MaxLength="1000" TextMode="MultiLine"
                                            Rows="3" onkeypress="return TextCounter('txtFinDescription','lblDesc',1000)"
                                            ondrop="return false;" TabIndex="9" class="form-control" />
                                        <cc1:FilteredTextBoxExtender ID="FEFinDescription" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                            TargetControlID="txtFinDescription" FilterMode="ValidChars" ValidChars=" .(),:/-">
                                        </cc1:FilteredTextBoxExtender>
                                        Maximum <span class="text-red">
                                            <asp:Label ID="lblDesc" runat="server" Text="1000"></asp:Label>
                                        </span>&nbsp;characters <span class="mandatory">&nbsp;</span>
                                    </div>
                                </div>
                                <div id="trRemark" runat="server" class="row m-b-10">
                                    <div class="row">
                                        <label class="col-sm-2">
                                        </label>
                                        <div class="col-sm-4">
                                            <asp:Repeater ID="RptCMDRemark" runat="server">
                                                <HeaderTemplate>
                                                    <table class="table table-bordered table-condensed">
                                                        <tr>
                                                            <th colspan="2">
                                                                <b>CMD Comments</b>
                                                            </th>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            No. of Times:
                                                            <asp:Label ID="lblSubject" runat="server" Text='<%#Eval("NO_OF_REMARK") %>' Font-Bold="true" />
                                                            <br />
                                                            <asp:Label ID="lblCMDRemark" runat="server" Text='<%#Eval("CMDRemark") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Repeater ID="RptGMRemark" runat="server">
                                                <HeaderTemplate>
                                                    <table class="table table-bordered table-condensed">
                                                        <tr>
                                                            <th colspan="2">
                                                                <b>GM Comments</b>
                                                            </th>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            No. of Times:
                                                            <asp:Label ID="lblSubject" runat="server" Text='<%#Eval("NO_OF_REMARK") %>' Font-Bold="true" />
                                                            <br />
                                                            <asp:Label ID="lblCMDRemark" runat="server" Text='<%#Eval("CMDRemark") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                                <div class="row m-b-10" id="trRemarkEnt" runat="server">
                                    <label class="col-sm-2">
                                        Comment</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtRemark" runat="server" Rows="3" CssClass="form-control" TextMode="MultiLine"
                                            MaxLength="500"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FERemark" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                            ValidChars=".-,() " TargetControlID="txtRemark" InvalidChars="!<>%'">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:HiddenField ID="hdnRemarkID" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4 col-sm-offset-2">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Next" CssClass="btn btn-success"
                                            OnClick="btnSubmit_Click" TabIndex="11" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_Click"
                                            TabIndex="12" /></div>
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
