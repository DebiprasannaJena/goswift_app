<%--'*******************************************************************************************************************
' File Name         : ViewIncentiveDetailsRpt.aspx
' Description       : To show the details of the incentive application as per the link clicked in the dashboard
' Created by        : Ritika lath
' Created On        : 11th December 2017
' Modification History:
'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                 
'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ViewIncentiveDetailsRpt.aspx.cs" Inherits="Portal_Incentive_ViewIncentiveDetailsRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"
    ClientIDMode="Static">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../js/custom.js" type="text/javascript"></script>
    <div class="content-wrapper">
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    View Incentive Details</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>DashBoard</a></li><li><a>Incentive Details</a></li></ul>
            </div>
        </section>
        <section class="content">
            <div class="row">
                <!-- Form controls -->
                <div class="col-sm-12">
                    <!--Main outer Div-->
                    <div class="panel panel-bd lobidisable">
                        <!--Panel body div-->
                        <div class="panel-body">
                            <!--Search Div-->
                            <div class="search-sec NOPRINT">
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-md-2 col-sm-3">
                                            Application No</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtAppNo" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <label class="col-md-2 col-sm-3">
                                            Status</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="drpStatus" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-md-2 col-sm-3">
                                            Unit Type</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="drpUnitType" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-md-2 col-sm-3">
                                            Incentive Type</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="drpIncentiveType" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-md-2 col-sm-3">
                                            Year</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="drpYear" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-md-2 col-sm-3">
                                            District</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="drpDistrict" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-md-2 col-sm-3">
                                            Unit Name</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtUnitName" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <label class="col-md-2 col-sm-3">
                                            Unit Priority</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:RadioButtonList ID="rdBtnLstPriority" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                                RepeatLayout="Table">
                                                <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-md-2 col-sm-3">
                                            Policy</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="drpPolicy" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success"
                                                OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--Search Div-->
                            <!--Gridview-->
                            <div class="table-responsive">
                                <div>
                                    <div style="text-align: right; width: 100%; text-align: right;" class="noprint pagelist">
                                        <asp:Label ID="lblDetails" runat="server"></asp:Label>
                                        <asp:DropDownList ID="ddlNoOfRec" ToolTip="Page Size" Width="80px" runat="server"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRec_SelectedIndexChanged"
                                            CssClass="form-control" Style="display: inline">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lnkExport" runat="server" OnClick="lnkExport_Click" CssClass="btn btn-success"
                                            ToolTip="Export to Excel" Visible="false"><i class="panel-control-icon fa fa-file-excel-o"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkPdf" runat="server" OnClick="lnkPdf_Click" CssClass="btn btn-success"
                                            ToolTip="Export to PDF" Visible="false"><i class="panel-control-icon fa fa-file-pdf-o"></i></asp:LinkButton>
                                        <a class="PrintBtn btn btn-success" data-tooltip="Print" data-toggle="tooltip" data-title="Print"
                                            visible="false" runat="server" id="ancPrint"><i class="panel-control-icon fa fa-print">
                                            </i></a>
                                    </div>
                                </div>
                                <div style="text-align: left; width: 100%;" class="noprint pagelist" id="divIncentiveDetails"
                                    runat="server">
                                    <strong>
                                        <asp:Label ID="lblSearchDetails" runat="server"></asp:Label></strong>
                                    <br />
                                    <br />
                                    <asp:GridView ID="grdIncentive" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdIncentive_RowDataBound"
                                        CssClass="table table-bordered table-hover" EmptyDataText="No records found....">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Application No." DataField="strApplicationNum" ItemStyle-Width="10%" />
                                            <asp:BoundField HeaderText="Unit Name" DataField="strUnitName" ItemStyle-Width="15%" />
                                            <asp:BoundField HeaderText="Incentive Name" DataField="strInctName" ItemStyle-Width="25%" />
                                            <asp:BoundField HeaderText="Sector Name" DataField="strSectorName" ItemStyle-Width="15%" />
                                            <asp:BoundField HeaderText="Unit Priority" DataField="strPriority" ItemStyle-Width="5%" />
                                            <asp:BoundField HeaderText="Unit Type" DataField="strUnitType" ItemStyle-Width="5%" />
                                            <asp:BoundField HeaderText="Applied On" DataField="strAppliedOn" ItemStyle-Width="10%" />
                                            <asp:BoundField HeaderText="Status" DataField="strStatus" ItemStyle-Width="10%" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div style="float: right;" class="noPrint" id="divPaging" runat="server">
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>
                                            <ul class="pagination">
                                                <li class='<%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %> '>
                                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                        OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                                </li>
                                            </ul>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <%--   <asp:HiddenField ID="HiddenField1" runat="server" Value="Blank Value" />--%>
                                    <asp:HiddenField ID="hdnPgindex" runat="server" Value="Blank Value" />
                                </div>
                            </div>
                            <!--Gridview-->
                        </div>
                        <!--Panel body div-->
                    </div>
                    <!--Main outer Div-->
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
