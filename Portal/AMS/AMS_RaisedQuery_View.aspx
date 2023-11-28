<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="AMS_RaisedQuery_View.aspx.cs" Inherits="Portal_AMS_AMS_RaisedQuery_View" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../../js/WebValidation.js"></script>
    <script src="../../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ExModal() {
            $('#ContentPlaceHolder1_slfcModal').modal();
        }
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
    <style type="text/css">
        .h4Class
        {
            margin: 8px 0px 8px;
            font-size: 15px;
            font-weight: 600;
            background: #eff7ff;
            padding: 6px 6px;
        }
    </style>
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
                    Query raised by AMS
                </h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>AMS</a></li><li><a>View</a></li></ul>
            </div>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-body">
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <div style="text-align: right; width: 100%; text-align: right;" class="noprint pagelist">
                                            <asp:Label ID="lblDetails" runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlNoOfRec" ToolTip="Page Size" Width="80px" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRec_SelectedIndexChanged"
                                                CssClass="form-control" Style="display: inline">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:GridView ID="grdQuery" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found..."
                                        OnRowDataBound="grdQuery_RowDataBound" CssClass="table table-bordered table-hover"
                                        DataKeyNames="intQueryConfigId" OnRowCommand="grdQuery_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl#">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Proposal No">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hypProposalNo" runat="server" Text='<%#Eval("strProposalNo") %>'></asp:HyperLink>
                                                    <asp:HiddenField ID="hdnIsPassed" runat="server" Value='<%#Eval("isDatePassed") %>' />
                                                    <asp:HiddenField ID="hdnIsUpdated" runat="server" Value='<%#Eval("isUpdated") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Investor name" DataField="strInvName" />
                                            <asp:BoundField HeaderText="Applied Date" DataField="strInvAppliedDate" />
                                            <asp:BoundField HeaderText="GM Forwarded Date" DataField="strForwardedDate" />
                                            <asp:BoundField HeaderText="No of Time" DataField="strQueryTime" />
                                            <asp:BoundField HeaderText="Last Date to Reply" DataField="strReplyDate" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkSLFCDetails" runat="server" Text="Add Query" ToolTip="View Queries provided by SLFC"
                                                        CommandArgument="<%#Container.DataItemIndex %>" CommandName="slfc"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkInvestorDetails" runat="server" Text="Add Query" ToolTip="View response provided by Investor"
                                                        CommandArgument="<%#Container.DataItemIndex %>" CommandName="inv"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div style="float: right;" class="noPrint" id="divPaging" runat="server">
                                        <asp:Repeater ID="rptPager" runat="server">
                                            <ItemTemplate>
                                                <ul class="pagination">
                                                    <li class='<%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %> '>
                                                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                            OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <%--   <asp:HiddenField ID="HiddenField1" runat="server" Value="Blank Value" />--%>
                                        <asp:HiddenField ID="hdnPgindex" runat="server" Value="Blank Value" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="container">
                                <asp:UpdatePanel ID="updModal" runat="server">
                                    <ContentTemplate>
                                        <div id="slfcModal" role="dialog" class="modal fade" runat="server">
                                            <div class="modal-dialog modal-lg">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <div style="text-align: left;">
                                                            <h4 class="modal-title">
                                                                SLFC Details of Proposal :
                                                                <asp:Label ID="lblProposalNo" runat="server"></asp:Label></h4>
                                                        </div>
                                                        <div style="float: right; margin-top: -16px">
                                                            <a href="#" class="close" data-dismiss="modal"><i class="fa fa-times"></i></a>
                                                        </div>
                                                    </div>
                                                    <div class="modal-body">
                                                        <asp:HiddenField ID="hdnQueryConfigId" runat="server" />
                                                        <h4 class="h4-header h4Class">
                                                            GM Comments</h4>
                                                        <div class="form-group">
                                                            <label class="col-md-6">
                                                                <asp:Label ID="lblGMComments" runat="server"></asp:Label></label>
                                                            <div class="clearfix">
                                                            </div>
                                                        </div>
                                                        <h4 class="h4-header h4Class" id="h4SLFCComments" runat="server">
                                                            SLFC Comments</h4>
                                                        <div class="form-group">
                                                            <div class="col-md-12">
                                                                <asp:GridView ID="grdSLFC" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found..."
                                                                    OnRowDataBound="grdSLFC_RowDataBound" CssClass="table table-bordered table-hover"
                                                                    DataKeyNames="intUserId">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SL#">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="User" DataField="strUsername" />
                                                                        <asp:TemplateField HeaderText="Remarks">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("strRemarks") %>'></asp:Label>
                                                                                <asp:HiddenField ID="hdnQuery" runat="server" Value='<%#Eval("BitNoQuery") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Files">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnFile" runat="server" Value='<%#Eval("strFileName") %>' />
                                                                                <asp:HyperLink ID="hypFile" runat="server" Target="_blank" ToolTip="View Document"
                                                                                    Visible="false">
                                                                        <i class="fa fa-download"></i>
                                                                                </asp:HyperLink>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                            <div class="clearfix">
                                                            </div>
                                                        </div>
                                                        <h4 class="h4-header h4Class">
                                                            Investor Response</h4>
                                                        <div class="form-group">
                                                            <label class="col-md-6">
                                                                Response Date :- &nbsp;
                                                                <asp:Label ID="lblResponseDate" runat="server"></asp:Label></label>
                                                            <div class="clearfix">
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-6">
                                                                Response :- &nbsp;
                                                                <asp:Label ID="lblInvReply" runat="server"></asp:Label></label>
                                                            <div class="clearfix">
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                <asp:Repeater ID="rptDocuments" runat="server" OnItemDataBound="rptDocuments_DataBound">
                                                                    <HeaderTemplate>
                                                                        <span style="font-size: 13px; font-weight: 600;">Investor Documents :- </span>
                                                                        <br />
                                                                        <ol>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <li>
                                                                            <asp:HyperLink ID="hypDocuments" runat="server" Text='<%#Eval("strFilename")%>' Target="_blank">
                                                                            </asp:HyperLink></li>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </ol>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                            <div class="clearfix">
                                                            </div>
                                                        </div>
                                                        <h4 class="h4-header h4Class">
                                                            Nodal Officer Comments</h4>
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                <asp:CheckBox ID="chkNoQuery" runat="server" Text="No Queries" />
                                                            </div>
                                                            <div class="clearfix">
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2">
                                                                Query<span style="color: Red;">*</span></label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtQuery" runat="server" TextMode="MultiLine" Rows="4" Columns="10"
                                                                    MaxLength="500" CssClass="form-control"></asp:TextBox>
                                                                <small>&nbsp;(Maximum&nbsp;
                                                                    <asp:Label ID="lblQuery" runat="server" Text="500" ForeColor="Red"></asp:Label>
                                                                    &nbsp; characters allowed)</small>
                                                                <cc1:FilteredTextBoxExtender ID="fteQuery" runat="server" FilterMode="InvalidChars"
                                                                    InvalidChars=":&quot;~!@#$%^&amp;*()?&gt;&lt;{}+=[];'|\~`" TargetControlID="txtQuery">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </div>
                                                            <div class="clearfix">
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2">
                                                                Upload file</label>
                                                            <div class="col-md-4">
                                                                <div id="divFileUpload" runat="server">
                                                                    <asp:FileUpload ID="fuQuery" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf', 'pdf', 4);" />
                                                                    <small><span style="color: Red;">Max Size Upto 4 MB in .pdf Format Only</span></small>
                                                                </div>
                                                                <asp:HyperLink ID="hypView" runat="server" CssClass="btn btn-md btn-primary" Target="_blank"
                                                                    ToolTip="View Document" Visible="false">
                                                                     <i class="fa fa-download"></i>
                                                                </asp:HyperLink>
                                                            </div>
                                                            <div class="clearfix">
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2">
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-md btn-success"
                                                                    OnClick="btnSubmit_Click" OnClientClick="return ValidatePage();" />
                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-md btn-danger"
                                                                    OnClick="btnCancel_Click" />
                                                            </div>
                                                            <div class="clearfix">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSubmit" />
                                        <asp:PostBackTrigger ControlID="btnCancel" />
                                    </Triggers>
                                </asp:UpdatePanel>
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
