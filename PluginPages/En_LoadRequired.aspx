<%@ Page Language="C#" AutoEventWireup="true" CodeFile="En_LoadRequired.aspx.cs" Inherits="PluginPages_En_LoadRequired" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="1" id="tblId">
    <tr><th>Items</th><th>NatureOfDemand</th><th>Number</th><th>ConnectedLoad</th><th>TotalConnectedLoad</th><th>Remark</th></tr>
    <tr><td>Light</td>
    <td><div><input type="text" id="txtNature_txt1" onBlur="JsonStringData();"/></div></td>
    <td><div><input type="text" id="txtNumber_txt1" onBlur="JsonStringData();"/></div></td>
    <td><div><input type="text" id="txtConnectedLoad_txt1" onBlur="JsonStringData();"/></div></td>
    <td><div><input type="text" id="txtTotalConnectedLoad_txt1" onBlur="JsonStringData();"/></div></td>
    <td><div><input type="text" id="txtRemark_txt1" onBlur="JsonStringData();"/></div></td></tr>
<tr>
<td>Fans</td>
<td><div><input type="text" id="txtNature_txt2" onBlur="JsonStringData();"/></div></td>
<td><div><input type="text" id="txtNumber_txt2" onBlur="JsonStringData();"/></div></td>
<td><div><input type="text" id="txtConnectedLoad_txt2" onBlur="JsonStringData();"/></div></td>
<td><div><input type="text" id="txtTotalConnectedLoad_txt2" onBlur="JsonStringData();"/></div></td>
<td><div><input type="text" id="txtRemark_txt2" onBlur="JsonStringData();"/></div></td>
</tr>
<tr>
<td>Plug Points</td>
<td><div><input type="text" id="txtNature_txt3" onBlur="JsonStringData();"/></div></td>
<td><div><input type="text" id="txtNumber_txt3" onBlur="JsonStringData();"/></div></td>
<td><div><input type="text" id="txtConnectedLoad_txt3" onBlur="JsonStringData();"/></div></td>
<td><div><input type="text" id="txtTotalConnectedLoad_txt3" onBlur="JsonStringData();"/></div></td>
<td><div><input type="text" id="txtRemark_txt3" onBlur="JsonStringData();"/></div></td>
</tr>
<tr>
<td>Plug Point Appliance</td>
<td><div><input type="text" id="txtNature_txt4" onBlur="JsonStringData();"/></div></td>
<td><div><input type="text" id="txtNumber_txt4" onBlur="JsonStringData();"/></div></td>
<td><div><input type="text" id="txtConnectedLoad_txt4" onBlur="JsonStringData();"/></div></td>
<td><div><input type="text" id="txtTotalConnectedLoad_txt4" onBlur="JsonStringData();"/></div></td>
<td><div><input type="text" id="txtRemark_txt4" onBlur="JsonStringData();"/></div></td>
</tr>
    <tr>
    <td>Consignees</td>
    <td><div><input type="text" id="txtNature_txt5" onBlur="JsonStringData();"/></div></td>
    <td><div><input type="text" id="txtNumber_txt5" onBlur="JsonStringData();"/></div></td>
    <td><div><input type="text" id="txtConnectedLoad_txt5" onBlur="JsonStringData();"/></div></td>
    <td><div><input type="text" id="txtTotalConnectedLoad_txt5" onBlur="JsonStringData();"/></div></td>
    <td><div><input type="text" id="txtRemark_txt5" onBlur="JsonStringData();"/></div></td>
    </tr>
    </table>
    <%--  <textarea id="TextArea1" cols="20" rows="2"></textarea>--%>
      <input type="hidden" id="hdnJsonDt" />
    </div>
    </form>
        <script >



            function JsonStringData() {
                debugger;
                var rows = [];
                $("[id*=tblId] >tbody > tr").each(function () {
                    var $row = $(this);
                    var parentId = $row.closest('tr').find('[id*=txtNature]').val();
                    if (parentId != undefined && parentId != 'undefined') {
                        rows.push({
                            Hp: $row.closest('tr').find('[id*=txtNature]').val(),
                            PrimaryVoltage: $row.closest('tr').find('[id*=txtNumber]').val(),
                            PrimaryProtection: $row.closest('tr').find('[id*=txtConnectedLoad]').val(),
                            SecondaryVoltage: $row.closest('tr').find('[id*=txtTotalConnectedLoad]').val(),
                            SecondaryProtection: $row.closest('tr').find('[id*=txtRemark]').val()

                        });
                    }
                });
                console.log(JSON.stringify(rows));
                $("#hdnJsonDt").val(JSON.stringify(rows));
                $("#TextArea1").val($("#hdnJsonDt").val());
                return JSON.stringify(rows);
            };


</script>
</body>
</html>
