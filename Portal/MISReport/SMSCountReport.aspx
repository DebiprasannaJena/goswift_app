<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="SMSCountReport.aspx.cs" Inherits="Portal_MISReport_SMSCountReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/decimalrstr.js" type="text/javascript"></script>

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



        //function ValidatePage() {
        //    var fDate = $("#ContentPlaceHolder1_TxtFromdate").val();
        //    var tDate = $("#ContentPlaceHolder1_TxtTodate").val();
        //    //if (fDate == null || fDate == undefined || fDate == '') {
        //    //    jAlert('<strong>Please select from date.</strong>', 'GO-SWIFT');
        //    //    return false;
        //    //}
        //    //if (tDate == null || tDate == undefined || tDate == '') {
        //    //    jAlert('<strong>Please select to date.</strong>', 'GO-SWIFT');
        //    //    return false;
        //    //}
        //    var dtmFromDate = new Date(GetDate(fDate));
        //    var dtmToDate = new Date(GetDate(tDate));

        //    if (dtmFromDate > dtmToDate) {
        //        jAlert('<strong>From date cannot be greater than to date.</strong>', 'GO-SWIFT');
        //        return false;
        //    }
        //    else {
        //        return true;
        //    }
           
        // }

         function ValidatePage() {
             if (new Date($('#ContentPlaceHolder1_TxtTodate').val()) > new Date()) {
                 jAlert('<strong>From date cannot be greater than current date.</strong>', projname);
                 $("#popup_ok").click(function () { $("#ContentPlaceHolder1_TxtFromdate").focus(); });
                 return false;
             }
             if (new Date($('#ContentPlaceHolder1_TxtTodate').val()) > new Date()) {
                 jAlert('<strong>To date cannot be greater than current date.</strong>', projname);
                 $("#popup_ok").click(function () { $("#ContentPlaceHolder1_TxtTodate").focus(); });
                 return false;
             }
             if (new Date($('#ContentPlaceHolder1_TxtFromdate').val()) > new Date($('#ContentPlaceHolder1_TxtTodate').val())) {
                 jAlert('<strong>From date cannot be greater than to date.</strong>', projname);
                 $("#popup_ok").click(function () { $("#ContentPlaceHolder1_TxtFromdate").focus(); });
                 return false;
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
   
    <style type="text/css">
    .bold-cell {
        font-weight: bold;
    }
   </style>
      <div class="content-wrapper">
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    SMS Count Report Details</h1>
            </div>
        </section>
        <section class="content">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
        <div class="row">

                    <div class="col-sm-12 " data-lobipanel-child-inner-id="CgbyYkSXVZ">
                    <div class="panel panel-bd lobidisable">
                    <div class="panel-heading noprint">
                     <div class="dropdown">

                     <ul class="dropdown-menu dropdown-menu-right">
                                <li>
                                        <asp:LinkButton ID="lnkPdf" OnClick="lnkPdf_Click" runat="server" CssClass=" fa fa-file-pdf-o" 
                                            title="Export to PDF" ></asp:LinkButton></li>
                                    <li><a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                                        <asp:LinkButton ID="lnkExport" OnClick="lnkExport_Click" CssClass="back" runat="server" 
                                            title="Export to Excel" ><i class="fa fa-file-excel-o"></i></asp:LinkButton>
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
                     <div class="search-sec"> 

                                                <div class="form-group"> 
                                                 <div class="row"> 

                                                     <label class="col-sm-1">
                                                  SMS Type</label>
                                                      <div class="col-sm-3">
                                                          <span class="colon">:</span>
                                                     <asp:ListBox ID="lbSmsType" runat="server" SelectionMode="Multiple" CssClass="form-control" ></asp:ListBox>
                                                            
                                                          </div>

                                                    <label class="col-sm-1">
                                                   From Date</label>

                                                   <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker">
                                                            <asp:TextBox ID="TxtFromdate" CssClass="form-control" runat="server" TabIndex="1" AutoComplete="Off"></asp:TextBox>
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>

                                                    <label class="col-sm-1">
                                                  To Date</label>

                                                  <div class="col-sm-2">
                                                 <span class="colon">:</span>
                                                 <div class="input-group  date datePicker">
                                                     <asp:TextBox ID="TxtTodate"  CssClass="form-control" runat="server" TabIndex="2" AutoComplete="Off"></asp:TextBox>
                                                     <span class="input-group-addon"><i class="fa fa-calendar"></i></span>                                                     
                                                 </div>                                                 
                                                </div> 

                                                     
                                                            
                                                    <div class="col-sm-1">                                                    
                                                     <asp:Button ID="btnSearch" CssClass="btn btn btn-add -sm" OnClick="btnSearch_Click" runat="server" 
                                                            Text="Show"  OnClientClick="return ValidatePage();"></asp:Button>
                                                    </div>                                                
                                                 
                                                 
                                                 </div>
                                                 </div> 

                                                   </div>

                      
                                                   <div class="table-responsive">
                                        <asp:Label ID="LblSearchDetails" runat="server" Font-Bold="true"></asp:Label>
                                        <div style="display: inline-block; text-align: right; width: 100%">
                                            <asp:LinkButton ID="LbtnAll" runat="server" OnClick="LbtnAll_Click" Visible="true" CssClass="" Text="All" ></asp:LinkButton>
                                            
                                            
                                            <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                        </div>
                                                    <div class="table-responsive" id="Div1" runat="server">
                                   <asp:GridView ID="GrdSMS" runat="server" AutoGenerateColumns="false" OnRowDataBound="GrdSMS_RowDataBound" EmptyDataText="No Records Found...."
                                            CssClass="table table-bordered table-hover"  AllowPaging="true" OnPageIndexChanging="GrdSMS_PageIndexChanging"  ShowFooter="true" Width="100%" PageSize="100">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(GrdSMS.PageIndex * GrdSMS.PageSize) + (GrdSMS.Rows.Count + 1)%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" />
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="SMS Type">
                                                    <ItemTemplate>
                                                        <%--<asp:HyperLink ID="hypSMSType" runat="server" Text='<%#Eval("StrSMSType")%>'></asp:HyperLink>--%>
                                                        <asp:Label ID="SMSType" runat="server" Text='<%# Eval("StrSMSType") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField> 
                                                  <asp:TemplateField HeaderText="SMS Count">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="HidStrSMSType" runat="server" Value='<%# Eval("StrSMSType") %>' />
                                                         <asp:HyperLink ID="hypNumberofSMS" runat="server" Text='<%#Eval("intNumberofSMS")%>'></asp:HyperLink>
                                                        <%--<asp:Label ID="NumberofSMS" runat="server" Text='<%# Eval("intNumberofSMS") %>'></asp:Label>--%>
                                                   </ItemTemplate>
                                                     
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                           </asp:GridView>
                                             </div>
                                           </div>
                           <div class="table-responsive" id="Div2" runat="server" visible="false">
                                   <asp:GridView ID="GridViewExcel" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
                                            CssClass="table table-bordered table-hover" AllowPaging="false" PageSize="100"  ShowFooter="true" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(GridViewExcel.PageIndex * GridViewExcel.PageSize) + (GridViewExcel.Rows.Count + 1)%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" />
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="SMS Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SMSType" runat="server" Text='<%# Eval("StrSMSType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField> 
                                                
                                                 <asp:TemplateField HeaderText="SMS Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="NumberofSMS" runat="server" Text='<%# Eval("intNumberofSMS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                           </asp:GridView>
                                             </div>

                                         </div>
                                     </ContentTemplate>
                                   </asp:UpdatePanel>
                  
                                  </div>
                                  </div>

                               </div>
                          </section>
                        </div>
</asp:Content><asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

