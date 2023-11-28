<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublicChildServiceDetails.aspx.cs"
    Inherits="PublicChildServiceDetails" %>

<!DOCTYPE html>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%--<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <title></title>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src='//code.jquery.com/jquery-1.8.3.js'></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/css/bootstrap-datepicker3.min.css">
    <script type='text/javascript' src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/js/bootstrap-datepicker.min.js"></script>
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
                    <h2 class=" margin-bottom15">
                        MIS Report On Services
                    </h2>
                    <div class="table-responsive">
                        <asp:Label ID="lblSearchDetails" runat="server"></asp:Label>
                        <br />
                        <asp:GridView ID="grdDepartment" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
                            DataKeyNames="intKey" CssClass="table table-bordered table-hover" OnRowDataBound="grdDepartment_RowDataBound"
                            ShowFooter="true" OnRowCommand="grdDegrdDepartment_RowCommand" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="4%" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Service" DataField="strDeptName" FooterStyle-Font-Bold="true" />
                                <asp:TemplateField HeaderText="Opening Balance" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                    FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemTemplate>
                                        <%--  <asp:LinkButton ID="lnkCarryFwdPending" runat="server" Text='<%#Eval("intCarryFwdPending")%>'
                                            Visible="false" CommandName="D" CommandArgument="8"></asp:LinkButton>--%>
                                        <asp:Label ID="lblCarryFwdPending" runat="server" Text='<%#Eval("intCarryFwdPending")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Application Received" FooterStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%--   <asp:LinkButton ID="lnkTotalApplication" runat="server" Text='<%#Eval("intTotalApplication")%>'
                                            Visible="false" CommandName="D" CommandArgument="0"></asp:LinkButton>--%>
                                        <asp:Label ID="lblTotalApplication" runat="server" Text='<%#Eval("intTotalApplication")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%--   <asp:LinkButton ID="lnkApproved" runat="server" Text='<%#Eval("intTotalApproved")%>'
                                            Visible="false" CommandName="D" CommandArgument="2"></asp:LinkButton>--%>
                                        <asp:Label ID="lblApproved" runat="server" Text='<%#Eval("intTotalApproved")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="6%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rejected" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%--   <asp:LinkButton ID="lnkRejected" runat="server" Text='<%#Eval("intTotalRejected")%>'
                                            Visible="false" CommandName="D" CommandArgument="3"></asp:LinkButton>--%>
                                        <asp:Label ID="lblRejected" runat="server" Text='<%#Eval("intTotalRejected")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="6%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. of Application with Query" FooterStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemTemplate>
                                        <%--   <asp:LinkButton ID="lnkQuery" runat="server" Text='<%#Eval("intTotalQueryRaised")%>'
                                            Visible="false" CommandName="Q" CommandArgument="0"></asp:LinkButton>--%>
                                        <asp:Label ID="lblQuery" runat="server" Text='<%#Eval("intTotalQueryRaised")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total application pending in current period" FooterStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemTemplate>
                                        <%--  <asp:LinkButton ID="lnkPending" runat="server" Text='<%#Eval("intTotalPending")%>'
                                            Visible="false" CommandName="D" CommandArgument="1"></asp:LinkButton>--%>
                                        <asp:Label ID="lblPending" runat="server" Text='<%#Eval("intTotalPending")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Applications Pending" FooterStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%--  <asp:LinkButton ID="lnkAllPending" runat="server" Text='<%#Eval("intAllTotalPending")%>'
                                            Visible="false" CommandName="D" CommandArgument="9"></asp:LinkButton>--%>
                                        <asp:Label ID="lblAllPending" runat="server" Text='<%#Eval("intAllTotalPending")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="8%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Application Exceeds ORTPSA Timeline" FooterStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%--  <asp:LinkButton ID="lnkORTPS" runat="server" Text='<%#Eval("intTotalORTPSAtimelinePassed")%>'
                                            Visible="false" CommandName="D" CommandArgument="4"></asp:LinkButton>--%>
                                        <asp:Label ID="lblORTPS" runat="server" Text='<%#Eval("intTotalORTPSAtimelinePassed")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mean" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("intAvgDaysApproval")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deferred" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                    FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemTemplate>
                                        <%--  <asp:LinkButton ID="lnkDeferred" runat="server" Text='<%#Eval("intTotalDeferred")%>'
                                            Visible="false" CommandName="D" CommandArgument="7"></asp:LinkButton>--%>
                                        <asp:Label ID="lblDeferred" runat="server" Text='<%#Eval("intTotalDeferred")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Forwarded" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                    FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemTemplate>
                                        <%--  <asp:LinkButton ID="lnkForwarded" runat="server" Text='<%#Eval("intTotalForwarded")%>'
                                            Visible="false" CommandName="D" CommandArgument="8"></asp:LinkButton>--%>
                                        <asp:Label ID="lblForwarded" runat="server" Text='<%#Eval("intTotalForwarded")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Median" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("decMedian")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ORTPSA Timeline" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORTPSTimeline" runat="server" Text='<%#Eval("intORTPSAtimeline")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="6%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Minimum Days Taken for Approval" FooterStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("intMinApprovalDays")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Maximum Days Taken for Approval" FooterStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("intMaxApprovalDays")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="form-group row NOPRINT">
                        <div class="col-sm-12">
                            <asp:Button ID="BtnBack" CssClass="btn btn-danger" runat="server" Text="Back" OnClick="BtnBack_Click">
                            </asp:Button>
                        </div>
                    </div>
                </div>
            </div>
            <%--  <script src="../js/custom.js" type="text/javascript"></script>
            <script type="text/javascript">
                function ViewModal(ModalPath) {
                    $('#DetailsModal').modal();
                    $('#DetailsModal .modal-body iframe').attr('src', ModalPath);
                }
            </script>--%>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
