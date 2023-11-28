<%--'*******************************************************************************************************************
' File Name         : Drafted_User.aspx
' Description       : To view  second level user (Person) which are in drafted stage and also repush the user details to Data Warehouse and update unique id.
' Created by        : Sushant Kumar Jena
' Created On        : 21-Sep-2018
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Drafted_User.aspx.cs" Inherits="InvestorPortal_Drafted_User" %>

<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/pealwebfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/animate.css" rel="stylesheet" type="text/css" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('.menumanage').addClass('active');
        })

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        
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
                        <uc4:pealmenu ID="pealmenu" runat="server" />
                    </div>
                    <div class="form-sec">
                        <div class="form-header">
                            <h2>
                                Drafted User Details
                            </h2>
                        </div>
                        <div class="form-body">
                            <div class="table-responsive" id="divGrd" style="margin-top: 15px;">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped bg-white"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SlNo">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_SlNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Name">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Investor_Name" runat="server" Text='<%# Eval("VCH_INV_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Full Name">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_User_Name" runat="server" Text='<%# Eval("VCH_FULL_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Id">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_User_Id" runat="server" Text='<%# Eval("VCH_INV_USERID") %>'></asp:Label>
                                                <asp:HiddenField ID="Hid_Investor_Id" runat="server" Value='<%# Eval("INT_INVESTOR_ID") %>' />
                                                <asp:HiddenField ID="Hid_Parent_Id" runat="server" Value='<%# Eval("INT_PARENT_ID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email Id">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Email_Id" runat="server" Text='<%# Eval("VCH_EMAIL") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile No">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Mobile_No" runat="server" Text='<%# Eval("VCH_OFF_MOBILE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PAN">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_PAN" runat="server" Text='<%# Eval("VCH_PAN") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="9%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="Btn_Action" runat="server" Text="Submit" Width="80px" OnClick="Btn_Action_Click"
                                                    CssClass="btn btn-success" />
                                            </ItemTemplate>
                                            <ItemStyle Width="8%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No drafted user found !!
                                    </EmptyDataTemplate>
                                </asp:GridView>
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
