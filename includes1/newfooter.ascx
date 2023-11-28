<%@ Control Language="C#" AutoEventWireup="true" CodeFile="newfooter.ascx.cs" Inherits="Application_includes_footer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:ScriptManager  runat="server" EnablePageMethods="true">
    </asp:ScriptManager>--%>
<div class="container footer-container">
    <div class="footer-wrapper bg-footertop " >
     
        <div class="footer">
        <div class="col-sm-6">
         
                    <ul>
                        <li><a href="http://odisha.gov.in/" target="_blank" title="Odisha Government Portal">Govt. of Odisha</a></li>
                        <li><a href="https://india.gov.in/" target="_blank" title="India Government Portal">Govt. of India</a></li>                      
                   
                        <li><a href="http://investodisha.gov.in" target="_blank" title="Invest Odisha">Invest Odisha</a></li>
                       
                    </ul>

            <p id="copy">
                <%--Copyright Single Window Portal © 2017, All Rights Reserved.--%></p>
                </div>
               
                  <div class="col-sm-6 text-right">
    <div class="social">
    <label>Social links :</label>
    	<ul class="social-links pull-right">

        <li><a href="https://www.facebook.com/InvestOdisha/" title="facebook" target="_blank" class="facebook">
                                <i class="fa fa-facebook"></i></a></li>
                            <li><a href="https://twitter.com/InvestInOdisha" title="twitter" target="_blank" class="twitter"><i
                                class="fa fa-twitter"></i></a></li>
                            <li><a href="https://in.linkedin.com/company/investodisha" title="Linked In" target="_blank" class="linkedin"><i
                                class="fa fa-linkedin"></i></a></li>

</ul>
    </div>	

                   <div class="clearfix"></div>
        </div>
        <div class="clearfix"></div>
    </div>
</div>

<!--//	Footer //-->
<!--//	Go to Top //-->
<a href="javascript:void(0);" title="Go Top" data-toggle="tooltip" class="scrollup"><i class="fa fa-angle-double-up text-white">
</i>
    <br />
</a>
<a href="http://investodisha.gov.in/" target="_blank" class="ideabx-panel2"><i class="fa fa-chevron-circle-left"></i>&nbsp;Invest Odisha</a>
<!--//	Go to Top //-->
<%--<script src="js/jquery-2.1.1.js" type="text/javascript"></script>--%>
<script src="https://code.jquery.com/jquery-2.1.1.min.js" integrity="sha256-h0cGsrExGgcZtSZ/fRz4AwV+Nn6Urh/3v3jFRQ0w9dQ=" crossorigin="anonymous"></script>
<%--<script src="js/bootstrap.min.js" type="text/javascript"></script>--%>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
<link href="css/chocolat.css" rel="stylesheet" type="text/css" />
<script src="js/jquery.chocolat.js" type="text/javascript"></script>
<%-- Scripts --%>
<script>
    $(document).ready(function () {
 
        //===Scroll to Top===//  	
        $(window).scroll(function () {
            if ($(this).scrollTop() > 100) {
                $('.scrollup').fadeIn();
                // $('header').addClass('navbar-fixed-top');

            } else {
                $('.scrollup').fadeOut();
                // $('header').removeClass('navbar-fixed-top');
            }
        });

        $('.scrollup').click(function () {
            $("html, body").animate({
                scrollTop: 0
            }, 1000);
            return false;
        });
        //===Scroll to Top===//  	

        function toggleIcon(e) {
            $(e.target)
        .prev('.panel-heading')
        .find(".more-less")
        .toggleClass('fa-minus fa-plus');
        }
        $('.panel-group').on('hidden.bs.collapse', toggleIcon);
        $('.panel-group').on('shown.bs.collapse', toggleIcon);
        //        $("#footer$txtMobileNumber").change(function () {
        //            var val = $(this).val();
        //            if (val.substring(0, 1) === '0') {
        //                jAlert('<strong>Mobile Number should not be start with zero</strong>', projname);
        //                $(this).val('');
        //                $(this).focus();
        //                return false;
        //            }
        //            if (val.length < 10) {
        //                jAlert('<strong>The minimum length of the mobile number should be 10.</strong>', projname);
        //                $(this).focus();
        //                $(this).val('');
        //                return false;
        //            }
        //        });

    });
</script>
<script >
    //*************** Copywright Function
    $.fillCopyright = function (selDiv, title) {
        var curDate = new Date();
        var curYear = curDate.getFullYear();
        var copyVal = "Copyright &copy; " + "<b>" + curYear + "</b>" + " " + title + ", All Rights Reserved";
        $('#' + selDiv).html(copyVal);
    }

    $(document).ready(function () {
        $.fillCopyright('copy', 'Invest Odisha');

        $('[data-toggle="tooltip"]').tooltip();
    });

    $(function () {
        $('.grid-ga a').Chocolat();
    });

    $(function () {
        $('.footer-gallery .img-container a').Chocolat();
    });
     
</script>
<script >
    function Validate() {
        debugger;
        if (document.getElementById("footer_txtFirstName").value == "") {
            jAlert("First Name can not be left blank !");
            document.getElementById("footer_txtFirstName").focus();
            return false;
        }
        if (document.getElementById("footer_txtLastName").value == "") {
            jAlert("Last Name can not be left blank !");
            document.getElementById("footer_txtLastName").focus();
            return false;
        }
        if (document.getElementById("footer_txtEmail").value == "") {
            jAlert("Email ID can not be left blank !");
            document.getElementById("footer_txtEmail").focus();
            return false;

        }
        if (document.getElementById("footer_txtEmail").value != "") {

            if (!emailCheck()) {
                document.getElementById('footer_txtEmail').focus();
                return false;
            }
        }
        function emailCheck() {
            debugger;
            var email = document.getElementById('footer_txtEmail');
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!filter.test(email.value) || footer_txtEmail.value == "") {
                jAlert('Invalid Email Id !');
                return false;
            }
            return true;
        }
        if (document.getElementById("footer_txtMobileNumber").value == "") {
            jAlert("Mobile Number can not be left blank !");
            document.getElementById('footer_txtMobileNumber').focus();
            return false;
        }
        var val = ($("#footer_txtMobileNumber").val());
        if (($("#footer_txtMobileNumber").val()).substring(0, 1) == '0') {
            jAlert('Mobile Number should not be start with zero !');
            $("#footer_txtMobileNumber").val('');
            $("#footer_txtMobileNumber").focus();
            return false;
        }
        if (($("#footer_txtMobileNumber").val().length < 10) && ($("#footer_txtMobileNumber").val().length > 0)) { jAlert('Mobile No. can not be less then 10 characters !'); $("#footer_txtMobileNumber").focus(); return false; }

        if (document.getElementById("footer_txtSubject").value == "") {
            jAlert("Subject can not be left blank !");
            document.getElementById('footer_txtSubject').focus();
            return false;
        }
        if (document.getElementById("footer_txtFeedback").value == "") {
            jAlert("Feedback can not be left blank !");
            document.getElementById('footer_txtFeedback').focus();
            return false;
        }
        return true;
    }
       
</script>
