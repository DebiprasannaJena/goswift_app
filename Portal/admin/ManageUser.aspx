<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ManageUser.aspx.cs" Inherits="Portal_admin_ManageUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
    var appendId = "ContentPlaceHolder1_";
        $(document).ready(function () {
            $('.ddluser').chosen({ allow_single_deselect: true, no_results_text: 'No Item found for ' });
        });

        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "100%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    for (var selector in config) {
                        $(selector).chosen(config[selector]);
                    }

                }
            });
        };

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);

        function pageLoad() {
            $('.ddluser').chosen({ allow_single_deselect: true, no_results_text: 'No Item found for ' });

            ("#rdBtnOrg input[type=radio]").onchange
        }
    </script>
    <style type="text/css">
        .chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
        .chosen-container .chosen-container-single .chosen-single
        {
            width: 100% !important;
        }
        .searchbox
        {
            background-color: #def3ff;
            padding: 8px;
        }
    </style>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    Manage user</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Proposal</a></li><li><a>View</a></li></ul>
            </div>
        </section>
        <section class="content">
            <div class="row">
                <!-- Form controls -->
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-body">
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                Select User</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="ddlUser" runat="server" class="form-control" CssClass="chosen-select-width ddluser">
                                            </asp:DropDownList>
                                            <span class="mandetory">*</span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="Action">
                                                Select Action</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:RadioButtonList ID="rdBtnAction" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                RepeatLayout="Table" AutoPostBack="true" OnSelectedIndexChanged="rdBtnAction_SelectedIndexChanged">
                                                <asp:ListItem Text="Unlock User" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Lock User" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Reset Password" Value="3"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="form-group row" id="divUnlockUser" runat="server" visible="false">
                                        <div class="col-sm-2">
                                            <label for="Action">
                                                No. of hours for which user is locked</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtNoOfHours" runat="server" MaxLength="2" CssClass="form-control"> </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row" id="divPassword" runat="server" visible="false">
                                        <div class="col-sm-2">
                                            <label for="Action">
                                                Enter New Password</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtPassword" runat="server" MaxLength="50" CssClass="form-control"
                                                TextMode="Password" AutoCompleteType="disabled" autocomplete="false" onPaste="return false"> </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row" id="divConfirmPassword" runat="server" visible="false">
                                        <div class="col-sm-2">
                                            <label for="Action">
                                                Re-Type New Password</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtReTypePassword" runat="server" MaxLength="50" CssClass="form-control"
                                                TextMode="Password" AutoCompleteType="disabled" autocomplete="false" onPaste="return false"> </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-3">
                                            <asp:Button ID="btnUpdatePassword" runat="server" CssClass="btn btn-sm btn-success"
                                                Text="Update Password" Visible="false" OnClick="btnUpdatePassword_Click" />
                                            <asp:Button ID="btnLockUser" runat="server" CssClass="btn btn-sm btn-success" Text="Lock"
                                                Visible="false" OnClick="btnLockUser_Click" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-sm btn-danger" Text="Cancel"
                                                Visible="false" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <link href="../Chosen/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../Chosen/chosen.jquery.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
