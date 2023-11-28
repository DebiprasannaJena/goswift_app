<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewEMPCommitteApproval.aspx.cs"
    MasterPageFile="~/MasterPage/Application.master" Inherits="Portal_Incentive_ViewEMPCommitteApproval" %>

<%@ Register Src="~/Portal/Include/PagingUserControl.ascx" TagName="PagingUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"
    ClientIDMode="Static">
     <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
        <div class="header-icon">
            <i class="fa fa-dashboard"></i>
        </div>
        <div class="header-title">
            <h1>
                Empowered Committee Approval</h1>
            <ul class="breadcrumb">
                <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                <li><a>View Delay Reason</a></li></ul>
        </div>
        </section>
        <!-- Main content -->
        <section class="content">
        <div class="row">
            <!-- Form controls -->
            <div class="col-sm-12">
                <div class="panel panel-bd lobidisable">
                    <div class="panel-body">
                        <div class="search-sec">
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-md-2 col-sm-3">
                                        Enterprise Name</label>
                                    <div class="col-md-3 col-sm-3">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_EntName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 col-sm-3">
                                        Status</label>
                                    <div class="col-md-3 col-sm-3">
                                        <span class="colon">:</span>
                                        <asp:DropDownList ID="DrpDwn_Status" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Pending</asp:ListItem>
                                            <asp:ListItem Value="2">Approved</asp:ListItem>
                                            <asp:ListItem Value="3">Rejected</asp:ListItem>
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
                            <asp:GridView ID="Grd_Application" runat="server" AutoGenerateColumns="false" class="table table-bordered table-hover"
                                OnRowDataBound="Grd_Application_RowDataBound"
                                PagerStyle-CssClass="pagination-grid" DataKeyNames="intDelayId">
                                <Columns>
                                    <asp:TemplateField HeaderText="SLNo.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>                                        
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="vchEnterpriseName" HeaderText="Enterprise Name"  />
                                    <asp:BoundField DataField="vchUnitCat" HeaderText="Unit Category" ItemStyle-Width="12%" />
                                    <asp:TemplateField HeaderText="Applied On">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbl_Created_On_G" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                        <asp:HiddenField ID="Hid_Status_G" runat="server" Value='<%# Eval("intStatus") %>'></asp:HiddenField>
                                            <asp:Label ID="Lbl_Status_G" runat="server" Text='<%# Eval("vchStatus" ) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:TemplateField>                                   
                                    <asp:TemplateField HeaderText="View Details" HeaderStyle-CssClass="noPrint" FooterStyle-CssClass="noPrint">
                                        <ItemTemplate>
                                            <a  href="#" onclick='openWindow("<%# Eval("intDelayId") %>");'
                                                class="btn btn-success btn-sm"><i class='fa fa-eye' aria-hidden='true'></i></a>
                                        </ItemTemplate>                                     
                                        <ItemStyle  Width="10%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="noPrint" FooterStyle-CssClass="noPrint">
                                        <ItemTemplate>
                                        <asp:LinkButton ID="LnkBtn_Take_Action" runat="server"  OnClientClick='<%# Eval("intDelayId","return redirect({0})")%>' class="btn btn-success btn-sm">TAKE ACTION</asp:LinkButton>
                                           
                                        </ItemTemplate>                                      
                                        <ItemStyle Width="6%"  HorizontalAlign="Center" />
                                    </asp:TemplateField>                                     
                                </Columns>
                                <EmptyDataTemplate>
                                    No Record Found
                                </EmptyDataTemplate>
                                <EmptyDataRowStyle ForeColor="Red" />
                            </asp:GridView>
                         <%--   <asp:Button ID="btnDownload" runat="server" Text="Download" Style="display: none"
                                OnClick="btnDownload_Click" />--%>
                            <asp:HiddenField ID="hdnFileNames" runat="server" />
                        </div>
                        <div style="float: right;" class="noPrint" id="divPaging" runat="server" visible="false">
                            <uc1:PagingUserControl ID="uclPager" runat="server" />
                            <asp:HiddenField ID="hdnCurrentIndex" runat="server" Value="Blank Value" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </section>
    </div>
    <script language="javascript" type="text/javascript">
        function openWindow(code) {
            window.open('ViewDelayReasonDetails.aspx?Did=' + code, 'open_window', ' width=1050, height=600, left=0, top=0,scrollbars=yes');
        }

        function redirect(id) {
            document.location.href = 'ApproveDelayReason.aspx?Did=' + id + '&linkm=' + '<%=Request.QueryString["linkm"]%>' + '&linkn=' + '<%=Request.QueryString["linkn"]%>' + '&btn=' + '<%= Request.QueryString["btn"] %>' + '&tab=' + '<%= Request.QueryString["tab"] %>';
            return false;
        }
    </script>
</asp:Content>
