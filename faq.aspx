<%@ Page Language="C#" AutoEventWireup="true" CodeFile="faq.aspx.cs" Inherits="faq" %>

<!DOCTYPE html>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<html >
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
     <title>SWP(Single Window Portal)</title>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.faqlink').addClass('active');

            $('.dbodisha,.plEDBusiness').addClass('active');
            var $activePanelHeading = $('.panel-group .panel .panel-collapse.in').prev().addClass('active');  //add class="active" to panel-heading div above the "collapse in" (open) div
            $activePanelHeading.find('a').prepend('<span class="fa fa-minus"></span> ');  //put the minus-sign inside of the "a" tag
            $('.panel-group .panel-heading').not($activePanelHeading).find('a').prepend('<span class="fa fa-plus"></span> ');  //if it's not active, it will put a plus-sign inside of the "a" tag
            $('.panel-group').on('show.bs.collapse', function (e) {  //event fires when "show" instance is called
                //$('.panel-group .panel-heading.active').removeClass('active').find('.fa').toggleClass('fa-plus fa-minus'); - removed so multiple can be open and have minus sign
                $(e.target).prev().addClass('active').find('.fa').toggleClass('fa-plus fa-minus');
            });
            $('.panel-group').on('hide.bs.collapse', function (e) {  //event fires when "hide" method is called
                $(e.target).prev().removeClass('active').find('.fa').removeClass('fa-minus').addClass('fa-plus');
            });

        });

    </script>
    <style>
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        
        <div class="navigatorheader-div faqheadernav">
            <div class="">
                <div class="col-sm-12">
                    <%-- <h2><i class="fa fa-question-circle"></i> FAQ</h2>--%>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx" title="Home page"><i class="fa fa-home"></i></a></li>
                        <li>FAQ</li></ul>
                </div>
                <div class="clearfix">
                </div>
            </div>
        </div>
        <div class="content-form-section">
            <div class="col-sm-12">
                <div class="faq-sec">
                    <h3>
                        Frequently asked questions (FAQ)</h3>
                    <div class="list-type2" id="divabout" runat="server">
                        <%--<ol>
<li><span class="faqqs">Lorem Ipsum is simply dummy text of the printing and typesetting ?</span><span class="faqans">started with a desire to take an experience people love and make it better. To make it even simpler, more useful, and more enjoyable</span></li>
<li>
<span class="faqqs">To make it even simpler, more useful, and more enjoyable ?</span>
<span class="faqans">It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.</span>
</li>
<li>
<span class="faqqs">Lorem Ipsum is simply dummy text of the printing and typesetting ?</span>
<span class="faqans">started with a desire to take an experience people love and make it better. To make it even simpler, more useful, and more enjoyable.</span>
</li>
<li>
<span class="faqqs"> To make it even simpler, more useful, and more enjoyable ?</span>
<span class="faqans">started with a desire to take an experience people love and make it better. To make it even simpler, more useful, and more enjoyable.</span>
</li>

<li>
<span class="faqqs"> Lorem Ipsum is that it has a more-or-less normal distribution of letters ?</span>
<span class="faqans">It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.</span>
</li>

<li>
<span class="faqqs"> Lorem Ipsum is simply dummy text of the printing and typesetting ?</span>
<span class="faqans">started with a desire to take an experience people love and make it better. To make it even simpler, more useful, and more enjoyable.</span>
</li>

<li>
<span class="faqqs"> To make it even simpler, more useful, and more enjoyable ?</span>
<span class="faqans">It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.</span>
</li>

<li>
<span class="faqqs"> Lorem Ipsum is simply dummy text of the printing and typesetting ?</span>
<span class="faqans">started with a desire to take an experience people love and make it better. To make it even simpler, more useful, and more enjoyable.</span>
</li>

</ol>--%>
                    </div>
<%--<div class="panel-group faqaccordion" id="accordion" role="tablist" aria-multiselectable="false">
            <div class="panel">
              <div class="panel-heading" role="tab" id="tab1"> <a data-toggle="collapse" data-parent="#accordion" data-target="#lnk1" aria-expanded="false"><span class="number">1</span> <span class="panel-title">How do I register on the Single Window Portal?</span></a> </div>
              <div id="lnk1" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="tab1">
                <div class="panel-body">
                  <p>The applicant needs to click on “Investor Login” button on the top right section of the homepage of the portal. A new user registration form needs to be filled. All fields marked with ‘*’ are mandatory and must be filled before submitting the form. Upon submission, a One Time Password (OTP) will be sent to the mail address and phone number provided in the registration form. Once the email address and the phone number, both, are verified, the user will be registered.</p>
                </div>
              </div>
            </div>
            <div class="panel">
              <div class="panel-heading" role="tab" id="tab2"> <a data-toggle="collapse" data-parent="#accordion" data-target="#lnk2" aria-expanded="false" ><span class="number">2</span><span class="panel-title">I want to apply for more than one project. Do I need to create separate login for each project?</span></a> </div>
              <div id="lnk2" class="panel-collapse collapse" role="tabpanel" aria-labelledby="tab2">
                <div class="panel-body">
                  <p>No, the same login credential should be used for applying for multiple projects.</p>
                </div>
              </div>
            </div>
            <div class="panel">
              <div class="panel-heading" role="tab" id="tab3"> <a data-toggle="collapse" data-parent="#accordion" data-target="#lnk3" aria-expanded="false"><span class="number">3</span><span class="panel-title">I have login credential for CICG. Do I need to register again?</span></a> </div>
              <div id="lnk3" class="panel-collapse collapse" role="tabpanel" aria-labelledby="tab3">
                <div class="panel-body">
                  The single sign-on credential used for logging into CICG/APAA/GO iPLUS can be used for accessing the services of the single window portal and new registration is not required.
                </div>
              </div>
            </div>
<div class="panel">
              <div class="panel-heading" role="tab" id="tab4"> <a data-toggle="collapse" data-parent="#accordion" data-target="#lnk4" aria-expanded="false"><span class="number">4</span><span class="panel-title">Obtaining Electricity Connection</span></a> </div>
              <div id="lnk4" class="panel-collapse collapse" role="tabpanel" aria-labelledby="tab4">
                <div class="panel-body">
                  <ul>
                    <li>An industrial consumer has to submit only 2 documents for getting a new electricity
                      connection instead of 14 documents required earlier.</li>
                    <li>LT and HT electricity connection is now mandated to be provided to industries within
                      15 days and 30 days from application respectively.</li>
                    <li>The Energy Department now provides a fixed cost estimate for a new connection –
                      Rs. 6,000 per KVA if infrastructure is available and Rs. 11,300 per KVA if not,
                      instead of inspection based varied estimates which earlier resulted in substantial
                      delays and grievances.</li>
                    <li>Third party inspection of internal installations is now allowed to expedite issue
                      of new connection.</li>
                  </ul>
                </div>
              </div>
            </div>
          </div>--%>
                </div>
            </div>
         <%--   <div class="col-sm-4">
                <uc4:rightpanel ID="rightpanel" runat="server" />
            </div>--%>
            <div class="clearfix">
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
