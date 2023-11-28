<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="AddQuery.aspx.cs" Inherits="Portal_AMS_AddQuery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../../js/WebValidation.js"></script>
    <script src="../../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ValidatePage() {
            var isChecked = $("#ContentPlaceHolder1_chkNoQuery").is(':checked');
            if (!isChecked && !blankFieldValidation('ContentPlaceHolder1_txtQuery', "Query", 'GO-SWIFT')) {
                return false;
            }
            if (confirm("Are you sure you want to submit the details?")) {
                return true;
            }
            else {
                return false;
            }
        }

        function pageLoad() {
            $("#ContentPlaceHolder1_chkNoQuery").change(function () {
                debugger;
                var isChecked = $(this).is(':checked');
                if (isChecked) {
                    $("#ContentPlaceHolder1_txtQuery").attr("disabled", "disabled");
                    $("#ContentPlaceHolder1_txtQuery").val('');
                    $("#ContentPlaceHolder1_fuQuery").val('');
                    $("#ContentPlaceHolder1_fuQuery").attr("disabled", "disabled");
                }
                else {
                    $("#ContentPlaceHolder1_txtQuery").removeAttr("disabled");
                    $("#ContentPlaceHolder1_txtQuery").val('');
                    $("#ContentPlaceHolder1_fuQuery").val('');
                    $("#ContentPlaceHolder1_fuQuery").removeAttr("disabled");
                }
            });
        }
    </script>
    <div class="content-wrapper">
        <asp:ScriptManager ID="sm1" runat="server">
        </asp:ScriptManager>
        <asp:HiddenField ID="hdnAction" runat="server" />
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    Add Query Details
                </h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>AMS</a></li><li><a>Add</a></li></ul>
            </div>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-body">
                            <div class="form-group row">
                                <label class="col-sm-2">
                                    Proposal No</label>
                                <div class="col-sm-4">
                                    <asp:Label ID="lblProposalNo" runat="server"></asp:Label>
                                    <br />
                                    <asp:CheckBox ID="chkNoQuery" runat="server" Text="No Queries" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2">
                                    Query<span style="color: Red;">*</span></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtQuery" runat="server" TextMode="MultiLine" Rows="4" Columns="10"
                                        MaxLength="500" CssClass="form-control"></asp:TextBox>
                                    <small>&nbsp;(Maximum&nbsp;
                                        <asp:Label ID="lblQuery" runat="server" Text="500" ForeColor="Red"></asp:Label>
                                        &nbsp; characters allowed)</small>
                                    <cc1:FilteredTextBoxExtender ID="fteQuery" runat="server" FilterMode="InvalidChars"
                                        InvalidChars=":&quot;~!@#$%^&amp;*()?&gt;&lt;{}+=[];'|\~`" TargetControlID="txtQuery">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2">
                                    Upload file</label>
                                <div class="col-sm-4">
                                    <div id="divFileUpload" runat="server">
                                        <asp:FileUpload ID="fuQuery" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf', 'pdf', 4);" />
                                        <small><span style="color: Red;">Max Size Upto 4 MB in .pdf Format Only</span></small>
                                    </div>
                                    <asp:HyperLink ID="hypView" runat="server" CssClass="btn btn-sm btn-primary" Target="_blank"
                                        ToolTip="View Document" Visible="false">
                                    <i class="fa fa-download"></i>
                                    </asp:HyperLink>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2">
                                </label>
                                <div class="col-sm-4">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-sm btn-success"
                                        OnClick="btnSubmit_Click" OnClientClick="return ValidatePage();" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-sm btn-danger"
                                        OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
