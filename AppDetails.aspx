<%--'*******************************************************************************************************************
' File Name         : ApplicationDetails.aspx
' Description       : Show the  details of Particular Applied Application
' Created by        : Prasun Kali
' Created On        : 19th September 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppDetails.aspx.cs" Inherits="AppDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="registration-div investors-bg">
            <div class="">
                <div id="exTab1">
                    <div class="row">
                        <!-- Form controls -->
                        <div class="col-sm-12">
                            <div class="panel panel-bd lobidrag">
                                <div class="dy-formview">
                                    <div class="dyformheader">
                                        <div class="header-details" runat="server" id="divHeader">
                                            <%-- <h2>Application Header</h2>
 <p>Government of Odisha</p>--%>
                                        </div>
                                    </div>
                                    <div class="dyformbody">
                                        <div id="frmContent" runat="server">
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12" align="center">
                                <%--    <asp:Button ID="btnBack" runat="server" Text="Back" 
                                                OnClick="btnBack_Click" class="btn btn-primary btn-sm" />--%>
                                <%--<asp:HyperLink ID="HyperLink1" NavigateUrl="~/Service/ServiceViewAndTakeAction.aspx" CssClass="btn btn-success" runat="server">Back</asp:HyperLink>--%>
                                <%--     <asp:LinkButton ID="LinkButton1" Text="Raise Query" runat="server" class="btn btn-danger btn-sm"
                data-toggle="modal" data-target="#customer1"></asp:LinkButton>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <!-- /.content -->
    </form>
</body>
</html>
