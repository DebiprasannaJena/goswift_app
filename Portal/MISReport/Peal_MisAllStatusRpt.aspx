<%--'*******************************************************************************************************************
' File Name         : Peal_MisAllStatusRpt.aspx
' Description       : PEAL MIS Report
' Created by        : 
' Created On        : 
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Peal_MisAllStatusRpt.aspx.cs"
    Inherits="Portal_MISReport_Peal_MisAllStatusRpt" MasterPageFile="~/MasterPage/Application.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('.datePicker').datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });
        });

        /*-----------------------------------------------------------------------*/

        function setDateValues() {

            var appendId = "ContentPlaceHolder1_";
            var intMonth = (new Date().getMonth());
            var intYear = new Date().getFullYear();
            var fromDate = new Date();
            var toDate = new Date();
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

        /*-----------------------------------------------------------------------*/

        function ShowPealstatewise(dstid, status, distname) {

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
            if (dstid == "0") {
                var e = document.getElementById("ContentPlaceHolder1_ddlDistrict");
                var dstid = e.options[e.selectedIndex].value;
            }
            else {
                var dstid = dstid;
            }
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
            try {
                var InvAmt = e.options[e.selectedIndex].value;
            }
            catch (err) {
                var InvAmt = 0;
            }
            if (status == "-1") {
                document.getElementById('ContentPlaceHolder1_myIframe').src = "ServiceAppQueryDetails.aspx?dstid=" + dstid + "&distname=" + distname + "&Act=" + Actq + "&Secid=" + Secid + "&projctType=" + intProjectType + "&InvAmt=" + InvAmt + "&fDate=" + fDate + "&tDate=" + tDate + "&status=" + 0;
            }
            else {
                document.getElementById('ContentPlaceHolder1_myIframe').src = "MISAllstatusRptDtls.aspx?dstid=" + dstid + "&distname=" + distname + "&Act=" + Act + "&Secid=" + Secid + "&projctType=" + intProjectType + "&InvAmt=" + InvAmt + "&fDate=" + fDate + "&tDate=" + tDate + "&status=" + status + "&Logic=" + 1;
            }
        }

        /*-----------------------------------------------------------------------*/

        function ValidatePage() {
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

        /*-----------------------------------------------------------------------*/

        function GetDate(str) {
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>MIS Report for PEAL</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>MIS Report</a></li>
                    <li><a>PEAL</a></li>
                </ul>
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
                                        <asp:LinkButton ID="LnkBtnPdfExport" runat="server" CssClass=" fa fa-file-pdf-o" title="Export to PDF"
                                            OnClick="LnkBtnPdfExport_Click"></asp:LinkButton></li>
                                    <li><a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                                        <asp:LinkButton ID="LnkBtnExcel" CssClass="back" runat="server" title="Export to Excel"
                                            OnClick="LnkBtnExcelExport_Click"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
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
                                        <asp:Button ID="BtnSearch" Style="margin-top: 22px" CssClass="btn btn btn-add btn-sm"
                                            runat="server" Text="Search" OnClick="BtnSearch_Click" OnClientClick="return ValidatePage();"></asp:Button>
                                        <asp:HiddenField ID="hdnLavelVal" runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdnDesgid" runat="server"></asp:HiddenField>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive" id="divMain" runat="server"> 
                                <asp:GridView ID="GrdPealDetails" runat="server" class="table table-bordered table-hover"
                                    AutoGenerateColumns="false" EmptyDataText="No Record(s) found...." ShowFooter="true"
                                    OnRowDataBound="GrdPealDetails_RowDataBound" DataKeyNames="intDistrictId">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="District">
                                            <ItemTemplate>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',0,'<%#Eval("strDistrictName")%>');">
                                                    <asp:Label ID="lblRcvdghgh" runat="server" Text='<%#Eval("strDistrictName")%>' Visible="true"></asp:Label></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Opening Balance" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',8,'<%#Eval("strDistrictName")%>');">
                                                    <asp:Label ID="lblCarryFwdPending" runat="server" Text='<%#Eval("cnt_CarryFwd_pending")%>'></asp:Label>
                                                </a>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblCarryFwdPendingFooter" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Application received in current period" FooterStyle-Font-Bold="true"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',0,'<%#Eval("strDistrictName")%>');">
                                                    <asp:Label ID="lblRcvd" runat="server" Text='<%#Eval("cnt_Total")%>' Visible="true"></asp:Label></a>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblRcvdFooter" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',2,'<%#Eval("strDistrictName")%>');">
                                                    <asp:Label ID="lblApproved" style="color:green;font-weight:bold;"  runat="server" Text='<%#Eval("cnt_Approved")%>'></asp:Label></a>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblApprovedFooter" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rejected" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',3,'<%#Eval("strDistrictName")%>');">
                                                    <asp:Label ID="lblRejected" runat="server" style="color:red;font-weight:bold;" Text='<%#Eval("cnt_rejected")%>'></asp:Label></a>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblRejectedFooter" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Deferred" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',7,'<%#Eval("strDistrictName")%>');">
                                                    <asp:Label ID="lblDefferec" runat="server" style="color:dodgerblue;font-weight:bold;" Text='<%#Eval("cnt_deferred")%>'></asp:Label>
                                                </a>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblDefferecFooter" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="No. under query" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',-1,'<%#Eval("strDistrictName")%>');">
                                                    <asp:Label ID="lblQuery1" runat="server" Text='<%#Eval("cnt_Query")%>'></asp:Label></a>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblQuery1Footer" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Application Pending in current period" FooterStyle-Font-Bold="true"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',1,'<%#Eval("strDistrictName")%>');">
                                                    <asp:Label ID="lblPending" runat="server" style="color:violet;font-weight:bold;" Text='<%#Eval("cnt_Pending")%>'></asp:Label>
                                                </a>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblPendingFooter" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Application Pending" FooterStyle-Font-Bold="true"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',9,'<%#Eval("strDistrictName")%>');">
                                                    <asp:Label ID="lblTotalPending" style="color:violet;font-weight:bold;" runat="server" Text='<%#Eval("int_Total_Pending")%>'></asp:Label>
                                                </a>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalPendingFooter" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Application Pending Beyond 30 days" FooterStyle-Font-Bold="true"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" title="" onclick="ShowPealstatewise('<%# Eval("intDistrictId") %>',-2,'<%#Eval("strDistrictName")%>');">
                                                    <asp:Label ID="lblORTPS" style="color:violet;font-weight:bold;"    runat="server" Text='<%#Eval("cnt_Total_ORTPSAtimeline")%>'></asp:Label></a>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblORTPSFooter" runat="server"></asp:Label>
                                            </FooterTemplate>
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
                                            DataField="cnt_Land_Allotment_ORTPSA" ItemStyle-ForeColor="orange"  FooterStyle-Font-Bold="true" ItemStyle-Font-Bold="true"   FooterStyle-HorizontalAlign="Right" />
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
                                            <asp:TemplateField HeaderText="Opening balance" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCarryFwdPending" runat="server" Text='<%#Eval("cnt_CarryFwd_pending")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Application received in current period" FooterStyle-Font-Bold="true"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRcvd" runat="server" Text='<%#Eval("cnt_Total")%>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approved" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApproved" runat="server" Text='<%#Eval("cnt_Approved")%>'></asp:Label>
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
                                                    <asp:Label ID="lblORTPS" runat="server" style="color:violet;font-weight:bold;"  Text='<%#Eval("cnt_Total_ORTPSAtimeline")%>'></asp:Label>
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
                                            <asp:BoundField HeaderText="Avg. No. of days for Land Allotment post Assessment"
                                                DataField="cnt_AvgDaysAllotment" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Land allotment application pending beyond ORTPSA timeline"
                                                DataField="cnt_Land_Allotment_ORTPSA" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                        </Columns>
                                    </asp:GridView>
                                </div>                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
        <div id="Div3" class="modal fade" role="dialog">
            <div class="modal-dialog modal-lg" style="width: 90%;">
                <!-- Modal content-->
                <div class="modal-content modal-primary ">
                    <div class="modal-header bg-red">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">
                            <asp:Label ID="lbldet1" runat="server" Text=""></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                        <iframe name="myIframe" id="myIframe" style="width: 100%; height: 450px;" runat="server"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
