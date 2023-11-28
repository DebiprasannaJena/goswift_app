<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCPrintLarge.aspx.cs" Inherits="Portal_Incentive_PCPrintLarge" %>

<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="Head1">
    <title>SWP</title>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/PcPrint.css" rel="Stylesheet" type="text/css" />
    <script src="../../js/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad() {
            $("#printbtn").click(function () {
                $(".print-area").css({ "border": "0px", "box-shadow": "none", "padding": "10px 0px" });
                window.print();
            });

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="print-area" id="divPdf" runat="server" style="font-size: 9pt;">
            <table style="width: 100%;">
                <tr>
                    <td colspan="2" class="header" style="text-align: center">
                        <h1>
                            Production Certificate</h1>
                        <h4>
                            (To be issued by RIC / DIC / Director of Industries – online)</h4>
                    </td>
                </tr>
            </table>
            <table style="width: 100%;" id="tblNewPc" runat="server">
                <tr>
                    <td colspan="2" class="info-secs padding5">
                        <p>
                            1.M/s.<asp:Label ID="lblUnit" runat="server" Font-Underline="true"></asp:Label>,
                            <asp:Label ID="lblOwnerType" runat="server"></asp:Label>
                            -
                            <asp:Label ID="lblOwner" runat="server" Font-Underline="true"></asp:Label>&nbsp;
                            has filled the Self Declaration Memorandum who has established a
                            <asp:Label ID="lblCompanyType" runat="server"></asp:Label>&nbsp; Enterprise at
                            <asp:Label ID="lblEnterpriseAdd" runat="server" Font-Underline="true"></asp:Label>&nbsp;
                            and is
                            <asp:Label ID="lblComType" runat="server"></asp:Label>&nbsp; as given below :</p>
                        <br />
                        <p>
                            2. Date of First Fixed Capital Investment in the Project (for new enterprises)
                            <asp:Label ID="lblFirstDate" Font-Bold="true" Font-Underline="true" runat="server"></asp:Label>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="info-secs padding5">
                        <p>
                            3. Details of Item(s)
                            <asp:Label ID="lblComType2" runat="server"></asp:Label>(for new Enterprises)
                        </p>
                        <br />
                        <asp:GridView ID="grdProducts" runat="server" class="table table-bordered table-hover"
                            AutoGenerateColumns="False" OnRowDataBound="grdProducts_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="SlNo.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ITEMS  MANUFACTURED / TYPE OF SERVICE(S) RENDERED">
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
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="info-secs padding5">
                        <p>
                            4. DETAILS OF PLANT AND MACHINERY AS PER DATE-WISE INVESTMENT (for new enterprises):
                        </p>
                        <br />
                        <asp:GridView ID="gvPlant" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found..."
                            CssClass="table table-bordered" DataKeyNames="id" Style="width: 100%;">
                            <Columns>
                                <asp:TemplateField HeaderText="SlNo." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="PLANT & MACHINERY NAME" DataField="MachineryName" />
                                <asp:BoundField HeaderText="DATE OF PURCHASE" DataField="DateofPurchase" />
                                <asp:BoundField HeaderText="AMOUNT(IN INR LAKH)" DataField="Cost" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="info-secs padding5">
                        <strong>PRODUCTION CERTIFICATE NUMBER :</strong>
                        <asp:Label ID="lblAppNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="info-secs padding5">
                        <strong>Date of Issue:</strong>
                        <asp:Label ID="lblPcIssuedate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="info-secs padding5">
                        <strong>NOTE: THE ISSUE OF THIS PRODUCTION CERTIFICATE DOES NOT BESTOW ANY LEGAL RIGHT
                            WITH RESPECT TO THE REQUISITE CLEARANCE / LICENCE / PERMIT. THE ENTERPRISE IS REQUIRED
                            TO SEEK REQUISITE CLEARANCE / LICENCE / PERMIT REQUIRED UNDER STATUTORY OBLIGATIONS
                            STIPULATED UNDER THE LAWS OF CENTRAL GOVERNMENT / STATE GOVERNMENT / UT ADMINISTRATIONS
                            / COURT ORDERS'.</strong><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 70%;" valign="top">
                        <strong>Place :</strong>
                        <asp:Label ID="lblPlaceValue" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <asp:Label ID="lblSignature" runat="server" Text="Signature with Office Seal"></asp:Label>
                        <br />
                        <asp:Image ID="imgSignature" runat="server" />
                        <br />
                        <asp:Label ID="lblSignatory" runat="server" Font-Bold="true" Text="DIRECTOR OF INDUSTRIES ODISHA,CUTTACK"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 100%;" id="tblAmendement" runat="server">
                <tr>
                    <td colspan="2" class="info-secs padding5">
                        <strong>PRODUCTION CERTIFICATE NUMBER:</strong>
                        <asp:Label ID="lblPcAmd" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="info-secs padding5">
                        <strong>Amended On:</strong>
                        <asp:Label ID="lblAmdDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trChangeCategory" runat="server" visible="false">
                    <td colspan="2" class="info-secs padding5">
                        <strong>1.Date of change of category from Micro / Small to Small / Medium or vice versa
                        </strong>
                        <asp:Label ID="lblCatChangeDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="info-secs padding5">
                        <p>
                            2. (Details to be described as applied for)
                        </p>
                        <br />
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
                    </td>
                </tr>
                <tr id="trProductChanges" runat="server">
                    <td colspan="2" class="info-secs padding5">
                        <p>
                            <strong>Changes in Product Details :</strong>
                            <asp:Label ID="lblProductChanges" runat="server"></asp:Label>
                        </p>
                        <br />
                        <asp:GridView ID="grdProductAmd" runat="server" class="table table-bordered table-hover"
                            AutoGenerateColumns="False" OnRowDataBound="grdProducts_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="SlNo.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ITEMS  MANUFACTURED / TYPE OF SERVICE(S) RENDERED">
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
                    </td>
                </tr>
                <tr id="trPlantChanges" runat="server">
                    <td colspan="2" class="info-secs padding5">
                        <p>
                            <strong>Changes in Plant and Machinery Details :</strong>
                            <asp:Label ID="lblPlantChanges" runat="server"></asp:Label></p>
                        <br />
                        <asp:GridView ID="grdPlantAmd" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found..."
                            CssClass="table table-bordered" DataKeyNames="id" Style="width: 100%;">
                            <Columns>
                                <asp:TemplateField HeaderText="SlNo." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="PLANT & MACHINERY NAME" DataField="MachineryName" />
                                <asp:BoundField HeaderText="DATE OF PURCHASE" DataField="DateofPurchase" />
                                <asp:BoundField HeaderText="AMOUNT(IN INR LAKH)" DataField="Cost" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="width: 70%;" valign="top">
                        <strong>Place :</strong>
                        <asp:Label ID="lblPlaceAmd" runat="server" Style="width: 70%; float: left;"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <asp:Label ID="Label4" runat="server" Text="Signature with Office Seal" Style="width: 50%;
                            float: right"></asp:Label>
                        <br />
                        <asp:Image ID="imgSignatureAmd" runat="server" />
                        <br />
                        <asp:Label ID="lblSignatoryAmend" runat="server" Font-Bold="true" Text="DIRECTOR OF INDUSTRIES ODISHA,CUTTACK"
                            Style="width: 70%; float: right"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
