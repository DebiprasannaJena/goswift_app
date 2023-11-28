<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Enterpreneurship_Subsidy_FormPreview.aspx.cs"
    Inherits="incentives_Enterpreneurship_Subsidy_FormPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/incentive.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="registration-div investors-bg">
            <div id="exTab1" class="">
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
                                    <li class="active"><a href="appliedindustrylist.aspx">Apply For incentive</a></li>
                                    <li><a href="javscript:void(0);">View Application Status</a></li>
                                </ul>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-header">
                                <div class="iconsdiv">
                                    <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                        <i class="fa fa-print"></i></a><a href="javascript:void(0);" title="Export to Excel"
                                            id="A1" class="pull-right printbtn"><i class="fa fa-file-excel-o"></i></a>
                                </div>
                                <h2>
                                    Application For Entrepreneurship Development Subsidy
                                </h2>
                            </div>
                            <div class="form-body">
                                <div class="incentivepreiview">
                                    <div class="preiviewheader text-center">
                                        <h4>
                                            Application For Entrepreneurship development subsidy under industrial policy resolution 2015(FINAL APPLY)</h4>
                                        <p>
                                            Application received after the due date / incomplete in any respect shalt be liable for rejection <br />
                                            (Strike out whichever is not applicable)</p>
                                    </div>
                                    <div class="prieviewdatasec">
                                        <h4>
                                            From</h4>
                                        <div class="padding-left-20">
                                            <p>
                                                M/s :<asp:Label ID="lblMr" runat="server"></asp:Label></p>
                                            <p>
                                               Address :<asp:Label ID="lblAddress" runat="server"></asp:Label></p>
                                        </div>
                                        <h4>
                                            To</h4>
                                        <div class="padding-left-20">
                                            <p>
                                                The General Manager,<br />
                                                Regional lndustries Centre / District lndustries Centre
                                                <asp:Label ID="lblGM" runat="server"></asp:Label></p>
                                        </div>
                                        <h4>
                                            Sub :
                                        </h4>
                                        <div class="padding-left-20">
                                            <p>
                                              Reimbursement of 75% of course fee as Entrepreneurship Development Subsidy under industrial Policy Resolution, 2015. 
                                              <br />
                                            </p>
                                        </div>
                                        <h4>
                                        <p>
                                           Ref:- Provisional Sanction Letter No Dt.<asp:Label ID="lbldate" runat="server"></asp:Label>of D.I Odisha
                                        </p>
                                            Sir,</h4>
                                        <div class="padding-left-20">
                                            <p>
                                              
                                                In accordance with the provisions laid down in Industrial Policy Resolution 2015 and operational guidelines,
                                                the claim for reimbursement of 75% course fee (limited to Rs. 50,000/-) as sanctioned vide letter under reference towards 
                                                Entrepreneurship Development Subsidy is submitted herewith with following particulars.

                                            </p>
                                        </div>
                                    </div>
                                    <div class="prievewdynamicdata">
                                        <div class="panel-group padding-20" id="accordion" role="tablist" aria-multiselectable="true">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="headingOne">
                                                    <h4 class="panel-title">
                                                        <a>Industrial Unit's Details </a>
                                                    </h4>
                                                </div>
                                                <div id="collapseOne" class="panel-collapsein" role="tabpanel" aria-labelledby="headingOne">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Name of Enterprise/Industrial Unit</label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span><asp:Label ID="LblEnterPrise" CssClass="dataspan" runat="server"
                                                                        Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Organization Type</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblOrgType" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Name of Applicant</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblApplicantName" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Application Applying By</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblApplyBy" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Address of Industrial Unit</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblAddressInd" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="divaadhar" runat="server" visible="false">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Aadhaar Card No</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblAadhaar" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Unit Category</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblUnitCategory" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Unit Type</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblUnitType" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Is Priority</label>
                                                                <div class="col-sm-3 ">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblPriority" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Address of Registered Office of the Industrial Unit</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblRegAddress" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Name of Managing Partner</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblManagingPartner" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    EIN/ IEM/ IL No.</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblEINNo" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Date of EIN/ IEM/ IL Date</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblEINDate" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    PC No.</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblPCNo" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    PC Issuance Date</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblPCInsuranceDate" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Date of Commencement of Production</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblCommenceDate" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="Div5">
                                                    <div class="panel-heading" role="tab" id="Div6">
                                                        <h4 class="panel-title">
                                                            <a>Course Details </a>
                                                        </h4>
                                                    </div>
                                                </div>
                                                <div id="CourseDetails" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Institution Name</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblInstName" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Institution Address</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblInstAddress" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Course Duration</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblCourseDur" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Course Fee</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblCourseFee" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Date of selection</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblDtSelect" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Excepted date of course complitation</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblExpectDt" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="Div3">
                                                    <h4 class="panel-title">
                                                        <a>Availed Details</a>
                                                    </h4>
                                                </div>
                                                <div id="AvailedClaimDetails" class="panel-collapse in" role="tabpanel" aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-5 ">
                                                                    <asp:CheckBox ID="radNeverAvailedPrior" runat="server" />
                                                                    Mark if Subsidy for Plant and Machinery was never availed prior to this
                                                                </label>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-6 ">
                                                                    <asp:CheckBox ID="radSubsidyAvailed" runat="server" />
                                                                    Mark if Subsidy already availed
                                                                </label>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12 ">
                                                                    Details of Subsidy Already availed
                                                                </label>
                                                                <asp:GridView ID="grdIncentiveAvailed" runat="server" CssClass="table table-bordered"
                                                                    AutoGenerateColumns="false" ShowFooter="false">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl#">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSlNo" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Body (Pvt, State Govt (Specify State),GoI)">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblBody" Text='<%# Eval("vchBody") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Name of Financial Institution">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblName" Text='<%# Eval("vchInstitutionName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount Availed">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAmountAvailed" Text='<%# Eval("decAmountAvailed") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Availed Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAvailedDate" Text='<%# Eval("dtmAvailedDate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sanction Order no.">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSanctionOrderNo" Text='<%# Eval("vchSanctionOrderNo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="Div4">
                                                    <h4 class="panel-title">
                                                        <a>Bank Details</a>
                                                    </h4>
                                                </div>
                                                <div id="BankDetails" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-2 ">
                                                                    Account No of Industrial Unit
                                                                </label>
                                                                <div class="col-sm-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblAccNo" runat="server"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-2 ">
                                                                    Bank Name
                                                                </label>
                                                                <div class="col-sm-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblBnkNm" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-2 ">
                                                                    Branch Name
                                                                </label>
                                                                <div class="col-sm-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblBranch" runat="server"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-2 ">
                                                                    IFSC
                                                                </label>
                                                                <div class="col-sm-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblIFSC" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-2 ">
                                                                    MICR No.</label>
                                                                <div class="col-sm-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblMICRNo" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="Div1">
                                                    <h4 class="panel-title">
                                                        <a class="tect-center">Document CheckList</a>
                                                    </h4>
                                                </div>
                                                <div class="panel-body">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th>
                                                                Document Name
                                                            </th>
                                                            <th width="150px">
                                                                View
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Document(s) in support of rehabilitated sick industrial unit treated at par with
                                                                new industrial unit and duly recommended by State Level Inter Institutional Committee
                                                                (SLIIC) for this incentive
                                                            </td>
                                                            <td>
                                                                <a id="LnkViewRehabilDoc" target="_blank" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Documeht(s) in support of Industrial unit seized under Section 29 of the State Financial
                                                                Corporation Act,1951/ SARFAESI Ac|,2002 and thereafter sold to a new entrepreneur
                                                                on sale of assets basis and treated as new industrial unit forthe purpose of this
                                                                IPR
                                                            </td>
                                                            <td>
                                                                <a id="LnkViewIndustryUnitDoc" target="_blank" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Certificate of registration under Indian Partnership Act1932 / Societies Registration
                                                                Act- 1860 / Certificate of incorporation (Memorandum of association & Article of
                                                                Association ) under Company Act1956
                                                            </td>
                                                            <td>
                                                                <a target="_blank" data-toggle="View Documen" title="Upload" id="LnkViewCertificateRegistration"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Certificate on Date of Commencement of production
                                                            </td>
                                                            <td>
                                                                <a target="_blank" id="LnkViewCertificateCommence" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr id="tr_authorizing" runat="server" visible="false">
                                                            <td>
                                                                Authorizing letter such as Power of attorney/ Board Resolution/Society Resolution/
                                                                signed by Authorized Signatory
                                                            </td>
                                                            <td>
                                                                <a id="LnkViewAUTHORIZEDFILE" target="_blank" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr id="tr_Pioneer" runat="server" visible="false">
                                                            <td>
                                                                Document of Priority Sector / Pioneer Unit in each Priority Sector / Migrated industrial
                                                                unit treated as new industrial unit /issued by Director of lndustries, Odisha
                                                            </td>
                                                            <td>
                                                                <a id="LnkViewPinoneerDoc" target="_blank" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Course Details Attachment
                                                            </td>
                                                            <td>
                                                                <a target="_blank" id="hypAttachment" data-toggle="View Documen" title="Upload" runat="server">
                                                                    View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Copy of letter of selection
                                                            </td>
                                                            <td>
                                                                <a target="_blank" id="hypLinkLetterselection" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Undertaking on non-availment of subsidy earlier on this project
                                                            </td>
                                                            <td>
                                                                <a target="_blank" id="lnkviewSubsidy" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Documents in Support of Interest Subsidy Availed if any Link to Upload documents
                                                                like Interest Paid/Gauruntee Fee Paid under CGTMSE or any other valid proofs
                                                            </td>
                                                            <td>
                                                                <a target="_blank" data-toggle="View Documen" id="lnkviewSubsidyAvailed" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Copy of Provisional Sanction Letter
                                                            </td>
                                                            <td>
                                                                <a target="_blank" id="lnkviewProvisional" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Copy of Certificate in support of successful completion of Management Development
                                                                Programme
                                                            </td>
                                                            <td>
                                                                <a target="_blank" data-toggle="View Documen" id="lnkviewcompletion" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="Div2">
                                                    <h4 class="panel-title">
                                                        <a class="text-center">Undertaking</a>
                                                    </h4>
                                                </div>
                                                <div class="preiviewfooter padding-20">
                                                    <p>
                                                        I ,Sri
                                                       <asp:Label ID="lblName" runat="server"></asp:Label>s/o
                                                        <asp:Label ID="lblSoName" runat="server"></asp:Label>
                                                        at present
                                                        <asp:Label ID="lblPresent" runat="server"></asp:Label>
                                                        (designation) of M/S
                                                        <asp:Label ID="lblUnitAddress" runat="server"></asp:Label>
                                                        (name of the industrial unit) certify that the information furnished as above is
                                                        true and correct to the best of my knowledge and belief.</p>
                                                    <p>
                                                        I hereby undertake to abide by the terms and conditions prescribed under the provisions of industrial Policy Resolution, 2015 and its operational guidelines.</p>
                                                    <p>
                                                        I hereby undertake to repay the subsidy or any part thereof with penal interest as decided by the authority lf the information furnished is found to be false/ incorrect / misleading or mis-represented and there has been suppression of facts / materials or if found to have been disbursed in excess of the amount actually admissible for whatsoever reason.</p>
                                                   <%-- <ol>
                                                        <li>lf the information furnished is found to be false / incorrect i misleading or misrepresented
                                                            and there has been suppression of facts / materials or disbursed in excess of the
                                                            amount actually admissible for whatsoever reason.</li>
                                                        <li>lf the patent and intellectual property right registered is revoked by the authority
                                                            for any reason within five years of registration.</li>
                                                    </ol>--%>
                                                    <p>
                                                        I hereby certify that I have not applied for / availed this Subsidy in any manner under any other scheme of the State Govt. or the Central Govt. or any financial institution.</p>
                                                    <p>
                                                       Copies of relevant documents in support of information / facts furnished above are self-attested and enclosed herewith.</p>
                                                    <div class="col-sm-4 ">
                                                    </div>
                                                    <div class="col-sm-2 ">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        Signature of the Proprietor / Managing Partner / Managing Director / Authorized
                                                        Signatory in full and behalf of
                                                        <br />
                                                        M/s :<img id="PreviewImage" src="" alt="" style="width: 144px; height: 70px;" runat="server" /><br />
                                                        <asp:HiddenField ID="hdnId" runat="server" />
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-2">
                                                                    Upload</label>
                                                                <div class="col-sm-6">
                                                                    <asp:FileUpload CssClass="form-control" ID="FluSign" runat="server" onchange="readURL(this);" /></div>
                                                            </div>
                                                        </div>
                                                        Date: <b>6-Sept-2017</b>
                                                        <br />
                                                        Enclose : 
                                                    </div>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-footer">
                                <div class="row">
                                    <div class="col-sm-12 text-right">
                                        <div id="myModal2" class="modal fade" role="dialog">
                                            <div class="modal-dialog modal-md">
                                                <!-- Modal content-->
                                                <div class="modal-content">
                                                    <div class="modal-body text-center">
                                                        <div class="form-group">
                                                            <h4 class="text-success">
                                                                Thanks for submiting your application
                                                            </h4>
                                                            <p>
                                                                Your Application no. : <b>App1234567</b></p>
                                                            <p>
                                                                Expected First response time : <b>7 Days </b>
                                                            </p>
                                                            <p>
                                                                Maximum eligible incentive : <b><i class="fa fa-inr"></i>75,000/-</b></p>
                                                            <p class="text-red">
                                                                <i>* This is an indicative value Disbursement amount may be lesser depending upon application
                                                                    details scrutiny.</i></p>
                                                            <a class="btn btn-success" href="ViewApplicationStatus.aspx">OK</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Button ID="btnApply" runat="server" Text="Apply" CssClass="btn btn-success"
                                            OnClick="btnApply_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
         $(function () {
             $('.datePicker').datepicker({
                 dateFormat: 'dd:mm:yyyy',
                 separator: ' @ ',
                 minDate: new Date(),autoclose: true,
             });
         });
         
        function readURL(input) {
            if (input.files && input.files[0]) {//Check if input has files.
                var reader = new FileReader(); //Initialize FileReader.

                reader.onload = function (e) {
                    $('#PreviewImage').attr('src', e.target.result);
                    $('#PreviewImage').attr('style', 'display:block');
                };
                reader.readAsDataURL(input.files[0]);
            }
        }

    </script>
    </form>
</body>
</html>
