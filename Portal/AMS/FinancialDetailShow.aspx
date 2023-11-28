<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinancialDetailShow.aspx.cs"
    Inherits="SingleWindow_FinancialDetailShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>IPICOL Agenda Monitoring System</title>
  <link href="../../PortalCSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/stylecrm.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
     <style>
    body{background:#fff;}
 .form-control-static { padding-top: 5px;display: inline-block;}
    </style>
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            groupTable($('#GrdFinanace tr:has(td)'), 1, 2);
            $('#GrdFinanace .deleted').remove();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Container">
        
        <div class="viewTable">
           <asp:GridView ID="grdFinDoc" runat="server" Width="100%" AutoGenerateColumns="False"
                DataKeyNames="ProjectId,FinNewDoc,KeyID" CssClass="table table-bordered" OnRowDataBound="grdFinDoc_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Sl#">
                        <ItemTemplate>
                            <span>
                                <%#Container.DataItemIndex + 1%>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Financial Document">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnFinDoc" runat="server" Value='<%# Eval("FinNewDoc") %>' />
                            <asp:HiddenField ID="hdnOriDoc" runat="server" Value='<%# Eval("FinOriDoc") %>' />
                            <asp:HyperLink ID="hlDoc" runat="server" Target="_blank" Text='<%# Eval("FinOriDoc") %>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblMessage1" runat="server" Text="No Financial Document Attached!!!" Visible="false" CssClass="lblMessage" />
        </div>
    </div>
    </form>
</body>
</html>
