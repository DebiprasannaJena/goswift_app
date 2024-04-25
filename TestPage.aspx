<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestPage.aspx.cs" Inherits="TestPage" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <title></title>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container wrapper">
        <div class="panel-body">
            <div class="row">
                <br />
                <table width="100%">
                    <tr>
                        <td width="50%" valign="top">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td colspan="3">
                                        <h3 style="color: Green;">
                                            Test Payment Order (Without Restful Service)</h3>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        Order No
                                    </td>
                                    <td width="2%">
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt_Order_No" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Challan Amount
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt_Challan_Amount" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="Btn_Payment_Service_Test" runat="server" Text="Test Payment Service"
                                            OnClick="Btn_Payment_Service_Test_Click" CssClass="btn btn-success" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="Lbl_Payment_Service_Response" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" valign="top">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td colspan="3">
                                        <h3 style="color: Red;">
                                            Test Payment Order (Restful Service)</h3>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        Order No
                                    </td>
                                    <td width="2%">
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt_Order_No_REST" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Challan Amount
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt_Challan_Amount_REST" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="Btn_Pay_REST" runat="server" Text="Test Payment Service (Restful)"
                                            OnClick="Btn_Pay_REST_Click" CssClass="btn btn-danger" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="Lbl_Msg_Restful" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr style="border: 1px solid #15CCD9" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" valign="top">
                            <table width="80%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td colspan="3">
                                        <h3 style="color: Blue;">
                                            Test CRM Service (Restful)</h3>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        Unique SSO ID
                                    </td>
                                    <td width="2%">
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt_SSO_Id" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="Btn_CRM_Service" runat="server" Text="Test CRM (Service)" OnClick="Btn_CRM_Service_Click"
                                            CssClass="btn btn-success" />
                                        <asp:Button ID="Btn_CRM_PEAL" runat="server" Text="Test CRM (PEAL)" OnClick="Btn_CRM_PEAL_Click"
                                            CssClass="btn btn-warning" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <asp:Label ID="Lbl_CRM_Response" runat="server"></asp:Label>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr style="border: 1px solid #15CCD9" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <h3 style="color: Blue;">
                                            Test Excise OSBC Service (WebService)</h3>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="Btn_Excise_OSBC_SignUp" runat="server" Text="Excise OSBC Signup"
                                            CssClass="btn btn-success" OnClick="Btn_Excise_OSBC_SignUp_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                        <br />
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank"></asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr style="border: 1px solid #15CCD9" />
                            <br />
                            <asp:Button ID="Button1" runat="server" Text="Test Multiple Redirects" CssClass="btn btn-success"
                                OnClick="Button1_Click" />
                            <asp:Label ID="Label2" runat="server"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr style="border: 1px solid #15CCD9" />
                            <br />
                            <asp:Button ID="Button4" runat="server" Text="SAML Integration" CssClass="btn btn-success" OnClick="Button4_Click"/>
                            <asp:Label ID="Label4" runat="server"></asp:Label>
                        </td>

                    </tr>

                    <tr>
                        <td colspan="2">
                            <hr style="border: 1px solid #15CCD9" />
                            <br />
                            <asp:Button ID="Button5" runat="server" Text="Token generate" CssClass="btn btn-success" OnClick="Button5_Click"/>
                            <asp:Label ID="Label5" runat="server"></asp:Label>
                        </td>

                    </tr>

                    <tr>
                        <td colspan="2">
                            <hr style="border: 1px solid #15CCD9" />
                            <br />

                            <asp:TextBox ID="TxtFilePath" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Button ID="BtnGetFileName" runat="server" Text="Get File Names" CssClass="btn btn-primary"
                                OnClick="BtnGetFileName_Click" />
                            <asp:Label ID="Label3" runat="server"></asp:Label>

                              
                        </td>
                    </tr>


                </table>

                <div class="table-responsive">
                    <asp:GridView ID="GrdFileNames" runat="server" CssClass="table table-bordered">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>

                                    <asp:Button ID="BtnDownload" runat="server" Text="Download" OnClick="BtnDownload_Click" />
                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>
                </div>

            </div>
            <hr style="border: 1px solid #15CCD9" />
                            <br />
            <div>
                <label>Test Encript</label> <asp:TextBox ID="TextBox1" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>

               <%-- <asp:Button ID="BTnid_encriprt" runat="server" Text="Encript" OnClick="BTnid_encriprt_Click"/>--%>
                <label runat="server" id="lbl_test"></label>
            </div>
            <hr style="border: 1px solid #15CCD9" />
                            <br />
            <div>
                <label>DWH Text decript</label> <asp:TextBox ID="Txt_Decript" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                <%--<asp:Button ID="btn_decript" runat="server" Text="Decript" OnClick="btn_decript_Click"/>--%>
                <label runat="server" id="lbl_decript"></label>
            </div>

            <hr style="border: 1px solid #15CCD9" />
                            <br />
            <div>
                <label>CIN NUMBER</label><asp:TextBox ID="txt_cin" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                <asp:Button ID="btn_cin" runat="server" Text="Validate CIN" OnClick="btn_cin_Click"/>
                <label runat="server" id="Lbl_cin"></label>
            </div>
            <div>
                        <td colspan="2">
                            <hr style="border: 1px solid #15CCD9" />
                            <br />
                            <h3 style="color: Blue;">
                                            Test Peal Data Push Service</h3>
                            <br />
                            <asp:Button ID="Btn_Peal_Data_Push" runat="server" Text="Peal Data Push" CssClass="btn btn-success" OnClick="Btn_Peal_Data_Push_Click"/>
                            <asp:Label ID="Lbl_Msg_Peal" runat="server"></asp:Label>
                     </td>

                    </div>
             
        </div>
    </div>
         
    <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
    <asp:Button ID="Button3" runat="server" Text="File Download" OnClick="Button3_Click" />
    <%----------------------------------------------------------------------------------------------------------%>
    
        </form>
</body>
</html>
