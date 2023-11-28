<%--'*******************************************************************************************************************
' File Name         : ViewInvestorDetails.aspx
' Description       : View details of Investor data
' Created by        : AMit Sahoo
' Created On        : 12 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="SectorView.aspx.cs"
    Inherits="Incentive_SectorView" %>

<%@ Register Src="~/Portal/Include/PagingUserControl.ascx" TagName="PagingUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script type="text/javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function checkvalidation() {
            var checkedCheckboxes = $("#<%=gvsector.ClientID%> input[id*='chkSelectSingle']:checkbox:checked").size();
            if (checkedCheckboxes > 0) {
                jConfirm('Are you sure you want to Delete?', projname, function (callback) {
                    if (callback) {
                        __doPostBack("<%= Btn_Delete.UniqueID %>", "");

                    } else {
                        return false;
                    }
                });
                return false;
            }
            else {
                jAlert("<strong>Please select atleast one record to delete !!</strong>", projname);
                return false;
            }
        }


        function pageLoad() {
            CheckUncheckGrid();
        }

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <%-- <section class="content-header">--%>
                <div class="content-header">
                    <div class="header-icon">
                        <i class="fa fa-dashboard"></i>
                    </div>
                    <div class="header-title">
                        <h1>
                            Manage Sector</h1>
                        <ul class="breadcrumb">
                            <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                            <li><a>Manage Sector</a></li><li><a>Sector</a></li></ul>
                    </div>
                </div>
                <%--</section>--%>
                <!-- Main content -->
                <%--  <section class="content">--%>
                <div class="content">
                    <div class="row">
                        <!-- Form controls -->
                        <div class="col-sm-12">
                            <div class="panel panel-bd lobidisable">
                                <div class="panel-heading">
                                    <div class="btn-group buttonlist">
                                        <a class="btn btn-add " href="Sector_Manage.aspx"><i class="fa fa-plus"></i>Add</a>
                                    </div>
                                    <div class="btn-group buttonlist">
                                        <a class="btn btn-add " href="SectorView.aspx"><i class="fa fa-file"></i>View</a>
                                    </div>
                                </div>
                                <div id="divPagingShow" runat="server" class="noPrint pull-right" visible="false">
                                    <%--  <div align="right">
                                <asp:LinkButton ID="lnkExport" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkExport_Click">Export To Excel<i class="fa fa-file-excel-o" aria-hidden="true"></i></asp:LinkButton>
                            </div>--%>
                                    <span class="text-muted">
                                        <asp:Literal ID="litStart" runat="server">1</asp:Literal>
                                        -
                                        <asp:Literal ID="litEnd" runat="server">10</asp:Literal>
                                        of
                                        <asp:Literal ID="litTotalRecord" runat="server"></asp:Literal>
                                        <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSize_SelectedIndexChanged">
                                            <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="500">500</asp:ListItem>
                                            <asp:ListItem Value="2147483647">All</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                    <asp:HiddenField ID="hdnTotalCount" runat="server" />
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvsector" runat="server" class="table table-bordered table-hover"
                                        AutoGenerateColumns="False" EmptyDataText="No Record(s) Found" CellPadding="4"
                                        DataKeyNames="intSecTagId" GridLines="None">
                                        <Columns>
                                            <asp:BoundField HeaderText="Sl#" DataField="Serial" HeaderStyle-Width="2%" ItemStyle-Width="2%" />
                                            <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-CssClass="no-print" ItemStyle-CssClass="no-print">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" ToolTip="Check All" class="noPrint" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelectSingle" runat="server" ToolTip="Check" class="noPrint RowCheck" />
                                                </ItemTemplate>
                                                <ItemStyle Width="2%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Policy Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Policy_Name" runat="server" Text='<%# Eval("vchPlcName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sector Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Sector_Name" runat="server" Text='<%# Eval("VCH_SECTOR") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Sector Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Sub_Sector_Name" runat="server" Text='<%# Eval("vchSubSectorName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Derived Sector">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Derived_Sector" runat="server" Text='<%# Eval("vchDesc") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is SectorPolicy">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPolicy" runat="server" Text='<%# Eval("bitSectoralPolicy") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="11%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is ProrityIPR">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblb" runat="server" Text='<%# Eval("bitPriorityIPR") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="noPrint" FooterStyle-CssClass="noPrint"
                                                ItemStyle-CssClass="noPrint">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" OnCommand="lbtnEdit_Command" CssClass="btn btn-xs bigger btn-primary"
                                                        ToolTip="Edit" CommandArgument='<%#Eval("intSecTagId") %>' CommandName="Edt"> <i class="ace-icon fa fa-pencil-square-o icon-only bigger-110"></i></asp:LinkButton>
                                                    <asp:HiddenField ID="hdnCnt" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblMessage" runat="server" Text="No Records Found"></asp:Label>
                                    <asp:HiddenField ID="hdnCurrentIndex" runat="server" Value="Blank Value" />
                                </div>
                                <div style="float: right;" class="noPrint" id="divPaging" runat="server" visible="false">
                                    <uc1:PagingUserControl ID="uclPager" runat="server" />
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value="Blank Value" />
                                </div>
                                <div class="form-group">
                                    <div class="row ">
                                        <div class="col-sm-2 col-sm ">
                                            <asp:Button class="btn btn-danger" ID="Btn_Delete" runat="server" Text="Delete" type="submit"
                                                OnClick="Btn_Delete_Click" OnClientClick="return checkvalidation();"></asp:Button></div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
                <%--</section>--%>
                <!-- /.content -->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
