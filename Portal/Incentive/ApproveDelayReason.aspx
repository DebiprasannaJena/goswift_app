<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApproveDelayReason.aspx.cs"
    MasterPageFile="~/MasterPage/Application.master" Inherits="Portal_Incentive_ApproveDelayReason" %>

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
               Empowered Committee Approval</h1>
            <ul class="breadcrumb">
                <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                <li><a>View Delay Reason</a></li></ul>
        </div>
         </section>
        <!-- Main content -->
        <section class="content">
        <div class="row">
            <!-- Form controls -->
            <div class="col-sm-12">
                <div class="panel panel-bd lobidisable">
                    <div class="panel-body">
                        <div class="ibox-content">
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Enterprise Name</label>
                                    <div class="col-sm-8">
                                        <span class="colon">:</span>
                                        <asp:Label ID="Lbl_Enterprise_Name" runat="server" CssClass="form-control-static">
                                        </asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Unit Category</label>
                                    <div class="col-sm-8">
                                        <span class="colon">:</span>
                                        <asp:Label ID="Lbl_Unit_Cat" runat="server" CssClass="form-control-static"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Unit Type</label>
                                    <div class="col-sm-8">
                                        <span class="colon">:</span>
                                        <asp:Label ID="Lbl_Unit_Type" runat="server" CssClass="form-control-static"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        View Details</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span> <a href="#" onclick='openWindow();' class="btn btn-success btn-sm" title="Click Here to View Application Details !!">
                                            <i class='fa fa-eye' aria-hidden='true'></i></a>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Status</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:DropDownList ID="DrpDwn_Status" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Approve" Value="2" />
                                            <asp:ListItem Text="Reject" Value="3" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="dvSanctionUp" runat="server">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Upload EC Letter
                                    </label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:FileUpload ID="FU_EC_Letter" CssClass="form-control" runat="server" onchange="return FileExtension(this);" />
                                        <small><span style="color: Red;">Max Size Upto 4 MB and pdf Format Only</span></small>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Time Period Allowed</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_Time_Allowed" MaxLength="2" CssClass="form-control" runat="server"
                                            Style="width: 40px; margin-right: 10px; display: inline-block"></asp:TextBox><span>Month(s)</span>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="Txt_Time_Allowed">
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
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_Remark" Rows="5" onKeyUp="limitText(this,this.form.count,250);"
                                            TextMode="MultiLine" MaxLength="250" CssClass="form-control" runat="server" onKeyDown="limitText(this,this.form.count,250);"></asp:TextBox>
                                        <a href="#" data-toggle="tooltip" class="fieldinfo" title="It can accept both alphabets and numbers.Special Characters like # & ( )  + , - . / : ; = ? @ [ \ ] are allowed">
                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a><small>Maximum
                                                <input name="count" class="inputCss" readonly="readonly" style="font-weight: bold;
                                                    color: red; width: 26px;" type="text" value="250" />
                                                Characters Left</small>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                            FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" ValidChars="# & ( ) + , - . / : ; = ? @ [ \ ] "
                                            FilterMode="ValidChars" TargetControlID="Txt_Remark">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-9 col-sm-offset-2">
                                        <div class="row">
                                            <div class="col-sm-9 col-sm-offset-2">
                                                <asp:Button ID="Btn_Save" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="return validateECApprove();"
                                                    OnClick="Btn_Submit_Click" />
                                                <a href='ViewEMPCommitteApproval.aspx?linkm=<%= Request.QueryString["linkm"] %>&linkn=<%= Request.QueryString["linkn"] %>&btn=<%= Request.QueryString["btn"] %>&tab=<%= Request.QueryString["tab"] %>'
                                                    class="btn  btn-primary">Cancel</a>
                                                <asp:HiddenField ID="Hid_Delay_Id" runat="server"></asp:HiddenField>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </section>
    </div>
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';
        var AllowPeriod = '<%=System.Configuration.ConfigurationManager.AppSettings["AllowPeriod"] %>';

        /*-------------------------------------------------------------------*/

        function FileExtension(e) {
            var ids = e.id;
            var fileExtension = ['pdf'];
            if ($.inArray($("#" + ids).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                jAlert('<strong>Only pdf files are allowed !!</strong>', projname);
                $("#" + ids).val(null);
                return false;
            }
            else {
                if ((e.files[0].size > 4194304) && ($("#" + ids).val() != '')) {
                    jAlert('<strong>File size must be less than 4 MB !!</strong>', projname);
                    $("#" + ids).val(null);
                    e.preventDefault();
                    return false;
                }
                else {
                }
            }
        }

        /*-------------------------------------------------------------------*/

        function limitText(limitField, limitCount, limitNum) {
            if (limitField.value.length > limitNum) {
                limitField.value = limitField.value.substring(0, limitNum);
            } else {
                limitCount.value = limitNum - limitField.value.length;
            }
        }
        /*-------------------------------------------------------------------*/

        function validateECApprove() {
            if (!DropDownValidation('DrpDwn_Status', '0', 'Status', projname)) {
                return false;
            }
            if ($("#FU_EC_Letter").val() == "") {
                jAlert('<strong>Please Upload EC Letter.</strong>', projname);
                return false;
            }
            if (!blankFieldValidation('Txt_Remark', 'Remark', projname)) {
                return false;
            };
            if (!WhiteSpaceValidation1st('Txt_Remark', 'Remark', projname)) {
                return false;
            }
        }

        /*-------------------------------------------------------------------*/

        function alertredirect(msg) {
            jAlert('Data Saved Successfully !', projname, function (r) {
                if (r) {

                    location.href = 'ViewEMPCommitteApproval.aspx' + msg;
                    return true;
                }
                else {
                    return false;
                }
            });
        }

        /*-------------------------------------------------------------------*/

        $(window).load(function () {
            $("#Txt_Time_Allowed").val(AllowPeriod);
        });


        function openWindow() {
            var code = $("#Hid_Delay_Id").val();
            window.open('ViewDelayReasonDetails.aspx?Did=' + code, 'open_window', ' width=1050, height=600, left=0, top=0');
        }

    </script>
</asp:Content>
