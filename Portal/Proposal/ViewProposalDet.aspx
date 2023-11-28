<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ViewProposalDet.aspx.cs" Inherits="Mastermodule_ViewProposalDet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/decimalrstr.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#btnCancel').click(function () {

            });
            $('#ContentPlaceHolder1_dvPEALCerti').hide();
            $('#ContentPlaceHolder1_drpStatus').change(function () {

                if (($(this).val() == "2")) {
                    $("#ContentPlaceHolder1_dvPEALCerti").show();
                }
                else {

                    $("#ContentPlaceHolder1_dvPEALCerti").hide();
                }
            });
        });

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
        function setvaluesOfrow(flu) {
            var a = flu.offsetParent.parentNode.rowIndex;
            var rows;
            rows = a - 1
            document.getElementById('ContentPlaceHolder1_hdnproposalno').value = document.getElementById('ContentPlaceHolder1_gvService_hdnTextVal_' + rows).value;
            document.getElementById('ContentPlaceHolder1_hdnCreted').value = document.getElementById('ContentPlaceHolder1_gvService_hdnCretedBy_' + rows).value;
            clear();
        }
        function setvaluesOfrow1(flu) {
            var a = flu.offsetParent.parentNode.rowIndex;
            var rows;
            rows = a - 1
            document.getElementById('ContentPlaceHolder1_hdnproposalno1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnTextValIDCo_' + rows).value;
            document.getElementById('ContentPlaceHolder1_txtLandRequiredByInvestor').value = document.getElementById('ContentPlaceHolder1_gvService_hdnLandReqIDCO_' + rows).value;
            document.getElementById('ContentPlaceHolder1_txtLandRecomendBySLFC').value = document.getElementById('ContentPlaceHolder1_gvService_hdnAMSLand_' + rows).value;
            document.getElementById('ContentPlaceHolder1_hdnCreted1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnCretedByIDCo_' + rows).value;

           // document.getElementById('ContentPlaceHolder1_LblLandUnit1').innerText = ' (in ' + document.getElementById('ContentPlaceHolder1_gvService_hdnLandUnit_' + rows).value + ')';
           // document.getElementById('ContentPlaceHolder1_LblLandUnit2').innerText = ' (in ' + document.getElementById('ContentPlaceHolder1_gvService_hdnLandUnit_' + rows).value + ')';

            if (document.getElementById('ContentPlaceHolder1_gvService_hdnIdcoDocURL_' + rows).value == "") {
                document.getElementById('ContentPlaceHolder1_hplnkPEALCerti').href = "";
            }
            else {
                document.getElementById('ContentPlaceHolder1_hplnkPEALCerti').href = 'https://investodisha.gov.in/goswift/Proposal/IDCODocs/' + document.getElementById('ContentPlaceHolder1_gvService_hdnIdcoDocURL_' + rows).value;
            }

            var k = parseFloat(document.getElementById('ContentPlaceHolder1_txtLandRequiredByInvestor').value);
            if (parseFloat(document.getElementById('ContentPlaceHolder1_txtLandRequiredByInvestor').value) > 0) {
                document.getElementById('Dvland').style.display = "block";
            }
            else {
                document.getElementById('Dvland').style.display = "none";
            }

            clear();
        }
        function setvaluesOfrowAMS(flu) {
            var a = flu.offsetParent.parentNode.rowIndex;
            var rows;
            rows = a - 1
            document.getElementById('ContentPlaceHolder1_hdnproposalnoAMS').value = document.getElementById('ContentPlaceHolder1_gvService_hdnTextValAMS_' + rows).value;
            document.getElementById('ContentPlaceHolder1_hdnCretedAMS').value = document.getElementById('ContentPlaceHolder1_gvService_hdnCretedByAMS_' + rows).value;
            clear();
        }
        function DocValid(Controlname) {

            var arr = new Array;
            var arr2 = new Array;
            var arrnew = new Array('pdf');
            var count = 0;
            var y, x, z;


            x = document.getElementById(Controlname).value;
            z = document.getElementById(Controlname);
            y = x.substring(x.lastIndexOf(".") - 1);
            arr = y.split('.');

            for (var j = 0; j < arrnew.length; j++) {
                if (arr[1] == arrnew[j])
                    count = 1;
            }

            if (count == 0) {
                alert('Please Upload PDF file Only!');
                //   jAlert('<strong>Please Upload PDF file Only !</strong>', projname);
                document.getElementById(Controlname).focus();
                return false;
            }
            else if (z.files[0].size > 10 * 1024 * 1024) {
                alert('The file size can not exceed 12 MB.');
                //  jAlert('<strong>The file size can not exceed 4MB.!</strong>', projname);
                document.getElementById(Controlname).focus();
                return false;
            }
            else
                return true;
        }

        function DocValidCerti(Controlname) {

            var arr = new Array;
            var arr2 = new Array;
            var arrnew = new Array('pdf');
            var count = 0;
            var y, x, z;
            x = $(Controlname).val(); //document.getElementById(Controlname).value;
            z = document.getElementById(Controlname);
            y = x.substring(x.lastIndexOf(".") - 1);
            arr = y.split('.');

            for (var j = 0; j < arrnew.length; j++) {
                if (arr[1] == arrnew[j])
                    count = 1;
            }

            if (count == 0) {
                // alert('Please Upload PDF file Only!');
                jAlert('<strong>Please Upload PDF file Only !</strong>', projname);
                //$(Controlname).focus();
                return false;
            }
            else if (z.files[0].size > 2 * 1024 * 1024) {
                //alert('The file size can not exceed 2MB.');
                jAlert('<strong>The file size can not exceed 4MB.!</strong>', projname);
                //  $(Controlname).focus();
                return false;
            }
            else
                return true;
        }

        function valid() {

            if (document.getElementById('ContentPlaceHolder1_drpStatus').value == "0") {
                jAlert('<strong>Please select Status !</strong>', projname);
                document.getElementById('ContentPlaceHolder1_drpStatus').focus();
                return false;
            }
            if (document.getElementById('ContentPlaceHolder1_txtRemarks').value == "") {
                jAlert('<strong>Remark can not be left blank !</strong>', projname);
                document.getElementById('ContentPlaceHolder1_txtRemarks').focus();
                return false;
            }
            if (document.getElementById('ContentPlaceHolder1_docUpload').value != '') {
                if (!DocValid('ContentPlaceHolder1_docUpload'))
                { return false; }
            }
            if (document.getElementById('ContentPlaceHolder1_drpStatus').value == "2") {
                if (document.getElementById('ContentPlaceHolder1_docPEALCerti').value == '') {
                    jAlert('<strong>Please upload PEAL Certificate !</strong>', projname);
                    document.getElementById('ContentPlaceHolder1_docPEALCerti').focus();
                    return false;
                }
                if (document.getElementById('ContentPlaceHolder1_hdnProjectType').value == "2") {
                    if (document.getElementById('ContentPlaceHolder1_docScoreCard').value == '') {
                        jAlert('<strong>Please upload Score Card !</strong>', projname);
                        document.getElementById('ContentPlaceHolder1_docScoreCard').focus();
                        return false;
                    }

                }
                if (document.getElementById('ContentPlaceHolder1_docPEALCerti').value != '') {
                    if (!DocValidCerti('ContentPlaceHolder1_docPEALCerti'))
                    { return false; }
                }

            }
            


            if (document.getElementById('ContentPlaceHolder1_docScoreCard').value != '') {
                if (!DocValidCerti('ContentPlaceHolder1_docScoreCard'))
                { return false; }
            }


            //            else {
            //                alert('Please upload PEAL Certificate !', projname);
            //            }
            var r = confirm("Are you sure you want to submit!");
            //            var r = jAlert('<strong>Are you sure you want to submit!</strong>', projname);
            if (r == true) {
                return true;
            } else {

                return false;
            }
        }
        function valid1() {
            if (document.getElementById('ContentPlaceHolder1_txtLandRecomendBySLFC').value == "") {
                jAlert('<strong>Remark can not be left blank !</strong>', projname);
                document.getElementById('ContentPlaceHolder1_txtLandRecomendBySLFC').focus();
                return false;
            }

            var r = confirm("Are you sure you want to submit!");
            if (r == true) {
                return true;
            } else {

                return false;
            }
        }
        function clear() {
            document.getElementById('ContentPlaceHolder1_txtRemarks').value = "";
            document.getElementById('ContentPlaceHolder1_docUpload').value = "";
            $('#ContentPlaceHolder1_docPEALCerti').value = "";
            $('#ContentPlaceHolder1_docScoreCard').value = "";
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
        function setvalue() {
            $('#charsLeft').html(1000 - $('#ContentPlaceHolder1_txtRemarks').val().length);
        }

        //Added By Pranay Kumar on 11-Sept-2017 for Textbox Character Remaining 
        function setQueryvalue(obj) {
            var ID = obj.id;
            var arr = ID.split('_');
            $('#charsQueryLeft').html(1000 - $('#ContentPlaceHolder1_gvService_txtq1_' + arr[3]).val().length);

        }

        function Queryvalid(obj) {
           
            var ID = obj.id;
            var arr = ID.split('_');
            if ($('#ContentPlaceHolder1_gvService_txtq1_' + arr[3]).val() == "") {
                jAlert('<strong>Query cannot be left blank!</strong>', projname);

                $('#ContentPlaceHolder1_gvService_txtq1_' + arr[3]).focus();
                return false;
            }
            if ($('#ContentPlaceHolder1_gvService_FileUpload1_' + arr[3]).val() != "") {
                if (!DocValidCerti('#ContentPlaceHolder1_gvService_FileUpload1_' + arr[3]))
                { return false; }
            }
        }

        //Ended By Pranay Kumar on 11-Sept-2017 for Textbox Character Remaining
        $(document).ready(function () {
            $("a").click(function (event) {
               
                var href = $(this).attr('href');
                //$(this).attr('href', '#');
                var Filename = href.split('/');
                if (Filename[3] != undefined) {
                    if (Filename[3].indexOf('.pdf') > -1) {
                        $('#ContentPlaceHolder1_hdnFileNames').val(Filename[3]);
                        document.getElementById('<%= btnDownload.ClientID %>').click();
                        event.preventDefault();
                    }
                };


            });
        });
    </script>
    <style>
        /* Comments
---------------------------------- */
        .comments
        {
            margin-top: 60px;
        }
        .comments h2.title
        {
            margin-bottom: 40px;
            border-bottom: 1px solid #d2d2d2;
            padding-bottom: 10px;
        }
        .comment
        {
            font-size: 14px;
        }
        .comment .comment
        {
            margin-left: 75px;
        }
        .comment-avatar
        {
            margin-top: 5px;
            width: 55px;
            float: left;
        }
        .comment-content
        {
            border-bottom: 1px solid #d2d2d2;
            margin-bottom: 25px;
        }
        .comment h3
        {
            margin-top: 0;
            margin-bottom: 5px;
        }
        .comment-meta
        {
            margin-bottom: 15px;
            color: #999999;
            font-size: 12px;
        }
        .comment-meta a
        {
            color: #666666;
        }
        .comment-meta a:hover
        {
            text-decoration: underline;
        }
        .comment .btn
        {
            font-size: 12px;
            padding: 7px;
            min-width: 100px;
            margin-top: 5px;
            margin-bottom: -1px;
        }
        .btn-gray
        {
            color: #ffffff;
            background-color: #666666;
            border-color: #666666;
        }
        .comment .btn i
        {
            padding-right: 5px;
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
            <h1>
                Proposals</h1>
            <ul class="breadcrumb">
                <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                <li><a>Proposal</a></li><li><a>View And Take Action</a></li></ul>
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
                            <a class="btn btn-add " href="ViewProposal.aspx"><i class="fa fa-plus"></i>Take Action</a>
                        </div>
                        <%-- <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="demoitems.aspx"> 
                              <i class="fa fa-file"></i> View List </a>  
                           </div>--%>
                    </div>
    <div class="panel-body">
        <div class="search-sec">
            <div class="form-group row">
                <label class="col-sm-3" for="Country">
                    Name Of Company/Enterprises</label>
                <div class="col-sm-3">
                    <span class="colon">:</span>
                    <asp:TextBox ID="txtCompanyName" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <label class="col-sm-2" for="State">
                    Status</label>
                <div class="col-sm-3" runat="server" id="st3">
                    <span class="colon">:</span>
                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="drpStatusDet" runat="server">
                        <asp:ListItem Value="0">---Select---</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-1">
                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn btn-add -sm"
                        runat="server" Text="Search"></asp:Button>
                </div>
            </div>
        </div>
        <div align="right">
            <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                OnClick="lbtnAll_Click"></asp:LinkButton>
            &nbsp;&nbsp;
            <asp:Label ID="lblPaging" runat="server"></asp:Label>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvService" class="table table-bordered table-hover" runat="server"
                AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gvService_PageIndexChanging"
                Width="100%" EmptyDataText="No Record(s) Found..." DataKeyNames="intProposalId,intActionToBeTakenBy,strFileName,intQueryStatus,strQueryStatus,intStatus,strStatus,intidcoCnt,decRecomendLand,intFowardAMS,intForwardIDCO,strDeptMailContent,IdcoStatus,IdcoBtnStatus"
                PageSize="10" OnRowDataBound="gvService_RowDataBound">
                <Columns>
                    <asp:BoundField HeaderText=" Sl No." />
                    <asp:TemplateField HeaderText="Proposal No" HeaderStyle-Width="12%">
                        <ItemTemplate>
                            <%--<a  class="label-primary label label-default"  href="ProposalDetails.aspx?Pno=<%# Eval("strFileName")%>">Details</a>--%>
                            <%--<a  href="ProposalDetails.aspx?Pno=<%# Eval("strFileName")%>&linkm=<%# Request.QueryString["linkm"].ToString() %>&linkn=<%# Request.QueryString["linkn"].ToString() %>&btn=<%# Request.QueryString["btn"].ToString() %>&tab=<%# Request.QueryString["tab"].ToString() %>"><%# Eval("strFileName")%></a>--%>
                            <asp:HyperLink ID="hypLink" runat="server" NavigateUrl="ProposalDetails.aspx" Text='<%# Eval("strFileName") %>'></asp:HyperLink>
                            <%--<asp:HyperLink ID="hypLink" runat="server" NavigateUrl="'ProposalDetails.aspx?Pno=<%# Eval("strFileName")%>&linkm=<%# Request.QueryString["linkm"].ToString() %>&linkn=<%# Request.QueryString["linkn"].ToString() %>&btn=<%# Request.QueryString["btn"].ToString() %>&tab=<%# Request.QueryString["tab"].ToString() %>'" Text='<%# Eval("strFileName") %>'></asp:HyperLink>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="decAmount" HeaderText="Industry Type" />
                    <asp:BoundField DataField="strRemarks" HeaderText="Name Of Company/Enterprises" />
                    <asp:BoundField DataField="dtmCreatedOn" HeaderText="Application Date" ItemStyle-Width="15%" />
                    <asp:BoundField DataField="strStatus" HeaderText="Status" ItemStyle-Width="12%" />
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Forward AMS
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnTextValAMS" runat="server" Value='<%# Eval("strFileName")%>'>
                            </asp:HiddenField>
                            <asp:Label ID="lblAMS" runat="server" Text="Forwarded" Style="color: Green;"></asp:Label>
                            <asp:LinkButton ID="LinkButtonAMS" Text="Forward AMS" OnClientClick="setvaluesOfrowAMS(this);"
                                runat="server" class="label-warning label label-default" data-toggle="modal"
                                data-target="#customer3"></asp:LinkButton>
                            <asp:HiddenField ID="hdnCretedByAMS" Value='<%# Eval("intCreatedBy")%>' runat="server">
                            </asp:HiddenField>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField>
                        <HeaderTemplate>
                            Take Action
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--<button type="button" id="myButton" class="label-warning label label-default" data-toggle="modal" data-target= '<%# "#" + Eval("intProposalId")%>'>Take Action</button>--%>
                            <%--<asp:Button ID="Button1" runat="server" Text="Take Action" class="label-warning label label-default" data-toggle="modal" data-target= '<%# "#" + Eval("intProposalId")%>'></asp:Button>--%>
                            <%--<asp:HiddenField ID="hdnTextVal" runat="server" Value='<%# Eval("strFileName")%>'>
                            </asp:HiddenField>
                            <asp:HiddenField ID="hdnLandReq" runat="server" Value='<%# Eval("decExtendLand")%>'>
                            </asp:HiddenField>
                            <asp:LinkButton ID="LinkButton1" Text="Take Action" OnClientClick="setvaluesOfrow(this);"
                                runat="server" class="label-warning label label-default" data-toggle="modal"
                                data-target="#customer1"></asp:LinkButton>
                            <asp:HiddenField ID="hdnCretedBy" Value='<%# Eval("intCreatedBy")%>' runat="server">
                            </asp:HiddenField>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Forward IDCO
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnTextValIDCo" runat="server" Value='<%# Eval("strFileName")%>' />                           
                            <asp:HiddenField ID="hdnLandReqIDCO" runat="server" Value='<%# Eval("decExtendLand")%>' />                           
                            <asp:HiddenField ID="hdnAMSLand" runat="server" Value='<%# Eval("decRecomendLand")%>' />
                          <%--  <asp:HiddenField ID="hdnLandUnit" runat="server" Value='<%# Eval("strLandUnit")%>'  />--%>
                            <asp:LinkButton ID="LinkButton2" Text="Forward IDCO" OnClientClick="setvaluesOfrow1(this);"
                                runat="server" class="label-warning label label-default" data-toggle="modal"
                                data-target="#customer2"></asp:LinkButton>
                            <asp:HiddenField ID="hdnCretedByIDCo" Value='<%# Eval("intCreatedBy")%>' runat="server">
                            </asp:HiddenField>
                            <asp:HiddenField ID="hdnIdcoDocURL" Value='<%# Eval("strIdcoDocs")%>' runat="server">
                            </asp:HiddenField>
                            <asp:Label ID="lblIdco" runat="server" Text="Forwarded" Style="color: Green;"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--Added By Pranay Kumar on 11-Sept-2017 for Showing Raise Query Info--%>
                    <asp:TemplateField HeaderText="Query">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnTextVal1" runat="server" Value='<%# Eval("strFileName")%>'>
                            </asp:HiddenField>
                            <asp:LinkButton ID="lbtnRaise" Text="Raise Query" runat="server" class="btn btn-success btn-sm"
                                data-toggle="modal" data-target='<%# "#"+Eval("strFileName")%>'></asp:LinkButton>
                            <!--Modal Start(Added By Pranay Kumar on 11-Sept-2017)-->
                            <div class="modal fade" id='<%# Eval("strFileName")%>' tabindex="-1" role="dialog"
                                aria-hidden="true">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content">
                                        <div class="modal-header modal-header-primary">
                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                ×</button>
                                            <h3>
                                                <i class="fa fa-user m-r-5"></i>Raise Query</h3>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="panel panel-bd ">
                                                        <div class="panel-heading">
                                                            Raise Query
                                                        </div>
                                                        <div class="panel-body">
                                                            <div id="Div1" class="form-group" runat="server">
                                                                <div id="QueryHist" runat="server">
                                                                </div>
                                                                <div class="clearfix">
                                                                </div>
                                                            </div>
                                                            <div id="Div2" class="form-group" runat="server">
                                                                <label class="col-md-2">
                                                                    Query</label>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtq1" MaxLength="1000" TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                                        Width="500px" onkeyup="setQueryvalue(this);" Onkeypress="return inputLimiter(event,'Address')"
                                                                        runat="server"></asp:TextBox>
                                                                    <div>
                                                                        <i>Maximum <span id="charsQueryLeft" class="mandatoryspan">1000</span> characters left</i></div>
                                                                </div>
                                                                <div class="clearfix">
                                                                </div>
                                                            </div>
                                                            <div id="Div3" class="form-group" runat="server">
                                                                <label class="col-md-2">
                                                                    File</label>
                                                                <div class="col-md-4">
                                                                    <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" Width="300px" />
                                                                    <span style="width: 200px" class="mandatoryspan pull-right">(Only pdf files are allowed,
                                                                        Max Size 12 MB) </span>
                                                                </div>
                                                                <div class="clearfix">
                                                                </div>
                                                            </div>
                                                            <div class="col-md-10 col-sm-offset-2 form-group user-form-group">
                                                                <asp:Button ID="btnQuerySubmit" CommandArgument='<%# Eval("strFileName")%>' runat="server"
                                                                    Text="Save" OnClientClick="return  Queryvalid(this);" OnClick="btnQuerySubmit_Click"
                                                                    class="btn btn-add btn-sm" />
                                                                <asp:Button ID="btnQueryCancel" runat="server" Text="Cancel" class="btn btn-danger btn-sm"
                                                                    Style="display: none" data-dismiss="modal" />
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
                            <!-- Modal End(Ended By Pranay Kumar on 11-Sept-2017)-->
                            <asp:LinkButton ID="lbtnQueryDtls" runat="server" class="btn btn-success btn-sm"
                                data-toggle="modal" data-target='<%# "#P" +Eval("strFileName")%>'></asp:LinkButton>
                            <!--Modal Start(Added By Pranay Kumar on 19-Sept-2017)-->
                            <div class="modal fade" id='<%# "P"+Eval("strFileName")%>' tabindex="-1" role="dialog"
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
                                                    <div class="panel panel-bd ">
                                                        <div class="panel-body">
                                                            <div class="form-group" runat="server">
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
                            <!-- Modal End(Ended By Pranay Kumar on 19-Sept-2017)-->
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--Ended By Pranay Kumar on 11-Sept-2017 for Showing Raise Query Info--%>
                </Columns>
                <PagerStyle CssClass="pagination-grid no-print" />
            </asp:GridView>
            <asp:Button ID="btnDownload" runat="server" Text="Download" Style="display: none"
                OnClick="btnDownload_Click" />
            <asp:HiddenField ID="hdnFileNames" runat="server" />
            <asp:HiddenField ID="hdnProjectType" runat="server" />
        </div>
    </div>
    </div> </div> </div>
    <!-- customer Modal1 -->
    <!-- /.modal -->
    <!-- Modal -->
    <!-- Customer Modal2 -->
    <!-- /.modal -->
    <div class="modal fade" id='customer1' tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header modal-header-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×</button>
                    <h3>
                        <i class="fa fa-user m-r-5"></i>Take Action</h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <form class="form-horizontal">
                            <fieldset>
                                <div class="panel panel-bd ">
                                    <div class="panel-heading">
                                        Take Action
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <label class="col-md-2">
                                                Status</label>
                                            <div class="col-md-4">
                                                <asp:HiddenField ID="hdnproposalno" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hdnCreted" runat="server"></asp:HiddenField>
                                                <asp:DropDownList ID="drpStatus" runat="server" class="form-control">
                                                </asp:DropDownList>
                                                <span class="mandetory">*</span>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2">
                                                Remark</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtRemarks" onkeyup="setvalue();" MaxLength="250" TextMode="MultiLine"
                                                    Rows="3" CssClass="form-control" Onkeypress="return inputLimiter(event,'Address')"
                                                    runat="server"></asp:TextBox>
                                                <span class="mandetory">*</span> <span class="text-red"><i>Maximum <span id="charsLeft"
                                                    class="mandatoryspan">1000</span> characters left</i></span>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2">
                                                Upload Reference document</label>
                                            <div class="col-md-4">
                                                <asp:FileUpload ID="docUpload" CssClass="form-control" runat="server" />
                                                <div style="margin-left: 15px; float: left">
                                                    <span style="width: 200px" class="mandatoryspan pull-right text-red">(Only pdf files
                                                        are allowed,Max Size 4 MB) </span>
                                                    <asp:HiddenField ID="hdnDoc" runat="server" />
                                                    <asp:HyperLink ID="lnkUFName" runat="server" Text="" Target="_blank"></asp:HyperLink>
                                                </div>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group" id="dvPEALCerti" runat="server">
                                            <label class="col-md-2">
                                                PEAL Certificate document</label>
                                            <div class="col-md-4">
                                                <asp:FileUpload ID="docPEALCerti" CssClass="form-control" runat="server" />
                                                <div style="margin-left: 15px; float: left">
                                                    <span style="width: 200px" class="mandatoryspan pull-right text-red">(Only pdf files
                                                        are allowed,Max Size 2 MB) </span>
                                                </div>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group" id="dvscore" runat="server">
                                            <label class="col-md-2">
                                                Upload Score Card</label>
                                            <div class="col-md-4">
                                                <asp:FileUpload ID="docScoreCard" CssClass="form-control" runat="server" />
                                                <div style="margin-left: 15px; float: left">
                                                    <span style="width: 200px" class="mandatoryspan pull-right text-red">(Only pdf files
                                                        are allowed,Max Size 4 MB) </span>
                                                </div>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <%--    <div class="form-group" id="Dvland">
                                      
                                       <label class="col-md-2">Land(in acre)</label>
                                          <div class="col-md-4"> 
                                         <asp:TextBox ID="txtLandRequired" CssClass="form-control" MaxLength="8"
                                                TabIndex="3" onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);" runat="server"></asp:TextBox>
                                           
                                            </div>
                                        <div class="clearfix"></div>
                                       </div>--%>
                                        <div class="col-md-10 col-sm-offset-2 form-group user-form-group">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" OnClientClick="return valid();"
                                                class="btn btn-add btn-sm" />
                                            <button type="btnCancel" class="btn btn-danger btn-sm" onclientclick="clear();" onclick="btnCancel_Click"
                                                data-dismiss="modal">
                                                Cancel</button>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Text input-->
                            </fieldset>
                            </form>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" onclientclick="clear();" class="btn btn-danger pull-right"
                        data-dismiss="modal">
                        Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id='customer2' tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header modal-header-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×</button>
                    <h3>
                        <i class="fa fa-user m-r-5"></i>Forward IDCO</h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <div class="panel panel-bd ">
                                    <div class="panel-heading">
                                        Forward IDCO
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-group" id="Div4">
                                            <label class="col-md-4">
                                                Land Required By Investor (In Acre) <%--<asp:Label ID="LblLandUnit1" runat="server"></asp:Label>--%></label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txtLandRequiredByInvestor" ReadOnly="true" CssClass="form-control"
                                                    MaxLength="8" TabIndex="3" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                    onblur="isNumberBlur(event, this, 2);" runat="server"></asp:TextBox>
                                                <asp:HiddenField ID="hdnproposalno1" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hdnCreted1" runat="server"></asp:HiddenField>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group" id="Div5">
                                            <label class="col-md-4">
                                                Land Recommended By SLFC (In Acre)  <%--<asp:Label ID="LblLandUnit2" runat="server"></asp:Label>--%></label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txtLandRecomendBySLFC" CssClass="form-control" MaxLength="8" TabIndex="3"
                                                    onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group" id="dvPEALCerti1" runat="server">
                                            <label class="col-md-4">
                                                Document</label>
                                            <div class="col-md-5">
                                                <asp:FileUpload ID="docIdcoDoc" CssClass="form-control" runat="server" Enabled="false" />
                                                <div style="margin-left: 15px; float: left">
                                                    <asp:HyperLink ID="hplnkPEALCerti" runat="server" Target="_blank" ToolTip="Download Document">
                                                  <i class="fa fa-download" aria-hidden="true"></i>
                                                    </asp:HyperLink>
                                                    <small class="text-danger">(Only pdf files are allowed,Max size 4 MB)</small>
                                                </div>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group" >
                                         <label class="col-md-4">
                                                 </label>
                                         <div class="col-md-5 user-form-group">
                                            <asp:Button ID="btnSubmitIdco" runat="server" Text="Save" OnClientClick="return valid1();"
                                                class="btn btn-add btn-sm" OnClick="btnSubmitIdco_Click" />
                                            <button type="btnCancelIdco" class="btn btn-danger btn-sm" onclientclick="clear();"
                                                onclick="btnCancelIdco_Click" data-dismiss="modal">
                                                Cancel</button>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Text input-->
                            </fieldset>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" onclientclick="clear();" class="btn btn-danger pull-right"
                        data-dismiss="modal">
                        Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id='customer3' tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header modal-header-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×</button>
                    <h3>
                        <i class="fa fa-user m-r-5"></i>Forward AMS</h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <div class="panel panel-bd ">
                                    <div class="panel-heading">
                                        Forward AMS
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-group" id="Dvlandwee">
                                            <label class="col-md-2">
                                                Nodal Officer:</label>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="drpNodalOffcr" runat="server" class="form-control">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdnproposalnoAMS" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hdnCretedAMS" runat="server"></asp:HiddenField>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="col-md-10 col-sm-offset-2 form-group user-form-group">
                                            <asp:Button ID="btnSubmitAMS" runat="server" Text="Save" class="btn btn-add btn-sm"
                                                OnClick="btnSubmitAMS_Click" />
                                            <button type="btnCancelAMS" class="btn btn-danger btn-sm" onclientclick="clear();"
                                                data-dismiss="modal">
                                                Cancel</button>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Text input-->
                            </fieldset>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" onclientclick="clear();" class="btn btn-danger pull-right"
                        data-dismiss="modal">
                        Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    </section>
        <!-- /.content -->
    </div>
</asp:Content>
