
/*   
   ===== Fetch browser and platform info =====
*/
		
		
function Is() {
    var agent = navigator.userAgent.toLowerCase();
    this.NN  = ((agent.indexOf('mozilla')!=-1) && ((agent.indexOf('spoofer')==-1) && (agent.indexOf('compatible') == -1)));
    this.IE   = (agent.indexOf("msie") != -1);
    this.WIN = (agent.indexOf("win") != -1);
    this.IE5 = (this.IE && (agent.indexOf('5') != -1));
}


/*-----------
		Lame function to account for the fact that on resize in Netscape
		  layers get thrashed 		
*/

function handleResize() {
	if (is.NN) {
		//location.reload();
		return false;
	}
}


// Set Global Variables

var is = new Is();

var nameArray = new Array(); // array of rollover image names
var menuItems = new Array(); // array containing each menu/submenu set
var buttons;

self.onError = null;


// movement speed of the dynamic menus in pixels/5ms
var menuSpeed = 7;

if (is.NN) {
	onState = "show"; 
  offState = "hide";
	window.captureEvents(Event.RESIZE);
	window.onresize = handleResize;
} 
else if (is.IE) {
	onState = "visible"; 
	offState = "hidden";
}




/*---------------------  
		Initialization
		Called from the onLoad() inside the body tag
		Calls functions to initialize rollover images
		and dynamic menu items
		
-----------------------------------*/
		
function initialize() { 
		GetImageNames(document);
    buttons = new ImageArray(nameArray);
		initMenu();
		
}



/*-----------------------------
		Utility Functions
		
--------------------------------------------*/

function getLayerHeight(layerName) {
	if(is.NN) {
		return document.layers[layerName].clip.height;
	} else {
		return document.all[layerName].clientHeight;
	}

}


function getLayer(layerID) {
	if (is.NN) {
		return document.layers[layerID];
	} else {
		return document.all[layerID].style;
	}
}



/*------------
		Move an object by a specified amount, usually a layer   */

function shiftBy(obj, deltaX, deltaY) {
  if (is.NN) {
    obj.moveBy(deltaX, deltaY);
  } else {
    obj.style.posLeft += deltaX;
    obj.style.posTop += deltaY;
  }
}


/*--------------------------
		Image rollover routines
		
---------------------------------------*/

/*---------------
		GetImageNames
		
		Compiles the rollover image aray by looking for all images
		on the page that have a name attribute.
		
		If the image has a name, it is assumed that it is a rollover
		image and is placed in the array after splitting the filename
		into the path/filename and the extension for use later when
		building the filenames of the on/off states.
		
		*/

function GetImageNames(obj) {
	var ftypeExp = /\.[^\.]*$/;   // regular expression used to split the filename string
	var idExp = /_\d*$/;          // idExp not used yet
	var fnameString;
	var extString;
	var srcString;
	
	// look for all the images in the object passed to the function
	
	for(var i = 0; i<obj.images.length; i++) {
		if (obj.images[i].name != "") {     // only look for ones with a name attribute set
			srcString = obj.images[i].src;
			fnameString = srcString.split(ftypeExp)[0];  // grab the filename without the extension
                        //alert(fnameString);
			extString = srcString.match(ftypeExp);			 // grab the extension
			nameArray[obj.images[i].name] =   // put all the info in an array, including a reference
				{fname:fnameString, 						//   to the image object itself to modify later
				 ftype:extString,
				 objRef:obj.images[i]};
		}
	}
	
	// since netscape nests layers within layers, we must parse all layers by recursion
	if (is.NN) {
		for (var i=0; i < obj.layers.length; i++) {
			GetImageNames(obj.layers[i].document);
		}
	}
}



/*-------------------
		ImageArray
		Arguments: imageNames - an array created by GetImageNames
		Returns:   imgArray - an array of image objects with state information
		
		*/


function ImageArray(imageNames) {
    var imgArray = new Array();

		// set state information for each image collected by GetImageNames
    for(var imgRef in imageNames) {
			imgArray[imgRef] = new Object();
			imgArray[imgRef].state = "norm";       // default to normal/off state
      imgArray[imgRef].norm = new Image();
			
			// build the filnames for the normal and highlighted state
			imgArray[imgRef].norm.src = imageNames[imgRef].fname + imageNames[imgRef].ftype;
      imgArray[imgRef].norm_over = new Image();
			imgArray[imgRef].norm_over.src = 
	    	imageNames[imgRef].fname + "_on" + imageNames[imgRef].ftype;
    }
    return imgArray;
}


