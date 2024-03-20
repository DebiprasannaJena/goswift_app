<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PcViewPage.aspx.cs" Inherits="PcViewPage" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/incentive.css" rel="stylesheet" type="text/css" />
    <script src="js/jQuery.alert.js" type="text/javascript"></script>
    <link href="css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="js/WebValidation.js" type="text/javascript"></script>
    <script src="js/decimalrstr.js" type="text/javascript"></script>
    <script src="js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.bg-panelnoBG').find('.fa-minus').hide();
            $('.bg-panelnoBG .panel-heading a').click(function () {
                $(this).find('.fa-plus').toggle();
                $(this).find('.fa-minus').toggle();
                $('.menuPc').addClass('active');
            });

        });

        function valid() {
            var radioButtons = $('#<%=rbtnCompanyType.ClientID%>');
            var companytype = radioButtons.find('input:checked').val(); // selected value

            if (radioButtons == "") {
                $("#rbtnCompanyType").focus();
                jAlert('<strong>Please Select your Company Type!</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#rbtnCompanyType").focus(); });
                return false;
            }
            var strInvLabel = '';
            if (companytype == "41") {
                strInvLabel = "Investment for Equipment (in Lakh)";
            }
            else {
                strInvLabel = "Investment for Plant & Machinery(in Lakh)";
            }

            if (!blankFieldValidation("txtInvestment", strInvLabel, "GO-SWIFT")) {
                return false;
            }
            var inv = parseFloat($("#txtInvestment").val())
            if (inv == 0.00) {
                jAlert('<strong>' + strInvLabel + ' cannot be 0!</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtInvestment").focus(); });
                return false;
            }
        }
        
    </script>
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.6;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 900px;
            height: 550px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
         <uc2:header ID="header" runat="server" />
    <div class="container">
        <asp:HiddenField ID="hdnUserType" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       
        <div class="container wrapper">

        
        <div class="registration-div investors-bg">
            <div id="exTab1" class="">
                <div class="investrs-tab">
                    <uc4:investoemenu ID="ineste" runat="server" />
                </div>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="1a">
                        <div class="col-sm-8">
                            <div class="form-sec">
                                <div class="form-header">
                                    <h2>
                                        Unit Details</h2>
                                </div>
                                <div align="center">
                                    <asp:Label ID="lblMessage" Font-Bold="true" Font-Size="Medium" ForeColor="Red" runat="server"></asp:Label>
                                </div>
                                <div class="form-body minheight350" id="dvMain" runat="server">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <fieldset>
                                            <strong>Before applying for &nbsp;<font color="blue">Production Certificate</font>&nbsp;please
                                                enter the below details :</strong>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Please Select your Nature of Activity
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:RadioButtonList ID="rbtnCompanyType" runat="server" RepeatDirection="Horizontal"
                                                            Width="60%" AutoPostBack="true" OnSelectedIndexChanged="rbtnCompanyType_SelectedIndexChanged">
                                                            <asp:ListItem Value="40" Selected="True">Manufacturing
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="41">Services</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvinvestId" runat="server">
                                                <div class="row">
                                                    <label for="Iname" runat="server" id="lblInvestment" class="col-sm-4 col-sm-offset-1">
                                                        Investment for Plant & Machinery(in Lakh)
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtInvestment" runat="server" Width="200" MaxLength="15" CssClass="form-control"
                                                                    onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="txtInvestment_FilteredTextBoxExtender" runat="server"
                                                                    Enabled="True" FilterType="custom,Numbers" ValidChars="." TargetControlID="txtInvestment">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" align="center">
                                                <div class="row">
                                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClientClick="return valid();"
                                                        Text="Submit" OnClick="btnSubmit_Click" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlmain" runat="server" Visible="false">
                                        <div id="tPeal" runat="server">
                                            Based on the input provided:
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Nature of Activity
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblComType" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Investment Amount in Plant & Machinery(in Lakh)
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblInvAmt" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            Below are the derived details :
                                        </div>
                                        <div class="details-section">
                                            <div class="form-group" id="dvInd" runat="server">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Industry Code
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblIndustryCode" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Name of Enterprise/Industrial Unit
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="txtEnterpriseName" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" id="divEin" runat="server" visible="false">
                                                <div class="row">
                                                    <asp:Label ID="lblDocName" runat="server" CssClass="col-sm-4 col-sm-offset-1"></asp:Label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="txtEin" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" id="divUan" runat="server" visible="false">
                                                <div class="row">
                                                    <asp:Label ID="Label1" runat="server" CssClass="col-sm-4 col-sm-offset-1" Text="Udyog Aadhaar Number"></asp:Label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="txtUan" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Sector of Activity
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="ddlSector" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Sub Sector
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="ddlSubSector" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Unit Category
                                                    </label>
                                                    <div class="col-sm-6 margin-bottom10">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblUnit" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnUnitCat" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvOrg" runat="server">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Constitution of Organization
                                                    </label>
                                                    <div class="col-sm-6 margin-bottom10">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="drpOrganizationType" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    </label>
                                                    <div class="col-sm-6 margin-bottom10">
                                                        <strong>
                                                            <asp:HyperLink ID="hypApply" runat="server" CssClass="btn btn-success">Apply For PC</asp:HyperLink>
                                                            <asp:Button ID="btnOfflinePc" runat="server" CssClass="btn btn-primary" Visible="false"
                                                                ToolTip="Upload details of your production certificate not recieved throught GO-SWIFT portal"
                                                                Text="Upload Offline Pc" />
                                                        </strong>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-sec">
                            <div class="form-header">
                                <h2>
                                    Important Links
                                </h2>
                            </div>
                            <div class="form-body">
                                <div class="details-section">
                                    <ul>
                                        <li class="bg-panelnoBG">
                                            <div class="panel panel-default m-b-10">
                                                <div class="panel-heading" role="tab" id="headingTwo">
                                                    <h4 class="panel-title p-b-0">
                                                        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                            href="#IndustryDetails" aria-expanded="false" aria-controls="collapseTwo"><i class="more-less fa  fa-minus">
                                                            </i><i class="more-less fa  fa-plus"></i>View Inspection Status </a>
                                                    </h4>
                                                </div>
                                                <div id="IndustryDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="details-section">
                                                                    <asp:GridView ID="gvViewIR" runat="server" HeaderStyle-Height="30px" class="table table-bordered table-hover"
                                                                        AutoGenerateColumns="false" OnRowDataBound="gvViewIR_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Sl#">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="vchAppFormattedNo" HeaderText="App. No." />
                                                                            <asp:BoundField DataField="dtmIRScheduleOn" HeaderText="Scheduled Date" NullDisplayText="-" />
                                                                            <asp:BoundField DataField="ISSUEDATE" HeaderText="Issue Date" />
                                                                            <asp:TemplateField HeaderText="View Report" HeaderStyle-CssClass="noPrint" ItemStyle-CssClass="noPrint"
                                                                                ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:HyperLink ID="hypIRForm" Target="_blank" ToolTip="View Inspection Form" runat="server"> View                                                                                     
                                                                                    </asp:HyperLink>
                                                                                    <asp:HiddenField ID="hdnAppNo" runat="server" Value='<%#Eval("vchAppNo")%>' />
                                                                                    <asp:HiddenField ID="hdnAppSts" runat="server" Value='<%#Eval("intApproved")%>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                        <li class="bg-panelnoBG">
                                            <div class="panel panel-default m-b-10">
                                                <div class="panel-heading" role="tab" id="headingThree">
                                                    <h4 class="panel-title p-b-0">
                                                        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                            href="#Industry" aria-expanded="false" aria-controls="collapseTwo"><i class="more-less fa  fa-minus">
                                                            </i><i class="more-less fa  fa-plus"></i>View PC status </a>
                                                    </h4>
                                                </div>
                                                <div id="Industry" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="details-section">
                                                                    <asp:GridView ID="gvPCReport" runat="server" AutoGenerateColumns="false" class="table table-bordered table-hover"
                                                                        OnRowDataBound="gvPCReport_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Sl#">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="vchPCNo" HeaderText="PC No." />
                                                                            <asp:BoundField DataField="ISSUEDATE" HeaderText="Issue Date" />
                                                                            <asp:TemplateField HeaderText="View Report" HeaderStyle-CssClass="noPrint" ItemStyle-CssClass="noPrint"
                                                                                ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:HyperLink ID="hypPCForm" Target="_blank" ToolTip="View Inspection Form" runat="server">View                                                                                        
                                                                                    </asp:HyperLink>
                                                                                    <asp:HiddenField ID="hdnAppStatus" runat="server" Value='<%#Eval("intApproved")%>' />
                                                                                    <asp:HiddenField ID="hdnAppNo" runat="server" Value='<%#Eval("vchAppNo")%>' />
                                                                                    <asp:HiddenField ID="hdnFilePath" runat="server" Value='<%#Eval("vchPCFilePath")%>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                        <%--Added By Pranay Kumar for Addition of Query Details on 25-OCT-2017--%>
                                        <li class="bg-panelnoBG">
                                            <div class="panel panel-default m-b-10">
                                                <div class="panel-heading" role="tab" id="headingFour">
                                                    <h4 class="panel-title p-b-0">
                                                        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                            href="#dvQueryRevert" aria-expanded="false" aria-controls="collapseTwo"><i class="more-less fa  fa-minus">
                                                            </i><i class="more-less fa  fa-plus"></i>View Query Status </a>
                                                    </h4>
                                                </div>
                                                <div id="dvQueryRevert" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingFour">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="details-section">
                                                                    <asp:GridView ID="grdQuery" runat="server" HeaderStyle-Height="30px" class="table table-bordered table-hover"
                                                                        AutoGenerateColumns="false" OnRowDataBound="grdQuery_RowDataBound" DataKeyNames="strQueryUnqNo,strQuerytype">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Sl#">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="strApplicationNum" HeaderText="App. No." />
                                                                            <asp:TemplateField HeaderText="Query Status" HeaderStyle-CssClass="noPrint" ItemStyle-CssClass="noPrint"
                                                                                ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:HyperLink ID="hypQueryDtls" runat="server"></asp:HyperLink>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                                <asp:HyperLink ID="hypQueryDtls" runat="server"></asp:HyperLink>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                        <%--Ended By Pranay Kumar for Addition of Query Details on 25-OCT-2017--%>
                                    </ul>
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
    </div>
    <uc3:footer ID="footer" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlOfflinePc"
        TargetControlID="btnOfflinePc" BackgroundCssClass="modalBackground" CancelControlID="btnCancel">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlOfflinePc" runat="server" CssClass="modalfade" Style="display: none;">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-purpul">
                    <h4 class="modal-title">
                        Uploading an Offline Production Certificate
                    </h4>
                </div>
                <div class="modal-body">
                    <p>
                        This option is provided for all the investors who already have a Production Certificate
                        which has not been received through the GO-SWIFT portal.
                    </p>
                    <p>
                        As per the IPR 2015 if your unit is NOT an Existing EMD unit, then follow the below
                        steps:-
                    </p>
                    <p>
                        Upload your latest Production certificate/EM-II/IEM-II and fill requisite details
                        such as the Production Certificate Number, Issuance date along with the basic details
                        of your unit, production and employment details as well as the investment details.
                        The details filled in by you should be in compliance with the information mentioned
                        in the Production Certificate/EM-II/IEM-II being uploaded.
                    </p>
                    <p>
                        As per the IPR 2015, if your unit is an Existing EMD unit, then follow the below
                        steps :-
                    </p>
                    <p>
                        You will have to fill all the details for the Original Unit ( i.e. before IPR 2015
                        policy effective date). Production Certificate/EM-II/IEM-II is not necessary, but
                        provide all details for original unit. After that fill all the details for the E/M/D
                        unit along with the Production Certificate/EM-II/IEM-II details.
                    </p>
                    <p class="text-red">
                        N.B - In case you have more than one Production Certificate/EM-II/IEM-II, please
                        make sure to upload the details of the latest one and the corresponding unit details.
                    </p>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-sm-6">
                            <asp:HyperLink ID="hypOfflinePc" runat="server" CssClass="btn btn-primary" ToolTip="Upload details of your production certificate/EM-II/IEM-II not recieved throught GO-SWIFT portal">Continue</asp:HyperLink>
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
