<%--'*******************************************************************************************************************
' File Name         : IncentiveApplicationStatus.aspx
' Description       : Show the status of the Incentive
' Created by        : AMit Sahoo
' Created On        : 30th June 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncentiveApplicationStatus.aspx.cs"
    Inherits="IncentiveApplicationStatus" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
     <script>
         $(document).ready(function () {

             $('.menuincentive').addClass('active');
         });
    </script>
</head>
<body>
    <form id="form2" runat="server">
    <uc2:header ID="header" runat="server" />
 
    <div class="registration-div investors-bg">
        <div class="container">
            <div id="exTab1">
                <div class="investrs-tab">
              <uc4:investoemenu ID="ineste" runat="server" />
                </div>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="5a">
                        <div class="form-sec">
                        <div class="form-header">
                            <h2>
                                Incentive</h2></div>
                    <div class="form-body">
                            <div class="form-group search-sec">
                                <div class="row">
                                <label class="col-md-2 col-sm-3" for="ApplicationNo">
                                            Application No</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" Text="Search Application No."></asp:TextBox>
                                    </div>
                                   <label for="Status"  class="col-md-1 col-sm-2">
                                            Status</label>
                                    <div class="col-sm-3">
                                        
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Approved" Value="0"></asp:ListItem>
                                             <asp:ListItem Text="Reject" Value="0"></asp:ListItem>
                                              <asp:ListItem Text="Inprogess" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1 col-sm-2">
                                   <span class="apply">
                                        <asp:Label ID="lblApply" runat="server" Text="Apply" Visible="false"></asp:Label>
                                        <asp:Button ID="btnApply" runat="server" Text="Search" CssClass="btn btn-success"
                                            />
                                            </span>
                                    </div>
                                </div>
                            </div>
                                 <div class="form-group">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvAppStatus" runat="server" CssClass="table table-bordered" AllowPaging="true" PageSize="2" onpageindexchanging="gvAppStatus_PageIndexChanging"
                                        AutoGenerateColumns="False" EmptyDataText="No Record(s) Found" 
                                        CellPadding="4" 
                                      >
                                     <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                     <Columns>
                                         <asp:TemplateField HeaderText="Sl No.">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label1" runat="server" Text='<%# Eval("SlNo") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Application No.">
                                           <ItemTemplate>
                                                 <%--<asp:Label ID="Label2" ></asp:Label>--%>
                                                 <asp:HyperLink ID="hypLink" runat="server" Text='<%# Eval("AppNo") %>'></asp:HyperLink>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Industries Name">
                                           <ItemTemplate>
                                                 <asp:Label ID="Label3" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Industry Type">
                                           <ItemTemplate>
                                                 <asp:Label ID="Label4" runat="server" Text='<%# Eval("IndType") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText=" Claim Amount">
                                           <ItemTemplate>
                                                 <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText=" Approved Amount">
                                           <ItemTemplate>
                                                 <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Status">
                                           <ItemTemplate>
                                                 <asp:Label ID="Label5" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                      
                                         <asp:TemplateField HeaderText="Query">
                                             <ItemTemplate>
                                                 <asp:Button ID="btnSubmit" runat="server" CssClass=" btn btn-success" OnClientClick="return false"
                                                     Text="Revert Query"  />
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
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
