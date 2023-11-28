<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="incentives_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

    <table class="table table-bordered">
                                                            <tr>
                                                                <th>
                                                                    Sources
                                                                </th>
                                                                <th>
                                                                    Existing
                                                                </th>
                                                                <th>
                                                                    Proposed
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Requirement (KL per Day)</td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox55" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox56" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Existing Allocation,If Any</td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox57" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox58" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Provide Source of Water</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Ground Water
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox51" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox52" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Surface Water
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox53" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox54" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>

    
    </div>
    </form>
</body>
</html>
