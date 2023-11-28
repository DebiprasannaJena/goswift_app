<%@ Page Language="C#" AutoEventWireup="true" CodeFile="draftedapplicationdetails.aspx.cs"
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
                                        
                                    <a href="javascript:history.back()" title="Back" id="A3" class="pull-right printbtn">
                            <i class="fa fa-chevron-circle-left"></i></a>
                            </div>
                            <h2>
                               Drafted Application Details </h2>
                        </div>
                        <div class="form-body">
                            <div class="row">
                                <div class="col-sm-12">
                                   <div class="form-group">
                                    <div class="row">
                                    <label class="col-sm-2">Industry Code</label>
                                    <div class="col-sm-4"><span class="colon">:</span>
                                        <input name="TextBox1" type="text" value="12-56-23-56-99999" readonly="readonly" id="Text1" class="form-control">
                                    </div>
                                    <label class="col-sm-2">Industry Name</label>
                                    <div class="col-sm-4"><span class="colon">:</span><input name="TextBox2" type="text" value="JRD Farma &amp; Research" readonly="readonly" id="Text2" class="form-control"></div>
                                    </div>
                                    </div>
                                    <div class="details-section">
                                       <table class="table table-bordered">
                                       <tr><th width="50px">Sl #</th><th>Application Name </th><th>Saved On</th><th width="100px">Details</th></tr>
                                       <tr>
                                       <td>1</td>
                                      <td> Stamp Duty Exemption</td>
                                      <td> 16-Aug-2017</td><td class="text-center"> <a class="" href="Stamp_Duty_Exemption.aspx" title="Details">Details</a></td> 
                                       </tr>
                                     <tr>
                                       <td>2</td>
                                      <td> Interest subsidy</td>
                                      <td> 22-Aug-2017</td><td class="text-center"> <a class="" href="InterestSubsidy.aspx" title="Details">Details</a></td> 
                                       </tr>
                                         <tr>
                                           <td>3</td>
                                      <td>Exemption Premium Land Conversion</td>
                                      <td>  24-Aug-2017</td><td class="text-center"> <a class="" href="Exemption_Premium_Land_Conversion.aspx" title="Details">Details</a></td> 
                                       </tr>
                                       </table>
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
