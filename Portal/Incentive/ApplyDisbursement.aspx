<%--'*******************************************************************************************************************
' File Name         : ApplyDisbursement.aspx
' Description       : Add Disbursement
' Created by        : Emani Parida
' Created On        : 20 Oct 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     
       
'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyDisbursement.aspx.cs"
    MasterPageFile="~/MasterPage/Application.master" Inherits="Portal_Incentive_ApplyDisbursement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"
    ClientIDMode="Static">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
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
                    <div class="panel-body">
                        <div class="search-sec">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Application No.</label>
                                            <div class="col-sm-4">
                                                <label id="lblAppNo" runat="server">
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Application Details</label>
                                            <div class="col-sm-4">
                                                Click to view
                                                <asp:HyperLink ID="hypView" runat="server" CssClass="btn btn-primary" Target="_blank"
                                                    class="form-control"><i class="fa fa-file-text-o" ></i></asp:HyperLink>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Remarks</label>
                                            <div class="col-sm-4">
                                                <label id="lblRemarks" runat="server">
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Unit Name</label>
                                            <div class="col-sm-4">
                                                <label id="lblUnitName" runat="server">
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Incentive Name</label>
                                            <div class="col-sm-4">
                                                <label id="lblIncentive" runat="server">
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblDisbursement" runat="server"><b>Disbursement Details :</b></asp:Label>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Sanction order Document</label>
                                            <div class="col-sm-4">
                                                Click to view
                                                <asp:HyperLink ID="hnkSanctionDoc" runat="server" CssClass="btn btn-primary" Target="_blank"
                                                    class="form-control"><i class="fa fa-file-text-o" ></i></asp:HyperLink>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Mode</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="ddlDisbursementMode" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Online Transfer" Value="4" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" id="Div11" runat="server">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                <div id="Div12" runat="server">
                                                    Sanctioned Amount</div>
                                            </label>
                                            <div class="col-sm-4">
                                                <asp:Label ID="lblsanctionedamt" runat="server"></asp:Label>
                                            </div>
                                            <small><span style="color: Red;">* All amounts in INR( in Lakhs)</small>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" id="Div1" runat="server">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                <div id="Div2" runat="server">
                                                    Amount</div>
                                            </label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtAmount" onblur="return makeDecimal(this.id);CheckDecimal(this.id);"
                                                    MaxLength="15" CssClass="form-control" runat="server"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtAmount"
                                                    FilterType="Custom,Numbers" Enabled="True" FilterMode="ValidChars" ValidChars=".">
                                                </cc1:FilteredTextBoxExtender>
                                            </div>
                                            <small><span style="color: Red;">* All amounts to be entered in INR( in Exact Amount)</small>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" id="Div3" runat="server">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                <div id="Div4" runat="server">
                                                    Date</div>
                                            </label>
                                            <div class="col-sm-3">
                                                <div class="input-group  date datePicker">
                                                    <asp:TextBox runat="server" class="form-control" ID="txtDate" name="txtDate"></asp:TextBox>
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" id="Div9" runat="server">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                <div id="Div10" runat="server">
                                                    Time</div>
                                            </label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtTime" runat="server" MaxLength="7" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                         </div>
                                        <div class="form-group" id="Div7" runat="server">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    <div id="Div8" runat="server">
                                                        Bank Name</div>
                                                </label>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtBank" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtBank"
                                                        FilterType="Custom,UppercaseLetters,LowercaseLetters" Enabled="True" FilterMode="ValidChars"
                                                        ValidChars=" ">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group" id="dvSanctionUp" runat="server">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    <div id="DivStTypeText" runat="server">
                                                        UTR(Transaction ID)<span class="text-red">*</span></div>
                                                </label>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtDisbursementNo" onKeyUp="limitText(this,this.form.count,12);"
                                                        MaxLength="12" CssClass="form-control" runat="server" onKeyDown="limitText(this,this.form.count,12);"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                        FilterType="Custom,Numbers" FilterMode="ValidChars" TargetControlID="txtDisbursementNo">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="hdnEmail" runat="server" />
                                        <asp:HiddenField ID="hdnMobile" runat="server" />
                                        <div class="form-group" id="Div5" runat="server">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    <div id="Div6" runat="server">
                                                        Upload Disbursement Document</div>
                                                </label>
                                                <div class="col-sm-4">
                                                    <asp:FileUpload ID="fupDisburseDoc" CssClass="form-control" runat="server" onchange="return FileExtension(this);" />
                                                    <asp:HiddenField ID="hdnDisburse" runat="server" />
                                                    <small>
                                                        <asp:Label ID="lblDisburse" runat="server" ForeColor="Red"></asp:Label>
                                                        <span style="color: Red;font-size:11px;">Max size upto 4 MB in .pdf/.doc/.docx/.xls/.xlsx format only</span></small>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="row">
                                                   <div class="col-sm-2"></div>
                                                        <div class="col-sm-4">
                                                            <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnSubmit_Click"
                                                                OnClientClick="return validation();" />
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
                                                        </div>
                                                    
                                            </div>
                                        </div>
                                   
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                </div>
            </div>
        </div>
         </section>
        <!-- customer Modal1 -->
        <!-- /.modal-dialog -->
    </div>
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script type="text/javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';
        function pageLoad(sender, args) {
            $('#txtTime').timepicker({ 'timeFormat': 'h:i A', 'step': '1', 'scrollDefaultNow': true });

            $(function () {
                $('.datePicker').datepicker({
                    minDate: new Date(),
                    autoclose: true,
                    format: "dd-M-yyyy"
                });
                $("#txtDate").datepicker({
                    format: "dd-M-yyyy",
                    autoclose: true
                }).on("changeDate", function (e) {
                    if (new Date($('#txtDate').val()) > new Date()) {
                        jAlert('Disbursement date should not be future date !', projname);
                        $("#txtDate").focus();
                        $("#<%=txtDate.ClientID %>").val("");
                        return false;
                    }
                    return true;
                });
            });
        }


        // To check decimal (controlId, DecimalPlaces)
        function CheckDecimal(e, t) { try { var n = ""; var r; if (parseInt(t)) { r = t } else { r = 2 } var i = document.getElementById(e); if (i == "undefined" || i == null) { i = e } if (typeof i.value === "undefined") { n = i.innerHTML.trim() } else { n = i.value.trim() } if (n.split(".").length - 1 > 1 || n.charAt(n.length - 1) == "." || n.charAt(0) == ".") { if (typeof i.value === "undefined") { setTimeout(function () { alert("Please enter valid decimal !"); $("#" + i.getAttribute("id")).effect("shake", { direction: "left", times: 2, distance: 5 }, 800) }, 1) } else { setTimeout(function () { alert("Please enter valid decimal !"); $(i).focus() }, 1) } return false } else { if (n.substr(n.lastIndexOf(".") + 1, n.length).length > r && n.lastIndexOf(".") > -1) { if (typeof i.value === "undefined") { setTimeout(function () { alert("Only " + r + " digits are allowed after decimal !"); $("#" + i.getAttribute("id")).effect("shake", { direction: "left", times: 2, distance: 5 }, 800) }, 1) } else { setTimeout(function () { alert("Only " + r + " digits are allowed after decimal !"); $(i).focus() }, 1) } return false } else { return true } } } catch (s) { } }

        // To make decimal (controlId, DecimalPlace)
        function makeDecimal(e, t) { var n = document.getElementById(e); var r; if (parseInt(t)) { r = t } else { r = 2 } if (n == "undefined" || n == null) { n = e } if (typeof n.value === "undefined") { if (n.innerHTML.trim().length > 0) { n.innerHTML = parseFloat(n.innerHTML.trim()).toFixed(r) } } else { if (n.value.trim().length > 0) { n.value = parseFloat(n.value.trim()).toFixed(r) } } }
        

        $('#btnSave').click(function () {

            if (DropDownValidation('ddlDisbursementMode', '0', 'Mode', projname) == false) {
                $("#ddlDisbursementMode").focus();
                return false;
            }
            if (blankFieldValidation('txtAmount', 'Amount', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtDate', 'Date', projname) == false) {
                return false;
            }
            if ($('#txtDate').val() != '') {
                if (new Date($('#txtDate').val()) > new Date()) {
                    jAlert('Disbursement date should not be future date !', projname);
                    $('#txtDate').focus();
                    return false;
                }
            }
            if (blankFieldValidation('txtTime', 'Time', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtBank', 'Bank Name', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtDisbursementNo', "UTR(Transaction ID)", projname) == false) {
                return false;
            }
            if ($("#fupDisburseDoc").val() == "") {
                jAlert('Disburse Document cannot be left blank.', 'SWP');
                return false;
            }
            if ($("#fupDisburseDoc").val() != "") {
                if (ValidFileExtentionAndSize('fupDisburseDoc', 'pdf,doc,docx,xls,xlsx', projname, 4, 'MB') == false) {
                    $("#fupSanctionDoc").focus();
                    return false;
                }
            }
            if (parseInt($("#lblsanctionedamt").text()) != '0') {
                if (parseFloat($("#txtAmount").val()) > parseFloat($("#lblsanctionedamt").text())) {
                    jAlert('Disburse amount should not greater than sanctioned amount', projname);
                    $("#txtAmount").focus();
                    return false;
                }
            }
            if ($("#lblsanctionedamt").text() != '') {
                if (parseFloat($("#txtAmount").val()) > parseFloat($("#lblsanctionedamt").text())) {
                    jAlert('Disburse amount should not greater than sanctioned amount', projname);
                    $("#txtAmount").focus();
                    return false;
                }
            }

            return ConfirmAction('btnSave');
        });


        function FileExtension(e) {
            var ids = e.id;
            var fileExtension = ['xls', 'xlsx', 'ods', 'pdf', 'doc', 'docx'];
            if ($.inArray($("#" + ids).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                jAlert('Only excel or pdf file are allowed.', projname);
                $("#" + ids).val(null);
                return false;
            }
            else {
                if ((e.files[0].size > 4194304) && ($("#" + ids).val() != '')) {
                    jAlert('File size must be less then 4MB.', projname);
                    $("#" + ids).val().val('');
                    e.preventDefault();
                    return false;
                }
                else {
                }
            }

        }

        function limitText(limitField, limitCount, limitNum) {
            if (limitField.value.length > limitNum) {
            } else {
                //                limitCount.value = limitNum - limitField.value.length;
            }
        }

        function ConfirmAction(cntr) {
            var strValue = $('#' + cntr).val();
            var strConfirm = '';
            if (strValue == 'Update') {

                if (!confirm('Are You Sure To Submit?')) {
                    return false;
                }
                else {
                    return true;
                }

            }
            else {
                if (!confirm('Are You Sure To Save?')) {

                    return false;
                }
                else {
                    return true;
                }
            }
        }

        function alertredirect(msg) {
            jAlert('Data Saved Successfully !', 'GO-SWIFT', function (r) {
                if (r) {

                    location.href = 'View_Inct_Application.aspx' + msg;
                    return true;
                }
                else {
                    return false;
                }
            });
        }
    </script>
</asp:Content>
