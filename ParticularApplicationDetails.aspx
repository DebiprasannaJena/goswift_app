<%--'*******************************************************************************************************************
' File Name         : ApplicationDetails.aspx
' Description       : Show the  details of Particular Applied Application
' Created by        : Prasun Kali
' Created On        : 19th September 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ParticularApplicationDetails.aspx.cs"
    Inherits="ParticularApplicationDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%--<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>--%>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%--<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <title></title>
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

        $(document).ready(function () {
            $('.menuservices').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <%--        <uc2:header ID="header" runat="server" />--%>
            <div class="registration-div investors-bg">
                <div class="">
                    <div id="exTab1">
                        <%--<div class="investrs-tab">
                        <uc4:investoemenu ID="ineste" runat="server" />
                    </div>--%>
                        <div class="form-sec ">
                            <div class="form-header noprint">
                                <div class="iconsdiv">
                                    <a href="javascript:void(0);" title="Print" data-toggle="tooltip" id="printbtn" class="pull-right printbtn">
                                        <i class="fa fa-print"></i></a>
                                    <asp:HyperLink title="Save as Pdf" data-toggle="tooltip" class="pull-right printbtn btn-primary"
                                        ID="hplPdf" runat="server"><i class="fa fa-file-pdf-o"></i></asp:HyperLink>
                                    <%--   <a href="javascript:void(0);"  title="Save as Pdf" data-toggle="tooltip"  id="A1" class="pull-right printbtn btn-primary" id="ancPdf" runat="server">
                            <i class="fa fa-file-pdf-o"></i></a>--%>
                                    <a href="javascript:history.back()" title="Back" data-toggle="tooltip" id="A2" class="pull-right printbtn">
                                        <i class="fa fa-chevron-circle-left"></i></a>
                                </div>
                            </div>
                            <div class="form-body minheight300">
                                <div class="row">
                                    <!-- Form controls -->
                                    <div class="col-sm-12">
                                        <div class="panel panel-bd lobidrag">
                                            <div class="dy-formview">
                                                <div class="dyformheader">
                                                    <div class="header-details" runat="server" id="divHeader">
                                                    </div>
                                                </div>
                                                <div class="dyformbody">
                                                    <div class="row">
                                                        <div class="col-sm-6 ">
                                                            <label for="sss" class="col-sm-6">
                                                                Application Number</label>
                                                            <label for="sss" class="col-sm-6" id="lblapplication" runat="server">
                                                                <span class="colon"></span>
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-6 ">
                                                            <label for="sss" class="col-sm-6">
                                                                Applied Date</label>
                                                            <label for="sss" class="col-sm-6" id="lblapplieddate" runat="server">
                                                                <span class="colon"></span>
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <div id="frmContent" runat="server">
                                                    </div>
                                                    <div>
                                                        <h4 class="modal-title">Transactional Details</h4>
                                                        <div class="row">
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
                                                        </div>
                                                        <div class="row" id="status" runat="server" visible="false">
                                                            <div class="col-sm-12">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-heading">
                                                                        Action Details
                                                                    </div>
                                                                    <div class="panel-body">
                                                                        <div id="divapplicationdetail" runat="server">
                                                                            <asp:GridView ID="gvapplication" runat="server" AutoGenerateColumns="false" DataKeyNames="VCH_APPLICATION_UNQ_KEY,INT_SERVICEID,intId"
                                                                                Style="width: 100%; border-collapse: collapse;" OnRowDataBound="gvapplication_RowDataBound" OnRowCommand="gvapplication_RowCommand">
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <HeaderTemplate>
                                                                                            Sl No.
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <%# Container.DataItemIndex + 1 %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Application No.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblapplication" runat="server" Text='<%# Eval("VCH_APPLICATION_UNQ_KEY") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Remarks">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblremarks" runat="server" Text='<%# Eval("vchRemarks") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Status">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("vchstatusName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Approval Document">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkapproval" runat="server" CommandName="Approvedfile">Download</asp:LinkButton>
                                                                                            <asp:Label ID="lblapproval" runat="server" Visible="false" Text='<%# Eval("vchApprovalDoc") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Reference Document">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkdoc" runat="server" CommandName="Filename">Download</asp:LinkButton>
                                                                                            <asp:Label ID="lbldoc" runat="server" Visible="false" Text='<%# Eval("vchFileName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="NOC Document">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnknoc" runat="server" CommandName="Nocfile">Download</asp:LinkButton>
                                                                                            <asp:Label ID="lblnoc" runat="server" Visible="false" Text='<%# Eval("VCH_Noc_FileName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Inspection Document">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkins" runat="server" CommandName="Inspectionfile">Download</asp:LinkButton>
                                                                                            <asp:Label ID="lblins" runat="server" Visible="false" Text='<%# Eval("VCH_INSPECTION_FILENAME") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Restoration Document">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkres" runat="server" CommandName="Restorationfile">Download</asp:LinkButton>
                                                                                            <asp:Label ID="lblres" runat="server" Visible="false" Text='<%# Eval("VCH_RESTORATION_FILENAME") %>'></asp:Label>
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
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12" align="center">
                                            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" class="btn btn-primary btn-sm" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc3:footer ID="footer" runat="server" />
        <!-- /.content -->
        <div class="modal fade" id='customer1' tabindex="-1" role="dialog" aria-hidden="true"
            style="display: none">
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
                                <form class="form-horizontal">
                                    <fieldset>
                                        <div class="panel panel-bd ">
                                            <div class="panel-heading">
                                                Raise Query
                                            </div>
                                            <div class="panel-body">
                                                <div class="form-group" id="divQ1" runat="server">
                                                    <label class="col-md-2">
                                                        Initial Query</label>
                                                    <div class="col-md-4">
                                                        <%--  <textarea name="address" class="form-control" rows="3"></textarea>--%>
                                                        <asp:Label ID="lblq1" runat="server" class="bindlabel" Text="" Width="500px"></asp:Label>
                                                        <asp:TextBox ID="txtq1" MaxLength="1000" TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                            Width="500px" onkeyup="setvalue();" Onkeypress="return inputLimiter(event,'Address')"
                                                            runat="server"></asp:TextBox>
                                                        <div id="div1stcnt" runat="server" visible="false">
                                                            <i>Maximum <span id="charsLeft" class="mandatoryspan">1000</span> characters left</i>
                                                        </div>
                                                        <asp:HiddenField ID="hdnNoofrecord" runat="server" />
                                                    </div>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                                <div class="form-group" id="divA1" runat="server">
                                                    <label class="col-md-2">
                                                        Initial Response From The Investor</label>
                                                    <div class="col-md-4">
                                                        <%--  <textarea name="address" class="form-control" rows="3"></textarea>--%>
                                                        <asp:Label ID="lbla1" runat="server" class="bindlabel" Text="" Width="500px"></asp:Label>
                                                        <%--<asp:TextBox ID="txta1" MaxLength="1000" TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                    Onkeypress="return inputLimiter(event,'Address')" runat="server"></asp:TextBox>--%>
                                                    </div>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                                <div class="form-group" id="divfile1" runat="server">
                                                    <label class="col-md-2">
                                                        Files</label>
                                                    <div class="col-md-4">
                                                        <asp:HyperLink ID="hlDoc1" runat="server" Target="_blank">
                                                            <asp:Image ID="pdficon1" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                                Width="25" Visible="false" />
                                                        </asp:HyperLink>
                                                        <asp:HyperLink ID="hlDoc2" runat="server" Target="_blank">
                                                            <asp:Image ID="pdficon2" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                                Width="25" Visible="false" />
                                                        </asp:HyperLink>
                                                        <asp:HyperLink ID="hlDoc3" runat="server" Target="_blank">
                                                            <asp:Image ID="pdficon3" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                                Width="25" Visible="false" />
                                                        </asp:HyperLink>
                                                    </div>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                                <div class="form-group" id="divQ2" runat="server">
                                                    <label class="col-md-2">
                                                        Second Set Of Query</label>
                                                    <div class="col-md-4">
                                                        <%--  <textarea name="address" class="form-control" rows="3"></textarea>--%>
                                                        <asp:Label ID="lblq2" runat="server" class="bindlabel" Text="" Width="500px"></asp:Label>
                                                        <asp:TextBox ID="txtq2" MaxLength="1000" TextMode="MultiLine" Rows="3" Width="500px"
                                                            CssClass="form-control" Onkeypress="return inputLimiter(event,'Address')" onkeyup="setvalue1();"
                                                            runat="server"></asp:TextBox>
                                                        <div id="div2ndcnt" runat="server" visible="false">
                                                            <i>Maximum <span id="charsLeft1" class="mandatoryspan">1000</span> characters left</i>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                                <div class="form-group" id="divA2" runat="server">
                                                    <label class="col-md-2">
                                                        Second Set Of Response From The Investor</label>
                                                    <div class="col-md-4">
                                                        <%--  <textarea name="address" class="form-control" rows="3"></textarea>--%>
                                                        <asp:Label ID="lbla2" runat="server" class="bindlabel" Text="" Width="500px"></asp:Label>
                                                        <%--<asp:TextBox ID="txta2" MaxLength="1000" TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                    Onkeypress="return inputLimiter(event,'Address')" runat="server"></asp:TextBox>--%>
                                                    </div>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                                <div class="form-group" id="divfile2" runat="server">
                                                    <label class="col-md-2">
                                                        Files</label>
                                                    <div class="col-md-4">
                                                        <asp:HyperLink ID="hlDoc4" runat="server" Target="_blank">
                                                            <asp:Image ID="pdficon4" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                                Width="25" Visible="false" />
                                                        </asp:HyperLink>
                                                        <asp:HyperLink ID="hlDoc5" runat="server" Target="_blank">
                                                            <asp:Image ID="pdficon5" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                                Width="25" Visible="false" />
                                                        </asp:HyperLink>
                                                        <asp:HyperLink ID="hlDoc6" runat="server" Target="_blank">
                                                            <asp:Image ID="pdficon6" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                                Width="25" Visible="false" />
                                                        </asp:HyperLink>
                                                    </div>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                                <div class="col-md-10 col-sm-offset-2 form-group user-form-group">
                                                    <%--<asp:Button ID="btnSubmit" runat="server" Text="Save" OnClientClick="return  valid();"
                                                        OnClick="btnSubmit_Click" class="btn btn-add btn-sm" />--%>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger btn-sm"
                                                        Style="display: none" data-dismiss="modal" />
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
                        <button type="button" style="display: none" class="btn btn-danger pull-right" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </form>
</body>
</html>
