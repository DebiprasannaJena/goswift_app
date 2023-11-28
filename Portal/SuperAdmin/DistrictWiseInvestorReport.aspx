<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" EnableEventValidation = "false" CodeFile="DistrictWiseInvestorReport.aspx.cs" Inherits="Portal_SuperAdmin_District_Wise_Investor_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <script src="../js/custom.js" type="text/javascript"></script>

        <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/decimalrstr.js" type="text/javascript"></script>
   
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                   District Wise Investor Report 
                </h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Admin Privileges</a></li><li><a>View Investor Report</a></li></ul>
            </div>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="dropdown">
                                <ul class="dropdown-menu dropdown-menu-right">
                                    <li>
                                        <asp:LinkButton ID="lnkPdf" CssClass="back" runat="server" title="Export to PDF" OnClick="lnkPdf_Click"
                                            ><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                    <li><a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                                        <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" title="Export to Excel" OnClick="lnkExport_Click"
                                            ><i class="fa fa-file-excel-o"></i></asp:LinkButton>
                                    </a></li>
                                    <li><a class="PrintBtn" data-tooltip="Print" data-toggle="tooltip" data-title="Print">
                                        <i class="panel-control-icon fa fa-print"></i></a></li>
                                    <li><a href="javascript:history.back()" data-tooltip="Back" data-toggle="tooltip"
                                        data-title="Back"><i class="panel-control-icon fa  fa-chevron-circle-left"></i></a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="search-sec NOPRINT">
                                <div class="form-group row NOPRINT">
                                    <asp:UpdatePanel ID="up1" runat="server">
                                        <ContentTemplate>
                                        
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                                <div class="form-group row NOPRINT">
                                    <div class="col-sm-3">
                                        <label for="Country">
                                            District</label>
                                        <asp:DropDownList CssClass="form-control" TabIndex="16" ID="ddlDistrict" runat="server">
                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnSearch" Style="margin-top: 22px" CssClass="btn btn btn-add btn-sm"
                                            runat="server" Text="Show" OnClick="btnSearch_Click" OnClientClick="return ValidatePage();"></asp:Button>
                                    </div>
                                </div>
                            </div>
                           
                                    <div class="table-responsive">
                                        <asp:Label ID="LblSearchDetails" runat="server" Font-Bold="true"></asp:Label>
                                        <div style="display: inline-block; text-align: right; width: 100%">
                                            <asp:LinkButton ID="LbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                                OnClick="LbtnAll_Click"></asp:LinkButton>
                                            
                                            
                                            <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                        </div>
                                         <div class="table-responsive" id="viewTable" runat="server">
                                   <asp:GridView ID="GrdInvestor" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
                                            CssClass="table table-bordered table-hover" AllowPaging="true" ShowFooter="false" OnPageIndexChanging="GrdInvestor_PageIndexChanging" OnRowDataBound="GrdInvestor_RowDataBound" Width="100%" PageSize="100">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(GrdInvestor.PageIndex * GrdInvestor.PageSize) + (GrdInvestor.Rows.Count + 1)%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Investor Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="investorname" runat="server" Text='<%# Eval("strInvestorName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="userid" runat="server" Text='<%# Eval("strUserId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">
                                                        <ItemTemplate>
                                                            <asp:Label ID="email" runat="server" Text='<%# Eval("strEmailId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact FirstName">
                                                        <ItemTemplate>
                                                            <asp:Label ID="contactfirstname" runat="server" Text='<%# Eval("strContactPersn") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                    </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="address" runat="server" Text='<%# Eval("strAddress") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Mobile No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mobileno" runat="server" Text='<%# Eval("strMobile") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="EIN_IEM">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Ein_Iem" runat="server" Text='<%# Eval("VCH_EIN_IEM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="DTM Created On">
                                                        <ItemTemplate>
                                                            <asp:Label ID="dtmcreatedon" runat="server" Text='<%# Eval("DTM_CREATED_ON") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="District Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DistrictName" runat="server" Text='<%# Eval("vchDistrictName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Block Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BlockName" runat="server" Text='<%# Eval("vchBlockName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Sector Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SectorName" runat="server" Text='<%# Eval("vchSectorName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="SubSector Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SubSectorName" runat="server" Text='<%# Eval("vchSubSectorName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Industry Type">
                                                        <ItemTemplate>
                                                             <asp:HiddenField ID="HdnIndustryType" runat="server" Value='<%# Eval("StrIndustyType") %>' />
                                                            <asp:Label ID="IndustryType" runat="server" Text='<%# Eval("StrIndustyType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Regd Source">
                                                        <ItemTemplate>
                                                            <asp:Label ID="RegdSource" runat="server" Text='<%# Eval("StrRegdSource") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="LICENCE NO TYPE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LICENCE_NO_TYPE" runat="server" Text='<%# Eval("StrLicenceNoType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="InvLevel">
                                                        <ItemTemplate>
                                                             <asp:HiddenField ID="HdnInvLevel" runat="server" Value='<%# Eval("StrIndustyType") %>' />
                                                            <asp:Label ID="InvLevel" runat="server" Text='<%# Eval("strInvLevel") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                        </asp:GridView>
                                             </div>

                                          <div runat="server" visible="false">
                                   <asp:GridView ID="Grdforexcel" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
                                            CssClass="table table-bordered table-hover" ShowFooter="false" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                     <ItemTemplate>
                                                            <asp:Label ID="lblsl" runat="server" Text='<%# (Container.DataItemIndex + 1)%>'></asp:Label>
                                                        </ItemTemplate>
                                                  
                                                    <ItemStyle Width="4%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Investor Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="investorname" runat="server" Text='<%# Eval("strInvestorName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="userid" runat="server" Text='<%# Eval("strUserId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">
                                                        <ItemTemplate>
                                                            <asp:Label ID="email" runat="server" Text='<%# Eval("strEmailId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact FirstName">
                                                        <ItemTemplate>
                                                            <asp:Label ID="contactfirstname" runat="server" Text='<%# Eval("strContactPersn") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                    </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="address" runat="server" Text='<%# Eval("strAddress") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Mobile No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mobileno" runat="server" Text='<%# Eval("strMobile") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="EIN_IEM">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Ein_Iem" runat="server" Text='<%# Eval("VCH_EIN_IEM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="DTM Created On">
                                                        <ItemTemplate>
                                                            <asp:Label ID="dtmcreatedon" runat="server" Text='<%# Eval("DTM_CREATED_ON") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="District Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DistrictName" runat="server" Text='<%# Eval("vchDistrictName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Block Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BlockName" runat="server" Text='<%# Eval("vchBlockName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Sector Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SectorName" runat="server" Text='<%# Eval("vchSectorName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="SubSector Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SubSectorName" runat="server" Text='<%# Eval("vchSubSectorName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Industry Type">
                                                        <ItemTemplate>
                                                             <asp:HiddenField ID="HdnIndustryType" runat="server" Value='<%# Eval("StrIndustyType") %>' />
                                                            <asp:Label ID="IndustryType" runat="server" Text='<%# Eval("StrIndustyType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Regd Source">
                                                        <ItemTemplate>
                                                            <asp:Label ID="RegdSource" runat="server" Text='<%# Eval("StrRegdSource") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="LICENCE NO TYPE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LICENCE_NO_TYPE" runat="server" Text='<%# Eval("StrLicenceNoType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="InvLevel">
                                                        <ItemTemplate>
                                                             <asp:HiddenField ID="HdnInvLevel" runat="server" Value='<%# Eval("StrIndustyType") %>' />
                                                            <asp:Label ID="InvLevel" runat="server" Text='<%# Eval("strInvLevel") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                        </asp:GridView>
                                             </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

