<%--'*******************************************************************************************************************
' File Name         : ThirdPartyAppVerification.aspx
' Description       : This page is used to display the service status of a particular application on the public page.
' Created by        : Sushant Kumar Jena
' Created On        : 13-Oct-2020
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>



<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ThirdPartyAppVerification.aspx.cs"
    Inherits="ThirdPartyAppVerification" %>

<!DOCTYPE html>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <title>SWP(Single Window Portal)</title>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src='//code.jquery.com/jquery-1.8.3.js'></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/css/bootstrap-datepicker3.min.css">
    <script type='text/javascript' src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/js/bootstrap-datepicker.min.js"></script>
    <script type='text/javascript'>
        $(function () {
            $('.input-group.date').datepicker({
                calendarWeeks: true,
                todayHighlight: true,
                autoclose: true
            });
        });

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';
        function ValidateApp() {

            if (blankFieldValidation('txtApplicationNo', 'Application no', projname) == false) {
                return false;
            }

            if (blankFieldValidation('txtCaptcha', 'Captcha', projname) == false) {
                return false;
            }
        }

    </script>
    <style>
        .panel-default > .panel-heading
        {
            padding: 8px 15px !important;
            font-size: 18px;
            text-transform: uppercase;
        }
        .note
        {
            border: 1px solid #f0f0f0;
            padding: 15px;
            border-radius: 4px;
            background: #f9f9f9;
        }
        .note ol
        {
            margin-bottom: 0;
        }
        .note ol li
        {
            font-style: italic;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="content-form-section">
            <div class="col-sm-12">
                <div class="aboutcontent-sec">
                    <span class="pull-right text-danger">(*) Indicate Mandatory</span>
                    <h2 class=" margin-bottom15">
                        Third Party Application Verification
                    </h2>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Application Details</div>
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <label>
                                            Application Number
                                        </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox CssClass="form-control" ID="txtApplicationNo" runat="server" autocomplete="off" ToolTip="Enter Application No Here !"></asp:TextBox>
                                        <span class="mandetory">*</span>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                            TargetControlID="txtApplicationNo" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            ValidChars="a-zA-Z0-9/">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                    <div class="col-sm-5">
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <label>
                                            Enter Captcha
                                        </label>
                                    </div>
                                    <div class="col-sm-2">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtCaptcha" runat="server" placeholder="Enter Captcha" CssClass="form-control"
                                            MaxLength="6" autocomplete="off"></asp:TextBox>
                                        <a data-toggle="tooltip" class="fieldinfo" title="Enter the characters shown in image !">
                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                            TargetControlID="txtCaptcha" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                            InvalidChars="&quot;'<>&;">
                                        </cc1:FilteredTextBoxExtender>
                                        <span class="mandetory">*</span>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="input-group">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <cc4:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                                        CaptchaMinTimeout="5" CaptchaMaxTimeout="240" CssClass="homecaptchaimg" NoiseColor="#B1B1B1" />
                                                    <div class="refresh">
                                                        <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false">
                                                        <span class="fa fa-refresh homerefreshbtn input-group-addon" style="cursor: pointer;" onclick="return RefreshCaptcha();"
                                                                                aria-hidden="true"></span></asp:LinkButton>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <label>
                                        </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="BtnSearch" CssClass="btn btn-success" runat="server" Text="Search"
                                            OnClick="BtnSearch_Click" OnClientClick="return ValidateApp();" ToolTip="Click here to search !" />
                                    </div>
                                    <div class="col-sm-5">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="grdService" class="table table-bordered table-hover" runat="server"
                            AutoGenerateColumns="false" Width="100%" EmptyDataText="No Record(s) Found, Please Check Your Application Number">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <%-- <asp:HiddenField ID="hdnFileName1" runat="server" Value='<%#Eval("vch_application_unq_key") %>' />
                                        <asp:HiddenField ID="hdnFileName2" runat="server" Value='<%#Eval("int_action_taken_by") %>' />
                                        <asp:HiddenField ID="hdnFileName3" runat="server" Value='<%#Eval("int_action_tobe_taken_by") %>' />--%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="vch_application_unq_key" HeaderText="Application No" />
                                <asp:BoundField DataField="vch_servicename" HeaderText="Service Name" />
                                <asp:BoundField DataField="vch_proposalid" HeaderText="Proposal Id" />
                                <asp:BoundField DataField="vch_investor_name" HeaderText="Investor Name" />
                                <asp:TemplateField HeaderText="Created On">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_Created_On" runat="server" Text='<%# Eval("dtm_CreatedOn" ,"{0:dd-MMM-yyyy HH:mm tt}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="int_payment_status" HeaderText="Payment Status" />--%>
                                <asp:BoundField DataField="vchStatusName" HeaderText="Application Status" />
                                <%--<asp:BoundField DataField="ActionTaken" HeaderText="Action Taken By" />--%>
                                <%-- <asp:BoundField DataField="ActionToBeTaken" HeaderText="Action to be Taken By" />--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
