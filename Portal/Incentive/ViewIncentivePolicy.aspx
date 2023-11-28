<%--'*******************************************************************************************************************
' File Name         : ViewIncentivePolicy.aspx
' Description       : View Incentive Policy
' Created by        : AMit Sahoo
' Created On        : 13 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ViewIncentivePolicy.aspx.cs" Inherits="Master_ViewIncentive" %>

<%@ Register Src="~/Portal/Include/PagingUserControl.ascx" TagName="PagingUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"
    ClientIDMode="Static">
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <div class="content-wrapper">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <link href="../chosen/chosen.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="../chosen/chosen.jquery.js"></script>
        <!-- Content Header (Page header) -->
        <%-- <section class="content-header">--%>
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    Manage Incentive Policy</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Incentive Policy</a></li><li><a>View Incentive</a></li></ul>
            </div>
        </div>
        <%--  </section>--%>
        <!-- Main content -->
        <asp:UpdatePanel ID="udpDiv" runat="server">
            <ContentTemplate>
                <%--  <section class="content">--%>
                <div class="content">
                    <div class="row">
                        <!-- Form controls -->
                        <div class="col-sm-12">
                            <div class="panel panel-bd lobidisable">
                                <div class="panel-heading">
                                    <div class="btn-group buttonlist">
                                        <a class="btn btn-add " href="AddIncentivePolicy.aspx"><i class="fa fa-plus"></i>Add
                                        </a>
                                    </div>
                                    <div class="btn-group buttonlist">
                                        <a class="btn btn-add " href="ViewIncentivePolicy.aspx"><i class="fa fa-file"></i>View
                                        </a>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="search-sec">
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-md-2 col-sm-3">
                                                    Policy Name</label>
                                                <div class="col-md-4 col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:DropDownList ID="DrpDwn_Policy_Name" CssClass="form-control" runat="server"
                                                        Width="330px">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2 col-sm-2">
                                                    <asp:Button ID="Btn_Search" runat="server" Text="Search" CssClass="btn btn-success"
                                                        OnClick="Btn_Search_Click" />
                                                </div>
                                            </div>
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
                                        <asp:GridView ID="Grd_Sector" runat="server" class="table table-bordered table-hover"
                                            OnRowCommand="Grd_Sector_RowCommand" AutoGenerateColumns="False" DataKeyNames="intPlcId"
                                            CellPadding="4" OnRowDataBound="RowDataBound" GridLines="None">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-CssClass="no-print" ItemStyle-CssClass="no-print">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" ToolTip="Check All" class="noPrint" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelectSingle" runat="server" ToolTip="Check" class="noPrint RowCheck" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Sl#" DataField="Serial" HeaderStyle-Width="2%" ItemStyle-Width="2%" />
                                                <asp:TemplateField HeaderText="Policy Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label0" runat="server" Text='<%# Eval("vchPolicyCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Policy Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("vchPlcName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Policy Effective Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("dtmEffectiveDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sector Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Sector_Name" runat="server" Text='<%# Eval("VCH_SECTOR") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sub Sector Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Sub_Sector_Name" runat="server" Text='<%# Eval("vchSubSectorName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Policy Doc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <span id="spnNoFile" runat="server" title="no file"><i class="fa fa-ban btn-xs btn-danger">
                                                        </i></span><a id="ancDwld" runat="server" title="Download Document" target="_blank">
                                                            <i class="fa fa-download btn-xs btn-success "></i></a>
                                                        <asp:HiddenField ID="hdnView" runat="server" Value='<%#Eval("vchPlcDoc")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Policy Amendment Doc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <span id="spnNoFileAmd" runat="server" title="no file"><i class="fa fa-ban btn-xs btn-danger">
                                                        </i></span><a id="ancDwldAmd" runat="server" title="Download Document" target="_blank">
                                                            <i class="fa fa-download btn-xs btn-success "></i></a>
                                                        <asp:HiddenField ID="hdnViewAmd" runat="server" Value='<%#Eval("vchAmendmentDoc")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="9%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnEdit" runat="server" CssClass="btn btn-xs bigger btn-primary"
                                                            ToolTip="Edit" CommandArgument='<%#Eval("intPlcId") %>' CommandName="Edt"> <i class="ace-icon fa fa-pencil-square-o icon-only bigger-110"></i></asp:LinkButton>
                                                        <asp:HiddenField ID="hdnCnt" runat="server" />
                                                        <asp:HiddenField ID="Hid_Del_Flag" runat="server" Value='<%# Eval("chrDelFlag") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" />
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- </section>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';
        function pageLoad() {
            CheckUncheckGrid();
            $('#DrpDwn_Policy_Name').chosen({ allow_single_deselect: true, no_results_text: 'No Item found for ' });
        }

        function checkvalidation() {
            var checkedCheckboxes = $("#<%=Grd_Sector.ClientID%> input[id*='chkSelectSingle']:checkbox:checked").size();
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
    </script>
    <script type="text/javascript">
        function pageLoad() {
            CheckUncheckGrid();
        }
    </script>
</asp:Content>
