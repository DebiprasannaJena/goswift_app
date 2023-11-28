<%@ Control Language="C#" AutoEventWireup="true" Inherits="TopHeader" CodeBehind="TopHeader.ascx.cs" %>
<style type="text/css">
    .topImgStyle
    {
        border: solid 3px #dbdbdb;
        width: 70px;
        height: 50px;
    }
</style>

<script type="text/javascript" language="javascript">

    var dayarray = new Array("Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat")
    var montharray = new Array("01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12")

    function getthedate() {
        var mydate = new Date()
        var year = mydate.getYear()
        if (year < 1000)
            year += 1900
        var month = mydate.getMonth()
        var day = mydate.getDay()
        var daym = mydate.getDate()
        if (daym < 10)
            daym = "0" + daym
        var hours = mydate.getHours()
        var minutes = mydate.getMinutes()
        var seconds = mydate.getSeconds()
        var dn = "AM"
        if (hours >= 12)
            dn = "PM"
        if (hours > 12) {
            hours = hours - 12
        }
        if (hours == 0)
            hours = 12
        if (minutes <= 9)
            minutes = "0" + minutes
        if (seconds <= 9)
            seconds = "0" + seconds
         var cdate = "<small><font  face='Arial' font size='2pt'>" + hours + ":" + minutes + ":" + seconds + " " + dn
+ " " + dayarray[day] + ", " + montharray[month] + "/ " + daym + "/ " + year + " </font></small>"
        if (document.all)
            document.all.clock.innerHTML = cdate
        else if (document.getElementById)
            document.getElementById("clock").innerHTML = cdate
        else
            document.write(cdate)
    }
    if (!document.all && !document.getElementById)
        getthedate()
    function goforit() {
        if (document.all || document.getElementById)
            setInterval("getthedate()", 1000)
    }

</script>

<script type="text/javascript" language="JavaScript">


    function startclock() {

        var thetime = new Date();

        var nhours = thetime.getHours();
        var nmins = thetime.getMinutes();
        var nsecn = thetime.getSeconds();
        var nday = thetime.getDay();
        var nmonth = thetime.getMonth();
        var ntoday = thetime.getDate();
        var nyear = thetime.getYear();
        var AorP = " ";

        if (nhours >= 12)
            AorP = "PM.";
        else
            AorP = "AM.";

        if (nhours >= 13)
            nhours -= 12;

        if (nhours == 0)
            nhours = 12;

        if (nsecn < 10)
            nsecn = "0" + nsecn;

        if (nmins < 10)
            nmins = "0" + nmins;

        if (nday == 0)
            nday = "Sunday";
        if (nday == 1)
            nday = "Monday";
        if (nday == 2)
            nday = "Tuesday";
        if (nday == 3)
            nday = "Wednesday";
        if (nday == 4)
            nday = "Thursday";
        if (nday == 5)
            nday = "Friday";
        if (nday == 6)
            nday = "Saturday";

        nmonth += 1;

        if (nyear <= 99)
            nyear = "19" + nyear;

        if ((nyear > 99) && (nyear < 2000))
            nyear += 1900;
        var monthnameShort = new Array("", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec")
        var monthnameFull = new Array("", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December")
        document.getElementById('clock').innerHTML = "" + nday + ", " + monthnameFull[nmonth] + " " + ntoday + ", " + nyear + "  &nbsp;" + nhours + ": " + nmins + " " + AorP + "&nbsp;&nbsp;";
        setTimeout('startclock()', 1000);

    }

    function resetPhotoHome(iHeightMax, iWidthMax, imgId) {

        var iHeight;
        var iWidth;

        iHeight = document.getElementById(imgId).height;
        iWidth = document.getElementById(imgId).width;
        if (iHeight > iHeightMax) {
            document.getElementById(imgId).height = iHeightMax;
        }

    }
    function sendrequest()
    {
       
        if (window.location.href.indexOf("id") != -1) {
            
        }
        else {

        }
    }

</script>

<script type="text/javascript" src="Scripts/D2DCommonjs.js"></script>

<link href="style/default.css" rel="stylesheet" type="text/css" />
<div class="topBgColor">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td valign="top" style="padding-left: 10px; padding-top: 15px;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="left">
                                        <asp:Image ID="topImgComp" Visible="false" CssClass="topImgStyle" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblCompName" runat="server" Text="Ipicol Portal" 
                                            Font-Names="Verdana" ForeColor="White" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <table border="0" align="right" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right">
                                        <table border="0" align="right" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="9">
                                                    <img src="images/topmenurightcurve.JPG" width="9" alt="" height="29" />
                                                </td>
                                                <td>
                                                <a  id="A1" class="itemTextnew" runat="server"  target=_blank href="~/ChangePassword.aspx">Change Password</a>
                                                    <div class="itemTextnew">
                                                        &nbsp;|&nbsp;</div>
                                                    <a href="#" class="itemTextnew" target="main">Profile Check</a>
                                                    <%if (strShowAdmin == "true")%>
                                                    <%{ %>
                                                    <div class="itemTextnew">
                                                        &nbsp;|&nbsp;</div>
                                                    <a  href="/swp_portal/Console/AdminDefault.aspx?dwXb=<%=Session["RandomNo"]%>" target="_parent"
                                                        class="itemTextnew">Admin Console</a>
                                                    <%}%>
                                                    <div class="itemTextnew">
                                                        &nbsp;|&nbsp;</div>
                                                    <asp:LinkButton ID="logout" runat="server" Text="Logout" OnClick="logout_Click" CssClass="itemTextnew"> </asp:LinkButton>
                                                </td>
                                                <td width="9">
                                                    <img src="images/topmenurightcurve.JPG" alt="" width="9" height="29" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="25" align="right">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td height="5" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-right: 10px;">
                                        <table border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td valign="top">
                                                    <table border="0" align="right" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="right">
                                                                <%if (intTemplateId >= 1)%>
                                                                <%{ %>
                                                                <span class="headerWelcomeMsgnew">Welcome
                                                                    <%=strfullname%>
                                                                </span><%} %><%else%><%{ %><font color="#FFFFFF">Welcome
                                                                    <%=strfullname%>
                                                                </font>
                                                                <%}%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="subSubHeader">
                                                                    <%if (strCFLL1Name != "")%>
                                                                    <%{ %>
                                                                    <%=strCFLL1Name%>
                                                                    <%} %>
                                                                    ,&nbsp;
                                                                    <%if (strCFLL2Name.Trim() != "")%>
                                                                    <%{ %>
                                                                    <%=strCFLL2Name%>
                                                                    &nbsp;
                                                                    <%} %>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="6">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <%if (intTemplateId >= 1)%>
                                                                <%{ %>
                                                                <div align="right" id="clock" class="subSubHeader">
                                                                </div>
                                                                <%} %>
                                                                <%else%>
                                                                <%{ %>
                                                                <font color="<%=strdateColor.Trim()%>">
                                                                    <div align="right" id="Div1">
                                                                    </div>
                                                                </font>
                                                                <%} %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="7">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td height="2" colspan="2">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script language="javascript" type="Text/javascript">
        startclock();
    </script>

</div>
