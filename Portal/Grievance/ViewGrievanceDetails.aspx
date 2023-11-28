<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ViewGrievanceDetails.aspx.cs" Inherits="Portal_Grievance_ViewGrievanceDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/decimalrstr.js" type="text/javascript"></script>
    <style></style>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#btnSubmit').click(function () {

            });


            $('.datePicker').datepicker({
                autoclose: true,
                format: 'dd-M-yyyy'
            });

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $('.datePicker').datepicker({ format: 'dd-M-yyyy' });
            }
        });

        /*------------------------------------------------------------------------------*/

        function htmlUnescape(value) {
            return String(value)
                .replace(/&quot;/g, '"')
                .replace(/&#39;/g, "'")
                .replace(/&lt;/g, '<')
                .replace(/&gt;/g, '>')
                .replace(/&amp;/g, '&');
        }

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
                <h1>View Grievance Details</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Grievance</a></li>
                    <li><a>View Grievance</a></li>
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
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="GrievanceTakeAction.aspx"><i class="fa fa-plus"></i>Take
                                    Action</a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="ViewGrievanceDetails.aspx"><i class="fa fa-file"></i>View</a>
                            </div>
                        </div>
                        <div class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>

                                    <div class="search-sec">
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Grievance Id</label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="Txt_Griv_Id" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <label class="col-sm-2">
                                                    District Name</label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:DropDownList ID="DrpDwn_District" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Investment Level</label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:DropDownList ID="DrpDwn_Investment_Level" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Project Cost >=50 Cr</asp:ListItem>
                                                        <asp:ListItem Value="2">Project Cost Upto < 50 Cr</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <label class="col-sm-2">
                                                    Status</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="DrpDwn_Status" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    From Date</label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <div class="input-group  date datePicker" id="datePicker1">
                                                        <asp:TextBox ID="txtFromdate" CssClass="form-control" runat="server" AutoCompleteType="None"
                                                            autoComplete="off"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    </div>
                                                </div>
                                                <label class="col-sm-2">
                                                    To Date</label>
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
                                                <div class="col-sm-7">
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:Button ID="BtnSearch" runat="server" Text="Search" class="btn btn-add" OnClick="BtnSearch_Click"></asp:Button>
                                                    <asp:Button ID="BtnReset" runat="server" Text="Reset" class="btn btn-danger" OnClick="BtnReset_Click"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="table-responsive">
                                        <div align="right">
                                            <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                                OnClick="lbtnAll_Click"></asp:LinkButton>
                                            &nbsp;&nbsp;
                                    <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                        </div>
                                        <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover"
                                            AutoGenerateColumns="False" AllowPaging="True" PageSize="10" Width="100%" EmptyDataText="No Record(s) Found..."
                                            DataKeyNames="vchGrivId" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SlNo" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(GridView1.PageIndex * GridView1.PageSize) + (GridView1.Rows.Count + 1)%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="3%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grievance Id" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HypLnk_Griv_Id" ForeColor="Blue" Text='<%#Eval("vchGrivId") %>'
                                                            runat="server" ToolTip="Click here to view grievance details"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name Of Company" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Company_Name" runat="server" Text='<%# Eval("VCH_INV_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Applicant Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Applicant_Name" runat="server" Text='<%# Eval("vchApplicantName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>--%>
                                                <%-- <asp:TemplateField HeaderText="Mobile No" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Mobile_No" runat="server" Text='<%# Eval("vchMobileNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                        </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Industry Type" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Industry_Type" runat="server" Text='<%# Eval("intIndustryCategory") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Investment Level" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="Hid_Invest_Level" runat="server" Value='<%# Eval("intInvestLevel") %>' />
                                                        <asp:Label ID="Lbl_Invest_Level" runat="server" Text='<%# Eval("vchInvestLevel") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="11%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="District Name" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_District_Name" runat="server" Text='<%# Eval("vchDistrictName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grievance Type" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Griv_Type" runat="server" Text='<%# Eval("vchGrivType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="8%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grievance Sub Type" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Griv_Sub_Type" runat="server" Text='<%# Eval("vchGrivSubType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grievance Title" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Griv_Title" runat="server" Text='<%# Eval("vchGrivTitle") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Apply Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Apply_Date" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="6%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="Hid_Status" runat="server" Value='<%# Eval("intStatus") %>' />
                                                        <asp:Label ID="Lbl_Status" runat="server" Text='<%# Eval("vchStatusName")  %>' Font-Bold="true"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Action_Date" runat="server" Text='<%# Eval("dtmActionDate" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="6%"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                        </asp:GridView>
                                    </div>

                                    <div class="table-responsive">
                                        <asp:HiddenField ID="hdnFileNames" runat="server" />
                                        <asp:HiddenField ID="hdnqueryFile" runat="server"></asp:HiddenField>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>               
        <!-- /.content -->
    </div>
    <link href="../Chosen/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../Chosen/chosen.jquery.js" type="text/javascript"></script>
</asp:Content>
