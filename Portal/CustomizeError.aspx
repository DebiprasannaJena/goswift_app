<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomizeError.aspx.cs" Inherits="CustomizeError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Custom Error</title>
    <!--<link href="eSET_styles/Login.css" type="text/css" rel="stylesheet" />-->
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
<!-- 
body
{
	margin: 0px;
	background-image: url(images/SessioLoginBG.jpg);
	background-repeat: repeat-x;
	background-position: left top;
}
#LoginArea {
	height: 205px;
	width: 615px;
	margin-top: 100px;
	margin-right: auto;
	margin-bottom: 100px;
	margin-left: auto;
	background-image: url(images/SessionLoginAreaBG.jpg);
	background-repeat: no-repeat;
	background-position: left top;
	padding-top: 75px;
	padding-right: 10px;
	padding-bottom: 10px;
	padding-left: 10px;
	font-family: Arial, Helvetica, sans-serif;
	font-size: 11px;
	font-weight: normal;
	color: #292828;
	text-decoration: none;
}
.expire
{
	font-family: Arial, Helvetica, sans-serif;
	font-size: 12px;
	font-weight: normal;
	color: #6f7752;
	text-decoration: none;
	line-height: 20px;
}
.expire strong {
	font-size: 13px;
	font-weight: bold;
	color: #6f7752;
	text-decoration: none;}
	
.TxtBold
{
	font-family: Arial, Helvetica, sans-serif;
	font-size: 18px;
	font-weight: bold;
	color: #515a32;
	text-decoration: none;
	line-height: 20px;
}
-->
</style>
</head>
<body>
    <div id="LoginArea">
        <table width="550" border="0" cellpadding="2" cellspacing="0" align="center">
            <tr>
                <td rowspan="6" valign="top">
                    <img src="images/SessionIcon.jpg" alt="Customized Errort" width="132" height="135" />
                </td>
                <td rowspan="6" valign="top">
                    &nbsp;
                </td>
                <td colspan="2" valign="top" class="TxtBold">
                    Custom Error
                </td>
            </tr>
            <tr>
                <td height="15" colspan="2" valign="top">
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top" class="expire">
                    <strong>This page can't found Due to some Application Error.</strong>
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top" class="expire">
                    Please Try to <a href="Login.aspx" style="text-decoration: none; color: #FF0000;">
                        <b>Login Again</b></a>
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top" class="expire">
                    If this type of Error Occue Again, then contact to Authorized Admin.
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top" class="expire">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
