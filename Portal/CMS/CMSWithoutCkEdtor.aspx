<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CMSWithoutCkEdtor.aspx.cs" Inherits="Portal_CMS_CMSWithoutCkEdtor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
      <script src="../js/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../CKEditor/ckeditor/ckeditor.js?t=C6HH5UF" type="text/javascript"></script>      <script>
          function ToPassValueToChild(strImg) {
              debugger;
              //alert(strImg);
             // window.opener.document.getElementById("txtUrl").value =strImg;
//              window.opener.$("#txtUrl").val(strImg);
//                    //  window.opener.document.getElementById("txtUrl").value = strImg;
              //                      window.close();

              if (window.opener != null && !window.opener.closed) {
                  var txtName = parent.window.opener.document.getElementById("ContentPlaceHolder1_txtUrl");
                  txtName.value = strImg;
              }
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
      <asp:ImageButton Width="100" ID="imgBtn" ImageUrl='<%# Bind("Text", "~/Portal/CMSImage/{0}") %>' runat="server" CommandName="imgClick" />
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
