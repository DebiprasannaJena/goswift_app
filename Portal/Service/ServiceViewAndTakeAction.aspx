<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ServiceViewAndTakeAction.aspx.cs" Inherits="Service_ServiceViewAndTakeAction" %>

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
        });

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

        function htmlUnescape(value) {
            return String(value)
                .replace(/&quot;/g, '"')
                .replace(/&#39;/g, "'")
                .replace(/&lt;/g, '<')
                .replace(/&gt;/g, '>')
                .replace(/&amp;/g, '&');
        }
        function setvaluesOfrowRaise(flu) {
            var a = flu.offsetParent.parentNode.rowIndex;
            var rows;
            rows = a - 2

            document.getElementById('ContentPlaceHolder1_gvService_txtQueryRemarks_' + rows).value = "";

            document.getElementById('divResults').innerHTML = "";
            document.getElementById('divResults').innerHTML = htmlUnescape(document.getElementById('ContentPlaceHolder1_gvService_hdnremark_' + rows).value);
            document.getElementById('ContentPlaceHolder1_hdnQryServiceId').value = document.getElementById('ContentPlaceHolder1_gvService_hdnServiceId2_' + rows).value;
            document.getElementById('ContentPlaceHolder1_hdnQryApplicationUnqKey').value = document.getElementById('ContentPlaceHolder1_gvService_hdnApplicationUnqKey_' + rows).value;

        }
        function setvaluesOfrow(flu) {

            var a = flu.offsetParent.parentNode.rowIndex;
            var rows;
            rows = a - 2
            document.getElementById('divdemandAmt').style.display = "none";

            // document.getElementById('ContentPlaceHolder1_hdnServiceId1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnServiceId2_' + rows).value;
            // document.getElementById('ContentPlaceHolder1_hdnServiceId1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnServiceId_' + rows).value;
            //            document.getElementById('ContentPlaceHolder1_hdnApplicationUnqKey1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnApplicationUnqKey_' + rows).value;
            //            document.getElementById('ContentPlaceHolder1_hdnProposalId1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnProposalId_' + rows).value;
            //            document.getElementById('ContentPlaceHolder1_hdnInvesterName1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnInvesterName_' + rows).value;
            //            document.getElementById('ContentPlaceHolder1_hdnActionTakenBy1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnActionTakenBy2_' + rows).value;
            //            document.getElementById('ContentPlaceHolder1_hdnActionTobeTakenBy1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnActionTobeTakenBy2_' + rows).value;
            //            document.getElementById('ContentPlaceHolder1_hdnlevel1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnlevel_' + rows).value;
            //            document.getElementById('lblInvestorName').innerHTML = document.getElementById('ContentPlaceHolder1_gvService_hdnInvesterName_' + rows).value;
            //            document.getElementById('lblProposalId').innerHTML = document.getElementById('ContentPlaceHolder1_gvService_hdnProposalId_' + rows).value;
            //            document.getElementById('lblActiontobetaken').innerHTML = document.getElementById('ContentPlaceHolder1_gvService_hdnActionTakentobetaken_' + rows).value;
            //            document.getElementById('lblActiontakenby').innerHTML = document.getElementById('ContentPlaceHolder1_gvService_hdnActiontakenby_' + rows).value;
            //            document.getElementById('lblIndustriesName').innerHTML = document.getElementById('ContentPlaceHolder1_gvService_hdnIndustryName_' + rows).value;
        }

        function inputLimiter(e, allow) {
            var AllowableCharacters = '';

            if (allow == 'NameCharacters') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'NameCharactersAndNumbers') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'Numbers') {
                AllowableCharacters = '1234567890';
            }
            if (allow == 'Decimal') {
                AllowableCharacters = '1234567890.';
            }
            if (allow == 'Email') {
                AllowableCharacters = '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@@._';
            }
            if (allow == 'Address') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#-,./;\'';
            }
            if (allow == 'DateFormat') {
                AllowableCharacters = '1234567890/-';
            }
            if (allow == 'NumbersSSN') {
                AllowableCharacters = '1234567890-';
            }
            var k;
            k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
            if (k != 13 && k != 8 && k != 0) {
                if ((e.ctrlKey == false) && (e.altKey == false)) {
                    return (AllowableCharacters.indexOf(String.fromCharCode(k)) != -1);
                }
                else {
                    return true;
                }
            }
            else {
                return true;
            }
        }
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
        function validate(obj) {
            var ID = obj.id;
            var arr = ID.split('_');
            if ($('#ContentPlaceHolder1_gvService_txtQueryRemarks_' + arr[3]).val() == "") {
                jAlert('<strong>Query cannot be left blank!</strong>', projname);
                $('#gvProposal_txtA1_' + arr[3]).focus();
                return false;
            }

            if ($('#ContentPlaceHolder1_gvService_docqueryUpload_' + arr[3]).val() != "") {
                if (!DocValid('#ContentPlaceHolder1_gvService_docqueryUpload_' + arr[3])) { return false; }
            }
        }
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
        function setvalue(obj) {
            var ID = obj.id;
            var arr = ID.split('_');
            $('#charsLeft').html(1000 - $('#ContentPlaceHolder1_gvService_txtQueryRemarks_' + arr[3]).val().length);

        }
        $(document).ready(function () {
            $("a").click(function (event) {
                var href = $(this).attr('href');
                //$(this).attr('href', '#');
                var Filename = href.split('/');
                if (Filename[4] != undefined) {
                    if (Filename[4].indexOf('.pdf') > -1) {
                        $('#ContentPlaceHolder1_hdnFileNames').val(Filename[4]);
                        document.getElementById('<%= btnDownload.ClientID %>').click();
                        event.preventDefault();
                    }
                };


            });
            $('.ddluser').chosen({ allow_single_deselect: true, no_results_text: 'No Item found for ' });

            $(".ddlsts").change(function () {


                $(this).closest('tr').find('.certificateupl').hide();
                $(this).closest('tr').find('.fwd').hide();
                $(this).closest('tr').find('.dmdamt').hide();
                $(this).closest('tr').find('.divappno').hide();
                if ($(this).val() == "2") {

                    $(this).closest('tr').find('.certificateupl').show();
                    if ($(this).closest('tr').find('input[name$="hdnServId"]').val() == "18") {
                        $(this).closest('tr').find('.divappno').show();
                    }

                }
                if ($(this).val() == "8") {

                    $(this).closest('tr').find('.fwd').show();
                    $(this).closest('tr').find('.divappno').hide();
                }

                if ($(this).val() == "9") {

                    $(this).closest('tr').find('.dmdamt').show();
                    $(this).closest('tr').find('.divappno').hide();
                }


            });
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
                else if ($(this).closest('tr').find('.ddlsts').val() == "2") {
                    var appcert = $(this).closest('tr').find('.docUpload').attr('id');
                    if ($(this).closest('tr').find('input[name$="hdnServId"]').val() == "18") {

                        if ($(this).closest('tr').find('.genappno').val() == "") {
                            jAlert('<strong>Please Enter Application No!</strong>', 'SWP');
                            $(this).closest('tr').find('.genappno').focus();
                            return false;
                        }
                    }

                    if ($(this).closest('tr').find('.docUpload').val() != "") {
                        if (!DocValid('#' + appcert + '')) { return false; }
                    }
                    else {
                        jAlert('<strong>Please Upload Approval Certificate  !</strong>', 'SWP');
                        $(this).closest('tr').find('.docUpload').focus();
                        return false;
                    }
                }
                else if ($(this).closest('tr').find('.ddlsts').val() == "8") {

                    if ($(this).closest('tr').find('.ddluser').val() == "0") {
                        jAlert('<strong>Please Select User !</strong>', 'SWP');
                        $(this).closest('tr').find('.ddluser').focus();
                        return false;
                    }
                }
                else if ($(this).closest('tr').find('.ddlsts').val() == "9") {

                    if ($(this).closest('tr').find('.demandgen').val() == "") {
                        jAlert('<strong>Please Enter Estimated Amount!</strong>', 'SWP');
                        $(this).closest('tr').find('.demandgen').focus();
                        return false;
                    }
                    else {

                        if (parseFloat($(this).closest('tr').find('.demandgen').val()) == 0.00) {
                            jAlert('<strong>Please Enter Estimated Amount!</strong>', 'SWP');
                            $(this).closest('tr').find('.demandgen').focus();
                            return false;
                        }
                    }
                }
            });
        });
    </script>
    <style>
        .control-label
        {
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
        .chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
        
        .chosen-container .chosen-container-single .chosen-single
        {
            width: 100% !important;
        }
        
        .searchbox
        {
            background-color: #def3ff;
            padding: 8px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>Services</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Take Action</a></li>
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
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="ServiceViewAndTakeAction.aspx"><i class="fa fa-plus"></i>
                                    Take Action</a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="ViewServiceApplication.aspx"><i class="fa fa-file"></i>
                                    View</a>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="search-sec">
                                <%--   <div class="form-group">
                              <div class="row">
                              
                             
                                 <label class="col-sm-2">Department</label>  <div class="col-sm-3">
                                 <span class="colon">:</span>
                                  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                  <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control dpt"  onselectedindexchanged="ddldept_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                     </ContentTemplate>
                                  
                                 </asp:UpdatePanel>
                                          </div>
                                
                                          <label class="col-sm-2">Service Sector</label><div class="col-sm-3"><span class="colon">:</span>
                                          <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                 <asp:DropDownList ID="ddlService" runat="server" CssClass="form-control" 
                                       >
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Aluminium Industry" Value="1" />
                                            <asp:ListItem Text="Cement, Lime and Plaster" Value="2" />
                                            <asp:ListItem Text="Chemicals and Chemical products" Value="3" />
                                        </asp:DropDownList>
                                         </ContentTemplate>
                                  <triggers>
                                    <asp:asyncpostbacktrigger controlid="ddldept"  />
                                   
                                    </triggers>
                                 </asp:UpdatePanel>
                                 </div>
                                 </div>
                                 </div>--%>
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-2">
                                            Application No</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtAppno" MaxLength="20" CssClass="form-control" runat="server"
                                                Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2">
                                            Proposal No</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtProposalno" MaxLength="20" CssClass="form-control" runat="server"
                                                Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
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
                                                <asp:TextBox ID="txtFromdate" CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                        </div>
                                        <label class="col-sm-2">
                                            To Date</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <div class="input-group  date datePicker">
                                                <asp:TextBox ID="txtTodate" CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Button ID="btnShow" runat="server" Text="Search" class="btn btn-add btn-sm"
                                                OnClick="btnShow_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <div style="display: inline-block; text-align: right; width: 100%">
                                    <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                        OnClick="lbtnAll_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                </div>
                                <%--  <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>--%>
                                <asp:GridView ID="gvService" class="table table-bordered table-hover" runat="server"
                                    AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvService_PageIndexChanging"
                                    Width="100%" EmptyDataText="No Record(s) Found..." DataKeyNames="intServiceId,strApplicationUnqKey,intActionTobeTakenBy,Deptid,intQueryStatus,strQueryStatus,strProposalId"
                                    OnRowDataBound="gvService_RowDataBound" OnRowCreated="gvService_RowCreated">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Sl No.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Application No.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkTkn" ForeColor="Blue" Text='<%#Eval("strApplicationUnqKey") %>'
                                                    runat="server" ToolTip="ApplicationNo"></asp:HyperLink>
                                                <asp:HiddenField ID="hdnServiceId" runat="server" Value='<%#Eval("intServiceId") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="hdnActionTakentobetaken" runat="server" Value='<%#Eval("strActionTobeTakenBy") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="hdnActiontakenby" runat="server" Value='<%#Eval("strActionTakenBy") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="hdnIndustryName" runat="server" Value='<%#Eval("strIndustryName") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="hdncurrentquerystatus" runat="server" Value='<%#Eval("VCHCURRENTQUERYSTATUSDATE") %>'></asp:HiddenField>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Industry Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblIndustry" Text='<%#Eval("VCHINDUSTRIESNAME") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Proposal No.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblmob1" Text='<%#Eval("strProposalId") %>' runat="server" Visible="false"></asp:Label>
                                                <asp:HyperLink ID="hlnkproposal" ForeColor="Blue" Text='<%#Eval("strProposalId") %>'
                                                    runat="server" ToolTip="ProposalNo"></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Service Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblmob2" Text='<%#Eval("strServiceName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Investor's Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblmob" Text='<%#Eval("strInvesterName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Requested Date
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblmobMM" Text='<%#Eval("Requestdate") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                District 
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldistrict" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Status
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblmob3" Text='<%#Eval("strStatus") %>' runat="server"></asp:Label>
                                                <%--<asp:Label ID="lblmob3" Text="Pending" runat="server"></asp:Label>--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Action Taken By
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%--  <asp:Label ID="lblmob3" Text='<%#Eval("INT_STATUS") %>' runat="server"></asp:Label>--%>
                                                <asp:Label ID="lblmob3d" Text='<%#Eval("strActionTakenBy") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Action to be Taken By
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%--  <asp:Label ID="lblmob3" Text='<%#Eval("INT_STATUS") %>' runat="server"></asp:Label>--%>
                                                <asp:Label ID="lblmob3dd" Text='<%#Eval("strActionTobeTakenBy") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Source">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnProposal" Value='<%#Eval("strProposalId") %>' runat="server" />
                                                <asp:Label ID="lblRouted" runat="server" Text="Routed Through SLFC/DLFC"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End Of ORPTS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblORTPS" runat="server"><%#Eval("strExcalationDays")%></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false">
                                            <HeaderTemplate>
                                                Payment Status
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%--  <asp:Label ID="lblmob3" Text='<%#Eval("INT_STATUS") %>' runat="server"></asp:Label>--%>
                                                <span class="label-custom label label-default">Required</span>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <%--     <asp:TemplateField>
                                        <HeaderTemplate>
                                         Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        
                                            <asp:LinkButton ID="LinkButton1" Text="Take Action"  OnClientClick="setvaluesOfrow(this);" runat="server" class="label-warning label label-default" data-toggle="modal" data-target='<%# "#" + Eval("strApplicationUnqKey")%>' >Take Action</asp:LinkButton>
                                          <div class="modal fade" id='<%#Eval("strApplicationUnqKey")%>' tabindex="-1" role="dialog" aria-hidden="true">
                                            
                                                               
                                          </div>
                                        </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Take Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnServiceId2" runat="server" Value='<%# Eval("intServiceId")%>'></asp:HiddenField>
                                                <asp:HiddenField ID="hdnApplicationUnqKey" runat="server" Value='<%# Eval("strApplicationUnqKey")%>'></asp:HiddenField>
                                                <asp:HiddenField ID="hdnProposalId" runat="server" Value='<%# Eval("strProposalId")%>'></asp:HiddenField>
                                                <asp:HiddenField ID="hdnInvesterName" runat="server" Value='<%# Eval("strInvesterName")%>'></asp:HiddenField>
                                                <asp:HiddenField ID="hdnActionTakenBy2" runat="server" Value='<%# Eval("intActionTakenBy")%>'></asp:HiddenField>
                                                <asp:HiddenField ID="hdnActionTobeTakenBy2" runat="server" Value='<%# Eval("intActionTobeTakenBy")%>'></asp:HiddenField>
                                                <asp:HiddenField ID="hdnlevel" runat="server" Value='<%# Eval("intEscalationId")%>'></asp:HiddenField>
                                                <asp:HiddenField ID="hdnstrServiceName" runat="server" Value='<%# Eval("strServiceName")%>'></asp:HiddenField>
                                                <asp:LinkButton ID="LinkButton1" Text="Take Action" runat="server" class="label-warning label label-default"
                                                    data-toggle="modal" data-target='<%# "#"+Eval("strApplicationUnqKey") %>'>Take Action</asp:LinkButton>
                                                <%--   <a href="#" data-toggle="modal" data-target='<%# "#"+Eval("strApplicationUnqKey") %>'  class="label-warning label label-default" data-toggle="modal"  OnClientClick="setvaluesOfrow(this);">
                                                               TakeAction</a>
                                                --%>
                                                <div class="modal fade" id='<%#Eval("strApplicationUnqKey") %>' tabindex="-1" role="dialog"
                                                    aria-hidden="true">
                                                    <div class="modal-dialog modal-lg" style="width:910px">
                                                        <div class="modal-content">
                                                            <div class="modal-header modal-header-primary">
                                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                                    ×</button>
                                                                <%--<h3><i class="fa fa-user m-r-5"></i> Take Action</h3>--%>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <%--     <form class="form-horizontal">--%>
                                                                        <fieldset>
                                                                            <div class="panel panel-bd ">
                                                                                <div class="panel-heading">
                                                                                    Details
                                                                                </div>
                                                                                <div class="panel-body">
                                                                                <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-3" style="color: #000000">
                                                                                                Application No.</label>
                                                                                            <div class="col-md-9">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="datalabel" id="lblApplicationNo" style="color: #FF0000">
                                                                                                    <span>
                                                                                                        <%#Eval("strApplicationUnqKey")%></span></label>
                                                                                            </div>
                                                                                             </div>
                                                                                    </div>

                                                                                     <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-3" style="color: #000000">
                                                                                                Investor's Name</label>
                                                                                            <div class="col-md-9">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="datalabel" id="lblInvestorName">
                                                                                                    <span>
                                                                                                        <%#Eval("strInvesterName")%></span></label>
                                                                                            </div>
                                                                                            </div>
                                                                                    </div>

                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-3" style="color: #000000">
                                                                                                Proposal No.</label>
                                                                                            <div class="col-md-3">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="datalabel" id="lblProposalId" style="color: #FF0000">
                                                                                                    <span>
                                                                                                        <%#Eval("strProposalId")%></span></label>
                                                                                            </div>
                                                                                            <label class="col-md-3" style="color: #000000">
                                                                                                Industry Type</label>
                                                                                            <div class="col-md-3">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="datalabel" id="lblIndustryType">
                                                                                                    <span>
                                                                                                        <%#Eval("strIndType")%></span></label>
                                                                                            </div>
                                                                                             </div>
                                                                                    </div>
                                                                                            <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-3" style="color: #000000">
                                                                                                Name Of Industries</label>
                                                                                            <div class="col-md-9">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="datalabel" id="lblIndustriesName">
                                                                                                    <span>
                                                                                                        <%#Eval("strIndustryName")%></span></label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                        
                                                                                    <%--     <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">Action to be Taken By</label>
                                    <div class="col-md-4">
                                   <label class="control-label" id="lblActiontobetaken"><span><%#Eval("strActionTobeTakenBy")%></span></label>
                                    </div>
                                     <label class="col-md-2" style="color: #000000">Action Taken By</label>
                                    <div class="col-md-4">
                                     <label class="control-label" id="lblActiontakenby"><span><%#Eval("strActionTakenBy")%></span></label>
                                    </div>
                                    </div>
                                    </div>  --%>
                                                                                </div>
                                                                            </div>
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
                                                                                                    <asp:ListItem Selected="True" Value="2" Text="Approve"></asp:ListItem>
                                                                                                    <%-- <asp:ListItem  Value="3" Text="Forward"></asp:ListItem>--%>
                                                                                                    <asp:ListItem Value="4" Text="Reject"></asp:ListItem>
                                                                                                    <%--<asp:ListItem  Value="5" Text="Raised Query"></asp:ListItem>--%>
                                                                                                </asp:DropDownList>
                                                                                                <asp:HiddenField ID="hdnServId" runat="server" Value='<%# Eval("intServiceId")%>'></asp:HiddenField>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group dmdamt" id="divdemandAmt" style="display: none">
                                                                                        <div class="row">
                                                                                            <label class="col-md-3">
                                                                                                Estimated Amount</label>
                                                                                            <div class="col-md-5">
                                                                                                <span class="colon">:</span>
                                                                                                <asp:TextBox ID="txtEstimatedAmt" TextMode="SingleLine" MaxLength="19" Rows="3" CssClass="form-control demandgen"
                                                                                                    runat="server" onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                                                                <%--<span style="color: Red">*</span>--%>
                                                                                                <span class="mandetory">*</span>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group dmdamt divappno" id="appno" style="display: none">
                                                                                        <div class="row">
                                                                                            <label class="col-md-3">
                                                                                                Application No</label>
                                                                                            <div class="col-md-5">
                                                                                                <span class="colon">:</span>
                                                                                                <asp:TextBox ID="txtApplicationNo" TextMode="SingleLine" MaxLength="30" Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')"
                                                                                                    Rows="3" CssClass="form-control genappno" runat="server"></asp:TextBox>
                                                                                                <span style="color: Red">*</span>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-3">
                                                                                                Remark</label>
                                                                                            <div class="col-md-5">
                                                                                                <span class="colon">:</span>
                                                                                                <%--   <textarea name="address" class="form-control" rows="3"></textarea>--%>
                                                                                                <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                                                                    runat="server" Onkeypress="return inputLimiter(event,'Address')"></asp:TextBox>
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
                                                                                                <div style="float: left">
                                                                                                    <span style="color: Red"><small>(Upload only .pdf file, maximum 4 MB)</small></span>
                                                                                                    <asp:HiddenField ID="hdnDoc" runat="server" />
                                                                                                    <%--  <b><asp:Label ID="lnkUFName" runat="server" ></asp:Label></b>--%>
                                                                                                    <asp:HyperLink ID="lnkUFName" runat="server" Text="" Target="_blank"></asp:HyperLink>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="form-group" id="insupload" runat="server">
                                                                                        <div class="row">
                                                                                            <label class="col-md-3">
                                                                                                Upload Inspection document</label>
                                                                                            <div class="col-md-5">
                                                                                                <span class="colon">:</span>
                                                                                                <asp:FileUpload ID="fluinsUpload" CssClass="form-control uploadinsdoc" runat="server" />
                                                                                                <div style="float: left">
                                                                                                    <span style="color: Red"><small>(Upload only .pdf file, maximum 4 MB)</small></span>
                                                                                                    <asp:HiddenField ID="hdninsDoc" runat="server" />
                                                                                                    <asp:HyperLink ID="lnkinsName" runat="server" Text="" Target="_blank"></asp:HyperLink>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="form-group" id="resupload" runat="server">
                                                                                        <div class="row">
                                                                                            <label class="col-md-3">
                                                                                                Upload Restoration document</label>
                                                                                            <div class="col-md-5">
                                                                                                <span class="colon">:</span>
                                                                                                <asp:FileUpload ID="fluresUpload" CssClass="form-control uploadresdoc" runat="server" />
                                                                                                <div style="float: left">
                                                                                                    <span style="color: Red"><small>(Upload only .pdf file, maximum 4 MB)</small></span>
                                                                                                    <asp:HiddenField ID="hdnresDoc" runat="server" />
                                                                                                    <asp:HyperLink ID="lnkresName" runat="server" Text="" Target="_blank"></asp:HyperLink>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group certificateupl" id="certificateupload" style="display: none">
                                                                                        <div class="row">
                                                                                            <label class="col-md-3">
                                                                                                Upload Approval Certificate</label>
                                                                                            <div class="col-md-5">
                                                                                                <span class="colon">:</span>
                                                                                                <asp:FileUpload ID="fluApprovalCert" CssClass="form-control docUpload" runat="server" />
                                                                                                <div style="float: left">
                                                                                                    <span style="color: Red"><small>(Only jpg,png,pdf and maximum 4 MB files allowed.)*</small></span>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group fwd" id="forward" style="display: none">
                                                                                        <div class="row">
                                                                                            <label class="col-md-3">
                                                                                                Select User</label>
                                                                                            <div class="col-md-5">
                                                                                                <span class="colon">:</span>
                                                                                                <%--<asp:DropDownList ID="ddluser" runat="server" class="form-control ddluser">
                                                  <asp:ListItem  Value="0" Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Selected="True" Value="2" Text="Approve"></asp:ListItem>
                                         
                                       
                                           
                                            </asp:DropDownList>--%>
                                                                                                <asp:DropDownList ID="ddlUser" runat="server" CssClass="chosen-select-width ddluser"
                                                                                                    Style="width: 100%">
                                                                                                </asp:DropDownList>
                                                                                                <%--<span class="mandetory">*</span>--%>
                                                                                                <div style="float: right">
                                                                                                    <span style="color: Red">*</small></span>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                     <div class="form-group">
                                                                                     <div class="col-md-3">
                                                                                     </div>
                                                                                     <div class="col-md-5">
                                                                                    <%--<div class="col-md-10 col-sm-offset-2 form-group user-form-group">--%>
                                                                                       <%-- <div class="row">--%>
                                                                                            <%--<button type="submit" class="btn btn-add btn-sm">Save</button>--%>
                                                                                            <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" class="btn btn-add btn-sm btnsubmit" />
                                                                                            <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">
                                                                                                Cancel</button>
                                                                                       <%-- </div>--%>
                                                                                        <asp:HiddenField ID="hdnServiceId1" runat="server" Value=""></asp:HiddenField>
                                                                                        <asp:HiddenField ID="hdnApplicationUnqKey1" runat="server" Value=""></asp:HiddenField>
                                                                                        <asp:HiddenField ID="hdnProposalId1" runat="server" Value=""></asp:HiddenField>
                                                                                        <asp:HiddenField ID="hdnInvesterName1" runat="server" Value=""></asp:HiddenField>
                                                                                        <asp:HiddenField ID="hdnActionTakenBy1" runat="server" Value=""></asp:HiddenField>
                                                                                        <asp:HiddenField ID="hdnActionTobeTakenBy1" runat="server" Value=""></asp:HiddenField>
                                                                                        <asp:HiddenField ID="hdnlevel1" runat="server" Value=""></asp:HiddenField>
                                                                                    <%--</div>--%>
                                                                                    </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <!-- Text input-->
                                                                        </fieldset>
                                                                        <%--        </form>--%>
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
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                View Query Details
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnraise" Text="Raise Query" runat="server" class="label-success label label-default"
                                                    data-toggle="modal" data-target='<%# "#R"+Eval("strApplicationUnqKey") %>'>Raise Query</asp:LinkButton>
                                                <asp:HiddenField ID="hdnremark" runat="server" Value='<%# Eval("strRemark")%>'></asp:HiddenField>
                                                <div class="modal fade" id='<%# "R"+Eval("strApplicationUnqKey") %>' tabindex="-1"
                                                    role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-lg">
                                                        <div class="modal-content">
                                                            <div class="modal-header modal-header-primary">
                                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                                    ×</button>
                                                                <h4>
                                                                    <i class="fa fa-user m-r-5"></i>Raise Query</h4>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <%--  <form class="form-horizontal">--%>
                                                                        <fieldset>
                                                                            <div class="panel panel-bd ">
                                                                                <div class="panel-heading">
                                                                                    Raise Query
                                                                                </div>
                                                                                <div id="Div1" class="form-group" runat="server">
                                                                                    <div id="QueryHist" runat="server">
                                                                                    </div>
                                                                                    <div class="clearfix">
                                                                                    </div>
                                                                                </div>
                                                                                <div class="panel-body dvRpt">
                                                                                    <div class="form-group">
                                                                                        <div class="row" id="divResults">
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2">
                                                                                                Query</label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <%--   <textarea name="address" class="form-control" rows="3"></textarea>--%>
                                                                                                <asp:TextBox ID="txtQueryRemarks" TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                                                                    runat="server"></asp:TextBox>
                                                                                                <div id="1stCnt" class="text-red">
                                                                                                    <i>Maximum <span id="charsLeft" class="mandatoryspan">1000</span> characters left</i>
                                                                                                    *
                                                                                                </div>
                                                                                            </div>
                                                                                            <asp:HiddenField ID="hdnQryServiceId" runat="server" Value='<%# Eval("intServiceId")%>'></asp:HiddenField>
                                                                                            <asp:HiddenField ID="hdnQryApplicationUnqKey" runat="server" Value='<%# Eval("strApplicationUnqKey")%>'></asp:HiddenField>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2">
                                                                                                Upload Reference document</label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <asp:FileUpload ID="docqueryUpload" CssClass="form-control" runat="server" />
                                                                                                <div style="float: left">
                                                                                                    <span class="mandatory"><small>(Upload only .pdf file)</small></span>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-10 col-sm-offset-2 form-group user-form-group">
                                                                                        <div class="row">
                                                                                            <%--<button type="submit" class="btn btn-add btn-sm">Save</button>--%>
                                                                                            <asp:Button ID="btnQuerySubmit" runat="server" Text="Save" OnClick="btnQuerySubmit_Click"
                                                                                                OnClientClick="return validate(this);" class="btn btn-add btn-sm" />
                                                                                            <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">
                                                                                                Cancel</button>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <!-- Text input-->
                                                                        </fieldset>
                                                                        <%--    </form>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- /.modal-content -->
                                                    </div>
                                                    <!-- /.modal-dialog -->
                                                </div>
                                                <asp:LinkButton ID="lbtnQueryDtls" runat="server" class="btn btn-success btn-sm"
                                                    data-toggle="modal" data-target='<%# "#P" +Eval("strApplicationUnqKey")%>'></asp:LinkButton>
                                                <!--Modal Start(Added By Pranay Kumar on 21-Sept-2017)-->
                                                <div class="modal fade" id='<%# "P"+Eval("strApplicationUnqKey")%>' tabindex="-1"
                                                    role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-lg">
                                                        <div class="modal-content">
                                                            <div class="modal-header modal-header-primary">
                                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                                    ×</button>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="panel panel-bd ">
                                                                            <div class="panel-body">
                                                                                <div id="Div2" class="form-group" runat="server">
                                                                                    <div id="QueryHist1" runat="server">
                                                                                    </div>
                                                                                    <div class="clearfix">
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <!-- Text input-->
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <button type="button" style="display: none" class="btn btn-danger pull-right" data-dismiss="modal">
                                                                    Close</button>
                                                            </div>
                                                        </div>
                                                        <!-- /.modal-content -->
                                                    </div>
                                                    <!-- /.modal-dialog -->
                                                </div>
                                                <!-- Modal End(Ended By Pranay Kumar on 21-Sept-2017)-->
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Order Details
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkOrder" Text="Order Details" runat="server" class="label-warning label label-default"
                                                    data-toggle="modal" data-target='<%# "#o"+Eval("strApplicationUnqKey") %>'>Order Details</asp:LinkButton>
                                                <div class="modal fade" id='<%# "o"+Eval("strApplicationUnqKey") %>' tabindex="-1"
                                                    role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-lg">
                                                        <div class="modal-content">
                                                            <div class="modal-header modal-header-primary">
                                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                                    ×</button>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="panel panel-bd ">
                                                                            <div class="panel-heading">
                                                                                Order Details
                                                                            </div>
                                                                            <div class="panel-body">
                                                                                <div class="clearfix">
                                                                                    <div id="Div31" class="form-group" runat="server">
                                                                                        <div class="col-sm-6">
                                                                                            <div class="panel panel-default">
                                                                                                <div class="panel-heading">
                                                                                                    Successfull Transaction
                                                                                                </div>
                                                                                                <div class="panel-body">
                                                                                                    <div id="OrderList" runat="server">
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-6">
                                                                                            <div class="panel panel-default">
                                                                                                <div class="panel-heading">
                                                                                                    Failure Transaction
                                                                                                </div>
                                                                                                <div class="panel-body">
                                                                                                    <div id="OrderList1" runat="server">
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="clearfix">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
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
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Change ORPTS Time">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Portal/Service/ChangeORPTSTimeline.aspx?Key={0}&Sid={1}",EncryptQueryString(Eval("strApplicationUnqKey").ToString()),EncryptQueryString(Eval("intServiceId").ToString())) %>' Target="_blank">Change ORPTS Time</asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-grid no-print" />
                                </asp:GridView>
                                <asp:Button ID="btnDownload" runat="server" Text="Download" Style="display: none"
                                    OnClick="btnDownload_Click" />
                                <asp:HiddenField ID="hdnFileNames" runat="server" />
                                <asp:HiddenField ID="hdnqueryFile" runat="server"></asp:HiddenField>
                                <%--</ContentTemplate>
                                <triggers>
                                   <asp:asyncpostbacktrigger controlid="btnShow"  />
                                
                                    </triggers>
   
                                 </asp:UpdatePanel>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- customer Modal1 -->
            <!-- /.modal -->
            <!-- Modal -->
        </section>
        <!-- /.content -->
    </div>
    <link href="../Chosen/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../Chosen/chosen.jquery.js" type="text/javascript"></script>
</asp:Content>
