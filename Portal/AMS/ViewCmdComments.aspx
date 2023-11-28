<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCmdComments.aspx.cs"
    Inherits="SingleWindow_ViewCmdComments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/custom.js" type="text/javascript"></script>
    <link href="../css/agenda.css" rel="stylesheet" type="text/css" />
    <style>
        .cmd-sec
        {
            background-color: #f1efef;
            margin-bottom: 5px;
            padding: 5px;
            border-radius: 3px;
        }
        sub, sup
        {
            font-size: 55%;
        }
        .text-blue
        {
            color: #08c;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
        <table class="table table-bordered">
            <tr>
                <td>
                    <asp:Repeater ID="RptrCMDComments" runat="server">
                        <HeaderTemplate>
                            <h4>
                                CMD Accepted Comments Details</h4>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="cmd-sec">
                                <%--<asp:Label ID="lblName" runat="server" Text='<%#Eval("VCH_FULLNAME") %>' Font-Bold="true" />
                                <i class="fa fa-comment text-blue"></i>
                                <br />--%>
                                <sup><i class="fa fa-quote-left"></i></sup>
                                <asp:Label ID="lblComment" runat="server" Text='<%#Eval("VCHCOMMENT") %>' />...
                                <sup><i class="fa fa-comment text-blue"></i></sup>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>
                    <%--<asp:Repeater ID="RptrCMDComments" runat="server">
                        <HeaderTemplate>
                            <table style="border: 0px solid #0000FF; width: 500px" cellpadding="0">
                                <tr style="background-color: #ac2d00; color: #000000; font-size: large; font-weight: bold;">
                                    <td colspan="2">
                                        <b style="color: #fff; padding: 5px; font-size: 16px;">CMD Accepted Comments Details</b>
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>                       
                            <tr>
                                <td>
                                    <asp:Label ID="lblComment" runat="server" Text='<%#Eval("VCHCOMMENT") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>--%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Repeater ID="RptrCMDCmntRetrn" runat="server">
                        <HeaderTemplate>
                            <h4>
                                CMD Return Comment Details</h4>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="cmd-sec">
                                <%--<asp:Label ID="lblName" runat="server" Text='<%#Eval("VCH_FULLNAME") %>' Font-Bold="true" />
                                <i class="fa fa-comment text-blue"></i>
                                <br />--%>
                                <sup><i class="fa fa-quote-left"></i></sup>
                                <asp:Label ID="lblComment" runat="server" Text='<%#Eval("VCHCOMMENT") %>' />...
                                <sup><i class="fa fa-comment text-blue"></i></sup>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblMessage" runat="server" Text="No Record(s) Found!!!" Visible="false"
            CssClass="lblMessage" />
    </div>
    </form>
</body>
</html>
