<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewEmpDetail.aspx.cs" ValidateRequest="false"
    EnableEventValidation="false" Inherits="Master_ViewEmpDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="../Include/UserName.ascx" TagName="User" TagPrefix="ucUser" %>
<%@ Register Src="../Include/Header.ascx" TagName="Header" TagPrefix="ucHeader" %>
<%@ Register Src="../Include/Footer.ascx" TagName="Footer" TagPrefix="ucFooter" %>
<%@ Register Src="~/Include/Leftmenupanel.ascx" TagName="LeftPannel" TagPrefix="ucLeftPannel" %>
<%@ Register Src="~/Include/Tab.ascx" TagName="Tab" TagPrefix="uc5" %>--%>
<%@ Register Src="../include/utils.ascx" TagName="utils" TagPrefix="ucutils" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Grievance Redressal System</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../js/jQuery.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../js/loadComponent.js"></script>
    <script src="../js/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../js/jQuery.alert.js" type="text/javascript"></script>
    <link href="../css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="../js/CSMValidation.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        pageHeader = "Inventory Report"
        strFirstLink = "ITIL"
        strLastLink = "Inventory Report"
        window.onload = function () {
            configTab();
            configButton();
            configTitleBar();
        }
    </script>
    <!-- Bootstrap 3.3.2 -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Font Awesome Icons -->
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="../css/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="../css/skin-yellow.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 9]>
        <script src="../js/html5shiv.js"></script>
        <script src="../js/respond.min.js"></script>
    <![endif]-->
    <script type="text/javascript" src="../js/jQuery_v1_10_2.js"></script>
    <script type="text/javascript" language="javascript">

        function checkValidation() {
            if (!blankFieldValidation('txtUserName', 'User Name'))
                return false;
            if (!blankFieldValidation('txtPassword', 'Password'))
                return false;
            if (!chkSingleQuote('txtPassword'))
                return false;
            return true;
        }
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            if ($('#hdnCheck').val() == "1") { $("#div10").slideDown(); $("#div11").slideUp(); }
            else { $("#div10").slideUp(); $("#div11").slideDown(); }
        });
    </script>
    <style type="text/css">
        .link {
            font-size: 12px;
            color: Blue;
            font-weight: bold;
        }

        .headingBar {
            position: absolute;
            padding: 6px 6px;
            font-size: 15px;
            font-weight: bold;
            color: #267DC1;
            margin: -46px 0px 10px -16px;
            border: solid 1px #A29F9F;
            border-bottom-color: #D9F0FC;
            background-color: #D9F0FC;
            display: inline-block;
        }

            .headingBar span {
                letter-spacing: 1px;
                padding: 1px 15px;
            }

        .reportArea {
            width: 46%;
            margin-right: 2%;
            margin-left: 0%;
            padding: 15px;
            margin-bottom: 25px;
            margin-top: 50px;
            background: #D9F0FC;
            border: solid 1px #A29F9F;
            float: left;
            min-height: 180px;
            -webkit-box-shadow: 0px 5px 5px -2px rgba(50, 50, 50, 0.50);
            -moz-box-shadow: 0px 5px 5px -2px rgba(50, 50, 50, 0.50);
            box-shadow: 0px 5px 5px -2px rgba(50, 50, 50, 0.50);
        }

            .reportArea table td a {
                color: #034C3F;
                text-decoration: none;
                line-height: 25px;
                font-size: 14px;
            }

                .reportArea table td a:hover {
                    color: #F00 !important;
                    text-decoration: none;
                }

        .style1 {
            width: 209px;
        }
    </style>
</head>
<body class="skin-yellow">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div class="wrapper">
            <aside class="main-sidebar">
                <section class="sidebar">
                </section>
            </aside>
            <div class="content-wrapper">
                <div class="content">
                    <div class="row">
                        <!-- Form controls -->
                        <div class="col-sm-12">
                            <div class="panel panel-bd lobidisable">
                                <div class="panel-body">
                                    <div class="search-sec">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="Label1" runat="server" CssClass="" Font-Bold="true" Text="Query Analyzer" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:TextBox CssClass="input" ID="txtQuery" TextMode="MultiLine" runat="server" Height="260px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:Button ID="btnExecute" runat="server" Text="Execute" ToolTip="Execute" CssClass="btn btn-success"
                                                        OnClick="btnExecute_Click" />&nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear"
                                                            ToolTip="CLear" OnClick="btnClear_Click" CssClass="btn btn-danger" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="Small"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="table-responsive">
                                                        <div style="height: 500px; overflow: auto;">
                                                            <asp:GridView ID="grdDetails" CssClass="table table-hover table-striped" AutoGenerateColumns="true"
                                                                runat="server" Width="100%" Visible="false">
                                                                <PagerStyle CssClass="paging noPrint" />
                                                                <FooterStyle CssClass="footer" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
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
