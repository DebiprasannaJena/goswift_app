<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCommentHistory.aspx.cs"
    Inherits="SingleWindow_ViewCommentHistory" %>

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
                    <asp:Repeater ID="RptrComments" runat="server">
                        <HeaderTemplate>
                            <table style="table-layout: fixed; width: 100%;">
                                <tr>
                                    <th>
                                        <h4>
                                            SLFC Memebrs</h4>
                                    </th>
                                    <th>
                                        <h4>
                                             Feedback by SLFC Memebrs</h4>
                                    </th>
                                    <th>
                                        <h4>
                                            Modified Feedbacks by GMSLNA</h4>
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <div class="cmd-sec">
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("VCH_FULLNAME") %>' Font-Bold="true" />
                                        <i class="fa fa-comment text-blue"></i>
                                    </div>
                                </td>
                                <td>
                                    <div class="cmd-sec">
                                        <sup><i class="fa fa-quote-left"></i></sup>
                                        <asp:Label ID="lblComment" runat="server" Text='<%#Eval("VCHCOMMENT") %>' />...
                                        <sup><i class="fa fa-quote-right"></i></sup>
                                    </div>
                                </td>
                                <td>
                                    <div class="cmd-sec">
                                        <%--   <asp:Label ID="lblName1" runat="server" Text='<%#Eval("VCH_FULLNAME") %>' Font-Bold="true" />
                                <i class="fa fa-comment text-blue"></i>--%>
                                        <%-- <br />--%>
                                        <sup><i class="fa fa-quote-left"></i></sup>
                                        <asp:Label ID="lblComment2" runat="server" Text='<%#Eval("VCH_COMMENT_GMSLNA") %>' />...
                                        <sup><i class="fa fa-quote-right"></i></sup>
                                    </div>
                                </td>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
        <table>
            <tr style='<%=IsVisible ? "": "display: none" %>'>
                <td>
                    <asp:Repeater ID="rptClarification" runat="server">
                        <HeaderTemplate>
                            <table style="table-layout: fixed; width: 100%;" >
                                <tr>
                                    <th>
                                        <h4>
                                            SLFC Memebrs</h4>
                                    </th>
                                    <th>
                                        <h4>
                                             Clarification by SLFC Memebrs
                                        </h4>
                                    </th>
                                    <th>
                                        <h4>
                                            Modified Clarification by GMSLNA</h4>
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <div class="cmd-sec">
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("vchFullName") %>' Font-Bold="true" />
                                        <i class="fa fa-comment text-blue"></i>
                                    </div>
                                </td>
                                <td>
                                    <div  class="cmd-sec">
                                        <sup><i class="fa fa-quote-left"></i></sup>
                                        <asp:Label ID="lblComment" runat="server" Text='<%#Eval("vchClarificationSLFC") %>' />...
                                        <sup><i class="fa fa-quote-right"></i></sup>
                                    </div>
                                </td>
                                <td>
                                    <div class="cmd-sec">
                                        <%--   <asp:Label ID="lblName3" runat="server" Text='<%#Eval("vchFullName") %>' Font-Bold="true" />
                                        <i class="fa fa-comment text-blue"></i>--%>
                                    <%--    <br />--%>
                                        <sup><i class="fa fa-quote-left"></i></sup>
                                        <asp:Label ID="lblComment3" runat="server" Text='<%#Eval("vchClarificationGM") %>' />...
                                        <sup><i class="fa fa-quote-right"></i></sup>
                                    </div>
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
        <asp:Label ID="lblMessage" runat="server" Text="No Record(s) Found!!!" Visible="false"
            CssClass="lblMessage" />
    </div>
    </form>
</body>
</html>
