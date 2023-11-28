<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="View_Inct_Application_IPR2022.aspx.cs" Inherits="Portal_Incentive_View_Inct_Application_IPR2022" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"
    ClientIDMode="Static">
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
                    Incentive</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>View Incentive</a></li></ul>
            </div>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- Form controls -->
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <%--<div class="panel-heading">
                           <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="AddServiceMaster.aspx"> 
                              <i class="fa fa-plus"></i>  Add List </a>  
                           </div>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewServiceMaster.aspx"> 
                              <i class="fa fa-file"></i> View List </a>  
                           </div>
                           
                        </div>--%>
                        <div class="panel-body">
                            <div class="search-sec">
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-md-2 col-sm-3">
                                            Application No</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_App_No" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <label class="col-md-2 col-sm-3">
                                            Status</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="DrpDwn_Status" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                <asp:ListItem Value="2">Approved</asp:ListItem>
                                                <asp:ListItem Value="1">Applied</asp:ListItem>
                                                <asp:ListItem Value="3">Rejected</asp:ListItem>
                                                 <asp:ListItem Value="4">Pending</asp:ListItem>
                                                                                     
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                            <asp:Button ID="Btn_Search" runat="server" OnClick="Btn_Search_Click" Text="Search" CssClass="btn btn-success"
                                                 />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divPagingShow" runat="server" class="noPrint pull-right" visible="false">
                                <span class="text-muted">
                                    <asp:Literal ID="litStart" runat="server">1</asp:Literal>
                                    -
                                    <asp:Literal ID="litEnd" runat="server">10</asp:Literal>
                                    of
                                    <asp:Literal ID="litTotalRecord" runat="server"></asp:Literal>
                                    <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="true" >
                                        <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="100">100</asp:ListItem>
                                        <asp:ListItem Value="500">500</asp:ListItem>
                                        <asp:ListItem Value="2147483647">All</asp:ListItem>
                                    </asp:DropDownList>
                                </span>
                                <asp:HiddenField ID="hdnTotalCount" runat="server" />
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="Grd_Application" runat="server" OnRowDataBound="Grd_Application_RowDataBound" OnRowCommand="Grd_Application_RowCommand"  AutoGenerateColumns="false" class="table table-bordered table-hover"
                                  
                                    PagerStyle-CssClass="pagination-grid" DataKeyNames="intQueryStatus,strQueryStatus">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Application No." ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkBtn_View_Application" OnClick="LnkBtn_View_Application_Click" runat="server" Text='<%# Eval("strApplicationNum") %>'
                                                    ToolTip="Show Incentive details" ></asp:LinkButton>
                                                <asp:HiddenField ID="Hid_Form_Preview_Id" runat="server" Value='<%# Eval("strFormPreviewId") %>' />
                                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("strAppNo") %>' />
                                                <asp:HiddenField ID="Hid_Unique_Id" runat="server" Value='<%# Eval("INTINCUNQUEID") %>' />
                                                <asp:HiddenField ID="hdnSanFileName" runat="server" Value='<%# Eval("strSanFileName") %>' />
                                                <asp:HiddenField ID="hdnRemarks" runat="server" Value='<%# Eval("Remark") %>' />
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Name">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Unit_Name" runat="server" Text='<%# Eval("strUnitName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Incentive Name">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Inct_Name" runat="server" Text='<%# Eval("strInctName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Applied On">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Created_On" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                <asp:HiddenField ID="hdnIncentiveNo" runat="server" Value='<%#Eval("intInctId")%>'>
                                                </asp:HiddenField>
                                               
                                                <asp:HiddenField ID="hdnAppStatus" runat="server" Value='<%#Eval("INTSTATUS")%>'>
                                                </asp:HiddenField>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Status" runat="server" Text='<%# Eval("strStatus") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="noPrint" FooterStyle-CssClass="noPrint">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkButton" runat="server" class="btn btn-success btn-sm" CommandName="ViewDetail"
                                                    Width="110px">
                                                                    TAKE ACTION
                                                </asp:LinkButton>
                                                <asp:HiddenField ID="hdnDisburedId" runat="server" Value='<%# Eval("INTINCUNQUEID")%>'></asp:HiddenField>
                                                <asp:LinkButton ID="lbtnDisbursedDtls" runat="server" class="btn btn-success btn-sm"
                                                    data-toggle="modal" data-target='<%# "#D" +Eval("INTINCUNQUEID")%>'>
                                                    <i class='fa fa-eye' aria-hidden='true'></i>
                                                </asp:LinkButton>
                                                <div class="modal fade" id='<%# "D"+Eval("INTINCUNQUEID")%>' tabindex="-1" role="dialog"
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
                                                                                <div id="Div13" class="form-group" runat="server">
                                                                                    <div id="DisbursedList" runat="server">
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
                                                                <button type="button" style="display: none" class="btn btn-danger pull-right" data-dismiss="modal">
                                                                    Close</button>
                                                            </div>
                                                        </div>
                                                        <!-- /.modal-content -->
                                                    </div>
                                                    <!-- /.modal-dialog -->
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Query">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnTextVal1" runat="server" Value='<%# Eval("INTINCUNQUEID")%>'>
                                                </asp:HiddenField>
                                                <asp:LinkButton ID="lbtnRaise" Text="RAISE QUERY" runat="server" class="btn btn-success btn-sm"
                                                    data-toggle="modal" data-target='<%# "#"+Eval("INTINCUNQUEID")%>'></asp:LinkButton>
                                                <!--Modal Start(Added By Pranay Kumar on 11-Sept-2017)-->
                                                <div class="modal fade" id='<%# Eval("INTINCUNQUEID")%>' tabindex="-1" role="dialog"
                                                    aria-hidden="true">
                                                    <div class="modal-dialog modal-lg">
                                                        <div class="modal-content">
                                                            <div class="modal-header modal-header-primary">
                                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                                    ×</button>
                                                                <h3>
                                                                    <i class="fa fa-user m-r-5"></i>RAISE QUERY</h3>
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
                                                                                    <asp:Button ID="btnQuerySubmit" CommandArgument='<%# Eval("INTINCUNQUEID")%>' runat="server"
                                                                                        Text="Save" OnClick="btnQuerySubmit_Click" OnClientClick="return  Queryvalid(this);" 
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
                                                    data-toggle="modal" data-target='<%# "#P" +Eval("INTINCUNQUEID")%>'></asp:LinkButton>
                                                <!--Modal Start(Added By Pranay Kumar on 19-Sept-2017)-->
                                                <div class="modal fade" id='<%# "P"+Eval("INTINCUNQUEID")%>' tabindex="-1" role="dialog"
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
                                                <!-- Modal End(Ended By Pranay Kumar on 19-Sept-2017)-->
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Record Found
                                    </EmptyDataTemplate>
                                    <EmptyDataRowStyle ForeColor="Red" />
                                </asp:GridView>
                                <asp:Button ID="btnDownload" runat="server" OnClick="btnDownload_Click" Text="Download" Style="display: none"
                                     />
                                <asp:HiddenField ID="hdnFileNames" runat="server" />
                            </div>
                            <div style="float: right;" class="noPrint" id="divPaging" runat="server" visible="false">
                               <%-- <uc1:PagingUserControl ID="uclPager" runat="server" />--%>
                                <asp:HiddenField ID="hdnCurrentIndex" runat="server" Value="Blank Value" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- customer Modal1 -->
            <!-- /.modal-dialog -->
    </div>
    <script language="javascript" type="text/javascript">


        $(document).ready(function () {
            function openPageModal(header, page, footer, frm_hit) {
                $('#pageModal .modal-header #myModalLabel').html(header);
                $('#pageModal .modal-body').html("<iframe width='100%' height='" + frm_hit + "px' src='" + page + "' frameborder='0' scrolling='yes'></iframe>");
                $('#pageModal .modal-footer').html(footer);
                if (footer == "") { $('#pageModal .modal-footer').remove(); }
                $('#pageModal').modal();
            }


        });

        //Added By Pranay Kumar on 10-OCT-2017 for Checking Validations
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
        function setQueryvalue(obj) {
            $('#charsQueryLeft').html(1000 - obj.value.length);
        }

        function Queryvalid(obj) {
            debugger;
            var ID = obj.name;
            var arr = ID.split('b');
            //            alert($('textarea[name="ctl00$ContentPlaceHolder1$Grd_Application$ctl12$txtq1"]').val());
            //            alert($('textarea[name="' + arr + 'txtq1"]').val());           

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

            $(".testd").click(function (event) {
                var href = $(this).attr('href');
                //$(this).attr('href', '#');
                var Filename = href.split('/');
                if (Filename[5] != undefined) {
                    if (Filename[5].indexOf('.pdf') > -1) {
                        $('#hdnFileNames').val(Filename[5]);
                        document.getElementById('<%= btnDownload.ClientID %>').click();
                        event.preventDefault();
                    }
                };


            });
        });
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

        //Ended By Pranay Kumar on 10-OCT-2017
    </script>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

