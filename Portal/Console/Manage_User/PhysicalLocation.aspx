<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Manage_User_PhysicalLocation"
    CodeBehind="PhysicalLocation.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Menu_Manage/AdminConsoleNavigation.ascx" TagName="Navigation"
    TagPrefix="uc4" %>
<%@ Register Src="~/Console/Includes/Admin_Console_Header.ascx" TagName="Header"
    TagPrefix="hdr" %>
<%@ Register Src="~/Console/Includes/AdminConsoleLeftMenu.ascx" TagName="LeftMenu"
    TagPrefix="lm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Admin Console::Physical Location</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.02)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.02)" />
    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/ajax.js" type="text/javascript"></script>

    <link href="../style/default.css" rel="stylesheet" type="text/css" />

    <script src="../jscript48/Validator.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function validation(btnId) {

            if (!blankFieldValidation('TabLevelDetails_TabCreateLevel_txtLocation', 'Location')) {
                return false;
            }
            if (!chkSingleQuote('TabLevelDetails_TabCreateLevel_txtLocation')) {
                return false;
            }
            if (!WhiteSpaceValidation1st('TabLevelDetails_TabCreateLevel_txtLocation')) {
                return false;
            }
            if (!SpecialCharcheck('TabLevelDetails_TabCreateLevel_txtLocation')) {
                return false;
            }
            if (!DropDownValidation('TabLevelDetails_TabCreateLevel_ddlCountry', 'Country')) {
                return false;
            }
            if (!DropDownValidation('TabLevelDetails_TabCreateLevel_ddlTimezone', 'Time Zone')) {
                return false;
            }
            return true;
        }
        function ckeckConfirm(buttonId) {
            var btnId = document.getElementById("TabLevelDetails_TabCreateLevel_btnSubmit");
            var result;
            if (!validation()) {
                return false;
            }
            else {
                if (buttonId == 'TabLevelDetails_TabCreateLevel_btnSubmit') {
                    return confirm('Do you want to ' + btnId.value + ' ?');
                }
            }

        }
