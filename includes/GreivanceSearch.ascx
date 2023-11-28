<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GreivanceSearch.ascx.cs" Inherits="Include_GreivanceSearch" %>

<script src="../js/jQuery-2.1.3.min.js"></script>
     <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="css/font-awesome.min.css" rel="stylesheet">
     <link href="css/customweb.css" rel="stylesheet">
     <link href="css/flexslider.css" rel="stylesheet">

     <link href="../css/font-awesome.min.css" rel="stylesheet">
<link href="../css/bootstrap.min.css" rel="stylesheet">
<link href="../css/open-sans.css" rel="stylesheet">
<link href="../css/simple.min.css" rel="stylesheet">
<link href="../css/jquerysctipttop.css">
<link href="../css/custum.css" media="all" rel="stylesheet">
<script src="../js/jquery.min.js"></script>
<script src="../js/bootstrap.min.js"></script>
<script src="../js/highcharts.js"></script>
<script src="../js/exporting.js"></script>
<script src="../js/jquery.marquee.js"></script>

     <link href="https://fonts.googleapis.com/css?family=Open+Sans" rel="stylesheet">

     <link href='https://fonts.googleapis.com/css?family=Raleway:400,600,700,700italic,500,400italic,500italic,600italic,900,900italic' rel='stylesheet' type='text/css'>
     <style type="text/css">
        .table tr.thStyle th { background-color: #0279b6 !important;color: #FFFFFF !important;}
        .modalPopup table td {padding: 3px 5px !important; }
        .cr_pass .title{ margin-top: 0px !important; }
    </style>
   <%-- <script src="../js/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../js/jQuery.alert.js" type="text/javascript"></script>
    <link href="../css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="../js/CSMValidation.js" type="text/javascript"></script>
  --%>
     <script type="text/javascript" language="javascript">

         function ShowAlert() {
             debugger;
           var frm_hit = 550; var location_detl_ftr = '<button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>';
//             if (blankFieldValidation("[id*=txtTokenNo]", 'Complainant Name', 'FS&CW-CGRC') == false) {
//                 return false;
//             }
//             if (blankFieldValidation('txtName', 'Complainant Name', 'FS&CW-CGRC') == false) {
//                 return false;
//             }
             if ($("[id*=txtTokenNo]").val() == "") {
                 alert('Please Enter Token  Number', 'FS&CW-CGRC');
                 $("[id*=txtTokenNo]").focus();
                 return false;
             }
             if ($("[id*=txtTelePhoneNo]").val() == "") {
                 alert('Please Enter Mobile Number', 'FS&CW-CGRC');
                 $("[id*=txtTicketnumber]").focus();
                 return false;
             }
             var mobile = $("[id*=txtTelePhoneNo]").val();
             var token = $("[id*=txtTokenNo]").val();
             var location_detl_body_cnt = 'Reports/ComplaintDetails.aspx?ticket=' + token + '&mob=' + mobile + '';
             $("[id*=txtTelePhoneNo]").val('');
             $("[id*=txtTokenNo]").val('');
             openPageModalnHdr(location_detl_body_cnt, location_detl_ftr, frm_hit); 
         }
        
         function openPageModalnHdr(page, footer, frm_hit) {
             $('#pageModal .modal-body').html("<iframe width='100%' height='" + frm_hit + "px' src='" + page + "' frameborder='0' scrolling='no'></iframe>");
             $('#pageModal .modal-footer').html(footer);
             if (footer == "") { $('#pageModal .modal-footer').remove(); }
             $('#pageModal').modal();
         }
    </script>
       
       <div class="col-sm-8 col-xs-12" >
<div class="bgblue">
<h1>Grievance status</h1>	
<hr>

<form class="form-inline" >
<div width="66%">
<div class="form-group" >
<input type="text" class="form-control" id="txtTelePhoneNo" runat="server"  placeholder="Enter Mobile Number"/>
</div>

<div class="form-group" >
<input type="text" name="user"  id="txtTokenNo"  runat="server"  class="form-control" placeholder="Enter Ticket Number"/>
</div>
	<div class="clearfix hidden-md visible-sm"></div>
<button type="submit" runat="server"  id="btnSubmit" onclick="ShowAlert();" class="btn btn-success">submit</button>
</div>
</form>	

</div>
</div>
   <%--  <form id="form2" runat="server" name="" method="post">--%>
 <%--<div class="col-sm-12 margintop">--%>
   <%-- <div class="grievance-bg">
    <h4>GRIEVANCE STATUS</h4>
      <div class="row">
        <div class="col-sm-7">
        <p>Enter your Complaint No.</p>
         <input type="text" name="user"  id="txtTokenNo"  runat="server" class="form-control"/>
        </div>
        <div class="col-sm-5 padding-left">
         <p>Telephone</p>
          <div class="input-group">
        
            <input type="text" class="form-control" id="txtTelePhoneNo" runat="server" placeholder=""/>
            <div class="input-group-btn">
                <%--<asp:Button Text="" id="btnSubmit" class="btn btn-default glyphicon glyphicon-search " OnClientClick="return ShowAlert();" runat="server" />--%>
                <%--<button class="btn btn-default" type="submit" runat="server" id="btnSubmit" onclick="ShowAlert();"><i class="glyphicon glyphicon-search"></i></button>
            </div>
        </div>
         
        </div>
      </div>
       
       
    </div>--%>

    <%--<div class="col-sm-8 col-xs-12" >
<div class="bgblue">
<h1>Grievance status</h1>	
<hr>

<form class="form-inline">
<div class="form-group">
<input type="email" class="form-control" id="txtTelePhoneNo" runat="server"  placeholder="Enter Mobile Number">
</div>

<div class="form-group">
<input type="text" name="user"  id="txtTokenNo"  runat="server"  class="form-control" placeholder="Enter Ticket Number">
</div>
	<div class="clearfix hidden-md visible-sm"></div>
<button type="button" runat="server"  id="btnSubmit" onclick="ShowAlert(); class="btn btn-success">submit</button>
</form>	

</div>
</div>--%>

       <%--<div class="modal fade bs-example-modal-lg" id="pageModal" tabindex="-1" role="dialog"
        aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
               <div class="modal-body">
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>--%>
   <%--</div>--%>
 <%--  </form>--%>

