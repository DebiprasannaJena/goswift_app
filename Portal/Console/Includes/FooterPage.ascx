<%--
 '*******************************************************************************************************************
     ' File Name         : FooterPage.ascx  
     ' Description       : Footer Page
     'Created by         : Amrita Nayak
     'Created On         : 6th-July-2010
     'Modification History:

     '                        <CR no.>                      <Date>             <Modified by>        <Modification Summary>'                                                         
     '
     '                          
     ' PDK Function Name :   
     ' Include files     :           
     ' Style sheet       :

' *******************************************************************************************************************
--%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="FooterPage" CodeBehind="FooterPage.ascx.cs" %>
<table height="18" width="90%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td class="whiteText" style="height: 19px" align="right">
            <div style=" float:left; text-align:left">
                <asp:Label ID="lblFooter" runat="server" Text="Label"></asp:Label></div>
            <div>
                Visit : <a href="<%=CompanyURL%>" target="_blank" class="whiteText" style="font-weight: bold;">
                    <%=CompanyURL%></a></div>
        </td>
    </tr>
</table>
