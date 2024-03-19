<%@ Control Language="C#" AutoEventWireup="true" CodeFile="investorfooter.ascx.cs" Inherits="Application_includes_footer" %>
<div class="container footer-container">
<div class="footer-wrapper bg-footertop">
<div class="footer-container">
 <%-- <div class="footer">
  
   
      <p>Copyright &copy; 2017 Invest Odisha, All Rights Reserved.</p>
    
  </div>--%>
    <div class="footer">
        <p id="copyright">
            Copyright &copy; <span id="currentYear"></span> Invest Odisha, All Rights Reserved.
        </p>
    </div>
    <!-- JavaScript code to update the year dynamically -->
    <script>
        // Get the current year
        var currentYear = new Date().getFullYear();
        // Update the year in the footer
        document.getElementById('currentYear').textContent = currentYear;
    </script>


</div>
</div></div>
<!--//	Footer //--> 
<!--//	Go to Top //--> 
<a href="#" title="Go Top"  data-toggle="tooltip" class="scrollup"><i class="fa fa-angle-double-up text-white"></i><br />
</a> 
<!--//	Go to Top //--> 
<script src="../js/jquery-2.1.1.js" type="text/javascript"></script>
<script src="../js/bootstrap.min.js" type="text/javascript"></script>
<script src="../js/wow.min.js" type="text/javascript"></script>
<script src="../js/owl.carousel.js" type="text/javascript"></script>
 <script src="../js/jquery.newsTicker.js" type="text/javascript"></script>
 <script src="../js/jquery.mCustomScrollbar.concat.min.js" type="text/javascript"></script>
 <script src="../js/jquery.counterup.min.js" type="text/javascript"></script>
<script type="text/javascript" src="../js/waypoints.min.js"></script>

   
 <%-- Scripts --%>
 <script>
     $(document).ready(function () {
         new WOW().init();
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
    });

    
     
		</script>
       
    