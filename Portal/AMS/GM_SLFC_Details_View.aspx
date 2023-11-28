<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="GM_SLFC_Details_View.aspx.cs" Inherits="Portal_AMS_GM_SLFC_Details_View" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <script type="text/javascript" src="../../js/WebValidation.js"></script>
        <script src="../../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
        <script type="text/javascript">
            function pageLoad() {
                UpdateTextLength();
            }

            function UpdateTextLength() {
                var arrTr = $("#ContentPlaceHolder1_grdSLFC > tbody > tr");
                var cnt = 1;
                var totalCnt = $(arrTr).length;
                for (cnt = 0; cnt < totalCnt - 1; cnt++) {
                    var txtVal = $("#ContentPlaceHolder1_grdSLFC_txtQuery_" + cnt).val().trim();
                    if (txtVal != null && txtVal != undefined && txtVal != '') {
                        CheckLengthKeyUp('ContentPlaceHolder1_grdSLFC_txtQuery_' + cnt, 'ContentPlaceHolder1_grdSLFC_lblQuery_' + cnt, 1000);
                    }
                }
            }

            function ValidatePage() {
                if (confirm("Are you sure you want to Forward the details?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        </script>
        <asp:ScriptManager ID="sm1" runat="server">
        </asp:ScriptManager>
        <asp:HiddenField ID="hdnAction" runat="server" />
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    SLFC Query Details
                </h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>AMS</a></li><li><a>View</a></li></ul>
            </div>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-body">
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlmain" runat="server">
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Details of Proposal No :</label>
                                                <div class="col-sm-4">
                                                    <asp:Label ID="lblProposalNo" runat="server"></asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    Investor Name :</label>
                                                <div class="col-sm-4">
                                                    <asp:Label ID="lblInvestorName" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:GridView ID="grdSLFC" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found..."
                                            OnRowDataBound="grdSLFC_RowDataBound" CssClass="table table-bordered table-hover"
                                            DataKeyNames="intUserId">
                                            <Columns>
                                                <asp:BoundField HeaderText="User" DataField="strUsername" />
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnNoQueries" runat="server" Value='<%#Eval("BitNoQuery") %>' />
                                                        <asp:Label ID="lblQueries" runat="server" Text='<%#Eval("strRemarks") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Documents">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnFile" runat="server" Value='<%#Eval("strFileName") %>' />
                                                        <asp:HyperLink ID="hypFile" runat="server" Target="_blank" ToolTip="View Document"
                                                            Visible="false" CssClass="btn btn-sm btn-primary">
                                                                        <i class="fa fa-download"></i>
                                                        </asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="form-group row">
                                            <label class="col-sm-2">
                                                Comments<span style="color: Red;">*</span>:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="6" Columns="10"
                                                    MaxLength="1000" CssClass="form-control" Style="height: 150px !important"></asp:TextBox>
                                                <small>&nbsp;(Maximum&nbsp;
                                                    <asp:Label ID="lblComments" runat="server" Text="1000" ForeColor="Red"></asp:Label>
                                                    &nbsp; characters allowed)</small>
                                                <cc1:FilteredTextBoxExtender ID="fteComments" runat="server" FilterMode="InvalidChars"
                                                    InvalidChars=":&quot;~!@#$%^&amp;*()?&gt;&lt;{}+=[];'|\~`" TargetControlID="txtComments">
                                                </cc1:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2">
                                                Upload file:</label>
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
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Investor Response Date :</label>
                                                <div class="col-sm-4">
                                                    <asp:Label ID="lblResponseDate" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Investor Response:</label>
                                                <div class="col-sm-4">
                                                    <asp:Label ID="lblInvReply" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Investor Documents:</label>
                                                <div class="col-sm-4">
                                                    <asp:Repeater ID="rptDocuments" runat="server" OnItemDataBound="rptDocuments_DataBound">
                                                        <HeaderTemplate>
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
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <asp:Button ID="btnForwardForAgenda" runat="server" Text="Forward for Agenda" CssClass="btn btn-sm btn-success"
                                                        OnClick="btnForwardForAgenda_Click" OnClientClick="return confirm('Are you sure you want to Forward for Agenda?');" />
                                                    <asp:Button ID="btnForward" runat="server" Text="Forward to Investor" CssClass="btn btn-sm btn-success"
                                                        OnClick="btnForward_Click" OnClientClick="return ValidatePage();" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-sm btn-danger"
                                                        OnClick="btnCancel_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnForward" />
                                    <asp:PostBackTrigger ControlID="btnForwardForAgenda" />
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
