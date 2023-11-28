<%--'*******************************************************************************************************************
' File Name         : BuiltUpRequirements.aspx
' Description       : Requirements to set up industry
' Created by        : AMit Sahoo
' Created On        : 18 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuiltUpRequirements.aspx.cs"
    Inherits="BuiltUpRequirements" %>

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
    <div class="registration-div">
        <div class="container">
            <div class="form-sec">
                <div class="form-header">
                    <span class="mandatoryspan pull-right">( * ) Indicate Mandatory Fields</span>
                    <h2>
                        Land Requirement Details</h2>
                </div>
                <div class="form-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-6">
                                <label for="Town">
                                    Village/Town<span class="text-red">*</span></label>
                                <asp:TextBox ID="TextBox10" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                                <label for="Land">
                                    Extent of Land required (in acre)<span class="text-red">*</span></label>
                                <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-6">
                                <label for="Land">
                                    Name of the industrial estate<span class="text-red">*</span></label>
                                <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                                <label for="Land">
                                    Within the land bank of IDCO</label><br />
                                <asp:RadioButtonList ID="rdbLandBank" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                    <asp:ListItem class="radio-inline" Text="Yes" Value="0" />
                                    <asp:ListItem class="radio-inline" Text="No" Value="1" />
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-6">
                                <label for="Land">
                                    Mention the land schedule within the Land Bank<span class="text-red">*</span></label>
                                <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                                <label for="Land">
                                    Whether Land to be acquired by IDCO</label><br />
                                <asp:RadioButtonList ID="rdbLandAcq" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                    <asp:ListItem class="radio-inline" Text="Yes" Value="0" />
                                    <asp:ListItem class="radio-inline" Text="No" Value="1" Selected="True" />
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-6">
                                <label for="Iname">
                                    Land to be acquired by the company directly</label>
                                <asp:TextBox ID="TextBox6" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                                <label for="Iname">
                                    Mention the land schedule to be acquired by the company directly<span class="text-red">*</span></label>
                                <asp:TextBox ID="TextBox7" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-footer">
                        <div class="row">
                            <div class="col-sm-12" align="center">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass=" btn btn-warning" />
                                <asp:Button ID="btnNext" runat="server" Text="Next" CssClass=" btn btn-success" />
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
