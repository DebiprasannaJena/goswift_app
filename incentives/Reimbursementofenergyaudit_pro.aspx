
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reimbursementofenergyaudit_pro.aspx.cs"
    Inherits="incentives_Reimbursementofenergyaudit_pro" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
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

            $(".ddlImportedIndigenous").change(function () {

                var valueselect = $(this).val();
                if (valueselect == "Imported") {
                    $('.hidelist').show();
                }
                else { $('.hidelist').hide(); }

            });

            $('.documupload').hide();
            $(".uploaddoc").on("click", function () {
                if ($("input:checked").val() == 'supportDoc') {
                    $('.documupload').show();
                }
                else {
                    $('.documupload').hide();
                }
            });

            $('.regdocument').hide();
            $(".reguploadbtn").on("click", function () {
                if ($("input:checked").val() == 'regdoc') {
                    $('.regdocument').hide();
                }
                else {

                    $('.regdocument').show();
                }
            });

        });

        $(function () {
            $("input[name='rbtsubsidy']").click(function () {
                if ($("#rbtYes").is(":checked")) {
                    $(".subsidytable").show();
                } else {
                    $(".subsidytable").hide();
                }
            });
            $("input[name='rbtsubsidy']").click(function () {
                if ($("#rbtNo").is(":checked")) {
                    $(".subsidycontent").show();
                } else {
                    $(".subsidycontent").hide();
                }
            });
        });			
		
    </script>
    <style>
        .not-active, .not-active2
        {
            pointer-events: none;
            cursor: default;
        }
        .lablespan
        {
            font-size: 14px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="registration-div investors-bg">
            <div id="exTab1"> 
                <div class="investrs-tab">
                <uc4:investoemenu ID="ineste" runat="server" />
                    <%--<ul class="nav nav-pills">
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
                    </ul>--%>
                </div>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="1a">
                        <div class="form-sec">
                            <div class="innertabs">
                                <ul class="nav nav-pills pull-right">
                                     <li ><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                                                <li><a href="Basic_Details.aspx">Apply For incentive</a></li>
                                                <li><a href="ViewApplicationStatus.aspx">View Application Status</a></li>
                                </ul>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-header">
                                <div class="iconsdiv">
                                    <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn"
                                        style="margin-top: 0;"><i class="fa fa-print"></i></a><a href="javascript:void(0);"
                                            title="Export to Excel" id="A1" class="pull-right printbtn bg-blue border-blue"
                                            style="margin-top: 0;"><i class="fa fa-file-excel-o"></i></a>
                                </div>
                                <h2>
                                    Application For One Time Reimbursement Of Energy Audit Cost Under IPR 2015</h2>
                            </div>
                            <div class="form-body">
                                <div class="pull-left">
                                    <i>All Amouts to be Entered in <span class="text-red">INR( in Lakhs )</span></i></div>
                                <div class="pull-right">
                                    <span class="text-red">( * )</span><i> All fields in this section are mandatory</i></div>
                                <div class="clearfix">
                                </div>
                                <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                    <div class="panel panel-default">
                                        <div class="panel-heading" role="tab" id="headingOne">
                                            <h4 class="panel-title">
                                                <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"
                                                    aria-expanded="true" aria-controls="collapseOne"><i class="more-less fa  fa-minus">
                                                    </i>Industrial Unit's Details </a>
                                            </h4>
                                        </div>
                                        <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3 col-sm-offset-1">
                                                            Name of Enterprise/Industrial Unit</label>
                                                        <div class="col-sm-7">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="TextBox2" CssClass="form-control" Text="JRD Farma &amp; Research"
                                                                ReadOnly="true" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3 col-sm-offset-1">
                                                            Organization Type</label>
                                                        <div class="col-sm-7 margin-bottom10">
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
                                                        <label for="Iname" class="col-sm-3 col-sm-offset-1">
                                                            Name of Applicant</label>
                                                        <div class="col-sm-1" style="padding-right: 0px">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="DropDownList1" runat="server">
                                                                <asp:ListItem>Mr.</asp:ListItem>
                                                                <asp:ListItem>Ms.</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3 col-sm-offset-1">
                                                            Application By</label>
                                                        <div class="col-sm-7 margin-bottom10">
                                                            <span class="colon">:</span>
                                                            <label class="radio-inline">
                                                                <input type="radio" class="applyby" value="Self" name="radioapply">
                                                                Self
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" class="applyby" value="Authorized Person" name="radioapply">
                                                                Authorized Person
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group adhardetails">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3 col-sm-offset-1">
                                                            Aadhar No.</label>
                                                        <div class="col-sm-7 margin-bottom10">
                                                            <span class="colon">:</span>
                                                            <div class="col-sm-4 padding-right-0 padding-left-0">
                                                                <asp:TextBox ID="TextBox1" CssClass="form-control" placeholder="1234" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4 padding-right-0">
                                                                <asp:TextBox ID="TextBox100" CssClass="form-control" placeholder="1234" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4 padding-right-0">
                                                                <asp:TextBox ID="TextBox30" CssClass="form-control" placeholder="1234" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="clerfix">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group attorneysec">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5 col-sm-offset-1">
                                                            Please provide Authorizeing latter such as Power of attorney/ Board Resolution/Society
                                                            Resolution/ signed by Authorized Signatory</label>
                                                        <div class="col-sm-5">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload4" CssClass="form-control" runat="server" />
                                                                <asp:LinkButton ID="LinkButton10" runat="server" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton11" data-toggle="tooltip" title="Update File" CssClass="input-group-addon bg-blue"
                                                                    runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton12" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-red"
                                                                    runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>
                                                        </div>
                                                        <%--<div class="col-sm-5">
                                                            <span class="colon">:</span>
                                                            <asp:LinkButton ID="LinkButton10" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                                runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton11" CssClass="btn btn-warning" data-toggle="tooltip"
                                                                title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton12" CssClass="btn btn-danger" data-toggle="tooltip"
                                                                title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                        </div>--%>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3 col-sm-offset-1">
                                                            Address of Industrial Unit</label>
                                                        <div class="col-sm-7">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="TextBox34" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3 col-sm-offset-1">
                                                            Unit Category</label>
                                                        <div class="col-sm-7 margin-bottom10">
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
                                                        <label for="Iname" class="col-sm-3 col-sm-offset-1">
                                                            Unit Type</label>
                                                        <div class="col-sm-7">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList ID="DropDownList7" CssClass="form-control" runat="server">
                                                                <asp:ListItem>-- Select --</asp:ListItem>
                                                                <asp:ListItem>New</asp:ListItem>
                                                                <asp:ListItem>Existing Unit Undertaking Expansion</asp:ListItem>
                                                                <asp:ListItem>Existing Unit Undertaking Diversification</asp:ListItem>
                                                                <asp:ListItem>Existing Unit Undertaking Modernization</asp:ListItem>
                                                                <asp:ListItem>Migrated Unit Treated As New</asp:ListItem>
                                                                <asp:ListItem>Rehabilitated Sick Unit Treated As New</asp:ListItem>
                                                                <asp:ListItem>Transferred unit under Section 29 of SFC Act 1951/SARFAESI Act 2002</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-sm-10 col-sm-offset-1">
                                                        <label class="radio-inline">
                                                            <input type="radio" class="uploaddoc" checked="checked" name="rbtDoc">
                                                            Document(s) in support of rehabilitated sick industrial unit treated at par with
                                                            new industrial unit and duly recommended by State Level lnter lnstitutional Committee
                                                            (SLllC) for this incentive
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-10 col-sm-offset-1">
                                                        <label class="radio-inline">
                                                            <input type="radio" class="uploaddoc" value="supportDoc" name="rbtDoc">
                                                            Documeht(s) in support of lndustrial unit seized under Section 29 of the State Financial
                                                            Corporation Act,1951
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-10 col-sm-offset-1">
                                                        <label class="radio-inline">
                                                            <input type="radio" class="uploaddoc" value="" name="rbtDoc">
                                                            SARFAESI Ac|,2002 and thereafter sold to a new entrepreneur on sale of assets basis
                                                            and treated as new industrial unit forthe purpose of this IPR
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="form-group documupload">
                                                    <div class="row">
                                                        <div class="col-sm-4 col-sm-offset-7">
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                                                                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton4" data-toggle="tooltip" title="Update File" CssClass="input-group-addon bg-blue"
                                                                    runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton7" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-red"
                                                                    runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            Whether in Priority Sector</label>
                                                        <div class="col-sm-6 margin-bottom10">
                                                            <span class="colon">:</span>
                                                            <label class="radio-inline">
                                                                <input type="radio" class="optradioPriority" value="Yes" name="optradioPriority">
                                                                Yes
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" class="optradioPriority" value="No" name="optradioPriority">
                                                                No
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
                                                                <input type="radio" class="optradioPriority" name="optradioPioneer">
                                                                Yes
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="optradioPioneer" />
                                                                No
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
                                                            <asp:LinkButton ID="LinkButton67" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                                runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton155" CssClass="btn btn-warning" data-toggle="tooltip"
                                                                title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton6" CssClass="btn btn-danger" data-toggle="tooltip"
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
                                                            <label>
                                                                <input type="checkbox" />
                                                                Same as Address of Industrial Unit</label>
                                                            <asp:TextBox ID="TextBox3000" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            Name of Managing Partner</label>
                                                        <div class="col-sm-1" style="padding-right: 0px">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="DropDownList6" runat="server">
                                                                <asp:ListItem>Mr.</asp:ListItem>
                                                                <asp:ListItem>Ms.</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-5">
                                                            <asp:TextBox ID="TextBox43" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-sm-10 col-sm-offset-1">
                                                        <label class="radio-inline">
                                                            <input type="radio" class="reguploadbtn" value="" checked="checked" name="regcertificate">
                                                            Certificate of registration under lndian Partnership Act1932
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-10 col-sm-offset-1">
                                                        <label class="radio-inline">
                                                            <input type="radio" class="reguploadbtn" value="regdoc" name="regcertificate">
                                                            Societies Registration Act- 1860
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-10 col-sm-offset-1">
                                                        <label class="radio-inline">
                                                            <input type="radio" class="reguploadbtn" value="" name="regcertificate">
                                                            Certificate of incorporation (Memorandum of association & Article of Association
                                                            ) under Company Act1956
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="form-group row regdocument">
                                                    <div class="col-sm-4 col-sm-offset-7">
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="FileUpload2" CssClass="form-control" runat="server" />
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton2" data-toggle="tooltip" title="Update File" CssClass="input-group-addon bg-blue"
                                                                runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton13" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-red"
                                                                runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        </div>
                                                        <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>
                                                    </div>
                                                </div>
                                                <%-- <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:LinkButton ID="LinkButton13" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton14" CssClass="btn btn-warning" data-toggle="tooltip"
                                                            title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton15" CssClass="btn btn-danger" data-toggle="tooltip"
                                                            title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>--%>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            EIN/ IEM/ IL No.</label>
                                                        <div class="col-sm-6 margin-bottom10">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="TextBox6" CssClass="form-control" ReadOnly="true" Text="1234-5678-1234"
                                                                runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            Date of EIN/ IEM/ IL Date</label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="TextBox4" CssClass="form-control" ReadOnly="true" Text="25-07-1990"
                                                                runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            PC No.</label>
                                                        <div class="col-sm-6 margin-bottom10">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="TextBox4asd" CssClass="form-control" Text="1234-5678-1234" ReadOnly="true"
                                                                runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            PC Issuance Date</label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="TextBox6ddsf" CssClass="form-control" Text="25-07-1990" ReadOnly="true"
                                                                runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1 ">
                                                            Date of Commencement of Production <span class="text-red">*</span></label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group  date datePicker" id="Div3">
                                                                <input name="txtTimescheduleforyearofcomm" type="text" id="Text5" class="form-control">
                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            <small class="text-gray">Certificate on Date of Commencement of production</small><span
                                                                class="text-red">*</span>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload3" CssClass="form-control" runat="server" />
                                                                <asp:LinkButton ID="LinkButton14" runat="server" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton15" data-toggle="tooltip" title="Update File" CssClass="input-group-addon bg-blue"
                                                                    runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton20" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-red"
                                                                    runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>
                                                        </div>
                                                        <%--<div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:LinkButton ID="LinkButton5sdf" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                                runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton6dfs" CssClass="btn btn-warning" data-toggle="tooltip"
                                                                title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton7sdf" CssClass="btn btn-danger" data-toggle="tooltip"
                                                                title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                        </div>--%>
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
                                                                        Interest Amount Original
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
                                                                        <asp:Label ID="Label1" runat="server" Text="Land"></asp:Label>
                                                                        <%--<asp:DropDownList ID="DropDownList10" CssClass="form-control" runat="server">
                                                                            <asp:ListItem>Land type</asp:ListItem>
                                                                            <asp:ListItem>Own ancestoral land </asp:ListItem>
                                                                            <asp:ListItem>Own land(purchased)</asp:ListItem>
                                                                            <asp:ListItem>Private land leased </asp:ListItem>
                                                                            <asp:ListItem>Govt. land leased </asp:ListItem>
                                                                            <asp:ListItem> IDCO land </asp:ListItem>
                                                                            <asp:ListItem>IDCO Shed</asp:ListItem>
                                                                        </asp:DropDownList>--%>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <input type="text" class="form-control text-right" value="58.9" />
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <input type="text" class="form-control text-right" value="48.9" />
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
                                                                        <input type="text" class="form-control text-right" value="58.9" />
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <input type="text" class="form-control text-right" value="48.9" />
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
                                                                        <input type="text" class="form-control text-right" value="58.9" />
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <input type="text" class="form-control text-right" value="48.9" />
                                                                    </td>
                                                                    <tr>
                                                                        <td>
                                                                            4
                                                                        </td>
                                                                        <td>
                                                                            Balancing Equipment
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <input type="text" class="form-control text-right" value="58.9" />
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <input type="text" class="form-control text-right" value="48.9" />
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
                                                                            <input type="text" class="form-control text-right" value="58.9" />
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <input type="text" class="form-control text-right" value="48.9" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <strong>Total</strong>
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <strong>365.7</strong>
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <strong>265.7</strong>
                                                                        </td>
                                                                    </tr>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <h4 class="h4-header">
                                                    MEANS OF FINANCE
                                                </h4>
                                                <div class="form-group row">
                                                    <label class="col-sm-2">
                                                        Equity</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="TextBox29" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <a href="#" data-toggle="tooltip" class="fieldinfo" title="Self Financed"><i class="fa fa-question-circle"
                                                            aria-hidden="true"></i></a>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-2">
                                                        Loan from Bank/FI</label>
                                                    <label class="col-sm-4">
                                                        <span class="colon">:</span> <span class="lablespan">Total Amount (Excluding Loan for
                                                            Working Capital)</span></label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox CssClass="form-control" runat="server"></asp:TextBox>
                                                        <a href="#" data-toggle="tooltip" class="fieldinfo" title="Sample text explaining what is this field">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-12 ">
                                                            <strong>Term Loan Details</strong></label>
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
                                                                    <th>
                                                                        State
                                                                    </th>
                                                                    <th>
                                                                        City
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
                                                                        <asp:TextBox ID="TextBox31" CssClass="form-control" runat="server"></asp:TextBox>
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
                                                        <label for="Iname" class="col-sm-12 ">
                                                            <strong>Working Capital Loan Table</strong></label>
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
                                                                    <th>
                                                                        State
                                                                    </th>
                                                                    <th>
                                                                        City
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        1
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <div class="input-group  date datePicker" id="Div7">
                                                                            <input name="txtTimescheduleforyearofcomm" type="text" class="form-control">
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <div class="input-group  date datePicker" id="Div6">
                                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="Text9" class="form-control">
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="Iname" class="col-sm-4 ">
                                                        Term loan sanction order of Financial lnstitute (s) / Banks</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <div class="input-group">
                                                            <asp:FileUpload CssClass="form-control" runat="server" />
                                                            <asp:LinkButton runat="server" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton data-toggle="tooltip" title="Update File" CssClass="input-group-addon bg-blue"
                                                                runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                            <asp:LinkButton data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-red"
                                                                runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                        </div>
                                                        <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-4 ">
                                                        FDI Component
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox CssClass="form-control" runat="server"></asp:TextBox>
                                                        <a href="#" data-toggle="tooltip" class="fieldinfo" title="Sample text explaining what is this field">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading" role="tab" id="Div4">
                                            <h4 class="panel-title">
                                                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                    href="#ContractDemand " aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                                    </i>Contract Demand / Connected load Details </a>
                                            </h4>
                                        </div>
                                        <div id="ContractDemand" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <p class="text-red text-right">
                                                    All Amouts to be Entered in INR(Exact Amount)</p>
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
                                                                        Contract Demand / Connected
                                                                    </th>
                                                                    <th>
                                                                        Consumer No.
                                                                    </th>
                                                                    <th width="90">
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
                                                            <small class="text-gray">Demand / connected load</small></label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload5" CssClass="form-control" runat="server" />
                                                                <asp:LinkButton ID="LinkButton8" runat="server" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton9" data-toggle="tooltip" title="Update File" CssClass="input-group-addon bg-blue"
                                                                    runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton16" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-red"
                                                                    runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>
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
                                                    href="#EnergyAuditorwithDetails " aria-expanded="false" aria-controls="collapseThree">
                                                    <i class="more-less fa  fa-plus"></i>Energy Audit Details </a>
                                            </h4>
                                        </div>
                                        <div id="EnergyAuditorwithDetails" class="panel-collapse collapse" role="tabpanel"
                                            aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-12 ">
                                                            Energy Audit Details</label>
                                                        <div class="col-sm-12">
                                                            <table class="table table-bordered">
                                                                <tr>
                                                                    <th colspan="2">
                                                                        Name of Energy Auditor / Organization
                                                                    </th>
                                                                    <th>
                                                                        Address of Energy Auditor / Organization
                                                                    </th>
                                                                    <th colspan="2">
                                                                        Accreditation of the Auditor
                                                                    </th>
                                                                    <th colspan="2">
                                                                        Expenditure incurred
                                                                    </th>
                                                                    <th>
                                                                        Date of completion of successful implementation Energy Audit
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox110" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="LinkButton31" CssClass="btn btn-danger" data-toggle="tooltip"
                                                                            title="Document (s) on engagement of Energy Auditor" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox11" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox23" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="LinkButton32" CssClass="btn btn-danger" data-toggle="tooltip"
                                                                            title="Accreditation of Energy Auditor with Details
" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox25" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="LinkButton33" CssClass="btn btn-danger" data-toggle="tooltip"
                                                                            title="Statement on expenditure incurred for Energy Audit with copy of the bills / vouchers / receipt etc.
" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                                    </td>
                                                                    <td>
                                                                        <div class="input-group  date datePicker" id="Div9">
                                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="Text8" class="form-control">
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Document in support of implementation of Energy Audit Report
                                                        </label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload6" CssClass="form-control" runat="server" />
                                                                <asp:LinkButton ID="LinkButton17" runat="server" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton18" data-toggle="tooltip" title="Update File" CssClass="input-group-addon bg-blue"
                                                                    runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton19" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-red"
                                                                    runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Date of completion of successful implementation Energy Audit</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload7" CssClass="form-control" runat="server" />
                                                                <asp:LinkButton ID="LinkButton21" runat="server" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton22" data-toggle="tooltip" title="Update File" CssClass="input-group-addon bg-blue"
                                                                    runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton23" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-red"
                                                                    runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-12 ">
                                                            <strong>Energy Consumption ( KWH)</strong></label>
                                                        <div class="col-sm-12">
                                                            <table class="table table-bordered">
                                                                <tr>
                                                                    <th>
                                                                        Before Audit
                                                                    </th>
                                                                    <th>
                                                                        After Audit
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox5" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox7" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Document(s) / proof on reduction of Energy expenses.
                                                        </label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload8" CssClass="form-control" runat="server" />
                                                                <asp:LinkButton ID="LinkButton24" runat="server" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton25" data-toggle="tooltip" title="Update File" CssClass="input-group-addon bg-blue"
                                                                    runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton26" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-red"
                                                                    runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Certificate on energy efficiency and reduction of carbon footprint
                                                                by independent and credible third part agency</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload9" CssClass="form-control" runat="server" />
                                                                <asp:LinkButton ID="LinkButton28" runat="server" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton29" data-toggle="tooltip" title="Update File" CssClass="input-group-addon bg-blue"
                                                                    runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton30" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-red"
                                                                    runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>
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
                                                    <i class="more-less fa  fa-plus"></i>Details of Incentives Availed Earlier</a>
                                            </h4>
                                        </div>
                                        <div id="AvailedClaimDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <div class="form-group row">
                                                    <label class="col-sm-6">
                                                        Has Subsidy/Incentive against the details in this application been availed earlier</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <label class="radio-inline" for="rbtYes">
                                                            <input type="radio" id="rbtYes" name="rbtsubsidy" />
                                                            Yes
                                                        </label>
                                                        <label class="radio-inline" for="rbtNo">
                                                            <input type="radio" id="rbtNo" name="rbtsubsidy" />
                                                            No
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="form-group subsidytable" style="display: none;">
                                                    <div class="row">
                                                        <div class="col-sm-12  margin-bottom10">
                                                            <table class="table table-bordered">
                                                                <tr>
                                                                    <th>
                                                                        Sl#
                                                                    </th>
                                                                    <th>
                                                                        Disbursing Agency
                                                                    </th>
                                                                    <th>
                                                                        Sanctioned Amount
                                                                    </th>
                                                                    <th>
                                                                        Sanction Order no.
                                                                    </th>
                                                                    <th>
                                                                        Availed Date
                                                                    </th>
                                                                    <th>
                                                                        Availed Amount
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
                                                                        <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <div class="input-group  date datePicker">
                                                                            <input type="text" class="form-control">
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="LinkButton5" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group subsidycontent" style="display: none;">
                                                    <div class="row">
                                                        <div class="col-sm-3">
                                                            Upload the undertaking</div>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload CssClass="form-control" runat="server" />
                                                                <asp:LinkButton runat="server" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton data-toggle="tooltip" title="Update File" CssClass="input-group-addon bg-blue"
                                                                    runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                                <asp:LinkButton data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-red"
                                                                    runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3">
                                                            Present Claim Amount</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <a href="#" data-toggle="tooltip" class="fieldinfo" title="Sample text explaining what is this field">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3">
                                                            Differential Amount</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="TextBox28" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <a href="#" data-toggle="tooltip" class="fieldinfo" title="Sample text explaining what is this field">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
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
                                                    href="#AdditionalDocuments" aria-expanded="false" aria-controls="collapseThree">
                                                    <i class="more-less fa  fa-plus"></i>Additional Documents</a>
                                            </h4>
                                        </div>
                                        <div id="AdditionalDocuments" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <div class="form-group not-active">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3">
                                                            Check if you have obtained the following Statutory Clearences
                                                        </label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                                                                <asp:ListItem>OSPCB-NOC</asp:ListItem>
                                                                <asp:ListItem>OSPCB-Consent to Operate</asp:ListItem>
                                                                <asp:ListItem>Central Excise-Clearence</asp:ListItem>
                                                                <asp:ListItem>Odisha FSHGSCD-Clearence</asp:ListItem>
                                                                <asp:ListItem>Expolsive Control-NOC</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3">
                                                            Document in support of delay in implementation condoned by
                                                                Empowered Committee</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload10" CssClass="form-control" runat="server" />
                                                                <asp:LinkButton ID="LinkButton34" runat="server" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton35" data-toggle="tooltip" title="Update File" CssClass="input-group-addon bg-blue"
                                                                    runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton36" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-red"
                                                                    runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3">
                                                            Statutory clearances including consent to operate issued by OSPCB</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload11" CssClass="form-control" runat="server" />
                                                                <asp:LinkButton ID="LinkButton37" runat="server" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton39" data-toggle="tooltip" title="Update File" CssClass="input-group-addon bg-blue"
                                                                    runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton40" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-red"
                                                                    runat="server"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>
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
                                        <asp:Button ID="Button1" runat="server" Text="Save as Draft" CssClass="btn btn-warning" />
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