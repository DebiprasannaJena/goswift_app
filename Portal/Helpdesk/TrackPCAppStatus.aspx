<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="TrackPCAppStatus.aspx.cs" Inherits="Portal_Helpdesk_TrackPCAppStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"
    ClientIDMode="Static">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../js/WebValidation.js" type="text/javascript"></script>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>Track PC Application Status</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>HelpDesk</a></li>
                    <li><a>Track PC App Status</a></li>
                </ul>
            </div>
        </div>
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-bd lobidisable">
                                <div class="panel-body">
                                    <div class="search-sec">
                                        <div class="form-group row">
                                            <label class="col-sm-2">
                                                Production Certificate No.
                                            </label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:TextBox ID="TxtPcNumberID" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click"  CssClass="btn btn btn-add -sm"
                                                    runat="server" Text="Search" OnClientClick="return ValidateApplicationKey();"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="DivDetails" runat="server">
                                        <div class="form-group ">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">Production Certificate Details
                                                    </h4>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="DivNoRecord" runat="server" class="col-sm-12">
                                            <asp:Label ID="Lbl_Norecord" runat="server" CssClass="form-control-static" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div id="Div_Applicationdetails" runat="server">

                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    Production Certificate Number</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>

                                                    <label>
                                                        <asp:HyperLink ID="Hypr_PcNo" runat="server" Font-Bold="true" CssClass="datalabel" Target="_blank" ToolTip="Click here to view the production certificate details "></asp:HyperLink></label>
                                                    
                                                </div>
                                                <label class="col-sm-2">
                                                    Company Name (Regd.)</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Company_Name" runat="server" CssClass="datalabel" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    Constitution of Company</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Constitution_Name" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    Project Type</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Project_Type" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    Application For</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Application_For" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    Current Status</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Current_Status" runat="server" CssClass="datalabel" ForeColor="Blue"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    Industry Code</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Industry_Code" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    Application Number</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Application_No" runat="server" CssClass="datalabel" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                               <div class="form-group row">                                              
                                                <label class="col-sm-2">
                                                    End of Raise Query Date</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Raise_Query_Date" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    End Of Revert Query Date</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Revert_Query_Date" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                   <label class="col-sm-2">
                                                    Production Certificate Apply Date</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_PC_Apply_Date" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                      
                                       
                                        <div class="form-group ">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">Query Details
                                                    </h4>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="table-responsive">
                                            <asp:GridView ID="GrdQuereyDetails" OnRowDataBound="GrdQuereyDetails_RowDataBound" class="table table-bordered table-hover" runat="server" 
                                                AutoGenerateColumns="false" Width="100%" EmptyDataText="No record found." DataKeyNames="vchFileName">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Production Certificate Number" ItemStyle-Width="10%">
                                                        <ItemTemplate>

                                                            <asp:Label ID="Lbl_PC_No" runat="server" Text='<%# Eval("vchAppFormattedNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Query Reference Number" ItemStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Query_Ref_No" runat="server" Text='<%# Eval("VCH_QUERY_UNQ_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Query Type" ItemStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_QueryType" runat="server" Text='<%# Eval("vchQuerytype") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="9%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="Hdnstatus" runat="server" Value='<%# Eval("intStatus") %>' />
                                                            <asp:Label ID="Lbl_Status" runat="server" Text='<%# Eval("vchQueryStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Remarks" runat="server" Text='<%# Eval("vchRemarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Query Date" ItemStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_CreatedOn" runat="server" Text='<%# Eval("CreatedDate" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Query Doc " ItemStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="HdnQueryFile" runat="server" Value='<%#Eval("vchFileName") %>'></asp:HiddenField>
                                                            <asp:LinkButton ID="LnkBtnQueryFile" OnClick="LnkBtnQueryFile_Click" runat="server" ><i class="fa fa-download" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:Label ID="LblQueryFile" runat="server" Visible="false" Text='<%# Eval("vchFileName") %>' ForeColor="Red"></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
    <script type="text/javascript" language="javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function ValidateApplicationKey() {
            if (!blankFieldValidation('TxtPcNumberID', 'PC No. ', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('TxtPcNumberID', 'PC No.', projname)) {
                $("#popup_ok").click(function () { $("#TxtPcNumberID").focus(); });
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

