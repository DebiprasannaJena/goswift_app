<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinancialPerformanceAdd.aspx.cs"
    Inherits="SingleWindow_FinancialPerformanceAdd" %>

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
        pageHeader = "Add Financial Performance"
        indicate = "yes"
        window.onload = function () {
            //            configTab();
            //            configButton();
            configTitleBar();
        }
        $(document).ready(function () {
            //            $("#<%=btnSubmit.ClientID %>").click(function () {

            //                if (!ConfirmAction('btnSubmit', 'Are you sure want to Submit ?', 'Are you sure want to Update ?')) {
            //                    return false;
            //                }
            //            });
        });

    </script>
    <style>
        a
        {
            text-decoration: none !important;
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
    <div class="wrapper">
        <form id="form1" runat="server" defaultbutton="btnSubmit" defaultfocus="ddlProject">
        <!--Header-->
        <script language="javascript" type="text/javascript">

            $(document).ready(function () {
                $("#<%=btnSubmit.ClientID %>").click(function () {
                    if ($("#GrdFinanace tr").length <= 0) {
                        alert("Please Enter Financial Details");
                        $("#<%=txtTurnover1.ClientID%>").focus();
                        return false;
                    }
                    //                if (!ConfirmAction('btnSubmit', 'Are you sure want to Save in draft ?', 'Are you sure want to Update ?')) {
                    //                    return false;
                    //                }
                });

                $("#btnAddMore").click(function (e) {

                    if (!blankFieldValidation('txtCompany', ' Company')) { return false; }
                    if (!WhiteSpaceValidation1st('txtCompany', ' Company ')) { return false; }

                    if (!blankFieldValidation('txtFinYear', ' Particulars')) { return false; }
                    if (!blankFieldValidation('txtFinYear1', ' Particulars')) { return false; }
                    if (!blankFieldValidation('txtFinYear2', ' Particulars')) { return false; }

                    if (!blankFieldValidation('txtTurnover1', 'Company Turnover')) { return false; }
                    //                if (!DecimalNumber('txtTurnover1', 'Company Turnover')) { return false; }

                    if (!blankFieldValidation('txtTurnover2', ' Company Turnover')) { return false; }
                    //                if (!DecimalNumber('txtTurnover2', 'Company Turnover')) { return false; }

                    if (!blankFieldValidation('txtTurnover3', ' Company Turnover')) { return false; }
                    //                if (!DecimalNumber('txtTurnover3', 'Company Turnover')) { return false; }

                    if (!blankFieldValidation('txtProTax1', 'Company Profit after Tax')) { return false; }
                    //                if (!DecimalNumber('txtProTax1', 'Company Profit after Tax')) { return false; }

                    if (!blankFieldValidation('txtProTax2', 'Company Profit after Tax')) { return false; }
                    //                if (!DecimalNumber('txtProTax2', 'Company Profit after Tax')) { return false; }

                    if (!blankFieldValidation('txtProTax3', 'Company Profit after Tax')) { return false; }
                    //                if (!DecimalNumber('txtProTax3', 'Company Profit after Tax')) { return false; }


                    if (!blankFieldValidation('txtNet1', 'Company Net worth')) { return false; }
                    //                if (!DecimalNumber('txtNet1', 'Company Net worth')) { return false; }

                    if (!blankFieldValidation('txtNet2', 'Company Net worth')) { return false; }
                    //                if (!DecimalNumber('txtNet2', 'Company Net worth')) { return false; }

                    if (!blankFieldValidation('txtNet3', 'Company Net worth')) { return false; }
                    //                if (!DecimalNumber('txtNet3', 'Company Net worth')) { return false; }
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
                                                Step-2</a>
                                        </h4>
                                    </span><i></i></li>
                                    <li class="past"><span>
                                        <h4>
                                            <a href="ProjectDetailsAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                Step-3</a></h4>
                                    </span><i></i></li>
                                    <li class="past"><span>
                                        <h4>
                                            <a href="FinancingDetailsAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                Step-4</a></h4>
                                    </span><i></i></li>
                                    <li class="present"><span>
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
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h4>
                                            Financial Performance of Company(Rs in Crores)</h4>
                                        <table class="table table-bordered">
                                            <tr>
                                                <th width="160px" style="vertical-align: top">
                                                    Company Name<span class="mandatory">*</span>
                                                </th>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control" TabIndex="2"
                                                        MaxLength="100"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,custom"
                                                        TargetControlID="txtCompany" InvalidChars="!<>%'*&" ValidChars=" .(),/">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="vertical-align: top">
                                                    Particulars
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="txtFinYear" runat="server" CssClass="form-control" TabIndex="3"
                                                        MaxLength="12" Font-Bold="true"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FEFinyear" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,custom"
                                                        TargetControlID="txtFinYear" ValidChars=":-">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFinYear1" runat="server" TabIndex="4" CssClass="form-control"
                                                        MaxLength="12" Font-Bold="true"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FEFinyear1" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,custom"
                                                        TargetControlID="txtFinyear1" ValidChars=":-">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFinYear2" runat="server" CssClass="form-control" TabIndex="5"
                                                        MaxLength="12" Font-Bold="true"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FEFinyear2" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,custom"
                                                        TargetControlID="txtFinYear2" ValidChars=":-">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="vertical-align: top">
                                                    TurnOver<span class="mandatory">*</span>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="txtTurnover1" runat="server" CssClass="form-control" TabIndex="6"
                                                        Style="text-align: right"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers,custom"
                                                        ValidChars=".-" TargetControlID="txtTurnover1">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTurnover2" runat="server" CssClass="form-control" TabIndex="7"
                                                        Style="text-align: right"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Numbers,custom"
                                                        ValidChars=".-" TargetControlID="txtTurnover2">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTurnover3" runat="server" CssClass="form-control" TabIndex="8"
                                                        Style="text-align: right"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers,custom"
                                                        ValidChars=".-" TargetControlID="txtTurnover3">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="vertical-align: top">
                                                    Profit after Tax<span class="mandatory">*</span>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="txtProTax1" runat="server" CssClass="form-control" TabIndex="9"
                                                        Style="text-align: right"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers,custom"
                                                        ValidChars=".-" TargetControlID="txtProTax1">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtProTax2" runat="server" CssClass="form-control" TabIndex="10"
                                                        Style="text-align: right"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers,custom"
                                                        ValidChars=".-" TargetControlID="txtProTax2">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtProTax3" runat="server" CssClass="form-control" TabIndex="11"
                                                        Style="text-align: right"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterType="Numbers,custom"
                                                        ValidChars=".-" TargetControlID="txtProTax3">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="vertical-align: top">
                                                    Net worth<span class="mandatory">*</span>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="txtNet1" runat="server" CssClass="form-control" TabIndex="12" Style="text-align: right"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" FilterType="Numbers,custom"
                                                        ValidChars=".-" TargetControlID="txtNet1">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNet2" runat="server" CssClass="form-control" TabIndex="13" Style="text-align: right"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" FilterType="Numbers,custom"
                                                        ValidChars=".-" TargetControlID="txtNet2">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNet3" runat="server" CssClass="form-control" TabIndex="14" Style="text-align: right"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" FilterType="Numbers,custom"
                                                        ValidChars=".-" TargetControlID="txtNet3">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="vertical-align: top">
                                                    Remark
                                                </th>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtUsrRemark" CssClass="form-control" runat="Server" Rows="3" TextMode="MultiLine"
                                                        MaxLength="500" TabIndex="15"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FEtxtUsrRemark" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                                        ValidChars=".-,() " TargetControlID="txtUsrRemark" InvalidChars="!<>%'">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="hdnFY1" Value="0" runat="server" />
                                        <asp:HiddenField ID="hdnFY2" Value="0" runat="server" />
                                        <asp:HiddenField ID="hdnFY3" Value="0" runat="server" />
                                        <asp:HiddenField ID="hdnProjNm" Value="0" runat="server" />
                                        <asp:HiddenField ID="hdnUid" Value="0" runat="server" />
                                    </div>
                                    <div class="col-sm-12 m-b-10">
                                        <asp:Button ID="btnAddMore" runat="server" CssClass="btn btn-success" Text="Add More"
                                            TabIndex="16" OnClick="btnAddMore_Click" />
                                    </div>
                                    <div class="col-sm-12 m-b-10">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GrdFinanace" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False"
                                                OnRowDeleting="GrdFinanace_RowDeleting" OnRowUpdating="GrdFinanace_RowUpdating"
                                                DataKeyNames="FinanceId,ComapnyName,KeyId" OnDataBound="OnDataBound" OnRowDataBound="GrdFinanace_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl#">
                                                        <ItemTemplate>
                                                            <span>
                                                                <%#Container.DataItemIndex + 1%>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ComapnyName" HeaderText="Company Name">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Particulars" HeaderText="Particulars">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FinYear1" HeaderText="Finance Year">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FinYear2" HeaderText="Finance Year">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FinYear3" HeaderText="Finance Year">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Remark" HeaderText="Remark">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Financial Document" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnFinDoc" runat="server" Value='<%# Eval("FinDoc") %>' />
                                                            <asp:HyperLink ID="hlDoc" runat="server" Target="_blank" ImageUrl="~/images/pdf_icon_32.png"></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnEdit" CssClass="btn btn-sm btn-success" runat="server"
                                                                CommandName="Update" OnClientClick="return confirm(' Are you sure want to Edit the selected Item!')">
                                                                <i class="fa fa-pencil-square-o"></i>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnDelete" CssClass="btn btn-sm btn-danger" runat="server"
                                                                CommandName="Delete" OnClientClick="return confirm('Do you want to delete this record?');">
                                                                <i class="fa fa-trash-o"></i> 
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="90px" CssClass="noPrint" />
                                                        <ItemStyle CssClass="noPrint" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="paging NOPRINT" />
                                                <PagerSettings Mode="NumericFirstLast" NextPageText="Next" FirstPageText="First"
                                                    LastPageText="Last" PreviousPageText="Prev" Position="Bottom" />
                                            </asp:GridView>
                                            <asp:Label ID="lblMessage" runat="server" Text="No Record(s) Found!!!" Visible="false"
                                                CssClass="lblMessage" />
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
                                        <div class="row m-b-10" id="divRemark" runat="server">
                                            <label class="col-sm-2">
                                                Comment</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:TextBox ID="txtRemark" runat="server" Rows="3" CssClass="form-control" TextMode="MultiLine"
                                                    MaxLength="500"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FERemark" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                                    ValidChars=".-,() " TargetControlID="txtRemark" InvalidChars="!<>%'">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-9 col-sm-offset-2">
                                                <asp:HiddenField ID="hdnRemarkID" runat="server" />
                                                <asp:Button ID="btnSubmit" runat="server" Text="Finish" CssClass="btn btn-success"
                                                    TabIndex="16" OnClick="btnSubmit_Click" />
                                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_Click"
                                                    TabIndex="17" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
        <!--Footer-->
        <ucfooter:footer ID="footer1" runat="server" />
        <!-- Modal -->
        </form>
    </div>
</body>
</html>
