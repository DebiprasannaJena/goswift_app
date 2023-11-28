<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="printpage.ascx.cs" Inherits="KwantifyportalV5._1.Console.Includes.printpage" %>

<div style="float:right; display: none;"  id="printIcon" class="Printr"><a href="javascript:PrintPage();void(0)"><img src="../images/print_ICON.jpg" title="Print by click on All link &#13;if  paging is there." alt="" style="border:none; height:25px;width:25px" align="absmiddle" /></a>  &nbsp;<a href="javascript:PrintPage();void(0)" style="text-decoration:none" title="Print by click on All link &#13;if  paging is there.">Print</a></div>

<div style="float:right; display: none;" id="backIcon" class="Printr"><img src="../Images/back.gif" align="absmiddle" /> &nbsp;<a href="#" onclick="history.back()" >Back</a></div>

<div style="float:right; display:none;  margin-left:5px; margin-top:5px;" class="mandatory" id="indicate"><img src="../Images/info.gif"  align="absmiddle" />* indicates mandatory field</div>
                                    
<script language="javascript" type="text/javascript">
if(printMe=="yes")
	{
		document.getElementById('printIcon').style.display='block'
	}
if(backMe=="yes")
	{
		document.getElementById('backIcon').style.display='block'
	}
	
if(indicate=="yes")
	{
		document.getElementById('indicate').style.display='block'
	}
</script>


