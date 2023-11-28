<%--'*******************************************************************************************************************
' File Name         : AddIncentivePolicy.aspx
' Description       : Add Incentive Policy
' Created by        : AMit Sahoo
' Created On        : 13 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     
                            1                           9th Oct 2017              GS Chhotray           Approval Process                            CKS
'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApproveIncentive.aspx.cs"
    MasterPageFile="~/MasterPage/Application.master" Inherits="Portal_Incentive_ApproveIncentive" %>

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
            <div class="col-sm-12">
                <div class="panel panel-bd lobidisable">
                    <div class="panel-body">
                        <div class="search-sec">
                            <div class="ibox-content">
                                <div class="clearfix">
                                </div>
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
                                            <%-- <asp:LinkButton ID="LinkButton1" runat="server"  
                                                            style="margin-top:5px;" onclick="LinkButton1_Click" ></asp:LinkButton>--%>
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
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-2">
                                            Status</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlStatusPop" CssClass="form-control" runat="server" onchange="return StatusTypText();">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Approve" Value="2" />
                                                <asp:ListItem Text="Reject" Value="3" />
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnDocText" runat="server" />
                                            <asp:HiddenField ID="hdnAvailType" runat="server" />
                                            <asp:HiddenField ID="hdnProvision" runat="server" />
                                            <asp:HiddenField ID="hdnVchInctNum" runat="server" />
                                            <asp:HiddenField ID="hdndisbursetyp" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group" id="dvSanctionUp" runat="server">
                                    <div class="row">
                                        <label class="col-sm-2">
                                            <div id="DivStTypeText" runat="server">
                                                Upload Sanction Document</div>
                                        </label>
                                        <div class="col-sm-4">
                                            <asp:FileUpload ID="fupSanctionDoc" CssClass="form-control" runat="server" onchange="return FileExtension(this);" />
                                            <asp:HiddenField ID="hdnSanction" runat="server" />
                                            <small>
                                                <asp:Label ID="lblSanction" runat="server" ForeColor="Red"></asp:Label>
                                                <span style="color: Red;font-size:11px;">Max size upto 4 MB in .pdf/.doc/.docx/.xls/.xlsx format only</span></small>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group" id="Div1">
                                    <div class="row">
                                        <label class="col-sm-2">
                                            Sanctioned Amount
                                        </label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtsanctionamount" MaxLength="15" onblur="DecimalValidation(this);"
                                                onkeypress="return  FloatOnly(event, this);" runat="server" CssClass="form-control"></asp:TextBox>
                                            <small><span style="color: Red;">Amount to be entered in INR (in Lakhs)</span></small>
                                            <cc1:FilteredTextBoxExtender ID="Filtersanctionedamt" runat="server" Enabled="True"
                                                TargetControlID="txtsanctionamount" FilterType="Custom,Numbers" ValidChars="1234567890."
                                                FilterMode="ValidChars">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-2">
                                            Remark</label>
                                        <div class="col-sm-4">
                                            <div class="">
                                                <asp:TextBox ID="txtRemark" Rows="5" onKeyUp="limitText(this,this.form.count,250);"
                                                    TextMode="MultiLine" MaxLength="250" CssClass="form-control" runat="server" onKeyDown="limitText(this,this.form.count,250);"></asp:TextBox>
                                                <a href="#" data-toggle="tooltip" class="fieldinfo" title="It can accept both alphabets and numbers.Special Characters like !  # $ % & ( )  + , - . / : ; = ? @ [ \ ] are allowed">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a><small><small>Maximum
                                                        <input name="count" class="inputCss" readonly="readonly" style="font-weight: bold;
                                                            color: red; width: 26px;" type="text" value="250" />
                                                        Characters Left</small>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                            FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" ValidChars="!# $ % & ( ) + , - . / : ; = ? @ [ \ ]  "
                                                            FilterMode="ValidChars" TargetControlID="txtRemark">
                                                        </cc1:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
                                            <asp:HiddenField ID="HdnMobNo" runat="server" />
                                            <asp:HiddenField ID="HdnEmail" runat="server" />
                                        </div>
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
        </section>
        <!-- customer Modal1 -->
        <!-- /.modal-dialog -->
    </div>
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script src="../../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
    <script type="text/javascript">

        function pageLoad() {
            var txtDesc = $("#txtRemark").val().length;
            $('.inputCss').val(250 - txtDesc);
        }

        $(window).load(function () {

            $('#Div1').hide();

            $('#btnSave').click(function () {
                var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

                if (DropDownValidation('ddlStatusPop', '0', 'status', projname) == false) {
                    $("#ddlStatusPop").focus();
                    return false;
                };

                var isprovisional = '<%=Request.QueryString["isprovisional"]%>';
                var IncCode = '<%=Request.QueryString["code"]%>';

                if (isprovisional == "0" || isprovisional == "3") {
                    if ($("#ddlStatusPop").val() == "2") {
                        if ($("#fupSanctionDoc").val() == "") {
                            jAlert('<strong>Sanction order document cannot be left blank.</strong>', projname);
                            return false;
                        }
                    }
                    if ($("#ddlStatusPop").val() == "2") {
                        if ($("#fupSanctionDoc").val() != "") {
                            if (ValidFileExtentionAndSize('fupSanctionDoc', 'pdf,doc,docx,xls,xlsx', projname, 4, 'MB') == false) {
                                $("#fupSanctionDoc").focus();
                                return false;
                            }
                        }
                    }
                }

                if (isprovisional == "1" && IncCode == "10100110") {
                    if ($("#fupProvisional").val() == "") {
                        jAlert('<strong>Provisional certificate cannot be left blank.</strong>', projname);
                        return false;
                    }
                }
                if (blankFieldValidation('txtRemark', 'Remark', projname) == false) {
                    $("#txtRemark").focus();
                    return false;
                };
                if (WhiteSpaceValidation1st('txtRemark', 'Remark', projname) == false) {
                    $("#txtRemark").focus();
                    return false;
                };
                return ConfirmAction('btnSave');
            });
        });

        function limitText(limitField, limitCount, limitNum) {
            if (limitField.value.length > limitNum) {
                limitField.value = limitField.value.substring(0, limitNum);
            } else {
                limitCount.value = limitNum - limitField.value.length;
            }
        }

        function ConfirmAction(cntr) {
            var strValue = $('#' + cntr).val();
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

        function chksanctioneligible() {
            var distype = $("#hdndisbursetyp").val();
            $("#txtsanctionamount").val("0.00");
            if (distype != '3') {
                if ($("#ddlStatusPop").val() == "2") {
                    $("#Div1").show();
                }
                else {
                    $("#Div1").hide();
                }
            }
            else {
                $("#Div1").hide();
            }
        }

        function StatusTypText() {
            var xx = $('#hdnDocText').val();
            if (xx != 1) {
                if ($("#ddlStatusPop").val() == "2") {
                    $("#DivStTypeText").text("Upload Sanction Document");

                }
                else if ($("#ddlStatusPop").val() == "3") {
                    $("#DivStTypeText").text("Upload Rejection Document");
                }
            }
            chksanctioneligible();
        }

        function DecimalValidation(Ctl) {
            var CtlId = Ctl.id;
            var amount = $("#" + CtlId).val();
            amount = Number(amount).toFixed(2);
            if (isNaN(amount)) {
                amount = Number(0).toFixed(2);
            }
            $("#" + CtlId).val(amount);
        }

        function FileExtension(e) {
            var ids = e.id;
            var fileExtension = ['xls', 'xlsx', 'ods', 'pdf', 'doc', 'docx'];
            if ($.inArray($("#" + ids).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                ////alert('Only excel file are allowed.');
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

        function alertredirect(msg) {
            jAlert('Data Saved Successfully !', 'SWP', function (r) {

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
