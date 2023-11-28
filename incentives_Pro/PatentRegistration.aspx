<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatentRegistration.aspx.cs"
    Inherits="incentives_PatentRegistration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/IncUC.ascx" TagName="Patentuc" TagPrefix="uc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            });

            $('.Pioneersec,.attorneysec,.adhardetails').hide();
            $(".applyby").on("click", function () {
                if ($("input:checked").val() == 'Self') {
                    $('.adhardetails').show();
                    $('.attorneysec').hide();
                }
                else {
                    $('.attorneysec').show();
                    $('.adhardetails').hide();
                }
            });
            $(".optradioPriority").on("click", function () {
                if ($("input:checked").val() == 'Yes') {


                    $('.Pioneersec').show();

                }
                else {

                    $('.Pioneersec').hide();
                }
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
                            </div>
                            <h2>
                               Application For Providing assistance on Patent Registration</h2>
                        </div>
                        <div class="form-body">
                       


                            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                

                <uc4:Patentuc ID="Patientuc" runat="server" />

                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div5">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#PatentDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-minus">
                                                </i>Patent Details </a>
                                        </h4>
                                    </div>
                                    <div id="PatentDetails" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            
                                                   <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                        Patented Items or Processes /Intellectual Property Right Details</label>
                                                    <div class="col-sm-12">
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <th>
                                                                    Item/Process/IPR Name
                                                                </th>
                                                                <th>
                                                                    Name of Authority
                                                                </th>
                                                                <th>
                                                                    Date of Commercial Use
                                                                </th>
                                                                <th colspan="2">
                                                                    Patent/IPR Registration No.
                                                                </th>
                                                                <th>
                                                                    Registration Date
                                                                </th>
                                                                <th colspan="2">
                                                                   Expenditure incurred
                                                                </th>
                                                                <th>
                                                                    Add More
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                   <asp:TextBox ID="TextBox24" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                 <td>
                                                                   <asp:TextBox ID="TextBox9" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox11" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox12" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                 <td>
                                                                     <asp:LinkButton ID="LinkButton43" CssClass="btn btn-danger btn-sm" data-toggle="tooltip" title="Upload Patent /IPR Registration Certificate" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                                </td>

                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div9">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="Text8" class="form-control">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox13" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                     <asp:LinkButton ID="LinkButton44" CssClass="btn btn-danger btn-sm" data-toggle="tooltip" title="Upload Copy of Bills/Vouchers/receipts as Patent Expenditure Statement" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton24" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                          <h4 class="h4-header">
                                                MEANS OF FINANCE FOR PATENT REGISTRATION</h4>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                        Loan Details</label>
                                                    <div class="col-sm-12">
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <th>
                                                                    Sl #
                                                                </th>
                                                                <th>
                                                                    Name of Financial Institution
                                                                </th>
                                                                <th>
                                                                    Amount Availed 
                                                                </th>
                                                                <th>
                                                                    Amount Avaialed Date
                                                                </th>
                                                                <th>
                                                                    Loan Number
                                                                </th>
                                                                <th>
                                                                    Add More
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    1
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox110" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div8">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="Text6" class="form-control">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox8" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton23" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                           
                                        </div>
                                    </div>
                                </div>
                                 <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div4">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#AvailedClaimDetails" aria-expanded="false" aria-controls="collapseThree">
                                                <i class="more-less fa  fa-plus"></i>Availed Details</a>
                                        </h4>
                                    </div>
                                    <div id="AvailedClaimDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                          
                                                <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12">
                                                       Assistance details for Patent/IPR incentives already availed by this enterprise
                                                    </label>
                                                    <div class="col-sm-12  margin-bottom10">
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <th>
                                                                    Sl#
                                                                </th>
                                                                <th>
                                                                    Body (Pvt, State Govt (Specify State),GoI)
                                                                </th>
                                                                <th>
                                                                    Name of Financial Institution
                                                                </th>
                                                                <th>
                                                                    Amount Availed
                                                                </th>
                                                                <th>
                                                                    Availed Date
                                                                </th>
                                                                <th>
                                                                    Sanction Order no.
                                                                </th>
                                                                <th>
                                                                    Add More
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    1
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox2855" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox2933" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox235348" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox2345349" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox38" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton20" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>

                                                <div class="form-group">
            <div class="row">
                <label for="Iname" class="col-sm-5 ">
                   <asp:RadioButton ID="RadioButton2" runat="server" />  Mark if Subsidy for Patent/IPR  was never availed prior to this
                </label>
                
                <div class="col-sm-3 not-active2">
                    <small class="text-gray">Undertaking on non-availment of subsidy earlier on this project</small>
                </div>
                <div class="col-sm-4 not-active2">
                    <span class="colon">:</span>
                   <asp:LinkButton ID="LinkButton2eq6" data-toggle="tooltip" title="View file" CssClass="btn btn-success " runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton21" CssClass="btn btn-warning" data-toggle="tooltip" title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton22" CssClass="btn btn-danger" data-toggle="tooltip" title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                </div>
            </div>
        </div>
         <div class="form-group not-active">
            <div class="row">
                <label for="Iname" class="col-sm-6 ">
                   <asp:RadioButton ID="RadioButton1" runat="server" />  Mark if Subsidy already availed
                </label>
             
            </div>
        </div>
                                              <div class="form-group not-active">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12">
                                                        Details of Incentive already availed for the Patent/IPR being applied for now  
                                                    </label>
                                                    <div class="col-sm-12  margin-bottom10">
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <th>
                                                                    Sl#
                                                                </th>
                                                                <th>
                                                                    Body (Pvt, State Govt (Specify State),GoI)
                                                                </th>
                                                                <th>
                                                                    Name of Financial Institution
                                                                </th>
                                                                <th>
                                                                    Amount Availed
                                                                </th>
                                                                <th>
                                                                    Availed Date
                                                                </th>
                                                                <th>
                                                                    Sanction Order no.
                                                                </th>
                                                                <th>
                                                                    Add More
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    1
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox39" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox40" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox41" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox42" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox43" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton26sada" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group not-active">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3">
                                                        <small class="text-gray">Details of assistance sanctioned / availed so far with sanction
                                                            order no & date and other supporting documents from State Govt / Central Govt /
                                                            Govt. Agencies / Financial lnstituttions</small></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                          <asp:LinkButton ID="LinkButton36" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton37" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton38" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group not-active">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3">
                                                        Amount of Differential Claim to be Exempted
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="TextBox44" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group not-active">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3">
                                                        Present Claim for reimbursement
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="TextBox45" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
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
                             
                                  <a class="btn btn-warning"  >Save as Draft</a> 
                                   <a class="btn btn-success" href="FormPreview.aspx" >Apply</a>
                                   
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
    </script>
    </form>
</body>
</html>
