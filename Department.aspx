<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Department.aspx.cs" Inherits="Department" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.department').addClass('active');            
        });

    </script>
    <style>
    .deptlist li a span {
    float: none!important;
    margin-right: 12px;
   
}
.deptlist{
    min-height: 420px!important;
    overflow: hidden;
    overflow-y: scroll;
}
 .rightnews ul li.active a {
   color:#f00
}
.aboutcontent-sec .listitem {margin-left:0;}
.aboutcontent-sec .listitem li {
    color: #000;
    font-size: 15px;
    line-height: 26px;
    margin: 0;
    background: url(images/play-symbol.png) 0 12px no-repeat;
    padding: 2px 0 0 15px;
    list-style:none;
}
.links{position:absolute;bottom:0px;right:20px}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc2:header ID="header" runat="server" />
            <div class="container wrapper">
        <div class="navigatorheader-div aboutheadernav">
            <div class="col-sm-12">
                <ul class="breadcrumb">
                    <li><a href="Default.aspx" title="Home page"><i class="fa fa-home"></i></a></li>
                    <%--<li>Labour &amp; ESI</li></ul>--%>
                    <li runat="server" id="lihid" >
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></li></ul>
            </div>
            <div class="clearfix">
            </div>
        </div>
        <div class="content-form-section">
    
            <div class="col-sm-4">
               <div class="rightpanel laboutesi">
               <div class="rightnews bglight-gray deptservice-list">
                    <h2>Departments</h2> 
                     <ul id="depulid" class="deptlist" runat="server">
                       <%--  <li class="plLesi"><a href="LabourESI.aspx" title="Labour & ESI" >Labour &amp; ESI</a></li>
                         <li class="plDIndustries"><a href="http://industries.odisha.gov.in/" target="_blank" title="Department of Industries" >Department of Industries</a></li>
                         <li class="plOPCBoard"><a href="http://ospcboard.org/" target="_blank" title="Odisha Pollution Control Board" >Odisha Pollution Control Board</a></li>
                         <li class="plCCTax"><a href="https://odishatax.gov.in/" target="_blank" title="Commissionerate Of Commercial Tax" >Commissionerate of Commercial Tax</a></li>--%>
                      </ul>
                </div>
               </div>
            </div>
                    <div class="col-sm-8">
                <div class="aboutcontent-sec">
                    <h3 id="hdid" runat="server"></h3> <br />
                    <h5 id="h1" runat="server">Pre-Establishment</h5>

                    <ol runat="server" id="oldeptid" class="listitem">
                       <%-- <li>--%>
                      <%--  <a href="LabourESIDetails.aspx">License for contractors & renewal of license  under provision of The Contracts Labour (Regulation and Abolition) Act, 1970</a></li>
                        <li><a href="ssjavascript:void(0);">Registration & renewal under The Shops and Establishment Act</a></li>
                        <li><a href="ssjavascript:void(0);">Registrationof principal employer's establishment under provision of The Contracts Labour (Regulation and Abolition) Act, 1970</a>
                        </li>--%>
                    </ol>
                    <br />
                     <h5 id="h2id" runat="server">Pre-Operation</h5>
                     
                    <ol runat="server" id="ol1" class="listitem">
                       <%-- <li>--%>
                      <%--  <a href="LabourESIDetails.aspx">License for contractors & renewal of license  under provision of The Contracts Labour (Regulation and Abolition) Act, 1970</a></li>
                        <li><a href="ssjavascript:void(0);">Registration & renewal under The Shops and Establishment Act</a></li>
                        <li><a href="ssjavascript:void(0);">Registrationof principal employer's establishment under provision of The Contracts Labour (Regulation and Abolition) Act, 1970</a>
                        </li>--%>
                    </ol>

                    <br />
                    <h5 id="h3id" runat="server" visible="false">Post-Operation</h5>

                    <ol runat="server" id="ol2" class="listitem" visible="false">

                          <li><a href="Water Tariff of IDCO.pdf" download>Water Tariff of IDCO Industrial Estates</a></li>

                    </ol>
                   
                   <a href="CertificateDownloadFB.aspx" class="btn btn-primary links" runat="server" id="linkfb"  title="Certificate & Licences ">Certificate &amp; Licences</a>
                    <a href="CertificateDownloadLM.aspx" class="btn btn-primary links" runat="server" id="linkfscw" title="Certificate & Licences ">Certificate &amp; Licences</a>
                   
                </div>
            </div>
            <div class="clearfix">
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            var x = $(".aboutcontent-sec").find('#hdid').text();
          
            var y = $('.plDIndustries a').attr('href');
          
        });
    </script>
    </div>
    <uc3:footer ID="footer" runat="server" /> 
    </form>
</body>
</html>