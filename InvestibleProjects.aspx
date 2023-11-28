<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvestibleProjects.aspx.cs"
    Inherits="InvestibleProjects" %>

<!DOCTYPE html>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<html>
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.dbodisha,.plEDBusiness').addClass('active');
        });
    </script>
    <title>SWP(Single Window Portal)</title>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="navigatorheader-div aboutheadernav">
            <div class="col-sm-12">
                <ul class="breadcrumb">
                    <li><a href="Default.aspx" title="Home page"><i class="fa fa-home"></i></a></li>
                    <li>Doing Business in Odisha</li>
                </ul>
            </div>
            <div class="clearfix">
            </div>
        </div>
        <div class="content-form-section">
            <div class="col-sm-8">
                <div class="aboutcontent-sec">
                    <h3>
                        List of Investible Projects</h3>
                    <h4>
                        Compendium of Investment Projects</h4>
                    <p>
                        Odisha is fast emerging as the manufacturing hub of the East in India through its
                        industry-friendly environment and policy framework. With a rich maritime trade history
                        and one of the largest major ports of the country at Paradip, Odisha is the eastern
                        gateway to the ASEAN region.</p>
                    <p>
                        The State has been a well-known mineral hub of the country. In the recent past,
                        the State has undertaken a series of initiatives to broad-base the industrial development
                        by attracting investments across diversified sectors with significant potential.
                        My Government has launched Odisha Industrial Development Plan: Vision 2025 with
                        focused attention on 5 sectors which aims to attract investments to the tune of
                        Rs. 2.5 lakh crore and generate direct &amp; indirect employment opportunities for
                        30 lakh people.</p>
                    <p>
                        On the back of these initiatives, numerous opportunities for investments have opened
                        up for investors across different sectors. This compendium includes key features
                        of investment projects in sectors and areas including industrial parks, textiles,
                        healthcare, logistics, transport, information technology, electronics manufacturing,
                        food processing, urban infrastructure, smart city, chemicals, petrochemicals, plastics,
                        energy, tourism and downstream industries.</p>
                    <p>
                        Odisha is committed to ensure "investor delight" and welcomes entrepreneurs and
                        investors from across the globe for a mutually rewarding engagement.</p>
                    <div class="text-right">
                        <a class="btn btn-danger" href="Download/Odisha_Compendium_of_Investment_Projects_1.pdf"
                            target="_blank">Download Compendium</a></div>
                    <div class="tab-content">
                        <div class="tab-pane active" id="a">
                            <h4>
                                Agro &amp; Food Processing</h4>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr>
                                        <th width="25">
                                            Sl#
                                        </th>
                                        <th>
                                            Project Name
                                        </th>
                                        <th width="60">
                                            Download
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            Rice Technology Park, Bhadrak
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-125.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2
                                        </td>
                                        <td>
                                            Sea Food Park at Deras
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-126.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3
                                        </td>
                                        <td>
                                            MITS Food Park at Rayagada
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-127.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            4
                                        </td>
                                        <td>
                                            Development of Mega and Medium Food Park - Ganjam
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-128.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            5
                                        </td>
                                        <td>
                                            Development of Mega and Medium Food Park - Kalahandi
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-129.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            6
                                        </td>
                                        <td>
                                            Development of Mega and Medium Food Park - Dhenkanal
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-130.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            7
                                        </td>
                                        <td>
                                            Development of Mega and Medium Food Park - Balasore
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-131.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            8
                                        </td>
                                        <td>
                                            Development of Mega and Medium Food Park - Baragarh
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-132.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            9
                                        </td>
                                        <td>
                                            Centre of Excellence (CoE) for Agro and marine products - Angul
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-133.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            10
                                        </td>
                                        <td>
                                            Centre of Excellence (CoE) for Agro and marine products - Dhenkanal
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-134.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            11
                                        </td>
                                        <td>
                                            Centre of Excellence (CoE) for Agro and marine products - Ganjam
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-135.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            12
                                        </td>
                                        <td>
                                            Centre of Excellence (CoE) for Agro and marine products - Nabarangapur
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-136.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            13
                                        </td>
                                        <td>
                                            Centre of Excellence (CoE) for Agro and marine products - Kandhamal
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-137.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            14
                                        </td>
                                        <td>
                                            Development of food quality testing and phyto-sanitary laboratories
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-138.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            15
                                        </td>
                                        <td>
                                            Commodity Value Chain based Cluster Development in PPP mode
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-139.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            16
                                        </td>
                                        <td>
                                            Packaging Development Centres
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-140.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            17
                                        </td>
                                        <td>
                                            Food processing exports through the development of AEZ - Angul
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-141.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            18
                                        </td>
                                        <td>
                                            Food processing exports through the development of AEZs - Dhenkanal
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-142.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            19
                                        </td>
                                        <td>
                                            Food processing exports through the development of AEZs - Ganjam
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-143.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            20
                                        </td>
                                        <td>
                                            Food processing exports through the development of AEZs – KBK Region
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-144.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            21
                                        </td>
                                        <td>
                                            Development of Cold Storage Facilities - Khurdha
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-145.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            22
                                        </td>
                                        <td>
                                            Development of Cold Storage Facilities - Bhadrak
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-146.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            23
                                        </td>
                                        <td>
                                            Development of Cold Storage Facilities - Jagatsinghpur
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-147.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            24
                                        </td>
                                        <td>
                                            Development of Cold Storage Facilities - Cuttack
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-148.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            25
                                        </td>
                                        <td>
                                            Development of Cold Storage Facilities - Balasore
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-149.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            26
                                        </td>
                                        <td>
                                            Development of Cold Storage Facilities - Balasore
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-150.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            27
                                        </td>
                                        <td>
                                            Development of Cold Storage Facilities - Ganjam
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-151.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            28
                                        </td>
                                        <td>
                                            Development of a state-of-the-art laboratory for fish and shrimp processors and
                                            mobile aqua labs
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/AF-152.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="b">
                            <h4>
                                Chemicals, Petro-Chemicals and Plastics</h4>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr>
                                        <th width="25">
                                            Sl#
                                        </th>
                                        <th>
                                            Project Name
                                        </th>
                                        <th width="60">
                                            Download
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            Petroleum, Chemicals and Petro-Chemicals Investment Region (PCPIR), Paradeep
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/CP-187.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2
                                        </td>
                                        <td>
                                            Paradeep Plastic Park
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/CP-188.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3
                                        </td>
                                        <td>
                                            Development of Chemical units based on Coal Gasification – Talcher
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/CP-189.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            4
                                        </td>
                                        <td>
                                            Development of Chemical units based on Coal Gasification - Angul
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/CP-190.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            5
                                        </td>
                                        <td>
                                            Development of Chemical units based on Coal Gasification - Jajpur
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/CP-191.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            6
                                        </td>
                                        <td>
                                            Development of Chemical units based on Coal Gasification – Paradeep
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/CP-192.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            7
                                        </td>
                                        <td>
                                            Development of a chemical and pharmaceutical cluster in Berhampur
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/CP-193.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            8
                                        </td>
                                        <td>
                                            Investment Opportunities for Downstream Industries in Plastics
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/download/InvestmentOpportunitiesforDownstreamIndustriesinPlastics.pdf"
                                                title="download" target="_blank"><i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="c">
                            <h4>
                                Energy</h4>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr>
                                        <th width="25">
                                            Sl#
                                        </th>
                                        <th>
                                            Project Name
                                        </th>
                                        <th width="60">
                                            Download
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            Development of Solar Parks of 1000 MW in Odisha - Balasore
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/EN-194.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2
                                        </td>
                                        <td>
                                            Development of Solar Parks of 1000 MW in Odisha - Keonjhar
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/EN-195.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3
                                        </td>
                                        <td>
                                            Development of Solar Parks of 1000 MW in Odisha - Deogarh
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/EN-196.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            4
                                        </td>
                                        <td>
                                            Development of Solar Parks of 1000 MW in Odisha - Boudh
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/EN-197.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            5
                                        </td>
                                        <td>
                                            Development of Solar Parks of 1000 MW in Odisha - Kalahandi
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/EN-198.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            6
                                        </td>
                                        <td>
                                            Development of Solar Parks of 1000 MW in Odisha - Angul
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/EN-199.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="d">
                            <h4>
                                Healthcare and Biotech</h4>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr>
                                        <th width="25">
                                            Sl#
                                        </th>
                                        <th>
                                            Name of the Project
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            Biotech Park
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2
                                        </td>
                                        <td>
                                            BMC Hospital
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3
                                        </td>
                                        <td>
                                            Development of Low Cost Hospitals
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="e">
                            <h4>
                                Industrial Parks / Infrastructure</h4>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr>
                                        <th colspan="2">
                                            Sl#
                                        </th>
                                        <th>
                                            Name of the Project
                                        </th>
                                    </tr>
                                    <tr>
                                        <td width="25">
                                            1
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Aluminium Park at Angul
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            National Investment and Manufacturing Zone
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Downstream Parks for Steel based units
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td width="25">
                                            3.1
                                        </td>
                                        <td>
                                            Jharsuguda
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            3.2
                                        </td>
                                        <td>
                                            Rourkela
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            3.3
                                        </td>
                                        <td>
                                            Kalinganagar
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            3.4
                                        </td>
                                        <td>
                                            Barbil
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            3.5
                                        </td>
                                        <td>
                                            Paradeep
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            3.6
                                        </td>
                                        <td>
                                            Dhenkanal
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            3.7
                                        </td>
                                        <td>
                                            Sambalpur
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            4
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Development of District Level Mini Tool Rooms
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            5
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Setting up Technology Facilitation Centre as a networking hub
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            6
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Private Industrial Estates
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.1
                                        </td>
                                        <td>
                                            Dampada, Cuttack
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.2
                                        </td>
                                        <td>
                                            Gouraprasad, Bhadrak
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.3
                                        </td>
                                        <td>
                                            Karanjamal, Bhadrak
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.4
                                        </td>
                                        <td>
                                            Narendrapur, Bhadrak
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.5
                                        </td>
                                        <td>
                                            Remuna, Balasore
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.6
                                        </td>
                                        <td>
                                            Siha, Jagatsinghpur
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.7
                                        </td>
                                        <td>
                                            Malipada, Khordha
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.8
                                        </td>
                                        <td>
                                            Mundamba, Khordha
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.9
                                        </td>
                                        <td>
                                            Landeihill, Ganjam
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.10
                                        </td>
                                        <td>
                                            Muliapali, Ganjam
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.11
                                        </td>
                                        <td>
                                            Dambasara, Rayagada
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.12
                                        </td>
                                        <td>
                                            Katikela, Jharsuguda
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.13
                                        </td>
                                        <td>
                                            Jharsugdua
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.14
                                        </td>
                                        <td>
                                            Lahanda, Sundergarh
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.15
                                        </td>
                                        <td>
                                            Jujumara, Sambalpur
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.16
                                        </td>
                                        <td>
                                            Kadopada, Deogarh
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            6.17
                                        </td>
                                        <td>
                                            Udaypur, Keonjhar
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            7
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Port Based Manufacturing Zone at Dhamra
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            8
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Development of Industrial Park under SIPC Master Plan, Paradeep
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            9
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Multi Product SEZ at Gopalpur
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="f">
                            <h4>
                                Incubation Centres</h4>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr>
                                        <th width="25">
                                            Sl#
                                        </th>
                                        <th>
                                            Project Name
                                        </th>
                                        <th width="60">
                                            Download
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            MSME Start-up Incubation Centres
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/Incubation-Centers-24102016.pdf" title="download"
                                                target="_blank"><i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="g">
                            <h4>
                                Investment Opportunities for Down-Stream Industries in Aluminium</h4>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr>
                                        <th width="25">
                                            Sl#
                                        </th>
                                        <th>
                                            Project Name
                                        </th>
                                        <th width="60">
                                            Download
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            Investment Opportunities for Down-Stream Industries in Aluminium
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/download/InvestmentOpportunitiesforDownstreamIndustriesinAluminium.pdf"
                                                title="download" target="_blank"><i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="h">
                            <h4>
                                Investment Opportunities for Down Stream Industries in Steel</h4>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr>
                                        <th width="25">
                                            Sl#
                                        </th>
                                        <th>
                                            Project Name
                                        </th>
                                        <th width="60">
                                            Download
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            Investment Opportunities for Down Stream Industries in Steel
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/download/InvestmentOpportunitiesforDownstreamIndustriesinSteel.pdf"
                                                title="download" target="_blank"><i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="i">
                            <h4>
                                Investment Opportunities for Down Stream Industries in Stainless Steel</h4>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr>
                                        <th width="25">
                                            Sl#
                                        </th>
                                        <th>
                                            Project Name
                                        </th>
                                        <th width="60">
                                            Download
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            Investment Opportunities for Down Stream Industries in Stainless Steel
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/download/InvestmentOpportunitiesforDownstreamIndustriesinStainlessSteel.pdf"
                                                title="download" target="_blank"><i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="j">
                            <h4>
                                Logistics and Transport</h4>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr>
                                        <th colspan="2">
                                            Sl#
                                        </th>
                                        <th>
                                            Name of the Project
                                        </th>
                                    </tr>
                                    <tr>
                                        <td width="25">
                                            1
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Coal Railway Corridor at Talcher
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Elevated Corridor at Joda
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Road Projects - Development &amp; Maintenance
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            4
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Bhubaneswar - Paradeep PCPIR Road
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            5
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Development of Ports
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td width="25">
                                            5.1
                                        </td>
                                        <td>
                                            Mahanadi Riverine Port
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            5.2
                                        </td>
                                        <td>
                                            Bichitrapur Port Project
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            5.3
                                        </td>
                                        <td>
                                            Bahabalpur Port Project
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            5.4
                                        </td>
                                        <td>
                                            Chandipur Port Project
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            5.5
                                        </td>
                                        <td>
                                            Inchudi Port Project
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            5.6
                                        </td>
                                        <td>
                                            Baliharachandi Port Project
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            5.7
                                        </td>
                                        <td>
                                            Palur Port Project
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            5.8
                                        </td>
                                        <td>
                                            Bahuda Port Project
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            6
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Development of storage and warehousing infrastructure
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            7
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Development of Multi Modal Logistics Parks
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            7.1
                                        </td>
                                        <td>
                                            Development of Multi Modal Logistics Park at Paradeep
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            7.2
                                        </td>
                                        <td>
                                            Angul
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            7.3
                                        </td>
                                        <td>
                                            Rourkela
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            7.4
                                        </td>
                                        <td>
                                            Muniguda, Ganjam
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            8
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Development of Inland Container Depots
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            8.1
                                        </td>
                                        <td>
                                            Paradeep
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            8.2
                                        </td>
                                        <td>
                                            Rayagada
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            8.3
                                        </td>
                                        <td>
                                            Kalinganagar
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            8.4
                                        </td>
                                        <td>
                                            Dhamra
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            8.5
                                        </td>
                                        <td>
                                            Bhadrak
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            9
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Development of Fishery Harbour
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            9.1
                                        </td>
                                        <td>
                                            Puri
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            9.2
                                        </td>
                                        <td>
                                            Ganjam
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            9.3
                                        </td>
                                        <td>
                                            Balasore
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            9.4
                                        </td>
                                        <td>
                                            Bhadrak
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            10
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Inspection and Certification Centre for Motor Vehicles
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            11
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Institute of Driving Training and Research (IDTR)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            12
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Automated Driving Testing System (ADTS)
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="k">
                            <h4>
                                Skill Development</h4>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr>
                                        <th width="25">
                                            Sl#
                                        </th>
                                        <th>
                                            Project Name
                                        </th>
                                        <th width="60">
                                            Download
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            Investment Opportunities in Skill Development
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/Skill-Development-24102016.pdf" title="download"
                                                target="_blank"><i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="l">
                            <h4>
                                Smart City Projects</h4>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr>
                                        <th width="25">
                                            Sl#
                                        </th>
                                        <th>
                                            Project Name
                                        </th>
                                        <th width="60">
                                            Download
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            Bhubaneswar Smart City - Railway Station Multi Modal Hub
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-168.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2
                                        </td>
                                        <td>
                                            Bhubaneswar Smart City – Janpath Government Housing Redevelopment
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-169.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3
                                        </td>
                                        <td>
                                            Bhubaneswar Smart City – Mission Awaas (Slum Redevelopment Projects)
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-170.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            4
                                        </td>
                                        <td>
                                            Bhubaneswar Smart City – Solid Waste Management
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-171.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            5
                                        </td>
                                        <td>
                                            Bhubaneswar Smart City – Public Bicycle Sharing Scheme
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-172.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            6
                                        </td>
                                        <td>
                                            Bhubaneswar Smart City – E-rickshaw project
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-173.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            7
                                        </td>
                                        <td>
                                            Bhubaneswar Smart City – LED Street Lighting
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-174.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            8
                                        </td>
                                        <td>
                                            Bhubaneswar Smart City – Micro Solar Power project
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-175.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            9
                                        </td>
                                        <td>
                                            Bhubaneswar Smart City – Satya Nagar Institutional Core
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-176.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            10
                                        </td>
                                        <td>
                                            Rourkela Smart City – JATAYAT Project
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-177.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            11
                                        </td>
                                        <td>
                                            Rourkela Smart City – SURAKSHIT ROURKELA
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-178.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            12
                                        </td>
                                        <td>
                                            Rourkela Smart City – Brahmani Riverfront Development in PPP
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-179.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            13
                                        </td>
                                        <td>
                                            Rourkela Smart City – JALDHARA Project
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-180.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            14
                                        </td>
                                        <td>
                                            Rourkela Smart City – Green Rourkela
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-181.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            15
                                        </td>
                                        <td>
                                            Rourkela Smart City – Integrated Informal Settlement
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-182.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            16
                                        </td>
                                        <td>
                                            Rourkela Smart City – Vibrant Rourkela
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-183.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            17
                                        </td>
                                        <td>
                                            Rourkela Smart City – CITY GOV Project
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-184.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            18
                                        </td>
                                        <td>
                                            Rourkela Smart City – PARIBAHAN Project
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-185.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            19
                                        </td>
                                        <td>
                                            Rourkela Smart City – Swachh Rourkela Project
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/SC-186.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="m">
                            <h4>
                                Textiles</h4>
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th width="25">
                                            Sl#
                                        </th>
                                        <th>
                                            Name of the Project
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            Bhadrak Apparel Park
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2
                                        </td>
                                        <td>
                                            Textile Park at Ramdaspur/ Choudwar
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3
                                        </td>
                                        <td>
                                            Integrated Textile Parks
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            4
                                        </td>
                                        <td>
                                            Development of Centre of Excellence
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="n">
                            <h4>
                                Tourism</h4>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr>
                                        <th width="25">
                                            Sl#
                                        </th>
                                        <th>
                                            Project Name
                                        </th>
                                        <th width="60">
                                            Download
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            Destination Development – Satapada, Puri
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/TO-200.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2
                                        </td>
                                        <td>
                                            Destination Development – Barkul, Ganjam
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/TO-201.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3
                                        </td>
                                        <td>
                                            Destination Development – Tampara, Ganjam
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/TO-202.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            4
                                        </td>
                                        <td>
                                            Destination Development – Special Tourism Area at Gopalpur, Ganjam
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/TO-203.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            5
                                        </td>
                                        <td>
                                            Chilika Development Authority – Aranya Eco Village, Ganjam
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/TO-204.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            6
                                        </td>
                                        <td>
                                            Chilika Development Authority – Iconic Tower, Satpada
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/TO-205.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            7
                                        </td>
                                        <td>
                                            Chilika Development Authority – Day Cruise
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/TO-206.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            8
                                        </td>
                                        <td>
                                            Chilika Development Authority – Water Sports at Rambha
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/TO-207.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            9
                                        </td>
                                        <td>
                                            Chilika Development Authority – Luxury Resort &amp; Convention Center
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/TO-208.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            10
                                        </td>
                                        <td>
                                            Chilika Development Authority – Tourism Node at Mangaljodi
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/TO-209.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            11
                                        </td>
                                        <td>
                                            Chilika Development Authority – Tourism Node at Rambha
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/TO-210.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            12
                                        </td>
                                        <td>
                                            Chilika Development Authority – Hotels Hub at Satpada
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/TO-211.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            13
                                        </td>
                                        <td>
                                            Chilika Development Authority – Eco Village at Rambha
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/TO-212.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="o">
                            <h4>
                                Urban Infrastructure &amp; Utilities</h4>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr>
                                        <th width="25">
                                            Sl#
                                        </th>
                                        <th>
                                            Project Name
                                        </th>
                                        <th width="60">
                                            Download
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            Improvement of Water Supply to Greater Berhampur
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/UI-153.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2
                                        </td>
                                        <td>
                                            Development of affordable housing with private sector participation in Bhubaneswar
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/UI-154.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3
                                        </td>
                                        <td>
                                            Sewerage System Project at Sambalpur
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/UI-155.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            4
                                        </td>
                                        <td>
                                            Solid Waste Management project in 36 towns grouped in 12 clusters
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/UI-156.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            5
                                        </td>
                                        <td>
                                            Sewerage System at Rourkela East and West Project
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/UI-157.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            6
                                        </td>
                                        <td>
                                            Mega Water Supply Projects in different districts of Odisha - Balasore
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/UI-158.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            7
                                        </td>
                                        <td>
                                            Mega Water Supply Projects in different districts of Odisha - Bhadrak
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/UI-159.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            8
                                        </td>
                                        <td>
                                            Mega Water Supply Projects in different districts of Odisha - Keonjhar
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/UI-160.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            9
                                        </td>
                                        <td>
                                            Mega Water Supply Projects in different districts of Odisha - Puri
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/UI-161.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            10
                                        </td>
                                        <td>
                                            Mega Water Supply Projects in different districts of Odisha - Bolangir
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/UI-162.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            11
                                        </td>
                                        <td>
                                            Mega Water Supply Projects in different districts of Odisha - Cuttack
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/UI-163.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            12
                                        </td>
                                        <td>
                                            Mega Water Supply Projects in different districts of Odisha - Jagatsinghpur
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/UI-164.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            13
                                        </td>
                                        <td>
                                            Mega Water Supply Projects in different districts of Odisha - Angul
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/UI-165.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            14
                                        </td>
                                        <td>
                                            Mega Water Supply Projects in different districts of Odisha - Jajpur
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/UI-166.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            15
                                        </td>
                                        <td>
                                            Affordable Housing Project at Rourkela
                                        </td>
                                        <td class="text-center">
                                            <a href="http://investodisha.org/MIO/download/UI-167.pdf" title="download" target="_blank">
                                                <i class="fa fa-download"></i></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-4">
                <div class="rightpanel">
                    <div>
                        <h2 class="h2-hdr">
                            Investible Projects list</h2>
                        <div class="tabs-right bglight-gray">
                            <ul class="nav nav-tabs ">
                                <li class="active"><a href="#a" data-toggle="tab">Agro &amp; Food Processing</a></li>
                                <li><a href="#b" data-toggle="tab">Chemicals, Petro-Chemicals and Plastics</a></li>
                                <li><a href="#c" data-toggle="tab">Energy</a></li>
                                <li><a href="#d" data-toggle="tab">Healthcare &amp; Biotechnology</a></li>
                                <li><a href="#e" data-toggle="tab">Industrial Parks / Infrastructure</a></li>
                                <li><a href="#f" data-toggle="tab">Incubation Centres</a></li>
                                <li><a href="#g" data-toggle="tab">Investment Opportunities for Down Stream Industries
                                    in Aluminium</a></li>
                                <li><a href="#h" data-toggle="tab">Investment Opportunities for Down Stream Industries
                                    in Steel</a></li>
                                <li><a href="#i" data-toggle="tab">Investment Opportunities for Down Stream Industries
                                    in Stainless Steel</a></li>
                                <li><a href="#j" data-toggle="tab">Logistics and Transport</a></li>
                                <li><a href="#k" data-toggle="tab">Skill Development</a></li>
                                <li><a href="#l" data-toggle="tab">Smart City Projects</a></li>
                                <li><a href="#m" data-toggle="tab">Textiles</a></li>
                                <li><a href="#n" data-toggle="tab">Tourism</a></li>
                                <li><a href="#o" data-toggle="tab">Urban Infrastructure &amp; Utilities</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clearfix">
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