/*-----------------
		RollOn
			set the rollover image in the on/highlighted state
*/

function RollOn(imageName) { 
	if(buttons != null) { 
    srcType = buttons[imageName].state + "_over";
    nameArray[imageName].objRef.src = buttons[imageName][srcType].src;
    // alert('RollOn >> ' + nameArray[imageName].objRef.src);
	}
}

/*-----------------
		RollOff
			set the rollover image in the off/normal state
*/

function RollOff(imageName) {
	if(buttons != null) {
    srcType = buttons[imageName].state;
    nameArray[imageName].objRef.src = buttons[imageName][srcType].src;
    //alert('RollOff ' + nameArray[imageName].objRef.src);
	}
}



/*-------------------------
		Dynamic Menu routines
		
-----------------------------------------------------*/



/*----------------------------
		initMenu
		Builds the array of menu items.
	
		Looks for layers with the name attribute set that have a matching layer name with
		"Sub" appended.  If this matched pair exists, they are both stored in the same array
		entry as parent "menuName" and child "menuNameSub".
		Layer Y origins and display height are also recorded for later use.
		
*/


function initMenu() {
	var layerArray;
	var nameRef;
	var layerName;
	var parent;
	var child;
	
	
	// netscape uses a different method of accessing all layers than IE does
	if(is.NN) {
		layerArray = document.layers;
		nameRef = 'name';
	} else {
		layerArray = document.all.tags('DIV');
		nameRef = 'id';
	}
	
	
	// look through all the layers on the page and find all matching sets
	for(var i = 0; i < layerArray.length; i++) {
	//alert("layer");
		layerName = layerArray[i][nameRef];
                //alert(layerName);

		if(layerArray[layerName + "Sub"] != null) {  // only use layers that match the convention

			menuItems[layerName] = new Object();
			menuItems[layerName].parent = layerArray[i];
			menuItems[layerName].child = layerArray[layerName + "Sub"];
			menuItems[layerName].closed = true;
			
			// less typing
			parent = menuItems[layerName].parent;
			child = menuItems[layerName].child;
			
			if(is.NN) {
			
				// reposition the child menus Y coord in relation to it's parent and it's own height
				child.pageY = parent.pageY + parent.clip.height - child.clip.height;
			
				// record the origin Y coords
				menuItems[layerName].childOriginY = child.pageY;
				menuItems[layerName].parentOriginY = parent.pageY;
			
			} else {
			
				// reposition the child menus Y coord in relation to it's parent and it's own height
				child.style.posTop = parent.style.posTop + parent.clientHeight - child.clientHeight;	

				// record the origin Y coords
				menuItems[layerName].childOriginY = child.style.posTop;
				menuItems[layerName].parentOriginY = parent.style.posTop;
			}			
		}
	}
}


function getOpenMenuHeight() {
	var tableHeight = 0;
	var sectionHeight = 0;
	var menuHeight = 0;
	var greatestHeight = 0;
	
	if(is.NN) {
		layerArray = document.layers;
		nameRef = 'name';
	} else {
		layerArray = document.all.tags('DIV');
		nameRef = 'id';
	}
	for(var i = 0; i < layerArray.length; i++) {
		layerName = layerArray[i][nameRef];
		if(layerArray[layerName + "Sub"] != null) {
			if(is.NN) {
				menuHeight += layerArray[layerName].clip.height;
				
				if(layerArray[layerName + "Sub"].clip.height > greatestHeight) {
					greatestHeight = layerArray[layerName + "Sub"].clip.height;
				}
				
			} else {
				menuHeight += layerArray[layerName].clientHeight;
				
				if(layerArray[layerName + "Sub"].clientHeight > greatestHeight) {
					greatestHeight = layerArray[layerName + "Sub"].clientHeight;
				}
				
			}
		}
	}
	
	/*
	if(sectionName != "") {
		if(is.NN) {
			sectionHeight = document.layers[sectionName + "Sub"].clip.height;
		} else {
			sectionHeight = document.all[sectionName + "Sub"].clientHeight;
		}
	} else {
		sectionHeight = 0;
	}*/
	
	
	tableHeight = menuHeight + greatestHeight - 106;
	
	if(tableHeight == 0) {
		return 400;
	} else {
		return tableHeight;
	}
}




