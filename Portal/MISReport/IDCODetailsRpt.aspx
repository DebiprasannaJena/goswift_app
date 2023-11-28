<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IDCODetailsRpt.aspx.cs" MasterPageFile="~/MasterPage/Application.master"
    Inherits="IDCODetailsRpt" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/decimalrstr.js" type="text/javascript"></script>
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
            if (intMonth == 0) {
                fromDate = new Date(intYear - 1, 11, 1);
                toDate = new Date();
            }
            else {
                fromDate = new Date(intYear, (intMonth - 1), 1);
                toDate = new Date();
            }

            $("#" + appendId + "txtFromdate").datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            }).datepicker("setDate", fromDate);
            $("#" + appendId + "txtTodate").datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            }).datepicker("setDate", toDate);
        }


        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'

    </script>
    <style>
        /* Comments
---------------------------------- */
        .comments
        {
            margin-top: 60px;
        }
        .comments h2.title
        {
            margin-bottom: 40px;
            border-bottom: 1px solid #d2d2d2;
            padding-bottom: 10px;
        }
        .comment
        {
            font-size: 14px;
        }
        .comment .comment
        {
            margin-left: 75px;
        }
        .comment-avatar
        {
            margin-top: 5px;
            width: 55px;
            float: left;
        }
        .comment-content
        {
            border-bottom: 1px solid #d2d2d2;
            margin-bottom: 25px;
        }
        .comment h3
        {
            margin-top: 0;
            margin-bottom: 5px;
        }
        .comment-meta
        {
            margin-bottom: 15px;
            color: #999999;
            font-size: 12px;
        }
        .comment-meta a
        {
            color: #666666;
        }
        .comment-meta a:hover
        {
            text-decoration: underline;
        }
        .comment .btn
        {
            font-size: 12px;
            padding: 7px;
            min-width: 100px;
            margin-top: 5px;
            margin-bottom: -1px;
        }
        .btn-gray
        {
            color: #ffffff;
            background-color: #666666;
            border-color: #666666;
        }
        .comment .btn i
        {
            padding-right: 5px;
        }
    </style>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <section class="content-header">
        <div class="header-icon">
            <i class="fa fa-dashboard"></i>
        </div>
        <div class="header-title">
            <h1>
                IDCO Forward Details Report</h1>
        </div>
        </section>
        <section class="content">
        <div class="row">
            <!-- Form controls -->
            <div class="col-sm-12 " data-lobipanel-child-inner-id="CgbyYkSXVZ">
                <div class="panel panel-bd lobidisable">
                    <div class="panel-heading noprint">
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
                        <div class="search-sec">
                            <div class="form-group row">
                                <label class="col-sm-3" for="Country">
                                    Proposal No (GO-SWIFT)</label>
                                <div class="col-sm-3">
                                    <span class="colon">:</span>
                                    <asp:TextBox ID="txtProposalNo" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <label class="col-sm-2" for="State">
                                    Application Number(IDCO ERP)</label>
                                <div class="col-sm-3" runat="server" id="st3">
                                    <span class="colon">:</span>
                                    <asp:TextBox ID="txtApplicationNumber" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3" for="State">
                                    Unit Name</label>
                                <div class="col-sm-3" runat="server" id="Div1">
                                    <span class="colon">:</span>
                                    <asp:TextBox ID="txtUnitName" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <label class="col-sm-2" for="State">
                                    District</label>
                                <div class="col-sm-3" runat="server" id="Div2">
                                    <span class="colon">:</span>
                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddlDistrict" runat="server">
                                        <asp:ListItem Value="0">---Select---</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3" for="State">
                                    Nodal Agency</label>
                                <div class="col-sm-3" runat="server" id="Div3">
                                    <span class="colon">:</span>
                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="drpNodalOffcr" runat="server">
                                        <asp:ListItem Value="0">---Select---</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2" for="State">
                                    Total Capital Investment</label>
                                <div class="col-sm-3" runat="server" id="Div4">
                                    <span class="colon">:</span>
                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="drpInvestment" runat="server">
                                        <asp:ListItem Value="0">---Select---</asp:ListItem>
                                        <asp:ListItem Value="1">>= 50 Crore</asp:ListItem>
                                        <asp:ListItem Value="2">< 50 Crore</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-3">
                                        Applied From Date</label>
                                    <div class="col-sm-3">
                                        <span class="colon">:</span>
                                        <div class="input-group  date datePicker" id="datePicker1">
                                            <asp:TextBox ID="txtFromdate" CssClass="form-control" runat="server"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                    </div>
                                    <label class="col-sm-2">
                                        Applied To Date</label>
                                    <div class="col-sm-3">
                                        <span class="colon">:</span>
                                        <div class="input-group  date datePicker">
                                            <asp:TextBox ID="txtTodate" CssClass="form-control" runat="server"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3" for="State">
                                    Sector</label>
                                <div class="col-sm-3" runat="server" id="Div6">
                                    <span class="colon">:</span>
                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddlSector" runat="server">
                                        <asp:ListItem Value="0">---Select---</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-1">
                                    <asp:Button ID="btnSearch" CssClass="btn btn btn-add -sm" runat="server" Text="Search"
                                        OnClick="btnSearch_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div align="right">
                            <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                OnClick="lbtnAll_Click"></asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:Label ID="lblPaging" runat="server"></asp:Label>
                        </div>
                        <div class="table-responsive" id="viewTable" runat="server">
                            <asp:GridView ID="grdIDCO" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                PageSize="50" CssClass="table table-bordered table-hover" EmptyDataText="No records found...."
                                OnPageIndexChanging="grdIDCO_PageIndexChanging" OnRowDataBound="grdIDCO_RowDataBound">
                                <Columns>
                                    <asp:BoundField HeaderText="Sl No." />
                                    <asp:BoundField DataField="vchProposalNo" HeaderText="Proposal Number" />
                                    <asp:BoundField DataField="vchCompName" HeaderText="Unit Name" />
                                    <asp:BoundField DataField="vchDistrictName" HeaderText="District" />
                                    <asp:BoundField DataField="vchSector" HeaderText="Sector" />
                                    <asp:BoundField DataField="strNodalOfcrName" HeaderText="Nodal Agency" />
                                    <asp:BoundField DataField="dtmForwardDate" HeaderText="Applied Date" />
                                    <asp:BoundField DataField="decAnnulTurnOvr1" HeaderText="Total Land Required" />
                                    <asp:BoundField DataField="strLandRecommendedtoidco" HeaderText="Land Recommended To Idco" />
                                    <asp:BoundField DataField="strLandRecommendedbyidco" HeaderText="Land Alloted By Idco" />
                                    <asp:BoundField DataField="decAnnulTurnOvr2" HeaderText="Total Capital Investment" />
                                    <asp:BoundField DataField="vchOtherState" HeaderText="Status" />
                                    <asp:BoundField DataField="vchPanfile" HeaderText="Application Number(IDCO ERP)" />
                                    <asp:TemplateField HeaderText="Approval Link">
                                        <ItemTemplate>
                                            <a id="hplnkPan" class="datalabel" href='<%# Eval("strApprovalorderlink") %>' target="_blank"
                                                runat="server" visible='<%#Eval("strApprovalorderlink").ToString() != "NA" %>'><i
                                                    class="fa fa-cloud-download" aria-hidden="true"></i></a>
                                            <asp:Label ID="lblData" Text='<%# Eval("strApprovalorderlink") %>' Visible='<%#Eval("strApprovalorderlink").ToString() == "NA" %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="strPayStatus" HeaderText="Processing Fee Payment Status" />
                                </Columns>
                                <PagerStyle CssClass="pagination-grid no-print" />
                            </asp:GridView>
                        </div>
    </div>
    <!-- panel-group -->
    </div> </div>
    <!-- panel-group -->
    </div> </div> </div> </section>
    <script>
        $(document).ready(function () {
            $('.panel-title a').click(function () {

                $(this).find('i').toggleClass('fa-minus fa-plus');
            });
        })
    </script>
    <!-- /.content -->
</asp:Content>
