<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewstatusdetails.aspx.cs"
    Inherits="viewstatusdetails" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script>

        $(document).ready(function () {

            $('.menuservices').addClass('active');

        });

    </script>
    <style>
        .guidelines {
            display: table;
            width: 100%;
            min-height: 200px;
            text-align: center;
            background: #eee;
            margin-bottom: 10px;
        }

            .guidelines p {
                display: table-cell;
                vertical-align: middle;
                font-size: 18px;
                letter-spacing: 1px;
            }

        .guidelinesdetails {
        }

            .guidelinesdetails h4 {
                margin-top: 0px;
                font-weight: 600;
            }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <uc2:header ID="header" runat="server" />
         <div class="container wrapper">
        <div class="registration-div investors-bg">
           
                <div id="exTab1">
                    <div class="investrs-tab">
                        <uc4:investoemenu ID="ineste" runat="server" />
                    </div>
                    <div class="form-sec">
                        <div class="form-header">
                            <h2>Status Details
                            </h2>
                        </div>
                        <div class="form-body ">
                            <div class="guidelinesdetails">
                                <div class="form-group ">
                                    <div class="table-responsive ">
                                        <asp:GridView ID="gvProposal" runat="server" CssClass="table table-bordered bg-white"
                                            AllowPaging="true" PageSize="10" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                                            CellPadding="4" GridLines="None">
                                            <AlternatingRowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Proposal No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("vchProposalNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CAF No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("vchcafno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Industry Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("vchIndustryCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Processing Fee Realization Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("vchProcessingFeeRealizationStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Realization Reference No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("vchPaymentRealizationReferenceNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice">
                                                    <ItemTemplate>
                                                        <a href='<%# Eval("vchDemandNoteLink") %>' target="_blank">Invoice</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Money Receipt">
                                                    <ItemTemplate>
                                                        <a href='<%# Eval("vchDemandReceipt") %>' target="_blank">Money Receipt</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Allotment Order Link">
                                                    <ItemTemplate>
                                                        <a href='<%# Eval("vchAllotmentOrderLink") %>' target="_blank">
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("vchAllotmentOrderLink") %>'></asp:Label></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created On">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("dtmCreatedOn") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
