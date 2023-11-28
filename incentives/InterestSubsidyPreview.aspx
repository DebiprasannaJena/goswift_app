<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InterestSubsidyPreview.aspx.cs"
    Inherits="incentives_InterestSubsidyPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc5" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/incentive.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function pageLoad() {

            var hdval = $('#HdnValueFlag').val();
            //            alert(hdval)
            if (hdval == 1) {
                $('.innertabs ,.investrs-tab').hide();
                $('#divHeader1').hide();
                $('#divUpload').hide();
                $('#btnApply').hide();

            }
            getAllLinks();
        }
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                $('.innertabs ,.investrs-tab').hide();
                window.print();
                $('.innertabs ,.investrs-tab').show();
            });

        });
   
    </script>
    <script type="text/javascript" language="javascript">

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
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
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
                                    <li class="active"><a href="appliedlistwithdetails.aspx">Apply For incentive</a></li>
                                    <li><a href="ViewApplicationStatus.ASPX">View Application Status</a></li>
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
                                        <h2>
                                            <asp:Label ID="lblTitle" runat="server" Style="font-weight: 700"></asp:Label></h2>
                                        <p>
                                            Application received after the due date / incomplete in any respect shall be liable
                                            for rejection</p>
                                    </div>
                                    <div class="prieviewdatasec">
                                        <div class="padding-left-20">
                                            <h4>
                                                From</h4>
                                            <div class="padding-left-20">
                                                <p>
                                                    M/s :<asp:Label ID="lblAplicantName" runat="server"></asp:Label></p>
                                                <p>
                                                    Address :<asp:Label ID="lblAddress" runat="server"></asp:Label></p>
                                                <p>
                                                    Dist. :
                                                    <asp:Label ID="lblDist" runat="server"></asp:Label></p>
                                                <p>
                                                    (Location of the Industrial Unit)</p>
                                            </div>
                                            <h4>
                                                To</h4>
                                            <div class="padding-left-20">
                                                <p>
                                                    The General Manager, Regional Industries Centre / District Industries Centre
                                                    <asp:Label ID="lbl_EnterPrise_Name1" runat="server" Text="Label"></asp:Label></p>
                                            </div>
                                            <h4>
                                                Sub :
                                            </h4>
                                            <div class="padding-left-20">
                                                <p>
                                                    Sanction of Interest Subsidy / Reimbursement of Guarantee fee paid under CGTMSE
                                                    Scheme/ under Industrial Policy Resolution 2015
                                                </p>
                                            </div>
                                            <h4>
                                                Sir,</h4>
                                            <div class="padding-left-20">
                                                <p>
                                                    In accordance with the provisions laid down in Industrial Policy Resolution 2015
                                                    and operational guidelines, the claim for Interest Subsidy is submitted herewith
                                                    for the period with following particulars.
                                                </p>
                                            </div>
                                        </div>
                                        <div class="prievewdynamicdata">
                                            <div class="panel-group padding-20" id="accordion" role="tablist" aria-multiselectable="true">
                                                <%--<div class="panel-group" id="Div1" role="tablist" aria-multiselectable="true">--%>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading" role="tab" id="headingOne">
                                                        <h4 class="panel-title">
                                                            <a>Industrial Unit's Details <span class="text-red pull-right " style="margin-right: 20px;">
                                                                All Amounts in INR Lakh</span> </a>
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
                                                                <div class="col-sm-6   ">
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
                                                                <div class="col-sm-6   ">
                                                                    <span class="colon">:</span>
                                                                    <%--<asp:DropDownList CssClass="form-control" ID="DdlGender"
                                                                    runat="server">
                                                                    <asp:ListItem Value="1">Mr.</asp:ListItem>
                                                                    <asp:ListItem Value="2">Ms.</asp:ListItem>
                                                                </asp:DropDownList>--%>
                                                                    <asp:Label ID="TxtApplicantName" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                    Applied By</label>
                                                                <div class="col-sm-6   ">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label runat="server" ID="lblApplyBy" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group " id="divadhhardetails" runat="server">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                    Aadhaar No.</label>
                                                                <div class="col-sm-6   ">
                                                                    <span class="colon">:</span>
                                                                    <div class="col-sm-4 padding-right-0 padding-left-0">
                                                                        <asp:Label ID="TxtAdhaar1" runat="server" MaxLength="4" CssClass="form-control-static"></asp:Label></div>
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
                                                                <div class="col-sm-6   ">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_Unit_Cat" runat="server" CssClass="form-control-static">
                                                                    </asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
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
                                                                    <%--<label>
                                                                <input type="checkbox" />Same as Address of Industrial Unit</label>--%>
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
                                                                    <%--   <asp:DropDownList CssClass="form-control" ID="DrpDwn_Gender_Partner"
                                                                            runat="server">
                                                                            <asp:ListItem Value="1">Mr.</asp:ListItem>
                                                                            <asp:ListItem Value="2">Ms.</asp:ListItem>
                                                                        </asp:DropDownList>--%>
                                                                </div>
                                                                <%-- <div class="col-sm-5">
                                                                        <asp:TextBox ID="Txt_Partner_Name" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </div>--%>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                    EIN/ IEM/ IL No.</label>
                                                                <div class="col-sm-6   ">
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
                                                                    <%--<div class="input-group  date datePicker" id="Div2">--%>
                                                                    <asp:Label ID="lbl_EIN_IL_Date" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    <%-- <span class="input-group-addon"><i class="fa fa-calendar"></i></span>--%>
                                                                    <%-- </div>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div runat="server" id="divbefor">
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                        PC No. Before E/M/D
                                                                    </label>
                                                                    <div class="col-sm-6   ">
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
                                                                        <%-- <div class="input-group  date datePicker" id="Div13">--%>
                                                                        <asp:Label ID="lbl_Prod_Comm_Date_Before" runat="server" />
                                                                        <%-- <span class="input-group-addon"><i class="fa fa-calendar"></i></span>--%>
                                                                        <%-- </div>--%>
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
                                                                        <%--<div class="input-group  date datePicker" id="Div9">--%>
                                                                        <asp:Label ID="lbl_PC_Issue_Date_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                                        <%--<span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div runat="server" id="divafter">
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <%--  <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                        PC No.</label>--%>
                                                                    <asp:Label for="Iname" class="col-sm-4 col-sm-offset-1" Text=" PC No. After" ID="lbl_PC_No_After"
                                                                        runat="server"></asp:Label>
                                                                    <div class="col-sm-6   ">
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
                                                                        <%-- <div class="input-group  date datePicker" id="Div3">--%>
                                                                        <asp:Label ID="lbl_Prod_Comm_Date_After" runat="server" CssClass="form-control-static" />
                                                                        <%--<span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                            </div>--%>
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
                                                                        <%--  <div class="input-group  date datePicker" id="Div9">--%>
                                                                        <asp:Label ID="lbl_PC_Issue_Date_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                                        <%--<span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                            </div>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4 col-sm-offset-1">
                                                                    District</label>
                                                                <div class="col-sm-6   ">
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
                                                                <div class="col-sm-6   ">
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
                                                                <div class="col-sm-6   ">
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
                                                                <div class="col-sm-6   ">
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
                                                                    <div class="col-sm-6   ">
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
                                                                    <div class="col-sm-6   ">
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
                                                                    <%--<asp:CheckBox ID="ChkBx_Sectoral" runat="server" Enabled="false" />--%>
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
                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
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
                                                                                <div class="col-sm-12    ">
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
                                                                                    Direct Empolyment in Numbers<small>(on Company Payroll)</small>
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
                                                                                <div class="col-sm-12    ">
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
                                                                                    Direct Empolyment in Numbers<small>(on Company Payroll)</small>
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
                                                                                            <asp:Label ID="lbl_Land_Before" runat="server"></asp:Label>
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
                                                                                            <asp:Label ID="lbl_Building_Before" runat="server"></asp:Label>
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
                                                                                            <asp:Label ID="lbl_Plant_Mach_Before" runat="server"></asp:Label>
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
                                                                                            <asp:Label ID="lbl_Other_Fixed_Asset_Before" runat="server"></asp:Label>
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
                                                                                            <asp:Label ID="lbl_Land_After" runat="server"></asp:Label>
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
                                                                                            <asp:Label ID="lbl_Building_After" runat="server"></asp:Label>
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
                                                                                            <asp:Label ID="lbl_Plant_Mach_After" runat="server"></asp:Label>
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
                                                                                            <asp:Label ID="lbl_Other_Fixed_Asset_After" runat="server"></asp:Label>
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
                                                                Means of Finance
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
                                                            <%--
      <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>--%>
                                                            <div class="form-group row">
                                                                <label class="col-sm-2">
                                                                    FDI(If Any)
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
                                                    <div class="panel-heading" role="tab" id="headingFour">
                                                        <h4 class="panel-title">
                                                            <a>Term Loan Details</a>
                                                        </h4>
                                                    </div>
                                                    <div id="termLoanDetails" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingFour">
                                                        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate >--%>
                                                        <div class="panel-body">
                                                            <div class="form-group row">
                                                                <label class="col-sm-3">
                                                                    Financial Institution (FI) /Bank Details</label>
                                                                <div class="col-sm-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="txtFinancialInstitution" runat="server" MaxLength="100" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <label class="col-sm-3">
                                                                    Loan Sanction date</label>
                                                                <div class="col-sm-3 ">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label runat="server" ID="txtLoanMaturityDate" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <label class="col-sm-3">
                                                                    Term Loan Sanction Amount</label>
                                                                <div class="col-sm-3 ">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="txtLoanSanctionAmount" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <%--<div class="col-sm-5 col-sm-offset-1">
                                                        <a href="#" data-toggle="modal" data-target="#myModal"><strong>View History if Term
                                                            Loan amount was Changed during the Loan Period</strong></a>
                                                    </div>--%>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <h4 class="h4-header">
                                                                            Planned Disbursal Schedule for all Financial Years</h4>
                                                                        <asp:GridView runat="server" ID="grdPlannedDisbursal" CssClass="table table-bordered"
                                                                            AutoGenerateColumns="false">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Slno.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Lbl_Sl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="DTMDisbursalDate" HeaderText="Disbursal Date" ItemStyle-Width="50%" />
                                                                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <label class="col-sm-3">
                                                                    TOTAL</label>
                                                                <div class="col-sm-3 ">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lblTotal" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="comparesection">
                                                                            <div class="content">
                                                                                <div class="table-responsive">
                                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:GridView runat="server" ID="grdRepayment" AutoGenerateColumns="false" ShowHeader="false"
                                                                                                class="table table-bordered table-striped">
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="decActualPrincipalAmount" HeaderText=" Principal Amount Component">
                                                                                                        <ItemStyle Width="15%" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="decActualInterestAmount" HeaderText="  Interest Amount Component">
                                                                                                        <ItemStyle Width="15%" />
                                                                                                    </asp:BoundField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <h4 class="h4-header">
                                                                            Previous Sanction</h4>
                                                                        <asp:GridView runat="server" ID="GVDPRESAN" CssClass="table table-bordered" AutoGenerateColumns="false"
                                                                            OnRowDataBound="GVDPRESAN_RowDataBound" EmptyDataText="No Records Found...">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Slno.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Lbl_Sl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="DECSactionAmount" HeaderText="Sanction Amount"></asp:BoundField>
                                                                                <asp:BoundField DataField="DTMSactionData" HeaderText="Sanction Date">
                                                                                    <ItemStyle Width="15%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="VCHSactionOrder" HeaderText="Sanction Order">
                                                                                    <ItemStyle Width="25%" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="Sanction Order">
                                                                                    <ItemTemplate>
                                                                                        <asp:HiddenField ID="hdnVCHSanctionOrderdOC" runat="server" Value='<%# Eval("VCHSanctionOrderdOC") %>' />
                                                                                        <asp:HyperLink runat="server" ID="hypvchIPRRegistrationFile" NavigateUrl='<%# "~/incentives/Files/TermLoan/"+Eval("VCHSanctionOrderdOC")  %>'
                                                                                            Target="_blank" CssClass="btn btn-primary btn-sm"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="15%" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <label class="col-sm-3">
                                                                    Amount of interest paid on term loan from the date</label>
                                                                <div class="col-sm-3 ">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="txtReinursementAmount1" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <label class="col-sm-3">
                                                                    Claim for interest subsidy</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="txtReinursementAmount2" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <label class="col-sm-3">
                                                                    Deferential amount of benefit claimed</label>
                                                                <div class="col-sm-3 ">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="txtReinursementAmount3" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <label class="col-sm-3">
                                                                    Amount of claim for Reimbursement of Guarantee Fee under CGTMSE Scheme</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="txtReinursementAmount4" runat="server" CssClass="form-control-static"></asp:Label>
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
                                                                <%-- <div >--%>
                                                                <tr runat="server" id="divAuthorizing" visible="false">
                                                                    <td>
                                                                        Authorizing letter signed by Authorized Signatory
                                                                        <asp:Label ID="Lbl_Org_Doc_Type" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="hypAUTHORIZEDFILE" runat="server" Target="_blank" ToolTip="View File">View Document
                                                                        </asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <%-- </div>--%>
                                                                <tr>
                                                                    <td>
                                                                        Certificate of incorporation
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="Hyp_View_Org_Doc" runat="server" Target="_blank">View Document
                                                                        </asp:HyperLink>
                                                                    </td>
                                                                </tr>
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
                                                                <tr runat="server" id="availgroup2">
                                                                    <td>
                                                                        Term Loan Sanction Order Containing Repayment Schedule
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="hypTermLoan" runat="server" Target="_blank">View Document</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="availgroup11">
                                                                    <td>
                                                                        Bank Statement
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="hypBankStatement" runat="server" Target="_blank">View Document</asp:HyperLink>
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
                                                                <asp:Label ID="lblAplicantName1" runat="server"></asp:Label>
                                                                <%--at present--%>
                                                                <asp:Label ID="lblAddress1" runat="server" Visible="false"></asp:Label>
                                                                of M/S
                                                                <asp:Label ID="Label10" runat="server" Text="Apparel"></asp:Label>
                                                                (name of the industrial unit) certify that the information furnished as above is
                                                                true and correct to the best of my knowledge and belief.</p>
                                                            <p>
                                                                I hereby undertake to abide by the terms and conditions prescribed under the provisions
                                                                of Industrial Policy Resolution 2015 and its operational guidelines.
                                                            </p>
                                                            <p>
                                                                I hereby undertake to repay the interest subsidy or any part thereof with penal
                                                                interest as decided by the authority.
                                                            </p>
                                                            <ol>
                                                                <li>If the information furnished is found to be false / incorrect i misleading or misrepresented
                                                                    and there has been suppression of facts / materials or disbursed in excess of the
                                                                    amount actually admissible for whatsoever reason.</li>
                                                                <li>If the industrial unit goes out of production for a period exceeding six months
                                                                    at a time for any reasons other than labor troubles, want of electric power or for
                                                                    the reason which is beyond the control of entrepreneur / management during the period
                                                                    of incentives.</li>
                                                            </ol>
                                                            <p>
                                                                I hereby certify that I /We the concerned promote(s) have not defaulted to OSFC
                                                                / IPCOL / SIDBI / Banks / Public Financial institutions / other Government agencies
                                                                in connection with the unit for which the incentive is sought or for any other unit
                                                                / activity in the state with which concerned promote(s) is / are directly or indirectly
                                                                associated.</p>
                                                            <p>
                                                                I hereby certify that this industrial unit has not been classified as a NPA at the
                                                                time of making the application. I hereby certify that this industrial unit has not
                                                                applied / applied / availed / not availed interest subsidy under any other scheme
                                                                of the state Govt. or the central Govt. or Govt. Agencies any Financial Institution’s.</p>
                                                            <p>
                                                                I hereby certify that this industrial unit has not been classified as a NPA</p>
                                                            <p>
                                                                I hereby certify that this industrial unit has not been classified as a NPA I hereby
                                                                undertake to furnish its audited financial statements and other periodical statements
                                                                of each financial year to the RIC I DIC I IPICOL / Directorate of Industries, Odisha
                                                                during the period of incentives.</p>
                                                            <p>
                                                                Copies of relevant documents in support of information / facts furnished above are
                                                                self-attested and enclosed herewith.
                                                            </p>
                                                            <div class="col-sm-2 ">
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <div class="form-group">
                                                                    <div class="row">
                                                                        <label class="col-sm-6">
                                                                        </label>
                                                                        <div class="col-sm-6">
                                                                            <img runat="server" id="PreviewImage" height="70" width="140" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="row" id="divUpload">
                                                                        <label class="col-sm-6">
                                                                            <asp:Label runat="server" ID="lblAuthorizing1" Visible="false"></asp:Label>
                                                                            Upload Signature of the Applicant in full and behalf of M/ s
                                                                            <asp:Label ID="Label101" runat="server"></asp:Label></label>
                                                                        <div class="col-sm-6">
                                                                            <asp:FileUpload CssClass="form-control" ID="flSignature" runat="server" onchange="readURL(this);" />
                                                                            <small class="text-danger">(.png, .jpeg, .jpg file only and Max file size 4 MB)</small></div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="row">
                                                                        <label class="col-sm-6">
                                                                            Date:
                                                                        </label>
                                                                        <div class="col-sm-6">
                                                                            <b>
                                                                                <asp:Label ID="lblcurdt" runat="server"></asp:Label></b>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:HiddenField ID="HdnValueFlag" runat="server" />
                                                            <asp:HiddenField ID="hdnId" runat="server" />
                                                            <div class="clearfix">
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
                                                <asp:HiddenField ID="hdnEmail" runat="server" />
                                                <asp:HiddenField ID="hdnMobile" runat="server" />
                                            </div>
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
                                                <%--<asp:Button ID="btnDraft" runat="server" Text="Save as Draft" CssClass="btn btn-warning" />
                                        <a class=""></a>--%>
                                                <%--<a class="btn btn-success" data-toggle="modal"
                                            data-target="#myModal2">Apply</a>--%>
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
        </div>
    </div>
    <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

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
