<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationForLicence.aspx.cs" Inherits="PluginPages_ApplicationForLicence" %>

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
      <asp:GridView ID="grvAddmore" runat="server" ShowFooter="True" AutoGenerateColumns="False"    CssClass="table table-bordered"
                                                CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="grvAddmore_RowDeleting" >
                                                <Columns>
                                                
                                                    <asp:TemplateField HeaderText="Name and address of the Establishment" >
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAddress"  runat="server" onBlur="GetXML();" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="12.5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. and date of certificate of registration of establishment under the Act" >
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCerification"  runat="server" onBlur="GetXML();" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="12.5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name of process,operation or work for which estblishment is engaged">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOpeEstab"  runat="server" onBlur="GetXML();" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="12.5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nature of process,operation or work for which contract labour is to be employed in the establishment" >
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtNatureEsta"  runat="server" onBlur="GetXML();" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="12.5%" />
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Duration of the proposed contract work (give proposed date of commencing and ending)" >
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtContWrk"  runat="server" onBlur="GetXML();" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="12.5%" />
                                                    </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Name and address of the agent or manager at the work establishment " >
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtMangName"  runat="server" onBlur="GetXML();" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="12.5%" />
                                                    </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Maximum No. employees proposed to be employed as contract labour in the establishment " >
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtMaxEmp"  runat="server" onBlur="GetXML();" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="12.5%" />
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
<%--    <asp:Button ID="btnSubmit" runat="server" text="Submit"   />--%>
    </ContentTemplate></asp:UpdatePanel>
<%--
        <asp:HiddenField ID="HiddenField1"
            runat="server" />--%>
            <input type="hidden" id="hdnId" />
            
 
<%--
    <asp:TextBox ID="TextBox1"
        runat="server" TextMode="MultiLine"></asp:TextBox>
        <asp:HiddenField ID="HiddenField2"
            runat="server" />--%>
    <%--<textarea id="TextArea1" cols="20" rows="2"></textarea>--%>

          
    </form>

    <script >

        $(function () {

            $("#btnSubmit").click(function () {
                debugger;


                console.log(xml2json(jQuery.parseXML(xml)));
                $("#TextArea1").val(xml2json(jQuery.parseXML(xml)));
                return false;
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
                        var $ConnectedLoad = $("<Connected Load>");
                        var $TotalConnectedLoad = $("<Total Connected Load>");
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
                alert($root.html());
                //                console.log($root.html());

                return false;
            })

        })





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
            // alert($root.html());
            // console.log($root.html());
            $('#hdnId').val($root.html());
            // var pp = $.xml2json($root.html());

            //var parser = new DOMParser();
            //var xml = parser.parseFromString($root.html(), "text/xml");
            //var obj = xmlToJson(xml);
            // $('#TextArea1').val($root.html());
            var jsonObject = xml2json(jQuery.parseXML($root.html()));
            console.log(jsonObject);
            $("#TextArea1").val(JSON.stringify(jsonObject));

            // $("#TextArea1").val(JSON.stringify(x2js.xml_str2json($("#xml").val())));
            // $('#TextArea1').val($root.html());
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
                console.log(e.message);
            }
        }
</script>
</body>
</html>
