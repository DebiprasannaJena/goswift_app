<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Quality_Certification.aspx.cs"
    Inherits="incentives_Quality_Certification" %>

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
                                Application For Quality Certification</h2>
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
                                                        <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
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
                                                            <asp:TextBox ID="TextBox1566" CssClass="form-control" placeholder="1234" runat="server"></asp:TextBox></div>
                                                             <div class="col-sm-4 padding-right-0">
                                                            <asp:TextBox ID="TextBox45" CssClass="form-control" placeholder="1234" runat="server"></asp:TextBox></div>
                                                             <div class="col-sm-4 padding-right-0">
                                                            <asp:TextBox ID="TextBox24564" CssClass="form-control"  placeholder="1234" runat="server"></asp:TextBox></div>
                                                            
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
                                                        <asp:LinkButton ID="LinkButton2" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton3" CssClass="btn btn-danger" data-toggle="tooltip"
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
                                                        <asp:TextBox ID="TextBox298" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
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
                                                        <asp:LinkButton ID="LinkButton1598" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton6978" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton105" CssClass="btn btn-danger" data-toggle="tooltip"
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
                                                        <div class="input-group  date datePicker" id="Div2">
                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="Text2" class="form-control">
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
                                                        <asp:LinkButton ID="LinkButton1134" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton1223" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton1398" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div6">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#ProductionEmploymentDetails" aria-expanded="false" aria-controls="collapseThree">
                                                <i class="more-less fa  fa-plus"></i>Production & Employment Details </a>
                                        </h4>
                                    </div>
                                    <div id="ProductionEmploymentDetails" class="panel-collapse collapse" role="tabpanel"
                                        aria-labelledby="headingThree">
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
                                                                    <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                                    <asp:LinkButton ID="LinkButton31" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
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
                                                    <label for="Iname" class="col-sm-5">
                                                        Date of First Fixed Capital Investment (for land/Building/plant and machinery &
                                                        Balancing Equipment) - Original
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker" id="Div3">
                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="Text3" class="form-control">
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-5">
                                                        Date of First Fixed Capital Investment (for land/Building/plant and machinery &
                                                        Balancing Equipment) - For E/M/D
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker" id="Div4">
                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="Text4" class="form-control">
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-5">
                                                        <small class="text-gray">Document(s) in support of date of first investment in fixed
                                                            capital i.e. land i building / plant & machinery and balancing equipment</small></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton22" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton23" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton24" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
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
                                                                    Investment Amount Original
                                                                </th>
                                                                <th>
                                                                    Investment Amount E/M/D
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
                                                                    <asp:TextBox ID="TextBox20" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox18" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                                    <asp:TextBox ID="TextBox21" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox27" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                                    <asp:TextBox ID="TextBox29" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox30" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    4
                                                                </td>
                                                                <td>
                                                                    Balancing Equipment
                                                                </td>
                                                                <td class="text-right">
                                                                    <asp:TextBox ID="TextBox31" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox32" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    5
                                                                </td>
                                                                <td>
                                                                    Electrical Installation
                                                                </td>
                                                                <td class="text-right">
                                                                    <asp:TextBox ID="TextBox33" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox34" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    6
                                                                </td>
                                                                <td>
                                                                    Loading, On-Loading, Transportation , Duties,Tax,INCTC
                                                                </td>
                                                                <td class="text-right">
                                                                    <asp:TextBox ID="TextBox35" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox36" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    7
                                                                </td>
                                                                <td>
                                                                    IDCO Shed
                                                                </td>
                                                                <td class="text-right">
                                                                    <asp:TextBox ID="TextBox37" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox38" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    8
                                                                </td>
                                                                <td>
                                                                    Lab & R&D
                                                                </td>
                                                                <td class="text-right">
                                                                    <asp:TextBox ID="TextBox41" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox42" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    9
                                                                </td>
                                                                <td>
                                                                    Other Fixed Assests
                                                                </td>
                                                                <td class="text-right">
                                                                    <asp:TextBox ID="TextBox39" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox40" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <strong>Total</strong>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td class="text-right">
                                                                    <strong>
                                                                        <asp:TextBox ID="TextBox43" runat="server" CssClass="form-control"></asp:TextBox></strong>
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
                                    <div class="panel-heading" role="tab" id="Div8">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#AvailedClaimDetails" aria-expanded="false" aria-controls="collapseThree">
                                                <i class="more-less fa  fa-plus"></i>MEANS OF FINANCE</a>
                                        </h4>
                                    </div>
                                    <div id="AvailedClaimDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                          <p class="text-red text-right"> All Amouts to be Entered in INR(Exact Amount)</p>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12">
                                                        Loan Details
                                                    </label>
                                                    <div class="col-sm-12  margin-bottom10">
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <th>
                                                                    Sr#
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
                                                                    Saction Date
                                                                </th>
                                                                <th>
                                                                    Availed Amount
                                                                </th>
                                                                <th>
                                                                    Availed Date
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
                                                                    <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox15" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox19" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div5">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="Text5" class="form-control">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox17" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div5">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="Text6" class="form-control">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton8" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4">
                                                        <small class="text-gray">Term loan sanction order of OSFC / Banks / FI</small></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton16" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton17" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton18" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4">
                                                        <small class="text-gray">Sanction order of loan availed from FI / Banks for the purpose
                                                            of obtaining euality Certification</small>
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton25" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton26" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton27" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div7">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#LandDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-minus">
                                                </i>Quality Certification Details</a>
                                        </h4>
                                    </div>
                                    <div id="LandDetails" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12">
                                                        Product/Activities Quality Certification Details
                                                    </label>
                                                    <div class="col-sm-12  margin-bottom10">
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <th>
                                                                    &nbsp;
                                                                </th>
                                                                <th>
                                                                    &nbsp;
                                                                </th>
                                                                <th colspan="3" style="text-align: center;">
                                                                    <strong>Certificate Details</strong>
                                                                </th>
                                                                <th colspan="3" style="text-align: center;">
                                                                    <strong>Certificate Renewal Details</strong>
                                                                </th>
                                                                <th colspan="2" style="text-align: center;">
                                                                    <strong>Expenditure Details</strong>
                                                                </th>
                                                                <th>
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    Product/Activity Name
                                                                </th>
                                                                <th>
                                                                    Name & address of the Registration Authority
                                                                </th>
                                                                <th>
                                                                    Certificate No
                                                                </th>
                                                                <th>
                                                                    Certificate Date
                                                                </th>
                                                                <th>
                                                                    Attachment
                                                                </th>
                                                                  <th>
                                                                    Renewal No
                                                                </th>
                                                                <th>
                                                                   Renewal Date
                                                                </th>
                                                                <th>
                                                                    Attachment
                                                                </th>
                                                                <th>
                                                                    Amount of Expenditure Incurred
                                                                </th>
                                                                <th>
                                                                    Attachment
                                                                </th>
                                                                <th>
                                                                    Action
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox44" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox46" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox47" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div9">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="Text7" class="form-control">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-danger" data-toggle="tooltip"
                                                                        title="Quality Certificate / Registration Certificate issued by the competent authority"
                                                                        runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                                </td>
                                                                  <td>
                                                                    <asp:TextBox ID="TextBox25" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div10">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="Text1" class="form-control">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton14" CssClass="btn btn-danger" data-toggle="tooltip"
                                                                        title="Quality Certificate / Registration Certificate issued by the competent authority"
                                                                        runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox49" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton37" CssClass="btn btn-danger" data-toggle="tooltip"
                                                                        title="Statement on expenditure incurred for obtaining Quality certification / its renewal
                                                        supported with copies of the bills / vouchers / receipt etc." runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton32" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                               
                                                                <td colspan="8" class="text-right">
                                                                   <strong> Total</strong>
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="TextBox48" runat="server" CssClass="form-control"></asp:TextBox>
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
                                    <div class="panel-heading" role="tab" id="Div1">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#AvailedClaimDetails2" aria-expanded="false" aria-controls="collapseThree">
                                                <i class="more-less fa  fa-plus"></i>Availed Details</a>
                                        </h4>
                                    </div>
                                    <div id="AvailedClaimDetails2" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                          
                                                <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12">
                                                       Assistance details for Quality Certificate incentives already availed by this enterprise
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
                                                                    <asp:TextBox ID="TextBox28" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton19" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>

                                                <div class="form-group">
            <div class="row">
                <label for="Iname" class="col-sm-5 ">
                   <asp:RadioButton ID="RadioButton2" runat="server" />  Mark if incentive for Quality Certificate was never availed prior to this
                </label>
                
                <div class="col-sm-3 not-active2">
                    <small class="text-gray">Undertaking on non-availment of incentive earlier on this project</small>
                </div>
                <div class="col-sm-4 not-active2">
                    <span class="colon">:</span>
                   <asp:LinkButton ID="LinkButton4" data-toggle="tooltip" title="View file" CssClass="btn btn-success " runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton39" CssClass="btn btn-warning" data-toggle="tooltip" title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton40" CssClass="btn btn-danger" data-toggle="tooltip" title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
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
                                                        Details of Incentive already availed for the Quality Certificate being applied for now  
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
                                                                    <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox16" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox22" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton41" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
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
                                                          <asp:LinkButton ID="LinkButton5" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton9" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton13" CssClass="btn btn-danger" data-toggle="tooltip"
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
                                                        <asp:TextBox ID="TextBox23" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                        <asp:TextBox ID="TextBox24" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                         
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div11">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#AddDocs" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                                </i>Additional Documents</a>
                                        </h4>
                                    </div>
                                    <div id="AddDocs" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                         <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3 ">
                                                        Check if you have obtained the following Statutory Clearences</label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                         <label><input type="checkbox" value="">OSPCB-NOC</label><br />
                                                          <label><input type="checkbox" value="">OSPCB-Consent to Operate</label>
                                                           <label><input type="checkbox" value="">Central Excise-Clearence</label>
                                                            <label><input type="checkbox" value="">Odisha FSHGSCD-Clearence</label>
                                                             <label><input type="checkbox" value="">Expolsive Control-NOC</label>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4">
                                                        <small class="text-gray">Document in support of delay in implementation condoned by
                                                            Empowered Committee</small>
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton42" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton43" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton44" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4">
                                                        <small class="text-gray">Valid statutory clearances including consent to operate issued
                                                            by OSPCB</small>
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton7" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton45" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton46" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
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
    </div></div>
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
