<%--'*******************************************************************************************************************
' File Name         : ViewDelayReason.aspx
' Description       : View details of Delay Reason
' Created by        : Gouri Shankar Chhotray
' Created On        : 15 Dec 2017
' Modification History:
' Procedure used    : USP_INCT_EC_DELAY_VIEW
' Table Used        : T_INCT_EC_DELAY_REASON

'   <CR no.>                          <Date>                <Modified by>        <Modification Summary>                   <Instructed By>        

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDelayReasonDetails.aspx.cs"
    Inherits="Portal_Incentive_ViewDelayReasonDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SWP(Single Window Portal)</title>
    <link href="../../PortalCSS/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/flash.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/pe-icon-7-stroke.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery.timepicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.main-sidebar').hide();
        });
    </script>
    <style type="text/css">
        .header-sec
        {
            padding: 6px 10px;
            background: #ffffff;
            border-bottom: 2px solid #ed3237;
        }
        .header-sec img
        {
            height: 50px;
            border-right: 1px solid #dedede;
            padding-right: 6px;
        }
        .header-sec img:last-child
        {
            border-right: 0px solid #000;
        }
        .content-wrapper
        {
            padding: 15px;
        }
        .content-wrapper h4
        {
            margin: 0px;
            padding: 10px;
            background: #f1f1f1;
        }
        .table > tbody > tr > th, .table > thead > tr > th
        {
            background: #f5f5f5;
            color: #000;
        }
        .panel
        {
            box-shadow: 0px 0px 5px #c7c7c7;
        }
        .panel-body
        {
            padding: 15px;
        }
        .content-wrapper h3
        {
            font-size: 18px;
        }
        .colon
        {
            float: left;
            margin-left: -10px;
            color: #000;
            margin-top: 0px;
            font-weight: 500;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="header-sec">
        <span class="logo-lg">
            <img src="../images/portallogo.png" alt="Logo">
            <img src="../images/logo.png" alt="Logo">
        </span>
    </div>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="panel">
            <section class="content-header">        
        <div class="header-title">
            <h4 class="text-center">
             Application Details for Condonation of Delay in Implementation</h4> 
        </div>
        </section>
            <div class="panel-body">
                <div class="search-sec">
                    <div class="form-group">
                        <div class="row">
                            <label for="Iname" class="col-sm-3">
                                Industry Code
                            </label>
                            <div class="col-sm-4">
                                <span class="colon">:</span>
                                <asp:Label ID="Lbl_Industry_Code" runat="server" CssClass="form-control-static"></asp:Label>
                            </div>
                            <label for="Iname" class="col-sm-2 ">
                                Status
                            </label>
                            <div class="col-sm-3">
                                <span class="colon">:</span>
                                <asp:Label ID="Lbl_Status" runat="server" Font-Bold="true" CssClass="form-control-static"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <label for="Iname" class="col-sm-3 ">
                                Name of Enterprise/Industrial Unit
                            </label>
                            <div class="col-sm-4">
                                <span class="colon">:</span>
                                <asp:Label ID="Lbl_Enterprise_Name" runat="server" CssClass="form-control-static"></asp:Label>
                            </div>
                            <label for="Iname" class="col-sm-2 ">
                                Approval Date
                            </label>
                            <div class="col-sm-3">
                                <span class="colon">:</span>
                                <asp:Label ID="Lbl_Approval_Date" runat="server" CssClass="form-control-static"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <label for="Iname" class="col-sm-3">
                                Unit Category
                            </label>
                            <div class="col-sm-4">
                                <span class="colon">:</span>
                                <asp:Label ID="Lbl_Unit_Cat" runat="server" CssClass="form-control-static"></asp:Label>
                            </div>
                            <label for="Iname" class="col-sm-2">
                                Time Period Allowed
                            </label>
                            <div class="col-sm-3">
                                <span class="colon">:</span>
                                <asp:Label ID="Lbl_Time_Allowed" runat="server" CssClass="form-control-static"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <label for="Iname" class="col-sm-3">
                                Applied On
                            </label>
                            <div class="col-sm-4">
                                <span class="colon">:</span>
                                <asp:Label ID="Lbl_Created_On" runat="server" CssClass="form-control-static"></asp:Label>
                            </div>
                            <label for="Iname" class="col-sm-2">
                                EC Letter
                            </label>
                            <div class="col-sm-3">
                                <span class="colon">:</span>
                                <asp:Label ID="Lbl_EC_Letter" runat="server" CssClass="form-control-static"></asp:Label>
                                <asp:HyperLink ID="Hy_EC_Letter" runat="server" class="btn btn-info" ToolTip="Click Here to View EC Letter !!"
                                    Target="_blank"><i class="fa fa-download"></i></asp:HyperLink>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <label for="Iname" class="col-sm-3">
                                Unit Type
                            </label>
                            <div class="col-sm-4">
                                <span class="colon">:</span>
                                <asp:Label ID="Lbl_Unit_Type" runat="server" CssClass="form-control-static"></asp:Label>
                            </div>
                            <label for="Iname" class="col-sm-2">
                                Remarks
                            </label>
                            <div class="col-sm-3">
                                <span class="colon">:</span>
                                <asp:Label ID="Lbl_Remark" runat="server" CssClass="form-control-static"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <label for="Iname" class="col-sm-3">
                                Date of FFCI
                            </label>
                            <div class="col-sm-4">
                                <span class="colon">:</span>
                                <asp:Label ID="Lbl_FFCI_Date" runat="server" CssClass="form-control-static"></asp:Label>
                            </div>
                            <label for="Iname" class="col-sm-2">
                            </label>
                            <div class="col-sm-3">
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <label for="Iname" class="col-sm-3">
                                Date of Production Commencement
                            </label>
                            <div class="col-sm-4">
                                <span class="colon">:</span>
                                <asp:Label ID="Lbl_Prod_Comm" runat="server" CssClass="form-control-static"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <label for="Iname" class="col-sm-3">
                                Reason(s) for Delay in Implementation
                            </label>
                            <div class="col-sm-9">
                                <span class="colon">:</span>
                                <asp:Label ID="Lbl_Delay_Reason" runat="server" CssClass="form-control-static"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-12">
                                <h3>
                                    Supporting Document List</h3>
                                <asp:GridView ID="Grd_Application" runat="server" AutoGenerateColumns="false" class="table table-bordered table-hover"
                                    PagerStyle-CssClass="pagination-grid" DataKeyNames="intDelayId">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SLNo.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="vchDocDesc" HeaderText="Document Description" />
                                        <asp:TemplateField HeaderText="View Document">
                                            <ItemTemplate>
                                                <a href='<%# "../../incentives/Files/InctEcDelayDoc/"+Eval("vchFileName") %>' target="_blank"
                                                    class="btn btn-success btn-sm" title="Click here to view document !!"><i class='fa fa-eye'
                                                        aria-hidden='true'></i></a>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Supporting Document(s) Found !!
                                    </EmptyDataTemplate>
                                    <EmptyDataRowStyle ForeColor="Red" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
