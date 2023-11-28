
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="Includes_Footer" %>
<!-- START SCRIPTS --> 
<div class="message-box animated fadeIn" data-sound="alert" id="mb-signout">
  <div class="mb-container">
    <div class="mb-middle">
      <div class="mb-title"><span class="fa fa-sign-out"></span> Log <strong>Out</strong> ?</div>
      <div class="mb-content">
        <p>Are you sure you want to log out?</p>
        <p>Press No if you want to continue work. Press Yes to logout current user.</p>
      </div>
      <div class="mb-footer">
        <div class="pull-right"> <a href="../LogOut.aspx" class="btn btn-success btn-lg">Yes</a>
          <button class="btn btn-default btn-lg mb-control-close">No</button>
        </div>
      </div>
    </div>
  </div>
</div>
<%--<script type="text/javascript" src="../js/jquery.min.js"></script> --%>
<script type="text/javascript" src="../js/jquery-ui.min.js"></script> 
<script type="text/javascript" src="../js/bootstrap.min.js"></script> 
<script type="text/javascript" src="../js/icheck.min.js"></script> 
<script type="text/javascript" src="../js/jquery.mCustomScrollbar.min.js"></script> 
<script type="text/javascript" src="../js/scrolltopcontrol.js"></script> 
<script type="text/javascript" src="../js/bootstrap-datepicker.js"></script> 
<script type="text/javascript" src="../js/owl.carousel.min.js"></script> 
<script type="text/javascript" src="../js/daterangepicker.js"></script> 
<%--<script type="text/javascript" src="../js/actions.min.js"></script> --%>
<%--<script src="../js/actions.js" type="text/javascript"></script>--%>
<!-- <script type="text/javascript" src="./js/highcharts.js"></script>--> 
<!-- END SCRIPTS -->
<div id="topcontrol" title="Scroll Back to Top" style="position: fixed; bottom: 10px; right: 10px; opacity: 0; cursor: pointer;"><!-- TO TOP -->
  <div class="to-top"><span class="fa fa-angle-up"></span></div>
  <!-- END TO TOP --></div>

<div class="clearfix"></div>
<div class="footer">
  <p>
 <%-- Copyright &copy; <% DateTime.Now.Year.ToString(); %> Grievance Redressal System  | All rights reserved.--%>
 Copyright @ 2017 Food Supplies and Consumer Welfare Department, Govt. of Odisha. All rights reserved
  </p>
</div>

<!-- START TEMPLATE --> 

<script type="text/javascript" src="../js/plugins.js"></script> 
<script type="text/javascript" src="../js/actions.js"></script> 
<script type="text/javascript" src="../js/demo_dashboard.js"></script> 
<!-- END TEMPLATE --> 


 <div class="modal fade bs-example-modal-lg" id="pageModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
        <h4 class="modal-title" id="myModalLabel">Complaint Deatils</h4>
        </div>
      <div class="modal-body"></div>
      <div class="modal-footer"></div>
    </div>
  </div>
</div>

