<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestNSWS.aspx.cs" Inherits="TestNSWS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <title></title>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container wrapper">
            <div class="panel-body">
                <asp:Button ID="BtnToken" runat="server" Text="Token Generation" OnClick="BtnToken_Click"
                    CssClass="btn btn-success" />
                <br />
                <br />
                <br />
                <asp:Label ID="Lbl_Token_Addrees" runat="server"></asp:Label>
                <br />
                <br />
                <br />
                <asp:Label ID="Lbl_Token_Response" runat="server"></asp:Label>
                <br />
                <br />
                <br />
                <asp:Label ID="Lbl_Token" runat="server"></asp:Label>
                <br />
                <br />
                <br />
                <div style="display: block;">
                    <asp:Button ID="BtnTokenGeneration" runat="server" Text="Token Generation and CAF Pull"
                        OnClick="BtnTokenGeneration_Click" CssClass="btn btn-success" />
                    <asp:TextBox ID="Txt_SWS_Id" runat="server" placeholder="Enter SWS Id Here"></asp:TextBox>

                    <br />
                    <br />
                    <br />
                    <asp:Button ID="BtnRedirectionUrl" runat="server" Text="Push Redirection URL" OnClick="BtnRedirectionUrl_Click"
                        CssClass="btn btn-danger" />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="BtnPullDoc" runat="server" Text="Pull Document" OnClick="BtnPullDoc_Click"
                        CssClass="btn btn-danger" />
                    <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="BtnTest2" runat="server" Text="Test HTTP" OnClick="BtnTest2_Click"
                        CssClass="btn btn-success" />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="BtnRunScheduler" runat="server" Text="Run Redirection URL Scheduler"
                        OnClick="BtnRunScheduler_Click" CssClass="btn btn-danger" />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="BtnPushLicense" runat="server" Text="Push License Status" OnClick="BtnPushLicense_Click"
                        CssClass="btn btn-danger" />
                </div>
                <br />
                <br />
                <br />
                <div class="form-group">
                    <div class="row">
                        <label class="col-sm-2">Enter JSON Text Returned from CAF</label>
                        <div class="col-sm-10">
                            <span class="colon">:</span>
                            <asp:TextBox ID="Txt_CAF_JSON" runat="server" TextMode="MultiLine" CssClass="form-control" Height="250px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <label class="col-sm-2"></label>
                        <div class="col-sm-10">
                            <asp:Button ID="Btn_Validate_CAF_JSON" runat="server" Text="Validate CAF JSON" OnClick="Btn_Validate_CAF_JSON_Click" CssClass="btn btn-success" />
                             <asp:Button ID="Btn_Pull_CAF_JSON_Test" runat="server" Text="Pull CAF" OnClick="Btn_Pull_CAF_JSON_Test_Click" CssClass="btn btn-primary" />
                            <br />
                            <asp:Label ID="Lbl_Error_Msg_CAF" runat="server" ForeColor="Red"></asp:Label>
                            <br />
                            <asp:Label ID="Lbl_Error_Line_Msg_CAF" runat="server" ForeColor="Blue"></asp:Label>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
