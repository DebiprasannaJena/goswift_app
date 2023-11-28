<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LandAllocated.aspx.cs" Inherits="LandAllocated" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <title></title>
      <script>

          $(document).ready(function () {

              $('.menuservices').addClass('active');

          });
   
    </script>
    <style>
        .guidelines{display:table;width:100%;min-height:200px;text-align:center;background: #eee;margin-bottom: 10px;}
        .guidelines p{display:table-cell;vertical-align:middle;font-size: 18px;letter-spacing: 1px;}
        .guidelinesdetails{}
        .guidelinesdetails h4{margin-top:0px;font-weight:600;}
        </style>
</head>
<body>
    <form id="form2" runat="server">
    <uc2:header ID="header" runat="server" />
    <div class="registration-div investors-bg">
        <div class="container">
            <div id="exTab1">
                <div class="investrs-tab">
                    <uc4:investoemenu ID="ineste" runat="server" />
                </div>
                <div class="form-sec">
                    <div class="form-header">
                        <h2>
                            Searvice Name
                            <%--(Code-Industry Name)--%></h2>
                    </div>
                    <div class="form-body ">
                        <div class="guidelinesdetails">
                            <div class="form-group ">
                                <div class="row">
                                    <div class="col-sm-12">
                                    <h4>Guideline</h4>

                                    <div class="guidelines">
                                    <p>
                                    Guideline, Instructions, Documents to be upload &amp; Payment details to be mentioned here.
                                      </p> 
                                      
                                      </div>
                                    </div>
                                    <%--<div class="col-sm-12 text-right">
                                        <span class="apply ">
                                            <asp:Label ID="lblApply" runat="server" Text="Apply" Visible="false"></asp:Label>
                                            <asp:Button ID="btnApply" runat="server" Text="Proceed" CssClass="btn btn-success"
                                                Width="80" OnClick="btnApply_Click" />
                                        </span>
                                    </div>--%>
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
