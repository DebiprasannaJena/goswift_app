<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="GrivMisAllStatusRpt.aspx.cs" Inherits="Portal_MISReport_GrivMisAllStatusRpt" %>

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

        function ShowGrvstatewise(dstid, status, distname) {
            debugger;
            var a = document.getElementById("ContentPlaceHolder1_hdnLavelVal").value;
            var b = document.getElementById("ContentPlaceHolder1_hdnDesgid").value;
            var Act = "VD";
            if (dstid == "0") {
                var e = document.getElementById("ContentPlaceHolder1_ddlDistrict");
                var dstid = e.options[e.selectedIndex].value;
            }
            else {
                var dstid = dstid;
            }
            var status = status;

            var fDate = $("#ContentPlaceHolder1_txtFromDate").val();
            var tDate = $("#ContentPlaceHolder1_txtToDate").val();

            var e = document.getElementById("ContentPlaceHolder1_drpGrievance");
            var Gtypeid = e.options[e.selectedIndex].value;

            document.getElementById('ContentPlaceHolder1_myIframe').src = "GrivMisDrillDownStatusRpt.aspx?dstid=" + dstid + "&distname=" + distname + "&Act=" + Act + "&Gtypeid=" + Gtypeid + "&fDate=" + fDate + "&tDate=" + tDate + "&status=" + status + "&Logic=" + 1;


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
                <h1>MIS Report for Grievance</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>MIS Report</a></li>
                    <li><a>Grievance</a></li>
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
                                        <asp:LinkButton ID="LnkBtnPdf" runat="server" CssClass=" fa fa-file-pdf-o" title="Export to PDF"
                                            OnClick="LnkBtnPdf_Click"></asp:LinkButton></li>
                                    <li><a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                                        <asp:LinkButton ID="LnkBtnExcel" CssClass="back" runat="server" title="Export to Excel"
                                            OnClick="LnkBtnExcel_Click"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
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
                                        <asp:DropDownList CssClass="form-control" ID="ddlDistrict" runat="server">
                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" runat="server" id="Div2">
                                        <label for="State">
                                            From Date
                                        </label>
                                        <div class="input-group date datePicker">
                                            <asp:TextBox runat="server" class="form-control" ID="txtFromDate" name="txtFromDate"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                    </div>
                                    <div class="col-sm-3" runat="server" id="Div5">
                                        <label for="State">
                                            To Date
                                        </label>
                                        <div class="input-group date datePicker">
                                            <asp:TextBox runat="server" class="form-control" ID="txtToDate"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                    </div>
                                    <div class="col-sm-3" runat="server" id="Div4">
                                        <label for="State">
                                            Grievance Type
                                        </label>
                                        <asp:DropDownList ID="drpGrievance" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row NOPRINT">
                                    <div class="col-sm-2">
                                        <asp:Button ID="BtnSearch" CssClass="btn btn btn-add"
                                            runat="server" Text="Search" OnClick="BtnSearch_Click" OnClientClick="return ValidatePage();"></asp:Button>
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
                                <div class="table-responsive" id="viewTable" runat="server">
                                    <asp:GridView ID="GrdGrivDetails" runat="server" class="table table-bordered table-hover"
                                        AutoGenerateColumns="false" EmptyDataText="No Record(s) found...." ShowFooter="true"
                                        OnRowDataBound="GrdGrivDetails_RowDataBound" DataKeyNames="intDistrictId">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="District">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblDistrict" runat="server" Text='<%#Eval("vchDistrictName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Opening Balance" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <a href="#Div3" data-toggle="modal" data-target="#Div3" title=""
                                                        onclick="ShowGrvstatewise('<%# Eval("intDistrictId") %>',10,'<%#Eval("vchDistrictName")%>');">
                                                        <asp:Label ID="LblCarryFwdPending" runat="server" Text='<%#Eval("cnt_CarryForwardPending")%>'></asp:Label>
                                                    </a>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="LblCarryFwdPendingFooter" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Grievance Received in Current Period" FooterStyle-Font-Bold="true"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <a href="#Div3" data-toggle="modal" data-target="#Div3" title=""
                                                        onclick="ShowGrvstatewise('<%# Eval("intDistrictId") %>',0,'<%#Eval("vchDistrictName")%>');">
                                                        <asp:Label ID="LblRcvd" runat="server" Text='<%#Eval("cnt_Total")%>' Visible="true"></asp:Label></a>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="LblRcvdFooter" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Resolved" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <a href="#Div3" data-toggle="modal" data-target="#Div3" title=""
                                                        onclick="ShowGrvstatewise('<%# Eval("intDistrictId") %>',13,'<%#Eval("vchDistrictName")%>');">
                                                        <asp:Label ID="LblApproved" runat="server" Text='<%#Eval("cnt_Approved")%>'></asp:Label></a>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="LblApprovedFooter" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rejected" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <a href="#Div3" data-toggle="modal" data-target="#Div3" title=""
                                                        onclick="ShowGrvstatewise('<%# Eval("intDistrictId") %>',3,'<%#Eval("vchDistrictName")%>');">
                                                        <asp:Label ID="LblRejected" style="color:red;font-weight:bold;" runat="server" Text='<%#Eval("cnt_Rejected")%>'></asp:Label></a>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="LblRejectedFooter" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Deferred" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <a href="#Div3" data-toggle="modal" data-target="#Div3" title=""
                                                        onclick="ShowGrvstatewise('<%# Eval("intDistrictId") %>',7,'<%#Eval("vchDistrictName")%>');">
                                                        <asp:Label ID="LblDeferred" style="color:dodgerblue;font-weight:bold;" runat="server" Text='<%#Eval("cnt_Deferred")%>'></asp:Label>
                                                    </a>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="LblDeferredFooter" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Forwarded" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <a href="#Div3" data-toggle="modal" data-target="#Div3" title=""
                                                        onclick="ShowGrvstatewise('<%# Eval("intDistrictId") %>',8,'<%#Eval("vchDistrictName")%>');">
                                                        <asp:Label ID="LblForwarded" runat="server" Text='<%#Eval("cnt_Forwarded")%>'></asp:Label>
                                                    </a>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="LblForwardedFooter" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Grievance Pending in Current Period" FooterStyle-Font-Bold="true"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <a href="#Div3" data-toggle="modal" data-target="#Div3" title=""
                                                        onclick="ShowGrvstatewise('<%# Eval("intDistrictId") %>',1,'<%#Eval("vchDistrictName")%>');">
                                                        <asp:Label ID="LblPending" style="color:violet;font-weight:bold;" runat="server" Text='<%#Eval("cnt_Pending")%>'></asp:Label>
                                                    </a>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="LblPendingFooter" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Grievance Pending" FooterStyle-Font-Bold="true"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <a href="#Div3" data-toggle="modal" data-target="#Div3" title=""
                                                        onclick="ShowGrvstatewise('<%# Eval("intDistrictId") %>',9,'<%#Eval("vchDistrictName")%>');">
                                                        <asp:Label ID="LblTotalPending" style="color:violet;font-weight:bold;" runat="server" Text='<%#Eval("cnt_TotalPending")%>'></asp:Label>
                                                    </a>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="LblTotalPendingFooter" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Grievance Pending Beyond 30 Days" FooterStyle-Font-Bold="true"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <a href="#Div3" data-toggle="modal" data-target="#Div3" title=""
                                                        onclick="ShowGrvstatewise('<%# Eval("intDistrictId") %>',-2,'<%#Eval("vchDistrictName")%>');">
                                                        <asp:Label ID="LblORTPSA" style="color:violet;font-weight:bold;" runat="server" Text='<%#Eval("cnt_Total_Pending30days")%>'></asp:Label></a>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="LblORTPSAFooter" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Avg. No. of Days for Resolved" DataField="cnt_Total_AvgDaysApproval"
                                                FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div id="divFooter" runat="server" visible="false">
                                    <h4>www.investodisha.org</h4>
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

