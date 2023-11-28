<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNodalOfficer.aspx.cs"
    Inherits="SingleWindow_AddNodalOfficer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/custom.js" type="text/javascript"></script>
    <link href="../css/agenda.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table align="center">
        <tr>
            <td>
                Nodal Officer :
            </td>
            <td>
                <div class="cmd-sec">
                    <asp:DropDownList ID="ddlName" runat="server" Width="250px">
                    </asp:DropDownList>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <p></p>
            </td>
           
        </tr>
        <tr>
            <td>
                Remark :
            </td>
            <td>
                <asp:TextBox ID="txtRemark" runat="server" Width="250px" TextMode="MultiLine" Rows="4"
                    MaxLength="500" onkeyup="return TextCounter('txtRemark','lblFAQ',500)"> </asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FEtxtRemark" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,custom"
                    ValidChars=" /.,()&-:_/?%" TargetControlID="txtRemark" InvalidChars="'*|">
                </cc1:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                (Maximum <strong class="text-danger">
                    <asp:Label ID="lblFAQ" runat="server" Text="500"></asp:Label></strong> Characters
                Remaining)
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-success" Text="Allocate"
                    OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ddlName").focus();

            $("#btnSubmit").click(function (e) {

                debugger;
                if (!ValidateDropdown('ddlName', 'Nodal Office Name')) {
                    return false;
                };

                if (!BlankTextBox('txtRemark', 'Remark')) {
                    return false;
                };

            });
        });

        function TextCounter(ctlTxtName, lblCouter, numTextSize) {
            var txtName = document.getElementById(ctlTxtName).value;
            var txtNameLength = txtName.length;
            if (parseInt(txtNameLength) > parseInt(numTextSize)) {
                var txtMaxTextSize = txtName.substr(0, numTextSize)
                document.getElementById(ctlTxtName).value = txtMaxTextSize;
                alert("Entered Text Exceeds '" + numTextSize + "' Characters.");
                document.getElementById(lblCouter).innerHTML = 0;
                return false;
            }
            else {
                document.getElementById(lblCouter).innerHTML = parseInt(numTextSize) - parseInt(txtNameLength);
                return true;
            }
        }
    </script>
</body>
</html>
