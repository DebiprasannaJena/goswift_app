<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnchorTenant.aspx.cs"
    Inherits="Subsidy_Plant_MC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
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
    <style>
    .not-active
        {
            pointer-events: none;
            cursor: default;
        }
    </style>
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
                            </div>
                            <h2>
                              Application For Anchor Tenant</h2>
                        </div>
                        <div class="form-body">
                            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                 <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="headingOne">
                                        <h4 class="panel-title">


                                            <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"
                                                aria-expanded="true" aria-controls="collapseOne"><i class="more-less fa  fa-minus">
                                                </i>  <span class="text-red pull-right " style="margin-right:20px;" >* All fields in this section are mandatory</span>Industrial Unit's Details   </a>


                                               
                                        </h4>
                                    </div>
                                    <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                        <div class="panel-body">
                                         <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Name of Enterprise/Industrial Unit</label>
                                                    
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span><asp:TextBox ID="TextBox23" Text="JRD Farma &amp; Research" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                          <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Organization Type</label>
                                                    <div class="col-sm-6 margin-bottom10">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="DropDownList2" CssClass="form-control" runat="server">
                                                            <asp:ListItem>Proprietorship</asp:ListItem>
                                                            <asp:ListItem>Partnership</asp:ListItem>
                                                            <asp:ListItem>Private Limited</asp:ListItem>
                                                            <asp:ListItem>Public Limited</asp:ListItem>
                                                             <asp:ListItem>One Person Company</asp:ListItem>
                                                           <asp:ListItem>Co-operative</asp:ListItem>
                                                            <asp:ListItem>Trust</asp:ListItem>
                                                            <asp:ListItem>Society</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Name of Applicant</label>
                                                    <div class="col-sm-1" style="padding-right: 0px">
                                                        <span class="colon">:</span><asp:DropDownList CssClass="form-control" ID="DropDownList1"
                                                            runat="server">
                                                            <asp:ListItem>Mr.</asp:ListItem>
                                                            <asp:ListItem>Ms.</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="TextBox5" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                       Application Applying By</label>
                                                    <div class="col-sm-6 margin-bottom10">
                                                        <span class="colon">:</span>
                                                        <label class="radio-inline">
                                                            <input type="radio" class="applyby" value="Self" name="radioapply">Self
                                                        </label>
                                                        <label class="radio-inline">
                                                            <input type="radio" class="applyby" value="Authorized Person" name="radioapply">Authorized Person
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group adhardetails">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Aadhar No.</label>
                                                    <div class="col-sm-6 margin-bottom10">
                                                    <span class="colon">:</span>
                                                       
                                                        <div class="col-sm-4 padding-right-0 padding-left-0">
                                                            <asp:TextBox ID="TextBox26" CssClass="form-control" placeholder="1234" runat="server"></asp:TextBox></div>
                                                             <div class="col-sm-4 padding-right-0">
                                                            <asp:TextBox ID="TextBox27" CssClass="form-control" placeholder="1234" runat="server"></asp:TextBox></div>
                                                             <div class="col-sm-4 padding-right-0">
                                                            <asp:TextBox ID="TextBox30" CssClass="form-control"  placeholder="1234" runat="server"></asp:TextBox></div>
                                                            
                                                         <div class="clerfix"></div>
                                                    </div>
                                                </div>
                                            </div>
                                                <div class="form-group attorneysec">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        <small class="text-gray">Please provide Authorizeing latter such as Power of attorney/ Board Resolution/Society Resolution/ signed
                                                            by Authorized Signatory</small></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton10" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton11" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton12" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Address of Industrial Unit</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="TextBox6" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Unit Category</label>
                                                    <div class="col-sm-6 margin-bottom10">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="DropDownList3" CssClass="form-control" runat="server">
                                                            <asp:ListItem>Micro</asp:ListItem>
                                                            <asp:ListItem>Small</asp:ListItem>
                                                            <asp:ListItem>Medium</asp:ListItem>
                                                            <asp:ListItem>Large</asp:ListItem>
                                                           
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                          
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Unit Type</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="DropDownList67" CssClass="form-control" runat="server">
                                                            <asp:ListItem>Existing E/M/D</asp:ListItem>
                                                            <asp:ListItem>New Unit</asp:ListItem>
                                                            <asp:ListItem>Migrated Unit Treated As New</asp:ListItem>
                                                            <asp:ListItem>Rehabilitated Unit Treated As New</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        <small class="text-gray">Document(s) in support of rehabilitated sick industrial unit
                                                            treated at par with new industrial unit and duly recommended by State Level lnter
                                                            lnstitutional Committee (SLllC) for this incentive</small></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton6" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton7" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton8" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        <small class="text-gray">Documeht(s) in support of lndustrial unit seized under Section
                                                            29 of the State Financial Corporation Act,1951/ SARFAESI Ac|,2002 and thereafter
                                                            sold to a new entrepreneur on sale of assets basis and treated as new industrial
                                                            unit forthe purpose of this IPR </small>
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton94" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton106" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton114" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Is Priority</label>
                                                    <div class="col-sm-6 margin-bottom10">
                                                        <span class="colon">:</span>
                                                        <label class="radio-inline">
                                                            <input type="radio" class="optradioPriority" value="Yes" name="optradioPriority">Yes
                                                        </label>
                                                        <label class="radio-inline">
                                                            <input type="radio" class="optradioPriority"  value="No" name="optradioPriority">No
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group Pioneersec">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Is Pioneer</label>
                                                    <div class="col-sm-6 margin-bottom10">
                                                        <span class="colon">:</span>
                                                        <label class="radio-inline">
                                                            <input type="radio" name="optradioPioneer" >Yes
                                                        </label>
                                                        <label class="radio-inline">
                                                            <input type="radio" name="optradioPioneer"/>No
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                              <div class="form-group Pioneersec">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        <small class="text-gray">Certificate of Priority Sector / Pioneer Unit in each Priority
                                                            Sector / Migrated industrial unit treated as new industrial unit /issued by Director
                                                            of lndustries, Odisha</small></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton123" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton139" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton140" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Address of Registered Office of the Industrial Unit</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <label ><input type="checkbox" />Same as Address of Industrial Unit</label>
                                                        <asp:TextBox ID="TextBox7" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                           
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Name of Managing Partner</label>
                                                    <div class="col-sm-1" style="padding-right: 0px">
                                                        <span class="colon">:</span><asp:DropDownList CssClass="form-control" ID="DropDownList6"
                                                            runat="server">
                                                            <asp:ListItem>Mr.</asp:ListItem>
                                                            <asp:ListItem>Ms.</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="TextBox13" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        <small class="text-gray">Certificate of registration under lndian Partnership Act1932
                                                            / Societies Registration Act- 1860 / Certificate of incorporation (Memorandum of
                                                            association & Article of Association ) under Company Act1956</small></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton15" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton16" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton17" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        EIN/ IEM/ IL No.</label>
                                                    <div class="col-sm-6 margin-bottom10">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="TextBox15" CssClass="form-control" ReadOnly="true" Text="1234-5678-1234" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Date of EIN/ IEM/ IL Date</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                         <asp:TextBox ID="TextBox24" CssClass="form-control" ReadOnly="true" Text="25-07-1990" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        

                                        </div>
                                    </div>
                                </div>
                                  <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div8">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#ProductionEmploymentDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                                </i>Major Operational Activities of the company </a>
                                        </h4>
                                    </div>
                                    <div id="ProductionEmploymentDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                         <p class="text-red text-right"> All Amouts to be Entered in INR(Exact Amount)</p>
                                               <div class="form-group">
            <div class="row">
                <label for="Iname" class="col-sm-12">
                    Items of Manufacture/Activity
                </label>
                <div class="col-sm-12  margin-bottom10">
                    <table class="table table-bordered">
                        <tr>
                            <th>
                                Sl#
                            </th>
                            <th>
                                Product/Service Name
                            </th>
                            <th>
                                Quantity
                            </th>
                            <th>
                                Units
                            </th>
                            <th>
                                Value 
                            </th>
                            <th>
                                Action
                            </th>
                        </tr>
                        <tr>
                            <td>
                                1
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control">
                                    <asp:ListItem>MT</asp:ListItem>
                                    <asp:ListItem>Litre</asp:ListItem>
                                    <asp:ListItem>Kg</asp:ListItem>
                                    <asp:ListItem>Other</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:LinkButton ID="LinkButton1" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div> 
         <h4 class="h4-header">
            Employment Generated</h4>
        <div class="form-group">
            <div class="row">
                <label for="Iname" class="col-sm-3 ">
                    Direct Empolyment IN NUMBERS <small>(on Company Payroll)</small></label>
                <div class="col-sm-3">
                    <span class="colon">:</span>
                    <input name="txtTimescheduleforyearofcomm" type="text" id="Text5" class="form-control">
                </div>
                <label for="Iname" class="col-sm-3 ">
                    Contractual Employment <small>( IN NUMBERS )</small></label>
                <div class="col-sm-3">
                    <span class="colon">:</span>
                    <input name="txtTimescheduleforyearofcomm" type="text" id="Text6" class="form-control">
                </div>
            </div>
        </div>
         <div class="form-group ">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4">
                                                        <small class="text-gray">Document in Support of Number of EMployyes shown as directly employed (e.g. Certificate by DLO)- this certificate has to be taken</small></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton4" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton18" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton19" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                                </div>
        <div class="form-group">
            <div class="row">
                <div class="col-sm-12">
                    <table class="table table-bordered">
                        <tr>
                            <th>
                                Type
                            </th>
                            <th>
                                Current
                            </th>
                            <th>
                                Proposed
                            </th>
                        </tr>
                        <tr>
                            <td>
                                Managerial
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox4" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Supervisory
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox17" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox18" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Skilled
                            </td>
                            <td>
                               <asp:TextBox ID="TextBox28" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                            <td>
                               <asp:TextBox ID="TextBox29" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Semi-Skilled
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox32" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox33" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Unskilled
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox34" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox35" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                TOTAL
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox36" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox37" CssClass="form-control" runat="server"></asp:TextBox>
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
                                    <div class="panel-heading" role="tab" id="headingThree">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#IndustryDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                                </i>Investment Details </a>
                                        </h4>
                                    </div>
                                    <div id="IndustryDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                         <p class="text-red text-right"> All Amouts to be Entered in INR(Exact Amount)</p>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                        Date of First Fixed Capital Investment <small>(for land/Building/plant and machinery
                                                            & Balancing Equipment)</small></label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker" id="Div10">
                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="Text11" class="form-control">
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                          
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                        Total Capital Investment</label>
                                                    <div class="col-sm-12">
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <th>
                                                                    Sl #
                                                                </th>
                                                                <th>
                                                                    Investment Head
                                                                </th>
                                                                <th>
                                                                    Interest Amount
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    1
                                                                </td>
                                                                <td>
                                                                     <asp:DropDownList ID="DropDownList9" CssClass="form-control" runat="server">
                                                                    <asp:ListItem>Land type</asp:ListItem>
                                                                     <asp:ListItem>Own ancestoral land </asp:ListItem>
                                                                     <asp:ListItem>Own land(purchased)</asp:ListItem>
                                                                     <asp:ListItem>Private land leased </asp:ListItem>
                                                                     <asp:ListItem>Govt. land leased </asp:ListItem>
                                                                       <asp:ListItem> IDCO land </asp:ListItem>
                                                                     <asp:ListItem>IDCO Shed</asp:ListItem>
                                                                     </asp:DropDownList>
                                                                </td>
                                                                <td class="text-right">
                                                                   <input type="text" class="form-control text-right" value="58.9"/>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    2
                                                                </td>
                                                                <td>
                                                                    Building
                                                                </td>
                                                                <td class="text-right">
                                                                    <input type="text" class="form-control text-right" value="58.9"/>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    3
                                                                </td>
                                                                <td>
                                                                    Plant & Machinery
                                                                </td>
                                                                <td class="text-right">
                                                                    <input type="text" class="form-control text-right" value="58.9"/>
                                                                </td>
                                                                <tr>
                                                                    <td>
                                                                        4
                                                                    </td>
                                                                    <td>
                                                                        Balancing Equipment
                                                                    </td>
                                                                    <td ><input type="text" class="form-control text-right" value="58.9"/> 
                                                                    </td>
                                                                </tr>


                                                                <tr>
                                                                    <td>
                                                                        5
                                                                    </td>
                                                                    <td>
                                                                        Other Fixed Assests
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <input type="text" class="form-control text-right" value="58.9"/>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <strong>Total</strong>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <strong>365.7</strong>
                                                                    </td>
                                                                </tr>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                              <h4 class="h4-header">
                                                MEANS OF FINANCE</h4>

      <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                        Internal Source of fund</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="TextBox16" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                              <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                        Term Loan Details</label>
                                                    <div class="col-sm-12">
                                                      <table class="table table-bordered">
                                                            <tr>
                                                                <th rowspan="2" width="40">
                                                                    Sl #
                                                                </th>
                                                                <th rowspan="2">
                                                                    Name of Financial Institution
                                                                </th>
                                                                <th colspan="2">
                                                                    Location
                                                                </th>
                                                                <th rowspan="2">
                                                                    Term Loan Amount
                                                                </th>
                                                                <th rowspan="2">
                                                                    Sanction Date
                                                                </th>
                                                                <th rowspan="2">
                                                                    Availed Amount
                                                                </th>
                                                                <th rowspan="2">
                                                                    Availed Date
                                                                </th>
                                                                <th rowspan="2">
                                                                    Add More
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                            <th>State</th><th>City</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    1
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox98" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox99" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox200" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox100" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div7">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="Text10" class="form-control">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox101" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div6">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="Text9" class="form-control">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton3" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                         <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                        Working Capital Loan Details</label>
                                                    <div class="col-sm-12">
                                                      <table class="table table-bordered">
                                                            <tr>
                                                                <th rowspan="2" width="40">
                                                                    Sl #
                                                                </th>
                                                                <th rowspan="2">
                                                                    Name of Financial Institution
                                                                </th>
                                                                <th colspan="2">
                                                                    Location
                                                                </th>
                                                                <th rowspan="2">
                                                                    Term Loan Amount
                                                                </th>
                                                                <th rowspan="2">
                                                                    Sanction Date
                                                                </th>
                                                                <th rowspan="2">
                                                                    Availed Amount
                                                                </th>
                                                                <th rowspan="2">
                                                                    Availed Date
                                                                </th>
                                                                <th rowspan="2">
                                                                    Add More
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                            <th>State</th><th>City</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    1
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox19" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox20" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox21" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox22" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div3">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="Text14" class="form-control">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox25" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div9">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="Text15" class="form-control">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton9" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
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
                                    <div class="panel-heading" role="tab" id="Div2">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#BriefDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                                </i>Brief Details of Proposed Activity</a>
                                        </h4>
                                    </div>
                                    <div id="BriefDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                         <p class="text-red text-right"> All Amouts to be Entered in INR(Exact Amount)</p>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                       Brief Details of Proposed Activity</label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                      <textarea class="form-control"></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                              <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                       Proposed Business Plan</label>
                                                    <div class="col-sm-12">
                                                        <table class="table table-bordered">
                                                        <tr><th>Proposed Business Plan  </th><th width="50%">Details thereof</th></tr>
                                                       <tr><th>Prospects Downstream Enterprises for utilization of end, intermediate & bye - products for value addition through; if any </th><td><textarea class="form-control"></textarea></td></tr>
                                                        <tr><th> Prospects of ancillary enterprises; if any</th><td><textarea class="form-control"></textarea></td></tr>
                                                         <tr><th>  Development of utility i nfrastructure like roduct I lines; if any</th><td><textarea class="form-control"></textarea></td></tr>
                                                          <tr><th> Externalities like R & D facilities or technology sourcing mechanism from Technical institutions / university research so as to provide the smaller firm an opportunity to lower their costs, and improve their prospects for future profitability & growth; if any</th><td><textarea class="form-control"></textarea></td></tr>
                                                           <tr><th>Proposed CFC like testing laboratory, training/ skill development centre etc. ;ifany </th><td><textarea class="form-control"></textarea></td></tr>
                                                            <tr><th> Any Others</th><td><textarea class="form-control"></textarea></td></tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                           <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                       Proposed common facilities to attract Secondary Tenants</label>
                                                    <div class="col-sm-6">
                                                        <table class="table table-bordered">
                                                       <tr><td>1</td><td><input type="text" class="form-control"/></td><td> <asp:LinkButton ID="LinkButton2" CssClass="btn btn-success " data-toggle="tooltip" title="Add More" runat="server"><i class="fa fa-plus"></i></asp:LinkButton></td></tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                         <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                       Details of Secoundry tenants</label>
                                                    <div class="col-sm-6">
                                                    <a Class="btn btn-success" data-toggle="tooltip" title="Download Excel" download href="Secondarytanent.xlsx"><i class="fa fa-file-excel-o"></i></a>
                                                       
                                                        <asp:LinkButton ID="LinkButton14" CssClass="btn btn-danger" data-toggle="tooltip" title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3">
                                                        <small class="text-gray">Details of Business plan for attracting Secondary Tenants</small></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton24" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton25" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton26" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                                <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                        <small class="text-gray">Consent of Secondary Tenant ( if any )</small></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton27" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton28" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton29" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                          
      
                                        </div>
                                    </div>
                                </div>


                                       <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div1">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#DLSWCA" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                                </i>DLSWCA / SLSWCA / HLCA Apporval Details</a>
                                        </h4>
                                    </div>
                                    <div id="DLSWCA" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                         <p class="text-red text-right"> All Amouts to be Entered in INR(Exact Amount)</p>
                                         <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                       Date of Approval</label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker" id="Div4">
                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="Text1" class="form-control">
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                           </div>
                                        
                                           <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                      Requirment of land approved by DLSWCA / SLSWCA / HLCA</label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                        
                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="Text3" class="form-control">
                                                           
                                                    </div>
                                                </div>
                                                
      
                                        </div>
                                        
                                              <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                       Cost of land </label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                        
                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="Text4" class="form-control">
                                                           
                                                    </div>
                                                </div>
                                                
      
                                        </div>
                                              <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                       Eligible Amount od subsidy(Details Calculation Sheet to Enclosed)</label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                        
                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="Text2" class="form-control">
                                                           
                                                    </div>
                                                </div>
                                                
      
                                        </div>
         <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3">
                                                        <small class="text-gray">Copy of Approval of DLSWCA / SLSWCA / HLCA</small></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton5" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton13" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton20" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                                <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                        <small class="text-gray">Copy of Documents to substantitate of land cost</small></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton21" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton22" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton23" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                              
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div5">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#BankDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                                </i>Bank Details</a>
                                        </h4>
                                    </div>
                                    <div id="BankDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            
                                           <p class="text-red text-right"> All Amouts to be Entered in INR(Exact Amount)</p>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-2 ">
                                                        Account No of Industrial Unit <span class="text-red">*</span></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <label for="Iname" class="col-sm-2 ">
                                                        Brank Name <span class="text-red">*</span></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                              <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-2 ">
                                                      Branch Name <span class="text-red">*</span></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                       <label for="Iname" class="col-sm-2 ">
                                                     IFSC <span class="text-red">*</span></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                              <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-2 ">
                                                      MICR No.</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control"></asp:TextBox>
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
                                    <asp:Button ID="Button1" runat="server" Text="Save as Draft" CssClass="btn btn-warning" /> <asp:Button ID="btnEdit" runat="server" Text="Apply" CssClass="btn btn-success" />
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
