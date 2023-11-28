<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="OTPPendingReportDateWise.aspx.cs" Inherits="Portal_SuperAdmin_OTPPendingReportDateWise_" %>

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



        function ValidatePage() {
            var fDate = $("#ContentPlaceHolder1_TxtFromdate").val();
            var tDate = $("#ContentPlaceHolder1_TxtTodate").val();
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
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    OTP Pending Report Details</h1>
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
                                        <asp:LinkButton ID="lnkPdf" runat="server" CssClass=" fa fa-file-pdf-o" 
                                            title="Export to PDF" OnClick="lnkPdf_Click"></asp:LinkButton></li>
                                    <li><a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                                        <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" 
                                            title="Export to Excel" OnClick="lnkExport_Click"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
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
                                                     <asp:Button ID="btnSearch" CssClass="btn btn btn-add -sm" runat="server" 
                                                            Text="Show" OnClick="btnSearch_Click" OnClientClick="return ValidatePage();"></asp:Button>
                                                    </div>                                                
                                                 
                                                 
                                                 </div>
                                                 </div> 

                                                   </div>

                      
                                                   <div class="table-responsive">
                                        <asp:Label ID="LblSearchDetails" runat="server" Font-Bold="true"></asp:Label>
                                        <div style="display: inline-block; text-align: right; width: 100%">
                                            <asp:LinkButton ID="LbtnAll" runat="server" Visible="false" CssClass="" Text="All" OnClick="LbtnAll_Click"></asp:LinkButton>
                                            
                                            
                                            <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                        </div>
                                                    <div class="table-responsive" id="Div1" runat="server">
                                   <asp:GridView ID="GrdOtp" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
                                            CssClass="table table-bordered table-hover" AllowPaging="true" OnPageIndexChanging="GrdOtp_PageIndexChanging" ShowFooter="false" Width="100%" PageSize="100">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(GrdOtp.PageIndex * GrdOtp.PageSize) + (GrdOtp.Rows.Count + 1)%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Investor Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="investorname" runat="server" Text='<%# Eval("strInvestorName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <asp:Label ID="emailid" runat="server" Text='<%# Eval("strEmailId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact FirstName">
                                                        <ItemTemplate>
                                                            <asp:Label ID="contactfirstname" runat="server" Text='<%# Eval("strContactPersn") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mobileno" runat="server" Text='<%# Eval("strMobile") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="address" runat="server" Text='<%# Eval("strAddress") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 
                                                 <asp:TemplateField HeaderText="User Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Userid" runat="server" Text='<%# Eval("strUserId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="DTM Created On">
                                                        <ItemTemplate>
                                                            <asp:Label ID="dtmcreatedon" runat="server" Text='<%# Eval("DTM_CREATED_ON") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Otp Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Otpstatus" runat="server" Text='<%# Eval("IntOtpStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Pan No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="panno" runat="server" Text='<%# Eval("strPanNo") %>'></asp:Label>
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
                                            CssClass="table table-bordered table-hover" ShowFooter="false" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(GridViewExcel.PageIndex * GridViewExcel.PageSize) + (GridViewExcel.Rows.Count + 1)%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Investor Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="investorname" runat="server" Text='<%# Eval("strInvestorName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <asp:Label ID="emailid" runat="server" Text='<%# Eval("strEmailId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact FirstName">
                                                        <ItemTemplate>
                                                            <asp:Label ID="contactfirstname" runat="server" Text='<%# Eval("strContactPersn") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mobileno" runat="server" Text='<%# Eval("strMobile") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="address" runat="server" Text='<%# Eval("strAddress") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 
                                                 <asp:TemplateField HeaderText="User Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Userid" runat="server" Text='<%# Eval("strUserId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="DTM Created On">
                                                        <ItemTemplate>
                                                            <asp:Label ID="dtmcreatedon" runat="server" Text='<%# Eval("DTM_CREATED_ON") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Otp Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Otpstatus" runat="server" Text='<%# Eval("IntOtpStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                  </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Pan No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="panno" runat="server" Text='<%# Eval("strPanNo") %>'></asp:Label>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

