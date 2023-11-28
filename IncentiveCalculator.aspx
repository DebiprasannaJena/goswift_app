<%--'*******************************************************************************************************************
' File Name         : IncentiveCalculator.aspx
' Description       : Calculation of Incentives
' Created by        : AMit Sahoo
' Created On        : 30th June 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncentiveCalculator.aspx.cs"
    Inherits="IncentiveCalculator" %>

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
            </a>Incentive Calculator</h2>
    </div>
    <div class="registration-div">
        <div class="container">
            <div id="exTab1" class="container">
                <ul class="nav nav-pills">
                    <li><a href="InvestorRegistration.aspx">Investor Profile</a> </li>
                    <li><a href="Proposals.aspx">Proposals</a> </li>
                    <li><a href="DepartmentClearance.aspx">Clearance</a> </li>
                    <li class="active"><a href="IncentiveCalculator.aspx">Incentive Calculator</a> </li>
                    <li><a href="IncentiveApplicationStatus.aspx">Incentive</a> </li>
                </ul>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="4a">
                        <div class="form-sec">
                            <h2>
                                Incentive Calculator</h2>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <label for="Iname">
                                            Industry Name</label>
                                        <asp:Label ID="lblIName" runat="server" Text="*"></asp:Label>
                                        <asp:TextBox ID="txtIName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-5">
                                        <label for="Sector">
                                            Sector</label>
                                        <asp:Label ID="lblSector" runat="server" Text="*"></asp:Label>
                                        <asp:DropDownList ID="ddlSector" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--Select Sector--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <label for="SubSector">
                                            Sub-sector</label>
                                        <asp:Label ID="lblSubSector" runat="server" Text="*"></asp:Label>
                                        <asp:DropDownList ID="ddlSubSector" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--Select Sub-Sector--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-5">
                                        <label for="Power">
                                            Apprx. power units consumed per month</label>
                                        <asp:TextBox ID="txtUnit" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <label for="Amount">
                                            Apprx. investment amount(Cr.)</label>
                                        <asp:Label ID="lblAmount" runat="server" Text="*"></asp:Label>
                                        <asp:TextBox ID="txtAmount" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-5">
                                        <label for="InvestAmount">
                                            Apprx. plant & Machinery Investment amount(Cr.)</label>
                                        <asp:TextBox ID="txtInvestAmount" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <label for="IndLoc">
                                            Industry Location</label>
                                        <asp:Label ID="lblLoc" runat="server" Text="*"></asp:Label>
                                        <asp:DropDownList ID="ddlLoc" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--Select District--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-5">
                                        <label for="Employment">
                                            Local Employment</label>
                                        <asp:DropDownList ID="ddlEmployment" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--Select Employment--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <label for="InvCat">
                                            Investor Category</label>
                                        <asp:Label ID="lblCategory" runat="server" Text="*"></asp:Label>
                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--Select Category--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-6">
                                        <label for="Gender">
                                            Gender</label><br />
                                        <asp:RadioButtonList ID="rdbGender" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                            <asp:ListItem class="radio-inline" Text="Male" Value="0" Selected="True" />
                                            <asp:ListItem class="radio-inline" Text="Female" Value="1" />
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-4" align="center">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass=" btn btn-success"
                                            Width="80" />
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="2a">
                        <h3>
                            We use the class nav-pills instead of nav-tabs which automatically creates a background
                            color for the tab</h3>
                    </div>
                    <div class="tab-pane" id="3a">
                        <h3>
                            We applied clearfix to the tab-content to rid of the gap between the tab and the
                            content</h3>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
