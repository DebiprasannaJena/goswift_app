<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CertificateDownloadLM.aspx.cs" Inherits="CertificateDownloadLM" %>

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
        .nodatafound{font-size:26px;text-align:center;color:#d2d2d2;}
         .nodatafound td{padding:60px!important}
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
                    <li>Food Supplies and Consumer Welfare Department (FSCW)</li></ul>
            </div>
            <div class="clearfix">
            </div>
        </div>
        <div class="content-form-section margin-top20">
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
                        <div class="">
                              
                                <div class="form-group ">
                                   
                                        
                                                <div >
                                            <label class="col-md-2 col-sm-2">
                                                District </label>
                                            <div class="col-sm-3">
                                            <span class="colon">:</span>
                                                <asp:DropDownList ID="ddldist" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>                                                    
                                                    <asp:ListItem>BOLANGIR</asp:ListItem>
                                                    <asp:ListItem>CUTTACK</asp:ListItem>
                                                    <asp:ListItem>GANJAM</asp:ListItem>
                                                    <asp:ListItem>JAJPUR</asp:ListItem>
                                                    <asp:ListItem>KENDRAPARA</asp:ListItem>
                                                    <asp:ListItem>KHURDA</asp:ListItem>
                                                    <asp:ListItem >SUNDAGARH</asp:ListItem>
                                             </asp:DropDownList>

                                         </div>
                                        </div>
                                       <div >
                                            <label class="col-md-2 col-sm-2">
                                                Factory Name</label>
                                            <div class="col-sm-3">
                                            <span class="colon">:</span>
                                                <asp:DropDownList ID="ddlFactname" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                       <asp:ListItem >EASTERN WEIGHING SYSTEMS</asp:ListItem>
                                            <asp:ListItem>M/S.SHIVA ENGINEERING CORP.</asp:ListItem>
                                           <asp:ListItem >M/S.ALCOS</asp:ListItem>
                                            <asp:ListItem>VENUS</asp:ListItem>
                                           <asp:ListItem>M/S.HIGHTEC WEIGHING SCALES</asp:ListItem>
                                           <asp:ListItem>M/S.SILICON WEIGHING TECHNOLOGIES</asp:ListItem>
                                           <asp:ListItem>M/S.KRISHNA INDUSTRIES</asp:ListItem>
                                            <asp:ListItem>M/S. KUMAR SYSTEM</asp:ListItem>
                                            <asp:ListItem>M/S. SIVA SCALES</asp:ListItem>
                                            <asp:ListItem>M/S. DS SYSTEMS</asp:ListItem>
                                           <asp:ListItem >M/S. WEIGHTRACK</asp:ListItem>
                                           <asp:ListItem >M/S.S.S. FOUNDARY</asp:ListItem>
                                            <asp:ListItem >HINDUSTAN ENTERPRISES</asp:ListItem>
                                            <asp:ListItem>M/S.PACIFIC ELECTRONICS & INSTRUMENTS CO.</asp:ListItem>
                                            <asp:ListItem>M/S.SHREE BAJRANGA ALLOYS AND METALS</asp:ListItem>
                                            <asp:ListItem>M/S. KALINGA WEIGHING INDUSTRIES</asp:ListItem>
                                           <asp:ListItem>M/S.VENKATESWAR WEIGHING SERVICES</asp:ListItem>
                                           <asp:ListItem >M/S.JYOTI SCALES & SYSTEM</asp:ListItem>
                                            <asp:ListItem>M/S.USHAWEIGHING & SECURITY SYSTEM</asp:ListItem>
                                            <asp:ListItem>M/S.GOPALJI ENGINEERING WORKS</asp:ListItem>
                                            <asp:ListItem>M/S. GAJALAXMI IRON WORKS</asp:ListItem>
                                            <asp:ListItem>M/S.SUBHAM WEIGHING & ENGG.</asp:ListItem>
                                            <asp:ListItem>M/S. SHREE UDYOG</asp:ListItem>
                                           <asp:ListItem>HI-TECH WEIGHING SCALES</asp:ListItem>
                                           <asp:ListItem>SHRI GOPAL KRISHNA MARODIA</asp:ListItem>
                                           <asp:ListItem >M/S.WEIGHTRACK INDIA PVT.LTD</asp:ListItem>
                                            <asp:ListItem>EASTERN AUTOMATION SYSTEMS</asp:ListItem>
                                            <asp:ListItem>SHIVE ENGINEERING CORP</asp:ListItem>
                                             </asp:DropDownList>

                                         





                                                
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <span class="apply ">
                                                
                                                <asp:Button ID="btnApply" runat="server" Text="Search" CssClass="btn btn-success"
                                                    Width="80"  />
                                            </span>
                                        </div>
                                  <div class="clearfix">   </div>
                                </div>
                                <hr />
                            <div>
                            </div>
                            <div class="table-responsive" id="divGrd" style="margin-top: 15px;">
                                <asp:GridView ID="gvDeptClearance" runat="server" CssClass="table table-bordered table-striped bg-white"  style="margin-bottom: 0px;"
                                    AllowPaging="false" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvDeptClearance_RowDataBound"
                                    OnPageIndexChanging="gvDeptClearance_PageIndexChanging" EmptyDataText="No Record(s) Found" EmptyDataRowStyle-CssClass="nodatafound"
                                    CellPadding="4" DataKeyNames="CERTIFICATEURL"
                                    GridLines="None">
                                    <Columns>
                                        <asp:BoundField HeaderText="Sl#." HeaderStyle-Width="50px" />
                                        <asp:TemplateField HeaderText="District">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDist" runat="server" Text='<%# Eval("DISTRICT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Factory Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfact" runat="server" Text='<%# Eval("FACTORYNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                      
                                       
                                        
                                        <asp:TemplateField HeaderText="Ceritficate" HeaderStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hypApply" runat="server" NavigateUrl="#" 
                                                    CssClass=" btn  btn-success btn-sm"><i class="fa fa-download"></i></asp:HyperLink>
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
