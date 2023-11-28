

var printMe
var backMe
document.write('<script language="javascript" src="../scripts/jquery-1.4.1.min.js" TYPE="text/javascript"></SCRIPT>');

function configTitleBar() {
    if (document.getElementById('title')) {
        document.getElementById('title').innerHTML = pageHeader
    }
    //document.getElementById('pageIcon').src=pageIcon

    document.getElementById('navigation').innerHTML = "<a href='WelcomeUser.aspx' class='home'>Home</a> &raquo; " + strFirstLink + " &raquo; " + "<b>" + strLastLink + "</b>"
    //alert()

}



function sldLft() {
    $(".cutMark a").click(function() {

        //$("#LeftMenu").animate( { paddingLeft: padLeft, paddingRight: padRight}, { queue:false, duration:100 } )

    })
}

function loadTopBar() {
    if ($("#topBarBg").is(":hidden")) {
        $("#topBarBg").slideDown("slow");
    }
    
    $("#topBarBg .close").click(function() {
        $("#topBarBg").slideUp();
    }
									  )

}

function menuCtr() {
    $("#MnCtr a").click(function() {

        if ($(this).hasClass('close') == false) {
            $("#LeftMenu ul:eq(0)").hide()
            $("#LeftMenu .heading").text(" ")
            $("#LeftMenu").animate({ width: "5px" }, "fast");
            $("#MnCtr").animate({ marginLeft: "0px" }, "fast");
            $(this).addClass("close")
        }
        else {
            $("#LeftMenu ul:eq(0)").fadeIn()
            $("#LeftMenu").animate({ width: "201px" }, "fast");
            $("#LeftMenu .heading").text("Relate Activities")
            $("#MnCtr").animate({ marginLeft: "184px" }, "fast");
            $(this).removeClass("close")
        }
    }
									  )


}

function PrintPage() {
debugger
    //alert(document.getElementById('printDiv').getElementsByTagName("table")[0])
  
    if (document.getElementById('printDiv').getElementsByTagName("table")[0] == undefined) {
        alert("No Print Content available")
        return false;
    }
       
        var lable1
        var lable2
        var lable3
        var lable4
        if (document.getElementById('lbl1')) {
            lable1 = document.getElementById('lbl1').innerHTML
        }
        if (document.getElementById('lbl2')) {
            lable2 = document.getElementById('lbl2').innerHTML
        }

        if (document.getElementById('lbl3')) {
            lable3 = document.getElementById('lbl3').innerHTML
        }
        if (document.getElementById('lbl4')) {
            lable4 = document.getElementById('lbl4').innerHTML
        }

        var sOption = "menubar=yes,scrollbars=yes,width=750,height=600,left=100,top=25,resizable=yes";
        var sWinHTML = document.getElementById('printDiv').innerHTML;
        var winprint = window.open("", "", sOption);
        winprint.document.open();
        winprint.document.write('<html><head><link href=../style/Print.css  rel=Stylesheet><title> ' + pageHeader + ' </title></head><body>');
        winprint.document.write('<div style="padding-bottom:10px;height:40px" id="header" align="left"> <img src="../images/logo.png" width="230px" height="50px" align="absmiddle"/> </div>');
        winprint.document.write('<table Width="100%"> ');
        if (lable1) {
            winprint.document.write('<strong>' + lable1 + '</strong>' + "</br>");
        }
        if (lable2) {
            winprint.document.write('<strong>' + lable2 + '</strong>' + "</br>");
        }
        if (lable3) {
            winprint.document.write('<strong>' + lable3 + '</strong>' + "</br>");
        }
        if (lable4) {
            winprint.document.write('<strong>' + lable4 + '</strong>' + "</br>");
        }

        winprint.document.write('</table>');
        winprint.document.write('<div class="printDiv">');
        winprint.document.write(sWinHTML);
        winprint.document.write('</div>');
        winprint.document.write('</body></html>');
        winprint.document.close();
        winprint.focus();
     
}
