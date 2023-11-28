<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FB_Division2.aspx.cs" Inherits="PluginPages_FB_Division2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
        <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="1" id="tblId" width="50%">

    <tr><td>Boiler Number</td>
    <td><input type="text" id="txtNature_txt1" onBlur="JsonStringData();" style="width:100%" /></td>
    </tr>

<tr>
<td>Boiler Rating</td>
<td><input type="text" id="txtNature_txt2" onBlur="JsonStringData();" style="width:100%"/></td>
</tr>
<tr>
<td>Fees</td>
<td><input type="text" id="txtNature_txt3" onBlur="JsonStringData();" style="width:100%"/></td>
</tr>
<tr>
<td>Extra fees for sunday and holiday inspection and other expenses</td>
<td><input type="text" id="txtNature_txt4" onBlur="JsonStringData();" style="width:100%"/></td>

</tr>
    <tr>
    <td>Total</td>
    <td><input type="text" id="txtNature_txt5" onBlur="JsonStringData();" style="width:100%"/></td>

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
