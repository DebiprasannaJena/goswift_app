<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CmdApproval.aspx.cs" Inherits="SingleWindow_CmdApproval" %>

<%@ Register Src="~/Include/IncludeScript.ascx" TagName="IncludeScript" TagPrefix="ucIncludeScript" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Include/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/includes/Leftmenupanel.ascx" TagName="leftMenu" TagPrefix="ucLeftMenu" %>
<%@ Register Src="~/include/AMSfooter.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head runat="server">
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
        pageHeader = "CMD Approval"
        indicate = "yes"
        window.onload = function () {
            //            configTab();
            //            configButton();
            configTitleBar();
        }
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
			width:86%;
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
			color:#000
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
		.steps .present a {color: #FFF;}
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
<body  class="hold-transition sidebar-mini">
 <div class="wrapper">
    <form id="form1" runat="server">
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
                                            <li class="past"><span>
                                                <h4>
                                                    <a href="FinancialPerformanceAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                        Step-5</a></h4>
                                            </span><i></i></li>
                                            <li class="past"><span>
                                                <h4>
                                                    <a href="FinancialDocumentAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                        Step-6</a></h4>
                                            </span><i></i></li>
                                            <li class="present"><span>
                                                <h4>
                                                    <a href="CmdApproval.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                        Step-7</a></h4>
                                            </span><i></i></li>
                                        </ul>
                                        <div class="clearfix"></div>
                                         <div class="row">
                                        <div class="col-sm-12">
                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Repeater ID="RptrComments" runat="server">
                                                            <HeaderTemplate>
                                                                <h4>
                                                                    SLFC Memebrs Feedback Details</h4>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="cmd-sec">
                                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("VCH_FULLNAME") %>' Font-Bold="true" />
                                                                    <asp:Label ID="lblDeptName" runat="server" Text='<%# "(" + Eval("DEPTNAME") + ") " %>'></asp:Label>
                                                                    <br />
                                                                    <sup><i class="fa fa-quote-left"></i></sup>
                                                                    <asp:Label ID="lblComment" runat="server" Text='<%#Eval("VCHCOMMENT") %>' />...
                                                                    <sup><i class="fa fa-quote-right"></i></sup>
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                                <tr style='<%=IsVisible ? "": "display: none" %>'>
                                                    <td>
                                                        <asp:Repeater ID="rptClarification" runat="server">
                                                            <HeaderTemplate>
                                                                <h4>
                                                                    SLFC Memebrs Clarification Details</h4>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="cmd-sec">
                                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("vchFullName") %>' Font-Bold="true" />
                                                                    <i class="fa fa-comment text-blue"></i>
                                                                    <br />
                                                                    <sup><i class="fa fa-quote-left"></i></sup>
                                                                    <asp:Label ID="lblComment" runat="server" Text='<%#Eval("vchClarificationSLFC") %>' />...
                                                                    <sup><i class="fa fa-quote-right"></i></sup>
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                            </table>
                                             <asp:HiddenField ID="hdnProjNm" runat="server" />
                                            <asp:HiddenField ID="hdnUid" runat="server" />
                                            <asp:Label ID="lblMessage" runat="server" Text="No Feedback(s) Found from SLFC Members!!!"
                                                Visible="false" CssClass="lblMessage" />
                                        </div>

                            </div>

             <div class="row" id="divRemark" runat="server">

<div class="col-sm-6">
  <asp:Repeater ID="RptCMDRemark" runat="server">
                                                                <HeaderTemplate>
                                                                    <table class="table table-bordered">
                                                                        <tr>
                                                                            <th colspan="2">
                                                                            <b >CMD Comments</b>
                                                                               
                                                                            </th>
                                                                        </tr>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr >
                                                                        <td>
                                                                          
                                                                                        No. of Times:
                                                                                        <asp:Label ID="lblSubject" runat="server" Text='<%#Eval("NO_OF_REMARK") %>' Font-Bold="true" />
                                                                                     <br /><asp:Label ID="lblCMDRemark" runat="server" Text='<%#Eval("CMDRemark") %>' />
                                                                        </td>
                                                                    </tr>
                                                                   
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
</div>
<div class="col-sm-6">
  <asp:Repeater ID="RptGMRemark" runat="server">
                                                                <HeaderTemplate>
                                                                    <table class="table table-bordered">
                                                                        <tr>
                                                                            <th colspan="2">
                                                                               
                                                                                <b >GM Comments</b>
                                                                                 
                                                                            </th>
                                                                        </tr>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr >
                                                                        <td>
                                                                           
                                                                                        No. of Times:
                                                                                        <asp:Label ID="lblSubject" runat="server" Text='<%#Eval("NO_OF_REMARK") %>' Font-Bold="true" />
                                                                                  <br /> <asp:Label ID="lblCMDRemark" runat="server" Text='<%#Eval("CMDRemark") %>' />
                                                                        </td>
                                                                    </tr>
                                                                   
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
   <asp:HiddenField ID="HdnCmdRemark" runat="server"></asp:HiddenField>
</div>
</div>


                    
                                   <div class="row">
                                    <div class="col-sm-12">
                                       <asp:Button ID="btnSubmit" runat="server" Text="Accept" CssClass="btn btn-success"
                                                            TabIndex="16" OnClientClick="return Validation();" OnClick="btnSubmit_Click" />
                                                        <asp:Button ID="btnReturn" runat="server" Text="Return" CssClass="btn btn-danger"
                                                            OnClick="btnReturn_Click" TabIndex="17" />
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
</html>
