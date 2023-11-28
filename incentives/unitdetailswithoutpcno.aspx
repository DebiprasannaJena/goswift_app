<%@ Page Language="C#" AutoEventWireup="true" CodeFile="unitdetailswithoutpcno.aspx.cs" Inherits="incentives_incentiveoffered" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/incentive.css" rel="stylesheet" type="text/css" />
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
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="registration-div investors-bg">
            <div id="exTab1" class="">
                <div class="investrs-tab">
                    <ul class="nav nav-pills">
                        
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
                                    <li class="active"><a href="appliedindustrylist.aspx">Apply For incentive</a></li>
                                    <li><a href="ViewApplicationStatus.aspx">View Application Status</a></li>
                                </ul>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-header">
                                <h2>
                                    Incentive Available</h2>
                            </div>
                            <div class="form-body incentivesec">
                                <div class="row">
                                    <div class="col-sm-8 padding-right-0">
                                        <h2>
                                            Unit Profile</h2>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Industry Code</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Label1" CssClass="dataspan" runat="server" Text="Label">12-56-23-56-99999</asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    Industry Name</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Label2" CssClass="dataspan" runat="server" Text="Label">Jagannath Texttiles</asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                          <ul  class="nav nav-pills">
			<li class="active">
        <a  href="#UnitDetails" data-toggle="tab">Unit Details</a>
			</li>
			<li><a href="#AdditionalDetails" data-toggle="tab">Additional Details</a>
			</li>
			
		</ul>
        	<div class="tab-content clearfix">
			  <div class="tab-pane active" id="UnitDetails">
                 <div class="panel panel-default">
                                                
                                                <div id="PromoterInformation" class="panel-collapse collapse in" role="tabpanel"
                                                    aria-labelledby="headingTwo">
                                                    <div class="panel-body">
                                                        <h3>
                                                            Basic Details</h3>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-6">
                                                                    Date (or Proposed Date) of First Fixed Capital Investment</label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                   
                                                                  <div class="input-group  date datePicker" id="Div1">
                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="Text5" class="form-control">
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                                  
                                                                </div>
                                                            </div>
                                                        </div>
                                                       
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4">
                                                                    Sector</label>
                                                                <div class="col-sm-8">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label3" CssClass="dataspan" runat="server" Text="Label">Tourism</asp:Label>
                                                                    &nbsp;&nbsp;<label><input type="checkbox" value="" checked="checked" readonly="true">
                                                                        Is Prority</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4">
                                                                    District</label>
                                                                <div class="col-sm-8">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label4" CssClass="dataspan" runat="server" Text="Label">Khurda</asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4">
                                                                    Unit Type</label>
                                                                <div class="col-sm-8">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label5" CssClass="dataspan" runat="server" Text="Label">Large Scale</asp:Label>
                                                                    &nbsp;&nbsp;
                                                                    <label>
                                                                        <input type="radio" value="" checked="checked" readonly="true">
                                                                        Manufacturing</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4">
                                                                    Unit Category</label>
                                                                <div class="col-sm-8">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label6" CssClass="dataspan" runat="server" Text="Label">New Unit</asp:Label>
                                                                    &nbsp;&nbsp;<label><input type="radio" value="" checked="checked" readonly="true">
                                                                        Services</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="margin-bottom15">
                                                        </div>
                                                        <h3>
                                                            Employment Details</h3>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4">
                                                                    Total no. of employees</label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox3" runat="server" Text="230" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-4">
                                                                    Total no. of state domicial employees</label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox4" runat="server" Text="122" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4">
                                                                    Total no. of labour</label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox5" Text="65" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-4">
                                                                    Count of state domicial labour</label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox6" Text="15" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4">
                                                                    Total no. of skilled labour</label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox7" Text="45" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-4">
                                                                    Count of state domicial skilled labour</label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox8" Text="12" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4">
                                                                    Total no. of unskilled labour</label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox9" Text="44" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-4">
                                                                    Count of state domicial unskilled labour</label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox10" Text="32" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4">
                                                                    Total no. of Managerial/Admin
                                                                </label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox11" Text="37" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-4">
                                                                    Count of state domicial Managerial/Admin</label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox12" Text="7" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="margin-bottom15">
                                                        </div>
                                                        <h3>
                                                            Investment / Porposed Investment Details</h3>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4">
                                                                    <small>First Fixed Capital Investment (FFCI) (In Cr.)</small></label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label7" runat="server" CssClass="dataspan" Text="Label">10</asp:Label>
                                                                </div>
                                                                <label class="col-sm-4">
                                                                    <small>Total investment in Fixed Assets (The above field is inclusive of FFCI) (In Cr.)</small></label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label8" runat="server" CssClass="dataspan" Text="Label">130</asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4">
                                                                    Capital Investment in Plant &amp; Mechinery</label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label9" runat="server" CssClass="dataspan" Text="Label">90</asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
              
              </div>
           
              <div class="tab-pane" id="AdditionalDetails">
            <div class="panel panel-default">
            
                                               
                                                <div id="IndustryDetails" class="panel" role="tabpanel" aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-3">
                                                                    Total no. of employees</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-3">
                                                                    Total no. of state domicial employees</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-3">
                                                                    Total no. of labour</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox16" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-3">
                                                                    Count of state domicial labour</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox17" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                           <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-3">
                                                                    Total no. of employees</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-3">
                                                                    Total no. of state domicial employees</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-3">
                                                                    Total no. of labour</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox15" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-3">
                                                                    Count of state domicial labour</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox18" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                           <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-3">
                                                                    Total no. of employees</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox19" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-3">
                                                                    Total no. of state domicial employees</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox20" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-3">
                                                                    Total no. of labour</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox21" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-3">
                                                                    Count of state domicial labour</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox22" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                           <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-3">
                                                                    Total no. of employees</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox23" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-3">
                                                                    Total no. of state domicial employees</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox24" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-3">
                                                                    Total no. of labour</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox25" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-3">
                                                                    Count of state domicial labour</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox26" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                           <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-3">
                                                                    Total no. of employees</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox27" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-3">
                                                                    Total no. of state domicial employees</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox28" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-3">
                                                                    Total no. of labour</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox29" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-3">
                                                                    Count of state domicial labour</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="TextBox30" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                         

            </div>
            </div>

   </div>

                                        
                                    </div>
                                    <div class="col-sm-4 ">
                                        <h2>
                                            Units Documents Available</h2>
                                        <div>
                                          <table class="table table-bordered bg-white">
                                                <tr>
                                                    <th>
                                                        Document Name
                                                    </th>
                                                    <th width="80px;">
                                                        Available
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Production Certificate
                                                    </td>
                                                    <td class="text-center">
                                                      <a href="javascript:void(0);" title="Apply">Apply</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Certificate on Date of Commencement of production
                                                    </td>
                                                    <td class="text-center">
                                                        <a href="javascript:void(0);" title="Download PDF"><i class="fa fa-file-pdf-o"></i>
                                                        </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Sanction order of loan availed from FI / Banks for the purpose of obtaining euality Certification
                                                    </td>
                                                    <td class="text-center">
                                                          <a id="LinkButton4" class="" data-toggle="tooltip" title="" href="javascript:void(0)" data-original-title="Upload"><i class="fa fa-cloud-upload"></i></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                      Document in support of delay in implementation condoned by Empowered Committee
                                                    </td>
                                                    <td class="text-center">
                                                       <a id="A1" class="" data-toggle="tooltip" title="" href="javascript:void(0)" data-original-title="Upload"><i class="fa fa-cloud-upload"></i></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                      Valid statutory clearances including consent to operate issued by OSPCB
                                                    </td>
                                                    <td class="text-center">
                                                         <a id="A2" class="" data-toggle="tooltip" title="" href="javascript:void(0)" data-original-title="Upload"><i class="fa fa-cloud-upload"></i></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Certificate on Proposed Date of Commencement of production
                                                    </td>
                                                    <td class="text-center">
                                                          <a id="A3" class="" data-toggle="tooltip" title="" href="javascript:void(0)" data-original-title="Upload"><i class="fa fa-cloud-upload"></i></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                       Approved DPR / Project Profile / Scheme -as the case may be for Origin al I E I M / D
                                                    </td>
                                                    <td class="text-center">
                                                         <a id="A4" class="" data-toggle="tooltip" title="" href="javascript:void(0)" data-original-title="Upload"><i class="fa fa-cloud-upload"></i></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                       Certificate of Priority Sector / Pioneer Unit in each Priority Sector / Migrated industrial unit treated as new industrial unit /issued by Director of lndustries, Odisha
                                                    </td>
                                                    <td class="text-center">
                                                          <a id="A5" class="" data-toggle="tooltip" title="" href="javascript:void(0)" data-original-title="Upload"><i class="fa fa-cloud-upload"></i></a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                           <h2>
                                            Application History</h2>

                                            <div class="incentive-portlet  text-center">
                                            <h1><span> <a href="draftedapplicationdetails.aspx" class="text-right" title="Drafted Application">Drafted Application : 6</a></span></h1>
                                         
                                            </div>

                                             <div class="incentive-portlet  text-center">
                                            
                                            <p>Applied Incentives</p>
                                            <ul><li><a href="javascript:void(0)" title="Approved"><span>5</span> <br />Approved</a></li><li><a href="javascript:void(0)" title="Scrutiny in Progress"><span>5</span><br /> Scrutiny in progress</a></li><li><a href="javascript:void(0)" title="Rejected"><span>3</span><br /> Rejected</a></li></ul>

                                            </div>

                                    </div>
                                  
                                </div>
                            </div>
                            <div class="form-footer">
                                <div class="row">
                                    <div class="col-sm-12 text-right">
                                        
                                              <a data-toggle="modal" data-target="#undertakingipr2015" title="Proceed for Eligibility Check" Class="btn btn-success"> Proceed for Eligibility Check</a>
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
                    <a href="NoPCnoappliedlistwithdetails.aspx" class="btn btn-success" title="Submit">Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
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
    </script>
    </form>
</body>
</html>
