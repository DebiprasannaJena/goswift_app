<%@ Control Language="C#" AutoEventWireup="true" CodeFile="simplefooter.ascx.cs" Inherits="Application_includes_footer" %>
<div class="container footer-container">
<div class="footer-wrapper bg-footertop">
<div class="footer-container">
  <div class="footer">
  
   
      <p>Copyright &copy; 2023 Invest Odisha, All Rights Reserved.</p>
    
  </div>
</div>
</div></div>
<!--//	Footer //--> 
<!--//	Go to Top //--> 
<a href="javascript:void(0);" title="Go Top"  data-toggle="tooltip" class="scrollup"><i class="fa fa-angle-double-up text-white"></i><br />
</a> 
<!--//	Go to Top //--> 
<%--<script src="js/jquery-2.1.1.js" type="text/javascript"></script>--%>
<script src="https://code.jquery.com/jquery-2.1.1.min.js" integrity="sha256-h0cGsrExGgcZtSZ/fRz4AwV+Nn6Urh/3v3jFRQ0w9dQ=" crossorigin="anonymous"></script>
<%--<script src="js/bootstrap.min.js" type="text/javascript"></script>--%>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>


 <script src="js/jquery.mCustomScrollbar.concat.min.js"    type="text/javascript"></script>
 <script src="js/jquery.counterup.min.js"  type="text/javascript"></script>
<script type="text/javascript" src="js/waypoints.min.js"  ></script>

   
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

     });
</script>
<script>
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();

        var windowheight = $(window).height();
        var headerheight = $("header").height();
        var footerheight = $(".footer-container").height();
        var regheight = windowheight - (headerheight + footerheight);
        //alert(windowheight + "---" + headerheight)
        $(".registration-div").css("min-height", regheight-15+"px")
        
    });

    
     
		</script>
       
    