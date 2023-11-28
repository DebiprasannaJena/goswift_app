<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plugins.aspx.cs" ValidateRequest="false" Inherits="Plugins" EnableEventValidation="false"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 
    <script src="../js/jquery.min.js" type="text/javascript"></script>
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
      <asp:GridView ID="grvAddmore" runat="server" ShowFooter="True" AutoGenerateColumns="False"    CssClass="table table-bordered"
                                                CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="grvAddmore_RowDeleting" >
                                                <Columns>
                                                
                                                    <asp:TemplateField HeaderText="Nature">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtNature"  runat="server" onBlur="JsonStringData();"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Number">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtNumber"  runat="server" onBlur="JsonStringData();"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Connected Load">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtConnLoad"  runat="server" onBlur="JsonStringData();"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Connected Load">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTotalConnLoad"  runat="server" onBlur="JsonStringData();"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemark"  runat="server" onBlur="JsonStringData();"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" OnClick="ButtonAdd_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>  
                                                     <asp:CommandField ShowDeleteButton="True" />                                               
                                                </Columns>
                                             
                                            </asp:GridView>
    </div>

    </ContentTemplate></asp:UpdatePanel>
<%--
        <asp:HiddenField ID="HiddenField1"
            runat="server" />--%>
            <input type="hidden" id="hdnId" />
            
 
<%--
    <asp:TextBox ID="TextBox1"
        runat="server" TextMode="MultiLine"></asp:TextBox>--%>
<input type="hidden"  id="hdnJsonDt"/>
  <textarea id="TextArea1" cols="20" rows="2"></textarea>

          
    </form>
    <script >


        function GetXML() {
            var $root = $("<Root>");
            var $xml = $("<DocumentElement>");

            var cnt = 0;
            $("[id*=grvAddmore] >tbody > tr").each(function () {

                var $schedule = $("<OrderTable>");
                var $row = $(this);
                var parentId = $row.closest('tr').find('[id*=txtNature]').val();

                if (parentId != undefined && parentId != 'undefined') {
                    var Nature = $row.closest('tr').find('[id*=txtNature]').val();
                    var Number = $row.closest('tr').find('[id*=txtNumber]').val();
                    var ConnLoad = $row.closest('tr').find('[id*=txtConnLoad]').val();
                    var TotalConnLoad = $row.closest('tr').find('[id*=txtTotalConnLoad]').val();
                    var Remark = $row.closest('tr').find('[id*=txtRemark]').val();
                    var $Nature = $("<Nature>");
                    var $Number = $("<Number>");
                    var $ConnectedLoad = $("<ConnectedLoad>");
                    var $TotalConnectedLoad = $("<TotalConnectedLoad>");
                    var $Remark = $("<Remark>");


                    $Nature.text(Nature)
                    $Number.text(Number);
                    $ConnectedLoad.text(ConnLoad);
                    $TotalConnectedLoad.text(TotalConnLoad);
                    $Remark.text(Remark);

                    $schedule.append($Nature);
                    $schedule.append($Number);
                    $schedule.append($ConnectedLoad);
                    $schedule.append($TotalConnectedLoad);
                    $schedule.append($Remark);
                    $xml.append($schedule);

                }
            })

            $root.append($xml);
                    $('#hdnId').val($root.html());
                    var jsonObject = xml2json(jQuery.parseXML($root.html()));
                  //  console.log(jsonObject);
                    //$("#TextArea1").val(JSON.stringify(jsonObject));
                    $("#hdnJsonDt").val(JSON.stringify(jsonObject));
                    $("#TextArea1").val(  $("#hdnJsonDt").val());
            return false;

        }
        function xml2json(xml) {
           
            try {
                var obj = {};
                if (xml.children.length > 0) {
                    for (var i = 0; i < xml.children.length; i++) {
                        var item = xml.children.item(i);
                        var nodeName = item.nodeName;

                        if (typeof (obj[nodeName]) == "undefined") {
                            obj[nodeName] = xml2json(item);
                        } else {
                            if (typeof (obj[nodeName].push) == "undefined") {
                                var old = obj[nodeName];

                                obj[nodeName] = [];
                                obj[nodeName].push(old);
                            }
                            obj[nodeName].push(xml2json(item));
                        }
                    }
                } else {
                    obj = xml.textContent;
                }
                return obj;
            } catch (e) {
               // console.log(e.message);
            }
        }


        function JsonStringData() {
            debugger;
                var rows = [];
                $("[id*=grvAddmore] >tbody > tr").each(function () {
                    var $row = $(this);
                    var parentId = $row.closest('tr').find('[id*=txtNature]').val();
                    if (parentId != undefined && parentId != 'undefined') {
                        rows.push({
                            Nature: $row.closest('tr').find('[id*=txtNature]').val(),
                            Number: $row.closest('tr').find('[id*=txtNumber]').val(),
                            ConnectedLoad: $row.closest('tr').find('[id*=txtConnLoad]').val(),
                            TotalConnectedLoad: $row.closest('tr').find('[id*=txtTotalConnLoad]').val(),
                            Remark: $row.closest('tr').find('[id*=txtRemark]').val()

                        });
                    }
                });
                console.log(JSON.stringify(rows));
                $("#hdnJsonDt").val(JSON.stringify(rows));
                $("#TextArea1").val($("#hdnJsonDt").val());
                return JSON.stringify(rows);
            };
//            $(function () {
//                console.log(convertTableToJson());
//            });
    


</script>
</body>
</html>
<%--(function($){
var convertTableToJson = function()
{
var rows = [];
$('table tr').each(function(i, n){
var $row = $(n);
rows.push({
display_name: $row.find('td:eq(0)').text(),
first_name: $row.find('td:eq(1)').text(),
last_name: $row.find('td:eq(2)').text(),
street: $row.find('td:eq(3)').text(),
city: $row.find('td:eq(4)').text(),
state: $row.find('td:eq(5)').text(),
zip: $row.find('td:eq(6)').text()
});
});
return JSON.stringify(rows);
};
$(function(){
console.log(convertTableToJson ());
});
})(jQuery);--%>