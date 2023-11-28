<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductionCertificate.aspx.cs"
    Inherits="Portal_Incentive_ProductionCertificate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SWP</title>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/jQuery.alert.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/PcPrint.css" rel="Stylesheet" type="text/css" />
    <script src="../../js/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../js/jQuery.alert.js" type="text/javascript"></script>
    <script src="../../js/WebValidation.js" type="text/javascript"></script>
    <script src="../../js/decimalrstr.js" type="text/javascript"></script>
    <script src="../../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad() {
            $('.datePicker').datepicker({
                format: 'dd-M-yyyy',
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });
        }


        function ValidatePage() {
            debugger;
          
            prodComDate = $("#hdnProdDate").val();
            prodComDt = new Date(prodComDate);
            prodComDate = prodComDt.setHours(0, 0, 0, 0);

            if ($("#divNewPc").is(":visible")) {
                currIssuedt = new Date($("#txtDateOfIssue").val());
                CurrDt = currIssuedt.setHours(0, 0, 0, 0);
                var dtmInspectionRpt = new Date($("#hdnInspectionReport").val());
                dtmInspectionRpt.setHours(0, 0, 0, 0);
                var todayDate = new Date();
                todayDate.setHours(0, 0, 0, 0);
                if (!blankFieldValidation('txtDateOfIssue', "Date of Issue", 'GO-SWIFT')) {
                    return false;
                }
                if (CurrDt > todayDate) {
                    jAlert('<strong>Issue date cannot be greater than current date', 'GO-SWIFT');
                    return false;
                }
                if (CurrDt < prodComDt) {
                    jAlert('<strong>Issue date cannot be before Production Commencement Date :</strong>' + prodComDate, 'GO-SWIFT');
                    return false;
                }
                if (!blankFieldValidation('txtPlaceNew', "Place", 'GO-SWIFT')) {
                    return false;
                }
                var hdnFileName = $("#hdnOrgDocument").val();
                if (hdnFileName == '' || hdnFileName == undefined || hdnFileName == null) {
                    jAlert('<strong>Please upload signature.</strong>', 'GO-SWIFT');
                    return false;
                }
            }
            if ($("#divAmendment").is(":visible")) {
                var issuedt = $("#hdnLastIssue").val();
                var lastissuedt = new Date(issuedt);
                issuedt = lastissuedt.setHours(0, 0, 0, 0);

                currIssuedt = new Date($("#txtAmendedOn").val());
                CurrDt = currIssuedt.setHours(0, 0, 0, 0);

                var dtmInspectionRpt = new Date($("#hdnInspectionReport").val());
                dtmInspectionRpt.setHours(0, 0, 0, 0);
                if (!blankFieldValidation('txtAmendedOn', "Date of Amendement", 'GO-SWIFT')) {
                    return false;
                }
                if ($("#dvChange").is(":visible")) {
                    if (!blankFieldValidation('txtDatefchange', "Date of change of category from Micro / Small to Small / Medium or vice versa", 'GO-SWIFT')) {
                        return false;
                    }
                }
                if (CurrDt > todayDate) {
                    jAlert('<strong>Issue date cannot be greater than current date', 'GO-SWIFT');
                    return false;
                }
                if (CurrDt < prodComDt) {
                    jAlert('<strong>Amendment date cannot be before Production Commencement Date :</strong>' + prodComDate, 'GO-SWIFT');
                    return false;
                }
                if (lastissuedt > CurrDt) {
                    jAlert('<strong>Amendment date cannot be before Issue date of last Production Certificate :</strong>' + issuedt, 'GO-SWIFT');
                    return false;
                }
                if (!blankFieldValidation('txtPlaceAmd', "Place", 'GO-SWIFT')) {
                    return false;
                }
                var hdnSecondSign = $("#hdnSecondSign").val();
                if (hdnSecondSign == '' || hdnSecondSign == undefined || hdnSecondSign == null) {
                    jAlert('<strong>Please upload signature.</strong>', 'GO-SWIFT');
                    return false;
                }
            }
            if (confirm('Are you sure you want to update the details?', 'GO-SWIFT')) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <asp:ScriptManager ID="sm1" runat="server">
        </asp:ScriptManager>
        <div class="print-area">
            <div class="header ">
                <asp:HiddenField ID="hdnInspectionReport" runat="server" />
                <div class="heading-sec text-center">
                    <h1>
                        Production Certificate</h1>
                    <h4>
                        (To be issued by RIC / DIC / Director of Industries – online)</h4>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div id="divNewPc" runat="server" visible="false">
                <div class="info-secs padding5">
                    <p>
                        1.M/s.<asp:Label ID="lblUnit" runat="server" Font-Underline="true"></asp:Label>,
                        <asp:Label ID="lblOwnerType" runat="server"></asp:Label>
                        -
                        <asp:Label ID="lblOwner" runat="server" Font-Underline="true"></asp:Label>
                        has filed the Self Declaration Memorandum who has established a
                        <asp:Label ID="lblCompanyType" runat="server"></asp:Label>
                        Enterprise at
                        <asp:Label ID="lblEnterpriseAdd" runat="server" Font-Underline="true"></asp:Label>
                        and is
                        <asp:Label ID="lblComType" runat="server"></asp:Label>
                        as given below :</p>
                    <p>
                        2. Date of First Fixed Capital Investment in the Project (for new enterprises)
                        <asp:Label ID="lblFirstDate" Font-Bold="true" Font-Underline="true" runat="server"></asp:Label>
                    </p>
                    <p>
                        3. Details Of Item(s)
                        <asp:Label ID="lblComType2" runat="server"></asp:Label>(for new Enterprises)
                    </p>
                    <div class="table-responsive">
                        <asp:GridView ID="grdProducts" runat="server" class="table table-bordered table-hover"
                            AutoGenerateColumns="False" GridLines="None" OnRowDataBound="grdProducts_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="SLNO.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ITEMS OF MANUFACTURE / TYPE OF SERVICE(S) RENDERED">
                                    <ItemTemplate>
                                        <asp:Label ID="Label0" runat="server" Text='<%#Eval("item") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QUANTITY">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("Qty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UNIT">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("unit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VALUE(IN INR LAKH)">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("cost") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DATE OF PRODUCTION / COMMENCEMENT OF SERVICE">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("dtmProd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <p>
                        4. DETAILS OF PLANT AND MACHINERY AS PER DATE-WISE INVESTMENT (for new enterprises):
                    </p>
                    <asp:GridView ID="gvPlant" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found..."
                        CssClass="table table-bordered" DataKeyNames="id" Style="width: 100%;">
                        <Columns>
                            <asp:TemplateField HeaderText="SLNO." ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="PLANT & MACHINERY NAME" DataField="MachineryName" />
                            <asp:BoundField HeaderText="DATE OF PURCHASE" DataField="DateofPurchase" />
                            <asp:BoundField HeaderText="AMOUNT" DataField="Cost" />
                        </Columns>
                    </asp:GridView>
                    <p>
                        PRODUCTION CERTIFICATE NUMBER :
                        <asp:Label ID="lblAppNo" runat="server"></asp:Label>
                    </p>
                    <br />
                    <div style="width: 100%;" id="divNewDate" runat="server">
                        <strong style="float: left; margin: 4px;">Date of Issue:</strong>
                        <div class="input-group  date datePicker" style="width: 30%; float: left;">
                            <input name="txtTimescheduleforyearofcomm" type="text" id="txtDateOfIssue" class="form-control datePicker"
                                runat="server">
                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                        </div>
                        <div style="clear: both;">
                        </div>
                    </div>
                    <br />
                    <div>
                        <strong>NOTE: THE ISSUE OF THIS PRODUCTION CERTIFICATE DOES NOT BESTOW ANY LEGAL RIGHT
                            WITH RESPECT TO REQUISITE CLEARANCE / LICENCE / PERMIT. THE ENTERPRISE IS REQUIRED
                            TO SEEK REQUISITE CLEARANCE / LICENCE / PERMIT REQUIRED UNDER STATUTORY OBLIGATION
                            STIPULATED UNDER THE LAWS OF CENTRAL GOVERNMENT / STATE GOVERNMENT ADMINISTRATIONS
                            / COURT ORDERS'.</strong><br />
                    </div>
                    <br />
                    <div>
                        <div style="width: 30%; float: left;">
                            <asp:Label ID="lblPlace" runat="server" Text="Place :" Style="float: left; margin: 4px;"></asp:Label>
                            <asp:TextBox ID="txtPlaceNew" runat="server" MaxLength="100" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftePlaceNew" runat="server" FilterMode="InvalidChars"
                                InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;,{}+=[];',/|\_-~`" TargetControlID="txtPlaceNew">
                            </cc1:FilteredTextBoxExtender>
                            <div style="clear: both;">
                            </div>
                        </div>
                        <div style="width: 70%; float: right;">
                            <asp:Label ID="lblSignature" runat="server" Text="Signature with Office Seal" Style="width: 50%;
                                float: right"></asp:Label>
                            <br />
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <div class="input-group" style="width: 50%; float: right">
                                        <asp:FileUpload ID="fuOrgDocument" CssClass="form-control" runat="server" />
                                        <br />
                                        <asp:HiddenField ID="hdnOrgDocument" runat="server" />
                                        <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                        <asp:LinkButton ID="lnkOrgDocumentPdf" runat="server" CssClass="input-group-addon bg-green"
                                            OnClick="lnkUploadSignature_Click"><i class="fa fa-upload" aria-hidden="true" ></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkOrgDocumentDelete" runat="server" CssClass="input-group-addon bg-red"
                                            Visible="false" OnClick="lnkOrgDocumentDelete_Click"><i class="fa fa-trash-o" aria-hidden="true" ></i></asp:LinkButton>
                                        <asp:HyperLink ID="hypOrdDocument" runat="server" Target="_blank" Visible="false"
                                            CssClass="input-group-addon bg-blue">
                                                          <i class="fa fa-cloud-download"></i></asp:HyperLink>
                                    </div>
                                    <asp:Label ID="lblOrgDocument" Style="font-size: 12px;" CssClass="text-blue" runat="server"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkOrgDocumentPdf" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div style="width: 70%; float: right;">
                                <asp:Label ID="lblSignatory" runat="server" Font-Bold="true" Text="DIRECTOR OF INDUSTRIES ODISHA,CUTTACK"
                                    Style="width: 70%; float: right"></asp:Label>
                            </div>
                            <div style="clear: both;">
                            </div>
                        </div>
                        <div style="clear: both;">
                        </div>
                    </div>
                </div>
            </div>
            <div id="divAmendment" runat="server" class="info-secs padding5" visible="false">
                <p>
                    PRODUCTION CERTIFICATE NUMBER :
                    <asp:Label ID="lblPcAmd" runat="server"></asp:Label>
                </p>
                <div style="width: 100%;">
                    <strong style="float: left; margin: 4px;">Amended On:</strong>
                    <div class="input-group  date datePicker" style="width: 30%; float: left;">
                        <input name="txtTimescheduleforyearofcomm" type="text" id="txtAmendedOn" class="form-control datePicker"
                            runat="server">
                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                    </div>
                    <div style="clear: both;">
                    </div>
                </div>
                <div>
                    <div style="float: left;">
                        1. Is Date of change of category from Micro / Small to Small / Medium or vice versa
                        applicable
                    </div>
                    <div class="input-group">
                        <asp:RadioButtonList ID="rbtnChange" runat="server" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="rbtnChange_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                            <asp:ListItem Value="2">Yes</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="clear: both;">
                    </div>
                </div>
                <div id="dvChange" runat="server" visible="false">
                    <div style="float: left;">
                        1.Date of change of category from Micro / Small to Small / Medium or vice versa
                        :
                    </div>
                    <div class="input-group  date datePicker" style="width: 30%; float: left; margin-left: 10px;">
                        <input name="txtTimescheduleforyearofcomm" type="text" id="txtDatefchange" class="form-control datePicker"
                            runat="server" onclick="return txtDatefchange_onclick()">
                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                    </div>
                    <div style="clear: both;">
                    </div>
                </div>
                <div>
                    <p>
                        2. (Details to be described as applied for)
                    </p>
                    <div class="table-responsive">
                        <asp:GridView ID="grdChanges" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found..."
                            CssClass="table table-bordered" Style="width: 100%;">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Field Description" DataField="vchDescription" />
                                <asp:BoundField HeaderText="Modified Value" DataField="vchModifiedValue" />
                            </Columns>
                        </asp:GridView>
                        <br />
                        <div id="divPlandAmd" runat="server" visible="false">
                            <asp:GridView ID="grdPlantAmd" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found..."
                                CssClass="table table-bordered" DataKeyNames="id" Style="width: 100%;">
                                <Columns>
                                    <asp:TemplateField HeaderText="SLNO." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="PLANT & MACHINERY NAME" DataField="MachineryName" />
                                    <asp:BoundField HeaderText="DATE OF PURCHASE" DataField="DateofPurchase" />
                                    <asp:BoundField HeaderText="AMOUNT" DataField="Cost" />
                                </Columns>
                            </asp:GridView>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        Changes in Plant and machinery details
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPlantAndMachinery" runat="server" TextMode="MultiLine" Rows="5"
                                            Columns="10" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        &nbsp;(Maximum&nbsp;
                                        <asp:Label ID="lblPlantAndMachinery" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                        &nbsp; characters allowed)
                                        <cc1:FilteredTextBoxExtender ID="ftePlantAndmachinery" runat="server" FilterMode="InvalidChars"
                                            InvalidChars=":&quot;~!@#$%^&amp;*()?&gt;&lt;{}+=[];'|\~`" TargetControlID="txtPlantAndMachinery">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div id="divProductAmd" runat="server" visible="false">
                            <asp:GridView ID="grdProductAmd" runat="server" class="table table-bordered table-hover"
                                AutoGenerateColumns="False" GridLines="None" OnRowDataBound="grdProducts_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="SLNO.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="40px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ITEMS OF MANUFACTURE / TYPE OF SERVICE(S) RENDERED">
                                        <ItemTemplate>
                                            <asp:Label ID="Label0" runat="server" Text='<%#Eval("item") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QUANTITY">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("Qty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UNIT">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("unit") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VALUE(IN INR LAKH)">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("cost") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DATE OF PRODUCTION / COMMENCEMENT OF SERVICE">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("dtmProd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        Changes in Product details
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtProductDetails" runat="server" TextMode="MultiLine" Rows="5"
                                            Columns="10" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        &nbsp;(Maximum&nbsp;
                                        <asp:Label ID="lblProductDetails" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                        &nbsp; characters allowed)
                                        <cc1:FilteredTextBoxExtender ID="fteProductDetails" runat="server" FilterMode="InvalidChars"
                                            InvalidChars=":&quot;~!@#$%^&amp;*()?&gt;&lt;{}+=[];'|\~`" TargetControlID="txtProductDetails">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div>
                    <div style="width: 30%; float: left;">
                        <asp:Label ID="Label3" runat="server" Text="Place :" Style="float: left; margin: 4px;"></asp:Label>
                        <asp:TextBox ID="txtPlaceAmd" runat="server" MaxLength="100" Onkeypress="return inputLimiter(event,'NameCharacters')"
                            CssClass="form-control"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="ftePlaceAmd" runat="server" FilterMode="InvalidChars"
                            InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;,{}+=[];',/|\_-~`" TargetControlID="txtPlaceAmd">
                        </cc1:FilteredTextBoxExtender>
                        <div style="clear: both;">
                        </div>
                    </div>
                    <div style="width: 70%; float: right;">
                        <asp:Label ID="Label4" runat="server" Text="Signature with Office Seal" Style="width: 50%;
                            float: right"></asp:Label>
                        <br />
                        <asp:UpdatePanel ID="up2" runat="server">
                            <ContentTemplate>
                                <div class="input-group" style="width: 50%; float: right">
                                    <asp:FileUpload ID="fuSecondSignature" CssClass="form-control" runat="server" />
                                    <br />
                                    <asp:HiddenField ID="hdnSecondSign" runat="server" />
                                    <asp:LinkButton ID="lnkSecondSignAdd" runat="server" CssClass="input-group-addon bg-green"
                                        OnClick="lnkUploadSignature_Click"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkSecondSignDel" runat="server" CssClass="input-group-addon bg-red"
                                        Visible="false" OnClick="lnkOrgDocumentDelete_Click"><i class="fa fa-trash-o" aria-hidden="true" ></i></asp:LinkButton>
                                    <asp:HyperLink ID="hypSecondSign" runat="server" Target="_blank" Visible="false"
                                        CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-cloud-download"></i></asp:HyperLink>
                                </div>
                                <asp:Label ID="lblSecondSign" runat="server"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkSecondSignAdd" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <div style="width: 70%; float: right;">
                            <asp:Label ID="lblSignatoryAmend" runat="server" Font-Bold="true" Text="DIRECTOR OF INDUSTRIES ODISHA,CUTTACK"
                                Style="width: 70%; float: right"></asp:Label>
                        </div>
                        <div style="clear: both;">
                        </div>
                    </div>
                    <div style="clear: both;">
                    </div>
                </div>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                CssClass="btn btn-sm btn-success" OnClientClick="return ValidatePage();" />
            <asp:HiddenField ID="hdnProdDate" runat="server" />
            <asp:HiddenField ID="hdnLastIssue" runat="server" />
            <asp:HiddenField ID="hdnFlag" runat="server" Value="0" />
        </div>
    </div>
    <link href="../../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    </form>
</body>
</html>
