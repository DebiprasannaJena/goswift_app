<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Exemption_Premium_FormPreview.aspx.cs"
    Inherits="incentives_PatentRegistration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
     <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
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
                                                        <p class="text-red text-right">
                                                            All Amouts to be Entered in INR(Exact Amount)</p>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <div class="col-sm-12 ">
                                                                    <asp:GridView ID="grdProduction" runat="server" CssClass="table table-bordered" DataKeyNames="VCHPRODUCTNAME"
                                                                        AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Sl No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblslno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="5%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Product Name">
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
                                                                            <asp:TemplateField HeaderText="Unit">
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
                                                            Proposed Date of Commencement of Production</h4>
                                                        <div class="form-group">
                                                            <div class="col-sm-12  margin-bottom10">
                                                                <table class="table table-bordered">
                                                                    <tbody>
                                                                        <tr>
                                                                            <th>
                                                                                DATE
                                                                            </th>
                                                                            <th>
                                                                                LOCATION
                                                                            </th>
                                                                            <th>
                                                                                STATUS
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="LblDateProduction" CssClass="dataspan" runat="server">--</asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="LblLocationProd" CssClass="dataspan" runat="server">--</asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="LblStatusProd" CssClass="dataspan" runat="server">--</asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
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
                                                                    <asp:Label ID="LblTimescheduleforyearofcomm" CssClass="dataspan" runat="server" Text="03-Jan-2017"></asp:Label>
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
                                                                                    <asp:Label ID="LblLandtype" runat="server">--</asp:Label>
                                                                                </td>
                                                                                <td class="text-right">
                                                                                    <asp:Label ID="txtLandtype" runat="server">--</asp:Label>
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
                                                                                    <asp:Label ID="txtBuilding" runat="server">--</asp:Label>
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
                                                                                    <asp:Label ID="txtPlantMachinery" runat="server">--</asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    4
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="lblOtherFixedAssests" Text="Other Fixed Assests"></asp:Label>
                                                                                </td>
                                                                                <td class="text-right">
                                                                                    <asp:Label ID="txtOtherFixedAssests" runat="server">--</asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <strong>Total</strong>
                                                                                </td>
                                                                                <td class="text-right">
                                                                                    <strong>
                                                                                        <asp:Label runat="server" ID="lblTotalAmount" Text="--"></asp:Label></strong>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
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
                                                                    <asp:GridView ID="grdMeansOfFinance" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"
                                                                        ShowHeader="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="gggggggg">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblslno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name">
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
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Loan Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTermLoan" runat="server" Text='<%# Eval("DEC_LOAN_AMT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Sanction Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSacDate" runat="server" Text='<%# Eval("DTM_SACTION_DATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Avail Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAvailedAmt" runat="server" Text='<%# Eval("DEC_AVAILED_AMT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Avail Date">
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
                                                <div class="panel-heading" role="tab" id="headingThree">
                                                    <h4 class="panel-title">
                                                        <a>Land Details </a>
                                                    </h4>
                                                </div>
                                                <div id="IndustryDetails" class="panel-collapsein" role="tabpanel" aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-6 ">
                                                                    <small>Cost of the Project ( New / Existing & E/M/D)</small></label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblCostofProject" CssClass="dataspan" runat="server" Text="500000"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-6 ">
                                                                    <small>Area of Land required as per DPR / Project report</small></label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblLandRequiredAsperRpt" CssClass="dataspan" runat="server" Text="1500SqrFt"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-6 ">
                                                                    <small>Area of Land acquired</small></label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="LblLandRequired" CssClass="dataspan" runat="server" Text="03-Jan-2017"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12 ">
                                                                    Particulars of Land to be converted</label>
                                                                <div class="col-sm-12">
                                                                    <table class="table table-bordered">
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                <asp:GridView ID="grvLandInfo" runat="server" CssClass="table table-bordered table-condensed"
                                                                                    AutoGenerateColumns="false" ShowHeader="false">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Sl#">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblslno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle Width="30px"></HeaderStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Mouza">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblMouza" runat="server" Text='<%# Eval("Mouza") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Khata No">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblKhataNo" runat="server" Text='<%# Eval("KhataNo") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Plot No">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblPlotNo" runat="server" Text='<%# Eval("PlotNo") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Area">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblArea" runat="server" Text='<%# Eval("Area") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Present Kisam">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblPresentKisam" runat="server" Text='<%# Eval("PresentKisam") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
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
                                                                Document in support of date of first investment in fixed capital i.e. land i building
                                                                / plant & machinery and balancing equipment
                                                            </td>
                                                            <td>
                                                                <a target="_blank" id="hypDocfirstinvestment" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Approved Detailed project report
                                                            </td>
                                                            <td>
                                                                <a target="_blank" id="hypLinkApprovedDetailDoc" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Land document with land particulars to be converted for industrial use
                                                            </td>
                                                            <td>
                                                                <a target="_blank" id="lnkLandfileview" data-toggle="View Documen" title="Upload"
                                                                    runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Clearance certificate of OSPCB
                                                            </td>
                                                            <td>
                                                                <a target="_blank" data-toggle="View Documen" id="lknSatutoryClean" title="Upload"
                                                                    runat="server">View Document</a>
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
                minDate: new Date(), autoclose: true
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
        function CheckFile() {

            if (($('#FluSign').get(0).files.length == 0)) {
                alert('Please Upload Signature file');
                return false;
            }
            else {  
                $('#aModal').attr('data-target', '#myModal2');
            }
        }
    </script>
    </form>
</body>
</html>
