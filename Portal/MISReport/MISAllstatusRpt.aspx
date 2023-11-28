<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MISAllstatusRpt.aspx.cs"
    Inherits="Portal_MISReport_MISAllstatusRpt" MasterPageFile="~/MasterPage/Application.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datePicker').datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });
        });
        function setDateValues() {
            var appendId = "ContentPlaceHolder1_";
            var intMonth = (new Date().getMonth());
            var intYear = new Date().getFullYear();
            var fromDate = new Date();
            var toDate = new Date();
            debugger;
            if (intMonth == 0) {
                fromDate = new Date(intYear - 1, 11, 1);
                toDate = new Date();
            }
            else {
                fromDate = new Date(intYear, (intMonth - 1), 1);
                toDate = new Date();
            }

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



        function ShowPealstatewise(dstid, status) {
            debugger;
            var a = document.getElementById("ContentPlaceHolder1_hdnLavelVal").value;
            var b = document.getElementById("ContentPlaceHolder1_hdnDesgid").value;
            var Act = "";
            var intProjectType = "0";
            var Actq = "";
            if (a == "4" || b == "97") {
                if (b == "126") {
                    Act = 't';
                    Actq = 'q';
                }
                else {
                    Act = 'ut';
                    Actq = 'uq';
                }
            }
            else {
                Act = 't';
                Actq = 'q';
            }

            var dstid = dstid;
            var status = status;

            if (a == 4 || b == "97") {
                if (b != "126") {
                    intProjectType = 2;
                }
                else {
                    intProjectType = 0;
                }
            }
            else {
                intProjectType = 0;
            }

            var fDate = $("#ContentPlaceHolder1_txtFromDate").val();
            var tDate = $("#ContentPlaceHolder1_txtToDate").val();

            var e = document.getElementById("ContentPlaceHolder1_ddlSector");
            var Secid = e.options[e.selectedIndex].value;

            var e = document.getElementById("ContentPlaceHolder1_drpInvestmentAmt");
            var InvAmt = e.options[e.selectedIndex].value;

            if (status == "-1") {
                document.getElementById('ContentPlaceHolder1_myIframe').src = "ServiceAppQueryDetails.aspx?dstid=" + dstid + "&Act=" + Actq + "&Secid=" + Secid + "&projctType=" + intProjectType + "&InvAmt=" + InvAmt + "&fDate=" + fDate + "&tDate=" + tDate + "&status=" + 0;
            }
            else {
                document.getElementById('ContentPlaceHolder1_myIframe').src = "MISAllstatusRptDtls.aspx?dstid=" + dstid + "&Act=" + Act + "&Secid=" + Secid + "&projctType=" + intProjectType + "&InvAmt=" + InvAmt + "&fDate=" + fDate + "&tDate=" + tDate + "&status=" + status;
            }
        }
    </script>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    MIS Report for PEAL</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Proposal</a></li><li><a>View</a></li></ul>
            </div>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- Form controls -->
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="dropdown">
                                <ul class="dropdown-menu dropdown-menu-right">
                                    <li>
                                        <asp:LinkButton ID="lnkPdf" runat="server" CssClass=" fa fa-file-pdf-o" title="Export to PDF"
                                            OnClick="lnkPdf_Click"></asp:LinkButton></li>
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
                                    <div class="col-sm-3">
                                        <label for="Country">
                                            District</label>
                                        <asp:DropDownList CssClass="form-control" TabIndex="16" ID="ddlDistrict" runat="server">
                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" runat="server" id="Div2">
                                        <label for="State">
                                            From Date
                                        </label>
                                        <div class="input-group  date datePicker">
                                            <asp:TextBox runat="server" class="form-control" ID="txtFromDate" name="txtFromDate"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                    </div>
                                    <div class="col-sm-3" runat="server" id="Div5">
                                        <label for="State">
                                            To Date
                                        </label>
                                        <div class="input-group  date datePicker">
                                            <asp:TextBox runat="server" class="form-control" ID="txtToDate"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                    </div>
                                    <div class="col-sm-3" runat="server" id="Div4">
                                        <label for="State">
                                            Investment Amount
                                        </label>
                                        <asp:DropDownList ID="drpInvestmentAmt" runat="server" CssClass="form-control" TabIndex="5">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row NOPRINT">
                                    <div class="col-sm-3" runat="server" id="Div1">
                                        <label for="State">
                                            Sector of activity</label>
                                        <asp:DropDownList ID="ddlSector" runat="server" CssClass="form-control" TabIndex="5">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnSearch" Style="margin-top: 22px" CssClass="btn btn btn-add btn-sm"
                                            runat="server" Text="Search" OnClick="btnSearch_Click"></asp:Button>
                                        <asp:HiddenField ID="hdnLavelVal" runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdnDesgid" runat="server"></asp:HiddenField>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive" id="divMain" runat="server">
                                <div id="divLogo" runat="server" visible="false">
                                    <asp:Image ID="imgLogo" runat="server" Height="100" Width="100" />
                                    <h5>
                                        <asp:Label ID="lblMessage" runat="server"></asp:Label></h5>
                                </div>
                                <br />
                                <asp:GridView ID="grdPealDetails" runat="server" class="table table-bordered table-hover"
                                    AutoGenerateColumns="false" EmptyDataText="No Record(s) found...." ShowFooter="true"
                                    OnRowDataBound="grdPealDetails_RowDataBound" DataKeyNames="intDistrictId">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="District">
                                            <ItemTemplate>
                                                <%--<asp:HyperLink ID="hypDistrict" runat="server" Text='<%#Eval("strDistrictName")%>' style="display:none;"></asp:HyperLink>--%>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',0);">
                                                    <asp:Label ID="lblRcvdghgh" runat="server" Text='<%#Eval("strDistrictName")%>' Visible="true"></asp:Label></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%--<asp:HyperLink ID="hyRcvd"  runat="server" Text='<%#Eval("cnt_Total")%>' style="display:none;"></asp:HyperLink>--%>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',0);">
                                                    <asp:Label ID="lblRcvd" runat="server" Text='<%#Eval("cnt_Total")%>' Visible="true"></asp:Label></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rejected" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%--<asp:HyperLink ID="hypRejected" runat="server" Text='<%#Eval("cnt_rejected")%>'  style="display:none;"></asp:HyperLink>--%>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',3);">
                                                    <asp:Label ID="lblRejected" runat="server" Text='<%#Eval("cnt_rejected")%>'></asp:Label></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deferred" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%--<asp:HyperLink ID="hypPending" runat="server" Text='<%#Eval("cnt_Pending")%>' style="display:none;"></asp:HyperLink>--%>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',7);">
                                                    <asp:Label ID="lblDefferec" runat="server" Text='<%#Eval("cnt_deferred")%>'></asp:Label>
                                                </a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%--<asp:HyperLink ID="hypApproved" runat="server" Text='<%#Eval("cnt_Approved")%>' style="display:none;"></asp:HyperLink>--%>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',2);">
                                                    <asp:Label ID="lblApproved" runat="server" Text='<%#Eval("cnt_Approved")%>'></asp:Label></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. under query" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%--<asp:HyperLink ID="hypQuery1" runat="server" Text='<%#Eval("cnt_Query")%>' style="display:none;"></asp:HyperLink>--%>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',-1);">
                                                    <asp:Label ID="lblQuery1" runat="server" Text='<%#Eval("cnt_Query")%>'></asp:Label></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Application Pending in current period" FooterStyle-Font-Bold="true"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%--<asp:HyperLink ID="hypPending" runat="server" Text='<%#Eval("cnt_Pending")%>' style="display:none;"></asp:HyperLink>--%>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',1);">
                                                    <asp:Label ID="lblPending" runat="server" Text='<%#Eval("cnt_Pending")%>'></asp:Label>
                                                </a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Balance" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%--<asp:HyperLink ID="hypPending" runat="server" Text='<%#Eval("cnt_Pending")%>' style="display:none;"></asp:HyperLink>--%>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',8);">
                                                    <asp:Label ID="lblCarryFwdPending" runat="server" Text='<%#Eval("cnt_CarryFwd_pending")%>'></asp:Label>
                                                </a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Application Pending" FooterStyle-Font-Bold="true"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%--<asp:HyperLink ID="hypPending" runat="server" Text='<%#Eval("cnt_Pending")%>' style="display:none;"></asp:HyperLink>--%>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',9);">
                                                    <asp:Label ID="lblTotalPending" runat="server" Text='<%#Eval("int_Total_Pending")%>'></asp:Label>
                                                </a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Application Pending Beyond 30 days" FooterStyle-Font-Bold="true"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',-2);">
                                                    <asp:Label ID="lblORTPS" runat="server" Text='<%#Eval("cnt_Total_ORTPSAtimeline")%>'></asp:Label></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Proposed Employment" DataField="cnt_Proposed_Emp" FooterStyle-Font-Bold="true"
                                            FooterStyle-HorizontalAlign="Right" />
                                        <asp:BoundField HeaderText="Proposed Investment (in INR Lakh.)" DataField="total_Capital_Investment"
                                            FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                        <asp:BoundField HeaderText="Avg. No. of days for disposal" DataField="cnt_AvgDaysApproval"
                                            FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                        <asp:BoundField HeaderText="Land Assessments Done" DataField="cnt_landAssessment"
                                            FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                        <asp:BoundField HeaderText="Land Allotment Completed" DataField="cnt_landAllotment"
                                            FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                        <asp:BoundField HeaderText="Avg. No. of days for Land Allotment" DataField="cnt_AvgDaysAllotment"
                                            FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                        <asp:BoundField HeaderText="Land allotment application pending beyond ORTPSA timeline"
                                            DataField="cnt_Land_Allotment_ORTPSA" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                        <%--<asp:BoundField HeaderText="Application Pending Beyond ORTPSA Timeline" DataField="cnt_Total_ORTPSAtimeline"
                                            FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />--%>
                                    </Columns>
                                </asp:GridView>
                                <div class="table-responsive" id="viewTable" runat="server">
                                    <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover"
                                        AutoGenerateColumns="false" EmptyDataText="No Record(s) found...." ShowFooter="true"
                                        OnRowDataBound="GridView1_RowDataBound" DataKeyNames="intDistrictId">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="District">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRcvdghgh" runat="server" Text='<%#Eval("strDistrictName")%>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRcvd" runat="server" Text='<%#Eval("cnt_Total")%>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rejected" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRejected" runat="server" Text='<%#Eval("cnt_rejected")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Deferred" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeffered" runat="server" Text='<%#Eval("cnt_deferred")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approved" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApproved" runat="server" Text='<%#Eval("cnt_Approved")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No. under query" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuery1" runat="server" Text='<%#Eval("cnt_Query")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Application pending in current period" FooterStyle-Font-Bold="true"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPending" runat="server" Text='<%#Eval("cnt_Pending")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Opening balance" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCarryFwdPending" runat="server" Text='<%#Eval("cnt_CarryFwd_pending")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Application pending" FooterStyle-Font-Bold="true"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalPending" runat="server" Text='<%#Eval("int_Total_Pending")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Application Pending Beyond 30 Days" FooterStyle-Font-Bold="true"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblORTPS" runat="server" Text='<%#Eval("cnt_Total_ORTPSAtimeline")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Proposed Employment" DataField="cnt_Proposed_Emp" FooterStyle-Font-Bold="true"
                                                FooterStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Proposed Investment (in INR Lakh.)" DataField="total_Capital_Investment"
                                                FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Avg. No. of days for approval" DataField="cnt_AvgDaysApproval"
                                                FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Land Assessments Done" DataField="cnt_landAssessment"
                                                FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Land Allotment Completed" DataField="cnt_landAllotment"
                                                FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Avg. No. of days for Land Allotment post Assessment"
                                                DataField="cnt_AvgDaysAllotment" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Land allotment application pending beyond ORTPSA timeline"
                                                DataField="cnt_Land_Allotment_ORTPSA" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div id="divFooter" runat="server" visible="false">
                                    <h4>
                                        www.investodisha.org</h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
        <div id="Div3" class="modal fade" role="dialog">
            <div class="modal-dialog modal-lg" style="width: 1000px !important;">
                <!-- Modal content-->
                <div class="modal-content modal-primary ">
                    <div class="modal-header bg-red">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">
                            <asp:Label ID="lbldet1" runat="server" Text=""></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                        <iframe name="myIframe" id="myIframe" width="100%" style="height: 450px;" runat="server">
                        </iframe>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
