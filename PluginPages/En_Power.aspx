<%@ Page Language="C#" AutoEventWireup="true" CodeFile="En_Power.aspx.cs" Inherits="PluginPages_En_Power" %>

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
      <asp:GridView ID="grvAddmore" runat="server" ShowFooter="True" AutoGenerateColumns="False" CssClass="table table-bordered"
                                                CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="grvAddmore_RowDeleting" onrowdatabound="grvAddmore_RowDataBound" >
                                                <Columns>
                                                  <asp:BoundField HeaderText="Item No."/>
                                                    <asp:TemplateField HeaderText="H.P of each K.W">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtHp" CssClass="form-control" runat="server" onBlur="JsonStringData();"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Voltage">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtVoltage" CssClass="form-control" runat="server" onBlur="JsonStringData();"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Winding">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtWinding" CssClass="form-control" runat="server" onBlur="JsonStringData();"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Use">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtUse" CssClass="form-control" runat="server" onBlur="JsonStringData();"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemark" CssClass="form-control" runat="server" onBlur="JsonStringData();"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="LinkButton1" OnClick="ButtonAdd_Click" runat="server" CssClass="btn btn-success btn-sm" ><i class="fa fa-plus"></i></asp:LinkButton>
                                       
                                                        </FooterTemplate>
                                                    </asp:TemplateField>  
                                                     <asp:CommandField ShowDeleteButton="True" />                                               
                                                </Columns>
                                         
                                            </asp:GridView>
    </div>
<%--  <asp:Button ID="btnSubmit" runat="server" text="Submit" OnClientClick="return JsonStringData();"   />--%>
    </ContentTemplate></asp:UpdatePanel>
<%--
        <asp:HiddenField ID="HiddenField1"
            runat="server" />--%>
            <input type="hidden" id="hdnId" />
            
 
<%--
    <asp:TextBox ID="TextBox1"
        runat="server" TextMode="MultiLine"></asp:TextBox>--%>
<input type="hidden"  id="hdnJsonDt"/>
  <%--<textarea id="TextArea1" cols="20" rows="2"></textarea>--%>

          
    </form>
    <script >


        function GetXML() {
            var $root = $("<Root>");
            var $xml = $("<DocumentElement>");

            var cnt = 0;
            $("[id*=grvAddmore] >tbody > tr").each(function () {

                var $schedule = $("<OrderTable>");
                var $row = $(this);
                var parentId = $row.closest('tr').find('[id*=txtHp]').val();

                if (parentId != undefined && parentId != 'undefined') {
                    var Hp = $row.closest('tr').find('[id*=txtHp]').val();
                    var Voltage = $row.closest('tr').find('[id*=txtVoltage]').val();
                    var Winding = $row.closest('tr').find('[id*=txtWinding]').val();
                    var Use = $row.closest('tr').find('[id*=txtUse]').val();
                    var Remark = $row.closest('tr').find('[id*=txtRemark]').val();
                    var $Hp = $("<txtHp>");
                    var $Voltage = $("<txtVoltage>");
                    var $Winding = $("<txtWinding>");
                    var $Use = $("<txtUse>");
                    var $Remark = $("<Remark>");


                    $Hp.text(Hp)
                    $Voltage.text(Voltage);
                    $Winding.text(Winding);
                    $Use.text(Use);
                    $Remark.text(Remark);

                    $schedule.append($Hp);
                    $schedule.append($Voltage);
                    $schedule.append($Winding);
                    $schedule.append($Use);
                    $schedule.append($Remark);
                    $xml.append($schedule);

                }
            })

            $root.append($xml);
            $('#hdnId').val($root.html());
            var jsonObject = xml2json(jQuery.parseXML($root.html()));
            $("#hdnJsonDt").val(JSON.stringify(jsonObject));
            $("#TextArea1").val($("#hdnJsonDt").val());
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
                var parentId = $row.closest('tr').find('[id*=txtHp]').val();
                if (parentId != undefined && parentId != 'undefined') {
                    rows.push({
                        Hp: $row.closest('tr').find('[id*=txtHp]').val(),
                        Voltage: $row.closest('tr').find('[id*=txtVoltage]').val(),
                        Winding: $row.closest('tr').find('[id*=txtWinding]').val(),
                        Use: $row.closest('tr').find('[id*=txtUse]').val(),
                        Remark: $row.closest('tr').find('[id*=txtRemark]').val()

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