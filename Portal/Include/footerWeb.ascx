<%@ Control Language="C#" AutoEventWireup="true" CodeFile="footerWeb.ascx.cs" Inherits="Include_footerWeb" %>

<div class="footer">
        <div class="Tempwidth">
            <div class="footerLft">
                
                <% if (Request.QueryString["lang"] == "hindi")%>
                <%{%>
                <a href="Index.aspx?GL=home">&#2361;&#2379;&#2350;</a><a href="Website/Aboutus.aspx?GL=aboutus">
                    &#2361;&#2350;&#2366;&#2352;&#2375; &#2348;&#2366;&#2352;&#2375; &#2350;&#2375;&#2306;</a><a href="HowitWorks.aspx?GL=3">&#2351;&#2361; &#2325;&#2376;&#2360;&#2375;  &#2325;&#2366;&#2350; &#2325;&#2352;&#2340;&#2366; &#2361;&#2376;</a><a href="Website/ViewComplaintType.aspx?GL=Complaint">&#2358;&#2367;&#2325;&#2366;&#2351;&#2340;</a><a href="Website/Faq.aspx?GL=faq" style="border-right: 0px;">&#2309;&#2325;&#2381;&#2360;&#2352; &#2346;&#2370;&#2331;&#2375; &#2332;&#2366;&#2344;&#2375; &#2357;&#2366;&#2354;&#2375; &#2346;&#2381;&#2352;&#2358;&#2381;&#2344;</a><a href="#">&#2360;&#2306;&#2346;&#2352;&#2381;&#2325; &#2325;&#2352;&#2375;&#2306;</a> 
                     <%}%>
                <% else %>
                <%{%>
                <a href="Index.aspx?GL=home">Home</a><a href="Aboutus.aspx?GL=2"> About Us</a><a href="HowitWorks.aspx?GL=3">How it Works</a><a href="ViewComplaintType.aspx?GL=4">Complaints</a><a href="Faq.aspx?GL=5">FAQ</a><a href="Contact.aspx?GL=5" style="border-right: 0px;">Contact</a><div class="clearfix"></div>
                <%}%>
                <div class="smlDN">
                    Copyright Reserved @ 2015. All rights Reserved</div>
            </div>
            <div class="footerRht">
               <div id="folwUs">Follow us on <a href="#" title="Follow us on Facebook">
                    <img src="Images/FBIcon.png" width="24" height="24" border="0" align="absmiddle" />
                     </a> <a href="#" title="Follow us on Twitter"><img src="Images/Twitter.png"
                        width="24" height="24" border="0" align="absmiddle" /></a></div>
                       <%-- <div class="visit">
                    Visitors &nbsp; : &nbsp; <strong><span id="strHitCount"></span></strong>
                </div>--%>
               <%-- <div align="right" style="margin-top: 8px;">Powered by &nbsp;&nbsp;<a href="http://csmpl.com" target="_blank" title="CSM Technologies"><img src="Images/CSMLogoWHt.png"
                        width="97" height="33" border="0" align="absmiddle" /></a></div>--%>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <div id="gkPopupLogin">
        <div class="gkPopupWrap">
            <div class="loghd">
                <img src="Images/officer.gif" width="25" height="25" align="absmiddle" />
                OFFICER LOGIN</div>
            <% if (Request.QueryString["lang"] == "hindi")%>
            <%{%>
            <form id="form2" runat="server" name="" method="post" action="OfficersPortal/GrievanceStatus.aspx?lang=hindi">
            <div class="logArea">
                <%--<input id="rdOff" type="radio" name="Off" value="Off" />
                <input id="rdls" type="radio" name="ls" value="ls"  />--%>
                <asp:RadioButton ID="RadioButton1" runat="server" />
                <label>
                    User Name</label>
                <input name="txtUsername" type="text" class="loginput" onfocus="{this.className='loginputactive';if (this.value == 'User Name') {this.value = '';}}"
                    onblur="{this.className='loginput';if (this.value == '') {this.value = 'User Name';}} "
                    maxlength="10" value="User Name" />
                <div class="spacer">
                </div>
                <label>
                    Password</label>
                <input name="txtPassword" type="password" class="loginput" onfocus="{this.className='loginputactive';if (this.value == 'Password') {this.value = '';}}"
                    onblur="{this.className='loginput';if (this.value == '') {this.value = 'Password';}}"
                    maxlength="10" value="Password" />
            </div>
            <div class="logfooter">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <img src="Images/help.gif" width="22" height="22" align="absmiddle" />
                            <a href="OfficersPortal/ForgetPasswordOfficer.aspx" class="forgot">Forgot Password ?</a>
                        </td>
                        <td align="right">
                            <input name="btnSubmit" type="submit" value="Log In" class="logbtn" onclick="return RedirectOfficerLogin();" />
                        </td>
                    </tr>
                </table>
            </div>
            </form>
            <%}%>
            <% else %>
            <%{%>
            <form id="form1" name="" method="post" action="OfficersPortal/GrievanceStatus.aspx">
            <div class="logArea">
                <label>
                    User Name</label>
                <input name="txtUsername" type="text" class="loginput" onfocus="{this.className='loginputactive';if (this.value == 'User Name') {this.value = '';}}"
                    onblur="{this.className='loginput';if (this.value == '') {this.value = 'User Name';}} "
                    maxlength="10" value="User Name" />
                <div class="spacer">
                </div>
                <label>
                    Password</label>
                <input name="txtPassword" type="password" class="loginput" onfocus="{this.className='loginputactive';if (this.value == 'Password') {this.value = '';}}"
                    onblur="{this.className='loginput';if (this.value == '') {this.value = 'Password';}}"
                    maxlength="10" value="Password" />
            </div>
            <div class="logfooter">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <img src="Images/help.gif" width="22" height="22" align="absmiddle" />
                            <a href="OfficersPortal/ForgetPasswordOfficer.aspx" class="forgot">Forgot Password ?</a>
                        </td>
                        <td align="right">
                            <input name="btnSubmit" type="submit" value="Log In" class="logbtn" onclick="return RedirectOfficerLogin();" />
                        </td>
                    </tr>
                </table>
            </div>
            </form>
            <%}%>
        </div>
    </div>
    <div id="gkPopupOverlay"></div>

    <script type="text/javascript" src="js/jquery-ui.min.js"></script> 
<script type="text/javascript" src="js/bootstrap.min.js"></script> 
<script type="text/javascript" src="js/icheck.min.js"></script> 
<script type="text/javascript" src="js/jquery.mCustomScrollbar.min.js"></script> 
<script type="text/javascript" src="js/scrolltopcontrol.js"></script> 
<script type="text/javascript" src="js/bootstrap-datepicker.js"></script> 
<script type="text/javascript" src="js/owl.carousel.min.js"></script> 
<script type="text/javascript" src="js/daterangepicker.js"></script> 