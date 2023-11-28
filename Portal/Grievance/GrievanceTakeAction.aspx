<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="GrievanceTakeAction.aspx.cs" Inherits="Portal_Grievance_GrievanceTakeAction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/decimalrstr.js" type="text/javascript"></script>
    <style></style>
    <script type="text/javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'

        function DocValid(Controlname) {

            var arr = new Array;
            var arr2 = new Array;
            var arrnew = new Array('pdf');
            var count = 0;
            var y, x, z;

            x = $(Controlname).val();
            z = document.getElementById(Controlname);
            y = x.substring(x.lastIndexOf(".") - 1);
            arr = y.split('.');

            for (var j = 0; j < arrnew.length; j++) {
                if (arr[1] == arrnew[j])
                    count = 1;
            }

            if (count == 0) {
                jAlert('<strong>Please Upload PDF file Only !</strong>', 'SWP');
                return false;
            }
            else if (z.files[0].size > 4 * 1024 * 1024) {
                jAlert('<strong>The file size can not exceed 4MB. !</strong>', 'SWP');
                return false;
            }
            else
                return true;
        }

        /*---------------------------------------------------------------------------------------------------*/

        $(document).ready(function () {

            $('.datePicker').datepicker({
                autoclose: true,
                format: 'dd-M-yyyy'
            });

            /*---------------------------------------------------------------------------------------------------*/

            $(".ddlsts").change(function () {

                if ($(this).val() == "8") {

                    $(this).closest('tr').find('.dist').show();

                }
                else {
                    $(this).closest('tr').find('.dist').hide();
                }
            });

            /*---------------------------------------------------------------------------------------------------*/

            $('.btnsubmit').click(function () {

                var refdoc = $(this).closest('tr').find('.uploadReferencedoc').attr('id');
                if ($(this).closest('tr').find('.uploadReferencedoc').val() != "") {
                    if (!DocValid('#' + refdoc + '')) { return false; }
                }

                if ($(this).closest('tr').find('.ddlsts').val() == "0") {
                    jAlert('<strong>Please Select Status  !</strong>', 'SWP');
                    $(this).closest('tr').find('.ddlsts').focus();
                    return false;
                }

                else if ($(this).closest('tr').find('.ddlsts').val() == "8") {

                    if ($(this).closest('tr').find('.ddldistc').val() == "0") {
                        jAlert('<strong>Please Select District !</strong>', 'SWP');
                        $(this).closest('tr').find('.dist').focus();
                        return false;
                    }
                }
            });
        });

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
                <h1>Grievance Take Action</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Grievance</a></li>
                    <li><a>Take Action</a></li>
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
                                        <div class="col-sm-3">
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
                                       <%-- add by anil sahoo--%>
                                        <asp:TemplateField HeaderText="Industry Type" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Industry_Type" runat="server" Text='<%# Eval("intIndustryCategory") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                        </asp:TemplateField>
                                        <%-- add by anil sahoo--%>
                                        <asp:TemplateField HeaderText="Name Of Company" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Company_Name" runat="server" Text='<%# Eval("VCH_INV_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Applicant Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Applicant_Name" runat="server" Text='<%# Eval("vchApplicantName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>--%>
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
                                        <asp:TemplateField HeaderText=" Grievance Title" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Griv_Title" runat="server" Text='<%# Eval("vchGrivTitle") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="Hid_Status" runat="server" Value='<%# Eval("intStatus") %>' />
                                                <asp:Label ID="Lbl_Status" runat="server" Text='<%# Eval("vchStatusName")  %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apply Date">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Apply_Date" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="6%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action to be Taken By">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Action_By" runat="server" Text='<%# Eval("vchFullName") %>'></asp:Label>
                                                <asp:HiddenField ID="hdnactiontakenby" runat="server" Value='<%# Eval("intActionToBeTakenBy") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="11%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Take Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" Text="Take Action" runat="server" class="label-warning label label-default"
                                                    data-toggle="modal" data-target='<%# "#"+Eval("vchGrivId") %>'>Take Action</asp:LinkButton>
                                                <div class="modal fade" id='<%#Eval("vchGrivId") %>' tabindex="-1" role="dialog"
                                                    aria-hidden="true">
                                                    <div class="modal-dialog modal-lg">
                                                        <div class="modal-content">
                                                            <div class="modal-header modal-header-primary">
                                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                                    ×</button>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <fieldset>
                                                                            <div class="panel panel-bd ">
                                                                                <div class="panel-heading">
                                                                                    Take Action
                                                                                </div>
                                                                                <div class="panel-body">
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-3">
                                                                                                Status</label>
                                                                                            <div class="col-md-5">
                                                                                                <span class="colon">:</span>
                                                                                                <asp:DropDownList ID="drpStatus" runat="server" class="form-control ddlsts">
                                                                                                    <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                                                                                    <asp:ListItem Value="13" Text="Resolved"></asp:ListItem>
                                                                                                    <asp:ListItem Value="3" Text="Rejected"></asp:ListItem>
                                                                                                    <asp:ListItem Value="7" Text="Deferred"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group dist" id="dist" style="display: none;">
                                                                                        <div class="row">
                                                                                            <label class="col-md-3">
                                                                                                District</label>
                                                                                            <div class="col-md-5">
                                                                                                <span class="colon">:</span>
                                                                                                <asp:DropDownList ID="ddldist" runat="server" class="form-control ddldistc">
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-3">
                                                                                                Upload Reference document</label>
                                                                                            <div class="col-md-5">
                                                                                                <span class="colon">:</span>
                                                                                                <asp:FileUpload ID="docUpload" CssClass="form-control uploadReferencedoc" runat="server" />
                                                                                                <span class="text-danger"><small>(Upload only .pdf file, Maximum size 4 MB) </small>
                                                                                                </span>
                                                                                                <asp:HiddenField ID="hdnDoc" runat="server" />
                                                                                                <asp:HyperLink ID="lnkUFName" runat="server" Text="" Target="_blank"></asp:HyperLink>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-3">
                                                                                                Remark</label>
                                                                                            <div class="col-md-5">
                                                                                                <span class="colon">:</span>
                                                                                                <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                                                                    runat="server" Onkeypress="return inputLimiter(event,'Address')">
                                                                                                </asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <div class="col-md-3">
                                                                                            </div>
                                                                                            <div class="col-md-5">
                                                                                                <asp:Button ID="BtnSubmit" runat="server" Text="Save" class="btn btn-add btnsubmit"
                                                                                                    OnClick="BtnSubmit_Click" />
                                                                                                <button type="button" class="btn btn-danger" data-dismiss="modal">
                                                                                                    Cancel</button>
                                                                                                <asp:HiddenField ID="hdnApplicationUnqKey" runat="server" Value='<%#Eval("vchGrivId") %>'></asp:HiddenField>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <%-- <div class="col-md-10 col-sm-offset-2 form-group user-form-group">
                                                                                        <div class="row">
                                                                                        </div>
                                                                                    </div>--%>
                                                                                </div>
                                                                            </div>
                                                                            <!-- Text input-->
                                                                        </fieldset>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">
                                                                    Close</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!-- /.modal-content -->
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="6%"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-grid no-print" />
                                </asp:GridView>
                            </div>
                            <div class="table-responsive">
                                <asp:Button ID="btnDownload" runat="server" Text="Download" Style="display: none"
                                    OnClick="btnDownload_Click" />
                                <asp:HiddenField ID="hdnFileNames" runat="server" />
                                <asp:HiddenField ID="hdnqueryFile" runat="server"></asp:HiddenField>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- customer Modal1 -->
            <!-- /.modal -->
            <!-- Modal -->
        </div>
        <!-- /.content -->
    </div>
    <link href="../Chosen/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../Chosen/chosen.jquery.js" type="text/javascript"></script>
</asp:Content>
