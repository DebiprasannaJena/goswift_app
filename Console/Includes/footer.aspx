<%@ Register Src="~/Console/Includes/FooterPage.ascx" TagName="footer" TagPrefix="uc1" %>
<style type="text/css">
    .ftQuick
    {
        opacity: 0.65;
        filter: alpha(opacity=65);
    }
    .ftQuick a:hover
    {
        opacity: 0.99;
        filter: alpha(opacity=99);
    }
</style>
<div id="footer">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td align="left" class="ftQuick">
               <%-- Powered by <a href="http://www.kwantify.com" target="_blank">Kwantify</a>--%> 
            </td>
            <td align="right">
               <uc1:footer ID="foot1" runat="server" />
            </td>
        </tr>
    </table>
</div>
