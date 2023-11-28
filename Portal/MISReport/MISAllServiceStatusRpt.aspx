<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MISAllServiceStatusRpt.aspx.cs"
    Inherits="Portal_MISReport_MISAllServiceStatusRpt" MasterPageFile="~/MasterPage/Application.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../css/chosen.css" rel="stylesheet" />
    <script src="../../js/chosen.jquery.js"></script>
    <script language="javascript" type="text/javascript">

        function pageLoad() {
            $("#ContentPlaceHolder1_ddlindustry").chosen({ placeholder_text: "Select Industry Name" });
            $("#ContentPlaceHolder1_ddlindustry").chosen({ no_results_text: "Search name not found" });

        }

    
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    MIS Report</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Proposal</a></li><li><a>View</a></li></ul>
            </div>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- Form controls -->
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="dropdown">
                                <ul class="dropdown-menu dropdown-menu-right">
                                    <li>
                                        <asp:LinkButton ID="lnkPdf" runat="server" CssClass=" fa fa-file-pdf-o" title="Export to PDF" OnClick="lnkPdf_Click"></asp:LinkButton></li>
                                    <li><a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                                        <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" title="Export to Excel" OnClick="lnkExport_Click"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
                                    </a></li>
                                    <li><a class="PrintBtn" data-tooltip="Print" data-toggle="tooltip" data-title="Print">
                                        <i class="panel-control-icon fa fa-print"></i></a></li>
                                    <li><a href="javascript:history.back()" data-tooltip="Back" data-toggle="tooltip"
                                        data-title="Back"><i class="panel-control-icon fa  fa-chevron-circle-left"></i></a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                           
                        <div class="panel-body">
                            <div class="search-sec NOPRINT">
                                <div class="form-group row NOPRINT">
                                    
                                    <div class="col-sm-3">
                                        <label for="Country">
                                            District</label>
                                        <asp:DropDownList CssClass="form-control" TabIndex="1" ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="State">
                                            Block
                                        </label>
                                        <asp:DropDownList ID="ddlblock" runat="server" CssClass="form-control" TabIndex="2" OnSelectedIndexChanged="ddlblock_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                        </asp:DropDownList>                                      
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="State">
                                            Industry Name
                                        </label>
                                        <asp:DropDownList ID="ddlindustry" runat="server" CssClass="form-control" TabIndex="3" OnSelectedIndexChanged="ddlindustry_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="State">
                                           Unit Name
                                        </label>
                                        <asp:DropDownList ID="ddlunity" runat="server" CssClass="form-control" TabIndex="4">
                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                     <div class="col-sm-2" style="margin-left:156px;">
                                        <asp:Button ID="btnSearch" Style="margin-top: 22px" CssClass="btn btn btn-add btn-sm" runat="server" Text="Search" OnClick="btnSearch_Click">
                                        </asp:Button> 
                                          <asp:Button ID="btnReset" Style="margin-top: 22px" CssClass="btn btn btn-add btn-sm" runat="server" Text="Reset" OnClick="btnReset_Click">
                                        </asp:Button>
                                    </div>
                                           
                                           
                                </div>                               
                            </div> 
                            <div align="right" >
                                    <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                        OnClick="lbtnAll_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblPaging" runat="server" Visible="false"></asp:Label>
                                </div>
                              <div class="table-responsive" id="viewTable" runat="server">                               
                                <asp:GridView ID="grdPealDetails" runat="server" class="table table-bordered table-hover"
                                    AutoGenerateColumns="false" EmptyDataText="No Record(s) found...." 
                                    DataKeyNames="INT_INVESTOR_ID" PageSize="10" OnPageIndexChanging="grdPealDetails_PageIndexChanging" AllowPaging="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#">
                                            <ItemTemplate>
                                                 <asp:Label ID="lblsl" runat="server" Text='<%#(grdPealDetails.PageIndex * grdPealDetails.PageSize) + (grdPealDetails.Rows.Count + 1)%>'></asp:Label>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Industry Name">
                                            <ItemTemplate>                                                
                                             <asp:Label ID="lblindustryname" runat="server" Text='<%#Eval("VCH_INV_NAME")%>'></asp:Label>                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land">
                                            <ItemTemplate>                                                
                                             <asp:Label ID="lblland" runat="server" Text='<%#Eval("Land")%>'></asp:Label>                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Water">
                                            <ItemTemplate>                                                
                                             <asp:Label ID="lblwater" runat="server" Text='<%#Eval("Water")%>'></asp:Label>                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Power">
                                            <ItemTemplate>                                                
                                             <asp:Label ID="lblpower" runat="server" Text='<%#Eval("Power")%>'></asp:Label>                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                     <PagerStyle CssClass="pagination-grid no-print" />
                                </asp:GridView>                   
                                
                            </div>
                        </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
       
    </div>
</asp:Content>
