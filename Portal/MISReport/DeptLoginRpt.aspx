<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="DeptLoginRpt.aspx.cs" Inherits="Portal_MISReport_DeptLoginRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/decimalrstr.js" type="text/javascript"></script>
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function validateLoginReport() {
            if (blankFieldValidation('ContentPlaceHolder1_txtFromdate', 'From date', projname) == false) {
                $("#ContentPlaceHolder1_txtFromdate").focus();
                return false;
            };
            if (blankFieldValidation('ContentPlaceHolder1_txtTodate', 'To date', projname) == false) {
                $("#ContentPlaceHolder1_txtTodate").focus();
                return false;
            };

        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('.datePicker').datepicker({
                autoclose: true,
                format: 'dd-M-yyyy'
            });
        });

        /*------------------------------------------------------------------------------*/

        //function htmlUnescape(value) {
        //    return String(value)
        //        .replace(/&quot;/g, '"')
        //        .replace(/&#39;/g, "'")
        //        .replace(/&lt;/g, '<')
        //        .replace(/&gt;/g, '>')
        //        .replace(/&amp;/g, '&');
        //}

        /*------------------------------------------------------------------------------*/

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

    </script>


    <style>
        .control-label {
            border: 1px solid #b9bdbf;
            padding: 6px 10px;
            border-radius: 2px;
            height: 31px;
            width: 100%;
            background: #f9f9f9;
            display: block;
            margin: 0px;
        }
    </style>
    <style type="text/css" media="all">
        /* fix rtl for demo */
        .chosen-rtl .chosen-drop {
            left: -9000px;
        }

        .chosen-container .chosen-container-single .chosen-single {
            width: 100% !important;
        }

        .searchbox {
            background-color: #def3ff;
            padding: 8px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>

    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>Department Login Report</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>MIS</a></li>
                    <li><a>Department Login Report</a></li>
                </ul>
            </div>
        </div>
        <!-- Main content -->
        <div class="content">
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
                            <div class="search-sec">


                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-2">
                                            From Date<span class="mandetory-y">*</span></label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <div class="input-group date datePicker " id="datePicker1">
                                                <asp:TextBox ID="txtFromdate" CssClass="form-control" runat="server" AutoCompleteType="None"
                                                    autoComplete="off"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                        </div>
                                        <label class="col-sm-2">
                                            To Date<span class="mandetory-y">*</span></label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <div class="input-group  date datePicker">
                                                <asp:TextBox ID="txtTodate" CssClass="form-control" runat="server" AutoCompleteType="None"
                                                    autoComplete="off"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-2">
                                            Login Type
                                        </label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="DrpDwn_Logintype" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1">For All User</asp:ListItem>
                                                <asp:ListItem Value="2">Only Login User</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-sm-2">
                                            User Name</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Username" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-7">
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="BtnSearch" runat="server" Text="Search" class="btn btn-add" OnClick="BtnSearch_Click" OnClientClick="return validateLoginReport() ;"></asp:Button>
                                            <asp:Button ID="BtnReset" runat="server" Text="Reset" class="btn btn-danger" OnClick="BtnReset_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="table-responsive" id="viewTable" runat="server">
                                <%--<div align="right">
                                    <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                        OnClick="lbtnAll_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                </div>--%>
                                <asp:GridView ID="GridDeptLoginRpt" runat="server" class="table table-bordered table-hover"
                                    AutoGenerateColumns="False" Width="100%" EmptyDataText="No Record(s) Found...">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SlNo" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsl" runat="server" Text='<%#(GridDeptLoginRpt.PageIndex * GridDeptLoginRpt.PageSize) + (GridDeptLoginRpt.Rows.Count + 1)%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name of Department/ Organization" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_DepartmentName" runat="server" Text='<%# Eval("nvchLevelName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="User Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="11%">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_UserName" runat="server" Text='<%# Eval("vchUserName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Full Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_FullName" runat="server" Text='<%# Eval("vchFullName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Designation" runat="server" Text='<%# Eval("nvchDesigName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. of Times Login" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="9%">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_LoginCount" runat="server" Text='<%# Eval("intLoginCount") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Login" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_LastLogin" runat="server" Text='<%# Eval("dtmLastLogin" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="pagination-grid no-print" />
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
        <!-- /.content -->
    </div>
</asp:Content>