<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScheduleInspection.aspx.cs"
    MasterPageFile="~/MasterPage/Application.master" Inherits="Portal_Incentive_ScheduleInspection" %>

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
                                <asp:HiddenField ID="hdnType" runat="server"></asp:HiddenField>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
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
                                                        Application For</label>
                                                    <div class="col-sm-4">
                                                        <label id="lblApplicationFor" runat="server">
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Company Name</label>
                                                    <div class="col-sm-4">
                                                        <label id="lblCompanyName" runat="server">
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Unit Category</label>
                                                    <div class="col-sm-4">
                                                        <label id="lblUnitCat" runat="server">
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Organisation Type</label>
                                                    <div class="col-sm-4">
                                                        <label id="lblOrgType" runat="server">
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Schedule Date of Inspection</label>
                                                    <div class="col-sm-4">
                                                        <div class="input-group  date datePicker">
                                                            <input name="txtInspectionDt" type="text" id="txtDateOfInspection" class="form-control datePicker"
                                                                runat="server" />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Remark</label>
                                                    <div class="col-sm-4">
                                                        <div class="">
                                                            <asp:TextBox ID="txtRemark" Rows="5" onKeyUp="limitText(this,this.form.count,200);"
                                                                TextMode="MultiLine" MaxLength="200" CssClass="form-control" runat="server" onKeyDown="limitText(this,this.form.count,200);"></asp:TextBox>
                                                            <a href="#" data-toggle="tooltip" class="fieldinfo" title="It can accept both alphabets and numbers.Special Characters like !  # $ % & ( )  + , - . / : ; = ? @ [ \ ] are allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a><small><small>Maximum
                                                                    <input name="count" class="inputCss" readonly="readonly" style="font-weight: bold;
                                                                        color: red; width: 26px;" type="text" value="200" />
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
                                                    <div class="col-sm-9 col-sm-offset-2">
                                                        <div class="row">
                                                            <div class="col-sm-9 col-sm-offset-2">
                                                                <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnSubmit_Click" />
                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
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
        $(document).ready(function () {
            $('.datePicker').datepicker({
                format: 'dd-M-yyyy',
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });
        });
        function pageLoad() {
            var txtDesc = $("#txtRemark").val().length;
            $('.inputCss').val(200 - txtDesc);



        }



        $(window).load(function () {

            $('#btnSave').click(function () {
                var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

                if (blankFieldValidation('txtDateOfInspection', 'Schedule Date of Inspection', projname) == false) {
                    $("#txtDateOfInspection").focus();
                    return false;
                };

                var dtmFfi = new Date($("#txtDateOfInspection").val());
                var currDate = new Date();
                dtmFfi.setHours(0, 0, 0, 0);
                currDate.setHours(0, 0, 0, 0);

                if (dtmFfi < currDate) {
                    jAlert('<strong>Schedule Date of Inspection cannot be less than current date</strong>', 'SWP');
                    $("#txtDateOfInspection").focus();
                    return false;
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





        function alertredirect(msg) {
            jAlert('Inspection Scheduled Successfully !', 'SWP', function (r) {

                if (r) {

                    location.href = 'ViewIncentiveApplication.aspx';
                    return true;
                }
                else {
                    return false;
                }
            });
        }
    </script>
</asp:Content>
