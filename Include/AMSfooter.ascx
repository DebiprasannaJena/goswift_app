<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AMSfooter.ascx.cs" Inherits="includes_AMSfooter" %>
 <footer class="main-footer">
            <strong>Copyright &copy; 2017 <a href="#">Invest Odisha </a>,</strong> All rights reserved.
         </footer>

         <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
<script src="../js/bootstrap.min.js" type="text/javascript"></script>
<script src="../js/lobipanel.min.js" type="text/javascript"></script>
<script src="../js/pace.min.js" type="text/javascript"></script>
<script src="../js/fastclick.min.js" type="text/javascript"></script>
      <script src="../js/custom2.js" type="text/javascript"></script>
     <script src="../js/dashboard.js" type="text/javascript"></script>
      
    <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
     <script>

         $(document).ready(function () {


             $('[data-toggle="tooltip"]').tooltip();
             $('.datePicker').datepicker({
                 autoclose: true,
                 format: 'mm/dd/yyyy'
             });

             $('.sidebar-menu li a').click(function () {
                 $(this).find(".arrow i").toggleClass('fa-angle-down fa-angle-right');

             })
             $('.PrintBtn').click(function () {
                 window.print();

             })

             var winwidth = $(window).width();
             if (winwidth < 1050) {

                 $('.sidebar-mini').addClass('sidebar-collapse');

             }

         });
</script>


      <!-- End js
         =====================================================================-->


        <!-- Large Modal Window Panel -->
        <div class="modal fade" id="pageModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title" id="myModalLabel"></h4>
                    </div>
                    <div class="modal-body">
                    </div>
                </div>
            </div>
        </div>
  </form>
  <script>

      //     document.body.onclick = onuser_activite;
      //     timeoutHnd = setTimeout('OnTimeoutReached();', logouTimeInterval);


    </script>
    <script>
        var eles = document.getElementsByTagName('input');
        for (i = 0; i < eles.length; i++) {
            eles[i].setAttribute("autocomplete", "off");

        }
        document.addEventListener('contextmenu', event => event.preventDefault());
              $('body').bind('copy paste',function(e) { e.preventDefault(); return false; });
});
</script>