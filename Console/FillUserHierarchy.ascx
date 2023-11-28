<%@ Control Language="C#" AutoEventWireup="true" Inherits="Admin_FillUserHierarchy"
    CodeBehind="FillUserHierarchy.ascx.cs" %>
<style type="text/css">
    .style1
    {
        width: 148px;
        float: left;
    }
</style>

<script language="javascript" type="text/javascript">

    window.onload = function() {
         var sessionlvl = '<%= Manage_Usercontrol_Property.SetpermissionProperties.hidLevel %>';
        if (sessionlvl) {
            if (document.forms[0][sessionlvl] != null) {
                if (sessionlvl != '') {

                     var ctlid = document.getElementById('<%= Manage_Usercontrol_Property.SetpermissionProperties.hidLevel %>');
                    if (ctlid.id.indexOf('_shidLevels') > 0) {
                         HideShow(ctlid.value, ctlid.id.substring(0, ctlid.id.indexOf('_shidLevels')), '<%=Manage_Usercontrol_Property.CommonProperties.PId %>');

                    }
                    else {
                         HideShow(ctlid.value, ctlid.id.substring(0, ctlid.id.indexOf('_hidLevel')), '<%=Manage_Usercontrol_Property.CommonProperties.PId %>');
                    }
                }
            }
        }
         var sessionlvls = '<%= Manage_Usercontrol_Property.SetpermissionProperties.hidLevels %>';
        if (sessionlvls) {
            if (document.forms[0][sessionlvls] != null) {
                if (sessionlvls != '') {

                     var ctlid = document.getElementById('<%= Manage_Usercontrol_Property.SetpermissionProperties.hidLevels %>');
                    if (ctlid.id.indexOf('_shidLevels') > 0) {
                         HideShow(ctlid.value, ctlid.id.substring(0, ctlid.id.indexOf('_shidLevels')), '<%=Manage_Usercontrol_Property.CommonProperties.PIds %>');
                    }
                    else {
                         HideShow(ctlid.value, ctlid.id.substring(0, ctlid.id.indexOf('_hidLevel')), '<%=Manage_Usercontrol_Property.CommonProperties.PIds %>');
                    }
                }
            }
        }

    }
    
</script>

<table border="0" cellpadding="0"  cellspacing="0">
    <tr id="tr1" runat="server">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td id="td1" runat="server" class="style1">
                        <asp:Label ID="Labels1" runat="server" Text="Location"></asp:Label>
                        <asp:HiddenField ID="shidLevels" runat="server" />
                        <asp:HiddenField ID="hidIds" runat="server" />
                        <asp:HiddenField ID="hidlstid" runat="server" />
                        <asp:HiddenField ID="hidnval" runat="server" />
                        <asp:HiddenField ID="hidbtnid" runat="server" />
                        <asp:HiddenField ID="hidadmin" runat="server" />
                        <asp:HiddenField ID="hidType" runat="server" />
                        <asp:HiddenField ID="hidUsrTxtIdd" runat="server" />
                    </td>
                    <td>
                          <strong> :</strong>
                    </td>
                    <td style="padding-left: 8px;">
                        <asp:DropDownList ID="sdrplayers0" runat="server" Width="185px">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <font color="#FF0000">*</font>
                        <asp:HiddenField ID="shidIDs0" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr2" runat="server" style="display: none">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="style1">
                        <asp:Label ID="Labels2" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                         <strong> :</strong>
                    </td>
                    <td style="padding-left: 8px;">
                        <asp:DropDownList ID="sdrplayers1" runat="server" Width="185px">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:HiddenField ID="shidIDs1" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr3" runat="server" style="display: none">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="style1">
                        <asp:Label ID="Labels3" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                         <strong> :</strong>
                    </td>
                    <td style="padding-left: 8px;">
                        <asp:DropDownList ID="sdrplayers2" runat="server" Width="185px">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:HiddenField ID="shidIDs2" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr4" runat="server" style="display: none">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="style1">
                        <asp:Label ID="Labels4" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                         <strong> :</strong>
                    </td>
                    <td style="padding-left: 8px;">
                        <asp:DropDownList ID="sdrplayers3" runat="server" Width="185px">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:HiddenField ID="shidIDs3" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr5" runat="server" style="display: none">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="style1">
                        <asp:Label ID="Labels5" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                          <strong> :</strong>
                    </td>
                    <td style="padding-left: 8px;">
                        <asp:DropDownList ID="sdrplayers4" runat="server" Width="185px">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:HiddenField ID="shidIDs4" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr6" runat="server" style="display: none">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="style1">
                        <asp:Label ID="Labels6" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                         <strong> :</strong>
                    </td>
                    <td style="padding-left: 8px;">
                        <asp:DropDownList ID="sdrplayers5" runat="server" Width="185px">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr7" runat="server" style="display: none">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="style1">
                        <asp:Label ID="Labels7" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                         <strong> :</strong>
                    </td>
                    <td style="padding-left: 8px;">
                        <asp:DropDownList ID="sdrplayers6" runat="server" Width="185px">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:HiddenField ID="shidIDs5" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr8" runat="server" style="display: none">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="style1">
                        <asp:Label ID="Labels8" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                          <strong> :</strong>
                    </td>
                    <td style="padding-left: 8px;">
                        <asp:DropDownList ID="sdrplayers7" runat="server" Width="185px">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:HiddenField ID="shidIDs6" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr9" runat="server" style="display: none">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="style1">
                        <asp:Label ID="Labels9" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                         <strong> :</strong>
                    </td>
                    <td style="padding-left: 8px;">
                        <asp:DropDownList ID="sdrplayers8" runat="server" Width="185px">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:HiddenField ID="shidIDs7" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr10" runat="server" style="display: none">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="style1">
                        <asp:Label ID="Labels10" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                         <strong> :</strong>
                    </td>
                    <td style="padding-left: 8px;">
                        <asp:DropDownList ID="sdrplayers9" runat="server" Width="185px">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:HiddenField ID="shidIDs8" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
