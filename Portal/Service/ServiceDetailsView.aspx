<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="ServiceDetailsView.aspx.cs" Inherits="Service_ServiceDetailsView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'

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

        function valid() {
            if (document.getElementById('ContentPlaceHolder1_hdnNoofrecord').value == "0") {
                if (document.getElementById('ContentPlaceHolder1_txtq1').value == "") {
                    alert('Initial Query can not be left blank!');
                    document.getElementById('ContentPlaceHolder1_txtq1').focus();
                    return false;
                }
            }
            if (document.getElementById('ContentPlaceHolder1_hdnNoofrecord').value == "2") {
                if (document.getElementById('ContentPlaceHolder1_txtq2').value == "") {
                    alert('Second Set Of Query can not be left blank!');
                    document.getElementById('ContentPlaceHolder1_txtq2').focus();
                    return false;
                }
            }
        }

        function setvalue() {
            $('#charsLeft').html(1000 - $('#ContentPlaceHolder1_txtq1').val().length);
        }

        function setvalue1() {
            $('#charsLeft1').html(1000 - $('#ContentPlaceHolder1_txtq2').val().length);
        }

    </script>
    <style>
        .padding-lr10 {
            padding: 4px 15px;
        }

        @media print {
            .panel-body {
                padding: 0px 0px;
                margin: 0px -15px
            }

            .main-header, .content-header, .main-footer, .back-top {
                display: none;
            }

            .dyformbody h2 {
                font-size: 15px;
                margin: 0px 0px 4px;
                padding: 0px 5px;
                border-bottom: 1px solid #e8e8e8;
                line-height: 26px;
            }

            .col-sm-6 {
                width: 50%;
                float: left;
            }

            .dropdown {
                display: none;
            }

            .dyformheader {
                border: 1px solid #b5b5b5;
            }

            .dyformbody {
                border: 1px solid #b5b5b5;
                margin-top: -1px
            }

            a[href]:after {
                content: none !important;
            }

            .btn {
                display: none;
            }
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
                <h1>Manage Service</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Services</a></li>
                    <li><a>Add Service</a></li>
                </ul>
            </div>
        </section>

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- Form controls -->
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidrag">
                        <div class="panel-heading ui-sortable-handle">
                            <div class="dropdown">
                                <ul class="dropdown-menu dropdown-menu-right">
                                    <li>
                                        <asp:HyperLink title="Save as Pdf" data-toggle="tooltip" ID="hplPdf" runat="server"><i class="panel-control-icon fa fa-file-pdf-o"></i></asp:HyperLink>
                                    </li>
                                    <li><a class="PrintBtn" data-tooltip="Print" data-toggle="tooltip" data-title="Print"><i class="panel-control-icon fa fa-print"></i></a></li>

                                </ul>
                            </div>
                        </div>
                        <div class="panel-body">

                            <div class="dyformheader">
                                <div class="header-details" runat="server" id="divHeader">
                                </div>
                            </div>
                            <div class="dyformbody">
                                <div class="sectionPanel">
                                    <div class="panel-body">
                                        <div class="row">
                                            <label for="sss" class="col-sm-3">
                                                Application Number
                                            </label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:Label for="sss" Font-Bold="true" ID="lblapplication" runat="server" ForeColor="#bd323e"></asp:Label>
                                            </div>
                                            <label for="sss" class="col-sm-3">
                                                Applied Date
                                            </label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:Label ID="lblapplieddate" Font-Bold="true" runat="server" ForeColor="#bd323e"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="frmContent" runat="server">
                                </div>

                                <%--Transactional Details--%>
                                <div class="sectionPanel">
                                    <h2>Transactional Details</h2>
                                    <div class="row">
                                        <div class="panel-body">
                                            <div class="col-sm-6">
                                                <div class="sectionPanel">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h2>Successfull Transaction</h2>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div id="OrderList" runat="server">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="sectionPanel">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h2>Failure Transaction</h2>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div id="OrderList1" runat="server">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--Action Details--%>
                                <div id="status" runat="server" visible="false">
                                    <div class="sectionPanel">
                                        <h2>Action Details</h2>
                                        <div class="panel-body">
                                            <asp:GridView class="table table-bordered table-hover" ID="gvapplication" runat="server" AutoGenerateColumns="false" Style="width: 100%; border-collapse: collapse;" OnRowDataBound="gvapplication_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="5%">
                                                        <HeaderTemplate>
                                                            Sl No.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Application No." ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapplication" runat="server" Text='<%# Eval("VCH_APPLICATION_UNQ_KEY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblremarks" runat="server" Text='<%# Eval("vchRemarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="9%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("vchstatusName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Date" ItemStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_CreatedOn" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approval Doc" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnfilevalcert" runat="server" Value='<%#Eval("vchApprovalDoc") %>'></asp:HiddenField>
                                                            <asp:HyperLink ID="hprApprodoc" runat="server" class="fa fa-download" aria-hidden="true" />
                                                            <asp:Label ID="lblapproval" runat="server" Visible="false" Text='<%# Eval("vchApprovalDoc") %>' ForeColor="Red"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reference Doc" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnfileval" runat="server" Value='<%#Eval("vchFileName") %>'></asp:HiddenField>
                                                            <asp:HyperLink ID="hprReferndoc" runat="server" class="fa fa-download" aria-hidden="true" />
                                                            <asp:Label ID="lblReferdoc" runat="server" Visible="false" Text='<%# Eval("vchFileName") %>' ForeColor="Red"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NOC Doc" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnNocDocu" runat="server" Value='<%#Eval("VCH_Noc_FileName") %>'></asp:HiddenField>
                                                            <asp:HyperLink ID="hprNocdoc" runat="server" class="fa fa-download" aria-hidden="true" />
                                                            <asp:Label ID="lblrNocdoc" runat="server" Visible="false" Text='<%# Eval("VCH_Noc_FileName") %>' ForeColor="Red"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Inspection Doc" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnInspectionDocu" runat="server" Value='<%#Eval("VCH_INSPECTION_FILENAME") %>'></asp:HiddenField>
                                                            <asp:HyperLink ID="hprInspectdoc" runat="server" class="fa fa-download" aria-hidden="true" />
                                                            <asp:Label ID="lblinspdoc" runat="server" Visible="false" Text='<%# Eval("VCH_INSPECTION_FILENAME") %>' ForeColor="Red"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Restoration Doc" ItemStyle-Width="9%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnRestorationDocu" runat="server" Value='<%#Eval("VCH_RESTORATION_FILENAME") %>'></asp:HiddenField>
                                                            <asp:HyperLink ID="hprRestordoc" runat="server" class="fa fa-download" aria-hidden="true" />
                                                            <asp:Label ID="lblrestdoc" runat="server" Visible="false" Text='<%# Eval("VCH_RESTORATION_FILENAME") %>' ForeColor="Red"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12" align="center">
                        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" class="btn btn-add" />                        
                    </div>
                </div>
            </div>
        </section>
    </div>

    <!-- /.content -->
    
</asp:Content>
