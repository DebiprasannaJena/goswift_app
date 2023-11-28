<%@ Control Language="C#" AutoEventWireup="true" CodeFile="headerWeb.ascx.cs" Inherits="Include_headerWeb" %>

<script type="text/javascript" language="javascript">

    $(document).ready(function () {
        $('#resMenu').click(function () {
            var content = $(".menu").html();
            $('#MenuArea').slideToggle();
            $('#MenuArea').html(content);
        });

        var y = window.innerHeight;
        $('.contBg').css('min-height', y - 250);

    });
        
    </script>
<div id="header">
        <div class="Tempwidth">
            <div id="resMenu"><i class="fa fa-bars"></i></div>
            <div id="MenuArea"> </div>
            <div class="tp_menu">
                
                    <a href="Login.aspx?lang=<%=Request.QueryString["lang"] %>" class="tplnk" target="_blank">
                        <img src="Images/UserIcn.png" width="16px" height="16px" border="0" align="absmiddle" />&nbsp;<% if (Request.QueryString["lang"] == "hindi")%><%{%>&#2346;&#2379;&#2352;&#2381;&#2335;&#2354;
                        &#2354;&#2377;&#2327;&#2311;&#2344;<%}%><%else%><%{%>Portal Login<%}%></a>
                    <% if (Request.QueryString["lang"] == "hindi")%>
                    <%{%>
                    <a href="Contact.aspx?GL=5&lang=hindi" class="tplnk">
                        <img src="Images/ContactPh.png" width="16px" height="16px" border="0" align="absmiddle" />
                        &nbsp;&#2360;&#2306;&#2346;&#2352;&#2381;&#2325; &#2325;&#2352;&#2375;&#2306;</a>
                    <%}%>
                    <%else%>
                    <%{%>
                    <a href="Contact.aspx?GL=5" class="tplnk"><img src="Images/ContactPh.png" width="16px" height="16px" border="0" align="absmiddle" />&nbsp;Contact Us</a>
                    <%}%>
                
                <%--<% if (Request.QueryString["lang"] == "hindi")%>
                <%{%>
                <a href="Index.aspx?lang=hindi" class="active" style=" padding-top:4px; padding-bottom:3px; font-size:14px;">&#2361;&#2367;&#2344;&#2381;&#2342;&#2368;</a><a
                        href="Index.aspx">English</a>
                <%}%>
                <%else%>
                <%{%>
                    <a href="Index.aspx?lang=hindi" style=" padding-top:4px; padding-bottom:3px; font-size:14px;">&#2361;&#2367;&#2344;&#2381;&#2342;&#2368;</a><a
                        href="Index.aspx" class="active">English</a>
                <%}%>--%>
            </div>
            <div class="logo">
                <% if (Request.QueryString["lang"] == "hindi")%>
                <%{%>
                <%--<img src="Images/Logo.jpg"width="530" height="86" alt="Logo" />--%>
                <img src="Images/ICDS_logoN.png"  alt="ICDS Logo" />
                <%}%>
                <%else%>
                <%{%>
                <img src="Images/ICDS_logoN.png"  alt="ICDS Logo" />
                <%}%>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>