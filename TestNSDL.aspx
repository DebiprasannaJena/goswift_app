<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestNSDL.aspx.cs" Inherits="TestNSDL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1"
                runat="server" Text="Validate" OnClick="Button1_Click" />

            <br />
            <br />
            <br />
            <br />


            <asp:TextBox ID="TxtPan" runat="server" placeholder="Enter PAN" Width="250px"></asp:TextBox>
            <br />
            <asp:TextBox ID="TxtName" runat="server" placeholder="Enter Name" Width="250px"></asp:TextBox>
            <br />
            <asp:TextBox ID="TxtDob" runat="server" placeholder="Enter Dob (DD/MM/YYYY)" Width="250px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button2"
                runat="server" Text="NSDL-Validate-Veriosn-4" OnClick="Button2_Click" />
            <br />
            <br />
            <asp:Label ID="LblMsg4" runat="server" Font-Bold="true"></asp:Label>


        </div>
    </form>
</body>
</html>
