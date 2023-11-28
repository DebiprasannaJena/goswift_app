<%@ Control Language="C#" AutoEventWireup="true" CodeFile="rightpannel.ascx.cs" Inherits="Application_includes_header" %>


<div class="rightpanel">
    <div class="rightnews bglight-gray ">
                    <h2>
                        News  <a href="News.aspx" title="View All">View All</a>
                    </h2> 
                     <ul>
                      <asp:Repeater ID="RepNews" runat="server">
                     
                        <ItemTemplate>
                          
                                <li><a href="News.aspx?annid=<%#Eval("INT_ID")%>"><span>
                                    <img src= "CMSImageGallery/<%#Eval("VCH_IMAGE")%>" class="img-sec" alt="newsimg" /></span> <%#Eval("VCH_HEADING")%>
                                    <div class="clearfix">
                                    </div>
                                </a></li>
                        </ItemTemplate>
                      
                    </asp:Repeater>
                      </ul>
                </div>
                  <div class="rightannouncements">
                    <h2>
                       Announcements <a href="Announcements.aspx" title="View All">View All</a>
                    </h2>
                     <ul>
                      <asp:Repeater ID="RepAnnouncement" runat="server" DataMember="INT_ID"   OnItemDataBound="RepAnnouncement_OnItemDataBound" >
                        <ItemTemplate>
                           
                                <li id="liNews" runat="server"><a href="Announcements.aspx?annid=<%#Eval("INT_ID")%>">
                                    <div class="date-sec">
                                        <%#Eval("DtmCreatedON")%></span></div>
                                   <span class="listdata"><%#Eval("VCH_CONTENT")%></span><span
                                            class="clearfix"></span></a> </li>
                         
                        </ItemTemplate>
                    </asp:Repeater>   </ul>
                </div>
   <a class="infowizard" href="http://investodisha.org/info-wizard" target="_blank" title="Info Wizard">Info Wizard &nbsp;<i class="fa fa-angle-right"></i></a>
   </div>
  <style>
.rightannouncements ul li.active a .listdata{color:#d61c24!important}
</style>
 