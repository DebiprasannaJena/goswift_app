<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeementCostSubsidy.aspx.cs" Inherits="incentives_EmployeementCostSubsidy" %>

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

             $('#nextmonthbtn').click(function () {
               
                 $('.monthadd').text("May-16");
                
             })
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
                                	Application For Employment Cost Subsidy Under Industrial Policy Resolution 2015</h2>
                        </div>
                        <div class="form-body">
                            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
      <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="headingOne">
                                        <h4 class="panel-title">


                                            <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"
                                                aria-expanded="true" aria-controls="collapseOne"><i class="more-less fa  fa-plus">
                                                </i>  <span class="text-red pull-right " style="margin-right:20px;" >* All fields in this section are mandatory</span>Industrial Unit's Details   </a>


                                               
                                        </h4>
                                    </div>
                                    <div id="collapseOne" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
                                        <div class="panel-body">
                                         <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Name of Enterprise/Industrial Unit</label>
                                                    
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span><asp:TextBox ID="TextBox2" Text="JRD Farma &amp; Research" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
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
                                                            <asp:TextBox ID="TextBox38" CssClass="form-control" placeholder="1234" runat="server"></asp:TextBox></div>
                                                             <div class="col-sm-4 padding-right-0">
                                                            <asp:TextBox ID="TextBox45" CssClass="form-control" placeholder="1234" runat="server"></asp:TextBox></div>
                                                             <div class="col-sm-4 padding-right-0">
                                                            <asp:TextBox ID="TextBox39" CssClass="form-control"  placeholder="1234" runat="server"></asp:TextBox></div>
                                                            
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
                                                        <asp:TextBox ID="TextBox65" CssClass="form-control" runat="server"></asp:TextBox>
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
                                                        <asp:LinkButton ID="LinkButton1" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton2" CssClass="btn btn-danger" data-toggle="tooltip"
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
                                                        <asp:TextBox ID="TextBox74" CssClass="form-control" ReadOnly="true" Text="1234-5678-1234" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Date of EIN/ IEM/ IL Date</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                         <asp:TextBox ID="TextBox68" CssClass="form-control" ReadOnly="true" Text="25-07-1990" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        PC No.</label>
                                                    <div class="col-sm-6 margin-bottom10">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="TextBox69" CssClass="form-control" Text="1234-5678-1234" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        PC Issuance Date</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                         <asp:TextBox ID="TextBox70" CssClass="form-control" Text="25-07-1990" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                               <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1 ">
                                                        Date of Commencement of Production <span class="text-red">*</span></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker" id="Div1">
                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="Text1" class="form-control">
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                  
                                                </div>
                                            </div>
                                             <div class="form-group">
                                                <div class="row">
                                                 
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        <small class="text-gray">Certificate on Date of Commencement of production</small><span class="text-red">*</span>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton3" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton4" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton5" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
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
                                                href="#ProductionEmploymentDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                                </i>Production & Employment Details </a>
                                        </h4>
                                    </div>
                                    <div id="ProductionEmploymentDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                         <p class="text-red text-right"> All Amouts to be Entered in INR(Exact Amount)</p>
                                          

                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                        Items of Manufacture/Activity</label>
                                                    <div class="col-sm-12">
                                                        <table class="table table-bordered">
                                                    

                                                            <tr>
                                                                <th width="40">
                                                                    Sl #
                                                                </th>
                                                                <th>
                                                                   Product/Service Name
                                                                </th>
                                                                <th>
                                                                    Quantity<span class="text-red">*</span>
                                                                </th>
                                                                <th>
                                                                    Units (Ltrs)<span class="text-red">*</span>
                                                                </th>
                                                                <th>
                                                                    Value 
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
                                                                    <asp:TextBox ID="TextBox16" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox18" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox23" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                              
                                                                
                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div15">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="Text7" class="form-control">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton38" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                    
                                            
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                       <small class="text-gray">Approved Project DPR</small></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                       <asp:LinkButton ID="LinkButton40" data-toggle="tooltip" title="View file" CssClass="btn btn-success " runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton41" CssClass="btn btn-warning" data-toggle="tooltip" title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton42" CssClass="btn btn-danger" data-toggle="tooltip" title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                          
                                              <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                      Employer's ESI/EPF Company Code/Registration No.</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                       <label class="checkbox-inline"><input type="checkbox" value=""> ESI Code</label>
