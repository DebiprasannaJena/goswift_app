
var dayarray=new Array("Sun","Mon","Tue","Wed","Thu","Fri","Sat")
var montharray=new Array("01","02","03","04","05","06","07","08","09","10","11","12")

function getthedate(){
var mydate=new Date()
var year=mydate.getYear()
if (year < 1000)
year+=1900
var month=mydate.getMonth()
var day=mydate.getDay()
var daym=mydate.getDate()
if (daym<10)
daym="0"+daym
var hours=mydate.getHours()
var minutes=mydate.getMinutes()
var seconds=mydate.getSeconds()
var dn="AM"
if (hours>=12)
dn="PM"
if (hours>12){
hours=hours-12
}
if (hours==0)
hours=12
if (minutes<=9)
minutes="0"+minutes
if (seconds<=9)
seconds="0"+seconds
//change font size here
var cdate="<small><font color='000000' face='Arial' font size='2pt'>"+hours+":"+minutes+":"+seconds+" "+dn
+" "+dayarray[day]+", "+montharray[month]+"/ "+daym+"/ "+year+" </font></small>"
if (document.all)
document.all.clock.innerHTML=cdate
else if (document.getElementById)
document.getElementById("clock").innerHTML=cdate
else
document.write(cdate)
}
if (!document.all&&!document.getElementById)
getthedate()
function goforit(){
if (document.all||document.getElementById)
setInterval("getthedate()",1000)
}


<!--

function startclock()
{

var thetime=new Date();

var nhours=thetime.getHours();
var nmins=thetime.getMinutes();
var nsecn=thetime.getSeconds();
var nday=thetime.getDay();
var nmonth=thetime.getMonth();
var ntoday=thetime.getDate();
var nyear=thetime.getYear();
var AorP=" ";

if (nhours>=12)
    AorP="PM";
else
    AorP="AM";

if (nhours>=13)
    nhours-=12;

if (nhours==0)
   nhours=12;

if (nsecn<10)
 nsecn="0"+nsecn;

if (nmins<10)
 nmins="0"+nmins;

if (nday==0)
  nday="Sunday";
if (nday==1)
  nday="Monday";
if (nday==2)
  nday="Tuesday";
if (nday==3)
  nday="Wednesday";
if (nday==4)
  nday="Thursday";
if (nday==5)
  nday="Friday";
if (nday==6)
  nday="Saturday";

nmonth+=1;

if (nyear<=99)
  nyear= "19"+nyear;

if ((nyear>99) && (nyear<2000))
 nyear+=1900;
			var monthnameShort	=	new Array("","Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec")
			var monthnameFull	=	new Array("","January","February","March","April","May","June","July","August","September","October","November","December")
//document.clockform.clockspot.value=nhours+": "+nmins+": "+nsecn+" "+AorP+" "+nday+", "+ntoday+" - "+monthnameFull[nmonth]+" - "+nyear;
//document.getElementById("clock").style.innerHTML	=	nhours+": "+nmins+": "+nsecn+" "+AorP+" "+nday+", "+ntoday+" - "+monthnameFull[nmonth]+" - "+nyear;
//document.getElementById("clock").innerHTML	=	"<div align='center'><font color='#000000'>"+nday+", "+monthnameFull[nmonth]+" "+ntoday+", "+nyear+" "+nhours+": "+nmins+": "+nsecn+" "+AorP+"</font></div>";
document.getElementById("clock").innerHTML	=	"<div align='center'><font color='#000000'>"+monthnameShort[nmonth]+" "+ntoday+", "+nyear+" "+nhours+":"+nmins+":"+nsecn+" "+AorP+"</font></div>";

setTimeout('startclock()',1000);

} 
//-->
