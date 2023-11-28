<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="MISFinancialReport.aspx.cs" Inherits="Portal_MISReport_MISFinancialReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/custom.js" type="text/javascript"></script>
   
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
        })
           

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

            /*-----------------------------------------------------------------------*/


            function ValidatePage() {
                var fDate = $("#ContentPlaceHolder1_txtFromDate").val();
                var tDate = $("#ContentPlaceHolder1_txtToDate").val();
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
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        
        
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    Financial Report
                </h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Proposal</a></li><li><a>Financial Report</a></li></ul>
            </div>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="dropdown">
                                <ul class="dropdown-menu dropdown-menu-right">
                                    <li>
                                       <%-- <asp:LinkButton ID="lnkPdf" CssClass="back" runat="server" title="Export to PDF"
                                            OnClick="lnkPdf_Click"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>--%>
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
                            <div class="search-sec NOPRINT">
                                <div class="form-group row NOPRINT">
                                   
                                       
                                            <div class="col-sm-3">
                                                <label for="Department">
                                                    Investment Amount</label>
                                                <asp:DropDownList ID="drpInvestmentAmount" runat="server"  CssClass="form-control" >
                                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>                                                   
                                                    <asp:ListItem Text="Above 50 Cr" Value="50000"></asp:ListItem>
                                                    <asp:ListItem Text="Less than equal to 5 Cr and Above 3 Cr" Value="20000"></asp:ListItem>
                                                    <asp:ListItem Text="Less than equal to 3 Cr" Value="1000"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="Service">
                                                    Fees </label>
                                                <asp:DropDownList ID="drpFees" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="100000" Value="100000"></asp:ListItem>
                                                    <asp:ListItem Text="50000" Value="50000"></asp:ListItem>
                                                    <asp:ListItem Text="20000" Value="20000"></asp:ListItem>
                                                    <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                       <div class="col-sm-3">
                                            <label for="Service">
                                                    Status </label>
                                           <asp:DropDownList CssClass="form-control" TabIndex="16" ID="ddlStatus" runat="server">
                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                            <asp:ListItem Value="2"> Approved </asp:ListItem>
                                            <asp:ListItem Value="3"> Reject </asp:ListItem>
                                            <asp:ListItem Value="7"> Deferred </asp:ListItem>
                                            <asp:ListItem Value="8"> Forwarded </asp:ListItem>
                                        </asp:DropDownList>
                                       </div>



                                   
                                    

                                </div>
                                <div class="form-group row NOPRINT">
                                    <div class="col-sm-3" >
                                        <label for="State">
                                            From Date
                                        </label>
                                        <div class="input-group date datePicker">
                                            <asp:TextBox runat="server" class="form-control" ID="txtFromDate" name="txtFromDate"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                    </div>
                                    <div class="col-sm-3" >
                                        <label for="State">
                                            To Date
                                        </label>
                                        <div class="input-group date datePicker">
                                            <asp:TextBox runat="server" class="form-control" ID="txtToDate"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <asp:Button ID="btnSearch" Style="margin-top: 22px" CssClass="btn btn btn-add btn-sm"
                                            runat="server" Text="Search"  OnClick="btnSearch_Click" OnClientClick="return ValidatePage();"></asp:Button>
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

                                <asp:Label ID="lblSearchDetails" runat="server"></asp:Label>
                                <asp:GridView ID="grdFinancialReport" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
                                     CssClass="table table-bordered table-hover" AllowPaging="True" PageSize="10"  OnPageIndexChanging="grdFinancialReport_PageIndexChanging" ShowFooter="true" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#">
                                            <ItemTemplate>
                                             <asp:Label ID="lblsl" runat="server" Text='<%#(grdFinancialReport.PageIndex * grdFinancialReport.PageSize) + (grdFinancialReport.Rows.Count + 1)%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="3%"></ItemStyle>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Proposal No" >
                                            <ItemTemplate>
                                                 <asp:Label ID="lblProposalno" runat="server" Text='<%#Eval("strProposalnumber")%>'></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalproposal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Investment Amount (Rs)" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvestmentAmount"  runat="server" Text='<%#Eval("strInvestment_Amount")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fees (Rs)" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblFees"  runat="server" Text='<%#Eval("decFee")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                            <FooterTemplate >
                                                <asp:Label ID="lblfee" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"  FooterStyle-HorizontalAlign="Right" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblStatus"  runat="server" Text='<%#Eval("strStatus")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>

                                 <div class="table-responsive" id="Div2" runat="server" visible="false">
                                <asp:GridView ID="grdExcel" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
                                     CssClass="table table-bordered table-hover"  >
                                    <Columns>
                                        
                                        <asp:TemplateField HeaderText="Proposal No" >
                                            <ItemTemplate>
                                                 <asp:Label ID="lblProposalno" runat="server" Text='<%#Eval("strProposalnumber")%>'></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Investment Amount (Rs)" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvestmentAmount"  runat="server" Text='<%#Eval("strInvestment_Amount")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fees (Rs)" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblFees"  runat="server" Text='<%#Eval("decFee")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"  FooterStyle-HorizontalAlign="Right" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblStatus"  runat="server" Text='<%#Eval("strStatus")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>

                                 </div>


                            </div>
                        </div>
                        


                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>


