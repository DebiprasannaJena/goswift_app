<%--'*******************************************************************************************************************
' File Name         : ServicesCheck.aspx
' Description       : Details of Promoter
' Created by        : Radhika Rani Patri
' Created On        : 03 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServicesCheck.aspx.cs" Inherits="ServicesCheck" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <style>
        
    </style>
</head>
<body>
    <form id="form2" runat="server">
    <uc2:header ID="header" runat="server" />
    <div class="pagenavigator">
        <h2>
            <a class="" href="javascript:history.back()"><i class="fa fa-chevron-circle-left"></i>
            </a>Enclosers List</h2>
    </div>
    <div class="registration-div">
        <div class="container">
            <div class="form-sec">
                <h2>
                    Service Wise Form</h2>
                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-6">
                            <label for="Iname">Service</label>
                <asp:DropDownList ID="ddlService" runat="server" CssClass="form-control" 
                               >
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                        </div>
                    </div>
                </div>
               
             

                   <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">                                       
                                    </div>
                                    <div class="col-sm-4" align="center">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass=" btn btn-success" 
                                            Width="80" onclick="btnSubmit_Click"  />
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                            </div>
              
        </div>
    </div>
    </div> 
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
