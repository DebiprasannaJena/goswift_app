<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatentRegistrationPrint.aspx.cs" Inherits="incentives_PatentRegistrationPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
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
                                                                            <asp:TemplateField HeaderText="Name of Financial Institution">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblslno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="30px"></HeaderStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="SL#">
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
                                                    </div>
                                                </div>
                                            </div>
                                           

                                           <div class="panel panel-default">
                                        <div class="panel-heading" role="tab" id="Div11">
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
                                                            <asp:GridView ID="grvItmDetail" runat="server" class="table table-bordered table-condensed"
                                                                AutoGenerateColumns="false">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl#">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%-- <asp:BoundField DataField="vchItemName" HeaderText="IPR Name" />--%>
                                                                    <asp:TemplateField HeaderText="Patent/IPR Category">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("VchCatgoryName") %>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnCategory" runat="server" Value='<%# Eval("IntCatgoryid") %>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField  HeaderText="Patent/IPR Sub Category">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSubCategory" runat="server" Text='<%# Eval("VchSubCatgoryName") %>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnSubCategory" runat="server" Value='<%# Eval("IntSubCatgoryid") %>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="vchAuthorityNm" HeaderText="Name of Patent/IPR Issuing Authority" />
                                                                    <asp:BoundField DataField="dtCommercialDt" HeaderText="Date of Commercial Use" />
                                                                    <asp:BoundField DataField="vchIPRRegistrationNo" HeaderText="Patent / IPR Registration Number" />
                                                                    <asp:BoundField DataField="vchIPRRegistrationFile" HeaderText="Patent /IPR Registration Certificate" />
                                                                    <asp:BoundField DataField="dtRegistrationDate" HeaderText="Patent / IPR Registration Date" />
                                                                    <asp:BoundField DataField="decExpenditureincurred" HeaderText="Expenditure Incurred to Obtain Patent/IPR" />
                                                                    <asp:BoundField DataField="vchExpenditureFile" HeaderText="Copy of Bills/Vouchers/receipts as Patent Expenditure Statement" />
                                                                </Columns>
                                                            </asp:GridView>
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
                                                          
                                                                        <asp:GridView ID="grdMeansOfFinancePatent" runat="server" CssClass="table table-bordered"
                                                                            AutoGenerateColumns="false" ShowHeader="true">
                                                                            <Columns>
                                                                                <asp:TemplateField >
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblslno" HeaderText="Sl #"  runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle Width="30px"></HeaderStyle>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name of Financial Institution">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPatFinancialName" runat="server" Text='<%# Eval("VCH_NAME_OF_FINANCIAL_INST") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Amount Availed">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPatAvailedAmt" runat="server" Text='<%# Eval("DEC_AVAILED_AMT") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left"  HeaderText="Amount Avaialed Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPatAvailedDate" runat="server" Text='<%# Eval("DTM_AVAILED_DATE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                  <asp:TemplateField HeaderStyle-HorizontalAlign="Left"  HeaderText="Loan Number">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPatLoanNo" runat="server" Text='<%# Eval("VCH_LOAN_NO") %>'></asp:Label>
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
                                                        <a>Availed Details</a>
                                                    </h4>
                                                </div>
                                                <div id="AvailedClaimDetails" class="panel-collapse in" role="tabpanel" aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                    <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-12">
                                                            Assistance details for Patent/IPR incentives already availed by this enterprise
                                                        </label>
                                                        <div class="col-sm-12  margin-bottom10">
                                                            <asp:GridView ID="grdAssistanceDetailsAD" runat="server" CssClass="table table-bordered"
                                                                AutoGenerateColumns="false">                                                              
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl#">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblSlNo" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Body (Pvt, State Govt (Specify State),GoI)">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblBody" Text='<%# Eval("vchBody") %>'></asp:Label>
                                                                        </ItemTemplate>                                                                     
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name of Financial Institution">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblName" Text='<%# Eval("vchInstitutionName") %>'></asp:Label>
                                                                        </ItemTemplate>                                                                       
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount Availed">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblAmountAvailed" Text='<%# Eval("decAmountAvailed") %>'></asp:Label>
                                                                        </ItemTemplate>                                                                       
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Availed Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblAvailedDate" Text='<%# Eval("dtmAvailedDate") %>'></asp:Label>
                                                                        </ItemTemplate>                                                                       
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sanction Order no.">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblSanctionOrderNo" Text='<%# Eval("vchSanctionOrderNo") %>'></asp:Label>
                                                                        </ItemTemplate>                                                                       
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                           
                                                        </div>
                                                    </div>
                                                </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-5 ">
                                                                    <%--<asp:RadioButton ID="radNeverAvailedPrior" runat="server" />--%>
                                                                    <asp:CheckBox ID="radNeverAvailedPrior" runat="server" />
                                                                    Mark if Subsidy for Plant and Machinery was never availed prior to this
                                                                </label>
                                                                <%--<div class="col-sm-3">
                                                            <small class="text-gray">Undertaking on non-availment of subsidy earlier on this project</small>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:HyperLink ID="hypSubsidyAvailed" data-toggle="tooltip" title="View file" CssClass="btn btn-success "
                                                                runat="server" Target="_blank"><i class="fa fa-file-pdf-o"></i></asp:HyperLink>
                                                            <asp:LinkButton ID="LinkButton21" CssClass="btn btn-warning" data-toggle="tooltip"
                                                                Visible="false" title="Update File" runat="server"><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton22" CssClass="btn btn-danger" data-toggle="tooltip"
                                                                OnClientClick="return openpopup(flSubsidyAvailed);" title="Upload" runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LnkflSubsidyAvailed" OnClientClick="JavaScript: return false;"
                                                                data-toggle="tooltip" title="View file" target="_blank" runat="server"></asp:LinkButton>
                                                            <div style="visibility: hidden;">
                                                                <asp:FileUpload ID="flSubsidyAvailed" runat="server" data-toggle="tooltip" title="Choose file"
                                                                    onchange="return FileCheck(this,LnkflSubsidyAvailed);" />
                                                            </div>
                                                        </div>--%>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-6 ">
                                                                    <%--<asp:RadioButton ID="radSubsidyAvailed" runat="server" />--%>
                                                                    <asp:CheckBox ID="radSubsidyAvailed" runat="server" />
                                                                    Mark if Subsidy already availed
                                                                </label>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12 ">
                                                                    Details of Subsidy Already availed
                                                                </label>
                                                                
                                                                <asp:GridView ID="grdIncentiveAvailed" runat="server" CssClass="table table-bordered"
                                                                    AutoGenerateColumns="false" ShowFooter="false">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl#">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSlNo" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Body (Pvt, State Govt (Specify State),GoI)">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblBody" Text='<%# Eval("vchBody") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Name of Financial Institution">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblName" Text='<%# Eval("vchInstitutionName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount Availed">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAmountAvailed" Text='<%# Eval("decAmountAvailed") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Availed Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAvailedDate" Text='<%# Eval("dtmAvailedDate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sanction Order no.">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSanctionOrderNo" Text='<%# Eval("vchSanctionOrderNo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        



                                                          <div class="form-group not-active">
                                                    <div class="row">
                                                        <%--<label for="Iname" class="col-sm-3">
                                                            Amount of Differential Claim to be Exempted
                                                        </label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="txtClaimtExempted" runat="server" CssClass="form-control" Text="0.00"></asp:Label>
                                                        </div>--%>
                                                         <label for="Iname" class="col-sm-3">
                                                                 Amount of Differential Claim to be Exempted</label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span><asp:Label ID="txtClaimtExempted" CssClass="dataspan" runat="server"
                                                                        Text="--"></asp:Label>
                                                                </div>
                                                    </div>
                                                </div>
                                                <div class="form-group not-active">
                                                    <div class="row">
                                                    <label for="Iname" class="col-sm-3">
                                                                  Present Claim for reimbursement</label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span><asp:Label ID="txtClaimReimbursement" CssClass="dataspan" runat="server"
                                                                        Text="--"></asp:Label>
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
                                                              Please provide Authorizing letter such as Power of attorney/ Board 
                                                              Resolution/Society Resolution/ signed by Authorized Signatory
                                                            </td>
                                                            <td>
                                                                <a id="LnkViewRehabilDoc" target="_blank" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
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
                                                                <a id="A2" target="_blank" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
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
                                                                <a id="LnkViewIndustryUnitDoc" target="_blank" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Certificate of registration under Indian Partnership Act1932 / Societies Registration
                                                                Act- 1860 / Certificate of incorporation (Memorandum of association & Article of
                                                                Association ) under Company Act1956
                                                            </td>
                                                            <td>
                                                                <a target="_blank" data-toggle="View Documen" title="Upload" id="LnkViewCertificateRegistration"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Certificate on Date of Commencement of production
                                                            </td>
                                                            <td>
                                                                <a target="_blank" id="LnkViewCertificateCommence" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr id="tr_authorizing" runat="server" visible="false">
                                                            <td>
                                                                Authorizing letter such as Power of attorney/ Board Resolution/Society Resolution/
                                                                signed by Authorized Signatory
                                                            </td>
                                                            <td>
                                                                <a id="LnkViewAUTHORIZEDFILE" target="_blank" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr id="tr_Pioneer" runat="server" visible="false">
                                                            <td>
                                                                Document of Priority Sector / Pioneer Unit in each Priority Sector / Migrated industrial
                                                                unit treated as new industrial unit /issued by Director of lndustries, Odisha
                                                            </td>
                                                            <td>
                                                                <a id="LnkViewPinoneerDoc" target="_blank" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Document in Support of Number of Employees shown as directly employed (e.g. Certificate
                                                                by DLO)- this certificate has to be taken
                                                            </td>
                                                            <td>
                                                                <a target="_blank" id="hypEmpDoc" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Upload Plant & Machinery Investment Details(In the format as downloaded from this form)
                                                            </td>
                                                            <td>
                                                                <a target="_blank" id="hypFirstInvestment" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Term loan sanction order of Financial lnstitute (s) / Banks
                                                            </td>
                                                            <td>
                                                                <a target="_blank" id="hypFinancial" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Undertaking on non-availment of subsidy earlier on this project
                                                            </td>
                                                            <td>
                                                                <a target="_blank" id="hypAvail1" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Documents in Support of Interest Subsidy Availed if any Link to Upload documents 
                                                                like Interest Paid/Gauruntee Fee Paid under CGTMSE or any other valid proofs
                                                            </td>
                                                            <td>
                                                                <a target="_blank" id="hypAvail2" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
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
                                                        I ,Sri
                                                        <asp:Label ID="Label7" runat="server" Text="Bikash Kumar Panda"></asp:Label>s/o
                                                        <asp:Label ID="Label8" runat="server" Text="Chittaranjan Panda"></asp:Label>
                                                        at present
                                                        <asp:Label ID="Label9" runat="server" Text="General Manager"></asp:Label>
                                                        (designation) of M/S
                                                        <asp:Label ID="Label10" runat="server" Text="Apparel"></asp:Label>
                                                        (name of the industrial unit) certify that the information furnished as above is
                                                        true and correct to the best of my knowledge and belief.</p>
                                                    <p>
                                                        I hereby undertake to abide by the terms and conditions prescribed under the provisions
                                                        of Odisha lndustrial Policy 2015 and its operational guidelines.</p>
                                                    <p>
                                                        I hereby undertake to repay the assistance amount or any part thereof with penal
                                                        interest as diecided by the authority-</p>
                                                    <ol>
                                                        <li>lf the information furnished is found to be false / incorrect i misleading or misrepresented
                                                            and there has been suppression of facts / materials or disbursed in excess of the
                                                            amount actually admissible for whatsoever reason.</li>
                                                        <li>lf the patent and intellectual property right registered is revoked by the authority
                                                            for any reason within five years of registration.</li>
                                                    </ol>
                                                    <p>
                                                        I hereby certify that this industrial unit has not applied / sanctioned / availed
                                                        any amount of assistance under any other scheme of the State Govt. or the Central
                                                        Govt. or any</p>
                                                    <p>
                                                        Financial lnstitution(s) / Support organization in the country and abroad against
                                                        which the present claim is made.</p>
                                                    <div class="col-sm-4 ">
                                                    </div>
                                                    <div class="col-sm-2 ">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        Signature of the Proprietor / Managing Partner / Managing Director / Authorized
                                                        Signatory in full and behalf of
                                                        <br />
                                                        M/s :<img runat="server" id="PreviewImage" height="70" width="140" /><br />
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-2">
                                                                    Upload</label>
                                                                <div class="col-sm-6">
                                                                    <asp:FileUpload CssClass="form-control" ID="flSignature" runat="server" onchange="readURL(this);" /></div>
                                                            </div>
                                                        </div>
                                                        Date: <b>6-Sept-2017</b>
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