<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GrantAndRenewaloflicense.aspx.cs" Inherits="PluginPages_GrantAndRenewaloflicense" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
<%--    <script src="../js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/js_jQuery-2.1.3.min.js" type="text/javascript"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div id="dvGrid">
      <asp:GridView ID="grvAddmore" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                                                 CssClass="table table-bordered" OnRowDeleting="grvAddmore_RowDeleting" >
                                                <Columns>
                                                
                                                    <asp:TemplateField HeaderText="Name" >
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtName"  CssClass="form-control" runat="server" onBlur="JsonStringData();"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="25%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Values" >
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtValue" CssClass="form-control" runat="server" onBlur="JsonStringData();"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="25%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            
                                                            <asp:LinkButton ID="ButtonAdd" OnClick="ButtonAdd_Click" runat="server" CssClass="btn btn-success btn-sm" ><i class="fa fa-plus"></i></asp:LinkButton>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>  
                                                     <asp:CommandField ShowDeleteButton="True" />                                               
                                                </Columns>
                                                
                                             
                                            </asp:GridView>
    </div>
<%--    <asp:Button ID="btnSubmit" runat="server" text="Submit"   />--%>
    </ContentTemplate></asp:UpdatePanel>
<%--
        <asp:HiddenField ID="HiddenField1"
            runat="server" />--%>
            <input type="hidden" id="hdnJsonDt" />
            
 
<%--
    <asp:TextBox ID="TextBox1"
        runat="server" TextMode="MultiLine"></asp:TextBox>
        <asp:HiddenField ID="HiddenField2"
            runat="server" />--%>
   <%-- <textarea id="TextArea1" cols="20" rows="2"></textarea>--%>

          
    </form>
    <script >





        function JsonStringData() {
            debugger;
            var rows = [];
            $("[id*=grvAddmore] >tbody > tr").each(function () {
                var $row = $(this);
                var parentId = $row.closest('tr').find('[id*=txtName]').val();
                if (parentId != undefined && parentId != 'undefined') {
                    rows.push({
                        Name: $row.closest('tr').find('[id*=txtName]').val(),
                       Value: $row.closest('tr').find('[id*=txtValue]').val()
                     
   

                    });
                }
            });
            console.log(JSON.stringify(rows));
            $("#hdnJsonDt").val(JSON.stringify(rows));
//            $("#TextArea1").val($("#hdnJsonDt").val());
            return JSON.stringify(rows);
        };
</script>
</body>
</html>
