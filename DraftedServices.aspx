<%--'*******************************************************************************************************************
' File Name         : DraftedServices.aspx
' Description       : Show the list of all drafted services.
' Created by        : Radhika Rani Patri
' Created On        : 29 Mar 2018
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DraftedServices.aspx.cs"
    Inherits="DraftedServices" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.menuservices').addClass('active');
            $("#BtnApplyMultipe").click(function () {
                var chkboxrowcount = $("#<%=gvDraftService.ClientID%> input[id*='ChkBxSelect']:checkbox:checked").size();
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
                <div class="">
                    <div id="exTab1">
                        <div class="investrs-tab">
                            <uc4:investoemenu ID="ineste" runat="server" />
                        </div>
                        <div class="tab-content clearfix">
                            <div class="tab-pane active" id="1a">
                                <div class="form-sec">
                                    <div class="form-header">
                                        <a href="ApplicationDetails.aspx" title="Application Details" class="pull-right proposalbtn">Application Details</a> 
                                        <a href="DepartmentClearance.aspx" title="Drafted Proposals" class="pull-right proposalbtn">Apply Service</a>
                                        <a href="DraftedServices.aspx" title="Drafted Services" class="pull-right proposalbtn  active">Draft Service</a>
                                        <h2>Draft Service</h2>
                                    </div>
                                    <div class="form-body minheight350">
                                        <div class="form-group">
                                            <h4 class="margin-top10 margin-bottom10 text-red">Internal Services</h4>
                                            <div class="pageResult text-right">
                                                <i class="fa fa-list" aria-hidden="true" id="icon" runat="server"></i>
                                                <asp:LinkButton ID="lbtnAll" Visible="true" runat="server" Text="All" OnClick="lbtnAll_Click"></asp:LinkButton>
                                                &nbsp;&nbsp;
                                                <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                            </div>
                                            <div class="table-responsive">

                                                <asp:GridView ID="gvDraftService" runat="server" CssClass="table table-bordered"
                                                    AllowPaging="true" PageSize="20" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                                                    CellPadding="4" GridLines="None" DataKeyNames="str_checkStatus,intServiceId,str_UlbCode,strProposalId,strCertificateFilename,Str_NocFileName,intStatus,Str_ExtrnalServiceUrl,str_ApplicationNo,Dec_Amount,intExternalType,vchTranscationNo"
                                                    Width="100%" OnPageIndexChanging="gvDraftService_PageIndexChanging">
                                                    <AlternatingRowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Group Code">
                                                            <ItemTemplate>
                                                              <%--  <asp:CheckBox ID="ChkBxSelect" runat="server"/>--%>
                                                                 <asp:Label ID="lblgrouipid" runat="server" Text='<%# Eval("vchTranscationNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="9%"  />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sl#.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsl" runat="server" Text='<%#(gvDraftService.PageIndex * gvDraftService.PageSize) + (gvDraftService.Rows.Count + 1)%>'></asp:Label>
                                                            </ItemTemplate>
                                                              <ItemStyle Width="3%" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="hypLink" runat="server" Text='<%# Eval("str_ApplicationNo") %>'></asp:Label>
                                                                <asp:HiddenField ID="Hid_Service_Type" runat="server" Value='<%# Eval("str_checkStatus") %>' />
                                                                <asp:HiddenField ID="Hid_Dept_Name" runat="server" Value='<%# Eval("str_Department") %>' />
                                                            </ItemTemplate>
                                                              <ItemStyle Width="13%"   />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Service Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblServiceName" runat="server" Text='<%# Eval("str_ServicesName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Investor's Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("str_ApplicantName") %>'></asp:Label>
                                                            </ItemTemplate>  
                                                            <ItemStyle Width="15%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Last Updated On">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("Requestdate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="11%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Draft" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkEdit" runat="server" CommandName="Edit" CssClass="btn btn-primary" OnClick="LinkEdit_Click">Continue</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="7%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pagination-grid no-print" />
                                                </asp:GridView>
                                                <div style="text-align: left;display:none">
                                                    <asp:Button ID="BtnApplyMultipe" runat="server" Text="Continue" Visible="false" CssClass="btn btn-danger" OnClick="BtnApplyMultipe_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <h4 class="margin-top10 margin-bottom10 text-red">External Services</h4>
                                            <div class="pageResult text-right">
                                                <i class="fa fa-list" aria-hidden="true" id="Exicon" runat="server"></i>
                                                <asp:LinkButton ID="ExlbtnAll" Visible="true" runat="server" Text="All" OnClick="ExlbtnAll_Click"></asp:LinkButton>
                                                &nbsp;&nbsp;
                                                <asp:Label ID="ExlblPaging" runat="server"></asp:Label>
                                            </div>

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvExDraftService" runat="server" CssClass="table table-bordered"
                                                    AllowPaging="true" PageSize="20" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                                                    CellPadding="4" GridLines="None" DataKeyNames="str_checkStatus,intServiceId,str_UlbCode,strProposalId,strCertificateFilename,Str_NocFileName,intStatus,Str_ExtrnalServiceUrl,str_ApplicationNo,Dec_Amount,intExternalType"
                                                    Width="100%" OnPageIndexChanging="gvExDraftService_PageIndexChanging">
                                                    <AlternatingRowStyle />
                                                    <Columns>                                                        
                                                        <asp:TemplateField HeaderText="Sl#.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsl" runat="server" Text='<%#(gvExDraftService.PageIndex * gvExDraftService.PageSize) + (gvExDraftService.Rows.Count + 1)%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApp" runat="server" Text='<%# Eval("str_ApplicationNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Service Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblServiceName" runat="server" Text='<%# Eval("str_ServicesName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Investor's Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvestor" runat="server" Text='<%# Eval("str_ApplicantName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="19%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Last Updated On">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblupdate" runat="server" Text='<%# Eval("Requestdate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="11%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Draft" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="ExLinkEdit" runat="server" CommandName="Edit" CssClass="btn btn-primary" OnClick="ExLinkEdit_Click">Continue</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="7%" />
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
            </div>
        </div>
        <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
