<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="InvestorDetails_Rpt.aspx.cs" Inherits="Portal_MISReport_InvestorDetails_Rpt"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        function pageLoad() {
            $(document).ready(function () {
                $('.ddlCompany').chosen({ allow_single_deselect: true, no_results_text: 'No Item found ' });
            });
        }

    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('.datePicker').datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $('.datePicker').datepicker({
                    format: 'dd-M-yyyy',
                    autoclose: true
                });
            }
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

        /*-----------------------------------------------------------------------*/

        //function ShowGrvstatewise(dstid, status, distname) {
        //    debugger;
        //    var a = document.getElementById("ContentPlaceHolder1_hdnLavelVal").value;
        //    var b = document.getElementById("ContentPlaceHolder1_hdnDesgid").value;
        //    var Act = "VD";
        //    if (dstid == "0") {
        //        var e = document.getElementById("ContentPlaceHolder1_ddlDistrict");
        //        var dstid = e.options[e.selectedIndex].value;
        //    }
        //    else {
        //        var dstid = dstid;
        //    }
        //    var status = status;

        //    var fDate = $("#ContentPlaceHolder1_TxtFromDate").val();
        //    var tDate = $("#ContentPlaceHolder1_TxtToDate").val();

        //    var e = document.getElementById("ContentPlaceHolder1_drpGrievance");
        //    var Gtypeid = e.options[e.selectedIndex].value;

        //    document.getElementById('ContentPlaceHolder1_myIframe').src = "GrivMisDrillDownStatusRpt.aspx?dstid=" + dstid + "&distname=" + distname + "&Act=" + Act + "&Gtypeid=" + Gtypeid + "&fDate=" + fDate + "&tDate=" + tDate + "&status=" + status + "&Logic=" + 1;


        //}

        /*-----------------------------------------------------------------------*/

        function ValidatePage() {
            var fDate = $("#ContentPlaceHolder1_TxtFromDate").val();
            var tDate = $("#ContentPlaceHolder1_TxtToDate").val();
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
    <div class="content-wrapper">
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>Investor Details</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>MISReport</a></li>
                    <li><a>Investor Details</a></li>
                </ul>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="dropdown">
                                <ul class="dropdown-menu dropdown-menu-right">
                                    <li><a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                                        <asp:LinkButton ID="LnkExport" CssClass="back" runat="server" title="Export to Excel"
                                            OnClick="LnkExport_Click"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
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
                                            <div class="col-sm-3">
                                                <label for="Department">
                                                    District</label>
                                                <asp:DropDownList ID="DdlDistrict" runat="server" AutoPostBack="true" CssClass="form-control"
                                                    OnSelectedIndexChanged="DdlDistrict_SelectedIndexChanged">
                                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="Service">
                                                    Block</label>
                                                <asp:DropDownList ID="DdlBlock" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="State">
                                                    Sector
                                                </label>
                                                <asp:DropDownList ID="DdlSector" runat="server" CssClass="form-control" OnSelectedIndexChanged="DdlSector_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="State">
                                                    Subsector
                                                </label>
                                                <asp:DropDownList ID="DdlSubsector" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row NOPRINT">
                                            <div class="col-sm-3">
                                                <label for="Department">
                                                    Company Name</label>
                                                <asp:DropDownList ID="DdlCompany" runat="server" CssClass="chosen-select-width ddlCompany"
                                                    Style="width: 100%">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="Pan">
                                                    PAN No</label>
                                                <asp:TextBox ID="TxtPanNo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="Industry Type">
                                                    Industry Type
                                                </label>
                                                <asp:DropDownList ID="DdlIndustryType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Industry" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Non Industry" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="Registration  Source">
                                                    Registration  Source
                                                </label>
                                                <asp:DropDownList ID="DdlRegdSource" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="GOSWIFT" Value="GOSWIFT"></asp:ListItem>
                                                    <asp:ListItem Text="NSWS" Value="NSWS"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>


                                        </div>
                                        <div class="form-group row NOPRINT">
                                            <div class="col-sm-3">
                                                <label for="State">
                                                    From Date
                                                </label>
                                                <div class="input-group date datePicker">
                                                    <asp:TextBox runat="server" class="form-control" ID="TxtFromDate" name="txtFromDate"></asp:TextBox>
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                </div>
                                            </div>
                                            <div class="col-sm-3" runat="server" id="Div5">
                                                <label for="State">
                                                    To Date
                                                </label>
                                                <div class="input-group date datePicker">
                                                    <asp:TextBox runat="server" class="form-control" ID="TxtToDate"></asp:TextBox>
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                </div>
                                            </div>
                                            <div class="col-sm-3" runat="server" >
                                                <label for="State">
                                                    Investment Level
                                                </label>
                                                <asp:DropDownList ID="DrpDwn_Invest_Level" runat="server" CssClass="form-control"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Project Cost >= 50 crore" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Project cost upto < 50 crore" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                             <div class="col-sm-3" runat="server" >
                                                 <label for="EINIEM" >
                                                     EIN / IEM Number
                                                 </label>
                                                 <asp:DropDownList ID="DrpDwn_License_Type" runat="server" CssClass="form-control"
                                                     onchange="return showDocName();">
                                                     <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                     <asp:ListItem Value="1">EIN</asp:ListItem>
                                                     <asp:ListItem Value="2">IEM</asp:ListItem>
                                                     <asp:ListItem Value="3">Production Certificate</asp:ListItem>
                                                     <asp:ListItem Value="4">Udyog Aadhaar</asp:ListItem>
                                                     <asp:ListItem Value="5">Udyam Registration</asp:ListItem>
                                                 </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Button ID="BtnSearch" Style="margin-top: 22px" CssClass="btn btn btn-add btn-sm"
                                                    runat="server" Text="Search" OnClick="BtnSearch_Click" OnClientClick="return ValidatePage();"></asp:Button>
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="table-responsive">
                                        <asp:Label ID="LblSearchDetails" runat="server" Font-Bold="true"></asp:Label>
                                        <div style="display: inline-block; text-align: right; width: 100%">
                                            <asp:LinkButton ID="LbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                                OnClick="LbtnAll_Click"></asp:LinkButton>
                                            &nbsp;&nbsp;
                                            
                                            <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                        </div>

                                        <asp:GridView ID="GrdInvestor" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
                                            CssClass="table table-bordered table-hover" OnPageIndexChanging="GrdInvestor_PageIndexChanging"
                                            AllowPaging="true" ShowFooter="false" Width="100%" PageSize="100" OnRowDataBound="GrdInvestor_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(GrdInvestor.PageIndex * GrdInvestor.PageSize) + (GrdInvestor.Rows.Count + 1)%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="District">
                                                    <ItemTemplate>
                                                        <asp:Label ID="districtName" runat="server" Text='<%# Eval("vchDistrictName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Block">
                                                    <ItemTemplate>
                                                        <asp:Label ID="BlockName" runat="server" Text='<%# Eval("vchBlockName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sector">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SectorName" runat="server" Text='<%# Eval("vchSectorName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sub Sector">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SubSector" runat="server" Text='<%# Eval("vchSubSectorName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                    </asp:TemplateField>
                                                <asp:BoundField HeaderText="Unit Name" DataField="strInvestorName" FooterStyle-Font-Bold="true"
                                                    ItemStyle-Width="15%" />
                                                <%--<asp:BoundField HeaderText="User Id" DataField="strUserId" FooterStyle-Font-Bold="true"
                                                    ItemStyle-Width="15%" />--%>
                                                
                                                <asp:TemplateField HeaderText="Details">
                                                    <ItemTemplate>

                                                        <table width="100%">

                                                            <tr>
                                                                <td width="23%">User Id
                                                                </td>
                                                                <td width="1%">:</td>
                                                                <td width="26%">
                                                                    <asp:Label ID="Lbl_User_Id" runat="server" Text='<%# Eval("strUserId") %>' Font-Bold="true" ForeColor="#B71F48"></asp:Label>
                                                                </td>
                                                                <td width="23%">Email Id
                                                                </td>
                                                                <td width="1%">:
                                                                </td>
                                                                <td width="26%">
                                                                    <asp:Label ID="Lbl_Email_Id" runat="server" Text='<%# Eval("strEmailId") %>' Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Industry Type</td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:HiddenField ID="HdnIndustryType" runat="server" Value='<%# Eval("IntIndustryType") %>' />
                                                                    <asp:Label ID="Lbl_Industry_Type" runat="server" Text='<%# Eval("StrIndustyType") %>' Font-Bold="true"></asp:Label></td>
                                                                <td>Mobile No
                                                                </td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:Label ID="Lbl_Mobile_No" runat="server" Text='<%# Eval("strMobile") %>' Font-Bold="true"></asp:Label>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td>Registration  Source</td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:Label ID="Lbl_RegdSourc" runat="server" Text='<%# Eval("StrRegdSource") %>' Font-Bold="true"></asp:Label></td>
                                                                <td>Contact Person </td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:Label ID="Lbl_ContPerson" runat="server" Text='<%# Eval("strContactPersn") %>' Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>EIN/IEM
                                                                </td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:Label ID="Lbl_EIN_IEM" runat="server" Text='<%# Eval("VCH_EIN_IEM") %>' Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td>Regd. Date
                                                                </td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:Label ID="Lbl_Regd_Date" runat="server" Text='<%# Eval("DTM_CREATED_ON" ,"{0:dd-MMM-yyyy}") %>' Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Address </td>
                                                                <td>: </td>
                                                                <td colspan="4">
                                                                    <asp:Label ID="Lbl_PAN" runat="server" Text='<%# Eval("strAddress") %>' Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>

                                                        </table>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                        </asp:GridView>

                                        <div runat="server" visible="false">
                                            <asp:GridView ID="GrdExcel" runat="server" AutoGenerateColumns="false" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsl" runat="server" Text='<%# (Container.DataItemIndex + 1)%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="District">
                                                        <ItemTemplate>
                                                            <asp:Label ID="districtName" runat="server" Text='<%# Eval("vchDistrictName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Block">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BlockName" runat="server" Text='<%# Eval("vchBlockName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Sector">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SectorName" runat="server" Text='<%# Eval("vchSectorName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sub Sector">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SubSector" runat="server" Text='<%# Eval("vchSubSectorName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                    </asp:TemplateField>

                                                    <asp:BoundField HeaderText="Unit Name" DataField="strInvestorName" FooterStyle-Font-Bold="true"
                                                        ItemStyle-Width="15%" />
                                                    <asp:BoundField HeaderText="User Id" DataField="strUserId" FooterStyle-Font-Bold="true"
                                                        ItemStyle-Width="15%" />
                                                    <asp:TemplateField HeaderText="Email Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Email_Id" runat="server" Text='<%# Eval("strEmailId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mobile No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Mobile_No" runat="server" Text='<%# Eval("strMobile") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_PAN" runat="server" Text='<%# Eval("strAddress") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact Person">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("strContactPersn") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EIN/IEM Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_EIN_IEM" runat="server" Text='<%# Eval("StrLicenceNoType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EIN/IEM No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_EIN_IEM" runat="server" Text='<%# Eval("VCH_EIN_IEM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Regd. Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Regd_Date" runat="server" Text='<%# Eval("DTM_CREATED_ON" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Industry Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Industry_Type" runat="server" Text='<%# Eval("StrIndustyType") %>' Font-Bold="true"></asp:Label></td>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Registration  Source">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_RegdSourc" runat="server" Text='<%# Eval("StrRegdSource") %>' Font-Bold="true"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Investment Level">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_InvLvl" runat="server" Text='<%# Eval("strInvLevel") %>' Font-Bold="true"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pagination-grid no-print" />
                                            </asp:GridView>
                                        </div>

                                        <div style="float: right;" class="noPrint" id="divPaging" runat="server">
                                            <asp:HiddenField ID="hdnPgindex" runat="server" Value="Blank Value" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSearch" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <link href="../Chosen/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../Chosen/chosen.jquery.js" type="text/javascript"></script>
</asp:Content>
