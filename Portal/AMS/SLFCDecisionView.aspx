<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SLFCDecisionView.aspx.cs"
    Inherits="SingleWindow_SLFCDecisionView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>IPICOL Agenda Monitoring System</title>
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-overrides.css" rel="stylesheet" type="text/css" />
    <link href="../css/portal.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/bootstrap.min.js" type="text/javascript" language="javascript"></script>
    <script src="../js/loadComponent.js" language="javascript" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" class="table" width="100%">

        <tr>
            <td colspan="3">
                <div class="viewTable" id="viewTable">
                    
                    <asp:Repeater ID="rptDecision" runat="server">
                        <HeaderTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="table" width="100%">
                            <tr>
                                <td >
                                   <%= DecisionText%>                       
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Container.ItemIndex + 1%>.
                                    <%#DataBinder.Eval(Container.DataItem, "CHECKLISTPOINT")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Label ID="lblMessage" runat="server" Text="No Record(s) Found!!!" Visible="false"
                        CssClass="lblMessage" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
