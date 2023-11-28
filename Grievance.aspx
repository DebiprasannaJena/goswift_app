<%--'*******************************************************************************************************************
' File Name         : Grievance.aspx
' Description       : Show the list of all approved and pending Grievance.
' Created by        : Manoj Kumar Behera
' Created On        : 05th Aug 2021
' Modification History:
'  <CR no.>  <Date> <Modified by><Modification Summary><Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Grievance.aspx.cs" Inherits="Grievance" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.menugrievance').addClass('active');
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
                <div class="">
                    <div id="exTab1">
                        <div class="investrs-tab">
                            <uc4:investoemenu ID="ineste" runat="server" />
                        </div>
                        <div class="tab-content clearfix">
                            <div class="tab-pane active" id="1a">
                                <div class="form-sec">
                                    <div class="form-header">
                                        <a href="Grievance/AddGrievance.aspx" title="Create Grievance" class="pull-right proposalbtn active">Add Grievance</a> <a href="Grievance.aspx" title="View Grievance" class="pull-right proposalbtn">View Grievance</a>
                                        <h2>Grievance Details</h2>
                                    </div>
                                    <div class="form-body minheight350">
                                        <div class="form-group">
                                            <asp:GridView ID="GrdGrivDetails" runat="server" CssClass="table table-bordered bg-white"
                                                AllowPaging="true" PageSize="10" AutoGenerateColumns="False" EmptyDataText="No grievance found"
                                                DataKeyNames="vchGrivId" CellPadding="4" GridLines="None" OnPageIndexChanging="GrdGrivDetails_PageIndexChanging"
                                                OnRowDataBound="GrdGrivDetails_RowDataBound" Width="100%">
                                                <AlternatingRowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SlNo">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="4%"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Grievance Id">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hypLink" runat="server" Text='<%# Eval("vchGrivId") %>' ToolTip="Click here to view Grievance details."></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="9%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Grievance Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblGrivType" runat="server" Text='<%# Eval("vchGrivType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Grievance Sub Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblGrivSubType" runat="server" Text='<%# Eval("vchGrivSubType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Name of the Company/Enterprise">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCompany" runat="server" Text='<%# Eval("VCH_INV_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    <%-- <asp:TemplateField HeaderText="Applicant Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApplicant" runat="server" Text='<%# Eval("vchApplicantName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    <%--<asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesig" runat="server" Text='<%# Eval("vchDesignation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    <%-- <asp:TemplateField HeaderText="Industry Type ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIndustryType" runat="server" Text='<%# Eval("intIndustryCategory") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Grievance Title">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltitle" runat="server" Text='<%# Eval("vchGrivTitle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Apply Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblApplyDate" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="Hid_Status" runat="server" Value='<%# Eval("intStatus") %>' />
                                                            <asp:Label ID="LblStatus" runat="server" Text='<%# Eval("vchStatusName") %>' Font-Bold="true"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="9%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblActionDate" runat="server" Text='<%# Eval("dtmActionDate" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="8%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle ForeColor="Red" Font-Italic="true" />
                                                <PagerStyle CssClass="pagination-grid no-print" />
                                            </asp:GridView>
                                        </div>
                                        <div class="form-group">
                                            <div style="text-align: center;">
                                                <asp:Button ID="Btn_Add_Griv" runat="server" Text="Add New Grievance" OnClick="Btn_Add_Griv_Click"
                                                    CssClass="btn btn-danger" ToolTip="Click here to add a new grievance" />
                                            </div>
                                        </div>
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
