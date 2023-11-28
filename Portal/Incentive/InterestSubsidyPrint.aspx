<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InterestSubsidyPrint.aspx.cs" Inherits="incentives_InterestSubsidyPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
<%--    <link href="../../css/custom.css" rel="stylesheet" type="text/css" />   --%>
   <%-- <link href="../../css/incentive.css" rel="stylesheet" type="text/css" />--%>
      <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
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
        
        <div class="registration-div investors-bg">
            <div id="exTab1" class="">
                
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="1a">
                        <div class="form-sec">                            
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
                                <div class="incentivepreiview">                                    
                                    <div class="prievewdynamicdata">
                                        <div class="panel-group padding-20" id="accordion" role="tablist" aria-multiselectable="true">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="headingOne">
                                                    <h4 class="panel-title">
                                                        <a>Industrial Unit's Details </a>
                                                    </h4>
                                                </div>
                                                <div id="collapseOne" class="panel-collapsein" role="tabpanel" aria-labelledby="headingOne">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Name of Enterprise/Industrial Unit</label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span><asp:Label ID="LblEnterPrise" CssClass="dataspan" runat="server"
                                                                        Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Organization Type</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblOrgType" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Name of Applicant</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblApplicantName" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Application Applying By</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblApplyBy" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Address of Industrial Unit</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblAddressInd" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="divaadhar" runat="server" visible="false">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Aadhaar Card No</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblAadhaar" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Unit Category</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblUnitCategory" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Unit Type</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblUnitType" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Is Priority</label>
                                                                <div class="col-sm-3 ">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblPriority" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Address of Registered Office of the Industrial Unit</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblRegAddress" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Name of Managing Partner</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblManagingPartner" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    EIN/ IEM/ IL No.</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblEINNo" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Date of EIN/ IEM/ IL Date</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblEINDate" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    PC No.</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblPCNo" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    PC Issuance Date</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblPCInsuranceDate" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Date of Commencement of Production</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblCommenceDate" CssClass="dataspan" runat="server" Text="--"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="headingTwo">
                                                    <h4 class="panel-title">
                                                        <a>Production & Employment Details</a>
                                                    </h4>
                                                </div>
                                                <div id="PromoterInformation" class="panel-collapse in" role="tabpanel" aria-labelledby="headingTwo">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <div class="col-sm-12 ">
                                                                    <asp:GridView ID="grdProduction" runat="server" CssClass="table table-bordered" DataKeyNames="VCHPRODUCTNAME"
                                                                        AutoGenerateColumns="false" ShowHeader="true">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Sl#">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblslno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="5%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Product/Service Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblProductionName" runat="server" Text='<%# Eval("VCHPRODUCTNAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="35%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Quantity">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("INTQUANTITY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="20%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("VCHUNIT") %>'></asp:Label>
                                                                                    <asp:HiddenField ID="hdnUnit" runat="server" Value='<%# Eval("INTUNIT") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Value">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblValue" runat="server" Text='<%# Eval("DECVALUE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="20%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <h4 class="h4-header">
                                                            Employment Generated</h4>
                                                        <div class="form-group">
                                                            <div class="col-sm-12  margin-bottom10">
                                                                <div class="row">
                                                                    <label for="lblDirEmp" class="col-sm-6 ">
                                                                        <small>Direct Empolyment IN NUMBERS (on Company Payroll)</small></label>
                                                                    <div class="col-sm-3">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lblDirEmp" CssClass="dataspan" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="lblContEmp" class="col-sm-6 ">
                                                                        <small>Contractual Employment ( IN NUMBERS )</small></label>
                                                                    <div class="col-sm-3">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lblContEmp" CssClass="dataspan" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12  margin-bottom10">
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
                                                                            <td class="text-right">
                                                                                <asp:Label ID="lblCurrentManagerial" CssClass="dataspan" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                            <td class="text-right">
                                                                                <asp:Label ID="lblProposedManagerial" CssClass="dataspan" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Supervisory
                                                                            </td>
                                                                            <td class="text-right">
                                                                                <asp:Label ID="lblCurrentSupervisory" CssClass="dataspan" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                            <td class="text-right">
                                                                                <asp:Label ID="lblProposedSupervisory" CssClass="dataspan" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Skilled
                                                                            </td>
                                                                            <td class="text-right">
                                                                                <asp:Label ID="lblCurrentSkilled" CssClass="dataspan" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                            <td class="text-right">
                                                                                <asp:Label ID="lblProposedSkilled" CssClass="dataspan" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Semi-Skilled
                                                                            </td>
                                                                            <td class="text-right">
                                                                                <asp:Label ID="lblCurrentSemiSkilled" CssClass="dataspan" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                            <td class="text-right">
                                                                                <asp:Label ID="lblProposedSemiSkilled" CssClass="dataspan" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Unskilled
                                                                            </td>
                                                                            <td class="text-right">
                                                                                <asp:Label ID="lblCurrentUnskilled" CssClass="dataspan" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                            <td class="text-right">
                                                                                <asp:Label ID="lblProposedUnskilled" CssClass="dataspan" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                TOTAL
                                                                            </td>
                                                                            <td class="text-right">
                                                                                <asp:Label ID="lblCurrentTotal" CssClass="dataspan" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                            <td class="text-right">
                                                                                <asp:Label ID="lblProposedTotal" CssClass="dataspan" runat="server" Text=""></asp:Label>
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
                                                <div class="panel-heading" role="tab" id="Div3">
                                                    <h4 class="panel-title">
                                                        <a>Investment Details </a>
                                                    </h4>
                                                </div>
                                                <div id="Div4" class="panel-collapsein" role="tabpanel" aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-6 ">
                                                                    Date of First Fixed Capital Investment <small>(for land/Building/plant and machinery
                                                                        & Balancing Equipment)</small></label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblTimescheduleforyearofcomm" CssClass="dataspan" runat="server" Text=""></asp:Label>
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
                                                                                <asp:Label ID="lblLandtype" CssClass="dataspan" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                            <td class="text-right">
                                                                                <asp:Label ID="lblLandtypeAmount" CssClass="dataspan" runat="server" Text=""></asp:Label>
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
                                                                                <asp:Label ID="lblBuilding" CssClass="dataspan" runat="server" Text=""></asp:Label>
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
                                                                                <asp:Label ID="lblPlantMachinery" CssClass="dataspan" runat="server" Text=""></asp:Label>
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
                                                                                <asp:Label ID="lblBalancingEquipment" CssClass="dataspan" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                8
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="lblOtherlbl" Text="Other Fixed Assests"></asp:Label>
                                                                            </td>
                                                                            <td class="text-right">
                                                                                <asp:Label ID="lblOtherFixedAssests" Text="" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:Label runat="server" ID="lblTotal" Text="Total"></asp:Label>
                                                                                <%--<strong>Total</strong>--%>
                                                                            </td>
                                                                            <td class="text-right">
                                                                                <asp:Label runat="server" ID="lblTotalAmount" Text=""></asp:Label>
                                                                                <%--<strong>365.7</strong>--%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <h4 class="h4-header">
                                                            MEANS OF FINANCE</h4>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12 ">
                                                                    Term Loan Details</label>
                                                                <div class="col-sm-12">
                                                                    <asp:GridView ID="grdMeansOfFinance" runat="server" CssClass="table table-bordered"
                                                                        AutoGenerateColumns="false" ShowHeader="true">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="SL#">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblslno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="30px"></HeaderStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name of Financial Institution">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFinancialName" runat="server" Text='<%# Eval("VCH_NAME_OF_FINANCIAL_INST") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="State">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblState" runat="server" Text='<%# Eval("VCH_STATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="City">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCity" runat="server" Text='<%# Eval("VCH_CITY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Term Loan Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTermLoan" runat="server" Text='<%# Eval("DEC_LOAN_AMT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Sanction Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSacDate" runat="server" Text='<%# Eval("DTM_SACTION_DATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Availed Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAvailedAmt" runat="server" Text='<%# Eval("DEC_AVAILED_AMT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Availed Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAvailedDate" runat="server" Text='<%# Eval("DTM_AVAILED_DATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12 ">
                                                                Financial Year Wise Term Loan Re-Payment

                                                                    </label>
                                                                <div class="col-sm-12">
                                                                    <asp:GridView ID="grdWorkingLoan" runat="server" CssClass="table table-bordered"
                                                                                    AutoGenerateColumns="false" >
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="SL#">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblslno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle Width="30px"></HeaderStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name of Financial Institution">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblFinancialName_working" runat="server" Text='<%# Eval("VCH_NAME_OF_FINANCIAL_INST_WORKING") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="State">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblState_working" runat="server" Text='<%# Eval("VCH_STATE_WORKING") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left"  HeaderText="City">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblCity_working" runat="server" Text='<%# Eval("VCH_CITY_WORKING") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Term Loan Amount">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblTermLoan_working" runat="server" Text='<%# Eval("DEC_LOAN_AMT_WORKING") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Sanction Date">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblSacDate_working" runat="server" Text='<%# Eval("DTM_SACTION_DATE_WORKING") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Availed Amount">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblAvailedAmt_working" runat="server" Text='<%# Eval("DEC_AVAILED_AMT_WORKING") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Availed Date">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblAvailedDate_working" runat="server" Text='<%# Eval("DTM_AVAILED_DATE_WORKING") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                       
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                             <div class="panel-heading" role="tab" id="Div1">
                                                    <h4 class="panel-title">
                                                        <a>Interest Subsidy Details </a>
                                                    </h4>
                                                </div>
                                                <div id="Div5" class="panel-collapsein" role="tabpanel" aria-labelledby="headingThree">
                                                  <div class="panel-body">

                                                  <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12 ">
                                                                    Interest Paid on Term Loan from Date of Production Commencemnet</label>
                                                                <div class="col-sm-12">
                                                                    <asp:GridView runat="server" ID="gvdProduction" CssClass="table table-bordered" 
                                                                                        AutoGenerateColumns="false" 
                                                                                       >
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="strFYrTLoan" HeaderText="Financial Year" />
                                                                                            <asp:BoundField DataField="decInterestAmt" HeaderText="Interest Amount"/>
                                                                                            <asp:BoundField DataField="strPaymentDate" HeaderText="Payment Date"/>
                                                                                           
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12 ">
                                                                    Details of Previously avialed/ sanctioned Interest Subsidy</label>
                                                                <div class="col-sm-12">
                                                                    <asp:GridView runat="server" ID="gvdsanctioned" AutoGenerateColumns="false" 
                                                                                        CssClass="table table-bordered" 
                                                                                     >
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="strFYrSanction" HeaderText="Financial Year" />
                                                                                            <asp:BoundField DataField="strFinInstitute" HeaderText="Name of Financial Institution"/>
                                                                                            <asp:BoundField DataField="strSanOrderNo" HeaderText="Sanction Order No."/>
                                                                                            <asp:BoundField DataField="strSanDate" HeaderText="Sanction Date" />
                                                                                            <asp:BoundField DataField="decSancAmt"  HeaderText="Sanctioned Amount"/>
                                                                                           
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                                <div class="row">
                                                                    <label for="Iname" class="col-sm-3 ">
                                                                        Amount Claimed for Interest Subsidy</label>
                                                                    <div class="col-sm-3">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label  ID="lblAmountClaimed"  runat="server"></asp:Label>
                                                                       
                                                                    </div>
                                                                    <label for="Iname" class="col-sm-3 ">
                                                                        Period of Claim of Interest Subsidy</label>
                                                                    <div class="col-sm-3">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lblPeriodofClaim"  runat="server"></asp:Label>
                                                                        
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-3 ">
                                                                Amount for claim for re-imbursement of Guarntee Fee under CGTMSE</label>
                                                            <div class="col-sm-3">
                                                                <span class="colon">:</span>
                                                                <asp:Label  ID="lblreimbursement"  runat="server"></asp:Label>
                                                               
                                                            </div>
                                                        </div>
                                                    </div>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label for="Iname" class="col-sm-3 ">
                                                                        Differential Amont of Benefit Claimed Already</label>
                                                                    <div class="col-sm-3">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lblDifferentialAmont"  runat="server"></asp:Label>
                                                                       
                                                                    </div>
                                                                 <%--   <label for="Iname" class="col-sm-3 ">
                                                                        <small class="text-gray">documents in support of claim for Differential Amount</small></label>
                                                                    <div class="col-sm-3">
                                                                        <span class="colon">:</span>
                                                                        <asp:HyperLink  ID="hypDifferAmtClaim" data-toggle="tooltip" title="View file" CssClass="btn btn-success " Target="_blank" 
                                                                            runat="server"><i class="fa fa-file-pdf-o"></i></asp:HyperLink>
                                                                       <asp:FileUpload runat="server" ID="DifferAmtClaim" Style="display: none" onchange="return FileCheck(this,LnkDifferAmtClaim);" />
                                                                        <asp:LinkButton ID="LinkButton22" CssClass="btn btn-danger" data-toggle="tooltip"
                                                                            title="Upload" runat="server" OnClientClick="return openpopup(DifferAmtClaim);"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="LnkDifferAmtClaim" OnClientClick="JavaScript: return false;"
                                                                            data-toggle="tooltip" title="View file" target="_blank" runat="server"></asp:LinkButton>
                                                                    </div>
                                                                 --%>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-12 ">
                                                                Upload Valid Statutory Clearences</label>
                                                            <div class="col-sm-12">
                                                                            
                                                                                    <asp:GridView runat="server" ID="gvdStatutoryClearences" CssClass="table table-bordered"
                                                                                        AutoGenerateColumns="false" 
                                                                                        >
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="strClearanceName" HeaderText="Clearence Name	"/>
                                                                                            <asp:BoundField DataField="ClearanceNameDoc" HeaderText="Upload"/>
                                                                                           
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                
                                                            </div>
                                                        </div>
                                                    </div>

                                                  </div> 
                                                </div> 
                                            </div> 





                                            <div class="panel panel-default">
                                             <asp:HiddenField ID="hdnId" runat="server" />
                                                <div class="panel-heading" role="tab" id="Div2">
                                                    <h4 class="panel-title">
                                                        <a class="tect-center">Document CheckList</a>
                                                    </h4>
                                                </div>
                                                <div class="panel-body">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th>
                                                                Document Name
                                                            </th>
                                                            <th width="150px">
                                                                View
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Document(s) in support of rehabilitated sick industrial unit treated at par with
                                                                new industrial unit and duly recommended by State Level Inter Institutional Committee
                                                                (SLIIC) for this incentive
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink  id="LnkViewRehabilDoc" target="_blank" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Documeht(s) in support of Industrial unit seized under Section 29 of the State Financial
                                                                Corporation Act,1951/ SARFAESI Ac|,2002 and thereafter sold to a new entrepreneur
                                                                on sale of assets basis and treated as new industrial unit forthe purpose of this
                                                                IPR
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink id="LnkViewIndustryUnitDoc" target="_blank" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Certificate of registration under Indian Partnership Act1932 / Societies Registration
                                                                Act- 1860 / Certificate of incorporation (Memorandum of association & Article of
                                                                Association ) under Company Act1956
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink target="_blank" data-toggle="View Documen" title="Upload" id="LnkViewCertificateRegistration"
                                                                    runat="server">View Document</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Certificate on Date of Commencement of production
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink target="_blank" id="LnkViewCertificateCommence" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr id="tr_authorizing" runat="server" visible="false">
                                                            <td>
                                                                Authorizing letter such as Power of attorney/ Board Resolution/Society Resolution/
                                                                signed by Authorized Signatory
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink id="LnkViewAUTHORIZEDFILE" target="_blank" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr id="tr_Pioneer" runat="server" visible="false">
                                                            <td>
                                                                Document of Priority Sector / Pioneer Unit in each Priority Sector / Migrated industrial
                                                                unit treated as new industrial unit /issued by Director of lndustries, Odisha
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink id="LnkViewPinoneerDoc" target="_blank" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Document in Support of Number of Employees shown as directly employed (e.g. Certificate
                                                                by DLO)- this certificate has to be taken
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink target="_blank" id="hypEmpDoc" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Upload Plant & Machinery Investment Details(In the format as downloaded from this form)
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink target="_blank" id="hypFirstInvestment" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Term loan sanction order of Financial lnstitute (s) / Banks
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink target="_blank" id="hypFinancial" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                               
                                                                documents in support of claim for Differential Amount
                                                            <td>
                                                                <asp:HyperLink target="_blank" id="hypDifferAmtClaim" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                               Document in support of implementation delay condoned by Empowered committee
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink target="_blank" id="hypDocumentinsupport" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</asp:HyperLink>
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
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <link href="../../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
         $(function () {
             $('.datePicker').datepicker({
                 dateFormat: 'dd:mm:yyyy',
                 separator: ' @ ',
                 minDate: new Date(),autoclose: true,
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
    </form>
</body>
</html>
