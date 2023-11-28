<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Enterpreneurship_Subsidy_FormPreview.aspx.cs"
    Inherits="incentives_Enterpreneurship_Subsidy_FormPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc5" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/incentive.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/Incentive/JS_Inct_Common_Validation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });
            $('.Pioneersec,.attorneysec,.adhardetails').hide();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
        <div id="divHeader1">
            <uc2:header ID="header1" runat="server" />
        </div>
        <div class="registration-div investors-bg">
            <div id="exTab1" class="container">
                <div class="investrs-tab">
                    <uc5:pealmenu ID="Peal" runat="server" />
                </div>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="1a">
                        <div class="form-sec">
                            <div class="innertabs m-b-10">
                                <ul class="nav nav-pills pull-right">
                                    <li><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                                    <li class="active"><a href="appliedlistwithdetails.aspx">Apply For incentive</a></li>
                                    <li><a href="ViewApplicationStatus.aspx">View Application Status</a></li>
                                </ul>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-header">
                                <div class="iconsdiv">
                                    <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                        <i class="fa fa-print"></i></a>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-body">
                                <div class="incentivepreiview">
                                    <div class="preiviewheader text-center">
                                        <h2>
                                            <asp:Label ID="lblTitle" runat="server" Style="font-weight: 700"></asp:Label>
                                        </h2>
                                        <h4 style="display: none;">
                                            <asp:Label ID="lblTitle2" runat="server" Visible="false"></asp:Label></h4>
                                    </div>
                                    <div class="prieviewdatasec">
                                        <h4>
                                            From</h4>
                                        <div class="padding-left-20">
                                            <p>
                                                M/s :<asp:Label ID="lblMr" runat="server"></asp:Label></p>
                                            <p>
                                                Address :<asp:Label ID="lblAddress" runat="server"></asp:Label></p>
                                            <p>
                                                Dist :<asp:Label ID="lblDist" runat="server"></asp:Label></p>
                                        </div>
                                        <h4>
                                            To</h4>
                                        <div class="padding-left-20">
                                            <p>
                                                The General Manager,<br />
                                                Regional Industries Centre / District Industries Centre
                                                <asp:Label ID="lblDistrict" runat="server"></asp:Label></p>
                                        </div>
                                        <h4>
                                            Sub :
                                        </h4>
                                        <div class="padding-left-20">
                                            <p>
                                                Reimbursement of 75% of course fee as Entrepreneurship Development Subsidy under
                                                Industrial Policy Resolution, 2015.
                                                <br />
                                            </p>
                                        </div>
                                        <h4>
                                            <p>
                                                Ref:- Provisional Sanction Letter No
                                                <asp:Label ID="lblSanctionNo" runat="server"></asp:Label>
                                                Dt.<asp:Label ID="lblSanctiondt" runat="server"></asp:Label>of D.I Odisha
                                            </p>
                                            Sir,</h4>
                                        <div class="padding-left-20">
                                            <p>
                                                In accordance with the provisions laid down in Industrial Policy Resolution 2015
                                                and operational guidelines, the claim for reimbursement of 75% course fee (limited
                                                to Rs. 50,000/-) as sanctioned vide letter under reference towards Entrepreneurship
                                                Development Subsidy is submitted herewith with following particulars.
                                            </p>
                                        </div>
                                    </div>
                                    <div class="prievewdynamicdata">
                                        <div class="panel-group padding-20" id="accordion" role="tablist" aria-multiselectable="true">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="headingOne">
                                                    <h4 class="panel-title">
                                                        <a>Industrial Unit's Details <span class="text-red pull-right " style="margin-right: 20px;">
                                                            All Amounts in INR Lakh</span></a>
                                                    </h4>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Name of Enterprise/Industrial Unit</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_EnterPrise_Name" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Organization Type</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_Org_Type" runat="server" CssClass="form-control-static">
                                                                </asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Name of Applicant</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="TxtApplicantName" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group ">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Applied By</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label runat="server" ID="lblApplyBy" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group " id="DivAppDocType" runat="server" visible="false">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Authorizing letter signed by Authorized Signatory Document Type
                                                            </label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label runat="server" ID="lblDocumentType" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group " id="divadhhardetails" runat="server">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Aadhaar No.</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label runat="server" ID="LblAadhaar" CssClass="form-control-static">--</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Address of Industrial Unit</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_Industry_Address" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Unit Category</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_Unit_Cat" runat="server" CssClass="form-control-static">
                                                                </asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Unit Type</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_Unit_Type" runat="server" CssClass="form-control-static">
                                                                </asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Address of Registered Office of the Industrial Unit</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_Regd_Office_Address" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                <asp:Label ID="Lbl_Org_Name_Type" runat="server" Text="Name of Managing Partner"></asp:Label>
                                                            </label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label runat="server" ID="lbl_Gender_Partner" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                EIN/ IEM/ IL No.</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_EIN_IL_NO" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Date of EIN/ IEM/ IL Date</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_EIN_IL_Date" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div runat="server" id="divbefor">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                    PC No. Befor E/M/D
                                                                </label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_pcno_befor" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <asp:Label for="Iname" class="col-sm-4 col-sm-offset-1" Text="Date of Production Commencement- Before E/M/D"
                                                                    ID="lblAfterEMD" runat="server"></asp:Label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_Prod_Comm_Date_Before" runat="server" CssClass="form-control-static" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <asp:Label for="Iname" class="col-sm-4 col-sm-offset-1" Text="PC Issurance Date Before E/M/D"
                                                                    ID="lblAfterEMD1" runat="server">
                                                                </asp:Label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_PC_Issue_Date_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div runat="server" id="divafter">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <asp:Label for="Iname" class="col-sm-4 col-sm-offset-1" Text=" PC No. After" ID="lbl_PC_No_After"
                                                                    runat="server"></asp:Label>
                                                                <div class="col-sm-6 ">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_PC_No" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <asp:Label for="Iname" class="col-sm-4 col-sm-offset-1" Text="Date of Production Commencement- After E/M/D"
                                                                    ID="lblAfterEMD11" runat="server"></asp:Label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_Prod_Comm_Date_After" runat="server" CssClass="form-control-static" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <asp:Label for="Iname" class="col-sm-4 col-sm-offset-1" Text="PC Issurance Date After E/M/D"
                                                                    ID="lblAfterEMD189" runat="server">
                                                                </asp:Label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_PC_Issue_Date_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                District</label>
                                                            <div class="col-sm-6 ">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_District" runat="server" CssClass="form-control-static">
                                                                </asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                Sector</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_Sector" runat="server" CssClass="form-control-static">
                                                                </asp:Label>
                                                            </div>
                                                            <div class="col-sm-4">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                Sub Sector</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_Sub_Sector" runat="server" CssClass="form-control-static">
                                                                </asp:Label>
                                                            </div>
                                                            <div class="col-sm-4">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Lies in IPR 2015 Priority Sector</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lblIs_Priority" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div runat="server" id="Pioneersec">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4 col-sm-offset-1">
                                                                    Derived Sector</label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Lbl_Derived_Sector" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                    Is Pioneer</label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblIs_Is_Pioneer" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                Lies in Sectoral Policy</label>
                                                            <div class="col-sm-4">
                                                                <span class="colon">:</span>
                                                                <asp:Label runat="server" ID="lbl_Sectoral" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                GSTIN</label>
                                                            <div class="col-sm-4">
                                                                <span class="colon">:</span>
                                                                <asp:Label runat="server" ID="lblGstin" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-4">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="Div5">
                                                    <h4 class="panel-title">
                                                        <a>Course Details </a>
                                                    </h4>
                                                </div>
                                                <div id="CourseDetails" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Institution Name</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblInstName" CssClass="form-control-static" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    <asp:Label ID="lblOtherInsitute" Text="Other Institution Name" runat="server"></asp:Label></label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblOtherInstitution" CssClass="form-control-static" runat="server"
                                                                        Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Institution Location</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblInstLocation" CssClass="form-control-static" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Institution Address</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblInstAddress" CssClass="form-control-static" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Course Duration</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblCourseDur" CssClass="form-control-static" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Course Fee</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblCourseFee" CssClass="form-control-static" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Date of Selection</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblDtSelect" CssClass="form-control-static" runat="server" Text="--"></asp:Label>
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
                                                                <label for="Iname" class="col-sm-6">
                                                                    Has Subsidy/Incentive against the details in this application been availed earlier
                                                                </label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblSubsidyEarlier" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group availdetailsec availgroup1" id="av1" runat="server">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12 ">
                                                                    Details of Subsidy Already Availed
                                                                </label>
                                                                <div class="col-sm-12 ">
                                                                    <asp:GridView ID="grdAssistanceDetailsAD" runat="server" CssClass="table table-bordered"
                                                                        AutoGenerateColumns="false" ShowFooter="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Slno." ItemStyle-Width="5%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="Lbl_Slno" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Disbursing Agency">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblBody" Text='<%# Eval("vchInstitutionName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Sanctioned Amount" ItemStyle-Width="15%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblName" Text='<%# Eval("decSanctionedAmount") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Sanction Order No." ItemStyle-Width="15%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblAmountAvailed" Text='<%# Eval("vchSanctionOrderNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Date of Sanction" ItemStyle-Width="15%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblAvailedDate" Text='<%# Eval("dtmAvailedDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Availed Amount" ItemStyle-Width="15%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblSanctionOrderNo" Text='<%# Eval("decAmountAvailed") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group availgroup1" id="av2" runat="server">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-6">
                                                                    Amount of Differential Claim to be Exempted
                                                                </label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbldiffclaimamt" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="av3" runat="server">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-6">
                                                                    Present Claim for Reimbursement
                                                                </label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblreimamt" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
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
                                                                <label for="Iname" class="col-sm-3 ">
                                                                    Account No of Industrial Unit
                                                                </label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblAccNo" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3 ">
                                                                    Bank Name
                                                                </label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblBnkNm" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Branch Name
                                                                </label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblBranch" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    IFSC Code
                                                                </label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblIFSC" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    MICR No.</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblMICRNo" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default docstatussec">
                                                <div class="panel-heading" role="tab" id="Div4">
                                                    <h4 class="panel-title">
                                                        <a>Documents to be submitted after completion of course</a>
                                                    </h4>
                                                </div>
                                                <div id="AdditionalDocuments" class="panel-collapse collapse in" role="tabpanel"
                                                    aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Date of course complitation
                                                                </label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <div class="input-group" id="Div7">
                                                                        <asp:Label runat="server" ID="txtExcepteddateofcourse" name="txtTimescheduleforyearofcomm"
                                                                            CssClass="form-control-static"></asp:Label>
                                                                    </div>
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
                                                <div class="panel-body ss">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th>
                                                                Document Name
                                                            </th>
                                                            <th width="150px">
                                                                View
                                                            </th>
                                                        </tr>
                                                        <tr runat="server" id="divAuthorizing">
                                                            <td>
                                                                Document(s) signed by Authorized Signatory in support of
                                                                <asp:Label ID="lblinstMultiselect" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <a id="LnkViewMultiselectDoc" target="_blank" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Certificate of incorporation
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="Hyp_View_Org_Doc" runat="server" Target="_blank">View Document
                                                                </asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="tr_Prod_Comm_After_Doc_Name">
                                                            <td>
                                                                <asp:Label ID="Lbl_Prod_Comm_After_Doc_Name" runat="server" Text="Certificate on Date of Commencement of production"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="Hyp_View_Prod_Comm_After_Doc" runat="server" Target="_blank">View Document</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="DivPioneer" visible="false">
                                                            <td>
                                                                <asp:Label ID="Lbl_Pioneer_Doc_Name" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="Hyp_View_Pioneer_Doc" runat="server" Target="_blank">View Document</asp:HyperLink>
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
                                                        <tr id="Sanc" runat="server">
                                                            <td>
                                                                Document details of assistance sanctioned
                                                            </td>
                                                            <td>
                                                                <a target="_blank" id="lnkviewSanction" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr id="UnderTkg" runat="server">
                                                            <td>
                                                                Undertaking on non-availment of subsidy earlier on this project
                                                            </td>
                                                            <td>
                                                                <a target="_blank" id="lnkviewUnderTkg" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="tr_Bank">
                                                            <td>
                                                                <asp:Label ID="lblBank" runat="server" Text="Cancelled cheque document to verify the entered A/c details"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="hypBank" runat="server" Target="_blank">View Document</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Copy of Provisional Sanction Letter
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="lnkviewProvisional" runat="server" Target="_blank">View Document</asp:HyperLink>
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
                                                        I ,
                                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                                                        <%--at present--%>
                                                        <asp:Label ID="lblPresent" runat="server" Visible="false"></asp:Label>
                                                        of M/S
                                                        <asp:Label ID="lblUnitAddress" runat="server"></asp:Label>
                                                        (name of the industrial unit) certify that the information furnished as above is
                                                        true and correct to the best of my knowledge and belief.</p>
                                                    <p>
                                                        I hereby undertake to abide by the terms and conditions prescribed under the provisions
                                                        of Industrial Policy Resolution, 2015 and its operational guidelines.</p>
                                                    <p>
                                                        I hereby undertake to repay the subsidy or any part thereof with penal interest
                                                        as decided by the authority lf the information furnished is found to be false/ incorrect
                                                        / misleading or mis-represented and there has been suppression of facts / materials
                                                        or if found to have been disbursed in excess of the amount actually admissible for
                                                        whatsoever reason.</p>
                                                    <p>
                                                        I hereby certify that I have not applied for / availed this Subsidy in any manner
                                                        under any other scheme of the State Govt. or the Central Govt. or any financial
                                                        institution.</p>
                                                    <p>
                                                        Copies of relevant documents in support of information / facts furnished above are
                                                        self-attested and enclosed herewith.</p>
                                                    <div class="col-sm-4 ">
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4">
                                                                </label>
                                                                <div class="col-sm-6">
                                                                    <img id="PreviewImage" src="" alt="" style="width: 144px; height: 70px; border: 0;"
                                                                        runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row" id="divUpload">
                                                                <label class="col-sm-4">
                                                                    Upload Signature of
                                                                    <asp:Label ID="lblauthority" Text="Applicant" runat="server"></asp:Label>
                                                                    in full and on behalf of M/ s
                                                                    <asp:Label ID="lblUnitAddr" runat="server"></asp:Label></label>
                                                                <div class="col-sm-8">
                                                                    <asp:FileUpload CssClass="form-control" onchange="readURL(this);" ID="flSignature"
                                                                        runat="server" />
                                                                    <small class="text-danger">(.png, .jpg, .jpeg file only and Max file Size 4 MB)</small>
                                                                </div>
                                                            </div>
                                                            <br />
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4">
                                                                    Date:
                                                                </label>
                                                                <div class="col-sm-6">
                                                                    <b>
                                                                        <asp:Label ID="lblDate" runat="server"></asp:Label></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:HiddenField ID="hdnEmail" runat="server" />
                                                    <asp:HiddenField ID="hdnMobile" runat="server" />
                                                    <asp:HiddenField ID="HdnValueFlag" runat="server" />
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

        var msgTitle = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';
        $(function () {
            $('.datePicker').datepicker({
                dateFormat: 'dd:mm:yyyy',
                separator: ' @ ',
                minDate: new Date(), autoclose: true
            });
        });

        function pageLoad() {

            var hdval = $('#HdnValueFlag').val();
            if (hdval == 1) {
                $('.innertabs ,.investrs-tab').hide();
                $('#divHeader1').hide();
                $('#divUpload').hide();
                $('#btnApply').hide();
            }
            $(function () {
                $('.datePicker').datepicker({
                    minDate: new Date(),
                    autoclose: true,
                    format: "dd-M-yyyy"
                });
            });

            $(function () {
                if ($("#hdnRadibutton").val() == 1) {
                    $('.adhardetails').show();
                    $('.attorneysec').hide();
                }
                else if ($("#hdnRadibutton").val() == 2) {
                    $('.attorneysec').show();
                    $('.adhardetails').hide();
                }
            });
            getAllLinks();
        }
        function getAllLinks() {
            $("div.ss a").each(function () {
                //                alert($(this).text());
                var attr = $(this).attr('href');
                if (attr == undefined) {
                    //$(this).closest('tr').hide();
                    $(this).text('No Document');
                }
            });
        }

    </script>
    <script type="text/javascript" language="javascript">

        function alertredirect(msg) {
            jAlert(msg, msgTitle, function (r) {
                if (r) {
                    location.href = 'IncentiveFeedback.aspx?InctUniqueNo='+<%= Request.QueryString["InctUniqueNo"] %> +'&ServiceId=500';
                    return true;
                }
                else {
                    return false;
                }
            });
        }
    </script>
    </form>
</body>
</html>
