<%@ Control Language="C#" AutoEventWireup="true" CodeFile="webfooter.ascx.cs" Inherits="Application_includes_footer" %>
<section class="gofootmenu">
    <div class="container" id="divFooterId" runat="server">
       <%-- <ul>
            <li><a href="#">Home</a></li>
            <li><a href="#">FAQ</a></li>
            <li><a href="#">Feedback</a></li>
            <li><a href="https://investodisha.gov.in/disclaimer/">Disclaimer</a></li>
            <li><a href="#">Contact Us</a></li>
            <li><a href="#">Privacy Statement</a></li>
        </ul>--%>
    </div>
</section>
<section class="gofooter">
    <div class="container">
         <div class="col-sm-7">
        <p id="copyright">
            Copyright &copy; <span id="year"></span> Invest Odisha, All Rights Reserved.
            <a href="#">Term of Use</a>
            <a href="#">Rate this Website</a>
        </p>
    </div>

    <!-- JavaScript code to update the year dynamically -->
    <script>
        // Get the current year
        var currentYear = new Date().getFullYear();
        // Update the year in the HTML
        document.getElementById('year').textContent = currentYear;
    </script>
         
        <div id="divLastUpdate" style="text-align:right;color:#aaaeb7" runat="server"></div>
        
        <div id="divUpdateText" style="text-align:right;color:#aaaeb7" runat="server">The dashboard information is updated on a daily basis</div>
        <div id="div1" style="text-align:right;color:#aaaeb7">This site is best viewed in 1024 * 768 resolution with latest version of Chrome, Firefox, Safari and Internet Explorer</div>
        <br />
        <div class="col-sm-5">
           <ul>
            <li><a href="https://www.facebook.com/InvestOdisha/" target="_blank"><i class="fa fa-facebook"></i></a></li>
            <li><a href="https://twitter.com/InvestInOdisha"  target="_blank"><i class="fa fa-twitter"></i></a></li>
            <li><a href="https://in.linkedin.com/company/investodisha" target="_blank"><i class="fa fa-linkedin"></i></a></li>
            <li><a href="https://youtu.be/Fb5txS4iciQ" target="_blank"><i class="fa fa-youtube"></i></a></li>
        </ul>
        </div>
    </div>

</section>