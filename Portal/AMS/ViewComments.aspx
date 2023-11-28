<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewComments.aspx.cs" Inherits="SingleWindow_ViewComments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
   <link href="../../PortalCSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/stylecrm.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
     <style>
    body{background:#fff;}
 .form-control-static { padding-top: 5px;display: inline-block;}
    
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
                            <h4>
                                SLFC Members Feedback Details</h4>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="cmd-sec">
                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("VCH_FULLNAME") %>' Font-Bold="true" />
                                (<%#Eval("DEPTNAME")%>)
                                <i class="fa fa-comment text-blue"></i>
                                <br />
                                <sup><i class="fa fa-quote-left"></i></sup>
                                <asp:Label ID="lblComment" runat="server" Text='<%#Eval("VCHCOMMENT") %>' />...
                                <sup><i class="fa fa-quote-right"></i></sup> <br />
                                Given on : 
                                <asp:Label ID="lblGivenOn" runat="server" Text='<%#Eval("DTMCREATEDON") %>' />
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr style='<%=IsVisible ? "": "display: none" %>'>
                <td>
                    <asp:Repeater ID="rptClarification" runat="server">
                        <HeaderTemplate>
                            <h4>
                                SLFC Members Clarification Details</h4>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="cmd-sec">
                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("vchFullName") %>' Font-Bold="true" />
                                
                                <i class="fa fa-comment text-blue"></i>
                                <br />
                                <sup><i class="fa fa-quote-left"></i></sup>
                                <asp:Label ID="lblComment" runat="server" Text='<%#Eval("vchClarificationSLFC") %>' />...
                                <sup><i class="fa fa-quote-right"></i></sup><br />
                                  Given on : 
                                <asp:Label ID="lblGivenOn" runat="server" Text='<%#Eval("DTMCREATEDON") %>' />
                            </div>
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
