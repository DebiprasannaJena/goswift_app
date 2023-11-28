<%--'*******************************************************************************************************************
' File Name         : View_Incentive_Name_Master.aspx
' Description       : View Incentive Name
' Created by        : Sushant Kumar Jena
' Created On        : 07th Sept 2017
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="View_Incentive_Name_Master.aspx.cs" Inherits="Portal_Incentive_View_Incentive_Name_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Portal/Include/PagingUserControl.ascx" TagName="PagingUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function checkvalidation() {
            var checkedCheckboxes = $("#<%=Grd_Inct_Details.ClientID%> input[id*='chkSelectSingle']:checkbox:checked").size();
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>View Incentive Name Master</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Incentive Policy</a></li><li><a>Add Incentive</a></li></ul>
               </div>
            </section>
        <div class="panel panel-bd lobidisable">
            <div class="panel-heading">
                <div class="btn-group buttonlist">
                    <a class="btn btn-add " href="Incentive_Name_Master.aspx"><i class="fa fa-plus"></i>
                        Add </a>
                </div>
                <div class="btn-group buttonlist">
                    <a class="btn btn-add " href="View_Incentive_Name_Master.aspx"><i class="fa fa-file">
                    </i>View </a>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="udpDiv" runat="server">
            <ContentTemplate>
                <div class="ibox-content">
                    <div class="clearfix">
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <label class="col-sm-2 col-sm-offset-1">
                                OG Name
                            </label>
                            <div class="col-sm-4">
                                <span class="colon">:</span>
                                <asp:DropDownList ID="DrpDwn_OG_Name" runat="server" CssClass="form-control" OnSelectedIndexChanged="DrpDwn_OG_Name_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <label class="col-sm-2 col-sm-offset-1">
                                Incentive Name
                            </label>
                            <div class="col-sm-4">
                                <span class="colon">:</span>
                                <asp:DropDownList ID="DrpDwn_Incentive_Name" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <label class="col-sm-2 col-sm-offset-1">
                        </label>
                        <div class="col-sm-4">
                            <asp:Button ID="Btn_Search" runat="server" Text="Search" class="btn btn-primary"
                                OnClick="Btn_Search_Click" />
                            <asp:HiddenField ID="Hid_Inct_Id" runat="server" />
                        </div>
                    </div>
                </div>
                <div id="divPagingShow" runat="server" class="noPrint pull-right" visible="false">
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
                    <asp:GridView ID="Grd_Inct_Details" runat="server" AutoGenerateColumns="false" class="table table-bordered table-hover"
                        OnRowDataBound="Grd_Inct_Details_RowDataBound">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-CssClass="no-print" ItemStyle-CssClass="no-print">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" ToolTip="Check All" class="noPrint" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelectSingle" runat="server" ToolTip="Check" class="noPrint RowCheck" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sl No">
                                <ItemTemplate>
                                    <asp:Label ID="Lbl_Sl_No" runat="server" Text='<%# Eval("Serial") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Incentive Name">
                                <ItemTemplate>
                                    <asp:Label ID="Lbl_Incentive_Name" runat="server" Text='<%# Eval("vchInctName") %>'></asp:Label>
                                    <asp:HiddenField ID="Hid_Inct_Id_G" runat="server" Value='<%# Eval("intInctId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OG Name">
                                <ItemTemplate>
                                    <asp:Label ID="Lbl_OG_Name" runat="server" Text='<%# Eval("vchOGName") %>'></asp:Label>
                                    <asp:HiddenField ID="Hid_OG_Id_G" runat="server" Value='<%# Eval("intOGId") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="12%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Disburement Type">
                                <ItemTemplate>
                                    <asp:Label ID="Lbl_Disburse_Type" runat="server" Text='<%# Eval("vchDisburseType") %>'></asp:Label>
                                    <asp:HiddenField ID="Hid_Disburse_Type_G" runat="server" Value='<%# Eval("intDisburseType") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="13%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Availability">
                                <ItemTemplate>
                                    <asp:Label ID="Lbl_Avail" runat="server" Text='<%# Eval("vchAvailType") %>'></asp:Label>
                                    <asp:HiddenField ID="Hid_Avail_G" runat="server" Value='<%# Eval("intAvailType") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="12%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nature">
                                <ItemTemplate>
                                    <asp:Label ID="Lbl_Nature" runat="server" Text='<%# Eval("vchInctNature") %>'></asp:Label>
                                    <asp:HiddenField ID="Hid_Nature_G" runat="server" Value='<%# Eval("intInctNature") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time Frame">
                                <ItemTemplate>
                                    <asp:Label ID="Lbl_Time_Frame" runat="server" Text='<%# Eval("intTimeFrame") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="9%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Periodicity">
                                <ItemTemplate>
                                    <asp:Label ID="Lbl_Periodicity" runat="server" Text='<%# Eval("vchPeriodicity") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Max Limit">
                                <ItemTemplate>
                                    <asp:Label ID="Lbl_Max_Limit" runat="server" Text='<%# Eval("intMaxLimit") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkBtn_Edit" class="btn btn-add btn-sm" runat="server" OnClick="LnkBtn_Edit_Click"><i class="fa fa-pencil"></i></asp:LinkButton>
                                    <asp:HiddenField ID="Hid_Del_Flag" runat="server" Value='<%# Eval("chrDelFlag") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:HiddenField ID="hdnCurrentIndex" runat="server" Value="Blank Value" />
                    <asp:Button ID="Btn_Delete" runat="server" Text="Delete" OnClick="Btn_Delete_Click"
                        class="btn btn-danger" OnClientClick="return checkvalidation();" />
                </div>
                <div style="float: right;" class="noPrint" id="divPaging" runat="server" visible="false">
                    <uc1:PagingUserControl ID="uclPager" runat="server" />
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="Blank Value" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
