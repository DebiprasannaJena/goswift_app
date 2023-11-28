<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeFile="CMSImages.aspx.cs" Inherits="Portal_CMS_CMSImages" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script src="../js/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../CKEditor/ckeditor/ckeditor.js?t=C6HH5UF" type="text/javascript"></script>    <script>
        // Helper function to get parameters from the query string.
        function getUrlParam(paramName) {
            var reParam = new RegExp('(?:[\?&]|&)' + paramName + '=([^&]+)', 'i');
            var match = window.location.search.match(reParam);

            return (match && match.length > 1) ? match[1] : null;
        }
        // Simulate user action of selecting a file to be returned to CKEditor.
        function returnFileUrl(imgPath) {

            var funcNum = getUrlParam('CKEditorFuncNum');
            var fileUrl = imgPath;
            window.opener.CKEDITOR.tools.callFunction(funcNum, fileUrl);
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:DataList ID="dtlist" runat="server" RepeatColumns="4" CellPadding="5" onitemcommand="dtlist_ItemCommand" 
           
          >  
      <ItemTemplate>  
      <asp:ImageButton Width="100" ID="imgBtn" OnClientClick=<%# "returnFileUrl('"+System.Configuration.ConfigurationManager.AppSettings["AppUrl"].ToString()+"Portal/CMSImage/"+ Eval("Text") + "');return false;" %> ImageUrl='<%# Bind("Text", "~/Portal/CMSImage/{0}") %>' runat="server" CommandName="imgClick" />
      <asp:HiddenField  ID="hdnImg" runat="server"/>
       <%--  <asp:Image Width="100" ID="Image1" ImageUrl='<%# Bind("Text", "~/Portal/CMSImage/{0}") %>' runat="server" />  --%>
         <br />  
       <%--  <asp:HyperLink ID="HyperLink1" Text='<%# Bind("Name") %>' NavigateUrl='<%# Bind("Name", "~/Images/{0}") %>' runat="server"/>  --%>
      </ItemTemplate>  
      <ItemStyle BorderColor="Brown" BorderStyle="dotted" BorderWidth="3px" HorizontalAlign="Center"  
         VerticalAlign="Bottom" />  
   </asp:DataList>  
    </div>
    </form>
</body>
</html>
