<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ContactusView.aspx.cs" Inherits="Portal_CMS_ContactusView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('.datePicker').datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });

            //To retain calender during postback inside update panel.
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                $('.datePicker').datepicker({
                    format: 'dd-M-yyyy',
                    autoclose: true
                });
            }
        });

        /*----------------------------------------------------------------------------*/

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

            $("#" + appendId + "TxtFromDate").datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            }).datepicker("setDate", fromDate);
            $("#" + appendId + "TxtToDate").datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            }).datepicker("setDate", toDate);
        }

        /*----------------------------------------------------------------------------*/

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
    <script type="text/javascript">
        function validate() {
            var fDate = $("#ContentPlaceHolder1_TxtFromDate").val();
            var tDate = $("#ContentPlaceHolder1_TxtToDate").val();
            //alert(fDate);
            //alert(tDate);
            if (fDate != "" && tDate == "") {
                jAlert('<strong>Please select To date.</strong>', 'GO-SWIFT');
                return false;
            }
            if (tDate != "" && fDate == "") {
                jAlert('<strong>Please select From date.</strong>', 'GO-SWIFT');
                return false;
            }
            var dtmFromDate = new Date(GetDate(fDate));
            var dtmToDate = new Date(GetDate(tDate));

            if (dtmFromDate > dtmToDate) {
                jAlert('<strong>From date cannot be greater than To date.</strong>', 'GO-SWIFT');
                return false;
            }
            else {
                return true;
            }
        }
    </script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>View Contact Us Details</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>CMS</a></li>
                    <li><a>View Contact Us</a></li>
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
                        </div>
                        <!--Search Filter-->
                        <div class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="search-sec">
                                        <div class="form-group ">
                                            <div class="row">
                                                <label class="col-sm-3" for="Country">
                                                    Name Of Company/Industry</label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtCompanyName" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <label class="col-sm-3">
                                                    Email Id</label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtEmail" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-3" for="Country">
                                                    Name
                                                </label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="TxtName" MaxLength="100" AutoCompleteType="None" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <label class="col-sm-3" for="Country">
                                                    Phone Number</label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="TxtPhoneNumber" MaxLength="10" onkeypress="return isNumberKey(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-3" for="Country">
                                                    From Date
                                                </label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <div class="input-group date datePicker">
                                                        <asp:TextBox runat="server" AutoCompleteType="None" class="form-control" ID="TxtFromDate" name="txtFromDate"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    </div>
                                                </div>
                                                <label class="col-sm-3" for="Country">
                                                    To Date</label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <div class="input-group date datePicker">
                                                        <asp:TextBox runat="server" AutoCompleteType="None" class="form-control" ID="TxtToDate" name="TxtToDate"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3"></div>
                                                <div class="col-sm-7">
                                                    <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" OnClientClick="return validate();" CssClass="btn btn btn-add"
                                                        runat="server" Text="Search"></asp:Button>

                                                    <asp:Button ID="BtnReset" OnClick="BtnReset_Click" CssClass="btn btn btn-danger"
                                                        runat="server" Text="Reset"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <div align="right" style="margin-bottom: 10px;">
                                            <asp:LinkButton ID="LbtnAll" Visible="true" runat="server" Text="ALL" OnClick="LbtnAll_Click"></asp:LinkButton>
                                            &nbsp;&nbsp;
                                            <asp:Label ID="lblPaging" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </div>
                                        <asp:GridView ID="GrdViewData" CssClass="table table-bordered table-hover"
                                            runat="server" AutoGenerateColumns="false" DataKeyNames="IntCmsId" AllowPaging="true"
                                            PageSize="50" OnRowDataBound="GrdViewData_RowDataBound" OnPageIndexChanging="GrdViewData_PageIndexChanging" Width="100%">
                                            <Columns>
                                                <asp:BoundField HeaderText="Sl#." ItemStyle-Width="4%" />
                                                <asp:BoundField HeaderText="Name" DataField="Strusername" ItemStyle-Width="10%" />
                                                <asp:BoundField HeaderText="Email Id" DataField="Strmail" ItemStyle-Width="15%" />
                                                <asp:BoundField HeaderText="Mobile No" DataField="Strmobileno" ItemStyle-Width="10%" />
                                                <asp:BoundField HeaderText="Industry Name" DataField="Strcompanyname" ItemStyle-Width="15%" />
                                                <asp:BoundField HeaderText="Description" DataField="StrDescription" />
                                                <asp:BoundField HeaderText="Date" DataField="StrDate" ItemStyle-Width="10%" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <h5 style="color: red"><b>No Record(s) Available</b></h5>
                                            </EmptyDataTemplate>
                                            <PagerStyle CssClass="pagination-grid" />
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

        </section>
        <!-- /.content -->
    </div>
</asp:Content>
