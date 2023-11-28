<%--'*******************************************************************************************************************
' File Name         : ExistingIndustryDetails.aspx
' Description       : Details of existing industry
' Created by        : AMit Sahoo
' Created On        : 01 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExistingIndustryDetails.aspx.cs"
    Inherits="ExistingIndustryDetails" %>

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
            </a>Existing Industry Details</h2>
    </div>
    <div class="registration-div">
        <div class="container">
            <div class="form-sec">
                <h2>
                    Existing Industry Details</h2>
                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-12">
                            <label for="INDUSTRY">
                                DETAILS OF EXISTING INDUSTRY</label>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-6">
                            <label for="Type3">
                                Whether the application is for *</label><br />
                            <asp:RadioButtonList ID="rdbApplication" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                <asp:ListItem class="radio-inline" Text="A new Unit" Value="0" />
                                <asp:ListItem class="radio-inline" Text="Expansion of an existing unit" Value="1" />
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-sm-6">
                            <label for="Type4">
                                Whether the existing Industry is located *</label><br />
                            <asp:RadioButtonList ID="rdbExIndustry" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                <asp:ListItem class="radio-inline" Text="With in the state" Value="0" />
                                <asp:ListItem class="radio-inline" Text="Outside the state" Value="1" />
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                    <div class="col-sm-12">
                                        <asp:CheckBox ID="chkBoxName" runat="server" Text="(NAME SAME AS NAME OF INDUSTRY/ENTERPRISE)">
                                        </asp:CheckBox>
                                    </div>
                                </div>
                            </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-6">
                            <label for="Name">
                                Name(if Different)</label>
                            <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-6">
                            <label for="Location">
                                Location</label>
                            <asp:TextBox ID="txtOthers" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-6">
                            <label for="land">
                                Extent of Land in Acres</label>
                            <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-6">
                            <label for="Activity">
                                Nature of Activity</label>
                            <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-6">
                            <label for="Capacity">
                                Capacity</label>
                            <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-6">
                            <label for="ICapacity">
                                Installed Capacity of Power</label>
                            <asp:TextBox ID="TextBox4" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-6">
                            <label for="Supply">
                                Source of Power Supply</label>
                            <asp:TextBox ID="TextBox5" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-6">
                            <label for="material">
                                Existing raw material arrangements</label>
                            <asp:TextBox ID="TextBox6" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-4">
                        </div>
                        <div class="col-sm-4" align="center">
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass=" btn btn-warning" 
                                Width="80" onclick="btnBack_Click" />
                            <asp:Button ID="btnNext" runat="server" Text="Next" CssClass=" btn btn-success" 
                                Width="80" onclick="btnNext_Click"
                                />
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
