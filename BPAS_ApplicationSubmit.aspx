<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BPAS_ApplicationSubmit.aspx.cs" Inherits="BPAS_ApplicationSubmit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="hdnOutput" runat="server" />
        <asp:HiddenField ID="hdnUsrId" runat="server" />
        <asp:HiddenField ID="hdnPassword" runat="server" />
        <asp:HiddenField ID="hdnEncUsrId1" runat="server" />
        <asp:HiddenField ID="hdnEncPassword1" runat="server" />
        <asp:HiddenField ID="hdnChkStatusUrl" runat="server" />
        <asp:HiddenField ID="hdnRedirectUrl1" runat="server" />
        <asp:HiddenField ID="hdn_swpcode" runat="server" />
        <asp:HiddenField ID="hdn_uniquecode" runat="server" />


        <asp:HiddenField ID="hdn_ChkUsrResult" runat="server" />
        <asp:HiddenField ID="hdn_InsertUserDtlResult1" runat="server" />
        <asp:HiddenField ID="hdn_InsertUserDtlResult2" runat="server" />
        <asp:HiddenField ID="hdn_EncypUsrId" runat="server" />
        <asp:HiddenField ID="hdn_EncypPasswd" runat="server" />
        <asp:HiddenField ID="hdn_RegisterResult" runat="server" />
        <asp:HiddenField ID="hdn_RedirectionUrl" runat="server" />
        <asp:HiddenField ID="hdn_ErrorDetls" runat="server" />
        <asp:HiddenField ID="HiddenField9" runat="server" />

    </div>
    </form>
</body>
</html>