<label class="checkbox-inline"><input type="checkbox" value="">EPF Code</label>

                                                    </div>
                                                </div>
                                            </div>
                                                     <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                        Employer's ESI/EPF Registration Details</label>
                                                    <div class="col-sm-12">
                                                        <table class="table table-bordered">
                                                    

                                                            <tr>
                                                               
                                                                <th>
                                                                   Registration No.
                                                                </th>
                                                                <th>
                                                                   Date
                                                                </th>
                                                               <th>
                                                                  Attachment
                                                                </th>
                                                               
                                                            </tr>
                                                            <tr>
                                                               
                                                                <td>
                                                                    <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                
                                                               <td>
                                                                    <div class="input-group  date datePicker" id="Div2">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="Text2" class="form-control">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                             <td>
                                                                <asp:LinkButton ID="LinkButton9" CssClass="btn btn-danger" data-toggle="tooltip" title="Upload ESI / EPF Registration document" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>     
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3">
                                                        Check Year Eligibility
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <input type="button" class="btn btn-danger" id="lnk_chk_eligible" value="Click Here to Check Year Eligibility">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group yeareligible" style="display: block;">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3">
                                                        Apply For Financial Year
                                                    </label>
                                                    <div class="col-sm-5  margin-bottom10">
                                                        <span class="colon">:</span>
                                                        <table class="table table-bordered">
                                                            <tbody><tr>
                                                                <td>
                                                                    <input type="radio" class="rad_1st_fy" name="RD_FY">
                                                                </td>
                                                                <td>
                                                                    <input name="TextBox18" type="text" value="01/04/2016" id="Text4" disabled="disabled" class="aspNetDisabled form-control">
                                                                </td>
                                                                <td>
                                                                    <input name="TextBox22" type="text" value="31/03/2017" id="Text6" disabled="disabled" class="aspNetDisabled form-control">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <input type="radio" class="rad_2nd_fy" name="RD_FY" disabled="disabled">
                                                                </td>
                                                                <td>
                                                                    <input name="TextBox23" type="text" value="01/04/2017" id="Text8" disabled="disabled" class="aspNetDisabled form-control">
                                                                </td>
                                                                <td>
                                                                    <input name="TextBox24" type="text" value="31/03/2018" id="Text12" disabled="disabled" class="aspNetDisabled form-control">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <input type="radio" class="rad_3rd_fy" name="RD_FY" disabled="disabled">
                                                                </td>
                                                                <td>
                                                                    <input name="TextBox25" type="text" value="01/04/2018" id="Text13" disabled="disabled" class="aspNetDisabled form-control">
                                                                </td>
                                                                <td>
                                                                    <input name="TextBox26" type="text" value="31/03/2019" id="TextBox26" disabled="disabled" class="aspNetDisabled form-control">
                                                                </td>
                                                            </tr>
                                                               <tr>
                                                                <td>
                                                                    <input type="radio" class="rad_3rd_fy" name="RD_FY" disabled="disabled">
                                                                </td>
                                                                <td>
                                                                    <input name="TextBox25" type="text" value="01/04/2019" id="Text14" disabled="disabled" class="aspNetDisabled form-control">
                                                                </td>
                                                                <td>
                                                                    <input name="TextBox26" type="text" value="31/03/2020" id="Text15" disabled="disabled" class="aspNetDisabled form-control">
                                                                </td>
                                                            </tr>
                                                               <tr>
                                                                <td>
                                                                    <input type="radio" class="rad_3rd_fy" name="RD_FY" disabled="disabled">
                                                                </td>
                                                                <td>
                                                                    <input name="TextBox25" type="text" value="01/04/2020" id="Text16" disabled="disabled" class="aspNetDisabled form-control">
                                                                </td>
                                                                <td>
                                                                    <input name="TextBox26" type="text" value="31/03/2021" id="Text17" disabled="disabled" class="aspNetDisabled form-control">
                                                                </td>
                                                            </tr>
                                                        </tbody></table>
                                                    </div>
                                                </div>
                                            </div>


                                            <h4 class="h4-header">Provide Details for the Month of <label class="monthadd">Apr-16</label></h4>
                                                     <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                      Employees on Company Payroll Details (Provide Employee Count)</label>
                                                    <div class="col-sm-12">
                                                        <table class="table table-bordered">
                                                    
                                              
                                                            <tr>
                                                                <th>
                                                                  Employee Segregation > Grade V
                                                                </th>
                                                                <th>
                                                                  Male
                                                                </th>
                                                                <th>
                                                                  	Female
                                                                </th>
                                                                <th>
                                                                    Male (Displaced persons)
                                                                </th>
                                                                <th>
                                                                   Female (Displaced persons)
                                                                </th>
                                                                 <th>
                                                                   Diabled Persons Male
                                                                </th>
                                                                <th>
                                                                  Disabled Persons Female
                                                                </th>
                                                                <th>
                                                                    TOTAL
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                             <th>Unskilled</th>   
                                                              <td>
                                                                  <asp:TextBox ID="TextBox30" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                <td> <asp:TextBox ID="TextBox31" CssClass="form-control" runat="server"></asp:TextBox></td> 
                                                                  <td> <asp:TextBox ID="TextBox32" CssClass="form-control" runat="server"></asp:TextBox></td>  
                                                                   <td> <asp:TextBox ID="TextBox36" CssClass="form-control" runat="server"></asp:TextBox></td>  
                                                                    <td> <asp:TextBox ID="TextBox374" CssClass="form-control" runat="server"></asp:TextBox></td>   
                                                                    <td> <asp:TextBox ID="TextBox383" CssClass="form-control" runat="server"></asp:TextBox></td> 
                                                                      <td> <asp:TextBox ID="TextBox397" CssClass="form-control" runat="server"></asp:TextBox></td>  
                                                                      
                                                                
                                                            </tr>
                                                            <tr>
                                                             <th>Semi-skilled</th>   
                                                              <td>
                                                                  <asp:TextBox ID="TextBox40" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                <td> <asp:TextBox ID="TextBox41" CssClass="form-control" runat="server"></asp:TextBox></td> 
                                                                  <td> <asp:TextBox ID="TextBox42" CssClass="form-control" runat="server"></asp:TextBox></td>  
                                                                   <td> <asp:TextBox ID="TextBox43" CssClass="form-control" runat="server"></asp:TextBox></td>  
                                                                    <td> <asp:TextBox ID="TextBox44" CssClass="form-control" runat="server"></asp:TextBox></td>   
                                                                    <td> <asp:TextBox ID="TextBox456" CssClass="form-control" runat="server"></asp:TextBox></td> 
                                                                      <td> <asp:TextBox ID="TextBox46" CssClass="form-control" runat="server"></asp:TextBox></td>  
                                                                      
                                                                
                                                            </tr>
                                                             <tr>
                                                             <th>Skilled</th>   
                                                              <td>
                                                                  <asp:TextBox ID="TextBox47" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                <td> <asp:TextBox ID="TextBox48" CssClass="form-control" runat="server"></asp:TextBox></td> 
                                                                  <td> <asp:TextBox ID="TextBox49" CssClass="form-control" runat="server"></asp:TextBox></td>  
                                                                   <td> <asp:TextBox ID="TextBox50" CssClass="form-control" runat="server"></asp:TextBox></td>  
                                                                    <td> <asp:TextBox ID="TextBox51" CssClass="form-control" runat="server"></asp:TextBox></td>   
                                                                    <td> <asp:TextBox ID="TextBox52" CssClass="form-control" runat="server"></asp:TextBox></td> 
                                                                      <td> <asp:TextBox ID="TextBox53" CssClass="form-control" runat="server"></asp:TextBox></td>  
                                                                      
                                                                
                                                            </tr>
                                                             <tr>
                                                             <th>Supervisory</th>   
                                                              <td>
                                                                  <asp:TextBox ID="TextBox54" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                <td> <asp:TextBox ID="TextBox55" CssClass="form-control" runat="server"></asp:TextBox></td> 
                                                                  <td> <asp:TextBox ID="TextBox56" CssClass="form-control" runat="server"></asp:TextBox></td>  
                                                                   <td> <asp:TextBox ID="TextBox57" CssClass="form-control" runat="server"></asp:TextBox></td>  
                                                                    <td> <asp:TextBox ID="TextBox58" CssClass="form-control" runat="server"></asp:TextBox></td>   
                                                                    <td> <asp:TextBox ID="TextBox59" CssClass="form-control" runat="server"></asp:TextBox></td> 
                                                                      <td> <asp:TextBox ID="TextBox60" CssClass="form-control" runat="server"></asp:TextBox></td>  
                                                                      
                                                                
                                                            </tr>
                                                             <tr>
                                                             <th>Managerial	</th>   
                                                              <td>
                                                                  <asp:TextBox ID="TextBox61" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                <td> <asp:TextBox ID="TextBox62" CssClass="form-control" runat="server"></asp:TextBox></td> 
                                                                  <td> <asp:TextBox ID="TextBox63" CssClass="form-control" runat="server"></asp:TextBox></td>  
                                                                   <td> <asp:TextBox ID="TextBox64" CssClass="form-control" runat="server"></asp:TextBox></td>  
                                                                    <td> <asp:TextBox ID="TextBox656" CssClass="form-control" runat="server"></asp:TextBox></td>   
                                                                    <td> <asp:TextBox ID="TextBox66" CssClass="form-control" runat="server"></asp:TextBox></td> 
                                                                      <td> <asp:TextBox ID="TextBox67" CssClass="form-control" runat="server"></asp:TextBox></td>  
                                                                      
                                                                
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                                <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                      Employer's Contribution Paid Towards ESI/EPF</label>
                                                    <div class="col-sm-12">
                                                    <table class="table table-bordered">
                                                    
                                               <tr>
                                                                <th>
                                                                 Employee Segregation
                                                                </th>
                                                                <th>
                                                                 EPF/ESI Amount 
                                                                </th>
                                                               
                                                            </tr>
                                                            <tr>
                                                             <th>Male</th>   
                                                              <td>
                                                                  <asp:TextBox ID="TextBox33" CssClass="form-control text-right" runat="server"></asp:TextBox></td>
                                                                 
                                                                      
                                                                
                                                            </tr>
                                                            <tr>
                                                             <th>Female</th>   
                                                              <td>
                                                                  <asp:TextBox ID="TextBox72" CssClass="form-control text-right" runat="server"></asp:TextBox></td>
                                                               
   
                                                            </tr>
                                                             <tr>
                                                             <th> Male (Displaced persons)</th>   
                                                              <td>
                                                                  <asp:TextBox ID="TextBox79" CssClass="form-control text-right" runat="server"></asp:TextBox></td>
                                                               
                                                                      
                                                                
                                                            </tr>
                                                             <tr>
                                                             <th>Female (Displaced persons)</th>   
                                                              <td>
                                                                  <asp:TextBox ID="TextBox86" CssClass="form-control text-right" runat="server"></asp:TextBox></td>
                                                               
                                                                      
                                                                
                                                            </tr>
                                                             <tr>
                                                             <th>Diabled Persons Male	</th>   
                                                              <td>
                                                                  <asp:TextBox ID="TextBox93" CssClass="form-control text-right" runat="server"></asp:TextBox></td>
                                                                
                                                                      
                                                                
                                                            </tr>
                                                             <tr>
                                                             <th>Disabled Persons Female</th>   
                                                              <td>
                                                                  <asp:TextBox ID="TextBox34" CssClass="form-control text-right" runat="server"></asp:TextBox></td>
                                                                
                                                                      
                                                                
                                                            </tr>
                                                             
                                                             <tr>
                                                             <th>Total</th>   
                                                              <td class="text-right">
                                                                  <strong>-</strong></td>
                                                                
                                                                      
                                                                
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                              <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                       <small class="text-gray">Company's ESI/EPF Contribution statement</small></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                       <asp:LinkButton ID="LinkButton36" data-toggle="tooltip" title="View file" CssClass="btn btn-success " runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton37" CssClass="btn btn-warning" data-toggle="tooltip" title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton39" CssClass="btn btn-danger" data-toggle="tooltip" title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 "><a  id="nextmonthbtn"  class="btn btn-success">Update status for Next Month</a></label>
                                                    </div>
                                                    </div>
                                             <hr />

                                             <div class="form-group" style="margin-top:20px;">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                      Reasons for delay in project implementation <small>(Beyond Management Control)</small> </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="TextBox35" CssClass="form-control" TextMode="MultiLine"  runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                           
                                             <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                      <small class="text-gray"> Documents in support of reason for delay to be examined by the by Empowered Committee</small></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                       <asp:LinkButton ID="LinkButton43" data-toggle="tooltip" title="View file" CssClass="btn btn-success " runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton44" CssClass="btn btn-warning" data-toggle="tooltip" title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton45" CssClass="btn btn-danger" data-toggle="tooltip" title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
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
                                                    <label for="Iname" class="col-sm-3 ">
                                                       <small class="text-gray"> Document in support of date of first investment in fixed capital i.e. land i building
                                                        / plant & machinery and balancing equipment</small></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                       <asp:LinkButton ID="LinkButton32" data-toggle="tooltip" title="View file" CssClass="btn btn-success " runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton33" CssClass="btn btn-warning" data-toggle="tooltip" title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton34" CssClass="btn btn-danger" data-toggle="tooltip" title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
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
                                                                    Interest Amount Original(in INR in Lakhs)
                                                                </th> <th>
                                                                    Investment Amount (in INR Lakhs)E/M/D
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    1
                                                                </td>
                                                                <td>
                                                                    Land
                                                                </td>
                                                                <td class="text-right">
                                                                    <input type="text" value="45.7" class="form-control text-right"/>
                                                                </td>
                                                                <td class="text-right">
                                                                    <input type="text" value="-" class="form-control text-right"/>
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
                                                                    <input type="text" value="45.7" class="form-control text-right"/>
                                                                </td>
                                                                <td class="text-right">
                                                                    <input type="text" value="-" class="form-control text-right"/>
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
                                                                    <input type="text" value="45.7" class="form-control text-right"/>
                                                                </td>
                                                                <td class="text-right">
                                                                    <input type="text" value="-" class="form-control text-right"/>
                                                                </td>
                                                                <tr>
                                                                    <td>
                                                                        4
                                                                    </td>
                                                                    <td>
                                                                        Balancing Equipment
                                                                    </td>
                                                                     <td class="text-right">
                                                                    <input type="text" value="45.7" class="form-control text-right"/>
                                                                </td>
                                                                <td class="text-right">
                                                                    <input type="text" value="-" class="form-control text-right"/>
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
                                                                    <input type="text" value="45.7" class="form-control text-right"/>
                                                                </td>
                                                                <td class="text-right">
                                                                    <input type="text" value="-" class="form-control text-right"/>
                                                                </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <strong>Total</strong>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <strong>365.7
                                                                    </td>
                                                                     <td class="text-right">
                                                                    <strong> -</strong>
                                                                </td>
                                                                </tr>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <h4 class="h4-header">
                                                MEANS OF FINANCE FOR WORKING CAPITAL</h4>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                        Term Loan Details</label>
                                                    <div class="col-sm-12">
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <th width="40">
                                                                    Sl #
                                                                </th>
                                                                <th>
                                                                    Name of Financial Institution
                                                                </th>
                                                                <th>
                                                                    Address
                                                                </th>
                                                                <th>
                                                                    Term Loan Amount
                                                                </th>
                                                                <th>
                                                                    Sanction Date
                                                                </th>
                                                                <th>
                                                                    Availed Amount
                                                                </th>
                                                                <th>
                                                                    Availed Date
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
                                                                    <asp:TextBox ID="TextBox20" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox19" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox21" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div7">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="Text10" class="form-control">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox22" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div6">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="Text9" class="form-control">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton27" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                              <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                        <small class="text-gray">Term loan sanction order of Financial lnstitute (s) / Banks</small></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                      <asp:LinkButton ID="LinkButton28" data-toggle="tooltip" title="View file" CssClass="btn btn-success " runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton29" CssClass="btn btn-warning" data-toggle="tooltip" title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton30" CssClass="btn btn-danger" data-toggle="tooltip" title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
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
                                                href="#AvailedClaimDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-minus">
                                                </i>Availed Claim Details</a>
                                        </h4>
                                    </div>
                                    <div id="AvailedClaimDetails" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                         <p class="text-red text-right"> All Amouts to be Entered in INR(Exact Amount)</p>
                                             <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-2 ">
                                                      Differential amount of claim</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                       
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                     Details of Employment Cost Subsidy already availed from the state Govt (other State Govt. / GoI )</label>
                                                    <div class="col-sm-12">
                                                    			

                                                        <table class="table table-bordered">
                                                        					

                                                            <tr>
                                                                <th>
                                                                   Name  of Financial Institution
                                                                </th>
                                                                <th>
                                                                   Sanction Order No.
                                                                </th>
                                                               <th>
                                                                   Sanction Date
                                                                </th>
                                                                <th>
                                                                   Sanctioned Amount
                                                                </th>
                                                                <th>
                                                                   Availed Amount
                                                                </th>
                                                                <th>
                                                                    Availed Date
                                                                </th>
                                                                <th>
                                                                    Add More
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                              
                                                                <td>
                                                                    <asp:TextBox ID="TextBox12" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                
                                                                 <td>
                                                                    <asp:TextBox ID="TextBox15" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                 <td>
                                                                    <div class="input-group  date datePicker" id="Div9">
                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="Text3" class="form-control">
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                                </td>
                                                                 <td>
                                                                    <asp:TextBox ID="TextBox24" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                 <td>
                                                                    <asp:TextBox ID="TextBox25" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                 <td>
                                                                    <div class="input-group  date datePicker" id="Div13">
                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="Text5" class="form-control">
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                                </td>
                                                                
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton16" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                               <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                       <small class="text-gray">Details of assistance applied for/Sanctioned/availed so far with sanction order & Date</small></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                       <asp:LinkButton ID="LinkButton17" data-toggle="tooltip" title="View file" CssClass="btn btn-success " runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton18" CssClass="btn btn-warning" data-toggle="tooltip" title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton20" CssClass="btn btn-danger" data-toggle="tooltip" title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
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
                                                        Account No. of Industrial Unit <span class="text-red">*</span></label>
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
                                  <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div3">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#AdditionalDocuments" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                                </i>Additional Documents</a>
                                        </h4>
                                    </div>
                                    <div id="AdditionalDocuments" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                       OSPCB-Consent to Operate</label>
                                                    <div class="col-sm-12">
                                                    			

                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <th>
                                                                   Document Name
                                                                </th>
                                                                <th>
                                                                    Upload
                                                                </th>
                                                               
                                                                <th>
                                                                    Add More
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                              
                                                                <td>
                                                                    <asp:TextBox ID="TextBox10" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                
                                                                <td>
                                                                    <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                                                                    
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton19" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
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
                                    <asp:Button ID="btnEdit" runat="server" Text="Apply" CssClass="btn btn-success" />
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
