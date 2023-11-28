<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CertificateDownloadFB.aspx.cs"
    Inherits="CertificateDownloadFB" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <style>
        .bindlabel
        {
            border: 1px solid #cccccc;
            padding: 3px 10px;
            margin-top: 0px !important;
        }
    </style>
    <style>
        .search-sec
        {
            padding: 20px 20px 10px;
        }
        .nodatafound
        {
            font-size: 26px;
            text-align: center;
            color: #d2d2d2;
        }
        .nodatafound td
        {
            padding: 60px !important;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="navigatorheader-div aboutheadernav">
            <div class="col-sm-12">
                <ul class="breadcrumb">
                    <li><a href="Default.aspx" title="Home page"><i class="fa fa-home"></i></a></li>
                    <li>Department of Labour (Directorate of Factories & Boilers)</li></ul>
            </div>
            <div class="clearfix">
            </div>
        </div>
        <div class="registration-div investors-bg">
            <div class="">
                <div id="exTab1">
                    <div class="form-sec">
                        <%--<div class="form-header">
                            <a href="ApplicationDetails.aspx" title="Drafted Proposals" class="pull-right proposalbtn active"> 
                                Application Details</a>
                                <a href="javascript:void(0);" title="Drafted Proposals" class="pull-right proposalbtn">
                                Apply Service</a>
                            <h2>
                                Clearance/Approval
                                <%--(Code-Industry Name)</h2>
                        </div>--%>
                        <div class="content-form-section margin-top20">
                            <div class="">
                                <div class="form-group ">
                                    
                                    <div>
                                        <label class="col-md-2 col-sm-2">
                                            Factory Name</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="ddlFactname" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            	<%--<asp:ListItem>PRESSELS PRIVATE LIMITED</asp:ListItem>
                                                <asp:ListItem>SWAGAT ENGINEERING</asp:ListItem>
                                                <asp:ListItem>ENERGY TEK BOILER</asp:ListItem>
                                                <asp:ListItem>STEELO PRECISION WORKS</asp:ListItem>
                                                <asp:ListItem>VEDVYAS ENGINEERING WORKS</asp:ListItem>
                                                <asp:ListItem>SMS INDIA PVT.LTD</asp:ListItem>
                                                <asp:ListItem>EAST END TECHNOLOGIES PVT.LTD</asp:ListItem>
                                                <asp:ListItem>BHAVANI ERECTORS PRIVATE LIMITED</asp:ListItem>
                                                <asp:ListItem>PCP INTERNATIONAL LIMITED</asp:ListItem>
                                                <asp:ListItem>JASPAL ENGINEERING</asp:ListItem>
                                                <asp:ListItem>PRESSELS PRIVATE LIMITED</asp:ListItem>
                                                <asp:ListItem>RUKMANI INFRA PROJECTS PVT.LTD</asp:ListItem>
                                                <asp:ListItem>BISI ENGINEERING</asp:ListItem>
                                                <asp:ListItem>SUN-TECH ENGINEERS</asp:ListItem>
                                                <asp:ListItem>PRECISION ENGINEERING</asp:ListItem>
                                                <asp:ListItem>EDAC ENGINEERING LTD</asp:ListItem>
                                                <asp:ListItem>ROYAL ENTERPRISER</asp:ListItem>
                                                <asp:ListItem>AMBIKA ENGINEERING</asp:ListItem>
                                                <asp:ListItem>EAST END TECHNOLOGIES PVT.LTD</asp:ListItem>
                                                <asp:ListItem>KARPARA PROJECT ENGINEERING PVT.LTD</asp:ListItem>
                                                <asp:ListItem>TESSY ENGINEERS & ENTERPRISES</asp:ListItem>
                                                <asp:ListItem>THERMAX ENGINEERING CONSTRUCTION CO.LTD</asp:ListItem>
                                                <asp:ListItem>CETHAR LIMITED</asp:ListItem>--%>
                                                <asp:ListItem>AMBIKA ENGINEERING</asp:ListItem>
                                            <asp:ListItem>BISI ENGINEERING</asp:ListItem>
                                            <asp:ListItem>BHAVANI ERECTORS PRIVATE LIMITED</asp:ListItem>
                                            <asp:ListItem>CETHAR LIMITED</asp:ListItem>
                                            <asp:ListItem>EAST END TECHNOLOGIES PVT.LTD</asp:ListItem>
                                            <asp:ListItem>EDAC ENGINEERING LTD</asp:ListItem>
                                            <asp:ListItem>ENERGY TEK BOILER</asp:ListItem>
                                            <asp:ListItem>JASPAL ENGINEERING</asp:ListItem>
                                            <asp:ListItem>KARPARA PROJECT ENGINEERING PVT.LTD</asp:ListItem>                                                  
                                            <asp:ListItem>PCP INTERNATIONAL LIMITED</asp:ListItem>
                                            <asp:ListItem>PRECISION ENGINEERING</asp:ListItem>
                                            <asp:ListItem>PRESSELS PRIVATE LIMITED</asp:ListItem>
                                            <asp:ListItem>ROYAL ENTERPRISER</asp:ListItem>
                                            <asp:ListItem>RUKMANI INFRA PROJECTS PVT.LTD</asp:ListItem>
                                            <asp:ListItem>SMS INDIA PVT.LTD</asp:ListItem>
                                            <asp:ListItem>STEELO PRECISION WORKS</asp:ListItem>
                                            <asp:ListItem>SUN-TECH ENGINEERS</asp:ListItem>                                                   
                                            <asp:ListItem>SWAGAT ENGINEERING</asp:ListItem> 
                                            <asp:ListItem>TESSY ENGINEERS & ENTERPRISES</asp:ListItem>
                                            <asp:ListItem>THERMAX ENGINEERING CONSTRUCTION CO.LTD</asp:ListItem>
                                            <asp:ListItem>VEDVYAS ENGINEERING WORKS</asp:ListItem>
                                                
                                                
                                                
                                                
                                               
                                                
                                               
                                                
                                                
                                                
                                                
                                                
                                               
                                               
                                                
                                               
                                                
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div>
                                        <label class="col-md-2 col-sm-2">
                                            Service Name</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="ddlServicename" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="BOILER MANUFACTURER" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="BOILER ERECTOR" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <span class="apply ">
                                            <asp:Button ID="btnApply" runat="server" Text="Search" CssClass="btn btn-success"
                                                Width="80" />
                                        </span>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div>
                                </div>
                                <div class="table-responsive" id="divGrd" style="margin-top: 15px;">
                                    <asp:GridView ID="gvDeptClearance" runat="server" CssClass="table table-bordered table-striped bg-white"
                                        Style="margin-bottom: 0px;" AllowPaging="false" Width="100%" AutoGenerateColumns="False"
                                        OnRowDataBound="gvDeptClearance_RowDataBound" OnPageIndexChanging="gvDeptClearance_PageIndexChanging"
                                        EmptyDataText="No Record(s) Found" EmptyDataRowStyle-CssClass="nodata" CellPadding="4"
                                        DataKeyNames="CERTIFICATEURL" GridLines="None">
                                        <Columns>
                                            <asp:BoundField HeaderText="Sl#." HeaderStyle-Width="50px" />
                                            <asp:TemplateField HeaderText="Factory Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfact" runat="server" Text='<%# Eval("FACTORYNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Service Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblService" runat="server" Text='<%# Eval("SERVICENAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ceritficate" HeaderStyle-Width="80px">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hypApply" runat="server" NavigateUrl="#" CssClass=" btn  btn-success btn-sm"><i class="fa fa-download"></i></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
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
