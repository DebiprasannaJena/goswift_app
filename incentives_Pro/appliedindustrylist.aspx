<%@ Page Language="C#" AutoEventWireup="true" CodeFile="appliedindustrylist.aspx.cs"
    Inherits="incentives_appliedindustrylist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />

    <script>
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            });
        });
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:header ID="header" runat="server" />
    <div class="registration-div investors-bg">
        <div id="exTab1" class="container">
            <div class="investrs-tab">
                <ul class="nav nav-pills">
                    <li class="menudashboard"><a href="javascript:void(0)"><i class="fa fa-tachometer"></i>
                        Dashboard</a> </li>
                    <li class="menuprofile"><a href="../InvesterProfile.aspx"><i class="fa fa-user"></i>
                        Investor Profile</a> </li>
                    <li class="menuproposal"><a href="../Proposals.aspx"><i class="fa fa-briefcase"></i>
                        Proposals</a> </li>
                    <li class="menuservices"><a href="../DepartmentClearance.aspx"><i class="fa fa-wrench">
                    </i>Services</a> </li>
                    <%--  <li><a href="IncentiveCalculator.aspx">Incentive Calculator</a> </li>--%>
                    <li class="menuincentive"><a href="incentiveoffered.aspx"><i class="fa fa-money"></i>
                        Incentive</a> </li>
                </ul>
            </div>
            <div class="tab-content clearfix">
                <div class="tab-pane active" id="1a">
                    <div class="form-sec">
                        <div class="innertabs">
                            <ul class="nav nav-pills pull-right">
                                <li><a href="incentiveoffered.aspx">Incentive Offered</a></li>     
                                 <%if (Session["InvestorId"].ToString() == "49")   { %>   
                                  <li class="active"><a href="IncentiveCalc.aspx">Apply For incentive</a></li> 
                                 <% }  else { %> 
                                                   
                                <li class="active"><a href="appliedindustrylist.aspx">Apply For incentive</a></li>
                                  <%}%>

                                <li><a href="ViewApplicationStatus.aspx">View Application Status</a></li>
                            </ul>
                            <div class="clearfix">
                            </div>
                        </div>
                        <div class="form-header">
                            <div class="iconsdiv">
                                <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                    <i class="fa fa-print"></i></a><a href="javascript:void(0);" title="Export to Excel"
                                        id="A1" class="pull-right printbtn"><i class="fa fa-file-excel-o"></i></a>
                                <a href="javascript:void(0);" title="Delete" id="A2" class="pull-right printbtn"><i
                                    class="fa fa-trash-o"></i></a>
                            </div>
                            <h2>
                                Your Units
                            </h2>
                        </div>
                        <div class="form-body">
                            <div class="row">
                                <div class="col-sm-12">                              
                                    <div class="details-section">
                                        <table class="table table-bordered">
                                            <tr>
                                                <th>
                                                    <input type="checkbox" value="">
                                                </th>
                                                <th>
                                                    Industry Name
                                                </th>
                                                <th>
                                                    EIN/IEM
                                                </th>
                                                <th>
                                                    PC
                                                </th>
                                                <th>
                                                    District
                                                </th>
                                                <th>
                                                    Address
                                                </th>
                                                <th>
                                                    Industry Code
                                                </th>
                                                <th>
                                                    Enterprise Type
                                                </th>
                                                <th>
                                                    Sector
                                                </th>
                                                <th>
                                                    Drafted Applications
                                                </th>
                                                <th width="120px">
                                                    Action
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="checkbox" value="">
                                                </td>
                                                <td>
                                                    Jagannath Texttiles
                                                </td>
                                                <td class="text-center">
                                                    <a href="javascript:void(0)" title="EIN/IEM Document" target="_blank"><i class="fa fa-file-text-o">
                                                    </i></a>
                                                </td>
                                                <td>
                                                    <a href="javascript:void(0)" title="Production Certificate" target="_blank"><i class="fa fa-file-text-o">
                                                    </i></a>
                                                </td>
                                                <td>
                                                    Koraput
                                                </td>
                                                <td>
                                                    #7878,Plot No. 144, Lane No.-2
                                                </td>
                                                <td>
                                                    12-56-23-56-23456
                                                </td>
                                                <td>
                                                    Manufacturing
                                                </td>
                                                <td>
                                                    Apparel
                                                </td>
                                                <td class="text-center">
                                                    <a href="draftedapplicationdetails.aspx" title="Drafted Applications"><span class="badge">
                                                        3</span></a>
                                                </td>
                                                <td>
                                                    <%--<a class="" href="unitdetails.aspx" title="Check Eligibility">Check Eligibility</a>--%>
                                                    <a data-toggle="modal" data-target="#undertakingipr2015" title="Check Eligibility">Check
                                                        Eligibility</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="checkbox" value="">
                                                </td>
                                                <td>
                                                    JRD Farma &amp; Research
                                                </td>
                                                <td class="text-center">
                                                    <a href="javascript:void(0)" title="EIN/IEM Document" target="_blank"><i class="fa fa-file-text-o">
                                                    </i></a>
                                                </td>
                                                <td>
                                                    <a data-toggle="modal" data-target="#myModal3" title="Apply">Apply</a>
                                                </td>
                                                <td>
                                                    Khorda
                                                </td>
                                                <td>
                                                    Near NH5 Khurda,Plot No. 145, Lane No.-2
                                                </td>
                                                <td>
                                                    12-56-23-56-23466
                                                </td>
                                                <td>
                                                    Manufacturing
                                                </td>
                                                <td>
                                                    Bio-Technology
                                                </td>
                                                <td class="text-center">
                                                    <a href="draftedapplicationdetails.aspx" title="Drafted Applications"><span class="badge">
                                                        1</span></a>
                                                </td>
                                                <td>
                                                    <%--<a class="" href="unitdetailsforpccleared.aspx" title="Check Eligibility">Check Eligibility</a>--%>
                                                    <a data-toggle="modal" data-target="#undertakingipr20152" title="Check Eligibility">
                                                        Check Eligibility</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="checkbox" value="">
                                                </td>
                                                <td>
                                                    JRD Farma &amp; Research2
                                                </td>
                                                <td class="text-center">
                                                    <a data-toggle="modal" data-target="#myModal4" title="Apply">Apply</a>
                                                </td>
                                                <td class="text-center">
                                                    <a href="javascript:void(0)" title="EIN/IEM Document" target="_blank">-</a>
                                                </td>
                                                <td>
                                                    Khorda
                                                </td>
                                                <td>
                                                    Near NH5 Khurda,Plot No. 145, Lane No.-2
                                                </td>
                                                <td>
                                                    12-56-23-56-23466
                                                </td>
                                                <td>
                                                    Manufacturing
                                                </td>
                                                <td>
                                                    Bio-Technology
                                                </td>
                                                <td class="text-center">
                                                    <a href="javascript:void(0)" title="Drafted Applications"><span class="badge">0</span></a>
                                                </td>
                                                <td>
                                                    <%--<a class="" href="unitdetailsforEINIEM.aspx" title="Check Eligibility">Check Eligibility</a>--%>
                                                    <a data-toggle="modal" data-target="#undertakingipr20153" title="Check Eligibility">
                                                        Check Eligibility</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <% } %>
                                </div>
                            </div>
                        </div>
                        <div class="form-footer">
                            <div class="row">
                                <div class="col-sm-12 text-right">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModal3" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body text-center">
                    <div class="form-group">
                        <label>
                            Redirect to Portal for Application for PC</label>
                        <button type="button" class="btn btn-primary" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModal4" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body text-center">
                    <div class="form-group">
                        <label>
                            Redirect to Portal for Application for EIN/IEM</label>
                        <button type="button" class="btn btn-primary" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingipr2015" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part the negative unit
                        list under <strong>IPR 2015</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>Units engaged in manufacturing and/or servicing activity belonging to following
                            categories.</li>
                        <li>Industries listed under the first schedule of the industries Department and regulation
                            Act,1951 and manufacturing enterprise classified under MSME.</li>
                        <li>Industries falling within the purview of the following Boards and public Agencies.
                            <ol>
                                <li>Coir Board </li>
                                <li>Silk Board</li>
                                <li>All India handloom and Handicraft Board</li>
                                <li>Khadi and village industries Commission/Board</li>
                                <li>Any other Agency con situation by Government for industrial department.</li>
                            </ol>
                        </li>
                        <li>Infrastructure projects only for the purpose of determining applicable land rate
                        </li>
                        <li>Service sector projects under priority sector.</li>
                        <li>Service sector other than priority sector only for the purpose of applicable land
                            rate.</li>
                        <li>Industrial unit will not include non-manufacturing/servicing industries except:
                            <ol>
                                <li>General workshops including repair workshops having investment in plant & machinery
                                    of Rs.50 Lakhs and above and running with power.</li>
                                <li>Cold storage and seafood freezing units having investment of Rs. 25.00 Lakhs and
                                    above.</li>
                                <li>Electronics repair and maintenance units for professional grade equipment and computer
                                    software ,ITES/BPO and related services with investment of Rs. 25,00 Lakhs and above.</li>
                                <li>Technology Development Laboratory/prototype Development centre/Research & Development
                                    with investment of Rs.25.00 Lakhs and above.</li>
                                <li>Printing press with investment in plant and machinery of Rs. 50.00 Lakhs and above.</li>
                                <li>Laundry/Dry Cleaning with investment in plant and machinery/equipment of Rs. 25.00
                                    Lakhs and above. </li>
                            </ol>
                        </li>
                        <li>The following units shall neither be eligible for fiscal incentives specified under
                            this IPR nor for allotment of land at concessional rates in the state, but shall
                            be eligible for investment facilitation ,allotment of land under normal rules at
                            benchmark value/market rate and recommendation to the financial institutions for
                            term loan and working capital and for recommendation, if necessary to the power
                            Distribution companies:
                            <ol>
                                <li>Hullers and Rice mills with investment in plant and machinery of less than Rs. 25
                                    Lakhs for industrially backward districts and less than Rs. 1 crore for other districts</li>
                                <li>Four mills including manufacture of besan, Pulse mills and chuda mills except investment
                                    in plant & machinery of more than 25 lakhs for industrially backward districts and
                                    less than Rs. 1 crore for other districts(excluding Roller Flour mills)
                                    <ol>
                                        <li>Processing of Spices with investment in plant & machinery with less than Rs.10 Lakhs
                                            for industrially backward districts and less than two crore rupees for other districts</li>
                                        <li>
                                        Units without Spice-mark or Agmark
                                    </ol>
                                </li>
                                <li>Confectionary with investment in plant and machinery with less than Rs.10 Lakhs
                                    for industrially backward districts and less than two crore rupees for other districts.</li>
                                <li>Oil mills with expellers including oil processing, filtering , de-coloring ,coloring
                                    ,refining of edible oils and hydro-generation thereof except investment in plant
                                    and machinery of RS. 10 Lakhs in industrial backward areas.</li>
                                <li>Preparation of sweets and savories etc</li>
                                <li>Bread making(excluding mechanized bakery)</li>
                                <li>Mixture.Bhujia and chanachur preparation units</li>
                                <li>Manufacture of ice candy</li>
                                <li>Manufacture and processing of betel nuts</li>
                                <li>Hacheries,Piggeries,rabbit or Broiler farming </li>
                                <li>Standalone sponge iron plants</li>
                                <li>Iron and steel processors, such as cutting of sheets,bars,angles,coils,M.S. sheets,
                                    recoiling, straightening,corrugating,drophammer units etc with low value addition</li>
                                <li>Cracker-making units</li>
                                <li>Tyre retreading units with investment in plant and machinery of less Rs.20 Lakhs</li>
                                <li>Stone crushing units</li>
                                <li>Coal/coke screening coal /coke Briquetting.</li>
                                <li>Production of firewood and charcoal.</li>
                                <li>Painting and spray-painting units with investment in plant and machinery of less
                                    than Rs. 20 Lakhs.</li>
                                <li>Units for physical mixing of fertilizers.</li>
                                <li>Brick- making units (except units making refractory bricks and those making bricks
                                    from flyash, red mud and similar industrial waste not less than 25% as base martial).</li>
                                <li>Manufacturing of tarpaulin out of canvas cloth with investment in plant and machinery
                                    of less than Rs. 20 Lakhs.</li>
                                <li>Saw mills, sawing of timber.</li>
                                <li>Carpentry, joinery and wooden furniture making except when part of a wood based
                                    cluster of at least 20 units.</li>
                                <li>Drilling rigs ,Bore-wells and Tube-wells</li>
                                <li>Units for mixing or blending/packaging of tea.</li>
                                <li>Units for cutting raw tobacco and sprinkling jiggery for chewing purpose and Gudakhu
                                    manufacturing units.</li>
                                <li>Units for bottling of medicines.</li>
                                <li>Bookbinding/Rubber stamp making/making notebooks, exercise notebook s and envelopes.</li>
                                <li>Distilled water units</li>
                                <li>Tailoring (other than readymade garment manufacturing units )</li>
                                <li>Repacking /stitching/printing of woven sacks out of woven fabrics.</li>
                                <li>Pre-Processing of oil seeds-Decorticating, expelling, crushing, parching and frying.</li>
                                <li>Aerated water and soft drinks units</li>
                                <li>Bottling units or any activity in respect of IMFL or liquor of any kind.</li>
                                <li>Size reducing/size separating units/ Grinding / mixing units with investment in
                                    plant and machinery of less than ten crore rupees except manufacturing of cement
                                    with clinker.</li>
                                <li>Polythene less than 40 micron in thickness /recycling of plastic materials.</li>
                                <li>Thermal power plants.</li>
                                <li>Repackaging units.</li>
                            </ol>
                        </li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided informatin will be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a data-toggle="modal" class="btn btn-success" data-dismiss="modal" data-target="#undertakingfoodprocessing">
                        Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingfoodprocessing" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part the negative unit
                        list under <strong>Food Processing</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>Hullers and rice mills with investment in plant and machinery of less than Rs. 25
                            lakhs for industrially backward districts and less than 1 crore for other.</li>
                        <li>Flour mills including manufacture of besan , pulse mills and chuda mills except
                            investment in plant and machinery of less than Rs.25 lakhs far industrially backward
                            districts and less than Rs. 1 crore for other areas (excluding rolling mills).
                        </li>
                        <li>
                            <ol>
                                <li>Processing of spices with investment in plant and machinery with less than Rs. 10
                                    lakh for industrially backward districts and less than Rs.2 crore for other areas.</li>
                                <li>Units without spice-mark or Ag-mark.</li>
                            </ol>
                        </li>
                        <li>Confectionary with investment in plant and machinery with less than Rs.10 lakhs
                            for industrially backward districts and less than Rs. 2 crore for other areas.
                        </li>
                        <li>Oil mills with expellers including oil processing , filtering, decoloring , coloring
                            refining of edible oils and hydrogenation thereof except investment in plant and
                            machinery more than Rs.10 lakhs in industrially backward districts </li>
                        <li>Preparation of sweets and savories excepts investment in plant and machinery of
                            less than Rs.10 lakhs for industrially backward districts and less than Rs.50 lakhs
                            for other areas.</li>
                        <li>Bread-making (excluding mechanized bakery)</li>
                        <li>Mixture, bhujia and chanachur preparation units.</li>
                        <li>Manufacture of ice candy</li>
                        <li>Manufacture and processing of betel units.</li>
                        <li>Hatcheries, piggeries, Rabbit or Broiler farming.</li>
                        <li>Units for mixing or blending /packaging of tea.</li>
                        <li>Units for cutting raw tobacco and sprinkling jaggery for chewing purposes and guddkhu
                            manufacturing units.</li>
                        <li>Pre-processing of oil seeds: decorticating, expelling, crushing, parching and frying.</li>
                        <li>Aerated water and soft drink units.</li>
                        <li>Bottling units or any activity in respect to IMFL or liquor of any kind.</li>
                        <li>Re-packaging units.</li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided information is be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a data-toggle="modal" class="btn btn-success" data-dismiss="modal" data-target="#undertakingMSME">
                        Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingMSME" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part the negative unit
                        list under <strong>MSME Policy</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>All service enterprises.</li>
                        <li>Hullers and rice mills with investment in plant and machinery of less than Rs. 25
                            lakhs for industrially backward districts and less than 1 crore for other.</li>
                        <li>Flour mills including manufacture of besan , pulse mills and chuda mills except
                            investment in plant and machinery of less than Rs.25 lakhs far industrially backward
                            districts and less than Rs. 1 crore for other areas (excluding rolling mills).</li>
                        <li>
                            <ol>
                                <li>Processing of spices with investment in plant and machinery with less than Rs. 10
                                    lakh for industrially backward districts and less than Rs.2 crore for other areas.</li>
                                <li>Units without spice-mark or Ag-mark.</li>
                            </ol>
                        </li>
                        <li>Confectionary with investment in plant and machinery with less than Rs.10 lakhs
                            for industrially backward districts and less than Rs. 2 crore for other areas.
                        </li>
                        <li>Oil mills with expellers including oil processing , filtering, decoloring , coloring
                            refining of edible oils and hydrogenation thereof except investment in plant and
                            machinery more than Rs.10 lakhs in industrially backward district.</li>
                        <li>Preparation of sweets and savories excepts investment in plant and machinery of
                            less than Rs.10 lakhs for industrially backward districts and less than Rs.50 lakhs
                            for other areas.</li>
                        <li>Bread-making (excluding mechanized bakery)</li>
                        <li>Mixture, bhujia and chanachur preparation units.</li>
                        <li>Manufacture of ice candy</li>
                        <li>Manufacture and processing of betel units.</li>
                        <li>Hatcheries, piggeries, Rabbit or Broiler farming.</li>
                        <li>Standalone Sponge iron plants.</li>
                        <li>“iron and steel processors” such as cutting of sheets, bars, angles, coils, M.S.
                            sheets, recoiling straightening , corrugating , drop hammer units etc. with low
                            value addition.</li>
                        <li>Cracker-making units.</li>
                        <li>Tyre retreading units with investment in plant and machinery of less than Rs.20
                            lakhs.</li>
                        <li>Stone crushing units.</li>
                        <li>Coal/coke screening. Coal washing ,coal/coke briquetting.</li>
                        <li>Production of firewood and charcoal.</li>
                        <li>Painting and spray-painting units with investment in plant and machinery of less
                            than Rs.20 lakhs.</li>
                        <li>Units for physical mixing of fertilizer.</li>
                        <li>Brick-making units (except units making refractory bricks and those making bricks
                            from fly ash, red mud and industrial waste not less than 25% as base material).</li>
                        <li>Manufacturing of tarpaulin out of canvas cloth with investment in plant and machinery
                            of less than Rs. 20 lakh.</li>
                        <li>Saw mill, sawing of timber.</li>
                        <li>Carpentry, joinery and wooden furniture making except when part of a wood based
                            cluster of at least 20 units.</li>
                        <li>Drilling rigs, Bore-wells and tube wells.</li>
                        <li>Units for mixing or blending /packaging of tea.</li>
                        <li>Units for cutting raw tobacco and sprinkling jiggery for chewing purposes and gudakhu
                            manufacturing units.</li>
                        <li>Units for bottling of medicines.</li>
                        <li>Bookbinding / Rubber stamp making /making notebooks, exercise notebooks and envelopes.</li>
                        <li>Distilled water units.</li>
                        <li>Tailoring (other than readymade garment manufacturing units).</li>
                        <li>Repacking /stitching /printing of woven sacks out of woven fabrics.</li>
                        <li>Pre-processing of oil seeds-decorticating , expelling ,crushing ,parching and frying.</li>
                        <li>Aerated water and soft drink units.</li>
                        <li>Bottling units or any activity in respect to IMFL or liquor of any kind.</li>
                        <li>Size reducing/size separating units /grinding/mixing units with investment in plant
                            and machinery of less than 10 crore except manufacturing of cement with clinker.</li>
                        <li>Polythene less than 40 micron in thickness/recycling of plastic materials</li>
                        <li>Thermal power plant.</li>
                        <li>Re-packaging units.</li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided information is be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a href="unitdetails.aspx" class="btn btn-success" title="Submit">Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingipr20152" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part thre negative unit
                        list under <strong>IPR 2015</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>Units engaged in manufacturing and/or servicing activity belonging to following
                            categories.</li>
                        <li>Industries listed under the first schedule of the industries Department and regulation
                            Act,1951 and manufacturing enterprise classified under MSME.</li>
                        <li>Industries falling within the purview of the following Boards and public Agencies.
                            <ol>
                                <li>Coir Board </li>
                                <li>Silk Board</li>
                                <li>All India handloom and Handicraft Board</li>
                                <li>Khadi and village industries Commission/Board</li>
                                <li>Any other Agency con situation by Government for industrial department.</li>
                            </ol>
                        </li>
                        <li>Infrastructure projects only for the purpose of determining applicable land rate
                        </li>
                        <li>Service sector projects under priority sector.</li>
                        <li>Service sector other than priority sector only for the purpose of applicable land
                            rate.</li>
                        <li>Industrial unit will not include non-manufacturing/servicing industries except:
                            <ol>
                                <li>General workshops including repair workshops having investment in plant & machinery
                                    of Rs.50 Lakhs and above and running with power.</li>
                                <li>Cold storage and seafood freezing units having investment of Rs. 25.00 Lakhs and
                                    above.</li>
                                <li>Electronics repair and maintenance units for professional grade equipment and computer
                                    software ,ITES/BPO and related services with investment of Rs. 25,00 Lakhs and above.</li>
                                <li>Technology Development Laboratory/prototype Development centre/Research & Development
                                    with investment of Rs.25.00 Lakhs and above.</li>
                                <li>Printing press with investment in plant and machinery of Rs. 50.00 Lakhs and above.</li>
                                <li>Laundry/Dry Cleaning with investment in plant and machinery/equipment of Rs. 25.00
                                    Lakhs and above. </li>
                            </ol>
                        </li>
                        <li>The following units shall neither be eligible for fiscal incentives specified under
                            this IPR nor for allotment of land at concessional rates in the state, but shall
                            be eligible for investment facilitation ,allotment of land under normal rules at
                            benchmark value/market rate and recommendation to the financial institutions for
                            term loan and working capital and for recommendation, if necessary to the power
                            Distribution companies:
                            <ol>
                                <li>Hullers and Rice mills with investment in plant and machinery of less than Rs. 25
                                    Lakhs for industrially backward districts and less than Rs. 1 crore for other districts</li>
                                <li>Four mills including manufacture of besan, Pulse mills and chuda mills except investment
                                    in plant & machinery of more than 25 lakhs for industrially backward districts and
                                    less than Rs. 1 crore for other districts(excluding Roller Flour mills)
                                    <ol>
                                        <li>Processing of Spices with investment in plant & machinery with less than Rs.10 Lakhs
                                            for industrially backward districts and less than two crore rupees for other districts</li>
                                        <li>
                                        Units without Spice-mark or Agmark
                                    </ol>
                                </li>
                                <li>Confectionary with investment in plant and machinery with less than Rs.10 Lakhs
                                    for industrially backward districts and less than two crore rupees for other districts.</li>
                                <li>Oil mills with expellers including oil processing, filtering , de-coloring ,coloring
                                    ,refining of edible oils and hydro-generation thereof except investment in plant
                                    and machinery of RS. 10 Lakhs in industrial backward areas.</li>
                                <li>Preparation of sweets and savories etc</li>
                                <li>Bread making(excluding mechanized bakery)</li>
                                <li>Mixture.Bhujia and chanachur preparation units</li>
                                <li>Manufacture of ice candy</li>
                                <li>Manufacture and processing of betel nuts</li>
                                <li>Hacheries,Piggeries,rabbit or Broiler farming </li>
                                <li>Standalone sponge iron plants</li>
                                <li>Iron and steel processors, such as cutting of sheets,bars,angles,coils,M.S. sheets,
                                    recoiling, straightening,corrugating,drophammer units etc with low value addition</li>
                                <li>Cracker-making units</li>
                                <li>Tyre retreading units with investment in plant and machinery of less Rs.20 Lakhs</li>
                                <li>Stone crushing units</li>
                                <li>Coal/coke screening coal /coke Briquetting.</li>
                                <li>Production of firewood and charcoal.</li>
                                <li>Painting and spray-painting units with investment in plant and machinery of less
                                    than Rs. 20 Lakhs.</li>
                                <li>Units for physical mixing of fertilizers.</li>
                                <li>Brick- making units (except units making refractory bricks and those making bricks
                                    from flyash, red mud and similar industrial waste not less than 25% as base martial).</li>
                                <li>Manufacturing of tarpaulin out of canvas cloth with investment in plant and machinery
                                    of less than Rs. 20 Lakhs.</li>
                                <li>Saw mills, sawing of timber.</li>
                                <li>Carpentry, joinery and wooden furniture making except when part of a wood based
                                    cluster of at least 20 units.</li>
                                <li>Drilling rigs ,Bore-wells and Tube-wells</li>
                                <li>Units for mixing or blending/packaging of tea.</li>
                                <li>Units for cutting raw tobacco and sprinkling jiggery for chewing purpose and Gudakhu
                                    manufacturing units.</li>
                                <li>Units for bottling of medicines.</li>
                                <li>Bookbinding/Rubber stamp making/making notebooks, exercise notebook s and envelopes.</li>
                                <li>Distilled water units</li>
                                <li>Tailoring (other than readymade garment manufacturing units )</li>
                                <li>Repacking /stitching/printing of woven sacks out of woven fabrics.</li>
                                <li>Pre-Processing of oil seeds-Decorticating, expelling, crushing, parching and frying.</li>
                                <li>Aerated water and soft drinks units</li>
                                <li>Bottling units or any activity in respect of IMFL or liquor of any kind.</li>
                                <li>Size reducing/size separating units/ Grinding / mixing units with investment in
                                    plant and machinery of less than ten crore rupees except manufacturing of cement
                                    with clinker.</li>
                                <li>Polythene less than 40 micron in thickness /recycling of plastic materials.</li>
                                <li>Thermal power plants.</li>
                                <li>Repackaging units.</li>
                            </ol>
                        </li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided informatin will be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a data-toggle="modal" class="btn btn-success" data-dismiss="modal" data-target="#undertakingfoodprocessing2">
                        Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingfoodprocessing2" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part thre negative unit
                        list under <strong>Food Processing</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>Hullers and rice mills with investment in plant and machinery of less than Rs. 25
                            lakhs for industrially backward districts and less than 1 crore for other.</li>
                        <li>Flour mills including manufacture of besan , pulse mills and chuda mills except
                            investment in plant and machinery of less than Rs.25 lakhs far industrially backward
                            districts and less than Rs. 1 crore for other areas (excluding rolling mills).
                        </li>
                        <li>
                            <ol>
                                <li>Processing of spices with investment in plant and machinery with less than Rs. 10
                                    lakh for industrially backward districts and less than Rs.2 crore for other areas.</li>
                                <li>Units without spice-mark or Ag-mark.</li>
                            </ol>
                        </li>
                        <li>Confectionary with investment in plant and machinery with less than Rs.10 lakhs
                            for industrially backward districts and less than Rs. 2 crore for other areas.
                        </li>
                        <li>Oil mills with expellers including oil processing , filtering, decoloring , coloring
                            refining of edible oils and hydrogenation thereof except investment in plant and
                            machinery more than Rs.10 lakhs in industrially backward districts </li>
                        <li>Preparation of sweets and savories excepts investment in plant and machinery of
                            less than Rs.10 lakhs for industrially backward districts and less than Rs.50 lakhs
                            for other areas.</li>
                        <li>Bread-making (excluding mechanized bakery)</li>
                        <li>Mixture, bhujia and chanachur preparation units.</li>
                        <li>Manufacture of ice candy</li>
                        <li>Manufacture and processing of betel units.</li>
                        <li>Hatcheries, piggeries, Rabbit or Broiler farming.</li>
                        <li>Units for mixing or blending /packaging of tea.</li>
                        <li>Units for cutting raw tobacco and sprinkling jaggery for chewing purposes and guddkhu
                            manufacturing units.</li>
                        <li>Pre-processing of oil seeds: decorticating, expelling, crushing, parching and frying.</li>
                        <li>Aerated water and soft drink units.</li>
                        <li>Bottling units or any activity in respect to IMFL or liquor of any kind.</li>
                        <li>Re-packaging units.</li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided information is be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a data-toggle="modal" class="btn btn-success" data-dismiss="modal" data-target="#undertakingMSME2">
                        Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingMSME2" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part thre negative unit
                        list under <strong>MSME Policy</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>All service enterprises.</li>
                        <li>Hullers and rice mills with investment in plant and machinery of less than Rs. 25
                            lakhs for industrially backward districts and less than 1 crore for other.</li>
                        <li>Flour mills including manufacture of besan , pulse mills and chuda mills except
                            investment in plant and machinery of less than Rs.25 lakhs far industrially backward
                            districts and less than Rs. 1 crore for other areas (excluding rolling mills).</li>
                        <li>
                            <ol>
                                <li>Processing of spices with investment in plant and machinery with less than Rs. 10
                                    lakh for industrially backward districts and less than Rs.2 crore for other areas.</li>
                                <li>Units without spice-mark or Ag-mark.</li>
                            </ol>
                        </li>
                        <li>Confectionary with investment in plant and machinery with less than Rs.10 lakhs
                            for industrially backward districts and less than Rs. 2 crore for other areas.
                        </li>
                        <li>Oil mills with expellers including oil processing , filtering, decoloring , coloring
                            refining of edible oils and hydrogenation thereof except investment in plant and
                            machinery more than Rs.10 lakhs in industrially backward district.</li>
                        <li>Preparation of sweets and savories excepts investment in plant and machinery of
                            less than Rs.10 lakhs for industrially backward districts and less than Rs.50 lakhs
                            for other areas.</li>
                        <li>Bread-making (excluding mechanized bakery)</li>
                        <li>Mixture, bhujia and chanachur preparation units.</li>
                        <li>Manufacture of ice candy</li>
                        <li>Manufacture and processing of betel units.</li>
                        <li>Hatcheries, piggeries, Rabbit or Broiler farming.</li>
                        <li>Standalone Sponge iron plants.</li>
                        <li>“iron and steel processors” such as cutting of sheets, bars, angles, coils, M.S.
                            sheets, recoiling straightening , corrugating , drop hammer units etc. with low
                            value addition.</li>
                        <li>Cracker-making units.</li>
                        <li>Tyre retreading units with investment in plant and machinery of less than Rs.20
                            lakhs.</li>
                        <li>Stone crushing units.</li>
                        <li>Coal/coke screening. Coal washing ,coal/coke briquetting.</li>
                        <li>Production of firewood and charcoal.</li>
                        <li>Painting and spray-painting units with investment in plant and machinery of less
                            than Rs.20 lakhs.</li>
                        <li>Units for physical mixing of fertilizer.</li>
                        <li>Brick-making units (except units making refractory bricks and those making bricks
                            from fly ash, red mud and industrial waste not less than 25% as base material).</li>
                        <li>Manufacturing of tarpaulin out of canvas cloth with investment in plant and machinery
                            of less than Rs. 20 lakh.</li>
                        <li>Saw mill, sawing of timber.</li>
                        <li>Carpentry, joinery and wooden furniture making except when part of a wood based
                            cluster of at least 20 units.</li>
                        <li>Drilling rigs, Bore-wells and tube wells.</li>
                        <li>Units for mixing or blending /packaging of tea.</li>
                        <li>Units for cutting raw tobacco and sprinkling jiggery for chewing purposes and gudakhu
                            manufacturing units.</li>
                        <li>Units for bottling of medicines.</li>
                        <li>Bookbinding / Rubber stamp making /making notebooks, exercise notebooks and envelopes.</li>
                        <li>Distilled water units.</li>
                        <li>Tailoring (other than readymade garment manufacturing units).</li>
                        <li>Repacking /stitching /printing of woven sacks out of woven fabrics.</li>
                        <li>Pre-processing of oil seeds-decorticating , expelling ,crushing ,parching and frying.</li>
                        <li>Aerated water and soft drink units.</li>
                        <li>Bottling units or any activity in respect to IMFL or liquor of any kind.</li>
                        <li>Size reducing/size separating units /grinding/mixing units with investment in plant
                            and machinery of less than 10 crore except manufacturing of cement with clinker.</li>
                        <li>Polythene less than 40 micron in thickness/recycling of plastic materials</li>
                        <li>Thermal power plant.</li>
                        <li>Re-packaging units.</li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided information is be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a href="unitdetailsforpccleared.aspx" class="btn btn-success" title="Submit">Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingipr20153" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part thre negative unit
                        list under <strong>IPR 2015</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>Units engaged in manufacturing and/or servicing activity belonging to following
                            categories.</li>
                        <li>Industries listed under the first schedule of the industries Department and regulation
                            Act,1951 and manufacturing enterprise classified under MSME.</li>
                        <li>Industries falling within the purview of the following Boards and public Agencies.
                            <ol>
                                <li>Coir Board </li>
                                <li>Silk Board</li>
                                <li>All India handloom and Handicraft Board</li>
                                <li>Khadi and village industries Commission/Board</li>
                                <li>Any other Agency con situation by Government for industrial department.</li>
                            </ol>
                        </li>
                        <li>Infrastructure projects only for the purpose of determining applicable land rate
                        </li>
                        <li>Service sector projects under priority sector.</li>
                        <li>Service sector other than priority sector only for the purpose of applicable land
                            rate.</li>
                        <li>Industrial unit will not include non-manufacturing/servicing industries except:
                            <ol>
                                <li>General workshops including repair workshops having investment in plant & machinery
                                    of Rs.50 Lakhs and above and running with power.</li>
                                <li>Cold storage and seafood freezing units having investment of Rs. 25.00 Lakhs and
                                    above.</li>
                                <li>Electronics repair and maintenance units for professional grade equipment and computer
                                    software ,ITES/BPO and related services with investment of Rs. 25,00 Lakhs and above.</li>
                                <li>Technology Development Laboratory/prototype Development centre/Research & Development
                                    with investment of Rs.25.00 Lakhs and above.</li>
                                <li>Printing press with investment in plant and machinery of Rs. 50.00 Lakhs and above.</li>
                                <li>Laundry/Dry Cleaning with investment in plant and machinery/equipment of Rs. 25.00
                                    Lakhs and above. </li>
                            </ol>
                        </li>
                        <li>The following units shall neither be eligible for fiscal incentives specified under
                            this IPR nor for allotment of land at concessional rates in the state, but shall
                            be eligible for investment facilitation ,allotment of land under normal rules at
                            benchmark value/market rate and recommendation to the financial institutions for
                            term loan and working capital and for recommendation, if necessary to the power
                            Distribution companies:
                            <ol>
                                <li>Hullers and Rice mills with investment in plant and machinery of less than Rs. 25
                                    Lakhs for industrially backward districts and less than Rs. 1 crore for other districts</li>
                                <li>Four mills including manufacture of besan, Pulse mills and chuda mills except investment
                                    in plant & machinery of more than 25 lakhs for industrially backward districts and
                                    less than Rs. 1 crore for other districts(excluding Roller Flour mills)
                                    <ol>
                                        <li>Processing of Spices with investment in plant & machinery with less than Rs.10 Lakhs
                                            for industrially backward districts and less than two crore rupees for other districts</li>
                                        <li>
                                        Units without Spice-mark or Agmark
                                    </ol>
                                </li>
                                <li>Confectionary with investment in plant and machinery with less than Rs.10 Lakhs
                                    for industrially backward districts and less than two crore rupees for other districts.</li>
                                <li>Oil mills with expellers including oil processing, filtering , de-coloring ,coloring
                                    ,refining of edible oils and hydro-generation thereof except investment in plant
                                    and machinery of RS. 10 Lakhs in industrial backward areas.</li>
                                <li>Preparation of sweets and savories etc</li>
                                <li>Bread making(excluding mechanized bakery)</li>
                                <li>Mixture.Bhujia and chanachur preparation units</li>
                                <li>Manufacture of ice candy</li>
                                <li>Manufacture and processing of betel nuts</li>
                                <li>Hacheries,Piggeries,rabbit or Broiler farming </li>
                                <li>Standalone sponge iron plants</li>
                                <li>Iron and steel processors, such as cutting of sheets,bars,angles,coils,M.S. sheets,
                                    recoiling, straightening,corrugating,drophammer units etc with low value addition</li>
                                <li>Cracker-making units</li>
                                <li>Tyre retreading units with investment in plant and machinery of less Rs.20 Lakhs</li>
                                <li>Stone crushing units</li>
                                <li>Coal/coke screening coal /coke Briquetting.</li>
                                <li>Production of firewood and charcoal.</li>
                                <li>Painting and spray-painting units with investment in plant and machinery of less
                                    than Rs. 20 Lakhs.</li>
                                <li>Units for physical mixing of fertilizers.</li>
                                <li>Brick- making units (except units making refractory bricks and those making bricks
                                    from flyash, red mud and similar industrial waste not less than 25% as base martial).</li>
                                <li>Manufacturing of tarpaulin out of canvas cloth with investment in plant and machinery
                                    of less than Rs. 20 Lakhs.</li>
                                <li>Saw mills, sawing of timber.</li>
                                <li>Carpentry, joinery and wooden furniture making except when part of a wood based
                                    cluster of at least 20 units.</li>
                                <li>Drilling rigs ,Bore-wells and Tube-wells</li>
                                <li>Units for mixing or blending/packaging of tea.</li>
                                <li>Units for cutting raw tobacco and sprinkling jiggery for chewing purpose and Gudakhu
                                    manufacturing units.</li>
                                <li>Units for bottling of medicines.</li>
                                <li>Bookbinding/Rubber stamp making/making notebooks, exercise notebook s and envelopes.</li>
                                <li>Distilled water units</li>
                                <li>Tailoring (other than readymade garment manufacturing units )</li>
                                <li>Repacking /stitching/printing of woven sacks out of woven fabrics.</li>
                                <li>Pre-Processing of oil seeds-Decorticating, expelling, crushing, parching and frying.</li>
                                <li>Aerated water and soft drinks units</li>
                                <li>Bottling units or any activity in respect of IMFL or liquor of any kind.</li>
                                <li>Size reducing/size separating units/ Grinding / mixing units with investment in
                                    plant and machinery of less than ten crore rupees except manufacturing of cement
                                    with clinker.</li>
                                <li>Polythene less than 40 micron in thickness /recycling of plastic materials.</li>
                                <li>Thermal power plants.</li>
                                <li>Repackaging units.</li>
                            </ol>
                        </li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided informatin will be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a data-toggle="modal" class="btn btn-success" data-dismiss="modal" data-target="#undertakingfoodprocessing3">
                        Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingfoodprocessing3" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part thre negative unit
                        list under <strong>Food Processing</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>Hullers and rice mills with investment in plant and machinery of less than Rs. 25
                            lakhs for industrially backward districts and less than 1 crore for other.</li>
                        <li>Flour mills including manufacture of besan , pulse mills and chuda mills except
                            investment in plant and machinery of less than Rs.25 lakhs far industrially backward
                            districts and less than Rs. 1 crore for other areas (excluding rolling mills).
                        </li>
                        <li>
                            <ol>
                                <li>Processing of spices with investment in plant and machinery with less than Rs. 10
                                    lakh for industrially backward districts and less than Rs.2 crore for other areas.</li>
                                <li>Units without spice-mark or Ag-mark.</li>
                            </ol>
                        </li>
                        <li>Confectionary with investment in plant and machinery with less than Rs.10 lakhs
                            for industrially backward districts and less than Rs. 2 crore for other areas.
                        </li>
                        <li>Oil mills with expellers including oil processing , filtering, decoloring , coloring
                            refining of edible oils and hydrogenation thereof except investment in plant and
                            machinery more than Rs.10 lakhs in industrially backward districts </li>
                        <li>Preparation of sweets and savories excepts investment in plant and machinery of
                            less than Rs.10 lakhs for industrially backward districts and less than Rs.50 lakhs
                            for other areas.</li>
                        <li>Bread-making (excluding mechanized bakery)</li>
                        <li>Mixture, bhujia and chanachur preparation units.</li>
                        <li>Manufacture of ice candy</li>
                        <li>Manufacture and processing of betel units.</li>
                        <li>Hatcheries, piggeries, Rabbit or Broiler farming.</li>
                        <li>Units for mixing or blending /packaging of tea.</li>
                        <li>Units for cutting raw tobacco and sprinkling jaggery for chewing purposes and guddkhu
                            manufacturing units.</li>
                        <li>Pre-processing of oil seeds: decorticating, expelling, crushing, parching and frying.</li>
                        <li>Aerated water and soft drink units.</li>
                        <li>Bottling units or any activity in respect to IMFL or liquor of any kind.</li>
                        <li>Re-packaging units.</li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided information is be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a data-toggle="modal" class="btn btn-success" data-dismiss="modal" data-target="#undertakingMSME3">
                        Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingMSME3" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part thre negative unit
                        list under <strong>MSME Policy</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>All service enterprises.</li>
                        <li>Hullers and rice mills with investment in plant and machinery of less than Rs. 25
                            lakhs for industrially backward districts and less than 1 crore for other.</li>
                        <li>Flour mills including manufacture of besan , pulse mills and chuda mills except
                            investment in plant and machinery of less than Rs.25 lakhs far industrially backward
                            districts and less than Rs. 1 crore for other areas (excluding rolling mills).</li>
                        <li>
                            <ol>
                                <li>Processing of spices with investment in plant and machinery with less than Rs. 10
                                    lakh for industrially backward districts and less than Rs.2 crore for other areas.</li>
                                <li>Units without spice-mark or Ag-mark.</li>
                            </ol>
                        </li>
                        <li>Confectionary with investment in plant and machinery with less than Rs.10 lakhs
                            for industrially backward districts and less than Rs. 2 crore for other areas.
                        </li>
                        <li>Oil mills with expellers including oil processing , filtering, decoloring , coloring
                            refining of edible oils and hydrogenation thereof except investment in plant and
                            machinery more than Rs.10 lakhs in industrially backward district.</li>
                        <li>Preparation of sweets and savories excepts investment in plant and machinery of
                            less than Rs.10 lakhs for industrially backward districts and less than Rs.50 lakhs
                            for other areas.</li>
                        <li>Bread-making (excluding mechanized bakery)</li>
                        <li>Mixture, bhujia and chanachur preparation units.</li>
                        <li>Manufacture of ice candy</li>
                        <li>Manufacture and processing of betel units.</li>
                        <li>Hatcheries, piggeries, Rabbit or Broiler farming.</li>
                        <li>Standalone Sponge iron plants.</li>
                        <li>“iron and steel processors” such as cutting of sheets, bars, angles, coils, M.S.
                            sheets, recoiling straightening , corrugating , drop hammer units etc. with low
                            value addition.</li>
                        <li>Cracker-making units.</li>
                        <li>Tyre retreading units with investment in plant and machinery of less than Rs.20
                            lakhs.</li>
                        <li>Stone crushing units.</li>
                        <li>Coal/coke screening. Coal washing ,coal/coke briquetting.</li>
                        <li>Production of firewood and charcoal.</li>
                        <li>Painting and spray-painting units with investment in plant and machinery of less
                            than Rs.20 lakhs.</li>
                        <li>Units for physical mixing of fertilizer.</li>
                        <li>Brick-making units (except units making refractory bricks and those making bricks
                            from fly ash, red mud and industrial waste not less than 25% as base material).</li>
                        <li>Manufacturing of tarpaulin out of canvas cloth with investment in plant and machinery
                            of less than Rs. 20 lakh.</li>
                        <li>Saw mill, sawing of timber.</li>
                        <li>Carpentry, joinery and wooden furniture making except when part of a wood based
                            cluster of at least 20 units.</li>
                        <li>Drilling rigs, Bore-wells and tube wells.</li>
                        <li>Units for mixing or blending /packaging of tea.</li>
                        <li>Units for cutting raw tobacco and sprinkling jiggery for chewing purposes and gudakhu
                            manufacturing units.</li>
                        <li>Units for bottling of medicines.</li>
                        <li>Bookbinding / Rubber stamp making /making notebooks, exercise notebooks and envelopes.</li>
                        <li>Distilled water units.</li>
                        <li>Tailoring (other than readymade garment manufacturing units).</li>
                        <li>Repacking /stitching /printing of woven sacks out of woven fabrics.</li>
                        <li>Pre-processing of oil seeds-decorticating , expelling ,crushing ,parching and frying.</li>
                        <li>Aerated water and soft drink units.</li>
                        <li>Bottling units or any activity in respect to IMFL or liquor of any kind.</li>
                        <li>Size reducing/size separating units /grinding/mixing units with investment in plant
                            and machinery of less than 10 crore except manufacturing of cement with clinker.</li>
                        <li>Polythene less than 40 micron in thickness/recycling of plastic materials</li>
                        <li>Thermal power plant.</li>
                        <li>Re-packaging units.</li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided information is be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a href="unitdetailsforEINIEM.aspx" class="btn btn-success" title="Submit">Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
