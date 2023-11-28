<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuWeb.ascx.cs" Inherits="Include_MenuWeb" %>

<% if (Request.QueryString["lang"] == "hindi")%>
            <%{%>
            <div class="menu">
                <ul class="hindi">
                    <li><a href="Index.aspx?lang=hindi" class="active">&#2361;&#2379;&#2350;</a></li>

                    <% if (Request.QueryString["GL"] == "2")%>
                    <%{%>
                    <li><a href="Aboutus.aspx?GL=2&lang=hindi" class="active">&#2361;&#2350;&#2366;&#2352;&#2375;
                        &#2348;&#2366;&#2352;&#2375; &#2350;&#2375;&#2306;</a></li></li>
                    <%}%>
                    <%else%>
                    <%{%>
                    <li><a href="Aboutus.aspx?GL=2&lang=hindi">&#2361;&#2350;&#2366;&#2352;&#2375;
                        &#2348;&#2366;&#2352;&#2375; &#2350;&#2375;&#2306;</a></li></li>
                    <%}%>

                    <% if (Request.QueryString["GL"] == "3")%>
                    <%{%>
                    <li><a href="HowitWorks.aspx?GL=3&lang=hindi" class="active">&#2351;&#2361; &#2325;&#2376;&#2360;&#2375;
                        &#2325;&#2366;&#2350; &#2325;&#2352;&#2340;&#2366; &#2361;&#2376;</a></li>
                    <%}%>
                    <%else%>
                    <%{%>
                    <li><a href="HowitWorks.aspx?GL=3&lang=hindi">&#2351;&#2361; &#2325;&#2376;&#2360;&#2375;
                        &#2325;&#2366;&#2350; &#2325;&#2352;&#2340;&#2366; &#2361;&#2376;</a></li>
                    <%}%>

                    <% if (Request.QueryString["GL"] == "4")%>
                    <%{%>
                    <li><a href="ViewComplaintType.aspx?GL=4&lang=hindi" class="active">&#2358;&#2367;&#2325;&#2366;&#2351;&#2340;</a></li>
                    <%}%>
                    <%else%>
                    <%{%>
                    <li><a href="ViewComplaintType.aspx?GL=4&lang=hindi">&#2358;&#2367;&#2325;&#2366;&#2351;&#2340;</a></li>
                    <%}%>

                    <% if (Request.QueryString["GL"] == "5")%>
                    <%{%>
                   <li><a href="Faq.aspx?GL=5&lang=hindi" class="active">&#2309;&#2325;&#2381;&#2360;&#2352;
                        &#2346;&#2370;&#2331;&#2375; &#2332;&#2366;&#2344;&#2375; &#2357;&#2366;&#2354;&#2375;
                        &#2346;&#2381;&#2352;&#2358;&#2381;&#2344;</a></li>
                    <%}%>
                    <%else%>
                    <%{%>
                    <li><a href="Faq.aspx?GL=5&lang=hindi" style="border-right: 0px;">&#2309;&#2325;&#2381;&#2360;&#2352;
                        &#2346;&#2370;&#2331;&#2375; &#2332;&#2366;&#2344;&#2375; &#2357;&#2366;&#2354;&#2375;
                        &#2346;&#2381;&#2352;&#2358;&#2381;&#2344;</a></li>
                    <%}%>


                   <%-- <li><a href="Aboutus.aspx?GL=2&lang=hindi">&#2361;&#2350;&#2366;&#2352;&#2375;
                        &#2348;&#2366;&#2352;&#2375; &#2350;&#2375;&#2306;</a></li>
                    <li><a href="HowitWorks.aspx?GL=3&lang=hindi">&#2351;&#2361; &#2325;&#2376;&#2360;&#2375;
                        &#2325;&#2366;&#2350; &#2325;&#2352;&#2340;&#2366; &#2361;&#2376;</a></li>
                    <li><a href="ViewComplaintType.aspx?GL=4&lang=hindi">&#2358;&#2367;&#2325;&#2366;&#2351;&#2340;</a></li>
                    <li><a href="Website/ReportList.aspx?GL=reports&lang=hindi">&#2352;&#2367;&#2346;&#2379;&#2352;&#2381;&#2335;</a></li>
                    <li><a href="Faq.aspx?GL=5&lang=hindi" style="border-right: 0px;">&#2309;&#2325;&#2381;&#2360;&#2352;
                        &#2346;&#2370;&#2331;&#2375; &#2332;&#2366;&#2344;&#2375; &#2357;&#2366;&#2354;&#2375;
                        &#2346;&#2381;&#2352;&#2358;&#2381;&#2344;</a></li>--%>
                   <%-- <li class="lstRht"><a href="#" class="offLogin" id="btnLogin">&#2309;&#2343;&#2367;&#2325;&#2366;&#2352;&#2368;
                                &#2354;&#2377;&#2327;&#2311;&#2344;</a></li>--%>
                </ul>
            </div>
            <%}%>
            <%else%>
            <%{%>
            <div class="menu">
                <ul>
                    <li><a href="Index.aspx">Home</a></li>
                    <% if (Request.QueryString["GL"] == "2")%>
                    <%{%>
                    <li><a href="Aboutus.aspx?GL=2" class="active">About Us</a></li>
                    <%}%>
                    <%else%>
                    <%{%>
                    <li><a href="Aboutus.aspx?GL=2">About Us</a></li>
                    <%}%>

                    <% if (Request.QueryString["GL"] == "3")%>
                    <%{%>
                    <li><a href="HowitWorks.aspx?GL=3" class="active">How it Works</a></li>
                    <%}%>
                    <%else%>
                    <%{%>
                    <li><a href="HowitWorks.aspx?GL=3">How it Works</a></li>
                    <%}%>

                    <% if (Request.QueryString["GL"] == "4")%>
                    <%{%>
                    <li><a href="ViewComplaintType.aspx?GL=4" class="active">Complaints</a></li>
                    <%}%>
                    <%else%>
                    <%{%>
                    <li><a href="ViewComplaintType.aspx?GL=4">Complaints</a></li>
                    <%}%>

                    <% if (Request.QueryString["GL"] == "5")%>
                    <%{%>
                    <li><a href="Faq.aspx?GL=5" class="active">FAQ</a></li>
                    <%}%>
                    <%else%>
                    <%{%>
                    <li><a href="Faq.aspx?GL=5" style="border-right: 0px;">FAQ</a></li>
                    <%}%>
                   <%-- <li class="lstRht"><a href="#" class="offLogin" id="btnLogin">Officer Login</a></li>--%>
                    <div class="clear"></div>
                </ul>
            </div>
            <%}%>