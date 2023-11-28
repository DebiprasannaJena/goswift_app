<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ChildServiceRpt_New.aspx.cs" Inherits="Portal_MISReport_ChildServiceRpt_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <script src="../js/custom.js" type="text/javascript"></script>
        <script type="text/javascript">
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


            function ValidatePage() {
                debugger;
                var fDate = $("#ContentPlaceHolder1_txtFromDate").val();
                var tDate = $("#ContentPlaceHolder1_txtToDate").val();
                if (fDate == null || fDate == undefined || fDate == '') {
                    jAlert('<strong>Please select from date.</strong>', 'GO-SWIFT');
                    return false;
                }
                if (tDate == null || tDate == undefined || tDate == '') {
                    jAlert('<strong>Please select to date.</strong>', 'GO-SWIFT');
                    return false;
                }
                var dtmFromDate = new Date(GetDate(fDate));
                var dtmToDate = new Date(GetDate(tDate));

                if (dtmFromDate > dtmToDate) {
                    jAlert('<strong>From date cannot be greater than to date.</strong>', 'GO-SWIFT');
                    return false;
                }
                else {
                    return true;
                }

            }

            function GetDate(str) {
                debugger;
                var arr = str.split('-');
                var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                var i = 1;
                for (i; i <= months.length; i++) {
                    if (months[i] == arr[1]) {
                        break;
                    }
                }
                var formatddate = i + '/' + arr[0] + '/' + arr[2];
                return formatddate;
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
                            <div class="search-sec NOPRINT">
                                <div class="form-group row NOPRINT">
                                    <asp:UpdatePanel ID="up1" runat="server">
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
                                        <label for="Country">
                                            District</label>
                                        <asp:DropDownList CssClass="form-control" TabIndex="16" ID="ddlDistrict" runat="server">
                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnSearch" Style="margin-top: 22px" CssClass="btn btn btn-add btn-sm"
                                            runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return ValidatePage();"></asp:Button>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <asp:Label ID="lblSearchDetails" runat="server"></asp:Label>
                                <asp:GridView ID="grdDepartment" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
                                    DataKeyNames="intKey" CssClass="table table-bordered table-hover" OnRowDataBound="grdDepartment_RowDataBound"
                                    ShowFooter="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hypDepartment" runat="server" Text='<%#Eval("strDeptName")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Balance" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%#Eval("intCarryFwdPending")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Application received in current period" FooterStyle-Font-Bold="true"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%#Eval("intTotalApplication")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                            <asp:Label ID="lblApproved" style="color:green;font-weight:bold;" runat="server" Text='<%#Eval("intTotalApproved")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rejected" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                             <asp:Label ID="lblRejected" runat="server" style="color:red;font-weight:bold;" Text='<%#Eval("intTotalRejected")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. of Application with Query" FooterStyle-Font-Bold="true"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%#Eval("intTotalQueryRaised")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Application pending in current period" FooterStyle-Font-Bold="true"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                               <asp:Label ID="lblPending" runat="server" style="color:violet;font-weight:bold;" Text='<%#Eval("intTotalPending")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Application Pending" FooterStyle-Font-Bold="true"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                            <asp:Label ID="lblTotalPending" style="color:violet;font-weight:bold;" runat="server" Text='<%#Eval("intAllTotalPending")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Application passed ORTPS Timeline" FooterStyle-Font-Bold="true"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                              <asp:Label ID="lblTotalORTPSAtimelinePassed" style="color:orange;font-weight:bold;" runat="server" Text='<%#Eval("intTotalORTPSAtimelinePassed")%>'></asp:Label>
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
                                               <asp:Label ID="lblDeffered" runat="server" style="color:dodgerblue;font-weight:bold;" Text='<%#Eval("intTotalDeferred")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Forwarded" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%#Eval("intTotalForwarded")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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
