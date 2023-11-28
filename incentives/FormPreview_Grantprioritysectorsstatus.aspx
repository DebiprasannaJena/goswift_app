<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormPreview_Grantprioritysectorsstatus.aspx.cs"
    Inherits="incentives_FormPreview_Grantprioritysectorsstatus" %>

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
    <script type="text/javascript" language="javascript">

         function alertredirect(msg) {
            jAlert(msg, msgTitle, function (r) {
                if (r) {
                    location.href = 'IncentiveFeedback.aspx?InctUniqueNo='+<%= Request.QueryString["InctUniqueNo"] %> + '&ServiceId=502';
                    return true;
                }
                else {
                    return false;
                }
            });
        }       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div id="divHeader1">
            <uc2:header ID="header" runat="server" />
        </div>
        <div class="registration-div investors-bg">
            <div id="exTab1" class="">
                <div class="investrs-tab">
                    <uc5:pealmenu ID="Peal" runat="server" />
                </div>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="1a">
                        <div class="form-sec">
                            <div class="innertabs m-b-10">
                                <ul class="nav nav-pills pull-right">
                                    <li><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                                    <li class="active"><a href="Basic_Details.aspx">Apply For incentive</a></li>
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
                                        <h4>
                                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                        </h4>
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
                                                Dist.(Location of the lndustrial Unit) :<asp:Label ID="Label5" runat="server" Text="Khurda"></asp:Label></p>
                                        </div>
                                        <h4>
                                            To</h4>
                                        <div class="padding-left-20">
                                            <p>
                                                The General Manager, Regional lndustries Centre / District lndustries Centre
                                                <asp:Label ID="lblGM" runat="server"></asp:Label></p>
                                        </div>
                                        <h4>
                                            Sub: - Application for Grant of Provisional Priority Sector status under Industrial
                                            Policy Resolution 2015.
                                        </h4>
                                        <h4>
                                            Sir,</h4>
                                        <div class="padding-left-20">
                                            <p>
                                                In accordance with the provisions laid down in Industrials Policy Resolution 2015
                                                and its operational guidelines, I do hereby submit the application for grant of
                                                Provisional Priority Sector status.
                                            </p>
                                        </div>
                                    </div>
                                    <div class="prievewdynamicdata">
                                        <div class="panel-group padding-20" id="accordion" role="tablist" aria-multiselectable="true">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="headingOne">
                                                    <h4 class="panel-title">
                                                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"
                                                            aria-expanded="true" aria-controls="collapseOne">Industrial Unit's Details </a>
                                                    </h4>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Name of Enterprise/Industrial Unit</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span><asp:Label ID="lbl_EnterPrise_Name" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Organization Type</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_Org_Type" runat="server" CssClass="form-control-static"></asp:Label>
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
                                                    <div class="form-group " id="divadhhardetails" runat="server">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Aadhaar No.</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <div class="col-sm-4 padding-right-0 padding-left-0">
                                                                    <asp:Label ID="TxtAdhaar1" runat="server" CssClass="form-control-static"></asp:Label></div>
                                                                <div class="col-sm-4 padding-right-0">
                                                                    <asp:Label ID="TxtAdhaar2" runat="server" CssClass="form-control-static"></asp:Label></div>
                                                                <div class="col-sm-4 padding-right-0">
                                                                    <asp:Label ID="TxtAdhaar3" runat="server" CssClass="form-control-static"></asp:Label></div>
                                                                <div class="clerfix">
                                                                </div>
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
                                                                <asp:Label ID="lbl_Unit_Cat" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Unit Type</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_Unit_Type" runat="server" CssClass="form-control-static"></asp:Label>
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
                                                            <div class="col-sm-6" style="padding-right: 0px">
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
                                                    <div style="display: none;">
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
                                                                    <div class="col-sm-6">
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
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                District</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_District" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                Sector</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_Sector" runat="server" CssClass="form-control-static"></asp:Label>
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
                                                                <asp:Label ID="lbl_Sub_Sector" runat="server" CssClass="form-control-static"></asp:Label>
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
                                                            <div class="col-sm-4">
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
                                                <div class="panel-heading" role="tab" id="Div3">
                                                    <h4 class="panel-title">
                                                        <a>Production & Employment Details </a>
                                                    </h4>
                                                </div>
                                                <div id="ProductionEmploymentDetails12" class="panel-collapse collapse in" role="tabpanel"
                                                    aria-labelledby="headingThree">
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="panel-body">
                                                                <p class="text-red text-right">
                                                                    All Amounts Entered in INR(In Lakhs)</p>
                                                                <div runat="server" id="divbefor1">
                                                                    <h4>
                                                                        <asp:Label runat="server" ID="lblemdBefore" Text="Before E/M/D" CssClass="h2-hdr"></asp:Label>
                                                                    </h4>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-12">
                                                                                Items of Manufacture/Activity
                                                                            </label>
                                                                            <div class="col-sm-12  margin-bottom10">
                                                                                <asp:GridView ID="Grd_Production_Before" runat="server" CssClass="table table-bordered"
                                                                                    DataKeyNames="vchProductName" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Slno.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_Sl_No_Product_Before" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="5%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Product/Service Name">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_Product_Name_Before" runat="server" Text='<%# Eval("vchProductName") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="35%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Quantity">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_Quantity_Before" runat="server" Text='<%# Eval("intQuantity") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="20%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Units">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_Unit_Before" runat="server" Text='<%# Eval("MeasunitName") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="10%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Other Units">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_Other_Unit_Before" runat="server" Text='<%# Eval("vchotherunit") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="10%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Value">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_Value_Before" runat="server" Text='<%# Eval("decValue") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="20%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                                Direct Employment In Numbers<small> (On Company Payroll)</small>
                                                                            </label>
                                                                            <div class="col-sm-2">
                                                                                <span class="colon">:</span>
                                                                                <asp:Label ID="lbl_Direct_Emp_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                                            </div>
                                                                            <label for="Iname" class="col-sm-4">
                                                                                Contractual Employment In Numbers</label>
                                                                            <div class="col-sm-2">
                                                                                <span class="colon">:</span>
                                                                                <asp:Label ID="lbl_Contract_Emp_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <table class="table table-bordered" id="tblEmployement" runat="server">
                                                                                <tr>
                                                                                    <td style="width: 20%;">
                                                                                        Managerial
                                                                                    </td>
                                                                                    <td style="width: 15%;">
                                                                                        <asp:Label ID="lbl_Managarial_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 20%;">
                                                                                        General
                                                                                    </td>
                                                                                    <td style="width: 15%;">
                                                                                        <asp:Label ID="lbl_General_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Supervisor
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Supervisor_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        SC
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_SC_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Skilled
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Skilled_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        ST
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_ST_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Semi Skilled
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Semi_Skilled_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        Total
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Total_Cast_Emp_Before" ReadOnly="true" runat="server" Text="0"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Un Skilled
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Unskilled_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        Women
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Women_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Total
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Total_Emp_Before" ReadOnly="true" runat="server" Text="0"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        Differently Abled Persons
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_PHD_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div runat="server" id="divafter1">
                                                                    <h4>
                                                                        <asp:Label runat="server" ID="lblemd" Text="After E/M/D" CssClass="h2-hdr"></asp:Label>
                                                                    </h4>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-12">
                                                                                Items of Manufacture/Activity
                                                                            </label>
                                                                            <div class="col-sm-12  margin-bottom10">
                                                                                <asp:GridView ID="Grd_Production_After" runat="server" CssClass="table table-bordered"
                                                                                    DataKeyNames="vchProductName" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Slno.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_Sl_No_Product_After" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="5%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Product/Service Name">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_Product_Name_After" runat="server" Text='<%# Eval("vchProductName") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="35%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Quantity">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_Quantity_After" runat="server" Text='<%# Eval("intQuantity") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="20%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Units">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_Unit_After" runat="server" Text='<%# Eval("MeasunitName") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="10%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Other Units">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_other_Unit_After" runat="server" Text='<%# Eval("vchotherunit") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="10%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Value">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_Value_After" runat="server" Text='<%# Eval("decValue") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="20%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                                Direct Employment In Numbers<small> (On Company Payroll)</small>
                                                                            </label>
                                                                            <div class="col-sm-2">
                                                                                <span class="colon">:</span>
                                                                                <asp:Label ID="lbl_Direct_Emp_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                                            </div>
                                                                            <label for="Iname" class="col-sm-4">
                                                                                Contractual Employment In Numbers</label>
                                                                            <div class="col-sm-2">
                                                                                <span class="colon">:</span>
                                                                                <asp:Label ID="lbl_Contract_Emp_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <table class="table table-bordered" id="Table1" runat="server">
                                                                                <tr>
                                                                                    <td style="width: 20%;">
                                                                                        Managerial
                                                                                    </td>
                                                                                    <td style="width: 15%;">
                                                                                        <asp:Label ID="lbl_Managarial_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 20%;">
                                                                                        General
                                                                                    </td>
                                                                                    <td style="width: 15%;">
                                                                                        <asp:Label ID="lbl_General_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Supervisor
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Supervisor_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        SC
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_SC_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Skilled
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Skilled_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        ST
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_ST_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Semi Skilled
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Semi_Skilled_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        Total
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Total_Cast_Emp_After" ReadOnly="true" runat="server" Text="0"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Un Skilled
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Unskilled_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        Women
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Women_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Total
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Total_Emp_After" ReadOnly="true" runat="server" Text="0"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        Differently Abled Persons
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_PHD_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                            MaxLength="4" Text="0"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="headingThree">
                                                    <h4 class="panel-title">
                                                        <a>Investment Details </a>
                                                    </h4>
                                                </div>
                                                <div id="IndustryDetails123" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div runat="server" id="divbefor2">
                                                            <h3>
                                                                <asp:Label class="h2-hdr" runat="server" ID="Before"> Before E/M/D</asp:Label></h3>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label for="Iname" class="col-sm-5">
                                                                        Date of First Fixed Capital Investment (for Land/Building/Plant and Machinery &
                                                                        Balancing Equipment)
                                                                    </label>
                                                                    <div class="col-sm-4">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="Txt_FFCI_Date_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label for="Iname" class="col-sm-12 ">
                                                                        Total Capital Investment</label>
                                                                    <div class="col-sm-12">
                                                                        <table class="table table-bordered">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <th>
                                                                                        Slno.
                                                                                    </th>
                                                                                    <th>
                                                                                        Investment Head
                                                                                    </th>
                                                                                    <th>
                                                                                        Investment Amount
                                                                                    </th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        1
                                                                                    </td>
                                                                                    <td>
                                                                                        Land Including Land Development
                                                                                    </td>
                                                                                    <td class="text-right">
                                                                                        <asp:Label ID="lbl_Land_Before" runat="server" onblur="calculetotal()"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        2
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label runat="server" ID="lblBuilding" Text="Building"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-right">
                                                                                        <asp:Label ID="lbl_Building_Before" runat="server" onblur="calculetotal()"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        3
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label runat="server" ID="lblPlantMachinery" Text="Plant & Machinery"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-right">
                                                                                        <asp:Label ID="lbl_Plant_Mach_Before" runat="server" onblur="calculetotal()"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        4
                                                                                    </td>
                                                                                    <td>
                                                                                        <label>
                                                                                            Other Fixed Assets</label>
                                                                                    </td>
                                                                                    <td class="text-right">
                                                                                        <asp:Label ID="lbl_Other_Fixed_Asset_Before" runat="server" onblur="calculetotal()"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <strong>Total</strong>
                                                                                    </td>
                                                                                    <td class="text-right">
                                                                                        <strong>
                                                                                            <asp:Label ID="lbl_Total_Capital_Before" runat="server"></asp:Label>
                                                                                        </strong>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div runat="server" id="divAfter2">
                                                            <h3>
                                                                <asp:Label runat="server" Text="After E/M/D" ID="lblEMDInvestment" CssClass="h2-hdr"></asp:Label>
                                                            </h3>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label for="Iname" class="col-sm-5">
                                                                        Date of First Fixed Capital Investment (for Land/Building/Plant and Machinery &
                                                                        Balancing Equipment)
                                                                    </label>
                                                                    <div class="col-sm-4">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lbl_FFCI_Date_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label for="Iname" class="col-sm-12 ">
                                                                        Total Capital Investment</label>
                                                                    <div class="col-sm-12">
                                                                        <table class="table table-bordered">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <th>
                                                                                        Slno.
                                                                                    </th>
                                                                                    <th>
                                                                                        Investment Head
                                                                                    </th>
                                                                                    <th>
                                                                                        Investment Amount
                                                                                    </th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        1
                                                                                    </td>
                                                                                    <td>
                                                                                        Land Including Land Development
                                                                                    </td>
                                                                                    <td class="text-right">
                                                                                        <asp:Label ID="lbl_Land_After" runat="server" onblur="calculetotal()"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        2
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label runat="server" ID="Label6" Text="Building"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-right">
                                                                                        <asp:Label ID="lbl_Building_After" runat="server" onblur="calculetotal()"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        3
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label runat="server" ID="Label7" Text="Plant & Machinery"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-right">
                                                                                        <asp:Label ID="lbl_Plant_Mach_After" runat="server" onblur="calculetotal()"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        4
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label runat="server" ID="Label11" Text="Other Fixed Assests"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-right">
                                                                                        <asp:Label ID="lbl_Other_Fixed_Asset_After" runat="server" onblur="calculetotal()"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <strong>Total</strong>
                                                                                    </td>
                                                                                    <td class="text-right">
                                                                                        <strong>
                                                                                            <asp:Label ID="lbl_Total_Capital_After" runat="server"></asp:Label>
                                                                                        </strong>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <h4 class="h4-header">
                                                            Means Of Finance
                                                        </h4>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-2">
                                                                    Equity</label>
                                                                <div class="col-sm-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_Equity_Amt" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <label class="col-sm-2">
                                                                    Loan from Bank/FI</label>
                                                                <div class="col-sm-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_Loan_Bank_FI" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    <br />
                                                                    <small class="text-gray lablespan">Total Amount (Excluding Loan for Working Capital)</small>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12 ">
                                                                    <strong>Term Loan Details</strong></label>
                                                                <div class="col-sm-12">
                                                                    <asp:GridView ID="Grd_TL" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Slno.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_TL_Sl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="4%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Name of Financial Institution">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_TL_Financial_Inst" runat="server" Text='<%# Eval("vchNameOfFinancialInst") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="State">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_TL_State" runat="server" Text='<%# Eval("vchState") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="13%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="City">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_TL_City" runat="server" Text='<%# Eval("vchCity") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="13%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Term Loan Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_TL_Amount" runat="server" Text='<%# Eval("decLoanAmt") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="13%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Sanction Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_TL_Sanction_Date" runat="server" Text='<%# Eval("dtmSanctionDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Availed Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_TL_Avail_Amt" runat="server" Text='<%# Eval("decAvailedAmt") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="13%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Availed Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_TL_Avail_Date" runat="server" Text='<%# Eval("dtmAvailedDate" , "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12 ">
                                                                    <strong>Working Capital Loan Details</strong></label>
                                                                <div class="col-sm-12">
                                                                    <asp:GridView ID="Grd_WC" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Slno.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_WC_Sl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="4%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Name of Financial Institution">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_WC_Financial_Inst" runat="server" Text='<%# Eval("vchNameOfFinancialInst") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="State">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_WC_State" runat="server" Text='<%# Eval("vchState") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="13%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="City">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_WC_City" runat="server" Text='<%# Eval("vchCity") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="13%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Loan Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_WC_Amount" runat="server" Text='<%# Eval("decLoanAmt") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="13%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Sanction Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_WC_Sanction_Date" runat="server" Text='<%# Eval("dtmSanctionDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Availed Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_WC_Avail_Amt" runat="server" Text='<%# Eval("decAvailedAmt") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="13%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Availed Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_WC_Avail_Date" runat="server" Text='<%# Eval("dtmAvailedDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label class="col-sm-4 ">
                                                                FDI Component
                                                            </label>
                                                            <div class="col-sm-4">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_FDI_Componet" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="Div6">
                                                    <h4 class="panel-title">
                                                        <a>DLSWCA / SLSWCA / HLCA Apporval Details</a>
                                                    </h4>
                                                </div>
                                                <div id="DLSWCA" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <div class="col-sm-5">
                                                                    <label for="Iname">
                                                                        Date of Approval
                                                                    </label>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblDLSWCADateOfApproval" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-5">
                                                                    Requirement of land approved by DLSWCA / SLSWCA / HLCA</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblDLSWCALandApproved" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" style="display: none;">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3 ">
                                                                    Cost of land
                                                                </label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblDLSWCALandCost" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" style="display: none;">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3 ">
                                                                    Eligible Amount of subsidy(Details Calculation Sheet to be Enclosed)</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblDLSWCASubsidyAmt" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <%-- <label for="Iname" class="col-sm-3">
                                                            <small class="text-gray">Copy of Approval of DLSWCA / SLSWCA / HLCA</small></label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">--%>
                                                                <%--<asp:FileUpload ID="fupDLSWCAApprovalDocUpload" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:LinkButton ID="lnkAddDLSWCAApprovalDoc" runat="server" CssClass="input-group-addon bg-green"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentPdf_Click" ToolTip="Upload File"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelDLSWCAApprovalDoc" runat="server" CssClass="input-group-addon bg-red"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentDelete_Click" Visible="false" ToolTip="Delete File"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="lnkDLSWCAApprovalDocView" data-toggle="tooltip" title="View file"
                                                                    CssClass="input-group-addon bg-blue" runat="server" OnClientClick="JavaScript: return false;"
                                                                    Target="_blank" Visible="false"><i class="fa fa-cloud-download"></i></asp:HyperLink>
                                                            </div>--%>
                                                                <%--<small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>
                                                            <asp:HiddenField ID="hdnDLSWCAApprovalDoc" runat="server" />
                                                            <asp:HiddenField ID="hdnDLSWCAApprovalDocId" runat="server" Value="D113" />
                                                            <asp:Label ID="lblDLSWCAApprovalDoc" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploded successfully"></asp:Label>
                                                        </div>--%>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <%--      <label for="Iname" class="col-sm-3 ">
                                                            <small class="text-gray">Copy of Documents to substantiate of land cost</small></label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                               <%-- <asp:FileUpload ID="fupDLSWCASubstanDocUpload" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:LinkButton ID="lnkAddDLSWCASubstanDoc" runat="server" CssClass="input-group-addon bg-green"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentPdf_Click" ToolTip="Uploaad File"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelDLSWCASubstanDoc" runat="server" CssClass="input-group-addon bg-red"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentDelete_Click" Visible="false" ToolTip="Delete file"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="lnkDLSWCASubstanDocView" data-toggle="tooltip" title="View file"
                                                                    CssClass="input-group-addon bg-blue" runat="server" OnClientClick="JavaScript: return false;"
                                                                    Target="_blank" Visible="false"><i class="fa fa-cloud-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small> --%>
                                                                <asp:HiddenField ID="hdnDLSWCASubstanDoc" runat="server" />
                                                                <asp:HiddenField ID="hdnDLSWCASubstanDocId" runat="server" Value="D114" />
                                                                <asp:Label ID="lblDLSWCASubstanDoc" Style="font-size: 12px;" CssClass="text-blue"
                                                                    Visible="false" runat="server" Text="Document uploded successfully"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="Div5">
                                                    <h4 class="panel-title">
                                                        <a>Priority Sector Details </a>
                                                    </h4>
                                                </div>
                                                <div id="PrioritySectorDetails" class="panel-collapse in" role="tabpanel" aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div id="removaldiv" style="display: none;">
                                                                <div class="row">
                                                                    <label for="Iname" class="col-sm-3 ">
                                                                        Sector</label>
                                                                    <div class="col-sm-6">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lblSector" CssClass="dataspan" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="Iname" class="col-sm-3 ">
                                                                        Sub Sector</label>
                                                                    <div class="col-sm-6">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lblSubSector" CssClass="dataspan" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="Iname" class="col-sm-3 ">
                                                                        Derive Sector</label>
                                                                    <div class="col-sm-6">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lblDriveSector" CssClass="dataspan" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="Iname" class="col-sm-3 ">
                                                                        Priority sector Certificate Availabe ?</label>
                                                                    <div class="col-sm-6">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lblPriority" CssClass="dataspan" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="Iname" class="col-sm-3 ">
                                                                        Priority Sector Specific Activity</label>
                                                                    <div class="col-sm-6">
                                                                        <span class="colon">:</span>
                                                                        <asp:DropDownList ID="ddlSpecificActivity" CssClass="form-control" runat="server">
                                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">Bio-enzyme</asp:ListItem>
                                                                            <asp:ListItem Value="2">Bio-pesticide</asp:ListItem>
                                                                            <asp:ListItem Value="3">Bio-fertilizer</asp:ListItem>
                                                                            <asp:ListItem Value="4">Bio-fuels</asp:ListItem>
                                                                            <asp:ListItem Value="5">Art Leather</asp:ListItem>
                                                                            <asp:ListItem Value="6">Art Textiles (Tie & Dye)</asp:ListItem>
                                                                            <asp:ListItem Value="7">Artificla I Bonsai</asp:ListItem>
                                                                            <asp:ListItem Value="8">Bio-technology</asp:ListItem>
                                                                            <asp:ListItem Value="9">Artistic Foot-ware</asp:ListItem>
                                                                            <asp:ListItem Value="10">Artistic Mat</asp:ListItem>
                                                                            <asp:ListItem Value="11">Batik Printing</asp:ListItem>
                                                                            <asp:ListItem Value="12">Computing Devices</asp:ListItem>
                                                                            <asp:ListItem Value="13">Computer Mother Boards and Cards</asp:ListItem>
                                                                            <asp:ListItem Value="14">Other Computer peripherals (input/output device)</asp:ListItem>
                                                                            <asp:ListItem Value="15">Other</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3 ">
                                                                    Priority Sector Specific Activity</label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblsectoravilability" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="Div7">
                                                    <h4 class="panel-title">
                                                        <a>Availed Earlier</a>
                                                    </h4>
                                                </div>
                                                <div id="AvailedEarlier" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <asp:GridView ID="grdAvailedEarlier" runat="server" CssClass="table table-bordered"
                                                                    DataKeyNames="intIncentiveTypeID" AutoGenerateColumns="false">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Slno.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="LblSlNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="5%"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Incentive Type ">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="LblIncentiveType" runat="server" Text='<%# Eval("vchIncentiveTypeID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="20%"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Quantum/Value">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Lbl_Quantity" runat="server" Text='<%# Eval("dcmQuantumValue") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="10%"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Period From">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Lbl_PeriodFrom" runat="server" Text='<%# Eval("dtmPeriodFrom") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="20%"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Period To">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Lbl_PeriodTo" runat="server" Text='<%# Eval("dtmPeriodto") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="20%"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="IPR Applicability">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Lbl_PeriodFrom" runat="server" Text='<%# Eval("vchIPRApplicabilityID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="10%"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading" role="tab">
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
                                                                        Please provide Authorizing letter signed by Authorized Signatory
                                                                        <asp:Label ID="Lbl_Org_Doc_Type" runat="server"></asp:Label>
                                                                        <asp:HiddenField ID="hdnIncentiveId" runat="server" />
                                                                        <asp:HiddenField ID="HiddenField1011" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="hypAUTHORIZEDFILE" runat="server" Target="_blank" ToolTip="View File">View Document
                                                                        </asp:HyperLink>
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
                                                                <%--<tr runat="server" id="tr_Prod_Comm_Before" visible="false" >
                                                            <td>
                                                                <asp:Label ID="Lbl_Prod_Comm_Before_Doc_Name" runat="server" Text="Certificate on Date of Commencement of production"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="Hyp_View_Prod_Comm_Before_Doc" runat="server" Target="_blank">View Document</asp:HyperLink>
                                                            </td>
                                                        </tr>--%>
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
                                                                <tr runat="server" id="divEmp_Before_Doc_Name" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="Lbl_Direct_Emp_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="Hyp_View_Direct_Emp_Before_Doc" runat="server" Target="_blank">View Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Div_Unit_Type_Doc" runat="server">
                                                                    <td>
                                                                        <asp:Label ID="Lbl_Unit_Type_Doc_Name" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="Hyp_View_Unit_Type_Doc" runat="server" Target="_blank">View
                                                                Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr id="tr_Direct_Emp_After_Doc_Name" runat="server">
                                                                    <td>
                                                                        <asp:Label ID="Lbl_Direct_Emp_After_Doc_Name" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="Hyp_View_Direct_Emp_After_Doc" runat="server" Target="_blank">View Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="FFCI_Before_Doc_Name" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="Lbl_FFCI_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="Hyp_View_FFCI_Before_Doc" runat="server" Target="_blank">View Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="tr_Approved_DPR_After_Doc_Name">
                                                                    <td>
                                                                        <asp:Label ID="Lbl_Approved_DPR_After_Doc_Name" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="Hyp_View_Approved_DPR_After_Doc" runat="server" Target="_blank"
                                                                            title="Click to View">View Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Approved_DPR_Before_Doc_Name" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="Lbl_Approved_DPR_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="Hyp_View_Approved_DPR_Before_Doc" runat="server" Target="_blank">View Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr id="tr_FFCI_After_Doc_Name" runat="server">
                                                                    <td>
                                                                        <asp:Label ID="Lbl_FFCI_After_Doc_Name" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="Hyp_View_FFCI_After_Doc" runat="server" Target="_blank">View Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr id="tr_Term_Loan_Doc_Name" runat="server">
                                                                    <td>
                                                                        <asp:Label ID="Lbl_Term_Loan_Doc_Name" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="Hyp_View_Term_Loan_Doc" runat="server" Target="_blank">View Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        A brief note on the present stage of implementation
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="HyperLink4" data-toggle="View Document" title="View Document"
                                                                            Target="_blank" OnClientClick="JavaScript: return false;" runat="server">View Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Migrated industrial unit treated as new industrial unit under Priority Sector
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="lnkViewCertificate" data-toggle="View Document" title="View Document"
                                                                            Target="_blank" OnClientClick="JavaScript: return false;" runat="server">View Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Undertaking in context of that Industrial units shall have to go in to production
                                                                        within three vears from the date of starting first fixed capital investment
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="lknViewApplAcknow" data-toggle="View Document" title="View Document"
                                                                            Target="_blank" OnClientClick="JavaScript: return false;" runat="server">View Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Document / certificate in support of Category fall under Priority Sector
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="HyperLink1" data-toggle="View Document" title="View Document"
                                                                            Target="_blank" OnClientClick="JavaScript: return false;" runat="server">View Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Copy of Approval of DLSWCA / SLSWCA / HLCA
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="lnkDLSWCAApprovalDocView" data-toggle="View Document" title="View Document"
                                                                            Target="_blank" OnClientClick="JavaScript: return false;" runat="server">View Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Copy of Documents to substantiate of land cost
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="lnkDLSWCASubstanDocView" data-toggle="View Document" title="View Document"
                                                                            Target="_blank" OnClientClick="JavaScript: return false;" runat="server">View Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Sector Relevant Document
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="lknUpladView" data-toggle="View Document" title="View Document"
                                                                            Target="_blank" OnClientClick="JavaScript: return false;" runat="server">View Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        OSPCB consent to Establishment (except white category)
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="HyperLink2" data-toggle="View Document" title="View Document"
                                                                            Target="_blank" runat="server">View Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Factory & Boiler - For all industry (10 with direct employment / 20 no of employment
                                                                        with power)
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="HyperLink3" data-toggle="View Document" title="View Document"
                                                                            Target="_blank" runat="server">View Document</asp:HyperLink>
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
                                                                I
                                                                <asp:Label ID="lblName" runat="server"></asp:Label>
                                                                <%--at present--%>
                                                                <asp:Label ID="lblPresent" runat="server" Visible="false"></asp:Label>
                                                                of M/S
                                                                <asp:Label ID="lblUnitAddress" runat="server"></asp:Label>
                                                                (Name of the Industrial Unit) certify that the information furnished as above is
                                                                true and correct to the best of my knowledge and belief.</p>
                                                            <p>
                                                                I hereby undertake to surrender the Provisional Certificate of Priority Status /
                                                                Certificate of Priority Sector and refund the incentives availed under Priority
                                                                Sector with penalty as decided by the authority
                                                            </p>
                                                            <p>
                                                                (1) If the information stated above is found to be false / fraud / incorrect / misleading
                                                                / misrepresented / or there has been suppression of facts / materials, or violates
                                                                the criteria of eligibility,
                                                            </p>
                                                            <p>
                                                                (2) lf the industrial unit fails to commence production within the prescribed period.
                                                            </p>
                                                            <p>
                                                                I hereby undertake to furnish information, reports, periodical statements etc to
                                                                the DIC / Directorate of Industries, Odisha as and when required.
                                                            </p>
                                                            <p>
                                                                I hereby undertake to abide by the terms and conditions prescribed under the provisions
                                                                of Industrial Policy Resolution -2015 and its operational guidelines. Copies of
                                                                relevant documents in support of information / facts furnished above are enclosed
                                                                here with.</p>
                                                            <div class="col-sm-4 ">
                                                            </div>
                                                            <div class="col-sm-2 ">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <br />
                                                                <img id="PreviewImage" src="" alt="" style="width: 144px; height: 70px;" runat="server" /><br />
                                                                <div class="form-group">
                                                                    <div class="row" id="divUpload">
                                                                        <label class="col-sm-2">
                                                                            Upload</label>
                                                                        <div class="col-sm-6">
                                                                            <asp:FileUpload CssClass="form-control" onchange="readURL(this);" ID="FileUpload1"
                                                                                runat="server" /></div>
                                                                    </div>
                                                                    <asp:HiddenField ID="HdnValueFlag" runat="server" />
                                                                    <br />
                                                                    Signature of
                                                                    <asp:Label ID="lblauthority" Text="Applicant" runat="server"></asp:Label>
                                                                    in full and on behalf of M/ s
                                                                    <asp:Label ID="Label101" runat="server"></asp:Label>
                                                                </div>
                                                                Date: <b>
                                                                    <asp:Label ID="lblcurdt" runat="server"></asp:Label></b>
                                                            </div>
                                                            <div class="clearfix">
                                                            </div>
                                                            <div class="clearfix">
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
            var msgTitle = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'; //  'Incentive';

            $(function () {
                $('.datePicker').datepicker({
                    dateFormat: 'dd:mm:yyyy', separator: ' @ ', minDate: new Date(), autoclose: true
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
    </form>
</body>
</html>
