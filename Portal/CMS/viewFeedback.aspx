<%--'*******************************************************************************************************************
' File Name         : viewFeedback.aspx
' Description       : View Feedback
' Created by        : AMit Sahoo
' Created On        : 22 August 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="viewFeedback.aspx.cs" Inherits="Miscellaneous_viewFeedback" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
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
                jAlert('<strong>From date cannot be greater than to date.</strong>', 'GO-SWIFT');
                return false;
            }
            else {
                return true;
            }

        }
    </script>
    <script type="text/javascript">
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
                <h1>View Feedback</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Feedback</a></li>
                    <li><a>View Feedback</a></li>
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
                            <%-- <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="AddCMS.aspx"> 
                              <i class="fa fa-plus"></i>  Add </a>  
                           </div>--%>

                            <div class="btn-group buttonlist">
                                <%-- <a class="btn btn-add " href="viewFeedback.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> ""> 
                                <i class="fa fa-file"></i>View </a>  --%>
                            </div>
                        </div>

                        <div class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="search-sec">
                                        <div class="form-group ">
                                            <div class="row">

                                                <label class="col-sm-3" for="Country">
                                                    Name
                                                </label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="TxtName" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <label class="col-sm-3">
                                                    Subject
                                                </label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="TxtSubject" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-3">
                                                    Email Id</label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtEmail" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
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
                                                    <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" OnClientClick="return validate();" CssClass="btn btn btn-add "
                                                        runat="server" Text="Search"></asp:Button>
                                                    <asp:Button ID="BtnReset" OnClick="BtnReset_Click" CssClass="btn btn btn-danger "
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
                                            <asp:LinkButton ID="LbtnAll" Visible="true" runat="server" Text="All" OnClick="LbtnAll_Click"></asp:LinkButton>
                                            &nbsp;&nbsp;
                                <asp:Label ID="lblPaging" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </div>                                      
                                        <asp:GridView ID="grdFeedback" runat="server" class="table table-bordered table-hover"
                                            AutoGenerateColumns="False" DataKeyNames="intFeedbackId"
                                            OnPageIndexChanging="grdFeedback_PageIndexChanging" PageSize="50" AllowPaging="true" Width="100%">
                                            <Columns>                                               
                                                <asp:TemplateField HeaderText="Sl.No." ItemStyle-Width="20">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Name" DataField="vchFirstName" ItemStyle-Width="15%" />
                                                <asp:BoundField HeaderText="Email Id" DataField="vchEmail" ItemStyle-Width="5%" />
                                                <asp:BoundField HeaderText="Mobile No." DataField="vchMobileNo" ItemStyle-Width="5%" />
                                                <asp:BoundField HeaderText="Subject" DataField="vchSubject" ItemStyle-Width="10%" />
                                                <asp:BoundField HeaderText="Feedback" DataField="vchFeedback" />
                                                <asp:BoundField HeaderText="Date" DataField="StrDate" ItemStyle-Width="10%" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <h5 style="color: red"><b>No Record(s) Available</b></h5>
                                            </EmptyDataTemplate>
                                            <PagerStyle CssClass="pagination-grid no-print" HorizontalAlign="Right" />

                                        </asp:GridView>
                                        <asp:Label ID="lblMessage" runat="server" Text="No Records found" Visible="false"></asp:Label>
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