//
// toggles the open/closed state of menu layers
//    accepts 2 arguments
//    
//    toggleMenu(<name of parent layer>, <true/false>);
//
//		the true/false argument determines whether or not the menu movement is visible or immediate
//       true = visible
//       false = immediate

var openThisMenu = "";
var menuOpened = "";
var menuMoving = false;

function toggleMenu(menuID, movement) {
	//alert(menuID+""+ movement);
	
	if(menuID != "") {
		if(!menuMoving) {
			menuMoving = true;
			//alert('menuMoving');
			if(menuOpened == menuID) {
				openThisMenu = "";
			} else {
				openThisMenu = menuID;
			}
			
			//alert('menuMoving: ' + menuMoving + '\nmenuOpened: ' + menuOpened + '\nopenThisMenu: ' + openThisMenu);
			
			if(menuOpened != "") {
				closeMenu(menuOpened, movement);
			} else {
				//alert('openMenu');
				openMenu(menuID, movement);
			}
		}
			
			
		//closeMenus(movement);
		
		
		
		//openMenu(menuID, movement);
	
		//setTimeout("openSubMenu('" + menuID + "', " + movement + ")", 30);
	
	}
	
	
	/*
	if(menuItems[menuID].closed) {
		menuItems[menuID].closed = false;
		closeMenus(movement);
		openMenu(menuID, movement);
		setTimeout("openSubMenu('" + menuID + "', " + movement ")", 15);
	} else {
		closeMenus(movement);
	}
	*/
	
	return false;
}
function toggleMenuOnload(menuID, movement) {
	//alert(menuID+""+ movement);
	
	if(menuID != "") {
		if(!menuMoving) {
			menuMoving = true;
			//alert('menuMoving');
			if(menuOpened == menuID) {
				openThisMenu = "";
			} else {
				openThisMenu = menuID;
			}
			
			//alert('menuMoving: ' + menuMoving + '\nmenuOpened: ' + menuOpened + '\nopenThisMenu: ' + openThisMenu);
			
			//if(menuOpened != "") {
				//closeMenu(menuOpened, movement);
			//} else {
				//alert('openMenu');
				openMenu(menuID, movement);
			//}
		}
			
			
		//closeMenus(movement);
		
		
		
		//openMenu(menuID, movement);
	
		//setTimeout("openSubMenu('" + menuID + "', " + movement + ")", 30);
	
	}
	
	
	/*
	if(menuItems[menuID].closed) {
		menuItems[menuID].closed = false;
		closeMenus(movement);
		openMenu(menuID, movement);
		setTimeout("openSubMenu('" + menuID + "', " + movement ")", 15);
	} else {
		closeMenus(movement);
	}
	*/
	
	return false;
}
function closeMenu(menuID, movement) {
	var moveMe = false;
	// close sub menus first
	
	if(is.NN) {
		subY = menuItems[menuID].child.pageY;
		originY = menuItems[menuID].childOriginY;
		menuHeight = menuItems[menuID].child.clip.height;
		menuItems[menuID].child.visibility = offState;
	} else {
		subY = menuItems[menuID].child.style.posTop;
		originY = menuItems[menuID].childOriginY;
		menuHeight = menuItems[menuID].child.clientHeight;
		menuItems[menuID].child.style.visibility = offState;
	}

	
		if(movement) {
			if(subY - menuSpeed  < originY) {
				moveDist = -(subY - originY);
			} else {
				moveDist = -menuSpeed;
			}
		} else {
			moveDist = -(subY - originY);
		}
		
		shiftBy(menuItems[menuID].child, 0, moveDist);
		
		if(moveDist == -menuSpeed) {
			moveMe = true;
		}
	
	
	if(moveMe) {
		setTimeout("closeMenu('" + menuID + "', " + movement + ")", 5);
	} else {
		closeMainMenu(menuID, movement);
	}
}

