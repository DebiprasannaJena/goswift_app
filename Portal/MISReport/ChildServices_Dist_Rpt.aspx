<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ChildServices_Dist_Rpt.aspx.cs" Inherits="Portal_MISReport_ChildServices_Dist_Rpt"
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
            $(document).ready(function () {
                $('.datePicker').datepicker({
                    format: "dd-M-yyyy",
                    changeMonth: true,
                    changeYear: true,
                    autoclose: true
                });
            });

            function setDateValues(strFromDate, strToDate) {
                var appendId = "ContentPlaceHolder1_";
                var fromDate = $("#" + appendId + "txtFromDate").val();
                var toDate = $("#" + appendId + "txtToDate").val();
                $("#" + appendId + "txtFromDate").datepicker({
                    format: "dd-M-yyyy",
                    changeMonth: true,
                    changeYear: true,
                    autoclose: true
                }).datepicker("setDate", fromDate);
                $("#" + appendId + "txtToDate").datepicker({
                    format: "dd-M-yyyy",
                    changeMonth: true,
                    changeYear: true,
                    autoclose: true
                }).datepicker("setDate", toDate);
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
                                    <div class="search-sec NOPRINT">
                                        <div class="form-group row NOPRINT">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-3">
                                                        <label for="Department">
                                                            Department</label>
                                                        <asp:DropDownList ID="drpDepartment" runat="server" AutoPostBack="true" CssClass="form-control"
                                                            OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged">
                                                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <label for="Service">
                                                            Service</label>
                                                        <asp:DropDownList ID="drpService" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="col-sm-3">
                                                <label for="State">
                                                    From Date
                                                </label>
                                                <div class="input-group  date datePicker">
                                                    <asp:TextBox runat="server" class="form-control" ID="txtFromDate" name="txtFromDate"></asp:TextBox>
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="State">
                                                    To Date
                                                </label>
                                                <div class="input-group  date datePicker">
                                                    <asp:TextBox runat="server" class="form-control" ID="txtToDate"></asp:TextBox>
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row NOPRINT">
                                            <div class="col-sm-3">
                                                <asp:Button ID="btnSearch" Style="margin-top: 22px" CssClass="btn btn btn-add btn-sm"
                                                    runat="server" Text="Search" OnClick="btnSearch_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <asp:Label ID="lblSearchDetails" runat="server"></asp:Label>
                                        <asp:GridView ID="grdDistrict" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
                                            DataKeyNames="intKey" CssClass="table table-bordered table-hover" OnRowDataBound="grdDistrict_RowDataBound"
                                            ShowFooter="true" OnRowCommand="grdDistrict_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="District" DataField="strDeptName" FooterStyle-Font-Bold="true" />
                                                <asp:TemplateField HeaderText="Total Application" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                                    FooterStyle-HorizontalAlign="Right">
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
                                                        <asp:Label ID="lblApproved" runat="server" Text='<%#Eval("intTotalApproved")%>' Visible="false"></asp:Label>
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
                                                <asp:TemplateField HeaderText="Pending" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                                    FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPending" runat="server" Text='<%#Eval("intTotalPending")%>'
                                                            Visible="false" CommandName="D" CommandArgument="1"></asp:LinkButton>
                                                        <asp:Label ID="lblPending" runat="server" Text='<%#Eval("intTotalPending")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Carry Forward Pending" FooterStyle-Font-Bold="true"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkCarryFwdPending" runat="server" Text='<%#Eval("intCarryFwdPending")%>'
                                                            Visible="false" CommandName="D" CommandArgument="8"></asp:LinkButton>
                                                        <asp:Label ID="lblCarryFwdPending" runat="server" Text='<%#Eval("intCarryFwdPending")%>'
                                                            Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Application passed ORTPS Timeline" FooterStyle-Font-Bold="true"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkORTPS" runat="server" Text='<%#Eval("intTotalORTPSAtimelinePassed")%>'
                                                            Visible="false" CommandName="D" CommandArgument="4"></asp:LinkButton>
                                                        <asp:Label ID="lblORTPS" runat="server" Text='<%#Eval("intTotalORTPSAtimelinePassed")%>'
                                                            Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rejected" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                                    FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkRejected" runat="server" Text='<%#Eval("intTotalRejected")%>'
                                                            Visible="false" CommandName="D" CommandArgument="3"></asp:LinkButton>
                                                        <asp:Label ID="lblRejected" runat="server" Text='<%#Eval("intTotalRejected")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Avg. No. of days for approval" FooterStyle-Font-Bold="true"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <%#Eval("intAvgDaysApproval")%>
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
