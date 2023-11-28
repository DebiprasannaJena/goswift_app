<%@ Page Language="C#" AutoEventWireup="true" CodeFile="websitemaintenance.aspx.cs" Inherits="websitemen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="UTF-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
<meta name="description" content="Industries Department of the Government of Odisha has developed an online Single Window portal, GO SWIFT i.e. Government of Odisha – Single Window for Investor Facilitation & Tracking, to promote a conducive business environment through transparency and time-bound clearances ."/>
<meta name="author" content="IPICOL"/>
<meta name="keywords" content="GoSwift,Invest, Odisha, Investor, IPICOl, Industry, Industries, Team Odisha, Indudtrial Invest in Odisha,Single Window Portal ,Invest Odisha"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0 minimum-scale=1.0"/>
<link rel="shortcut icon" href="images/favicon.ico" type="image/x-icon" />
    <title>GO-SWIFT | Single Window Portal</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style>
    body{background:#e84c3d}
    .maintenance-sec{margin:10% auto;width:90%;}
    .maintenance-sec img{height:300px;margin-bottom1:10px}
     .maintenance-sec h1{font-size:3em;text-transform:uppercase;color:#fff;margin-bottom:30px}
        .maintenance-sec h2{font-size:2em;color:#fff;margin-bottom:30px}
      .maintenance-sec p{font-size:1.2em;color:#fff}
    </style>
</head>
<body>
    <form id="form1" runat="server">
   
   <center>
   <div class="maintenance-sec">
   
   <h1>Site Maintenance</h1>
       <h2>Site is temporarily unavailable due to server migration activity.</h2>
   <p>
      <%-- We sincerely apologize for the inconvenience. Our site is currently undergoing scheduled maintenance and upgrades, But will return shortly.
   Thank you for Your Patient.--%>
       <marquee behavior="alternate">
           <center>
       We are currently undergoing scheduled migration activity from existing server to cloud server. 
       The downtime will start from 20:00 PM on 26th July,2019  until 18:00 PM on 27th July,2019.
     <br /> Sorry for any inconvenience caused.
        Thank you for your patience.</center>

       </marquee>
   </p>
   </div>
   </center>
    </form>
</body>
</html>
