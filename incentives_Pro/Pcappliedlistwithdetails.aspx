<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pcappliedlistwithdetails.aspx.cs"
    Inherits="incentives_incentiveoffered" %>

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
            $('#checkfalse').click(function () {
                alert("hii");
            });
            $('#checktrue').click(function () {
                alert("hii");
            })
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
                                <li ><a href="incentiveoffered.aspx">Incentive Offered</a></li>
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
                                         <a href="javascript:void(0);" title="Delete" id="A2" class="pull-right printbtn">
                                    <i class="fa fa-trash-o"></i></a>
                            </div>
                            <h2>
                               Your Units </h2>
                        </div>
                        <div class="form-body">
                            <div class="row">
                                <div class="col-sm-12">
                             
                                    <div class="details-section">
                                       <table class="table table-bordered">
                                       <tr><th><input type="checkbox" value=""></th><th>Industry Name </th><th>EIN/IEM</th><th>PC</th><th>District </th><th>Address</th><th>Industry Code</th><th>Enterprise Type</th><th>Sector</th><th>Drafted Applications</th><th>Action</th></tr>
                                       <tr>
                                       <td><input type="checkbox" value=""></td>
                                       <td> Jagannath Texttiles</td>
                                       <td class="text-center"><a href="javascript:void(0)" title="EIN/IEM Document" target="_blank"><i class="fa fa-file-text-o"></i></a></td>
                                        <td><a href="javascript:void(0)" title="Production Certificate" target="_blank"><i class="fa fa-file-text-o"></i></a></td>
                                        <td> Koraput</td>   <td> #7878,Plot No. 144, Lane No.-2</td>   <td> 12-56-23-56-23456</td>   <td> Manufacturing</td>   <td> Apparel</td>   <td class="text-center"><a href="draftedapplicationdetails.aspx" title="Drafted Applications"><span class="badge">3</span></a></td> <td> <a class="" href="unitdetails.aspx" title="Check Eligibility">Check Eligibility</a></td> 
                                       </tr>
                                       <tr>
                                       <td><input type="checkbox" value=""></td>
                                       <td> JRD Farma &amp; Research</td>
                                          <td class="text-center"><a href="javascript:void(0)" title="EIN/IEM Document" target="_blank"><i class="fa fa-file-text-o"></i></a></td>
                                        <td><a href="javascript:void(0)" title="Apply">Apply</a></td>
                                        <td> Khorda</td>   <td> Near NH5 Khurda,Plot No. 145, Lane No.-2</td>   <td> 12-56-23-56-23466</td>   <td> Manufacturing</td>   <td> Bio-Technology</td>   <td class="text-center"><a href="draftedapplicationdetails.aspx" title="Drafted Applications"><span class="badge">1</span></a></td> <td> <a class="" href="unitdetails.aspx" title="Check Eligibility">Check Eligibility</a></td> 
                                       </tr>
                                         <tr>
                                       <td><input type="checkbox" value=""></td>
                                       <td> JRD Farma &amp; Research</td>
                                        <td><a href="javascript:void(0)" title="Apply">Apply</a></td>
                                         <td class="text-center"><a href="javascript:void(0)" title="EIN/IEM Document" target="_blank"><i class="fa fa-file-text-o"></i></a></td>
                                        <td> Khorda</td>   <td> Near NH5 Khurda,Plot No. 145, Lane No.-2</td>   <td> 12-56-23-56-23466</td>   <td> Manufacturing</td>   <td> Bio-Technology</td>  <td class="text-center"><a href="javascript:void(0)" title="Drafted Applications"><span class="badge">0</span></a></td>  <td> <a class="" href="unitdetails.aspx" title="Check Eligibility">Check Eligibility</a></td> 
                                       </tr>
                                       </table>
                                    </div>
                                </div>
                             
                            </div>
                               <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                              
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="headingTwo">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#PromoterInformation" aria-expanded="false" aria-controls="collapseTwo"><i
                                                    class="more-less fa  fa-minus"></i> List of Eligible Subsidy/Incentives For </a>
                                        </h4>
                                    </div>
                                    <div id="PromoterInformation" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingTwo">
                                        <div class="panel-body">
                                          <div class="form-group">
                                    <div class="row">
                                    <label class="col-sm-2">Industry Code</label>
                                    <div class="col-sm-4"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Text="12-56-23-56-99999" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-2">Industry Name</label>
                                    <div class="col-sm-4"><span class="colon">:</span><asp:TextBox ID="TextBox2" CssClass="form-control" runat="server" Text="JRD Farma &amp; Research" ReadOnly="true"></asp:TextBox></div>
                                    </div>
                                    </div>


                                           <div class="form-group">
                                    <div class="row">
                                    <label class="col-sm-12">You provide the following eligibility Criteria :</label>
                                   </div>
                                    </div>
                                       <div class="form-group">
                                    <div class="row">
                                      <div class="col-sm-12">
                                    
                                    <p>Sector : <strong><asp:Label ID="Label1" runat="server" Text="Pharmaceuticals"></asp:Label></strong>
                                     Date of FFCI :<strong><asp:Label ID="Label2" runat="server" Text="15-Aug-2016"></asp:Label></strong> &amp;
                                    Date of production :<strong> <asp:Label ID="Label3" runat="server" Text="23-Aug-2016"></asp:Label></strong> </p>
                                    
                                    </div>
                                 
                                    </div>
                                    </div>
                                          
                                              <div class="form-group">
                                    <div class="row">
                                    <label class="col-sm-12">Based on which, Your industrial unit is Eligible for the incentive under the following policies &amp; incentive Category :</label>
                                   </div>
                                    </div>
                                       <div class="form-group">
                                    <div class="row">
                                      <div class="col-sm-12">
                                    
                                    <p>Policies : <strong><asp:Label ID="Label4" runat="server" Text="IPR 2015, Pharmaceuticals Policy 2016, District category B, Employment &amp; Investment Rating B2 "></asp:Label></strong>
                                      </p>
                                    
                                    </div>
                                 
                                    </div>
                                    </div>

                                        <div class="form-group">
                                    <div class="row">
                                    <label class="col-sm-12">Base on your date of FFCI and date of Production Commencement and Total :</label>
                                   </div>
                                    </div>
                                       <div class="form-group">
                                    <div class="row">
                                     <label class="col-sm-3">Your Unit Type is determind to be</label>
                                      <div class="col-sm-9">
                                    <label class="radio-inline"><input type="radio" checked="checked" name="optradio">New</label>
