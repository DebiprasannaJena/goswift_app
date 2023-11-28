<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ChildServiceRptDtls.aspx.cs" Inherits="Portal_MISReport_ChildServiceRptDtls"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <script src="../js/custom.js" type="text/javascript"></script>
        <script type="text/javascript">
            function ViewModal(ModalPath) {
                $('#DetailsModal').modal();
                $('#DetailsModal .modal-body iframe').attr('src', ModalPath);
            }
        </script>
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    MIS Report on Services
                </h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Proposal</a></li><li><a>View</a></li></ul>
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
                                        <asp:LinkButton ID="lnkPdf" CssClass="back" runat="server" title="Export to PDF"
                                            OnClick="lnkPdf_Click"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                    <li><a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                                        <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" title="Export to Excel"
                                            OnClick="lnkExport_Click"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
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
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblSearchDetails" runat="server"></asp:Label>
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdDepartment" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
                                            DataKeyNames="intKey" CssClass="table table-bordered table-hover" OnRowDataBound="grdDepartment_RowDataBound"
                                            ShowFooter="true" OnRowCommand="grdDegrdDepartment_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Service" DataField="strDeptName" FooterStyle-Font-Bold="true" />
                                                <asp:TemplateField HeaderText="Opening Balance" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                                    FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkCarryFwdPending" runat="server" Text='<%#Eval("intCarryFwdPending")%>'
                                                            Visible="false" CommandName="D" CommandArgument="15"></asp:LinkButton>
                                                        <asp:Label ID="lblCarryFwdPending" runat="server" Text='<%#Eval("intCarryFwdPending")%>'
                                                            Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Application received in current period" FooterStyle-Font-Bold="true"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTotalApplication" runat="server" Text='<%#Eval("intTotalApplication")%>'
                                                            Visible="false" CommandName="D" CommandArgument="0"></asp:LinkButton>
                                                        <asp:Label ID="lblTotalApplication" runat="server" Text='<%#Eval("intTotalApplication")%>'
                                                            Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approved" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                                    FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkApproved" runat="server" Text='<%#Eval("intTotalApproved")%>'
                                                            Visible="false" CommandName="D" CommandArgument="2"></asp:LinkButton>
                                                        <asp:Label ID="lblApproved" style="color:green;font-weight:bold;" runat="server" Text='<%#Eval("intTotalApproved")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rejected" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                                    FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkRejected" runat="server" Text='<%#Eval("intTotalRejected")%>'
                                                            Visible="false" CommandName="D" CommandArgument="3"></asp:LinkButton>
                                                        <asp:Label ID="lblRejected" style="color:red;font-weight:bold;" runat="server" Text='<%#Eval("intTotalRejected")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. of Application with Query" FooterStyle-Font-Bold="true"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkQuery" runat="server" Text='<%#Eval("intTotalQueryRaised")%>'
                                                            Visible="false" CommandName="Q" CommandArgument="0"></asp:LinkButton>
                                                        <asp:Label ID="lblQuery" runat="server" Text='<%#Eval("intTotalQueryRaised")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total application pending in current period" FooterStyle-Font-Bold="true"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPending" runat="server" Text='<%#Eval("intTotalPending")%>'
                                                            Visible="false" CommandName="D" CommandArgument="1"></asp:LinkButton>
                                                        <asp:Label ID="lblPending" style="color:violet;font-weight:bold;" runat="server" Text='<%#Eval("intTotalPending")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Applications pending" FooterStyle-Font-Bold="true"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAllPending" runat="server" Text='<%#Eval("intAllTotalPending")%>'
                                                            Visible="false" CommandName="D" CommandArgument="9"></asp:LinkButton>
                                                        <asp:Label ID="lblAllPending" style="color:violet;font-weight:bold;" runat="server" Text='<%#Eval("intAllTotalPending")%>'
                                                            Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Application passed ORTPS Timeline" FooterStyle-Font-Bold="true"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkORTPS" runat="server" Text='<%#Eval("intTotalORTPSAtimelinePassed")%>'
                                                            Visible="false" CommandName="D" CommandArgument="4"></asp:LinkButton>
                                                        <asp:Label ID="lblORTPS" style="color:orange;font-weight:bold;" runat="server" Text='<%#Eval("intTotalORTPSAtimelinePassed")%>'
                                                            Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Avg. No. of days for approval" FooterStyle-Font-Bold="true"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <%#Eval("intAvgDaysApproval")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  
                                       

                                          <asp:TemplateField HeaderText="Deferred" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                                    FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDeferred" runat="server" Text='<%#Eval("intTotalDeferred")%>'
                                                            Visible="false" CommandName="D" CommandArgument="7"></asp:LinkButton>
                                                        <asp:Label ID="lblDeferred" style="color:dodgerblue;font-weight:bold;" runat="server" Text='<%#Eval("intTotalDeferred")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Forwarded" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                                    FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkForwarded" runat="server" Text='<%#Eval("intTotalForwarded")%>'
                                                            Visible="false" CommandName="D" CommandArgument="8"></asp:LinkButton>
                                                        <asp:Label ID="lblForwarded" runat="server" Text='<%#Eval("intTotalForwarded")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkPdf" />
                                    <asp:PostBackTrigger ControlID="lnkExport" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <asp:UpdatePanel ID="updModal" runat="server">
        <ContentTemplate>
            <div id="DetailsModal" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content modal-primary ">
                        <div class="modal-header bg-red">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="lbldet1" runat="server" Text=""></asp:Label></h4>
                        </div>
                        <div class="modal-body" style="height: 500px;">
                            <iframe name="myIframe" id="myIframe" width="100%" style="height: 500px;" runat="server">
                            </iframe>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
