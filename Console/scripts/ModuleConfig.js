$(function () {
        $("[id*=tvML] input[type=checkbox]").bind("click", function () {
            var table = $(this).closest("table");
            if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                //Is Parent CheckBox
                debugger
                var childDiv = table.next();
                var isChecked = $(this).is(":checked");
                $("input[type=checkbox]", childDiv).each(function () {
                    if (isChecked) {
                        $(this).attr("checked", "checked");
                    } else {
                        $(this).removeAttr("checked");
                    }
                });
            } else {
            debugger
                //Is Child CheckBox
                var parentDIV = $(this).closest("DIV");
//                if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                  if($("input[type=checkbox]:checked", parentDIV).length!=0){
                    $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                } else {
                        if($("input[type=checkbox]:checked", parentDIV).length==0){
                             $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                    }
                }
            }
        });
    })
    
    
    /*-----------------For Manage User----------------------------------*/
    $(function () {
         $("[id*=tvMU] input[type=checkbox]").bind("click", function () {
            var table = $(this).closest("table");
            if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                //Is Parent CheckBox
                debugger
                var childDiv = table.next();
                var isChecked = $(this).is(":checked");
                $("input[type=checkbox]", childDiv).each(function () {
                    if (isChecked) {
                        $(this).attr("checked", "checked");
                    } else {
                        $(this).removeAttr("checked");
                    }
                });
            } else {
                 //Is Child CheckBox
                var parentDIV = $(this).closest("DIV");
//                if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                  if($("input[type=checkbox]:checked", parentDIV).length!=0){
                    $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                } else {
                        if($("input[type=checkbox]:checked", parentDIV).length==0){
                             $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                    }
                }
            }
        });
    })
    /*----------------------For Office Timing--------------------*/
        $(function () {
         $("[id*=tvOT] input[type=checkbox]").bind("click", function () {
            var table = $(this).closest("table");
            if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                //Is Parent CheckBox
                debugger
                var childDiv = table.next();
                var isChecked = $(this).is(":checked");
                $("input[type=checkbox]", childDiv).each(function () {
                    if (isChecked) {
                        $(this).attr("checked", "checked");
                    } else {
                        $(this).removeAttr("checked");
                    }
                });
            } else {
                 //Is Child CheckBox
                var parentDIV = $(this).closest("DIV");
//                if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                  if($("input[type=checkbox]:checked", parentDIV).length!=0){
                    $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                } else {
                        if($("input[type=checkbox]:checked", parentDIV).length==0){
                             $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                    }
                }
            }
        });
    })
    /*----------------------For Roaming Facilities--------------------*/
        $(function () {
         $("[id*=tvRF] input[type=checkbox]").bind("click", function () {
            var table = $(this).closest("table");
            if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                //Is Parent CheckBox
                debugger
                var childDiv = table.next();
                var isChecked = $(this).is(":checked");
                $("input[type=checkbox]", childDiv).each(function () {
                    if (isChecked) {
                        $(this).attr("checked", "checked");
                    } else {
                        $(this).removeAttr("checked");
                    }
                });
            } else {
                 //Is Child CheckBox
                var parentDIV = $(this).closest("DIV");
//                if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                  if($("input[type=checkbox]:checked", parentDIV).length!=0){
                    $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                } else {
                        if($("input[type=checkbox]:checked", parentDIV).length==0){
                             $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                    }
                }
            }
        });
    })
       /*----------------------For Reports--------------------*/
        $(function () {
         $("[id*=tvReports] input[type=checkbox]").bind("click", function () {
            var table = $(this).closest("table");
            if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                //Is Parent CheckBox
                debugger
                var childDiv = table.next();
                var isChecked = $(this).is(":checked");
                $("input[type=checkbox]", childDiv).each(function () {
                    if (isChecked) {
                        $(this).attr("checked", "checked");
                    } else {
                        $(this).removeAttr("checked");
                    }
                });
            } else {
                 //Is Child CheckBox
                var parentDIV = $(this).closest("DIV");
//                if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                  if($("input[type=checkbox]:checked", parentDIV).length!=0){
                    $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                } else {
                        if($("input[type=checkbox]:checked", parentDIV).length==0){
                             $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                    }
                }
            }
        });
    })
      /*----------------------For Personalise Login Screen--------------------*/
        $(function () {
         $("[id*=tvPLS] input[type=checkbox]").bind("click", function () {
            var table = $(this).closest("table");
            if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                //Is Parent CheckBox
                debugger
                var childDiv = table.next();
                var isChecked = $(this).is(":checked");
                $("input[type=checkbox]", childDiv).each(function () {
                    if (isChecked) {
                        $(this).attr("checked", "checked");
                    } else {
                        $(this).removeAttr("checked");
                    }
                });
            } else {
                 //Is Child CheckBox
                var parentDIV = $(this).closest("DIV");
//                if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                  if($("input[type=checkbox]:checked", parentDIV).length!=0){
                    $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                } else {
                        if($("input[type=checkbox]:checked", parentDIV).length==0){
                             $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                    }
                }
            }
        });
    })
      /*----------------------For Personalise Inner Page--------------------*/
        $(function () {
         $("[id*=tvPIP] input[type=checkbox]").bind("click", function () {
            var table = $(this).closest("table");
            if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                //Is Parent CheckBox
                debugger
                var childDiv = table.next();
                var isChecked = $(this).is(":checked");
                $("input[type=checkbox]", childDiv).each(function () {
                    if (isChecked) {
                        $(this).attr("checked", "checked");
                    } else {
                        $(this).removeAttr("checked");
                    }
                });
            } else {
                 //Is Child CheckBox
                var parentDIV = $(this).closest("DIV");
//                if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                  if($("input[type=checkbox]:checked", parentDIV).length!=0){
                    $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                } else {
                        if($("input[type=checkbox]:checked", parentDIV).length==0){
                             $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                    }
                }
            }
        });
    })