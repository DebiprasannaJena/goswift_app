<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MISAllstatusRptDtlsCount.aspx.cs"
    Inherits="Portal_MISReport_MISAllServiceStatusRptCount" MasterPageFile="~/MasterPage/Application.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../css/chosen.css" rel="stylesheet" />
    <script src="../../js/chosen.jquery.js"></script>
   
    <script language="javascript" type="text/javascript">
        
        function pageLoad() {
            $("#ContentPlaceHolder1_ddlindustry").chosen({ placeholder_text: "Select Industry Name" });
            $("#ContentPlaceHolder1_ddlindustry").chosen({ no_results_text: "Search name not found" });

        }
        function SelectAllCheckboxes(chk) {
            $('#<%=gvunity.ClientID %>').find("input:checkbox").each(function() {
                if (this != chk) {
                    this.checked = chk.checked;
                }
            });
        } 
         function validReport() {
             var frdt = $('#ContentPlaceHolder1_txtFromDate').val();
             var todt = $('#ContentPlaceHolder1_txtToDate').val();
             if (customParse(frdt) > customParse(todt)) {
                 alert('From Date Can Not Be Greater Than To Date');
                 document.getElementById('txtFromDate').focus();
                 return false;
             }            
        }
        function customParse(str) {
                 var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                 var n = months.length;
                 var re = /(\d{2})-([a-z]{3})-(\d{4})/i;
                 var matches;
                 while (n--) {
                     months[months[n]] = n;
                 }
                 matches = str.match(re);
                 return new Date(matches[3], months[matches[2]], matches[1]);
        }


    </script>
     <script src="../js/custom.js" type="text/javascript"></script>
        <script type="text/javascript">
            function ViewModal() {
                $('#DetailsModal').modal();
                 $(".closeModal").click(function () {
                        $(".modal-backdrop").removeClass("in").hide();
                    });
            }
             $(document).ready(function () {
                $('.datePicker').datepicker({
                    format: "dd-M-yyyy",
                    changeMonth: true,
                    changeYear: true,
                    autoclose: true
                });
            });

            function setDateValues(strFromDate, strToDate) {
                var appendId = "ContentPlaceHolder1_";
                var fromDate = $("#" + appendId + "txtFromDate").val();
                var toDate = $("#" + appendId + "txtToDate").val();
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
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                             $('.datePicker').datepicker({
                                    format: "dd-M-yyyy",
                                    changeMonth: true,
                                    changeYear: true,
                                    autoclose: true
                             });
                        }
                    });

                };
        </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    MIS Report Count
                </h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Proposal</a></li><li><a>View</a></li>
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
                                        <asp:LinkButton ID="lnkPdf" runat="server" CssClass=" fa fa-file-pdf-o" title="Export to PDF" OnClick="lnkPdf_Click"></asp:LinkButton></li>
                                    <li><a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                                        <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" title="Export to Excel" OnClick="lnkExport_Click"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
                                    </a></li>
                                    <li><a class="PrintBtn" data-tooltip="Print" data-toggle="tooltip" data-title="Print">
                                        <i class="panel-control-icon fa fa-print"></i></a></li>
                                    <li><a href="javascript:history.back()" data-tooltip="Back" data-toggle="tooltip"
                                        data-title="Back"><i class="panel-control-icon fa  fa-chevron-circle-left"></i></a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>                           
                        <div class="panel-body">
                            <div class="search-sec NOPRINT">
                                <div class="form-group row NOPRINT">
                                    <div class="col-sm-3">
                                        <label for="State">
                                            Industry Name
                                        </label>
                                        <asp:DropDownList ID="ddlindustry" runat="server" CssClass="form-control" TabIndex="3" OnSelectedIndexChanged="ddlindustry_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                                <label for="State">
                                                    From Date
                                                </label>
                                                <div class="input-group  date datePicker">
                                                    <asp:TextBox runat="server" class="form-control" ID="txtFromDate" name="txtFromDate"></asp:TextBox>
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <label for="State">
                                                    To Date
                                                </label>
                                                <div class="input-group  date datePicker">
                                                    <asp:TextBox runat="server" class="form-control" ID="txtToDate"></asp:TextBox>
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                </div>
                                            </div>
                                                                              
                                           
                                </div> 
                                <div class="form-group row NOPRINT">
                                     <div class="col-sm-5">
                                        <asp:GridView ID="gvunity" runat="server" class="table table-bordered table-hover" DataKeyNames="INT_INVESTOR_ID" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" Text="" onclick="javascript:SelectAllCheckboxes(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelectAdd" runat="server" Checked="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblname" runat="server" Text='<%#Eval("VCH_INV_NAME")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblemail" runat="server" Text='<%#Eval("VCH_EMAIL")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Mobile">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("VCH_OFF_MOBILE")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Industry Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblunit" runat="server" Text='<%#Eval("status")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                     <div class="col-sm-2">
                                        <asp:Button ID="btnSearch" Style="margin-top: 22px" CssClass="btn btn btn-add btn-sm" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="validReport();">
                                        </asp:Button> 
                                          <asp:Button ID="btnReset" Style="margin-top: 22px" CssClass="btn btn btn-add btn-sm" runat="server" Text="Reset" OnClick="btnReset_Click">
                                        </asp:Button>
                                    </div>
                                    </div>
                            </div> 
                            <div align="right" >
                                    <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                        OnClick="lbtnAll_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblPaging" runat="server" Visible="false"></asp:Label>
                                </div>
                              <div class="table-responsive" id="viewTable" runat="server">                               
                                <asp:GridView ID="grdPealDetails" runat="server" class="table table-bordered table-hover"
                                    AutoGenerateColumns="false" EmptyDataText="No Record(s) found...." 
                                    DataKeyNames="intleveldetailid" PageSize="20" OnPageIndexChanging="grdPealDetails_PageIndexChanging" AllowPaging="true" OnRowCommand="grdPealDetails_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#">
                                            <ItemTemplate>
                                                 <asp:Label ID="lblsl" runat="server" Text='<%#(grdPealDetails.PageIndex * grdPealDetails.PageSize) + (grdPealDetails.Rows.Count + 1)%>'></asp:Label>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department Name">
                                            <ItemTemplate>                                                
                                             <asp:Label ID="lblDepartmentname" runat="server" Text='<%#Eval("nvchlevelname")%>'></asp:Label>                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Total Application Applied">
                                            <ItemTemplate>  
                                       <asp:LinkButton ID="lnkreceived" runat="server" Text='<%#Eval("cnt_Total")%>' CommandName="Received"></asp:LinkButton>                                                                                      
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Application Pending">
                                            <ItemTemplate>    
                                            <asp:LinkButton ID="lnkpending" runat="server" Text='<%#Eval("cnt_AllTotalPending")%>' CommandName="Pending"></asp:LinkButton>                                                                                         
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Total Application Approved">
                                            <ItemTemplate> 
                                     <asp:LinkButton ID="lnkapproved" runat="server" Text='<%#Eval("cnt_approved")%>' CommandName="Approved"></asp:LinkButton>                                                                                   
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Application Rejected">
                                            <ItemTemplate>  
                                        <asp:LinkButton ID="lnkrejected" runat="server" Text='<%#Eval("cnt_rejected")%>' CommandName="Rejected"></asp:LinkButton>                                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>                                       
                                    </Columns>
                                     <PagerStyle CssClass="pagination-grid no-print" />
                                </asp:GridView>                   
                                
                            </div>


                             <div id="DetailsModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content modal-primary ">
                        <div class="modal-header bg-red">
                            <button type="button" class="close closeModal" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="lbldet1" runat="server" Text=""></asp:Label></h4>
                              </div>
                        <div class="modal-body" style="height: 500px;">                           
                         <asp:GridView ID="grdService" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
            CssClass="table table-bordered table-hover" OnPageIndexChanging="grdService_PageIndexChanging" AllowPaging="true" PageSize="7">
            <Columns>
                <asp:TemplateField HeaderText="Sl#" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblsl" runat="server" Text='<%#(grdService.PageIndex * grdService.PageSize) + (grdService.Rows.Count + 1)%>'></asp:Label>                                                
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:BoundField HeaderText="Application No" DataField="VCH_APPLICATION_UNQ_KEY" ItemStyle-Width="10%" /> 
                <asp:BoundField HeaderText="Service Name" DataField="VCH_SERVICENAME" ItemStyle-Width="10%" /> 
                <asp:BoundField HeaderText="Investor Name" DataField="VCH_INVESTOR_NAME" ItemStyle-Width="10%" />
                <asp:BoundField HeaderText="Applied Date" DataField="dtm_Payment_date" ItemStyle-Width="10%" />               
                <asp:BoundField HeaderText="Payment Amount" DataField="NUM_PAYMENT_AMOUNT" ItemStyle-Width="10%" />                
            </Columns>
        </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default closeModal" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
                        </div>
                                <asp:HiddenField ID="hdnPgindex" runat="server" Value="Blank Value" />
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
       
    </div>
</asp:Content>
