<%@ Page Language="C#" AutoEventWireup="true" CodeFile="registration.aspx.cs" Inherits="website_registration" %>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <style>
   
    </style>
</head>
<body>
    <form id="form1" runat="server">
   <uc2:header ID="header" runat="server" />
   <div class="pagenavigator">
   <h2><a class="" href="javascript:history.back()"><i class="fa fa-chevron-circle-left"></i></a>Registration</h2>
   </div>
  <div class="registration-div">
  <div class="container">
  <div class="form-sec">
  <h2>Personal Details</h2>
  <div class="form-group">
  <div class="row">
  <div class="col-sm-4">
    <label for="fname">First Name</label>
    <input type="text" class="form-control" id="fname" />
    </div>
    <div class="col-sm-4">
    <label for="mname">Middle Name</label>
    <input type="text" class="form-control" id="mname"/> 
    </div>
    <div class="col-sm-4">
    <label for="lname">Last Name</label>
    <input type="text" class="form-control" id="lname" />
    </div>
    </div>
  </div>
  <div class="form-group">
  <div class="row">
  <div class="col-sm-4">
    <label for="email">Email</label>
    <input type="email" class="form-control" id="email" />
    </div>
    <div class="col-sm-4">
    <label for="cemail">Confirm Email</label>
    <input type="text" class="form-control" id="cemail"/> 
    </div>
    <div class="col-sm-4">
    <label for="dob">Date of Birth</label>
    <div class="input-group">
                <input type="text" class="form-control" >
                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
            </div>
    </div>
    </div>
  </div>
    <div class="form-group">
  <div class="row">
  <div class="col-sm-4">
    <label for="email">Address</label>
   <textarea class="form-control">
   
   </textarea>
    </div>
    <div class="col-sm-4">
    <label for="country">Country</label>
    <select class="form-control"><option>---Select---</option></select>
    </div>
    <div class="col-sm-4">
    <label for="State">State</label>
    <select class="form-control"><option>---Select---</option></select>
    </div>
     <div class="col-sm-4">
    <label for="State">District/Zone</label>
    <select class="form-control"><option>---Select---</option></select>
    </div>
     <div class="col-sm-4">
    <label for="State">Talika</label>
    <select class="form-control"><option>---Select---</option></select>
    </div>
    </div>
  </div>
  </div>
  </div>
  </div>
   <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
