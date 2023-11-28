<%--'*******************************************************************************************************************
' File Name         :ServiceInstruction.aspx
' Description       : Show the Service Instruction of Investors 
' Created by        : Prasun Kali
' Created On        : 18th August 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationConsolidateDetails.aspx.cs"
    Inherits="ApplicationConsolidateDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('.menuservices').addClass('active');
            $("#BtnConfirm").click(function () {
                var chkboxrowcount = $("#<%=GrdServiceDetails.ClientID%> input[id*='ChkBxSelect']:checkbox:checked").size();
                if (chkboxrowcount == 0) {
                    jAlert("<strong>Please select at least one check box to proceed !</strong>");
                    return false;
                }
                return true;
            });
        });
    </script>
</head>
<body>
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc2:header ID="header" runat="server" />
        <div class="container wrapper">
            <div class="registration-div investors-bg">
                <div id="exTab1">
                    <div class="investrs-tab">
                        <uc4:investoemenu ID="ineste" runat="server" />
                    </div>
                    <div class="form-sec">
                        <div class="form-header">
                            Consolidated Payment
                        </div>
                        <div class="form-body">
                            <div class="guidelinesdetails">
                                <div class="form-group ">
                                    <div class="row">
                                        <div class="col-sm-12 guideline__cont">
                                            You have selected below services for bulk payment. Check and Confirm the amount
                                            before proceeding.
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group ">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:GridView ID="GrdServiceDetails" runat="server" CssClass="table table-bordered"
                                                ShowFooter="true" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                                                Width="100%" OnRowDataBound="GrdServiceDetails_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                              <asp:CheckBox ID="ChkBxSelect" runat="server" />
                                                            <%--<asp:CheckBox ID="ChkBxSelect" runat="server" Visible='<%# (Eval("decAmount").ToString() == "0.00" || Eval("decAmount").ToString()=="0")? false : true %>' />--%>
                                                            <asp:HiddenField ID="Hid_HOA_Count" runat="server" Value='<%# Eval("intHoaAccount") %>' />
                                                            <asp:HiddenField ID="Hid_Disable_Status" runat="server" Value='<%# Eval("vchApplicationKey") %>' />
                                                            <asp:HiddenField ID="hdncomplete" runat="server" Value='<%# Eval("intCompletedStatus") %>' />
                                                            <asp:HiddenField ID="hdnserviceid" runat="server" Value='<%# Eval("intServiceId") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Application No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblApplicationNo" runat="server" Text='<%# Eval("vchApplicationKey") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Service Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblServiceName" runat="server" Text='<%# Eval("vchServiceName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Proposal Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblProposalNumber" runat="server" Text='<%# Eval("vchProposalNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="12%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblStatus" runat="server" Text='<%# Eval("intCompletedStatus").ToString() == "1"  ? "Applied" : "Draft"%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblPaidStatus" runat="server" Text='<%# (Eval("intCompletedStatus").ToString() == "1" && (Eval("decAmount").ToString() == "0.00" || Eval("decAmount").ToString()=="0")) ? "Not Required" :"Not Paid" %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="11%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblAmount" runat="server" Text='<%# Eval("decAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 text-center margin-top10">
                            <asp:Button ID="BtnConfirm" runat="server" Text="Confirm and Proceed" CssClass="btn btn-success"
                                OnClick="BtnConfirm_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
