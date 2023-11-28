<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDBusiness.aspx.cs" Inherits="EDBusiness" %>

<!DOCTYPE html>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<html>
<head id="Head1" runat="server">
<uc1:doctype ID="doctype" runat="server" />
<link href="css/custom.css" rel="stylesheet" type="text/css" />
 <title>SWP(Single Window Portal)</title>
<script type="text/javascript">
        $(document).ready(function () {
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

</head>
<body>
<form id="form1" runat="server">
  <div class="container">
    <uc2:header ID="header" runat="server" />
    <div class="navigatorheader-div aboutheadernav">
      <div class="col-sm-12">
        <ul class="breadcrumb">
          <li><a href="Default.aspx" title="Home page"><i class="fa fa-home"></i></a></li>
          <li>Doing Business in Odisha</li>
        </ul>
      </div>
      <div class="clearfix"> </div>
    </div>
    <div class="content-form-section">
      <div class="col-sm-12">
        <div class="aboutcontent-sec">
          <h3> Ease of Doing Business</h3>
          <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="false">
            <div class="panel">
              <div class="panel-heading" role="tab" id="tab1"> <a data-toggle="collapse" data-parent="#accordion" data-target="#lnk1" aria-expanded="false"><span class="panel-title">Single Window</span></a> </div>
              <div id="lnk1" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="tab1">
                <div class="panel-body">
                  <ul>
                    <li>An online single window portal, GO SWIFT, has been set up to facilitate online application submission, payments, tracking of status, approvals and final issuance of the certificate.
</li>
                    <li>Approvals/Clearances for 32 services from 15 State Government departments are now provided through the GO SWIFT Portal.</li>
                    <li>Queries/clarifications related to an investor's application will be sought only once and within 7 days of receiving the complete application.
</li>
                    <li>Incentives of all applicable industrial/sectoral policies can now be applied and availed through the GO SWIFT portal.</li>
                    <li>Real time status of applications submitted, being processed and certificates issued by the different departments is available online on the portal.
</li>
                    <li>Timelines mandated by the Orissa Right to Public Services Act (ORTPSA 2012) have been mapped for each service to ensure that all approvals are delivered in a time bound manner to the investors.
</li>
                    <li>Other existing applications of the State including GO PLUS, Central Inspection Framework, Automated Post Allotment Application, CSR Portal and State Project Monitoring Group Portal have also been integrated with the GO SWIFT. Access to all these services is now available through a single sign-on user credential.</li>
                  </ul>
                </div>
              </div>
            </div>
            <div class="panel">
              <div class="panel-heading" role="tab" id="tab2"> <a data-toggle="collapse" data-parent="#accordion" data-target="#lnk2" aria-expanded="false" ><span class="panel-title">Construction Permit Enablers</span></a> </div>
              <div id="lnk2" class="panel-collapse collapse" role="tabpanel" aria-labelledby="tab2">
                <div class="panel-body">
                  <ul>
                    <li>A comprehensive formal building code applicable to the entire State has been enacted covering comprehensive rules, relating to receipt of applications in Common Form for the purpose of obtaining permissions for building operation, land development, and the procedure for disposal to be followed as well as provisions for registration and accreditation of technical persons.</li>
                    <li>An online Building Plan approval system is in place wherein applicants can apply for construction, completion and occupancy permits and make the required payments online. The system auto-scrutinizes the uploaded building plans from compliance perspective according to the applicable building code.</li>
                    <li>An online system has been developed which allows approvals based on third party certification (during construction and/or completion stage, as applicable) of structural design and architectural drawings by authorized structural engineers and architects, respectively, across all urban areas and IDCs.</li>
                    <li>Legally valid master plans/zonal plans/land use plans for all urban areas are available online in public domain</li>
                    <li>A dedicated conflict resolution mechanism for land and construction permits has been established.</li>
                  </ul>
                </div>
              </div>
            </div>
            <div class="panel">
              <div class="panel-heading" role="tab" id="tab3"> <a data-toggle="collapse" data-parent="#accordion" data-target="#lnk3" aria-expanded="false"><span class="panel-title">Obtaining Electricity Connection</span></a> </div>
              <div id="lnk3" class="panel-collapse collapse" role="tabpanel" aria-labelledby="tab3">
                <div class="panel-body">
                  <ul>
                    <li>The department has implemented a system that allows online application submission, payment and tracking of status without the need for a physical touch point for document submission for new electricity connection and has mandated that all applications are to be submitted online.</li>
                    <li>An industrial consumer has to submit only 2 documents for getting a new electricity connection instead of 14 documents required earlier.</li>
                    <li>LT and HT electricity connections are now mandated to be provided to industries within 15 days and 30 days, respectively, from the date of submission of complete application.</li>
                    <li>The department has notified that charged electrical connections (up to 150 KVA) is provided within 7 days (where no ‘Right of Way’ (RoW) is required) and in 15 days where RoW is required from the concerned agencies.</li>
                    <li>The Energy Department now provides a fixed cost estimate for a new connection – Rs. 6,000 per KVA if infrastructure is available and Rs. 11,300 per KVA if not, instead of inspection based varied estimates which earlier resulted in substantial delays and grievances.</li>
                     <li>Third party inspection of internal installations is now allowed to expedite issue of new connection.</li>
                      <li>OERC has fixed the total outage cap for a year. All DISCOMs in the State publish quarterly data regarding total duration and frequency of outages online in the public domain.</li>
                       <li>The State has implemented PLCC band automated tools for monitoring outages and restoring service in all Industrial areas of the State included under the R-ARDRP scheme.</li>
                        
                  </ul>
                </div>
              </div>
            </div>
            <div class="panel">
              <div class="panel-heading" role="tab" id="tab4"> <a data-toggle="collapse" data-parent="#accordion" data-target="#lnk4" aria-expanded="false"><span class="panel-title">Labour Regulation</span></a> </div>
              <div id="lnk4" class="panel-collapse collapse" role="tabpanel" aria-labelledby="tab4">
                <div class="panel-body">
                  <ul>
                    <li>Application for Licenses/Registrations and subsequent renewals under all labour laws, Factories Act, 1948 and the Boilers Act, 1923, can now be made online on the GO SWIFT portal.</li>
                    <li>Approved certificates, licenses, clearances and NoCs by the Department are made available in the public domain.</li>
                    <li>Provision for third party audit of factories and industries has been introduced, leading to reduced frequency of inspections by the department.</li>
                    <li>Factory License and all subsequent renewals are now issued with a validity of 10 years or more.</li>
                    <li>Employers can now file single integrated return under all applicable labour laws online on the CICG Portal.</li>
                    <li>Low and Medium risk industries with a history of satisfactory compliance have been exempted from annual labour compliance inspections. Instead, such industries can opt for the Voluntary Compliance Scheme for self-certification under which the department will conduct physical inspection under all Labour laws once in 3 years. </li>
                    <li>The department has introduced provision for third party certification of boilers during use under section 34(3) of the Boilers Act, 1923, by persons having requisite qualification and experience.</li>
                  </ul>
                </div>
              </div>
            </div>
            <div class="panel">
              <div class="panel-heading" role="tab" id="tab5"> <a data-toggle="collapse" data-parent="#accordion" data-target="#lnk5" aria-expanded="false"><span class="panel-title">Availability of Land</span></a> </div>
              <div id="lnk5" class="panel-collapse collapse" role="tabpanel" aria-labelledby="tab5">
                <div class="panel-body">
                  <ul>
                    <li>Detailed information about availability of industrial plots is available online. It also provides location specific attributes in terms of connectivity, rail and road linkages and other physical, health and educational infrastructure available in the vicinity of a selected industrial land.</li>
                    <li>Information on zoning of the industrial land in terms of environmental categories i.e. Green, Orange and Red, is available online to assist an investor in deciding on a suitable location for investment based on the proposed business activities.</li>
                    <li>Detailed information about key attributes of existing operational industries in a particular cluster such as sector of operation, products, capacity, employment, raw material linkages etc. is also made available online.</li>
                  </ul>
                </div>
              </div>
            </div>
            <div class="panel">
              <div class="panel-heading" role="tab" id="tab6"> <a data-toggle="collapse" data-parent="#accordion" data-target="#lnk6" aria-expanded="false"><span class="panel-title">Environmental Clearance</span></a> </div>
              <div id="lnk6" class="panel-collapse collapse" role="tabpanel" aria-labelledby="tab6">
                <div class="panel-body">
                  <ul>
                    <li>Green category industries setting up in the State are now completely exempted from the consent management process.</li>
                    <li>Validity of Consent to Establish (CTE) and Consent to Operate (CTO) is now increased to 5 years or more for all types of Industries.</li>
                    <li>Auto-renewal of CTE and CTO is now allowed based on self/third-party certification reducing the cost of paperwork and inspections in many cases where there are no change in pollution levels or activities.</li>
                    <li>Orange category industries in State are exempted from departmental inspections, if they provide an audit report from an empaneled third-party agency.</li>
                    <li>The consent management system has been made completely online with provision to make online application, payment, approval, tracking of application and download of approved certificate.</li>
                    <li>The department has also provisioned for third party certifications instead of departmental inspection under all the pollution laws for medium risk industries.</li>
                  </ul>
                </div>
              </div>
            </div>
            <div class="panel">
              <div class="panel-heading" role="tab" id="tab7"> <a data-toggle="collapse" data-parent="#accordion" data-target="#lnk7" aria-expanded="false"><span class="panel-title">Inspection Reforms</span></a> </div>
              <div id="lnk7" class="panel-collapse collapse" role="tabpanel" aria-labelledby="tab7">
                <div class="panel-body">
                  <ul>
                    <li>An online system has replaced manual scheduling of inspections with computerized synchronized inspections for Labour Directorate, Directorate of Factories & Boilers, Odisha State Pollution Control Board, and Legal Metrology Department.</li>
                    <li>Detailed inspection Standard Operating Procedure (SoP), Checklists and Inspection Formats have been made publicly available for the benefit of industrial units. Inspectors have been instructed to follow the SoP and not to seek any information beyond this checklist.</li>
                    <li>Identification of industries for inspections and preparation of Inspection Schedule is done based on a risk-based computerised process. All industries in the State have been assigned a risk rating, based on a defined criteria, which is also publicly available online.</li>
                    <li>Allocation of inspectors is carried out by in a randomized manner. The System ensures that same inspector does not inspect the same industry in two consecutive inspections.</li>
                    <li>The schedule for inspections for all industries in the State is generated three months in advance. The system sends automated e-mail and SMS updates with the inspection details including date and time of inspection, name of inspector etc. to the industrial units in advance.</li>
                    <li>Inspection reports are made available online within 48 hours of completion of inspection.  </li>
                    <li>The industrial units have the provision to provide feedback on the process of inspection and/or the inspection report. This feedback is reported directly to the Head of the Department for review every week.</li>
                  </ul>
                </div>
              </div>
            </div>
            <div class="panel">
              <div class="panel-heading" role="tab" id="tab8"> <a data-toggle="collapse" data-parent="#accordion" data-target="#lnk8" aria-expanded="false"><span class="panel-title">Access to Information</span></a> </div>
              <div id="lnk8" class="panel-collapse collapse" role="tabpanel" aria-labelledby="tab8">
                <div class="panel-body">
                  <ul>
                    <li>Info Wizard: An online system where an investor can key in specific details (such as type of industry, number of employees, location etc.) and obtain information regarding all State approvals applicable to starting operations of her/his business/industrial unit</li>
                    <li>A comprehensive checklist of all required pre-establishment as well as pre-operation Licenses, Registrations, No Objection Certificates and other mandatory State approvals required for setting up of a business in the State has been made available online.</li>
                    <li>Procedures and Timelines for all Licenses, Registrations, No Objection Certificates and State approvals/clearances has been made available online.</li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <%--<div class="col-sm-4">
        <uc4:rightpanel ID="rightpanel" runat="server" />
      </div>--%>
      <div class="clearfix"> </div>
    </div>
  </div>
  <uc3:footer ID="footer" runat="server" />
</form>
</body>
</html>