function ResetCancel(id)
{
    var val=document.getElementById(id).value;
    if(val=="Reset")
    {
        return confirm("Do you want to reset?");
    }
    else{
        return confirm("Do you want to cancel?");
    }
}

        function checkSelect(Buttonid) {

            var result;
            if (!ConfirmCheck('form1')) {

                return false;
            }
            else {
                if (Buttonid == 'TabLevelDetails_TabEditLevel_btnDelete') {
                    result = confirm('Do you want to delete?');
                    if (result == true) {
                        return true;
                    }
                    else {
                        if (!validation()) {
                            return false;
                        }
                        else {
                            return true;
                        }
                    }
                }
            }

        }              
                
               
                
         
    </script>

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="MainArea">
        <hdr:Header ID="header1" runat="server" />
        <div id="MidArea">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td valign="top" id="LeftPannel">
                        <lm:LeftMenu ID="lm1" runat="server" />
                    </td>
                    <td valign="top" class="midRightArea">
                        <div id="container">
                            <uc4:Navigation ID="Navigation1" runat="server" />
                            <cc1:TabContainer ID="TabLevelDetails" runat="server" Width="100%" ActiveTabIndex="1"
                                CssClass="ajax__tab_yuitabview-theme" AutoPostBack="True" OnActiveTabChanged="TabLevelDetails_ActiveTabChanged">
                                <cc1:TabPanel runat="server" HeaderText="CREATE" ID="TabCreateLevel" TabIndex="0">
                                    <ContentTemplate>
                                        <div class="mandatory">
                                            (* indicates mandatory fields)</div>
                                        <div class="nodata">
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label></div>
                                        <div class="addTable">
                                            <table id="td" width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="125px">
                                                        Location
                                                    </td>
                                                    <td width="5px">
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtLocation" runat="server" MaxLength="50" Width="200px"></asp:TextBox>&nbsp;<font
                                                            color="#FF0000">*</font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="125px">
                                                        Country
                                                    </td>
                                                    <td width="5px">
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCountry" runat="server" Width="208px">
                                                        </asp:DropDownList>
                                                        &nbsp;<font color="#FF0000">*</font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Time Zone
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlTimezone" runat="server" Width="210px" Text='<%#Eval("LocDiff")%>'>
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="+04:00:00">(GMT +4:00) Abu Dhabi, Muscat, Baku, Tbilisi</asp:ListItem>
                                                            <asp:ListItem Value="+09:30:00">(GMT +9:30) Adelaide, Darwin</asp:ListItem>
                                                            <asp:ListItem Value="+06:00:00">(GMT +6:00) Almaty, Dhaka, Colombo</asp:ListItem>
                                                            <asp:ListItem Value="-09:00:00">(GMT -9:00) Alaska</asp:ListItem>
                                                            <asp:ListItem Value="-04:00:00">(GMT -4:00) Atlantic Time (Canada), Caracas, La Paz</asp:ListItem>
                                                            <asp:ListItem Value="+12:00:00">(GMT +12:00) Auckland, Wellington, Fiji, Kamchatka</asp:ListItem>
                                                            <asp:ListItem Value="-01:00:00">(GMT -1:00 hour) Azores, Cape Verde Islands</asp:ListItem>
                                                            <asp:ListItem Value="+07:00:00">(GMT +7:00) Bangkok, Hanoi, Jakarta</asp:ListItem>
                                                            <asp:ListItem Value="+03:00:00">(GMT +3:00) Baghdad, Riyadh, Moscow, St. Petersburg</asp:ListItem>
                                                            <asp:ListItem Value="+08:00:00">(GMT +8:00) Beijing, Perth, Singapore, Hong Kong</asp:ListItem>
                                                            <asp:ListItem Value="+05:30:00">(GMT +5:30) Bombay, Calcutta, Madras, New Delhi</asp:ListItem>
                                                            <asp:ListItem Value="-03:00:00">(GMT -3:00) Brazil, Buenos Aires, Georgetown</asp:ListItem>
                                                            <asp:ListItem Value="+01:00:00">(GMT +1:00 hour) Brussels, Copenhagen, Madrid, Paris</asp:ListItem>
                                                            <asp:ListItem Value="-06:00:00">(GMT -6:00) Central Time (US &amp; Canada), Mexico City</asp:ListItem>
                                                            <asp:ListItem Value="-05:00:00">(GMT -5:00) Eastern Time (US &amp; Canada), Bogota, Lima</asp:ListItem>
                                                            <asp:ListItem Value="+10:00:00">(GMT +10:00) Eastern Australia, Guam, Vladivostok</asp:ListItem>
                                                            <asp:ListItem Value="+05:00:00">(GMT +5:00) Ekaterinburg, Islamabad, Karachi, Tashkent</asp:ListItem>
                                                            <asp:ListItem Value="-12:00:00">(GMT -12:00) Eniwetok, Kwajalein</asp:ListItem>
                                                            <asp:ListItem Value="-10:00:00">(GMT -10:00) Hawaii</asp:ListItem>
                                                            <asp:ListItem Value="+04:30:00">(GMT +4:30) Kabul</asp:ListItem>
                                                            <asp:ListItem Value="+02:00:00">(GMT +2:00) Kaliningrad, South Africa</asp:ListItem>
                                                            <asp:ListItem Value="+05:45:00">(GMT +5:45) Kathmandu</asp:ListItem>
                                                            <asp:ListItem Value="+11:00:00">(GMT +11:00) Magadan, Solomon Islands, New Caledonia</asp:ListItem>
                                                            <asp:ListItem Value="-11:00:00">(GMT -11:00) Midway Island, Samoa</asp:ListItem>
                                                            <asp:ListItem Value="-02:00:00">(GMT -2:00) Mid-Atlantic</asp:ListItem>
                                                            <asp:ListItem Value="-07:00:00">(GMT -7:00) Mountain Time (US &amp; Canada)</asp:ListItem>
                                                            <asp:ListItem Value="-03:30:00">(GMT -3:30) Newfoundland</asp:ListItem>
                                                            <asp:ListItem Value="-08:00:00">(GMT -8:00) Pacific Time (US &amp; Canada)</asp:ListItem>
                                                            <asp:ListItem Value="+03:30:00">(GMT +3:30) Tehran</asp:ListItem>
                                                            <asp:ListItem Value="+09:00:00">(GMT +9:00) Tokyo, Seoul, Osaka, Sapporo, Yakutsk</asp:ListItem>
                                                            <asp:ListItem Value="00:00:00">(GMT) Western Europe Time, London, Lisbon, Casablanca</asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;<font color="#FF0000">*</font>
                                                    </td>
                                                </tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click1"
                                                        OnClientClick="return  ckeckConfirm('TabLevelDetails_TabCreateLevel_btnSubmit');"
                                                        Width="70px" />
                                                    <asp:Button ID="btnReset" OnClientClick="return  ResetCancel('TabLevelDetails_TabCreateLevel_btnReset');"
                                                        runat="server" Text="Reset" OnClick="btnReset_Click" Width="70px" />
                                                </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel runat="server" HeaderText="VIEW" ID="TabEditLevel" TabIndex="1">
                                    <ContentTemplate>
                                        <div style="margin-right: 7px; height: 20px">
                                            <table border="0" align="right">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkBtnAll" Visible="False" runat="server" Text="All" OnClick="lnkBtnAll_Click"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPaging" Visible="False" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="viewTable">
                                            <asp:GridView runat="server" ID="grd" AutoGenerateColumns="False" AllowPaging="True"
                                                DataKeyNames="PhysicalLocationID" OnRowDataBound="grd_RowDataBound" EmptyDataText="No Records Found"
                                                OnPageIndexChanging="grd_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <input type="checkbox" name="chkAll" value="chkAll" onclick="SelectAll(chkAll,'grd','form1')" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="return deSelectHeader(chkAll,'grd','form1')" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Sl No." DataField="Rowno" />
                                                    <asp:BoundField HeaderText="Physical Location" DataField="PhysicalLocationName" />
                                                    <asp:BoundField HeaderText="Country" DataField="PhysicalLocationCountryName" />
                                                    <asp:BoundField HeaderText="Time Zone" DataField="LocDiff" />
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Edit
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hypEdit" ImageUrl="~/Console/images/editIcon.gif" runat="server"></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle Font-Bold="True" />
                                                <PagerStyle CssClass="paging" />
                                            </asp:GridView>
                                            <div style="margin-left: 10px; margin-top: 10px">
                                                <asp:Button ID="btnDelete" OnClientClick="return checkSelect('TabLevelDetails_TabEditLevel_btnDelete');"
                                                    runat="server" Text="Delete" OnClick="btnDelete_Click" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <!--#include file="../Includes/footer.aspx" -->
    </div>
    </form>
</body>
</html>
