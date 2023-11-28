<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptForgotPassword.aspx.cs" Inherits="Portal_DeptForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>SWP(single Window Portal)</title>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
<meta charset="utf-8" />
<meta name="description" content="ODISHA RANKS 1st IN INDIA with 17% of the total live projects in manufacturing sector worth Rs.33 lakh crore.,BRANDING ODISHA Attractive destination in the identified focus sectors of the state, LARGE SCALE INDUSTRIES The state by providing necessary support services,STRENGTHS & COMPETITIVENESS Highlighting the investment opportunities in the state" />
<meta name="author" content="IPICOL"/>
<meta name="keywords" content="Invest, Odisha, Investor, IPICOl, Industry, Industries, Team Odisha, Indudtrial Invest in Odisha,Single Window Portal"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
<link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
<link href="../css/login.css" rel="stylesheet" type="text/css" />

        <!--<link href="assets/dist/css/stylecrm-rtl.css" rel="stylesheet" type="text/css"/>-->
        <script language="javascript" type="text/javascript" src="~/Portal/Console/scripts/Validator.js"></script>
        <script src="Console/jscript48/Validator.js" type="text/javascript"></script>
        <script type="text/javascript" language="javascript" src="Console/scripts/md5.js"></script>
        <script type="text/javascript" language="javascript" src="Console/scripts/swfobject.js"></script>
        <script src="Console/scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
        <script src="Console/scripts/popup.js" type="text/javascript"></script>
</head>
<body>
   <form id="form1" runat="server">
      <!-- Content Wrapper -->
         <div class="login"> 
         <div class="top-header">
         <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
            </asp:ScriptManager>
    <div class="logo"><a href="" alt="GO SWIFT"><img src="images/logo2.png" alt="Odisha Government" ><img src="images/logo.png" alt="GO Swift" ></a>
    <span class="clr"></span>
    <%-- <h1 >SWP(Single Window Portal)</h1>--%>
    </div>
    
    <div class="clr"></div>
  </div>
  
  <div class="login-inner">
     <div class="form">
   

      <div class="field-wrap" id="uid">
        <label> Enter Email Id </label>
       
          <asp:TextBox ID="txtEmailID" runat="server"   AutoCompleteType="disabled" autocomplete="false" onPaste="return false"  class="form-control"></asp:TextBox>

      </div>
   

 <div>
                            <div class="clr">
                            </div>
                        </div>
    <p class="forgot"><a href="Default.aspx" id="forgotPwdLink">Back to login </a> 
      </p>



        <asp:Button class="button button-block" ID="btnSubmit" runat="server" 
             Text="Submit" onclick="btnSubmit_Click" 
                                             />
                                <input name="hidden" type="hidden" id="hidmsg" runat="server" />


     <%-- <button class="button button-block " id="Button1" name="btnSubmit"  /> Submit </button>--%>
   
    </div>
    </div>
         </div>
         <div class="footer">&copy; Copyright 2017 Invest Odisha, All Rights Reserved.</div>

    
        <!-- /.content-wrapper -->
        <!-- jQuery -->
        <script src="js/jquery-1.12.4.min.js" type="text/javascript"></script>
        <!-- bootstrap js -->
        <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script src="js/login.js" type="text/javascript"></script>
    </form>
</body>
</html>
