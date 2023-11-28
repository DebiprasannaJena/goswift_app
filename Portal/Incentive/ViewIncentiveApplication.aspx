<%--'*******************************************************************************************************************
' File Name         : ViewIncentiveApplication.aspx
' Description       : View details of Incentive Application
' Created by        : AMit Sahoo
' Created On        : 12 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     
                            1                             24-OCT-2017            Pranay Kumar         Implementation of Query Mgnt in PC Large      Chinmaya Sir
'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ViewIncentiveApplication.aspx.cs" Inherits="Incentive_ViewIncentiveApplication"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Portal/Include/PagingUserControl.ascx" TagName="PagingUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../../js/WebValidation.js"></script>
    <script src="../../js/decimalrstr.js" type="text/javascript"></script>
    <div class="content-wrapper">
        <asp:ScriptManager ID="sm1" runat="server">
        </asp:ScriptManager>
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    Production Certificate Approval</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Production Certificate</a></li>
                </ul>
            </div>
        </section>
        <!-- Main content -->
        <section class="content">
        <div class="row">
            <!-- Form controls -->
            <%-- <asp:UpdatePanel ID="up1" runat="server">
                    <ContentTemplate>--%>
            <div class="col-sm-12">
                <div class="panel panel-bd lobidisable">
                    <div class="panel-body">
                        <div class="search-sec">
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-md-2 col-sm-3">
                                        Application No</label>
                                    <div class="col-md-3 col-sm-3">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtAppNo" CssClass="form-control" runat="server" MaxLength="15"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="fteAppno" TargetControlID="txtAppNo" runat="server"
                                            FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers" ValidChars="-">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                    <label class="col-md-2 col-sm-3">
                                        Status</label>
                                    <div class="col-md-3 col-sm-3">
                                        <span class="colon">:</span>
                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="--Select--" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Approved" Value="2" />
                                            <asp:ListItem Text="IR Updated" Value="1" />
                                            <asp:ListItem Text="Pending" Value="0" />
                                            <asp:ListItem Text="Reject" Value="3" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-md-2 col-sm-3">
                                        District</label>
                                    <div class="col-md-3 col-sm-3">
                                        <span class="colon">:</span>
                                        <asp:DropDownList ID="ddlDist" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 col-sm-3">
                                        Unit Name</label>
                                    <div class="col-md-3 col-sm-3">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtUnitName" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="fteUnitName" runat="server" FilterMode="InvalidChars"
                                            InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;.,{}+=[];'.,/|\_-~`" TargetControlID="txtUnitName">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-success"
                                            OnClick="btnsearch_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive">
                            <div style="text-align: right;" class="NOPRINT">
                                <asp:Label ID="lblDetails" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlNoOfRec" ToolTip="Page Size" Width="50px" runat="server"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRec_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <asp:GridView ID="grdPcApplication" runat="server" OnRowDataBound="grdPcApplication_RowDataBound"
                                OnRowCommand="grdPcApplication_RowCommand" AutoGenerateColumns="false" class="table table-bordered table-hover"
                                DataKeyNames="intAppNo,intQueryStatus,CURRENT_QUERY_STATUS">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl#">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="distname" HeaderText="District" />
                                    <asp:TemplateField HeaderText="Application No.">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hypDetails" runat="server" Text='<%#Eval("vchAppNo") %>' ToolTip="Show Incentive details"></asp:HyperLink>
                                            <asp:HiddenField ID="hdnDetails" Value='<%#Eval("intAppNo") %>' runat="server"></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="requestType" HeaderText="Application For" />
                                    <asp:BoundField DataField="vchCompName" HeaderText="Company Name" />
                                    <asp:BoundField DataField="unitCategory" HeaderText="Unit Category" />
                                    <asp:BoundField DataField="organizationType" HeaderText="Organization Type" />
                                    <asp:BoundField DataField="strApplied" HeaderText="Status" />
                                    <asp:TemplateField HeaderText="Applied On">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbl_Created_On" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Take Action" HeaderStyle-Width="100px" HeaderStyle-CssClass="noPrint"
                                        ItemStyle-CssClass="noPrint">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hypScheduleIR" Width="100px" ToolTip="Schedule Inspection" runat="server"
                                                Visible="true" CssClass="btn btn-primary btn-sm" Text="SCHED INSP">
                                            </asp:HyperLink>
                                            <asp:HyperLink ID="hypIpcForm" Width="100px" ToolTip="Update/View IR Form" runat="server"
                                                Visible="true" CssClass="btn btn-warning btn-sm" Text="UPDATE IR" Style="margin-bottom: 5px;">
                                            </asp:HyperLink>
                                            <br />
                                            <asp:HyperLink ID="hypUploadPC" Width="100px" Target="_blank" data-toggle="tooltip"
                                                ToolTip="View PC Certificate" runat="server" Visible="false" CssClass="btn btn-success btn-sm"> GENRATE PC
                                            </asp:HyperLink>
                                            <asp:HyperLink ID="hypViewPC" Width="100px" Target="_blank" data-toggle="tooltip"
                                                ToolTip="View PC Certificate" runat="server" Visible="false" CssClass="btn btn-success btn-sm"> VIEW PC
                                            </asp:HyperLink>
                                            <asp:HiddenField ID="hdnStatus" runat="server" Value='<%#Eval("intApproved") %>' />
                                            <asp:HiddenField ID="hdnGenerate" runat="server" Value='<%#Eval("intGeneratePC") %>' />
                                            <asp:HiddenField ID="hdnScheduleSt" runat="server" Value='<%#Eval("intScheduleStatus") %>' />
                                            <asp:HiddenField ID="hdnScheduleDate" runat="server" Value='<%#Eval("dtmIRScheduleOn") %>' />
                                            <asp:HiddenField ID="hdnPcPdfPath" runat="server" Value='<%#Eval("vchPCFilePath") %>' />
                                            <asp:HiddenField ID="hdnOfflinePc" runat="server" Value='<%#Eval("intOfflinePc") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Added By Pranay Kumar on 24-OCT-2017 for Showing Raise Query Info--%>
                                    <asp:TemplateField HeaderText="Query" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnTextVal1" runat="server" Value='<%# Eval("intAppNo")%>'>
                                            </asp:HiddenField>
                                            <asp:LinkButton ID="lbtnRaise" Text="RAISE QUERY" runat="server" Width="110px" class="btn btn-success btn-sm"
                                                data-toggle="modal" data-target='<%# "#"+Eval("intAppNo")%>'></asp:LinkButton>
                                            <!--Modal Start(Added By Pranay Kumar on 24-OCT-2017)-->
                                            <div class="modal fade" id='<%# Eval("intAppNo")%>' tabindex="-1" role="dialog" aria-hidden="true">
                                                <div class="modal-dialog modal-lg">
                                                    <div class="modal-content">
                                                        <div class="modal-header modal-header-primary">
                                                            <button type="button" width="100px" class="close" data-dismiss="modal" aria-hidden="true">
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
                                                                                    <asp:HiddenField ID="hdnUnitCategory" runat="server" Value='<%# Eval("unitCategory")%>'>
                                                                                    </asp:HiddenField>
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
                                                                                <asp:Button ID="btnQuerySubmit" CommandArgument='<%# Eval("intAppNo")%> ' runat="server"
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
                                            <!-- Modal End(Ended By Pranay Kumar on 24-OCT-2017)-->
                                            <asp:LinkButton ID="lbtnQueryDtls" runat="server" Width="110px" class="btn btn-success btn-sm"
                                                data-toggle="modal" data-target='<%# "#P" +Eval("intAppNo")%>'></asp:LinkButton>
                                            <!--Modal Start(Added By Pranay Kumar on 24-OCT-2017)-->
                                            <div class="modal fade" id='<%# "P"+Eval("intAppNo")%>' tabindex="-1" role="dialog"
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
                                                                            <div id="Div12" class="form-group" runat="server">
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
                                            <!-- Modal End(Ended By Pranay Kumar on 24-OCT-2017)-->
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Ended By Pranay Kumar on 24-OCT-2017 for Showing Raise Query Info--%>
                                </Columns>
                                <EmptyDataTemplate>
                                    No Record Found
                                </EmptyDataTemplate>
                                <EmptyDataRowStyle ForeColor="Red" />
                            </asp:GridView>
                            <%--Added By Pranay Kumar on 24-OCT-2017 for Showing Raise Query Info--%>
                            <asp:Button ID="btnDownload" runat="server" Text="Download" Style="display: none"
                                OnClick="btnDownload_Click" />
                            <asp:HiddenField ID="hdnFileNames" runat="server" />
                            <%--Ended By Pranay Kumar on 24-OCT-2017 for Showing Raise Query Info--%>
                            <div style="float: right;" class="noPrint" id="divPaging" runat="server">
                                <asp:Repeater ID="rptPager" runat="server">
                                    <ItemTemplate>
                                        <ul class="pagination">
                                            <li class='<%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %> '>
                                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                    OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                            </li>
                                        </ul>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <%--   <asp:HiddenField ID="HiddenField1" runat="server" Value="Blank Value" />--%>
                                <asp:HiddenField ID="hdnPgindex" runat="server" Value="Blank Value" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--   </ContentTemplate>
                </asp:UpdatePanel>--%>
        </div>
    </section>
    </div>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            $("#ddlStatusPop").change(function () {
                var el = $(this);
                if (el.val() === "Forward") {
                    $("#ddlUser").show();
                    $("#ddlUser").append("<option>AAA</option>");
                }
            });
        });
        //Added By Pranay Kumar on 24-OCT-2017 for Checking Validations
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
        function setQueryvalue(obj) {
            $('#charsQueryLeft').html(1000 - obj.value.length);
        }

        function Queryvalid(obj) {
            debugger;
            var ID = obj.name;
            var arr = ID.split('b');

            if ($('textarea[name="' + arr[0] + 'txtq1"]').val() == "") {
                jAlert('<strong>Query cannot be left blank!</strong>', projname);
                $('textarea[name="' + arr[0] + 'txtq1"]').focus();
                return false;
            }

            if ($('input[name="' + arr[0] + 'FileUpload1"]').val() != "") {
                if (!DocValidCerti(arr[0] + 'FileUpload1'))
                { return false; }
            }
        }
        $(document).ready(function () {
            $("a.ancDownload").click(function (event) {
                debugger;
                var href = $(this).attr('href');
                //$(this).attr('href', '#');
                var Filename = href.split('/');
                if (Filename[5] != undefined) {
                    if (Filename[5].indexOf('.pdf') > -1) {
                        $('#ContentPlaceHolder1_hdnFileNames').val(Filename[5]);
                        document.getElementById('<%= btnDownload.ClientID %>').click();
                        event.preventDefault();
                    }
                };


            });
        });
        function DocValidCerti(Controlname) {

            var arr = new Array;
            var arr2 = new Array;
            var arrnew = new Array('pdf');
            var count = 0;
            var y, x, z;
            x = $('input[name="' + Controlname + '"]').val(); //document.getElementById(Controlname).value;
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
            else if (z.files[0].size > 10 * 1024 * 1024) {
                //alert('The file size can not exceed 2MB.');
                jAlert('<strong>The file size can not exceed 12 MB.!</strong>', projname);
                //  $(Controlname).focus();
                return false;
            }
            else
                return true;
        }

        //Ended By Pranay Kumar on 24-OCT-2017
    </script>
</asp:Content>
