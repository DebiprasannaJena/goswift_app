<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AMSLeftmenupanel.ascx.cs"
    Inherits="includes_AMSLeftmenupanel" %>


<script src="../js/ddaccordion.js" type="text/javascript"></script>
<script type="text/javascript" src="../js/milonic_src.js"></script>
<script src="../js/mmenudom.js" type="text/javascript"></script>

<script type="text/javascript">


    ddaccordion.init({
        headerclass: "submenuheader", //Shared CSS class name of headers group
        contentclass: "submenu", //Shared CSS class name of contents group
        revealtype: "click", //Reveal content when user clicks or onmouseover the header? Valid value: "click" or "mouseover
        collapseprev: true, //Collapse previous content (so only one open at any time)? true/false 
        defaultexpanded: [], //index of content(s) open by default [index1, index2, etc] [] denotes no content
        onemustopen: false, //Specify whether at least one header should be open always (so never all headers closed)
        animatedefault: false, //Should contents open by default be animated into view?
        persiststate: true, //persist state of opened contents within browser session?
        toggleclass: ["", ""], //Two CSS classes to be applied to the header when it's collapsed and expanded, respectively ["class1", "class2"]
        togglehtml: ["suffix", "<img src='../img/mPlus.png' class='statusicon' />", "<img src='../img/mMinus.png' class='statusicon' />"], //Additional HTML added to the header when it's collapsed and expanded, respectively  ["position", "html1", "html2"] (see docs)
        animatespeed: "normal", //speed of animation: "fast", "normal", or "slow"
        oninit: function(headers, expandedindices) { //custom code to run when headers have initalized
            //do nothing
        },
        onopenclose: function(header, index, state, isuseractivated) { //custom code to run whenever a header is opened or closed
            //do nothing
        }
    })	


</script>

<script language="javascript" type="text/javascript">

    $(document).ready(function () {
        menuCtr();
    });
</script>
<div id="MnCtr">
    <a href="#"></a>
</div>
<div class="lftPnl">    
    <asp:Literal ID="litMenuStr" runat="server"></asp:Literal>
</div>
