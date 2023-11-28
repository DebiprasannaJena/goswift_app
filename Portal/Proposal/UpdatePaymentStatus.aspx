<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="UpdatePaymentStatus.aspx.cs" Inherits="Portal_Proposal_UpdatePaymentStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <script type="text/javascript" src="../../js/WebValidation.js"></script>
        <script src="../../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
        <script src="../js/custom.js" type="text/javascript"></script>
        <script src="../../js/decimalrstr.js" type="text/javascript"></script>
        <script type="text/javascript">
            var appendId = "ContentPlaceHolder1_";
            function pageLoad() {
                $('.datePicker').datepicker({
                    minDate: new Date(),
                    autoclose: true,
                    format: "dd-M-yyyy"
                });
            }

            function ValidatePage() {
                if (!DropDownValidation(appendId + 'ddlProposalNo', '0', 'Proposal No', 'GO-SWIFT')) {
                    $("#popup_ok").click(function () { $("#" + appendId + "ddlProposalNo").focus(); });
                    return false;
                }
                if (!blankFieldValidation(appendId + 'txtOrderNo', "Order No.", 'GO-SWIFT')) {
                    return false;
                }
                if (!blankFieldValidation(appendId + 'txtRequestId', "Request Id", 'GO-SWIFT')) {
                    return false;
                }
                if (!blankFieldValidation(appendId + 'txtChallanAmount', "Challan Amount", 'GO-SWIFT')) {
                    return false;
                }
                if (!blankFieldValidation(appendId + 'txtBankTransactionId', "Bank Transaction Id", 'GO-SWIFT')) {
                    return false;
                }
                if (!blankFieldValidation(appendId + 'txtChallanRefId', "Challan Ref Id", 'GO-SWIFT')) {
                    return false;
                }
                if (!blankFieldValidation(appendId + 'txtDate', "Payment Date", 'GO-SWIFT')) {
                    return false;
                }
                debugger;
                var paymentDate = new Date($("#" + appendId + "txtDate").val());
                if (paymentDate > new Date()) {
                    jAlert('<strong>Payment date cannot be greater than current date.</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#" + appendId + "txtDate").focus(); });
                    return false;
                }
                if (confirm("Are you sure you want to submit the details?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        </script>
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    Update Payment Status of Pending PEAL
                </h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Proposal</a></li><li><a>View</a></li></ul>
            </div>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-body">
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <div class="form-group row ">
                                        <div class="col-sm-2">
                                            <label for="Proposal No.">
                                                Proposal No.</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList CssClass="form-control" ID="ddlProposalNo" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlProposalNo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <span class="mandetory">*</span>
                                        </div>
                                    </div>
                                    <div class="form-group row ">
                                        <div class="col-sm-2">
                                            <label for="Proposal No.">
                                                Investor Name</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:Label ID="lblInvestorName" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group row ">
                                        <div class="col-sm-2">
                                            <label for="AppliedDate">
                                                Applied Date</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:Label ID="lblAppliedDate" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group row ">
                                        <div class="col-sm-2">
                                            <label for="Order">
                                                Order No</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="fteOrderNo" runat="server" TargetControlID="txtOrderNo"
                                                FilterType="Custom,Numbers" Enabled="True" FilterMode="ValidChars">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>
                                    </div>
                                    <div class="form-group row ">
                                        <div class="col-sm-2">
                                            <label for="Request">
                                                Request Id</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtRequestId" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="fteRequestId" runat="server" TargetControlID="txtRequestId"
                                                FilterType="Custom,Numbers" Enabled="True" FilterMode="ValidChars">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="ChallanAmt">
                                                Challan Amount
                                            </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtChallanAmount" runat="server" CssClass="form-control" MaxLength="10"
                                                onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="fteChallanAmount" runat="server" TargetControlID="txtChallanAmount"
                                                FilterType="Custom,Numbers" Enabled="True" FilterMode="ValidChars" ValidChars=".">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="ChallanAmt">
                                                Bank Transaction Id
                                            </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtBankTransactionId" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="fteBankTransactionId" runat="server" TargetControlID="txtBankTransactionId"
                                                FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" Enabled="True"
                                                FilterMode="ValidChars">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="ChallanAmt">
                                                Challan Ref Id
                                            </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtChallanRefId" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="fteChallanRefId" runat="server" TargetControlID="txtChallanRefId"
                                                FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" Enabled="True"
                                                FilterMode="ValidChars">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="ChallanAmt">
                                                Payment Date
                                            </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <div class="input-group  date datePicker">
                                                <asp:TextBox runat="server" class="form-control" ID="txtDate" name="txtDate"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                            <span class="mandetory">*</span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="ChallanAmt">
                                            </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-sm btn-success"
                                                OnClick="btnUpdate_Click" OnClientClick="return ValidatePage();" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-sm btn-danger" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUpdate" />
                                    <asp:PostBackTrigger ControlID="btnCancel" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