function closeMainMenu(menuID, movement) {
	var currentY;
	var menuHeight;
	var parentY;
	var moveDist;
	var moveMe = false;
	
	if(is.NN) {
		parentY = menuItems[menuID].parent.pageY;
		childY = menuItems[menuID].child.pageY;	
		menuHeight = menuItems[menuID].child.clip.height;
	} else {
		parentY = menuItems[menuID].parent.style.posTop;
		childY = menuItems[menuID].child.style.posTop;
		menuHeight = menuItems[menuID].child.clientHeight;
	}

	for(layerName in menuItems) {
		if(is.NN) {
			itemY = menuItems[layerName].parent.pageY;
		} else {
			itemY = menuItems[layerName].parent.style.posTop;
		}
	
		if(itemY > parentY) {
			
			if(movement) {
				if(itemY - menuSpeed < menuItems[layerName].parentOriginY) {
					moveDist = -(itemY - menuItems[layerName].parentOriginY);
				} else {
					moveDist = -menuSpeed;
				}
			} else {
				moveDist = -(itemY - menuItems[layerName].parentOriginY);
			}
			
			shiftBy(menuItems[layerName].parent, 0, moveDist);
			
			if(moveDist == -menuSpeed) {
				moveMe = true;
			} 
		} 
	}
	
	if(moveMe) {
		setTimeout("closeMainMenu('" + menuID + "', " + movement + ")", 5);
	} else {
		if(openThisMenu != "") {
			openMenu(openThisMenu, movement);
		} else {
			menuOpened = "";
			menuMoving = false;
		}
	}

}



function openMenu(menuID, movement) {
	var currentY;
	var menuHeight;
	var parentY;
	var moveDist;
	var moveMe = false;
	
	if(is.NN) {
		parentY = menuItems[menuID].parent.pageY;
		childY = menuItems[menuID].child.pageY;	
		menuHeight = menuItems[menuID].child.clip.height;
	} else {
		parentY = menuItems[menuID].parent.style.posTop;
		childY = menuItems[menuID].child.style.posTop;
		menuHeight = menuItems[menuID].child.clientHeight;
	}
        
	for(layerName in menuItems) {
	//alert("hello");
		if(is.NN) {
			itemY = menuItems[layerName].parent.pageY;
                      
		} else {
			itemY = menuItems[layerName].parent.style.posTop;
		}
	       
		if(itemY > parentY) {
			
			if(movement) {
				if(itemY + menuSpeed - menuItems[layerName].parentOriginY > menuHeight) {
					moveDist = menuItems[layerName].parentOriginY + menuHeight - itemY;
				} else {
					moveDist = menuSpeed;
				}
			} else {
				moveDist = menuHeight;
			}
			
			shiftBy(menuItems[layerName].parent, 0, moveDist);
			
			if(moveDist == menuSpeed) {
				moveMe = true;
			} 
		} 
	}
	
	if(moveMe) {
		setTimeout("openMenu('" + menuID + "', " + movement + ")", 5);
	} else {
	//alert("call");
		openSubMenu(menuID, movement);
	}
}


function openSubMenu(menuID, movement) {
//alert("open submenu");
	var moveMe = false;
	
	if(is.NN) {
		subY = menuItems[menuID].child.pageY;
		originY = menuItems[menuID].childOriginY;
		menuHeight = menuItems[menuID].child.clip.height;
		menuItems[menuID].child.visibility = onState;
	} else {
		subY = menuItems[menuID].child.style.posTop;
		originY = menuItems[menuID].childOriginY;
		menuHeight = menuItems[menuID].child.clientHeight;
		menuItems[menuID].child.style.visibility = onState;
	}
	
	/*
	if(!confirm(menuItems[menuID].child.clip.height)){
		return 0;
	}
	*/
        //originY = 2;
        //alert('OriginY and MenuHt ' + originY + ',' + menuHeight);
	if(subY <= originY + menuHeight) {
		if(movement) {
			if(subY + menuSpeed - originY > menuHeight) {
				moveDist = originY + menuHeight - subY;
			} else {
				moveDist = menuSpeed;
			}
		} else {
			moveDist = menuHeight;
		}
		
		shiftBy(menuItems[menuID].child, 0, moveDist);
		
		
		if(moveDist == menuSpeed) {
			moveMe = true;
		}
	}
	
	if(moveMe) {
		setTimeout("openSubMenu('" + menuID + "', " + movement + ")", 5);
	} else {
		menuOpened = menuID;
		menuMoving = false;
	}
	
}
