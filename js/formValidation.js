
/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */ 
 function validateForm(lang)
        { 
		var alrtMsg =  "Are you Sure to Submit?";
            alrtMsg =  "Are you Sure to Submit?";

            $('.form-error, .form-error2').remove();
            $('.req-file, .form-error2').remove();

           var all = $(".req").map(function () {
              
               var vrTitle = $(this).attr('title');
               var strng = $(this).attr('id');
               var incStr = strng.includes("fil_");
               var ctrlId = $(this).prop('id');
               if (incStr == true) {

                   if (($(this).val() == null || $(this).val() == '') && ($(this).css('display') != 'none')) {
                       $(this).parent().parent().append('<p class="form-error"> This field can not be left blank ! </p>');                       
                       $('#' + ctrlId).focus();
                       return false;
                   }
                   else {
                       var filename = "NA.pdf";
                       if (($('#hdn_' + strng + '').val() != null) && ($('#hdn_' + strng + '').val() != "")) {
                           filename = $('#hdn_' + strng + '').val();
                       }
                       var result = doesFileExist("Portal/Document/Upload/" + filename + "");
                       if (result == false) {
                           $(this).parent().parent().append('<p class="form-error"> This field can not be left blank ! </p>');
                           $('#' + ctrlId).focus();
                           return false;
                       }                       
                   }
               }
               else {
                   if (($(this).val() == null || $(this).val() == '') && ($(this).css('display') != 'none')) {                      
                       $(this).parent().append('<p class="form-error"> This field can not be left blank ! </p>');                       
                       $('#' + ctrlId).focus();
                       return false;
                   }
               }          
           });

     
           var allEmailReq = $(".emailR").map(function () {
                if (($(this).val() == null || $(this).val() == '') && ($(this).css('display') != 'none')) {
                    var vrTitle = $(this).attr('title');
                    //var vrTitle = $(this).title;
                    //$(this).css('border-color', 'red');
                    $(this).parent().append('<p class="form-error">This field can not be left blank ! </p>');
                    var ctrlId = $(this).prop('id');
                    $('#' + ctrlId).focus();
                    return false;
                }
              
           });

           var allEmail = $(".email").map(function () {
            if ($(this).val().trim() != '' && $(this).css('display') != 'none') {
                    if (isEmail($(this).val().trim()) === false) {
                       // $(this).css('border-color', 'red');
                        $(this).parent().append('<p class="form-error"> Invalid Email </p>');

                        var ctrlId = $(this).prop('id');
                        $('#' + ctrlId).focus();
                        return false;
                    }
                }
           });

           var allRadio = $(".reqR").map(function () {


            var name = $(this).attr('name'); 
            var dispR = $(this).css('display');
            if (!$("input:radio[name='" + name + "']").is(":checked") && dispR != 'none') {
                //$(this).css('border-color', 'red');
                var vrTitle = $(this).attr('title');
                $(this).parent().append('<p class="form-error"> Please select This Field </p>');

                return false;
            }
           }); 
     
           var allCheck = $(".reqC").map(function() {
            //            var name    = $(this).data('name')+"[]";
            var name = $(this).attr('name'); 
            var dispC   = $(this).css('display');
            var count_checked = $("[name='"+name+"']:checked").length;
            if(count_checked == 0 && dispC != 'none') 
            {
           // $(this).css('border-color','red');
            var vrTitle = $(this).attr('title');
            $(this).parent().append('<p class="form-error">Please select This Field </p>');

            return false;
            }
           });

           var allDropDwn = $(".reqD").map(function () {
        if (($(this).val() == null || $(this).val() == '') || $(this).val() == 0 || $(this).val()=="select" && ($(this).css('display') != 'none')) {
            var vrTitle = $(this).attr('title');
            //var vrTitle = $(this).title;
            //$(this).css('border-color', 'red');
            $(this).parent().append('<p class="form-error">This Field  can not be left blank !</p>');

            var ctrlId = $(this).prop('id');
            $('#' + ctrlId).focus();
            return false;
        }
    });
      


            var blankStatus=true;
            for(var ctr=0;ctr<all.length;ctr++)
        {
            blankStatus=all[ctr];
            if(blankStatus==false)
            {
            return false;
            break;
            }
     }

            var emailStatus=true;
            for(var i=0;i<allEmail.length;i++)
        {
            emailStatus=allEmail[i];
            if(emailStatus==false)
            {
            return false;
            break;
            }
        }

            var emailReqStatus = true;
            for (var i = 0; i < allEmailReq.length; i++) {
        emailReqStatus = allEmailReq[i];
        if (emailReqStatus == false) {
            return false;
            break;
        }
    }

            var radioStatus=true;
            for(var i=0;i<allRadio.length;i++)
        {
            radioStatus=allRadio[i];
            if(radioStatus==false)
            {
            return false;
            break;
            }
        }
        
            var chkboxStatus=true;
            for(var i=0;i<allCheck.length;i++)
        {
            chkboxStatus=allCheck[i];
            if(chkboxStatus==false)
            {
            return false;
            break;
            }
    }

            var dropdwnStatus = true;
            for (var i = 0; i < allDropDwn.length; i++) {
        dropdwnStatus = allDropDwn[i];
        if (dropdwnStatus == false) {
            return false;
            break;
        }
    }
    
        //confirmAlert(alrtMsg);
}

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}
function doesFileExist(urlToFile) {
    var xhr = new XMLHttpRequest();
    xhr.open('HEAD', urlToFile, false);
    xhr.send();

    if (xhr.status == "404") {
        return false;
    } else {
        return true;
    }
}

