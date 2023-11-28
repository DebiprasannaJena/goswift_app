<%--'*******************************************************************************************************************
' File Name         : SSOLoginNSWS.aspx
' Description       : This page is used as an intermediate page between GOSWIFT and NSWS for Single Sign On (SSO). [ NSWS- Nationa Single Window System]
' Created by        : Sushant Jena
' Created On        : 01-Apr-2021
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SSOLoginNSWS.aspx.cs" Inherits="SSOLoginNSWS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <title></title>
    <script type="text/javascript" language="javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';
        function alertredirect(msg) {
            jAlert(msg, projname, function (r) {
                if (r) {
                    location.href = 'Default.aspx';
                    return true;
                }
                else {
                    return false;
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    </form>
</body>
</html>
