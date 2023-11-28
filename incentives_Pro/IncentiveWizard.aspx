<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/incentives/IncentiveWizard.aspx.cs"
    Inherits="incentives_IncentiveWizard" %>

<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            });
        });
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:header ID="header" runat="server" />
    <div class="registration-div investors-bg">
        <div id="exTab1" class="container">
            <div class="investrs-tab">
                <ul class="nav nav-pills">
                    <li class="menudashboard"><a href="javascript:void(0)"><i class="fa fa-tachometer"></i>
                        Dashboard</a> </li>
                    <li class="menuprofile"><a href="../InvesterProfile.aspx"><i class="fa fa-user"></i>
                        Investor Profile</a> </li>
                    <li class="menuproposal"><a href="../Proposals.aspx"><i class="fa fa-briefcase"></i>
                        Proposals</a> </li>
                    <li class="menuservices"><a href="../DepartmentClearance.aspx"><i class="fa fa-wrench">
                    </i>Services</a> </li>
                    <%--  <li><a href="IncentiveCalculator.aspx">Incentive Calculator</a> </li>--%>
                    <li class="menuincentive"><a href="incentiveoffered.aspx"><i class="fa fa-money"></i>
                        Incentive</a> </li>
                </ul>
            </div>
            <div class="tab-content clearfix">
                <div class="tab-pane active" id="1a">
                    <div class="form-sec">
                        <div class="innertabs">
                            <ul class="nav nav-pills pull-right">
                                <li><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                                <li class="active"><a href="appliedindustrylist.aspx">Apply For incentive</a></li>
                                <li><a href="javscript:void(0);">View Application Status</a></li>
                            </ul>
                            <div class="clearfix">
                            </div>
                        </div>
                        <div class="form-header">
                            <div class="iconsdiv">
                                <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                    <i class="fa fa-print"></i></a><a href="javascript:void(0);" title="Export to Excel"
                                        id="A1" class="pull-right printbtn"><i class="fa fa-file-excel-o"></i></a>
                                <a href="javascript:history.back()" title="Back" id="A3" class="pull-right printbtn">
                                    <i class="fa fa-chevron-circle-left"></i></a>
                            </div>
                            <h2>
                                Query to Check Eligibility Condition
                            </h2>
                        </div>
                        <div class="form-body">
                            <div class="row" runat="server">
                                <div class="col-sm-12" align="center" runat="server" id="dvHeader">
                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red"
                                        Text="Which sector are you interested in ?"></asp:Label>
                                </div>
                                <div class="details-section" runat="server" id="dvSection">
                                    <table class="table table-bordered">
                                        <tr align="center">
                                            <td width="40%" align="center">
                                                <strong>Select Sector :</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:DropDownList ID="ddlSector" runat="server" Width="300px">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem>Textiles (including Technical Textile & Apparel)</asp:ListItem>
                                                    <asp:listitem value="1">Agro/Food Processing including Seafood
                                                    </asp:listitem>
                                                    <asp:listitem>Ancillary and Downstream
                                                    </asp:listitem>
                                                    <asp:listitem>Automobiles and Auto-components
                                                    </asp:listitem>
                                                    <asp:listitem>Fly ash & Blast furnace slag based industries utilizing a minimum of 25% by weight as base raw material
                                                    </asp:listitem>
                                                    <asp:listitem>Gem stone cutting and polishing
                                                    </asp:listitem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-sm-12" align="center" runat="server" id="dvHeader1" visible="false">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red"
                                        Text="What is the expected investment in plant and machinery in INR Crores?"></asp:Label>
                                </div>
                                <div class="details-section" id="dvSection1" runat="server" visible="false">
                                    <table class="table table-bordered" visible="false">
                                        <tr align="center">
                                            <td width="40%">
                                                <strong>Expected Investment Size(INR Crore) :</strong>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:DropDownList ID="ddlInvest" runat="server" Width="200px">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">< 5 Crore</asp:ListItem>
                                                    <asp:ListItem Value="1">5-10</asp:ListItem>
                                                    <asp:ListItem Value="1">11-50</asp:ListItem>
                                                    <asp:ListItem Value="1">51-100</asp:ListItem>
                                                    <asp:ListItem Value="1">101-200</asp:ListItem>
                                                    <asp:ListItem Value="1">201-250</asp:ListItem>
                                                    <asp:ListItem Value="1">251-500</asp:ListItem>
                                                    <asp:ListItem Value="1">501 and More</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-sm-12" align="center" id="dvHeader2" runat="server" visible="false">
                                    <asp:Label ID="Label3" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red"
                                        Text="What is the expected employment generation for domicile workers?"></asp:Label>
                                </div>
                                <div class="details-section" id="dvSection2" runat="server" visible="false">
                                    <table class="table table-bordered" visible="false">
                                        <tr align="center">
                                            <td width="40%">
                                                <strong>Select Employment range :</strong>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:DropDownList ID="DropDownList1" runat="server" Width="200px">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">< 20</asp:ListItem>
                                                    <asp:ListItem Value="2">20-75</asp:ListItem>
                                                    <asp:ListItem Value="3">75-80</asp:ListItem>
                                                    <asp:ListItem Value="4">81-100</asp:ListItem>
                                                    <asp:ListItem Value="5">101-150</asp:ListItem>
                                                    <asp:ListItem Value="6">151-180</asp:ListItem>
                                                    <asp:ListItem Value="7">181-200</asp:ListItem>
                                                    <asp:ListItem Value="8">201-250</asp:ListItem>
                                                    <asp:ListItem Value="9">251-300</asp:ListItem>
                                                    <asp:ListItem Value="10">301-500</asp:ListItem>
                                                    <asp:ListItem Value="11">501-750</asp:ListItem>
                                                    <asp:ListItem Value="12">751-1000</asp:ListItem>
                                                    <asp:ListItem Value="13">1001-1500</asp:ListItem>
                                                    <asp:ListItem Value="14">More than 1500</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-sm-12" align="center" id="dvHeader3" runat="server" visible="false">
                                    <asp:Label ID="Label4" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red"
                                        Text="In which district are you planning to setup your Industry?"></asp:Label>
                                </div>
                                <div class="details-section" id="dvSection3" visible="false" runat="server">
                                    <table class="table table-bordered" visible="false">
                                        <tr align="center">
                                            <td width="40%">
                                                <strong>Select District :</strong>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:DropDownList ID="DropDownList2" runat="server" Width="200px">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                       <asp:ListItem Value="1">Angul</asp:ListItem>
                                                         <asp:ListItem Value="2">Baleswar</asp:ListItem>
                                                         <asp:ListItem Value="3">BARGARH</asp:ListItem>
                                                         <asp:ListItem Value="4">BAUDH</asp:ListItem>
                                                            <asp:ListItem Value="4">BHADRAK</asp:ListItem>
                                                               <asp:ListItem Value="4">BOLANGIR</asp:ListItem>
                                                                  <asp:ListItem Value="4">CUTTACK</asp:ListItem>
                                                                       <asp:ListItem Value="4">DEBAGARH</asp:ListItem>
                                                                     <asp:ListItem Value="4">DHENKANAL</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12" align="center" id="dvHeader4" runat="server" visible="false">
                                    <asp:Label ID="Label5" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red"
                                        Text="Would you like to know the incentives available based on your sector and the investment size under the State Policies?"></asp:Label>
                                </div>
                                <div class="details-section" id="dvSection4" visible="false" runat="server">
                                    <table class="table table-bordered" visible="false">                                        
                                        <tr align="center">
                                            <td>
                                                <asp:Button ID="Button4" runat="server" Height="30" Width="50" 
                                                    ForeColor="White" Text="Yes" BackColor="Green" onclick="Button4_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div align="center">
                                    <asp:Button ID="Button1" Height="30" Width="100" runat="server" Font-Bold="true" BackColor="Red"
                                        ForeColor="White" Visible="false" Text="<< Go Back" 
                                        onclick="Button1_Click" />
                                    <asp:Button ID="Button2" Height="30" runat="server" Font-Bold="true" ForeColor="White"
                                        BackColor="Red" Text="Next >>" OnClick="Button2_Click" />
                                   <%-- <asp:Button ID="Button3" Height="30" runat="server" BackColor="Red" Font-Bold="true"
                                        Visible="false" Text="Skip" />--%>
                                </div>
                            </div>
                        </div>
                        <div class="form-footer">
                            <div class="row">
                                <div class="col-sm-12 text-right">
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
