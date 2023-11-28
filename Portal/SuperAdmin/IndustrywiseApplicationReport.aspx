<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="IndustrywiseApplicationReport.aspx.cs" Inherits="Portal_SuperAdmin_IndustrywiseApplicationReport"  EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <link href="../Chosen/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../Chosen/chosen.jquery.js" type="text/javascript"></script>
     <asp:ScriptManager ID="sm1" EnablePageMethods="true" runat="server">
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


         function ValidatePage() {
             var fDate = $("#ContentPlaceHolder1_TxtFromDate").val();
             var tDate = $("#ContentPlaceHolder1_TxtToDate").val();
             //if (fDate == null || fDate == undefined || fDate == '') {
             //    jAlert('<strong>Please select from date.</strong>', 'GO-SWIFT');
             //    return false;
             //}
             //if (tDate == null || tDate == undefined || tDate == '') {
             //    jAlert('<strong>Please select to date.</strong>', 'GO-SWIFT');
             //    return false;
             //}
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

    <div class="content-wrapper">
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>Industry Wise Application Report</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Admin Privileges</a></li>
                    <li><a>Industry Report</a></li>
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
                                        <asp:LinkButton ID="LnkExport" OnClick="LnkExport_Click" CssClass="back" runat="server" title="Export to Excel"

><i class="fa fa-file-excel-o"></i></asp:LinkButton>
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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="search-sec NOPRINT">
  
                                        <div class="form-group row NOPRINT">
                                            <div class="col-sm-3">
                                                <label for="Department">
                                                    Industry Name</label>
                                                <asp:DropDownList ID="DdlCompany" runat="server"  CssClass="chosen-select-width ddlCompany"
                                                    Style="width: 100%">
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
                                                <asp:DropDownList ID="DrpDwn_Invest_Level" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Project Cost >= 50 crore" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Project Cost upto < 50 crore" Value="2"></asp:ListItem>
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
                                          <div align="right">
                                            <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All" OnClick="lbtnAll_Click"></asp:LinkButton>
                                            &nbsp;&nbsp;
                                    <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                        </div>

                                        <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover"
                                            AutoGenerateColumns="False" AllowPaging="True" PageSize="100" Width="100%" EmptyDataText="No Record(s) Found..." OnPageIndexChanging="GridView1_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SlNo" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(GridView1.PageIndex * GridView1.PageSize) + (GridView1.Rows.Count + 1)%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="3%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User Id" ItemStyle-HorizontalAlign="Left">
                                                   <ItemTemplate>
                                                        <asp:Label ID="Userid" runat="server" Text='<%# Eval("Str_USER_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Industry Name" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IndustryName" runat="server" Text='<%# Eval("strInvestorName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="6%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Industry Type" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IndustryType" runat="server" Text='<%# Eval("strInvLevel") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. Of Proposal" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                            <asp:Label ID="noofproposal" runat="server" Text='<%# Eval("Int_PROPOSAL_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. Of Service" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                            <asp:Label ID="noofservice" runat="server" Text='<%# Eval("Int_SERVICE_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. Of Incentive" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                            <asp:Label ID="noofIncentive" runat="server" Text='<%# Eval("Int_INCENTIVE_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="8%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. Of Grievance" ItemStyle-HorizontalAlign="Left">
                                                     <ItemTemplate>
                                                            <asp:Label ID="noofGrievance" runat="server" Text='<%# Eval("Int_GRIEVANCE_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="8%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. Of LARGE PC" ItemStyle-HorizontalAlign="Left">
                                                     <ItemTemplate>
                                                            <asp:Label ID="noofLargePc" runat="server" Text='<%# Eval("Int_LARGE_PC_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="8%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. Of Small Pc">
                                                   <ItemTemplate>
                                                            <asp:Label ID="noofSmallPc" runat="server" Text='<%# Eval("Int_SMALL_PC_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="6%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. Of PPC">
                                                     <ItemTemplate>
                                                            <asp:Label ID="noofppc" runat="server" Text='<%# Eval("Int_PPC_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="6%"></ItemStyle>
                                                </asp:TemplateField>
                                       
                                            </Columns>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                        </asp:GridView>

                                        <div runat="server" Visible="false">
                                            <asp:GridView ID="GrdExcel" runat="server"  AutoGenerateColumns="false" Width="100%">
                                                 <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(GrdExcel.PageIndex * GrdExcel.PageSize) + (GrdExcel.Rows.Count + 1)%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="User Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Userid" runat="server" Text='<%# Eval("Str_USER_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Industry Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IndustryName" runat="server" Text='<%# Eval("strInvestorName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="6%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Industry Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IndustryType" runat="server" Text='<%# Eval("strInvLevel") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. Of Proposal">
                                                        <ItemTemplate>
                                                            <asp:Label ID="noofproposal" runat="server" Text='<%# Eval("Int_PROPOSAL_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. Of Sevice">
                                                        <ItemTemplate>
                                                            <asp:Label ID="noofservice" runat="server" Text='<%# Eval("Int_SERVICE_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No. Of Incentive">
                                                        <ItemTemplate>
                                                            <asp:Label ID="noofIncentive" runat="server" Text='<%# Eval("Int_INCENTIVE_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No. Of Grievance ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="noofGrievance" runat="server" Text='<%# Eval("Int_GRIEVANCE_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No. Of LARGE PC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="noofLargePc" runat="server" Text='<%# Eval("Int_LARGE_PC_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. Of Small Pc">
                                                        <ItemTemplate>
                                                            <asp:Label ID="noofSmallPc" runat="server" Text='<%# Eval("Int_SMALL_PC_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No. Of PPC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="noofppc" runat="server" Text='<%# Eval("Int_PPC_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
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
                     
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

