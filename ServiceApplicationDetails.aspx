<%--'*******************************************************************************************************************
' File Name         : ServiceApplicationDetails.aspx
' Description       : Show the list of all Details of particular application.
' Created by        : Praun Kali
' Created On        : 13th September 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServiceApplicationDetails.aspx.cs" Inherits="ServiceApplicationDetails" %>


<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <uc1:doctype ID="doctype" runat="server" />
         <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />

      <script>

          $(document).ready(function () {

              $('.menuservices').addClass('active');

          });
   
    </script>
</head>
<body>
    <form id="form2" runat="server">
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="registration-div investors-bg">
            <div class="">
                <div id="exTab1">
                    <div class="investrs-tab">
                        <uc4:investoemenu ID="ineste" runat="server" />
                          
                    </div>
                    <div class="tab-content clearfix">
                        <div class="tab-pane active" id="1a">
                            <div class="form-sec">
                                <div class="form-header">
                                    
                                    <h2>
                                        Application Status Details</h2>
                                </div>
                                <div class="form-body minheight350">
                                    <div class="form-group">
                                        <div class="table-responsive ">
                                            <asp:GridView ID="grvDetails" runat="server" CssClass="table table-bordered bg-white" EmptyDataText="No Data Found">
                                                <AlternatingRowStyle />
                                             
                                                <PagerStyle CssClass="pagination-grid no-print" />

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
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
