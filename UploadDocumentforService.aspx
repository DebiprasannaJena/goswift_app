<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadDocumentforService.aspx.cs" Inherits="UploadDocumentforService" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    

<script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="js/jQuery.alert.js" type="text/javascript"></script>
<link href="css/jQuery.alert.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
  <%-- <uc2:header ID="header" runat="server" />--%>
    <div class="registration-div">

    <div>
  File Name:  <asp:textbox ID="txtFileName" runat="server"></asp:textbox>
  File Path:<asp:textbox ID="txtPath" Text="Portal/Document/Upload" runat="server"></asp:textbox>
 File  <asp:FileUpload  ID="docUpload" CssClass="form-control docUpload" runat="server"/>
                                             
                                                              
    </div>
        <div>
          <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                onclick="btnSubmit_Click" />  
        </div>
        </div>
        </div>
    </form>
</body>
</html>

