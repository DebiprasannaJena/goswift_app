// JavaScript Document
var printMe
var indicate
var backMe

function configTitleBar(){
	viewTable();
	if($('#title')){
		$('#title').html(pageHeader)
	}
	TotLink='';
		if (strFirstLink !='')
			{
				TotLink="&raquo; " + "<b>" + strFirstLink + "</b>"
				}
				if (strLastLink !='')
			{
				TotLink=TotLink + " &raquo; " + "<b>" + strLastLink + "</b>"
				}

$('#navigation').html("<a href='../Common/Dashboard.aspx'>Home</a>" + TotLink + " &raquo; ")
}

//Print Page Function
function PrintPage() {
	if($('#viewTable table').length>0)
	{
		var sOption="menubar=yes,scrollbars=yes,width=750,height=600,left=100,top=25,resizable=yes"; 
		var sWinHTML = $('#viewTable').html(); 
        sWinHTML = sWinHTML .replace(/<a/gi,"<span");
        sWinHTML = sWinHTML .replace(/a>/gi,"span>");
		var winprint=window.open("","",sOption);
		winprint.document.open();
		winprint.document.write('<html><head><link href=../Style/Print.css  rel=Stylesheet><title> '+ pageHeader +' </title></head><body>');
		winprint.document.write('<div style="padding-bottom:10px;" id="header" align="left"> <img src="../Images/logosmall.jpg" align="absmiddle"/> </div>');
		winprint.document.write('<table Width="100%"> ');  
		winprint.document.write('</table>');
		winprint.document.write('<div class="viewTable">');
		winprint.document.write(sWinHTML); 
		winprint.document.write('</div>');
		winprint.document.write('</body></html>'); 
		winprint.document.close(); 
		winprint.focus(); 	
	}
	else{	
		alert("No Print Content available")
		return false;
	}
}

function viewTable()
	{
		$(".viewTable tr:odd").css({backgroundColor:"#fffaf8"})	
		$(".viewTable tr:even").css({backgroundColor:"#ffffff"})	
	
		$(".viewTable tr:odd").not(':first').hover(
		  function () {
			$(this).css("background","#fcf6d6");
		  }, 
		  function () {
			$(this).css("background","#fffaf8");
		  });
		
		$(".viewTable tr:even").not(':first').hover(
		  function () {
			$(this).css("background","#fcf6d6");
		  }, 
		  function () {
			$(this).css("background","#ffffff");
		  });
	}

function menuCtr(){
	$("#MnCtr a").click(function(){
		if($(this).hasClass('close')==false)
			{
				$("#LeftMidArea .lftPnl:eq(0)").hide()
				$("#LeftMidArea").animate({width: "18px"}, "fast" );
				$("#LeftMidArea .heading").text("")
				$("#MnCtr").animate({marginLeft:"0px"},"fast");
				$(this).addClass("close")
			}
		else
			{											 
				$("#LeftMidArea .lftPnl:eq(0)").fadeIn()
				$("#LeftMidArea").animate({width:"200px"}, "fast");
				$("#LeftMidArea .heading").text("Related Activities")							  
				$("#MnCtr").animate({marginLeft:"182px"},"fast");
				$(this).removeClass("close")
			}
		}
	)
}

//*************** Function for Copy rihgt 
$.fillCopyright = function(selDiv,title)
	{
		var curDate 	= new Date();
		var curYear 	= curDate.getFullYear();
		//var NextYear 	= curYear+1+'';
		//var finYear 	= curYear+"-"+NextYear.substr(2,[3]);
		var copyVal		= "Copyright &copy; "+ "<b>" +curYear + "</b>" +" "+title+", All Rights are Reserved";
		$('#'+selDiv).html(copyVal);
	}
	
//$.fillCopyright = function(selDiv,title)
//	{
//		var curDate 	= new Date();
//		var curYear 	= curDate.getFullYear();
//		var NextYear 	= curYear+1+'';
//		var finYear 	= curYear+"-"+NextYear.substr(2,[3]);
//		var copyVal		= "Copyright &copy; "+finYear+" "+title+", All Rights are Reserved";
//		$('#'+selDiv).html(copyVal);
//	}
