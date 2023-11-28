<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewProjectsDtls.aspx.cs"
    Inherits="SingleWindow_ViewProjectsDtls" %>
    <%@ Register Src="~/includes/IncludeScript.ascx" TagName="IncludeScript" TagPrefix="ucIncludeScript" %>
<%@ Register Src="../includes/header.ascx" TagName="header" TagPrefix="ucheader" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <title>Welcome - Agenda Monitoring System</title>
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.1)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.1)" />
    <ucIncludeScript:IncludeScript ID="header2" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <style>
        .listX ul li
        {
            list-style-type: disc;
            margin-left: 15px;
        }
        .listX ol
        {
            list-style-type: decimal;
        }
        .listX ol li
        {
            list-style-type: decimal;
            margin-left: 15px;
        }
        .listX li
        {
            list-style-type: upper-roman;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ucheader:header ID="header1" runat="server" />
    <div class="container">
        <div class=" panel-default">
            <div class="panel-content">
                <div id="divpaging" style="float: right;" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="height: 20px" align="right">
                                <span id="spanPaging" runat="server">
                                    <asp:LinkButton ID="lbtnAll" CssClass="more" runat="server" Text="All" OnClick="lbtnAll_Click"></asp:LinkButton>&nbsp;
                                    <asp:Label CssClass="tdDataNormal" ID="lblPaging" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="clear: both;">
                </div>
                <asp:GridView ID="GrdProjects" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="true" DataKeyNames="INTPROJCTID" CssClass="table table-bordered table-condensed"
                    OnPageIndexChanging="GrdProjects_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Sl#">
                            <ItemTemplate>
                                <asp:Label ID="lblsl" runat="server" Text='<%#(GrdProjects.PageIndex * GrdProjects.PageSize) + (GrdProjects.Rows.Count + 1)%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company Name">
                            <ItemTemplate>
                                <a href="../SingleWindow/ProjectDetails.aspx?ID=<%# Eval("INTPROJCTID") %>" title="Details"
                                    target="_blank">
                                    <%# Eval("VCHPROJCT_NAME")%></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="250" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />                          
                        </asp:TemplateField>
                          <asp:BoundField DataField="VCHPROJECT_TITLE" HeaderText="Project Title">
                                <headerstyle horizontalalign="Center" verticalalign="Middle" />
                                <itemstyle horizontalalign="Left" verticalalign="Middle" />
                            </asp:BoundField>
                        <%--   <asp:BoundField DataField="VCHPROJCT_NAME" HeaderText="Company Name">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"  Width="30%"  />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="VchSectorName" HeaderText="Sector">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DTMAPPLICATION_EBIZ" HeaderText="Date of Application">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="9%" />
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle CssClass="paging NOPRINT" />
                    <PagerSettings Mode="NumericFirstLast" NextPageText="Next" FirstPageText="First"
                        LastPageText="Last" PreviousPageText="Prev" Position="Bottom" />
                </asp:GridView>
                <asp:Label ID="lblMessage" runat="server" Text="No Record(s) Found!!!" Visible="false"
                    CssClass="lblMessage" />
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {


        });
    </script>
</body>
</html>
