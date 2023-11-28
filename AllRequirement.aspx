<%--'*******************************************************************************************************************
' File Name         : AllRequirement.aspx
' Description       : Details of Requirement
' Created by        : Radhika Rani Patri
' Created On        : 01 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllRequirement.aspx.cs" Inherits="AllRequirement" %>

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
                    <ol class="breadcrumb breadcrumb-arrow">
                        <li><a href="PromoterDetails.aspx">Promoter Details</a></li>
                        <li><a href="IndustryInformation.aspx">Industry Information</a></li>
                        <li><a href="ProposedSiteDetails.aspx">Site Details</a></li>
                        <li><a href="ManPowerDetails.aspx">Man Power Details</a></li>
                         <li><a href="landdetails.aspx" >Land Details</a></li>
                        <li><a href="AllRequirement.aspx" class="active">Resouces Requirement</a></li>
                         <li><a href="javascript:void(0)">Power Details</a></li>
                        <li><a href="javascript:void(0)">Water Details</a></li>
                          <li><a href="javascript:void(0)">Waste Water Details</a></li>
                        <li><a href="javascript:void(0)">Enclosures</a></li>
                        <li><a href="javascript:void(0)">Declaration</a></li>
                    </ol>
                </div>
              
                <div class="form-sec">
                    <div class="form-header">
                        <h2>
                            Power Requirement(in KVA)</h2>
                    </div>
                    <div class="form-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="h4-header">
                                        Temporary Connection(During Construction)</h4>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2" for="Iname">
                                    Power required<span class="text-red">*</span></label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlPower" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="Capacity">
                                        Load demand<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox9" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <label for="ICapacity">
                                        Expected demand start date<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox10" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="h4-header">
                                        Regular Connection(During Production)</h4>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-3">
                                    Sources Of Regular Connection</label>
                                <div class="col-sm-1">
                                    <asp:CheckBox ID="CheckBox2" runat="server" Text="GRID"></asp:CheckBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:CheckBox ID="CheckBox6" runat="server" Text="CPP"></asp:CheckBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:CheckBox ID="CheckBox7" runat="server" Text="IPP"></asp:CheckBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="Capacity">
                                        Load demand from GRID /CPP/IPP<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox11" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <label for="ICapacity">
                                        Type of Process<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox12" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="Capacity">
                                        Capacity of the CPP Plant (in KW)<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox13" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <label for="ICapacity">
                                        Provide IPP source name<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox14" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="Capacity">
                                        Whether use Renewable energy<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox15" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <label for="ICapacity">
                                        Load demand (if YES)</label>
                                    <asp:TextBox ID="TextBox16" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-sec">
                    <div class="form-header">
                        <h2>
                            Water Requirement</h2>
                    </div>
                    <div class="form-body">
                        <div class="form-group">
                          <div class="row">
                                <div class="col-sm-4">
                                    <label for="Capacity">
                                      Existing<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TextBox17" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                               
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <label for="Promoter">
                                       Provide Proposed Sources of Water</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-12">
                                    <label for="INDUSTRY">
                                        Sources of Water</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-2">
                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Ground Water"></asp:CheckBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:CheckBox ID="CheckBox3" runat="server" Text="Surface Water"></asp:CheckBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:CheckBox ID="CheckBox4" runat="server" Text="Sea Water"></asp:CheckBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:CheckBox ID="CheckBox5" runat="server" Text="IDCO Supply"></asp:CheckBox>
                                </div>
                                 <div class="col-sm-2">
                                    <asp:CheckBox ID="CheckBox8" runat="server" Text="ULB"></asp:CheckBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:CheckBox ID="CheckBox9" runat="server" Text="Others"></asp:CheckBox>
                                </div>
                            </div>
                        </div>
                          <div class="form-group">
                         <div class="row">
                                <div class="col-sm-4">
                                    <label for="Capacity">
                                        Ground/Surface/…proposed</label>
                                    <asp:TextBox ID="TextBox18" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <label for="ICapacity">
                                        Ground/Surface/…existing</label>
                                    <asp:TextBox ID="TextBox19" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-8">
                                    <label for="RAddress">
                                      Details of Rain Water harvesting and/or other water conservation measures (proposed by the company)</label>
                                    <asp:TextBox ID="txtDetl" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-sec">
                    <div class="form-header">
                        <h2>
                            Waste Water Management</h2>
                    </div>
                    <div class="form-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-3">
                                    <label for="Promoter">
                                        Treatment technology</label>
                                    <asp:TextBox ID="txttech" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    <label for="Promoter">
                                        Quantum of recycling of waste water</label>
                                    <asp:TextBox ID="txtQuat" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <label for="INDUSTRY">
                                        Management of Hazardous waste (if any)</label>
                                    <asp:TextBox ID="txtMang" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-sec">
                <%--    <div class="form-header">
                        <h2>
                            PROPOSED PROJECT SCHEDULE</h2>
                    </div>
                    <div class="form-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="Promoter">
                                        Start of Project Construction<span class="text-red">*</span></label>
                                    <div class="input-group  date datePicker" id="datePicker">
                                        <input type="text" class="form-control ">
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <label for="Promoter">
                                        Start of Production<span class="text-red">*</span></label>
                                    <div class="input-group  date datePicker" id="datePicker2">
                                        <input type="text" class="form-control">
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                    <div class="form-footer">
                        <div class="row">
                            <div class="col-sm-12" align="center">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass=" btn btn-warning" OnClick="btnBack_Click" />
                                <asp:Button ID="btnNext" runat="server" Text="Next" CssClass=" btn btn-success" OnClick="btnNext_Click" />
                                <asp:Button ID="Button1" runat="server" Text="Save as draft" CssClass=" btn btn-primary" />
                                <input type="reset" text="Reset" class=" btn btn-reset" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <link href="css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
        <script src="js/bootstrap-datetimepicker.js" type="text/javascript"></script>
        <script>
            $(document).ready(function () {
                $('.datePicker').datepicker({
                    autoclose: true,
                    format: 'mm/dd/yyyy'
                });

            });
        </script>
        <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
