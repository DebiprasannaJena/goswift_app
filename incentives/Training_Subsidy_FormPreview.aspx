﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Training_Subsidy_FormPreview.aspx.cs"
    Inherits="incentives_Training_Subsidy_FormPreview" %>

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
    <script type="text/javascript">
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                $('.innertabs ,.investrs-tab').hide();
                window.print();
                $('.innertabs ,.investrs-tab').show();
            });

            $('.Pioneersec,.attorneysec,.adhardetails').hide();

            $(".optradioPriority").on("click", function () {
                if ($("input:checked").val() == 'Yes') {


                    $('.Pioneersec').show();

                }
                else {

                    $('.Pioneersec').hide();
                }
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
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="SMHome" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <div class="container">
        <div id="divHeader1">
            <uc2:header ID="header1" runat="server" />
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
                                        <h4 style="display: none;">
                                            <asp:Label ID="lblTitle2" runat="server"></asp:Label></h4>
                                        <h2>
                                            <asp:HiddenField ID="hdnTitle" runat="server" />
                                            <asp:Label ID="lblTitle" runat="server" Style="font-weight: 700"></asp:Label></h2>
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
                                                Dist. :<asp:Label ID="lblDist" runat="server"></asp:Label></p>
                                        </div>
                                        <h4>
                                            To</h4>
                                        <div class="padding-left-20">
                                            <p>
                                                The General Manager, Regional Industries Centre / District Industries Centre
                                                <asp:Label ID="lblDistrict" runat="server"></asp:Label><br />
                                                Managing Director,<br />
                                                Industrial Promotion and investment Corporation of Odisha limited<br />
                                                IPICOL House, Jan path, Bhubaneswar,751022
                                            </p>
                                        </div>
                                        <h4>
                                            Sub :
                                        </h4>
                                        <div class="padding-left-20">
                                            <p>
                                                Application for reimbursement of Training Subsidy under IPR-2015.
                                            </p>
                                        </div>
                                        <h4>
                                            Sir,</h4>
                                        <div class="padding-left-20">
                                            <p>
                                                In accordance with the provisions laid down in Industrial Policy Resolution -2015
                                                and its operational guidelines, the claim is submitted with following particulars.
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
                                                                <span class="colon">:</span><asp:Label ID="lbl_EnterPrise_Name" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Organization Type</label>
                                                            <div class="col-sm-6  ">
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
                                                            <div class="col-sm-6  ">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="TxtApplicantName" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group ">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Applied By</label>
                                                            <div class="col-sm-6  ">
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
                                                            <div class="col-sm-6  ">
                                                                <span class="colon">:</span>
                                                                <asp:Label runat="server" ID="lblDocumentType" CssClass="form-control-static">--</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group " id="divadhhardetails" runat="server">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Aadhaar No.</label>
                                                            <div class="col-sm-6  ">
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
                                                            <div class="col-sm-6  ">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_Unit_Cat" runat="server" CssClass="form-control-static">
                                                                </asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:HiddenField ID="hdnEmail" runat="server" />
                                                    <asp:HiddenField ID="hdnMobile" runat="server" />
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
                                                            <div class="col-sm-6  ">
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
                                                                <div class="col-sm-6  ">
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
                                                                <asp:Label for="Iname" class="col-sm-4 col-sm-offset-1" Text="PC Issuance Date Before E/M/D"
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
                                                                <div class="col-sm-6  ">
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
                                                                <asp:Label for="Iname" class="col-sm-4 col-sm-offset-1" Text="PC Issuance Date After E/M/D"
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
                                                            <div class="col-sm-6  ">
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
                                                            <div class="col-sm-6  ">
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
                                                            <div class="col-sm-6  ">
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
                                                            <div class="col-sm-6  ">
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
                                                                <div class="col-sm-6  ">
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
                                                                <div class="col-sm-6  ">
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
                                                <div class="panel-heading" role="tab" id="Div8">
                                                    <h4 class="panel-title">
                                                        <a>Production & Employment Details </a>
                                                    </h4>
                                                </div>
                                                <div id="ProductionEmploymentDetails12" class="panel-collapse collapse in" role="tabpanel"
                                                    aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div runat="server" id="divbefor1">
                                                            <h4>
                                                                <asp:Label runat="server" ID="lblemdBefore" Text="Before E/M/D" CssClass="h2-hdr"></asp:Label>
                                                            </h4>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label for="Iname" class="col-sm-12">
                                                                        Items of Manufacture/Activity
                                                                    </label>
                                                                    <div class="col-sm-12   ">
                                                                        <asp:GridView ID="Grd_Production_Before" runat="server" CssClass="table table-bordered"
                                                                            DataKeyNames="vchProductName" AutoGenerateColumns="false" EmptyDataText="No records found...">
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
                                                                        Direct Employment in Numbers<small> (On Company Payroll)</small>
                                                                    </label>
                                                                    <div class="col-sm-2">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lbl_Direct_Emp_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                    <label for="Iname" class="col-sm-4">
                                                                        Contractual Employment in Numbers</label>
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
                                                                    <div class="col-sm-12   ">
                                                                        <asp:GridView ID="Grd_Production_After" runat="server" CssClass="table table-bordered"
                                                                            DataKeyNames="vchProductName" AutoGenerateColumns="false" EmptyDataText="No records found...">
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
                                                                        Direct Employment in Numbers<small> (On Company Payroll)</small>
                                                                    </label>
                                                                    <div class="col-sm-2">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lbl_Direct_Emp_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                    <label for="Iname" class="col-sm-4">
                                                                        Contractual Employment in Numbers</label>
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
                                                                        Date of First Fixed Capital Investment (In Land/Building/Plant and Machinery & Balancing
                                                                        Equipment)
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
                                                                        Date of First Fixed Capital Investment (In Land/Building/Plant and Machinery & Balancing
                                                                        Equipment)
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
                                                                                        <asp:Label runat="server" ID="Label8" Text="Other Fixed Assets"></asp:Label>
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
                                                            Means of finance
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
                                                            <label class="col-sm-2">
                                                                FDI(If any)
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
                                                <div class="panel-heading" role="tab" id="Div1">
                                                    <h4 class="panel-title">
                                                        <a>Training Details</a>
                                                    </h4>
                                                </div>
                                                <div id="AvailedClaimDetails" class="panel-collapse in" role="tabpanel" aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <div class="col-sm-12  margin-bottom11">
                                                                    <h4 class="h4-header">
                                                                        Newly Recruited</h4>
                                                                    <asp:GridView ID="grdNewRecruited" runat="server" CssClass="table table-bordered"
                                                                        AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Slno.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblslno_NewRec" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="5%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Type of Trainee">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTraineeType_NewRec" runat="server" Text='<%# Eval("vchTraineeType") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="20%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Training Location">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTrainingLoc_NewRec" runat="server" Text='<%# Eval("vchTraingLoc") %>'></asp:Label>
                                                                                    <asp:HiddenField ID="hdnTrainingLoc_NewRec" runat="server" Value='<%# Eval("intTraingLoc") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="15%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="No. of trainees undergone training">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNoOfTrainee_NewRec" runat="server" Text='<%# Eval("intNoOftrainee") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="25%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="No. of days training">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNoOfDays_NewRec" runat="server" Text='<%# Eval("intNoOfDays") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="If outside, name of the organisation/ institution">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblOrg_NewRec" runat="server" Text='<%# Eval("vchOrgName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="35%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                            <div class="row" style="height: 4px">
                                                            </div>
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-4 ">
                                                                    Total number of Trainee in Newly Recruited</label>
                                                                <div class="col-sm-1">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="txtTotal_NewRec" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <div class="col-sm-12  margin-bottom11">
                                                                    <h4 class="h4-header">
                                                                        Skill Upgradation</h4>
                                                                    <asp:GridView ID="grdSkillUpgrade" runat="server" CssClass="table table-bordered"
                                                                        AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Slno.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblslno_SkillUpgrade" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="5%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Type of Trainee">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTraineeType_SkillUpgrade" runat="server" Text='<%# Eval("vchTraineeType") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="20%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Training Location">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTrainingLoc_SkillUpgrade" runat="server" Text='<%# Eval("vchTraingLoc") %>'></asp:Label>
                                                                                    <asp:HiddenField ID="hdnTrainingLoc_SkillUpgrade" runat="server" Value='<%# Eval("intTraingLoc") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="15%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="No. of trainees undergone training">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNoOfTrainee_SkillUpgrade" runat="server" Text='<%# Eval("intNoOftrainee") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="25%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="No. of days training">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNoOfDays_SkillUpgrade" runat="server" Text='<%# Eval("intNoOfDays") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="If outside, name of the organisation/ institution">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblOrg_SkillUpgrade" runat="server" Text='<%# Eval("vchOrgName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="35%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                            <div class="row" style="height: 4px">
                                                            </div>
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-4 ">
                                                                    Total number of Trainee in Skill Upgradation</label>
                                                                <div class="col-sm-1">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="txtTotal_SkillUpgrade" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <h4 class="h4-header">
                                                                        Year wise Subsidy already claimed</h4>
                                                                    <asp:GridView ID="grdSubsidy" runat="server" class="table table-bordered table-responsive"
                                                                        HeaderStyle-CssClass="GVHeader" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Slno.">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Period" DataField="vchFYear" />
                                                                            <asp:BoundField HeaderText="Amount Claimed" DataField="decAmount" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="Div5">
                                                    <h4 class="panel-title">
                                                        <a>Bank Details </a>
                                                    </h4>
                                                </div>
                                                <div id="Div3" class="panel-collapsein" role="tabpanel" aria-labelledby="headingOne">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Account No of Industrial Unit</label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span><asp:Label ID="lblAccNo" CssClass="form-control-static"
                                                                        runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Bank Name</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblBankName" CssClass="form-control-static" runat="server"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Branch Name</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblBranchName" CssClass="form-control-static" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    IFSC Code</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblIFSC" CssClass="form-control-static" runat="server"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    MICR No</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblMICRNo" CssClass="form-control-static" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="Div2">
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
                                                        <tr runat="server" id="divAuthorizing" visible="false">
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
                                                        <%-- <tr runat="server" id="tr_Prod_Comm_Before" visible="false">
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
                                                        <tr runat="server" id="divEmp_Emp_After_Doc_Name" visible="false">
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
                                                        <tr runat="server" id="Div_Approved_DPR_After_Doc" visible="false">
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
                                                                Trainee Details
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="hlnkViewTraineeDetails" data-toggle="View Document" title="View Document"
                                                                    Target="_blank" OnClientClick="JavaScript: return false;" runat="server">View Document</asp:HyperLink>
                                                                <asp:HiddenField ID="hdnSatutory4TdetDoc" runat="server" Value="D251" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Bills & Money receipt towards payment of Training Imparted for production purpose
                                                                only
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="lknViewMnyReceipt" data-toggle="View Document" title="View Document"
                                                                    Target="_blank" OnClientClick="JavaScript: return false;" runat="server">View Document</asp:HyperLink>
                                                                <asp:HiddenField ID="hdnMnyReceipt" runat="server" Value="D265" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Cancelled cheque document to verify the entered A/c details
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="hypBank" runat="server" Target="_blank">View Document</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                OSPCB consent to operate (except white category)
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="lknViewSatutoryClean" data-toggle="View Document" title="View Document"
                                                                    Target="_blank" OnClientClick="JavaScript: return false;" runat="server">View Document</asp:HyperLink>
                                                                <asp:HiddenField ID="hdnSatutory1Doc" runat="server" Value="D252" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Sector Relevant Document
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="lknViewSatutoryClean1" data-toggle="View Document" title="View Document"
                                                                    Target="_blank" OnClientClick="JavaScript: return false;" runat="server">View Document</asp:HyperLink>
                                                                <asp:HiddenField ID="hdnSatCleanDocId" runat="server" Value="D229" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Factory & Boiler - For all industry (10 with direct employment / 20 no of employment
                                                                without power )
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="HypCleanApproveAuthority" runat="server" Target="_blank">View Document</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="Div6">
                                                    <h4 class="panel-title">
                                                        <a class="text-center">Undertaking</a>
                                                    </h4>
                                                </div>
                                                <div class="preiviewfooter padding-20">
                                                    <p>
                                                        I ,
                                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                                                        <%-- at present--%>
                                                        <asp:Label ID="lblPresent" runat="server" Visible="false"></asp:Label>
                                                        of M/S
                                                        <asp:Label ID="lblUnitAddress" runat="server"></asp:Label>
                                                        (name of the industrial unit) certify that the information furnished as above is
                                                        true and correct to the best of my knowledge and belief.</p>
                                                    <p>
                                                        I hereby undertake to abide by the terms and conditions prescribed under the provisions
                                                        of IPR-2015 and its operational guidelines. I hereby certify that the training fees
                                                        claimed in respect of Trainees new/skill up-gradation training has not been claimed
                                                        twice for any trainee.</p>
                                                    <p>
                                                        I hereby undertake to repay / surrender the incentives or any part thereof availed
                                                        with penal interest as decided by the authority-</p>
                                                    <ol>
                                                        <li>If the information stated above is found to be false/ incorrect / misleading or
                                                            misrepresented and there has been suppression of facts / materials or if found to
                                                            have been disbursed in excess of the amount actually admissible for whatsoever reason.</li>
                                                        <li>If the industrial unit goes out of production for a period exceeding six months
                                                            at a time for any reasons other than labor troubles, want of electric power or for
                                                            the reason which is beyond the control of entrepreneur / management during the period
                                                            of incentives.</li>
                                                        <li>If any part of Plant & machinery is disposed of and not reported which affects the
                                                            maximum limit of incentives reimbursement during the period of incentives. I hereby
                                                            certify that this industrial unit has not applied / availed the incentives claimed
                                                            here under any other scheme of the State Govt. or the Central Govt. or any Financial
                                                            institutions. </li>
                                                    </ol>
                                                    <p>
                                                        Copies of relevant documents in support of information / facts furnished above are
                                                        enclosed here with.</p>
                                                    <div class="col-sm-4 ">
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4">
                                                                </label>
                                                                <div class="col-sm-6">
                                                                    <asp:Image runat="server" ID="PreviewImage" Height="70" Width="140" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <asp:HiddenField ID="hdnId" runat="server" />
                                                        <div class="form-group">
                                                            <div class="row" id="divUpload">
                                                                <label class="col-sm-4">
                                                                    Upload Signature of
                                                                    <asp:Label ID="lblauthority" runat="server" Text="Applicant"></asp:Label>
                                                                    in full and on behalf of M/ s
                                                                    <asp:Label ID="Label101" runat="server"></asp:Label>
                                                                    <br />
                                                                </label>
                                                                <div class="col-sm-8">
                                                                    <asp:FileUpload CssClass="form-control" ID="flSignature" runat="server" onchange="readURL(this);" />
                                                                    <small class="text-danger">(.png, .jpeg, .jpg file only and Max file size 4 MB)</small></div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:HiddenField ID="HdnValueFlag" runat="server" />
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
    <uc3:footer ID="footer" runat="server" />
    <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $('.datePicker').datepicker({ dateFormat: 'dd:mm:yyyy', separator: ' @ ', minDate:
    new Date(), autoclose: true
            });
        });
        
         function readURL(input) {
            if (input.files && input.files[0]) {//Check if input has files.

                var ids = input.id;
                var fileExtension = ['png', 'jpg', 'jpeg'];
                if ($.inArray($("#" + ids).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    jAlert('Only png / jpg / jpeg files are allowed.', 'GO-SWIFT');
                    $("#" + ids).val(null);
                    return false;
                }
                else {
                    if ((input.files[0].size > parseInt(4 * 1024 * 1024)) && ($("#" + ids).val() != '')) {
                        jAlert('File must be less then 4MB!', 'GO-SWIFT');
                        $("#" + ids).val(null);
                        //e.preventDefault();
                        return false;
                    }
                    else {
                        var reader = new FileReader(); //Initialize FileReader.

                        reader.onload = function (e) {
                            $('#PreviewImage').attr('src', e.target.result);
                            $('#PreviewImage').attr('style', 'display:block');
                        };
                        reader.readAsDataURL(input.files[0]);
                    }
                }

            }
        }

        function alertredirect(msg) {
            jAlert(msg, 'GO-SWIFT', function (r) {
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