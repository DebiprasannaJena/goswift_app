<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LinkBrowser.aspx.cs" Inherits="LinkBrowser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="sample.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
        <script type="text/javascript">
        $(function () {
            $('a').click(function (e) {
                e.preventDefault();
                var ckEditorNum = parseInt($('#CKEditorFuncNum').val());
                window.opener.CKEDITOR.tools.callFunction(ckEditorNum, $(this).attr('href'), '');
                window.close();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="exLabel" runat="server" Text="Label"></asp:Label>
        <asp:HiddenField ID="CKEditorFuncNum" runat="server" />
        </div>
    </form>
</body>
</html>
