<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="GM_Details_View.aspx.cs" Inherits="Portal_AMS_GM_Details_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ExModal() {
            $('#ContentPlaceHolder1_slfcModal').modal();
        }
    </script>
    <div class="content-wrapper">
        <asp:ScriptManager ID="sm1" runat="server">
        </asp:ScriptManager>
        <asp:HiddenField ID="hdnAction" runat="server" />
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    Query raised by AMS
                </h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>AMS</a></li><li><a>View</a></li></ul>
            </div>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-body">
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <div style="text-align: right; width: 100%; text-align: right;" class="noprint pagelist">
                                            <asp:Label ID="lblDetails" runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlNoOfRec" ToolTip="Page Size" Width="80px" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRec_SelectedIndexChanged"
                                                CssClass="form-control" Style="display: inline">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:GridView ID="grdQuery" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found..."
                                        OnRowDataBound="grdQuery_RowDataBound" CssClass="table table-bordered table-hover"
                                        DataKeyNames="intConfigId">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl#">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Proposal No">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hypProposalNo" runat="server" Text='<%#Eval("strProposalNo") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Investor name" DataField="strInvName" />
                                            <asp:BoundField HeaderText="Applied Date" DataField="strInvAppliedDate" />
                                            <asp:BoundField HeaderText="Forwarded Date" DataField="strForwardedDate" />
                                            <asp:BoundField HeaderText="No of Time" DataField="strQueryTime" />
                                            <asp:TemplateField HeaderText="SLFC Details">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hypDetails" runat="server" ToolTip="Edit Queries added by Nodal and SLFC"
                                                        Text="Details">
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="container">
                                <asp:UpdatePanel ID="updModal" runat="server">
                                    <ContentTemplate>
                                        <div id="slfcModal" role="dialog" class="modal fade" runat="server">
                                            <div class="modal-dialog modal-lg">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <div style="text-align: left;">
                                                            <h4 class="modal-title">
                                                                SLFC Details of Proposal :
                                                                <asp:Label ID="lblProposalNo" runat="server"></asp:Label></h4>
                                                        </div>
                                                        <div style="float: right; margin-top: -16px">
                                                            <a href="#" class="close" data-dismiss="modal"><i class="fa fa-times"></i></a>
                                                        </div>
                                                    </div>
                                                    <div class="modal-body">
                                                        <asp:GridView ID="grdSLFC" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found..."
                                                            OnRowDataBound="grdSLFC_RowDataBound" CssClass="table table-bordered table-hover"
                                                            DataKeyNames="intUserId">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SL#">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="User" DataField="strUsername" />
                                                                <asp:BoundField HeaderText="Remarks" DataField="strRemarks" />
                                                                <asp:TemplateField HeaderText="Files">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdnFile" runat="server" Value='<%#Eval("strFileName") %>' />
                                                                        <asp:HyperLink ID="hypFile" runat="server" Target="_blank" ToolTip="View Document"
                                                                            Visible="false">
                                                                        <i class="fa fa-download"></i>
                                                                        </asp:HyperLink>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <br />
                                                        <div class="form-group">
                                                            <label class="col-md-2">
                                                                Investor Response Date</label>
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblResponseDate" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="clearfix">
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2">
                                                                Investor Response Date</label>
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblInvReply" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="clearfix">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
