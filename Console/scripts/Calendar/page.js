  function sortColumn(el) {
  	return false;
  }
  function changePage() {
  	document.writeln("<a href='"+absoluteWebRoot+"link.cfm?display_page="+document.location+"'><b>Load Menu</b></a>");
  }
  
	function openNewWindow(fileName,windowName,theWidth,theHeight,theScroll) {
		if (document.all) {
			showLeft = document.body.clientWidth - theWidth - 40;
		} else {
			showLeft = 	document.width - theWidth - 40;
		}
		window.open(fileName,windowName,"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars="+theScroll+",resizable=0,left="+showLeft+",top=10,width="+theWidth+",height="+theHeight);
	}

	function WidgetToggle(id,widgetName,graphicsRoot) {
		i = eval("document.images."+widgetName+"IconExpand");
		c=document.all[id];
		if(c.style.visibility == "hidden") {
			c.style.visibility = "visible";
			c.style.display='block';
			img = graphicsRoot+"icon_collapse.gif";
			WidgetToggleImg(i,img);
			setTimeout("WidgetToggleImg(i,img);",500);
		} else {
			i.src = graphicsRoot+"icon_expand.gif";
			c.style.visibility = "hidden";
			c.style.display='none';
			//alert(i.src);
			img = graphicsRoot+"icon_expand.gif";
			WidgetToggleImg(i,img);
			setTimeout("WidgetToggleImg(i,img);",500);
		}
	}	
	function WidgetToggleImg(i,img) {
		i.src = img;
	}
function AddFromLookup(frmName,frmField,txt,val) {
	frmFieldObj = eval("document."+frmName+"."+frmField);
	target = frmFieldObj.options.length;
	newOpt = new Option(txt, val, false, false);
	frmFieldObj.options[target]=newOpt;
	frmFieldObj.selectedIndex = target;
	}
function showMenu(e, s, oElement) {
	// find anchor element
	parent.noDisplayElement('OBJECT');parent.noDisplayElement('SELECT');parent.noDisplayElement('IFRAME');
	var el = e.target ? e.target : e.srcElement;
	while (el.tagName != "TD" && el.tagName != "A")
		el = el.parentNode;
	
	// is there already a tooltip? If so, remove it
	if (el._cMenu) {
		document.body.removeChild(el._cMenu);
		el._cMenu = null;
		el.onblur = null;
		displayElement('OBJECT');displayElement('SELECT');displayElement('IFRAME');
		return;
	}

	// create element and insert last into the body
	var d = document.createElement("DIV");
	d.className = "cMenuSub";
	document.body.appendChild(d);
	str = "";
	for (i=0; i<s.length; i++) {
		str += '<a target="'+s[i][2]+'" href="'+s[i][1]+'">'+s[i][0]+'</a><br>';
	}
	str += "";
	d.style.backgroundColor = 'menu';
	d.innerHTML = str;
	
	// Allow clicks on A elements inside tooltip
	d.onmousedown = function (e) {
		if (!e) e = event;
		var t = e.target ? e.target : e.srcElement;
		while (t.tagName != "A" && t != d)
			t = t.parentNode;
		if (t == d) return;
		
		el._onblur = el.onblur;
		el.onblur = null;
	};
	d.onmouseup = function () {
		el.onblur = el._onblur;
		el.focus();
	};

	// position tooltip
	var dw = document.width ? document.width : document.documentElement.offsetWidth - 25;
	var scroll = getScroll();
	if (e.clientX > dw - d.offsetWidth)
		d.style.left = dw - d.offsetWidth + scroll.x + "px";
	else
	   var tmp = oElement;
	   var left = 0;
	   var top = 0;
	   while (tmp != null) {
	      left += tmp.offsetLeft;
	      top += tmp.offsetTop;
	      tmp = tmp.offsetParent;
	   }
		d.style.left = left;
		d.style.top = top + oElement.offsetHeight + 3;
		//d.style.left = e.clientX - 2 + scroll.x + "px";
		//d.style.top = e.clientY + 8 + scroll.y + "px";

	// add a listener to the blur event. When blurred remove tooltip and restore anchor
	el.onblur = function () {
		if (d) document.body.removeChild(d);
		el.onblur = null;
		el._cMenu = null;
		displayElement('OBJECT');displayElement('SELECT');displayElement('IFRAME');
	};
	
	// store a reference to the tooltip div
	el._cMenu = d;
}

function leadingZero(nr) {
//alert("parse value:" + parseInt(nr));
	if (parseInt(nr) < 10) nr = "0" + parseInt(nr);
	return nr;
}

function noDisplayElement(elmID) {
	if (document.all) {
		for (i = 0; i < document.all.tags(elmID).length; i++) {
			obj = document.all.tags(elmID)[i];
			if (! obj || ! obj.offsetParent)
				continue;
			obj.style.visibility = "hidden";
		}
	}
}

function displayElement(elmID) {
	if (document.all) {
		for (i = 0; i < document.all.tags(elmID).length; i++) {
			obj = document.all.tags(elmID)[i];
			if (! obj || ! obj.offsetParent)
			continue;
			obj.style.visibility = "";
		}
	}
}

// returns the scroll left and top for the browser viewport.
function getScroll() {
	if (document.body.scrollTop) {	// IE model
		var ieBox = document.compatMode != "CSS1Compat";
		var cont = ieBox ? document.body : document.documentElement;
		return {x : cont.scrollLeft, y : cont.scrollTop};
	}
	else {
		return {x : window.pageXOffset, y : window.pageYOffset};
	}
}

var TimeOutWaitMilliseconds = 120000; // 2 minutes
var timerID, timeoutUrl, resetTimeoutURL="";

function writeToTimeoutWin(curTimeOut) {
	 var timeout_option = "toolbar=0" + ",location=0" + ",directories=0"
             + ",status=0" + ",menubar=0" + ",scrollbars=0"
             + ",resizable=0"  + ",width=300" + ",height=200";
	var timeout_win = window.open(timeoutPopUpUrl, "TimeOut", timeout_option, true );
}

function clearGoToTimeout()  {
 	clearTimeout(timerID);
 	if(this["UITimeoutMilliseconds"] != null) {
		curTimeOut=UITimeoutMilliseconds;
	}
 	setupTimeout(curTimeOut);
}

function goToTimeout(curTimeOut) {
	self.location=timeoutUrl;
	return;
}

function setupTimeout(curTimeOut) {
	window.setTimeout('writeToTimeoutWin(curTimeOut)', curTimeOut);
	timerID=window.setTimeout('goToTimeout(curTimeOut)', curTimeOut+TimeOutWaitMilliseconds); 
}

function showHelp(hlp) {
	window.open(absoluteWebRoot+'modules/portal/help.cfm?h='+hlp,"Help","toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=0,left=0,top=0,width=320,height=275");
}

	function ListFind(list,val,del) {
		if (!del) del=",";
		listArr = list.split(del);
		fnd=0;
		for (l=0; l < listArr.length; l++) {
			if (listArr[l] == val) {
				fnd=l+1;
				break;
			}
		}
		return fnd;
	}

	function ListDeleteAt(list,ind,del) {
		if (!del) del=",";
		listArr = list.split(del);
		list="";
		for (l=0; l < listArr.length; l++) {
			if (l == (ind-1)) txt = "";
			else txt = listArr[l];
			if (txt != "") {
				list += (list.length>0)?del+txt:""+txt;
			}
		}
		return list;
	}

	function ListAppend(list,val,del) {
		if (!del) del=",";
		list += (list.length>0)?del+val:""+val;
		return list;
	}


window.focus();
window.status = '';
