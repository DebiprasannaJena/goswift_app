<%@ Control Language="C#" AutoEventWireup="true" CodeFile="utils.ascx.cs" Inherits="includes_utils" %>
<div class="utils pull-right" style="margin-top:5px;">
    <div style="float: right; margin-left: 5px; margin-top: -0px;" class="text-danger"
        id="indicate">
        <img src="../images/indicates.gif" align="absmiddle" />
        <% if (Session["Language"] == "HINDI")%>
        <%{%>
        <asp:Literal runat="server" ID="Literal2" Text="* अनिवार्य क्षेत्र इंगित करता है"></asp:Literal>
        <%}%>
        <%else%>
        <%{%>
        <asp:Literal runat="server" ID="Literal1" Text="* indicate mandatory field"></asp:Literal>
        <%}%>
    </div>
    <a href="javascript:PrintPage();void(0)" id="printIcon" class="btn btn-primary" title="Print"
        data-toggle="tooltip" data-placement="top"><i class="fa fa-print"></i></a>
        
        <a href="javascript:void(0)"
            id="refreshIcon" class="btn btn-small" title="Refresh" data-toggle="tooltip"
            data-placement="top"><i class="icon-refresh"></i></a><a href="javascript:void(0)"
                id="deleteIcon" class="btn btn-small" title="Delete" data-toggle="tooltip" data-placement="top">
                <i class="icon-trash"></i></a><a href="javascript:void(0)" id="backIcon" class="btn btn-small"
                    title="Back" data-toggle="tooltip" data-placement="top" onclick="goBack()"><i class="icon-arrow-left">
                    </i></a>
</div>
<div class="clear">
</div>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        $("#printIcon").tooltip();
        $("#backIcon").tooltip();
        $("#refreshIcon").tooltip();
        $("#deleteIcon").tooltip();
    });

    $("#printIcon").hide();
    $("#backIcon").hide();
    $("#refreshIcon").hide();
    $("#deleteIcon").hide();
    $("#indicate").hide();

    if (printMe == "yes") {
        $('#printIcon').css('display', 'block');
    }
    if (backMe == "yes") {
        $('#backIcon').css('display', 'block');
    }

    if (indicate == "yes") {
        $('#indicate').css('display', 'block');
    }
    function goBack() {
        window.history.back()
    }	
</script>
