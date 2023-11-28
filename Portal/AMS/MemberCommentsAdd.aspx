<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberCommentsAdd.aspx.cs"
    Inherits="SingleWindow_MemberCommentsAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../PortalCSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/custom.js" type="text/javascript"></script>
    <link href="../css/agenda.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function TextCounter(ctlTxtName, lblCouter, numTextSize) {
            var txtName = document.getElementById(ctlTxtName).value;
            var txtNameLength = txtName.length;
            if (parseInt(txtNameLength) > parseInt(numTextSize)) {
                var txtMaxTextSize = txtName.substr(0, numTextSize)
                document.getElementById(ctlTxtName).innerHTML = txtMaxTextSize;
                alert("Entered Text Exceeds '" + numTextSize + "' Characters.");
                document.getElementById(lblCouter).innerHTML = 0;
                return false;
            }
            else {
                document.getElementById(lblCouter).innerHTML = parseInt(numTextSize) - parseInt(txtNameLength);
                return true;
            }
        }

        $(document).ready(
            function () {
                $('#btnSubmit').click(function () {


                    var sval = $('input[name=hdnStatus]').val();
                    if (parseInt(sval) == 4) {
                        if (!BlankTextBox('txtClarification', 'Clarification')) { return false; }
                        if (!IsSpecialCharacter1stPalce('txtClarification')) { return false; }
                    }
                    else {
                        if (!BlankTextBox('txtComments', 'Your Comments')) { return false; }
                        if (!IsSpecialCharacter1stPalce('txtComments')) { return false; }
                    }
                })

            });
    </script>
    <style>
        .inner-table .table
        {
            margin-bottom: 5px;
        }
        .ppul
        {
            margin-left: 10px;
            margin-top: 0px;
            list-style-type: square;
        }
        .listX ul li
        {
            list-style-type: disc;
            margin-left: 30px;
        }
        .listX ol
        {
            list-style-type: decimal;
        }
        .listX ol li
        {
            list-style-type: decimal;
            margin-left: 30px;
        }
        .listX li
        {
            list-style-type: upper-roman;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <h4>
            <asp:Label ID="lblProjTitle" runat="server" Text=""></asp:Label></h4>
        <table class="table table-bordered">
            <tr>
                <th>
                    <label>
                        SECTOR</label>
                </th>
                <th colspan="2">
                    <asp:Label ID="lblSector" runat="server" Text=""></asp:Label>
                </th>
            </tr>
            <tr>
                <td>
                    <label>
                        1. Name of the Project</label>
                </td>
                <td colspan="2">
                    <asp:Label ID="lblProjNm" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        2. Date of submission</label>
                </td>
                <td colspan="2">
                    <asp:Label ID="lblApplDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        3. Location of the Project</label>
                </td>
                <td colspan="2">
                    <div id="placeholder" runat="server">
                    </div>
                    <%--<asp:Label ID="lblLocation" runat="server" Text=""></asp:Label>--%>
                    <%-- <asp:GridView ID="GridLoc" runat="server" Width="100%" AutoGenerateColumns="False"
                            class="table table-bordered">
                            <Columns>
                                <asp:BoundField DataField="District" HeaderText="District">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Location" HeaderText="Location">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>--%>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        4. Profile of the project</label>
                </td>
                <td colspan="2">
                    <table class="inner-table" width="100%">
                        <tr>
                            <td border="0" width="50%">
                                <table class="inner-table" width="100%">
                                    <tr border="0">
                                        <td border="0">
                                            <asp:Repeater ID="rptCapacity" runat="server">
                                                <HeaderTemplate>
                                                    <table width="100%" class="table table-bordered" border="0">
                                                        <tr>
                                                            <th>
                                                                Product
                                                            </th>
                                                            <th>
                                                                Capacity
                                                            </th>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%#Eval("VCHPRODUCT")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("vchCapacity")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px" border="0">
                            </td>
                            <td border="0">
                                <table width="100%" class="table table-bordered">
                                    <tr>
                                        <th>
                                            Type
                                        </th>
                                        <th>
                                            <asp:Label ID="lblNew" runat="server" Text=""></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            Category
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        5. Proposal in brief</label>
                </td>
                <td colspan="2">
                    <asp:Repeater ID="RptrProposal" runat="server">
                        <HeaderTemplate>
                            <ul class="listX">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <%#Container.ItemIndex + 1%>.
                                <%-- <%#Eval("ProposalDtl") %>--%>
                                <%# Server.HtmlDecode( (string) Eval( "ProposalDtl" ) ) %>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        6. Promoters</label>
                </td>
                <td width="33%">
                    Board of Directors:-
                    <br />
                    <asp:Repeater ID="RptrPromoterDirectors" runat="server">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <%#Container.ItemIndex + 1%>.
                                <%#Eval("VCHPROMOTOR") %>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
                <td>
                    Existing Business Interest of the company:-
                    <asp:Repeater ID="RptrPromoterBusiness" runat="server">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <%#Container.ItemIndex + 1%>.
                                <%#Eval("VCHPROMOTOR") %>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        7. Financial Performance of company(Rs in crores)</label>
                </td>
                <td colspan="2">
                    <asp:GridView ID="GrdFinanace" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="FinanceId,ComapnyName,KeyId" OnDataBound="OnDataBound" CssClass="table table-bordered"
                        OnRowDataBound="GrdFinanace_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="ComapnyName" HeaderText="Company Name">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Particulars" HeaderText="Particulars">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FinYear1" HeaderText="Finance Year">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FinYear2" HeaderText="Finance Year">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FinYear3" HeaderText="Finance Year">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remark" HeaderText="Remark">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Financial Document" Visible="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnFinDoc" runat="server" Value='<%# Eval("FinDoc") %>' />
                                    <asp:HyperLink ID="hlDoc" runat="server" Target="_blank" ImageUrl="~/img/pdf_icon_32.png"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        8. Project Cost</label>
                </td>
                <td colspan="2">
                    <asp:Label ID="lblProjCost" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td rowspan="2">
                    a) Project Cost details
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="GrdProjectCostDtls" runat="server" Width="100%" AutoGenerateColumns="False"
                        ShowFooter="true" OnRowDataBound="GrdProjectCostDtls_RowDataBound" CssClass="table table-bordered">
                        <Columns>
                            <asp:TemplateField HeaderText="Details Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("VCH_COST_DTLS_DESC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cost (Rs in Crores)" ItemStyle-HorizontalAlign="Right"
                                FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCost" runat="server" Text='<%# Eval("DEC_COST") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblGrandTotal" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Percentage of project cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPerCost" runat="server" Text='<%# Eval("VCH_COST_PERCENT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                        </Columns>
                        <FooterStyle Font-Bold="True" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    b) Financing details(Rs. in Crores)
                </td>
                <td width="200px" colspan="2">
                    <%#Eval("VCHPRODUCT")%>
                    <asp:Repeater ID="rptFinDtls" runat="server">
                        <HeaderTemplate>
                            <table width="100%" class="table table-bordered" border="0">
                                <tr>
                                    <th>
                                        Description
                                    </th>
                                    <th>
                                        Amount
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Eval("VCH_FIN_DTLS_DESC")%>
                                </td>
                                <td style="text-align: right;">
                                    <%#Eval("DEC_FIN_COST")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <%#Eval("vchCapacity")%>
                </td>
            </tr>
            <tr>
                <td>
                    c) Financing description
                </td>
                <td colspan="2">
                    <asp:Label ID="lblFinDesc" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td rowspan="4">
                    <label>
                        9. Infrastructure requirement</label>
                </td>
                <td>
                    i. Land
                </td>
                <td>
                    <asp:Label ID="lblLand" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    ii. Water
                </td>
                <td>
                    <asp:Label ID="lblWater" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    iii. Power
                </td>
                <td>
                    <asp:Label ID="lblPower" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    Source :<asp:Label ID="lblCPP" runat="server"></asp:Label>
                    <%-- <%#Container.ItemIndex + 1%>       --%>
                    <%--  <%#Eval("ProposalDtl") %>--%>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        10. Raw Materials Source</label>
                </td>
                <td colspan="2">
                    <%# Server.HtmlDecode( (string) Eval( "ProposalDtl" ) ) %>
                    <asp:GridView ID="GrdSource" runat="server" class="table table-bordered" Width="100%"
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Materials">
                                <ItemTemplate>
                                    <asp:Label ID="lblRawmaterial" runat="server" Text='<%# Eval("Materials") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Source">
                                <ItemTemplate>
                                    <asp:Label ID="lblSource" runat="server" Text='<%# Eval("Source") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        11. Implementation Period</label>
                </td>
                <td colspan="2">
                    <asp:Label ID="lblImple" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td rowspan="3">
                    <label>
                        12. Employment Potential</label>
                </td>
                <td>
                    Direct
                </td>
                <td>
                    <asp:Label ID="lblDirect" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Contractual
                </td>
                <td>
                    <asp:Label ID="lblContra" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Total</label>
                </td>
                <td>
                    <asp:Label ID="lblEmTotal" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="padding-top: 20px;">
                <td>
                    <label>
                        Your Comments <span class="text-danger">*</span></label>
                </td>
                <td colspan="2" align="left">
                    <asp:TextBox ID="txtComments" TextMode="MultiLine" runat="server" MaxLength="500"
                        Width="570px" Height="100px" Rows="2" TabIndex="1" spellcheck="true" onkeypress="return TextCounter('txtComments','lblDesc',500)" />
                    <cc1:FilteredTextBoxExtender ID="FEComments" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,custom"
                        ValidChars=" /.\()," TargetControlID="txtComments" InvalidChars="'%*|">
                    </cc1:FilteredTextBoxExtender>
                    <br />
                    <small class="text-danger">Maximum <span class="mandatory">
                        <asp:Label ID="lblDesc" runat="server" Text="500"></asp:Label>
                    </span>&nbsp;characters</small>
                    <!--<span class="mandatory">&nbsp;</span>-->
                </td>
            </tr>
            <tr style='<%=IsGMVisible ? "": "display: none" %>'>
                <td>
                    <label>
                        GM(SLNA) Comments</label>
                </td>
                <td colspan="2" align="left">
                    <asp:Label ID="lblComments" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style='<%=isSLFCVisible ? "": "display: none" %>'>
                <td>
                    <label>
                        Clarification</label>
                </td>
                <td colspan="2" align="left">
                    <asp:TextBox ID="txtClarification" TextMode="MultiLine" runat="server" MaxLength="500"
                        Width="570px" Height="100px" Rows="2" TabIndex="2" spellcheck="true" onkeypress="return TextCounter('txtClarification','lblCount',500)" />
                    <span class="mandatory">*</span>
                    <cc1:FilteredTextBoxExtender ID="FEClarification" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,custom"
                        ValidChars=" /.\()," TargetControlID="txtClarification" InvalidChars="'%*|">
                    </cc1:FilteredTextBoxExtender>
                    <asp:HiddenField ID="hdnStatus" Value="0" runat="server" />
                    <asp:HiddenField ID="hdnClariID" Value="0" runat="server" />
                    <br />
                    Maximum <span class="mandatory">
                        <asp:Label ID="lblCount" runat="server" Text="500"></asp:Label>
                    </span>&nbsp;characters <span class="mandatory">&nbsp;</span>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="2" align="left">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success"
                        OnClick="btnSubmit_Click" TabIndex="2" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
