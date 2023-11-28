// JScript File

function convertDate(dt)
			{
				var	strTemp="";
				var	strChar;
				var	date1 = new Array(3);
				var	j=0;
				var strDateTo=dt;
				var todatelen=strDateTo.length;
			
				for(var i=0;i<=todatelen;i++)
				{
					strChar=strDateTo.charAt(i);
					if (strChar=='-' || strChar=='')
					{
					    date1[j]=strTemp;
						strTemp="";
						j=j+1;
					}
					else
					{
						strTemp=strTemp+strChar;
					}
				}
				
					if (date1[1]=='Jan')
					{
						date1[1]=01;
					}
					else if (date1[1]=='Feb')
					{
						date1[1]=02;
					}
					else if (date1[1]=='Mar')
					{
						date1[1]=03;
					}
					else if (date1[1]=='Apr')
					{
						date1[1]=04;
					}
					else if (date1[1]=='May')
					{
						date1[1]=05;
					}
					else if (date1[1]=='Jun')
					{
						date1[1]=06;
					}
					else if (date1[1]=='Jul')
					{
						date1[1]=07;
					}
					else if (date1[1]=='Aug')
					{
						date1[1]=08;
					}
					else if (date1[1]=='Sep')
					{
						date1[1]=09;
					}
					else if (date1[1]=='Oct')
					{
						date1[1]=10;
					}
					else if (date1[1]=='Nov')
					{
						date1[1]=11;
					}
					else if (date1[1]=='Dec')
					{
						date1[1]=12;
					}
				var	conDate=date1[1]+"/"+date1[0]+"/"+date1[2];
				return(conDate);
			
			}