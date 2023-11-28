<%@ Control Language="VB" AutoEventWireup="false" CodeFile="tab.ascx.vb" Inherits="tab" %>

<%
    Dim btn As String = ""
    Dim tab As String = ""
    btn = Request.QueryString("btn")
    tab = Request.QueryString("tab")
%>
<script type="text/javascript" language="javascript">
        var RedURL;


//------------------Creates Button-------------------------//


function configButton(){
		var i;	          
		var strRetVal=$('#Tab1_hdnBtn').val();
		var Arr=new Array();
		Arr=strRetVal.split('|'); 
		var Btn=Arr[0].split(','); 
		RedURL=Btn[0]; 
		<%
			if (btn="") then
		%>
			window.location.href=RedURL;
		<%
		   end if
		%>
			var MyButton	= '<div class="pull-right">';
	            for(i=0;i<Arr.length-1;i++){
	                    var Vals=Arr[i].split(',');
						var buttonAactive	= (Vals[1]=='<%=btn%>')?'btn btn-default':'btn btn-warning';
			            MyButton +='&nbsp;<a href='+Vals[0]+' id='+Vals[1]+' class="'+buttonAactive+'">'+Vals[2]+'</a>';
	                }
	         MyButton	+= '</div>';  
			 $('.top-buttons').append(MyButton);
	       }
		   
		   
//-------------------Creates Tabs---------------------------//


function configTab(){
			var i;	          
			var strRetVal=document.getElementById('Tab1_hdnTabVal').value;
			var Arr1=new Array();
			Arr1=strRetVal.split('|');
			var Tab=Arr1[0].split(',');
			<%
				if (btn<>"" and  tab="") then
			%>
				window.location.href+="&tab="+Tab[1];
			<%
				end if
			%>
			var myTab	= '<ul class="nav nav-tabs">';  
			for(i=0;i<Arr1.length-1;i++)
				{
					var Vals=Arr1[i].split(',');
					var tabAactive	= (Vals[1]=='<%=tab%>')?'active':'';
					myTab += '<li id="'+Vals[1]+'" class="'+tabAactive+'">';
					myTab += '<a href='+Vals[0]+'>'+Vals[2]+'</a>';
					myTab += '</li>';
				}
			myTab += '</ul>' ;
			$('#MyTab').append(myTab);
}
 
function ToRedirect()
	    {
	        window.location.href=RedURL;
	        return false;
	    }	
</script>
<asp:HiddenField ID="hdnBtn" runat="server" />
<asp:HiddenField ID="hdnTabVal" runat="server" />
        