<label class="radio-inline"><input type="radio" name="optradio">Existibg (E/M/D)</label>
<label class="radio-inline"><input type="radio" name="optradio">Migrated</label>
<label class="radio-inline"><input type="radio" name="optradio">Rehabitated</label>
<label class="radio-inline"><input type="radio" name="optradio">Transferred</label>
                                    
                                    </div>
                                 
                                    </div>
                                    </div>



                                    
                                        </div>
                                    </div>
                                </div>

                                  <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="headingThree">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#IndustryDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                                </i>View Eligibility Incentive Summary </a>
                                        </h4>
                                    </div>
                                    <div id="IndustryDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="details-section">
                                       <table class="table table-bordered">
                                       <tr><th>Incentive</th><th>Unit type </th><th>Incentive Category</th><th>Action</th></tr>
                                   <tr><td> Interest subsidy</td><td>New</td><td>Pre Production</td><td><a data-toggle="modal" data-target="#myModal">Apply</a></td></tr>
                                   <tr><td>Patent Registration</td><td>New</td><td>Pre Production</td><td><a  href="PatentRegistration.aspx">Apply</a></td></tr>
                                   <tr><td>Subsidy for Plant &amp; MC</td><td>Existing</td><td>Post Production</td><td><a href="Subsidy_Plant_MC.aspx">Apply</a></td></tr>
                                  <tr><td> Stamp Duty Exemption</td><td>New</td><td>Pre Production</td><td><a href="Stamp_Duty_Exemption.aspx">Apply</a></td></tr>
                                   <tr><td>Technical Know How</td><td>New</td><td>Pre Production</td><td><a  href="TechnicalKnowhow.aspx">Apply</a></td></tr>
                                   <tr><td>Employment Cost Subsidy</td><td>Existing</td><td>Post Production</td><td><a href="EmployeementCostSubsidy.aspx">Apply</a></td></tr>

                                   <tr><td> Enterpreneurship Subsidy</td><td>New</td><td>Pre Production</td><td><a href="Enterpreneurship_Subsidy.aspx">Apply</a></td></tr>
                                   <tr><td>One time reimbursement of energy audit cost under IPR 2015</td><td>New</td><td>Pre Production</td><td><a  href="Reimbursementofenergyaudit.aspx">Apply</a></td></tr>
                                   <tr><td>Exemption Premium Land Conversion</td><td>Existing</td><td>Post Production</td><td><a href="Exemption_Premium_Land_Conversion.aspx">Apply</a></td></tr>
                                   <tr><td>Quality certification</td><td>Existing</td><td>Post Production</td><td><a href="Quality_Certification.aspx">Apply</a></td></tr>
                                   <tr><td>Employement Rating</td><td>Existing</td><td>Post Production</td><td><a href="EmploymentRating.aspx">Apply</a></td></tr>
                                   <tr><td>Category of Districts</td><td>Existing</td><td>Post Production</td><td><a href="District.aspx">Apply</a></td></tr>
                                   <tr><td>Classification of Industry</td><td>Existing</td><td>Post Production</td><td><a href="ClassificationofIndustry.aspx">Apply</a></td></tr>
                                    <tr><td>Power</td><td>Existing</td><td>Post Production</td><td><a href="Power.aspx">Apply</a></td></tr> 
                                    <tr><td>Training Subsidy</td><td>Existing</td><td>Post Production</td><td><a href="TrainingSubsidy.aspx">Apply</a></td></tr> 
                                     <tr><td>Land for Workers Hostels</td><td>Existing</td><td>Post Production</td><td><a href="LandforWorkersHostels.aspx">Apply</a></td></tr>
                                      <tr><td>Application For Capital Grant To Support Quality Infrastructure</td><td>Existing</td><td>Post Production</td><td><a href="CapitalgranttosupportQinf.aspx">Apply</a></td></tr>
                                       <tr><td>Application For  Plant and Machinery</td><td>Existing</td><td>Post Production</td><td><a href="PlantandMachinery.aspx">Apply</a></td></tr>
                                        <tr><td>Pioneer units under IPR 2015</td><td>Existing</td><td>Post Production</td><td><a href="PioneerUnit.aspx">Apply</a></td></tr>
                                       </table>
                                        
                                    </div>
                                </div>
                             
                            </div>
                                        </div>
                                    </div>
                                </div>
                          <div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog modal-lg">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header bg-gray">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">We want to make Sure your Application is Eligible, Please provide us / Confirm a few More details before Proceeding</h4>
      </div>
      <div class="modal-body">
          <div class="form-group">
                                    <div class="row">
                                    <label class="col-sm-3">Total No. of Male Workers</label>
                                    <div class="col-sm-3"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" Text="" ></asp:TextBox>
                                    </div>
                                    <label class="col-sm-3">Total No. of Female Workers</label>
                                    <div class="col-sm-3"><span class="colon">:</span><asp:TextBox ID="TextBox4" CssClass="form-control" runat="server" Text=""></asp:TextBox></div>
                                    </div>
                                    </div>
                                      <div class="form-group">
                                    
                                   <h4 class="h4-header"> We Found that your an Existing Unit</h4>
                                  
                                   
                                   
                                    </div>
                                      <div class="form-group">
                                    <div class="row">
                                    <label class="col-sm-9">Please mentioned Amount of additional investment on fixed assets for Expansion / Mordernization / Diversification</label>
                                    <div class="col-sm-3"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" Text="14,00,00,00" ></asp:TextBox>
                                    </div>
                                    
                                    </div>
                                    </div>
                                       <div class="row">
                                   
                                    <div class="col-sm-12 text-right">
                                      <a class="btn btn-danger" data-toggle="modal" data-target="#myModal3" >Proceed to Check if false</a>
                                      <a class="btn btn-success" data-toggle="modal" data-target="#myModal2"  >Proceed to Check if true</a>
                                    </div>
                                    
                                    </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
      </div>
    </div>

  </div>
</div>

            <div id="myModal2" class="modal fade" role="dialog">
  <div class="modal-dialog modal-sm">

    <!-- Modal content-->
    <div class="modal-content">
     
      <div class="modal-body text-center">
        
                                      <div class="form-group">
                                    
                                   <h4 class="text-success"> Congratulations You are Eligible</h4>
                                   <br />
                                  <a class="btn btn-success" href="InterestSubsidy.aspx">Proceed to Apply</a>
                                   
                                   
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
                                    
                                   <h4 > Sorry You are not Eligible</h4>
                                 <hr>
                                   <label>Reason for Rejection</label>
                                  <p> You are not eligible as thia amount is less than 50% book value of your fixed assets !</p>
                                    <br />
                                  <a class="btn btn-danger" href="appliedlistwithdetails.aspx">Check eligibilty for another incentive</a>
                                   
                                   
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
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    
    </form>
</body>
</html>
