<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomError.aspx.cs" Inherits="Dashboard_CustomError" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            background-color: #f7f7f7;
        }
        .navbar
        {
            height: 75px;
        }
        .navbar-header img
        {
            margin-right: 10px;
            height: 74px;
            display: inline-block;
        }
        .error-bg
        {
            margin-top: 90px;
            background-color: #ffffff;
            border-radius: 4px;
            padding: 20px;
            margin-bottom: 100px;
        }
        .error-bg p
        {
            line-height: 24px;
            margin-top: 2px;
            font-size: 14px;
            margin-bottom: 20px;
        }
        .error-bg img
        {
            float: left;
            margin-right: 30px;
            width: 250px;
        }
        p.sessionTxt strong
        {
            font-size: 22px;
            color: #e30e15;
            line-height: 50px;
        }
        .error-bg p a
        {
            color: #EC4444;
            font-weight: bold;
        }
        .error-bg p a:hover
        {
            text-decoration: none;
            color: #3B64DE;
        }
        .top-header
        {
            display: table;
            width: 100%;
            box-shadow: none;
            margin-bottom: 0px;
        }
        .navbar-brand
        {
            padding: 6px 15px 0 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container wrapper">
        <div class="top-header">
            <div class="navbar-header">
                <a class="navbar-brand" href="Default.aspx">
                    <img src="images/Logo2.png" alt="Odisha Govt." /><img src="images/Logo.png" alt="GO Swift"></a>
            </div>
            <div class="tollfreesec">
                <p>
                    Toll Free Helpline - <span>1800 345 7157</span>
                </p>
                <p class="timespan">
                    <small>( Timing 10.00 A.M to 06.00 PM on working days)</small></p>
            </div>
        </div>
        <div class="clearfix">
        </div>
        <div class="container">
            <div class="col-xs-12">
                <!-- PAGE CONTENT BEGINS -->
                <div class="col-md-8 col-md-offset-2 error-bg">
                    <img src="images/Error-Generic.jpg" />
                    <p class="sessionTxt">
                        <strong>An Error Has Occurred</strong><br />
                        We apologize,an error occurred and your request could not be completed.
                        <br />
                        This error has been logged. If you have additional information regarding what may
                        have caused this error, please contact our <a href="javascript:void();" title="Administrator">
                            Administrator</a>.
                        <%--An unexpected error occurred on our website. <br />The website administrator has been notified.--%>
                    </p>
                    <a class="btn btn-primary" href="Default.aspx">Login <i class="ace-icon fa fa-arrow-right">
                    </i></a>
                    <div class="clearfix">
                    </div>
                </div>
                <!-- PAGE CONTENT ENDS -->
            </div>
        </div>
    </div>
    </form>
</body>
</html>
