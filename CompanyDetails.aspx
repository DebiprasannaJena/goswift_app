<%--'*******************************************************************************************************************
' File Name         : CompanyDetails.aspx
' Description       : Details of Company
' Created by        : AMit Sahoo
' Created On        : 18 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyDetails.aspx.cs" Inherits="CompanyDetails" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
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
            <div class="wizard wizard2">
                <div class="wizard-inner">
                    <div class="connecting-line">
                    </div>
                </div>
                <div class="form-sec">
                    <div class="form-header">
                        <span class="mandatoryspan pull-right">( * ) Indicate Mandatory Fields</span>
                        <h2>
                            Company Details</h2>
                    </div>
                    <div class="form-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-12">
                                    <label for="Iname">
                                        Name of the Enterprise<span class="text-red">*</span></label>
                                    <asp:CheckBox ID="chkEnterprise" runat="server" Text="(NAME SAME AS NAME OF THE INDUSTRY/ENTERPRISE)">
                                    </asp:CheckBox>
                                    <asp:TextBox ID="txtIName" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-12">
                                    <label for="RAddress">
                                        CORRESPONDENCE ADDRESS</label>
                                    <asp:CheckBox ID="chkAddress" runat="server" Text="(ADDRESS SAME AS FACTORY ADDRESS)">
                                    </asp:CheckBox>
                                </div>
                                <div class="col-sm-6">
                                    <label for="Address1">
                                        Address Line 1<span class="text-red">*</span></label>
                                    <asp:TextBox ID="txtAddress1" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <label for="Address2">
                                        Address Line 2</label>
                                    <asp:TextBox ID="txtAddress2" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-6">
                                    <label for="Address3">
                                        Address Line 3</label>
                                    <asp:TextBox ID="txtAddress3" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <label for="City">
                                        City<span class="text-red">*</span></label>
                                    <asp:TextBox ID="txtCity" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="State">
                                        State<span class="text-red">*</span></label>
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Odisha" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Chattisgarh" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <label for="Country">
                                        Country<span class="text-red">*</span></label>
                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="India" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <label for="PinCode">
                                        PinCode<span class="text-red">*</span></label>
                                    <asp:TextBox ID="txtPinCode" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-6">
                                    <label for="PhoneNo">
                                        Phone Number</label>
                                    <asp:TextBox ID="txtPhoneNo" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <label for="MobileNo">
                                        Mobile Number<span class="text-red">*</span></label>
                                    <asp:TextBox ID="txtMobileNo" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-6">
                                    <label for="FaxNo">
                                        Fax Number</label>
                                    <asp:TextBox ID="txtFaxNo" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <label for="Email">
                                        Email ID<span class="text-red">*</span></label>
                                    <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-12">
                                    <label for="FAddress">
                                        CORPORATE OFFICE ADDRESS</label>
                                    <asp:CheckBox ID="chkCOAddress" runat="server" Text="(ADDRESS SAME AS REGISTERED ADDRESS OF THE INDUSTRY/ENTERPRISE)">
                                    </asp:CheckBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-6">
                                    <label for="Address1">
                                        Address Line 1<span class="text-red">*</span></label>
                                    <asp:TextBox ID="txtFAddress1" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <label for="Address2">
                                        Address Line 2</label>
                                    <asp:TextBox ID="txtFAddress2" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-6">
                                    <label for="Address3">
                                        Address Line 3</label>
                                    <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <label for="Address2">
                                        City<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="State">
                                        State<span class="text-red">*</span></label>
                                    <asp:DropDownList ID="ddlStateO" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Odisha" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Chattisgarh" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <label for="Country">
                                        Country<span class="text-red">*</span></label>
                                    <asp:DropDownList ID="ddlCountryO" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="India" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <label for="PinCode">
                                        PinCode<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox5" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-6">
                                    <label for="PhoneNo">
                                        Phone Number</label>
                                    <asp:TextBox ID="TextBox6" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <label for="MobileNo">
                                        Mobile Number<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox7" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-6">
                                    <label for="FaxNo">
                                        Fax Number</label>
                                    <asp:TextBox ID="TextBox8" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <label for="Email">
                                        Email ID<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox9" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-12">
                                    <label for="FAddress">
                                        ADDRESS FOR CORRESSPONDENCE</label>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="(ADDRESS SAME AS REGISTERED ADDRESS OF THE INDUSTRY/ENTERPRISE)">
                                    </asp:CheckBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-6">
                                    <label for="Address1">
                                        Address Line 1<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox10" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <label for="Address2">
                                        Address Line 2</label>
                                    <asp:TextBox ID="TextBox11" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-6">
                                    <label for="Address3">
                                        Address Line 3</label>
                                    <asp:TextBox ID="TextBox12" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <label for="Address2">
                                        City<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox13" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="State">
                                        State<span class="text-red">*</span></label>
                                    <asp:DropDownList ID="ddlStateC" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="India" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <label for="Country">
                                        Country<span class="text-red">*</span></label>
                                    <asp:DropDownList ID="ddlCountryC" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="India" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <label for="PinCode">
                                        PinCode<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox16" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-6">
                                    <label for="PhoneNo">
                                        Phone Number</label>
                                    <asp:TextBox ID="TextBox17" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <label for="MobileNo">
                                        Mobile Number<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox18" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-6">
                                    <label for="FaxNo">
                                        Fax Number</label>
                                    <asp:TextBox ID="TextBox19" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <label for="Email">
                                        Email ID<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox20" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        
                    </div>
                </div>

                <div class="form-sec">
            <div class="form-header">
                <h2>
                    Company Registration Details</h2>
                    </div>
                    <div class="form-body">                
                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-4">
                            <label for="Date">
                               Date<span class="text-red">*</span></label>
                           
                            <asp:TextBox ID="txtDate" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <label for="Registration Number">
                               Registration Number<span class="text-red">*</span></label>
                            
                            <asp:TextBox ID="txtRegNo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <label for="CIN">
                               CIN (if applicable) <span class="text-red">*</span></label>
                           
                            <asp:TextBox ID="txtCIN" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-4">
                            <label for="PAN">
                               PAN<span class="text-red">*</span></label>
                           
                            <asp:TextBox ID="txtPAN" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <label for="GST">
                               GST</label>
                            
                            <asp:TextBox ID="txtGST" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <label for="CIN">
                               Number of employee in the company</label>
                           
                            <asp:TextBox ID="txtEmpNo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-footer">
                            <div class="row">
                                <div class="col-sm-12" align="center">
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
