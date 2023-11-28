<%@ Page Language="C#" AutoEventWireup="true" Inherits="SessionRedirect" CodeBehind="SessionRedirect.aspx.cs" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Console::Session Expired</title>

    <script type="text/javascript">
        if (top.location != self.location) {
            top.location = self.location.href
        }
    </script>

    <link type="text/css" rel="stylesheet" href="Console/style/default.css" />
</head>
<body>
    <form id="form1" runat="server">
    </br></br></br></br></br></br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label1" runat="server" Style="color: #800000; font-size: medium; font-weight: 700"
        Text="Label" Visible="False"></asp:Label>
    </br></br>
    <table width="50%" height="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="middle">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="21" bgcolor="#006699" class="idxBodyHdr">
                            <font color="#FFFFFF">&nbsp;&nbsp;<span class="blBoldText">Session Expired !! </span>
                            </font>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#006699">
                            <table width="100%" border="0" cellspacing="1" cellpadding="0">
                                <tr>
                                    <td bgcolor="#F4F4F4">
                                        <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td height="60" bgcolor="#F4F4F4">
                                                    <div align="center">
                                                        <img src="Console/images/logored.jpg"  height="120px"></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#F4F4F4">
                                                    <table width="70%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td height="1" bgcolor="#006699">
                                                                <img src="Console/images/spacer.gif" width="1" height="1">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 70; width: 54px; padding: 5px 7px">
                                                                <div align="center">
                                                                    <img src="Console/images/sessionExpired.jpg" style="width: 56px; border-radius: 5px;
                                                                        border: solid 1px #006699; height: 44px"></div>
                                                            </td>
                                                            <td class="bodytext">
                                                                &nbsp;
                                                            </td>
                                                            <td class="btn">
                                                                Sorry !! Session has expired
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div align="right">
                                                        <span class="btn"><a href="Default.aspx" class="sidenavtext" style="text-decoration: none">
                                                            login again</a> </span>&nbsp;<a href="Default.aspx"><img src="Console/images/home_arrow.gif"
                                                                width="17" height="17" border="0" align="absmiddle"></a></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="17">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
--%>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            background-color: #f7f7f7;
        }
        .navbar-header img
        {
            margin-right: 10px;
            height: 74px;
            display: inline-block;
        }
        .navbar
        {
            height: 75px;
        }
        .error-bg
        {
            margin-top: 100px;
            background-color: #ffffff;
            box-shadow: 0px 0px 3px #b1acac;
            border-radius: 4px;
            padding: 20px;
            margin: 8% auto;
            width: 490px;
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
            width: 150px;
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
    <div class="container">
        <header>
  <div class="top-header">
  <div class="">
    <div class="navbar-header">
      
      <a class="navbar-brand" href="Default.aspx"><img src="images/Logo2.png"  alt="Odisha Govt."/><img src="images/Logo.png"  alt="GO Swift"></a> </div>
  
    <div class="tollfreesec">
   <p> Toll Free Helpline - <span>1800 345 7157 </span></p>
   <p class="timespan"><small>( Timing 10.00 A.M to 06.00 PM on working days)</small></p>
    </div>
    </div>
 
    <div class="clearfix"></div>
  </div>

</header>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <!-- PAGE CONTENT BEGINS -->
                <div class="error-bg">
                    <img src="../images/sessionExpired.gif" />
                    <p class="sessionTxt">
                        <strong>Your session expired.</strong><br />
                        Please click login to continue.
                        <%--<a href="javascript:void();" title="Administrator">Administrator</a>.--%>
                        <%--An unexpected error occurred on our website. <br />The website administrator has been notified.--%>
                    </p>
                    <a class="btn btn-primary" href="Default.aspx">Login <i class="ace-icon fa fa-arrow-right">
                    </i></a>
                    <div class="clearfix">
                    </div>
                </div>
                <!-- PAGE CONTENT ENDS -->
            </div>
            <!-- /.col -->
        </div>
    </div>
    </form>
</body>
</html>
