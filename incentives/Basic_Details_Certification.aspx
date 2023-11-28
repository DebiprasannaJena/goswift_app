<%--'*******************************************************************************************************************
' File Name         : Basic_Details_Certification.aspx
' Description       : Common Profile Details for Applying Provisional Priority,Priority and Pioneer Certificate
' Created by        : Sushant Kumar Jena
' Created On        : 8th Nov 2017
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Basic_Details_Certification.aspx.cs"
    Inherits="incentives_Basic_Details_Certification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/incentive.css" rel="stylesheet" type="text/css">
    <script src="../js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script src="../js/Incentive/JS_Inct_Basic_Details.js" type="text/javascript"></script>
    <style type="text/css">
        .fieldinfo-left
        {
            float: left;
            margin-right: 7px;
            left: 10px;
            font-size: 17px;
            margin-top: -23px;
            color: #337ab7;
            position: relative;
            z-index: 2;
        }
        .listdiv ol
        {
            margin-left: 20px;
        }
        .listdiv ol li
        {
            font-size: 13px;
            line-height: 22px;
        }
    </style>
    <script>

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });

            var $activePanelHeading = $('.panel-group .panel .panel-collapse.in').prev().addClass('active');  //add class="active" to panel-heading div above the "collapse in" (open) div
            $activePanelHeading.find('a').prepend('<span class="fa fa-minus"></span> ');  //put the minus-sign inside of the "a" tag
            $('.panel-group .panel-heading').not($activePanelHeading).find('a').prepend('<span class="fa fa-plus"></span> ');  //if it's not active, it will put a plus-sign inside of the "a" tag
            $('.panel-group').on('show.bs.collapse', function (e) {  //event fires when "show" instance is called
                //$('.panel-group .panel-heading.active').removeClass('active').find('.fa').toggleClass('fa-plus fa-minus'); - removed so multiple can be open and have minus sign
                $(e.target).prev().addClass('active').find('.fa').toggleClass('fa-plus fa-minus');
            });
            $('.panel-group').on('hide.bs.collapse', function (e) {  //event fires when "hide" method is called
                $(e.target).prev().removeClass('active').find('.fa').removeClass('fa-minus').addClass('fa-plus');
            });

        });

    </script>
    <script type="text/javascript" language="javascript">

        function alertredirect(msg) {
            jAlert(msg, projname, function (r) {
                if (r) {

                    location.href = 'incentiveoffered.aspx?';
                    return true;
                }
                else {
                    return false;
                }
            });
        }

        /////////////////// jquery method for Industrial Unit////////////////////////////////////////

        function openpopup(flu) {
            var i = flu.id;
            $("#" + i).click();
            return false;
        }

        function SameAddressIndustry() {
            var cc = $('#Txt_Industry_Address').val();
            if ($("#ChkSameData").is(':checked')) {
                $('#Txt_Regd_Office_Address').val(cc);
            }
        }

        /////////////////// jquery method for Industrial Unit////////////////////////////////////////
    </script>
    <style>
        .unitdtl .groupmastreportlet2 .portletdivider
        {
            width: 20%;
        }
        .groupmastreportlet2 .portletdivider span
        {
            font-size: 20px;
            margin-left: 3px;
        }
        ul ol
        {
            margin-left: 35px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <uc2:header ID="header" runat="server" />
                <div class="registration-div investors-bg">
                    <div id="exTab1" class="">
                        <div class="investrs-tab">
                            <uc4:investoemenu ID="ineste" runat="server" />
                        </div>
                        <div class="tab-content clearfix">
                            <div class="tab-pane active" id="1a">
                                <div class="form-sec">
                                    <div class="innertabs  m-b-10">
                                        <ul class="nav nav-pills pull-right">
                                            <li><a href="incentiveoffered.aspx" title="Click Here to View Incentive Offered !!">
                                                Incentive Offered</a></li>
                                            <%-- <li class="active"><a href="Basic_Details.aspx">Apply For Incentive</a></li>--%>
                                            <li><a href="ViewApplicationStatus.aspx" title="Click Here to View Application Status !!">
                                                View Application Status</a></li>
                                        </ul>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-header">
                                        <%--<div class="iconsdiv">
                                                <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                                    <i class="fa fa-print"></i></a><a href="javascript:void(0);" title="Export to Excel"
                                                        id="A1" class="pull-right printbtn bg-blue border-blue"><i class="fa fa-file-excel-o">
                                                        </i></a>
                                            </div>--%>
                                        <h2>
                                            BASIC UNIT DETAILS</h2>
                                    </div>
                                    <%--  <div class="details-section m-t-20">
                                        <div class="masterportletsec unitdtl">
                                            <h2>
                                                Application History</h2>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="masterportlet">
                                                        <div class="fontsec">
                                                            <h4>
                                                                Total <small>Applications</small></h4>
                                                        </div>
                                                        <div id="divApp1" class="countsec">
                                                            <span class="counter "><a id="TagTotalApp" runat="server" href="#" title="Total Application">
                                                                <asp:Label ID="Lbl_Total_App" runat="server"></asp:Label>
                                                            </a></span>
                                                        </div>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-9">
                                                    <div class=" groupmastreportlet2">
                                                        <h4>
                                                            Applied Incentives</h4>
                                                        <div class="portletdivider colm3">
                                                            <div class="fontsec">
                                                                <a id="TagDraft" runat="server" href="#" title="Drafted">Drafted Application <span
                                                                    class="counter">
                                                                    <asp:Label ID="Lbl_Drafted_App" runat="server"></asp:Label></span></a>
                                                            </div>
                                                        </div>
                                                        <div class="portletdivider colm3">
                                                            <div class="fontsec">
                                                                <a id="TagApproved" runat="server" href="#" title="Approved">Approved <span iclass="counter">
                                                                    <asp:Label ID="Lbl_Approved" runat="server"></asp:Label></span> </a>
                                                            </div>
                                                        </div>
                                                        <div class="portletdivider colm3">
                                                            <div class="fontsec">
                                                                <a id="TagScrutiny" runat="server" href="#" title="Scrutiny in Progress">Scrutiny in
                                                                    Progress <span class="counter">
                                                                        <asp:Label ID="Lbl_Scrutiny" runat="server"></asp:Label></span></a>
                                                            </div>
                                                        </div>
                                                        <div class="portletdivider colm3">
                                                            <div class="fontsec">
                                                                <a id="TagRejected" runat="server" href="#" title="Rejected">Rejected <span class="counter">
                                                                    <asp:Label ID="Lbl_Rejected" runat="server"></asp:Label></span></a>
                                                            </div>
                                                        </div>
                                                        <div class="portletdivider colm3  borderright0">
                                                            <div class="fontsec">
                                                                <a id="TagDisbursed" runat="server" href="#" title="Disbursed">Disbursed <span class="counter">
                                                                    <asp:Label ID="Lbl_Disbursed" runat="server"></asp:Label></span></a>
                                                            </div>
                                                        </div>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="incentivesec">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="details-section leftcolm">
                                                    <div class="panel-group p-t-20" id="accordion" role="tablist" aria-multiselectable="true">
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading" role="tab" id="headingOne">
                                                                <h4 class="panel-title">
                                                                    <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"
                                                                        aria-expanded="true" aria-controls="collapseOne"><span class="text-red pull-right "
                                                                            style="margin-right: 20px;">* All fields in this section are mandatory</span>Industrial
                                                                        Unit's Details </a>
                                                                </h4>
                                                            </div>
                                                            <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne"
                                                                runat="server">
                                                                <div class="panel-body">
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                                Name of Enterprise/Industrial Unit</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span><asp:TextBox ID="Txt_EnterPrise_Name" CssClass="form-control"
                                                                                    runat="server" MaxLength="100"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                                Organization Type</label>
                                                                            <div class="col-sm-8 margin-bottom10">
                                                                                <span class="colon">:</span>
                                                                                <asp:DropDownList ID="DrpDwn_Org_Type" CssClass="form-control" runat="server" OnSelectedIndexChanged="DrpDwn_Org_Type_SelectedIndexChanged"
                                                                                    AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4 ">
                                                                                Address of Industrial Unit</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <asp:TextBox ID="Txt_Industry_Address" CssClass="form-control" MaxLength="100" TextMode="MultiLine"
                                                                                    runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4 ">
                                                                                Unit Category</label>
                                                                            <div class="col-sm-8 margin-bottom10">
                                                                                <span class="colon">:</span>
                                                                                <asp:DropDownList ID="DrpDwn_Unit_Cat" CssClass="form-control" runat="server">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4 ">
                                                                                Unit Type</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <asp:DropDownList ID="DrpDwn_Unit_Type" CssClass="form-control" runat="server" OnSelectedIndexChanged="DrpDwn_Unit_Type_SelectedIndexChanged"
                                                                                    AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group" id="Div_Unit_Type_Doc" runat="server">
                                                                        <div class="row">
                                                                            <label class="col-sm-4">
                                                                                <asp:Label ID="Lbl_Unit_Type_Doc_Name" runat="server"></asp:Label>
                                                                                <asp:HiddenField ID="Hid_Unit_Type_Doc_Code" runat="server" />
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <div class="input-group">
                                                                                    <asp:FileUpload ID="FU_Unit_Type" CssClass="form-control" runat="server" />
                                                                                    <asp:HiddenField ID="Hid_Unit_Type_File_Name" runat="server" />
                                                                                    <asp:LinkButton ID="LnkBtn_Upload_Unit_Type_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                        OnClick="LnkBtn_Add_Doc_Click" ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                    <asp:LinkButton ID="LnkBtn_Delete_Unit_Type_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                        OnClick="LnkBtn_Delete_Doc_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                    <asp:HyperLink ID="Hyp_View_Unit_Type_Doc" runat="server" Target="_blank" Visible="false"
                                                                                        CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                </div>
                                                                                <asp:Label ID="Lbl_Msg_Unit_Type_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                                    Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                                Address of Registered Office of the Industrial Unit</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <asp:CheckBox ID="ChkSameData" runat="server" Text="Same as Address of Industrial Unit"
                                                                                    onclick="return SameAddressIndustry();" />
                                                                                <asp:TextBox ID="Txt_Regd_Office_Address" MaxLength="500" CssClass="form-control"
                                                                                    TextMode="MultiLine" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                                <asp:Label ID="Lbl_Org_Name_Type" runat="server" Text="Name of Managing Partner"></asp:Label>
                                                                            </label>
                                                                            <div class="col-sm-1" style="padding-right: 0px">
                                                                                <span class="colon">:</span><asp:DropDownList CssClass="form-control" ID="DrpDwn_Gender_Partner"
                                                                                    runat="server">
                                                                                    <asp:ListItem Value="1">Mr.</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Ms.</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-sm-7">
                                                                                <asp:TextBox ID="Txt_Partner_Name" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label class="col-sm-4">
                                                                                <asp:Label ID="Lbl_Org_Doc_Type" runat="server" Text="Document in Support of Managing Partner"></asp:Label>
                                                                                <asp:HiddenField ID="Hid_Org_Doc_Type" runat="server" />
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <div class="input-group">
                                                                                    <asp:FileUpload ID="FU_Org_Doc" CssClass="form-control" runat="server" />
                                                                                    <asp:HiddenField ID="Hid_Org_Doc_File_Name" runat="server" />
                                                                                    <asp:LinkButton ID="LnkBtn_Upload_Org_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                        OnClick="LnkBtn_Add_Doc_Click" ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                    <asp:LinkButton ID="LnkBtn_Delete_Org_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                        OnClick="LnkBtn_Delete_Doc_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                    <asp:HyperLink ID="Hyp_View_Org_Doc" runat="server" Target="_blank" Visible="false"
                                                                                        CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                                </div>
                                                                                <asp:Label ID="Lbl_Msg_Org_Doc" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                                    runat="server" Text="Document Uploaded successfully"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                                EIN/ IEM/ IL No.</label>
                                                                            <div class="col-sm-8 margin-bottom10">
                                                                                <span class="colon">:</span>
                                                                                <asp:TextBox ID="Txt_EIN_IL_NO" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                                EIN/ IEM/ IL Date</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <div class="input-group  date datePicker" id="Div_Date_EIN" runat="server">
                                                                                    <asp:TextBox ID="Txt_EIN_IL_Date" CssClass="form-control" type="text" runat="server"></asp:TextBox>
                                                                                    <span id="Span_Date_EIN" runat="server" class="input-group-addon"><i class="fa fa-calendar">
                                                                                    </i></span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div id="Div_Prod_Comm_Before" runat="server">
                                                                        <div id="Div1" runat="server" class="form-group">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    PC No. Before E/M/D</label>
                                                                                <div class="col-sm-8 margin-bottom10">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_PC_No_Before" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                                                                    <asp:HiddenField ID="Hid_PC_App_No_Before" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    Date of Production Commencement- Before E/M/D
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group  date datePicker" id="Div_Date_Prod_Before" runat="server">
                                                                                        <asp:TextBox ID="Txt_Prod_Comm_Date_Before" runat="server" class="form-control" MaxLength="30" />
                                                                                        <span id="Span_Date_Prod_Before" class="input-group-addon" runat="server"><i class="fa fa-calendar">
                                                                                        </i></span>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    PC Issuance Date Before E/M/D</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group  date datePicker" id="Div_Date_PC_Before" runat="server">
                                                                                        <asp:TextBox ID="Txt_PC_Issue_Date_Before" CssClass="form-control" type="text" runat="server"></asp:TextBox>
                                                                                        <span id="Span_Date_PC_Before" class="input-group-addon" runat="server"><i class="fa fa-calendar">
                                                                                        </i></span>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-4">
                                                                                    <asp:Label ID="Lbl_Prod_Comm_Before_Doc_Name" runat="server" Text="Certificate on Date of Commencement of Production"></asp:Label>
                                                                                    <asp:HiddenField ID="Hid_Prod_Comm_Before_Doc_Code" runat="server" />
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <%-- <div class="input-group">
                                                                                           <asp:FileUpload ID="FU_Prod_Comm_Before" CssClass="form-control" runat="server" />
                                                                                            <asp:HiddenField ID="Hid_Prod_Comm_Before_File_Name" runat="server" />
                                                                                            <asp:LinkButton ID="LnkBtn_Upload_Prod_Comm_Before_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                                OnClick="LnkBtn_Add_Doc_Click" ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="LnkBtn_Delete_Prod_Comm_Before_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                                OnClick="LnkBtn_Delete_Doc_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                            <asp:HyperLink ID="Hyp_View_Prod_Comm_Before_Doc" runat="server" Target="_blank"
                                                                                                Visible="false" CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>--%>
                                                                                    <asp:LinkButton ID="LnkBtn_View_Prod_Comm_Before_Doc" runat="server" CssClass="btn btn-info"
                                                                                        Visible="false" OnClick="LnkBtn_View_Prod_Comm_Before_Doc_Click" ToolTip="Click to View"><i class="fa fa-download"></i></asp:LinkButton>
                                                                                    <%--</div>--%>
                                                                                    <asp:Label ID="Lbl_Msg_Prod_Comm_Before_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div id="Div_Prod_Comm_After" runat="server">
                                                                        <div id="Div2" runat="server" class="form-group">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    PC No.
                                                                                    <asp:Label ID="Lbl_Text_PC_No" runat="server"></asp:Label></label>
                                                                                <div class="col-sm-8 margin-bottom10">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_PC_No_After" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                                                                    <asp:HiddenField ID="Hid_PC_App_No_After" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    Date of Production Commencement
                                                                                    <asp:Label ID="Lbl_Text_Prod_Comm" runat="server"></asp:Label>
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group  date datePicker" id="Div_Date_Prod_After" runat="server">
                                                                                        <asp:TextBox ID="Txt_Prod_Comm_Date_After" runat="server" class="form-control" MaxLength="30" />
                                                                                        <span id="Span_Date_Prod_After" class="input-group-addon" runat="server"><i class="fa fa-calendar">
                                                                                        </i></span>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    PC Issuance Date
                                                                                    <asp:Label ID="Lbl_Text_PC_Issue_Date" runat="server"></asp:Label>
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group  date datePicker" id="Div_Date_PC_After" runat="server">
                                                                                        <asp:TextBox ID="Txt_PC_Issue_Date_After" CssClass="form-control" type="text" runat="server"></asp:TextBox>
                                                                                        <span class="input-group-addon" id="Span_Date_PC_After" runat="server"><i class="fa fa-calendar">
                                                                                        </i></span>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-4">
                                                                                    <asp:Label ID="Lbl_Prod_Comm_After_Doc_Name" runat="server" Text="Certificate on Date of Commencement of Production"></asp:Label>
                                                                                    <asp:HiddenField ID="Hid_Prod_Comm_After_Doc_Code" runat="server" />
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <%--  <div class="input-group">
                                                                                            <asp:FileUpload ID="FU_Prod_Comm_After" CssClass="form-control" runat="server" />
                                                                                               <asp:HiddenField ID="Hid_Prod_Comm_After_File_Name" runat="server" />
                                                                                                 <asp:LinkButton ID="LnkBtn_Upload_Prod_Comm_After_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                                OnClick="LnkBtn_Add_Doc_Click" ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                                   <asp:LinkButton ID="LnkBtn_Delete_Prod_Comm_After_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                                OnClick="LnkBtn_Delete_Doc_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                                     <asp:HyperLink ID="Hyp_View_Prod_Comm_After_Doc" runat="server" Target="_blank" Visible="false"
                                                                                                CssClass="input-group-addon bg-blue" ToolTip="Click to View"><i class="fa fa-download"></i></asp:HyperLink>--%>
                                                                                    <asp:LinkButton ID="LnkBtn_View_Prod_Comm_After_Doc" runat="server" CssClass="btn btn-info"
                                                                                        Visible="false" OnClick="LnkBtn_View_Prod_Comm_After_Doc_Click" ToolTip="Click to View"><i class="fa fa-download"></i></asp:LinkButton>
                                                                                    <%--</div>--%>
                                                                                    <asp:Label ID="Lbl_Msg_Prod_Comm_After_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label class="col-sm-4">
                                                                                District</label>
                                                                            <div class="col-sm-8 margin-bottom10">
                                                                                <span class="colon">:</span>
                                                                                <asp:DropDownList ID="DrpDwn_District" runat="server" CssClass="form-control">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label class="col-sm-4">
                                                                                Sector</label>
                                                                            <div class="col-sm-8 margin-bottom10">
                                                                                <span class="colon">:</span>
                                                                                <asp:DropDownList ID="DrpDwn_Sector" runat="server" CssClass="form-control" OnSelectedIndexChanged="DrpDwn_Sector_SelectedIndexChanged"
                                                                                    AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label class="col-sm-4">
                                                                                Sub Sector</label>
                                                                            <div class="col-sm-8 margin-bottom10">
                                                                                <span class="colon">:</span>
                                                                                <asp:DropDownList ID="DrpDwn_Sub_Sector" runat="server" CssClass="form-control" AutoPostBack="true"
                                                                                    OnSelectedIndexChanged="DrpDwn_Sub_Sector_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                                Nature of Activity
                                                                            </label>
                                                                            <div class="col-sm-8 margin-bottom10">
                                                                                <span class="colon">:</span>
                                                                                <asp:RadioButtonList ID="Rad_Nature_Of_Activity" runat="server" CssClass="radio-inline"
                                                                                    RepeatLayout="Flow" RepeatDirection="Horizontal">
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                                Whether in Priority Sector Under IPR-2015
                                                                            </label>
                                                                            <div class="col-sm-2 margin-bottom10">
                                                                                <span class="colon">:</span>
                                                                                <asp:RadioButtonList ID="Rad_Priority_User" runat="server" CssClass="radio-inline"
                                                                                    RepeatLayout="Flow" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="Rad_Priority_User_SelectedIndexChanged">
                                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <asp:Label ID="Lbl_Priority_Msg" runat="server" CssClass="text-danger" Text="You may apply for Priority Sector Certificate in order to avail incentives of IPR 2015 under priority sector."></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                                Priority Sector Status Granted?
                                                                            </label>
                                                                            <div class="col-sm-8 margin-bottom10">
                                                                                <span class="colon">:</span>
                                                                                <asp:RadioButtonList ID="Rad_Is_Priority" class="optradioPriority" runat="server"
                                                                                    CssClass="radio-inline" RepeatLayout="Flow" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                                    OnSelectedIndexChanged="Rad_Is_Priority_SelectedIndexChanged">
                                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                                                    <asp:ListItem Value="3">Provisional</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label class="col-sm-4">
                                                                                Priority Sector Name as in IPR-2015</label>
                                                                            <div class="col-sm-8 margin-bottom10">
                                                                                <span class="colon">:</span>
                                                                                <asp:Label ID="Lbl_Derived_Sector" runat="server" CssClass="form-control-static"></asp:Label>
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div id="Div_Pioneer" runat="server">
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    Is Pioneer</label>
                                                                                <div class="col-sm-8 margin-bottom10">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:RadioButtonList ID="Rad_Is_Pioneer" runat="server" RepeatDirection="Horizontal"
                                                                                        CssClass="radio-inline">
                                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                        <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-4">
                                                                                    <asp:Label ID="Lbl_Pioneer_Doc_Name" runat="server" Text=""></asp:Label>
                                                                                    <asp:HiddenField ID="Hid_Pioneer_Doc_Code" runat="server" />
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FU_Pioneer_Doc" CssClass="form-control" runat="server" />
                                                                                        <asp:HiddenField ID="Hid_Pioneer_Doc_File_Name" runat="server" />
                                                                                        <asp:LinkButton ID="LnkBtn_Upload_Pioneer_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                            OnClick="LnkBtn_Add_Doc_Click" ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkBtn_Delete_Pioneer_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                            OnClick="LnkBtn_Delete_Doc_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="Hyp_View_Pioneer_Doc" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <asp:Label ID="Lbl_Msg_Pioneer_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label class="col-sm-4">
                                                                                Has Sectoral Policy</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <asp:CheckBox ID="ChkBx_Sectoral" runat="server" Enabled="false" />
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label class="col-sm-4">
                                                                                GSTIN</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <asp:TextBox ID="Txt_GSTIN" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                                Ancillary/DownStream</label>
                                                                            <div class="col-sm-8 margin-bottom10">
                                                                                <span class="colon">:</span>
                                                                                <asp:RadioButtonList ID="Rad_Is_Ancillary" runat="server" RepeatDirection="Horizontal"
                                                                                    CssClass="radio-inline" OnSelectedIndexChanged="Rad_Is_Ancillary_SelectedIndexChanged"
                                                                                    AutoPostBack="true">
                                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group" id="Div_Ancillary" runat="server">
                                                                        <div class="row">
                                                                            <label class="col-sm-4">
                                                                                <asp:Label ID="Lbl_Ancillary_Doc_Name" runat="server" Text=""></asp:Label>
                                                                                <asp:HiddenField ID="Hid_Ancillary_Doc_Code" runat="server" />
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <div class="input-group">
                                                                                    <asp:FileUpload ID="FU_Ancillary_Doc" CssClass="form-control" runat="server" />
                                                                                    <asp:HiddenField ID="Hid_Ancillary_Doc_File_Name" runat="server" />
                                                                                    <asp:LinkButton ID="LnkBtn_Upload_Ancillary_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                        OnClick="LnkBtn_Add_Doc_Click" ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                    <asp:LinkButton ID="LnkBtn_Delete_Ancillary_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                        OnClick="LnkBtn_Delete_Doc_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                    <asp:HyperLink ID="Hyp_View_Ancillary_Doc" runat="server" Target="_blank" Visible="false"
                                                                                        CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                </div>
                                                                                <asp:Label ID="Lbl_Msg_Ancillary_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                                    Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>
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
                                                                        Production & Employment Details </a>
                                                                </h4>
                                                            </div>
                                                            <div id="ProductionEmploymentDetails" class="panel-collapse collapse " role="tabpanel"
                                                                aria-labelledby="headingThree">
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div class="panel-body">
                                                                            <p class="text-red text-right">
                                                                                All Amounts to be Entered in INR Lakhs</p>
                                                                            <div id="Div_Prod_Emp_Before" runat="server">
                                                                                <h4>
                                                                                    Before E/M/D</h4>
                                                                                <div class="form-group">
                                                                                    <div class="row">
                                                                                        <label for="Iname" class="col-sm-12">
                                                                                            Items of Manufacture/Activity
                                                                                        </label>
                                                                                        <div class="col-sm-12  margin-bottom10">
                                                                                            <table class="table table-bordered">
                                                                                                <tr>
                                                                                                    <th width="5%">
                                                                                                        SlNo
                                                                                                    </th>
                                                                                                    <th>
                                                                                                        Product/Service Name
                                                                                                    </th>
                                                                                                    <th width="20%">
                                                                                                        Quantity
                                                                                                    </th>
                                                                                                    <th width="12%">
                                                                                                        Units
                                                                                                    </th>
                                                                                                    <th width="20%">
                                                                                                        Value
                                                                                                    </th>
                                                                                                    <th width="5%">
                                                                                                        Action
                                                                                                    </th>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="Txt_Product_Name_Before" runat="server" CssClass="form-control"
                                                                                                            MaxLength="100"></asp:TextBox>
                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="Txt_Product_Name_Before"
                                                                                                            FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" ">
                                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="Txt_Quantity_Before" runat="server" CssClass="form-control" MaxLength="15"
                                                                                                            onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="Txt_Quantity_Before"
                                                                                                            FilterType="Numbers,Custom" ValidChars=".">
                                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="DrpDwn_Unit_Before" runat="server" CssClass="form-control"
                                                                                                            OnSelectedIndexChanged="DrpDwn_Unit_Before_SelectedIndexChanged" AutoPostBack="true">
                                                                                                        </asp:DropDownList>
                                                                                                        <asp:TextBox ID="Txt_Other_Unit_Before" runat="server" CssClass="form-control" placeholder="Enter Other Unit"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="Txt_Value_Before" runat="server" CssClass="form-control" MaxLength="11"
                                                                                                            onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="Txt_Value_Before"
                                                                                                            FilterType="Numbers,Custom" ValidChars=".">
                                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:LinkButton ID="LnkBtn_Add_Item_Before" CssClass="btn btn-success btn-sm" runat="server"
                                                                                                            OnClick="LnkBtn_Add_Item_Before_Click" OnClientClick="return validateItemAddBefore();"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="6">
                                                                                                        <asp:GridView ID="Grd_Production_Before" runat="server" CssClass="table table-bordered"
                                                                                                            DataKeyNames="vchProductName" AutoGenerateColumns="false" ShowHeader="false">
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField>
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="Lbl_Sl_No_Product_Before" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField>
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="Lbl_Product_Name_Before" runat="server" Text='<%# Eval("vchProductName") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField>
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="Lbl_Quantity_Before" runat="server" Text='<%# Eval("intQuantity") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle Width="20%"></ItemStyle>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField>
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="Lbl_Unit_Before" runat="server" Text='<%# Eval("vchUnit") %>'></asp:Label>
                                                                                                                        <asp:HiddenField ID="Hid_Unit_Before" runat="server" Value='<%# Eval("intUnit") %>' />
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle Width="12%"></ItemStyle>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField>
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="Lbl_Other_Unit_Before" runat="server" Text='<%# Eval("vchOtherUnit") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle Width="12%"></ItemStyle>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField>
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="Lbl_Value_Before" runat="server" Text='<%# Eval("decValue") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle Width="20%"></ItemStyle>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField>
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:ImageButton ID="ImgBtn_Delete_Before" runat="server" ImageUrl="~/Portal/images/deleteIcon.png"
                                                                                                                            CommandArgument='<%# Container.DataItemIndex %>' OnClick="ImgBtn_Delete_Before_Click" />
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                                                                </asp:TemplateField>
                                                                                                            </Columns>
                                                                                                        </asp:GridView>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="row">
                                                                                        <label for="Iname" class="col-sm-3 ">
                                                                                            Direct Employment in Numbers<small>(On Company Payroll)</small><%--<span class="text-red">*</span>--%></label><div
                                                                                                class="col-sm-3">
                                                                                                <span class="colon">:</span>
                                                                                                <asp:TextBox ID="Txt_Direct_Emp_Before" runat="server" CssClass="form-control" Text="0"
                                                                                                    Onkeypress="return numeralsOnly(event, this);" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="Txt_Direct_Emp_Before"
                                                                                                    FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789" />
                                                                                                <a href="#" data-toggle="tooltip" class="fieldinfo" title="These are the employee of the company/industry without any contract agreement ">
                                                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                                            </div>
                                                                                        <label for="Iname" class="col-sm-3 ">
                                                                                            Contractual Employment in Numbers</label>
                                                                                        <div class="col-sm-3">
                                                                                            <span class="colon">:</span>
                                                                                            <asp:TextBox ID="Txt_Contract_Emp_Before" runat="server" CssClass="form-control"
                                                                                                Text="0" Onkeypress="return numeralsOnly(event, this);" AutoCompleteType="None"
                                                                                                autocomplete="off"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="Txt_Contract_Emp_Before"
                                                                                                FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="row">
                                                                                        <label class="col-sm-4">
                                                                                            <asp:Label ID="Lbl_Direct_Emp_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                                                            <asp:HiddenField ID="Hid_Direct_Emp_Before_Doc_Code" runat="server" />
                                                                                        </label>
                                                                                        <div class="col-sm-8">
                                                                                            <span class="colon">:</span>
                                                                                            <div class="input-group">
                                                                                                <asp:FileUpload ID="FU_Direct_Emp_Before" CssClass="form-control" runat="server" />
                                                                                                <asp:HiddenField ID="Hid_Direct_Emp_Before_File_Name" runat="server" />
                                                                                                <asp:LinkButton ID="LnkBtn_Upload_Direct_Emp_Before_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                                    OnClick="LnkBtn_Add_Doc_Click" ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="LnkBtn_Delete_Direct_Emp_Before_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                                    OnClick="LnkBtn_Delete_Doc_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                                <asp:HyperLink ID="Hyp_View_Direct_Emp_Before_Doc" runat="server" Target="_blank"
                                                                                                    Visible="false" CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                            </div>
                                                                                            <asp:Label ID="Lbl_Msg_Direct_Emp_Before_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                                                Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>
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
                                                                                                    <asp:TextBox ID="Txt_Managarial_Before" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                        MaxLength="4" Text="0" onkeyup="return funCalTotalEmp_Before();" AutoCompleteType="None"
                                                                                                        autocomplete="off"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 20%;">
                                                                                                    General
                                                                                                </td>
                                                                                                <td style="width: 15%;">
                                                                                                    <asp:TextBox ID="Txt_General_Before" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                        MaxLength="4" Text="0" onkeyup="return funCalTotalEmp_Before();" AutoCompleteType="None"
                                                                                                        autocomplete="off"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Supervisor
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Supervisor_Before" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                        MaxLength="4" Text="0" onkeyup="return funCalTotalEmp_Before();" AutoCompleteType="None"
                                                                                                        autocomplete="off"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    SC
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_SC_Before" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                        MaxLength="4" Text="0" onkeyup="return funCalTotalEmp_Before();" AutoCompleteType="None"
                                                                                                        autocomplete="off"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Skilled
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Skilled_Before" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                        MaxLength="4" Text="0" onkeyup="return funCalTotalEmp_Before();" AutoCompleteType="None"
                                                                                                        autocomplete="off"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    ST
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_ST_Before" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                        MaxLength="4" Text="0" onkeyup="return funCalTotalEmp_Before();" AutoCompleteType="None"
                                                                                                        autocomplete="off"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Semi Skilled
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Semi_Skilled_Before" runat="server" CssClass="form-control"
                                                                                                        Onkeypress="return numeralsOnly(event, this);" MaxLength="4" Text="0" onkeyup="return funCalTotalEmp_Before();"
                                                                                                        AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    Total
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Total_Cast_Emp_Before" ReadOnly="true" runat="server" CssClass="form-control"
                                                                                                        Text="0" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Un Skilled
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Unskilled_Before" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                        MaxLength="4" Text="0" onkeyup="return funCalTotalEmp_Before();" AutoCompleteType="None"
                                                                                                        autocomplete="off"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    Women
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Women_Before" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                        MaxLength="4" Text="0" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Total
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Total_Emp_Before" ReadOnly="true" runat="server" CssClass="form-control"
                                                                                                        Text="0" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    Differently Abled Persons
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_PHD_Before" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                        MaxLength="4" Text="0" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <%--  <br />--%>
                                                                            <h4>
                                                                                <asp:Label ID="Lbl_Header_Prod_Emp" runat="server"></asp:Label></h4>
                                                                            <div class="form-group">
                                                                                <div class="row">
                                                                                    <label for="Iname" class="col-sm-12">
                                                                                        Items of Manufacture/Activity
                                                                                    </label>
                                                                                    <div class="col-sm-12  margin-bottom10">
                                                                                        <table class="table table-bordered">
                                                                                            <tr>
                                                                                                <th width="5%">
                                                                                                    SlNo
                                                                                                </th>
                                                                                                <th>
                                                                                                    Product/Service Name
                                                                                                </th>
                                                                                                <th width="20%">
                                                                                                    Quantity
                                                                                                </th>
                                                                                                <th width="12%">
                                                                                                    Units
                                                                                                </th>
                                                                                                <th width="20%">
                                                                                                    Value
                                                                                                </th>
                                                                                                <th width="5%">
                                                                                                    Action
                                                                                                </th>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Product_Name_After" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="Txt_Product_Name_After"
                                                                                                        FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" ">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Quantity_After" runat="server" CssClass="form-control" MaxLength="15"
                                                                                                        onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="Txt_Quantity_After"
                                                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="DrpDwn_Unit_After" runat="server" CssClass="form-control" OnSelectedIndexChanged="DrpDwn_Unit_After_SelectedIndexChanged"
                                                                                                        AutoPostBack="true">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:TextBox ID="Txt_Other_Unit_After" runat="server" CssClass="form-control" placeholder="Enter Other Unit"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Value_After" runat="server" CssClass="form-control" MaxLength="11"
                                                                                                        onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" TargetControlID="Txt_Value_After"
                                                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:LinkButton ID="LnkBtn_Add_Item_After" CssClass="btn btn-success btn-sm" runat="server"
                                                                                                        OnClick="LnkBtn_Add_Item_After_Click" OnClientClick="return validateItemAddAfter();"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="6">
                                                                                                    <asp:GridView ID="Grd_Production_After" runat="server" CssClass="table table-bordered"
                                                                                                        DataKeyNames="vchProductName" AutoGenerateColumns="false" ShowHeader="false">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="Lbl_Sl_No_Product_After" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                                <ItemStyle Width="5%"></ItemStyle>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="Lbl_Product_Name_After" runat="server" Text='<%# Eval("vchProductName") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="Lbl_Quantity_After" runat="server" Text='<%# Eval("intQuantity") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                                <ItemStyle Width="20%"></ItemStyle>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="Lbl_Unit_After" runat="server" Text='<%# Eval("vchUnit") %>'></asp:Label>
                                                                                                                    <asp:HiddenField ID="Hid_Unit_After" runat="server" Value='<%# Eval("intUnit") %>' />
                                                                                                                </ItemTemplate>
                                                                                                                <ItemStyle Width="12%"></ItemStyle>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="Lbl_Other_Unit_After" runat="server" Text='<%# Eval("vchOtherUnit") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                                <ItemStyle Width="12%"></ItemStyle>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="Lbl_Value_After" runat="server" Text='<%# Eval("decValue") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                                <ItemStyle Width="20%"></ItemStyle>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:ImageButton ID="ImgBtn_Delete_After" runat="server" ImageUrl="~/Portal/images/deleteIcon.png"
                                                                                                                        CommandArgument='<%# Container.DataItemIndex %>' OnClick="ImgBtn_Delete_After_Click" />
                                                                                                                </ItemTemplate>
                                                                                                                <ItemStyle Width="5%"></ItemStyle>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <div class="row">
                                                                                    <label for="Iname" class="col-sm-3 ">
                                                                                        Direct Employment in Numbers<small>(On Company Payroll)</small><%--<span class="text-red">*</span>--%></label><div
                                                                                            class="col-sm-3">
                                                                                            <span class="colon">:</span>
                                                                                            <asp:TextBox ID="Txt_Direct_Emp_After" runat="server" CssClass="form-control" Text="0"
                                                                                                Onkeypress="return numeralsOnly(event, this);" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="Txt_Direct_Emp_After"
                                                                                                FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789" />
                                                                                            <a href="#" data-toggle="tooltip" class="fieldinfo" title="These are the employee of the company/industry without any contract agreement ">
                                                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                                        </div>
                                                                                    <label for="Iname" class="col-sm-3 ">
                                                                                        Contractual Employment in Numbers</label>
                                                                                    <div class="col-sm-3">
                                                                                        <span class="colon">:</span>
                                                                                        <asp:TextBox ID="Txt_Contract_Emp_After" runat="server" CssClass="form-control" Text="0"
                                                                                            Onkeypress="return numeralsOnly(event, this);" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="Txt_Contract_Emp_After"
                                                                                            FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <div class="row">
                                                                                    <label class="col-sm-4 ">
                                                                                        <asp:Label ID="Lbl_Direct_Emp_After_Doc_Name" runat="server" Text=""></asp:Label>
                                                                                        <asp:HiddenField ID="Hid_Direct_Emp_After_Doc_Code" runat="server" />
                                                                                    </label>
                                                                                    <div class="col-sm-8">
                                                                                        <span class="colon">:</span>
                                                                                        <div class="input-group">
                                                                                            <asp:FileUpload ID="FU_Direct_Emp_After" CssClass="form-control" runat="server" />
                                                                                            <asp:HiddenField ID="Hid_Direct_Emp_After_File_Name" runat="server" />
                                                                                            <asp:LinkButton ID="LnkBtn_Upload_Direct_Emp_After_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                                OnClick="LnkBtn_Add_Doc_Click" ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="LnkBtn_Delete_Direct_Emp_After_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                                OnClick="LnkBtn_Delete_Doc_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                            <asp:HyperLink ID="Hyp_View_Direct_Emp_After_Doc" runat="server" Target="_blank"
                                                                                                Visible="false" CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                        </div>
                                                                                        <asp:Label ID="Lbl_Msg_Direct_Emp_After_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                                            Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>
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
                                                                                                <asp:TextBox ID="Txt_Managarial_After" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                    MaxLength="4" Text="0" onkeyup="return funCalTotalEmp_After();" AutoCompleteType="None"
                                                                                                    autocomplete="off"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%;">
                                                                                                General
                                                                                            </td>
                                                                                            <td style="width: 15%;">
                                                                                                <asp:TextBox ID="Txt_General_After" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                    MaxLength="4" Text="0" onkeyup="return funCalTotalEmp_After();" AutoCompleteType="None"
                                                                                                    autocomplete="off"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                Supervisor
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_Supervisor_After" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                    MaxLength="4" Text="0" onkeyup="return funCalTotalEmp_After();" AutoCompleteType="None"
                                                                                                    autocomplete="off"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                SC
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_SC_After" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                    MaxLength="4" Text="0" onkeyup="return funCalTotalEmp_After();" AutoCompleteType="None"
                                                                                                    autocomplete="off"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                Skilled
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_Skilled_After" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                    MaxLength="4" Text="0" onkeyup="return funCalTotalEmp_After();" AutoCompleteType="None"
                                                                                                    autocomplete="off"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                ST
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_ST_After" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                    MaxLength="4" Text="0" onkeyup="return funCalTotalEmp_After();" AutoCompleteType="None"
                                                                                                    autocomplete="off"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                Semi Skilled
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_Semi_Skilled_After" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                    MaxLength="4" Text="0" onkeyup="return funCalTotalEmp_After();" AutoCompleteType="None"
                                                                                                    autocomplete="off"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                Total
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_Total_Cast_Emp_After" ReadOnly="true" runat="server" CssClass="form-control"
                                                                                                    Text="0"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                Un Skilled
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_Unskilled_After" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                    MaxLength="4" Text="0" onkeyup="return funCalTotalEmp_After();" AutoCompleteType="None"
                                                                                                    autocomplete="off"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                Women
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_Women_After" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                    MaxLength="4" Text="0" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                Total
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_Total_Emp_After" ReadOnly="true" runat="server" CssClass="form-control"
                                                                                                    Text="0"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                Differently Abled Persons
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_PHD_After" runat="server" CssClass="form-control" Onkeypress="return numeralsOnly(event, this);"
                                                                                                    MaxLength="4" Text="0"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
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
                                                                    <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                                        href="#IndustryDetails" aria-expanded="false" aria-controls="collapseThree">Investment
                                                                        Details </a>
                                                                </h4>
                                                            </div>
                                                            <div id="IndustryDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                                                <div class="panel-body">
                                                                    <p class="text-red text-right">
                                                                        All Amounts to be Entered in INR Lakhs</p>
                                                                    <div id="Div_Investment_Before" runat="server">
                                                                        <h4>
                                                                            Before E/M/D</h4>
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    Date of First Fixed Capital Investment (FFCI) (In Land/Building/Plant and Machinery
                                                                                    & Balancing Equipment)
                                                                                </label>
                                                                                <div class="col-sm-7">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group  date datePicker" id="Div_Date_FFCI_Before" runat="server">
                                                                                        <asp:TextBox ID="Txt_FFCI_Date_Before" class="form-control" runat="server"></asp:TextBox>
                                                                                        <span id="Span_Date_FFCI_Before" runat="server" class="input-group-addon"><i class="fa fa-calendar">
                                                                                        </i></span>
                                                                                    </div>
                                                                                    <a href="#" data-toggle="tooltip" class="fieldinfo" title="First Fixed Capital Investment means investment in land or building or plant & machinery or balancing equipment or electrical installations/electrification or other fixed assets (whichever is done first) made after the policy effective date ">
                                                                                        <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-5">
                                                                                    <asp:Label ID="Lbl_FFCI_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                                                    <asp:HiddenField ID="Hid_FFCI_Before_Doc_Code" runat="server" />
                                                                                </label>
                                                                                <div class="col-sm-7">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FU_FFCI_Before" CssClass="form-control" runat="server" />
                                                                                        <asp:HiddenField ID="Hid_FFCI_Before_File_Name" runat="server" />
                                                                                        <asp:LinkButton ID="LnkBtn_Upload_FFCI_Before_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                            OnClick="LnkBtn_Add_Doc_Click" ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkBtn_Delete_FFCI_Before_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                            OnClick="LnkBtn_Delete_Doc_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="Hyp_View_FFCI_Before_Doc" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <asp:Label ID="Lbl_Msg_FFCI_Before_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>
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
                                                                                                    SlNo
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
                                                                                                    <asp:TextBox ID="Txt_Land_Before" CssClass="form-control text-right" runat="server"
                                                                                                        Text="0" onkeyup="return funCalTotalInvestAmtBefore();" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Land_Before" ValidChars="1234567890.">
                                                                                                    </cc1:FilteredTextBoxExtender>
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
                                                                                                    <asp:TextBox ID="Txt_Building_Before" CssClass="form-control text-right" runat="server"
                                                                                                        onkeyup="return funCalTotalInvestAmtBefore();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Building_Before" ValidChars="1234567890.">
                                                                                                    </cc1:FilteredTextBoxExtender>
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
                                                                                                    <asp:TextBox ID="Txt_Plant_Mach_Before" CssClass="form-control text-right" runat="server"
                                                                                                        onkeyup="return funCalTotalInvestAmtBefore();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Plant_Mach_Before" ValidChars="1234567890.">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    4
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label runat="server" ID="lblOtherFixedAssests" Text="Other Fixed Assets"></asp:Label>
                                                                                                </td>
                                                                                                <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Other_Fixed_Asset_Before" CssClass="form-control text-right"
                                                                                                        runat="server" onkeyup="return funCalTotalInvestAmtBefore();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                                    <a href="#" data-toggle="tooltip" class="fieldinfo-left" title="1. A fixed asset is a long-term tangible piece of property that a firm owns and uses in the production of its income and is not expected to be consumed or converted into cash any sooner than at least one year's time.2. Electrification & electrical installations 3. Loading, unloading, transportation, tax & duties paid, erection expenses.4. Other fixed assets of permanent nature ">
                                                                                                        <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Other_Fixed_Asset_Before" ValidChars="1234567890.">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="2">
                                                                                                    <strong>Total</strong>
                                                                                                </td>
                                                                                                <td class="text-right">
                                                                                                    <strong>
                                                                                                        <asp:TextBox ID="Txt_Total_Capital_Before" runat="server" CssClass="form-control text-right"
                                                                                                            Text="0" Enabled="false" onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                                                    </strong>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-4">
                                                                                    <asp:Label ID="Lbl_Approved_DPR_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                                                    <asp:HiddenField ID="Hid_Approved_DPR_Before_Doc_Code" runat="server" />
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FU_Approved_DPR_Before" CssClass="form-control" runat="server" />
                                                                                        <asp:HiddenField ID="Hid_Approved_DPR_Before_File_Name" runat="server" />
                                                                                        <asp:LinkButton ID="LnkBtn_Upload_Approved_DPR_Before_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                            OnClick="LnkBtn_Add_Doc_Click" ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkBtn_Delete_Approved_DPR_Before_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                            OnClick="LnkBtn_Delete_Doc_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="Hyp_View_Approved_DPR_Before_Doc" runat="server" Target="_blank"
                                                                                            Visible="false" CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <asp:Label ID="Lbl_Msg_Approved_DPR_Before_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div>
                                                                        <h4>
                                                                            <asp:Label ID="Lbl_Header_Investment" runat="server"></asp:Label>
                                                                        </h4>
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    Date of First Fixed Capital Investment (FFCI) (In Land/Building/Plant and Machinery
                                                                                    & Balancing Equipment)
                                                                                </label>
                                                                                <div class="col-sm-7">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group  date datePicker" id="Div_Date_FFCI_After" runat="server">
                                                                                        <asp:TextBox ID="Txt_FFCI_Date_After" class="form-control" runat="server"></asp:TextBox>
                                                                                        <span id="Span_Date_FFCI_After" runat="server" class="input-group-addon"><i class="fa fa-calendar">
                                                                                        </i></span>
                                                                                    </div>
                                                                                    <a href="#" data-toggle="tooltip" class="fieldinfo" title="First fixed capital investment means investment in land or building or plant & machinery or balancing equipment or electrical installations/electrification or other fixed assets (whichever is done first) made after the policy effective date ">
                                                                                        <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-5">
                                                                                    <asp:Label ID="Lbl_FFCI_After_Doc_Name" runat="server" Text=""></asp:Label>
                                                                                    <asp:HiddenField ID="Hid_FFCI_After_Doc_Code" runat="server" />
                                                                                </label>
                                                                                <div class="col-sm-7">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FU_FFCI_After" CssClass="form-control" runat="server" />
                                                                                        <asp:HiddenField ID="Hid_FFCI_After_File_Name" runat="server" />
                                                                                        <asp:LinkButton ID="LnkBtn_Upload_FFCI_After_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                            OnClick="LnkBtn_Add_Doc_Click" ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkBtn_Delete_FFCI_After_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                            OnClick="LnkBtn_Delete_Doc_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="Hyp_View_FFCI_After_Doc" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <asp:Label ID="Lbl_Msg_FFCI_After_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>
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
                                                                                                    SlNo
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
                                                                                                    <asp:TextBox ID="Txt_Land_After" CssClass="form-control text-right" runat="server"
                                                                                                        onkeyup="return funCalTotalInvestAmtAfter();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Land_After" ValidChars="1234567890.">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    2
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label runat="server" ID="Label1" Text="Building"></asp:Label>
                                                                                                </td>
                                                                                                <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Building_After" CssClass="form-control text-right" runat="server"
                                                                                                        onkeyup="return funCalTotalInvestAmtAfter();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Building_After" ValidChars="1234567890.">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    3
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label runat="server" ID="Label2" Text="Plant & Machinery"></asp:Label>
                                                                                                </td>
                                                                                                <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Plant_Mach_After" CssClass="form-control text-right" runat="server"
                                                                                                        onkeyup="return funCalTotalInvestAmtAfter();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Plant_Mach_After" ValidChars="1234567890.">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    4
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label runat="server" ID="Label3" Text="Other Fixed Assets"></asp:Label>
                                                                                                </td>
                                                                                                <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Other_Fixed_Asset_After" CssClass="form-control text-right"
                                                                                                        runat="server" onkeyup="return funCalTotalInvestAmtAfter();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                                    <a href="#" data-toggle="tooltip" class="fieldinfo-left" title="1. A fixed asset is a long-term tangible piece of property that a firm owns and uses in the production of its income and is not expected to be consumed or converted into cash any sooner than at least one year's time.2. Electrification & electrical installations 3. Loading, unloading, transportation, tax & duties paid, erection expenses.4. Other fixed assets of permanent nature ">
                                                                                                        <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Other_Fixed_Asset_After" ValidChars="1234567890.">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="2">
                                                                                                    <strong>Total</strong>
                                                                                                </td>
                                                                                                <td class="text-right">
                                                                                                    <strong>
                                                                                                        <asp:TextBox ID="Txt_Total_Capital_After" runat="server" CssClass="form-control text-right"
                                                                                                            Text="0" Enabled="false" onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                                                    </strong>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-4">
                                                                                    <asp:Label ID="Lbl_Approved_DPR_After_Doc_Name" runat="server" Text=""></asp:Label>
                                                                                    <asp:HiddenField ID="Hid_Approved_DPR_After_Doc_Code" runat="server" />
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FU_Approved_DPR_After" CssClass="form-control" runat="server" />
                                                                                        <asp:HiddenField ID="Hid_Approved_DPR_After_File_Name" runat="server" />
                                                                                        <asp:LinkButton ID="LnkBtn_Upload_Approved_DPR_After_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                            OnClick="LnkBtn_Add_Doc_Click" ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkBtn_Delete_Approved_DPR_After_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                            OnClick="LnkBtn_Delete_Doc_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="Hyp_View_Approved_DPR_After_Doc" runat="server" Target="_blank"
                                                                                            Visible="false" CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <asp:Label ID="Lbl_Msg_Approved_DPR_After_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>
                                                                                </div>
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
                                                                            <asp:TextBox ID="Txt_Equity_Amt" CssClass="form-control" runat="server" Text="0"
                                                                                onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                            <a href="#" data-toggle="tooltip" class="fieldinfo" title="Self Financed"><i class="fa fa-question-circle"
                                                                                aria-hidden="true"></i></a>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" TargetControlID="Txt_Equity_Amt"
                                                                                FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <label class="col-sm-2">
                                                                            Loan From Bank/FI</label>
                                                                        <%--<label class="col-sm-4">
                                                                            <span class="colon">:</span> <span class="lablespan">Total Amount (Excluding Loan for
                                                                                Working Capital)</span></label>--%>
                                                                        <div class="col-sm-4">
                                                                            <span class="colon">:</span>
                                                                            <asp:TextBox ID="Txt_Loan_Bank_FI" CssClass="form-control" runat="server" Text="0"
                                                                                onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                            <a href="#" data-toggle="tooltip" class="fieldinfo" title="The amount of loan borrowed from any financial institute/friend">
                                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" TargetControlID="Txt_Loan_Bank_FI"
                                                                                FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                            <small class="text-gray lablespan">Total Amount (Excluding Loan for Working Capital)</small>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-12 ">
                                                                                <strong>Term Loan Details</strong></label>
                                                                            <div class="col-sm-12">
                                                                                <table class="table table-bordered">
                                                                                    <tr>
                                                                                        <th rowspan="2" width="4%">
                                                                                            SlNo
                                                                                        </th>
                                                                                        <th rowspan="2" width="13%">
                                                                                            Name of Financial Institution
                                                                                        </th>
                                                                                        <th colspan="2" width="26%" style="text-align: center;">
                                                                                            Location
                                                                                        </th>
                                                                                        <th rowspan="2" width="10%">
                                                                                            Term Loan Amount
                                                                                        </th>
                                                                                        <th rowspan="2">
                                                                                            Sanction Date
                                                                                        </th>
                                                                                        <th rowspan="2" width="10%">
                                                                                            Availed Amount
                                                                                        </th>
                                                                                        <th rowspan="2">
                                                                                            Availed Date
                                                                                        </th>
                                                                                        <th rowspan="2" width="5%">
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
                                                                                            <asp:TextBox ID="Txt_TL_Financial_Institution" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_TL_State" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_TL_City" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_TL_Amount" CssClass="form-control" runat="server" onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" Enabled="True"
                                                                                                TargetControlID="Txt_TL_Amount" ValidChars="1234567890.">
                                                                                            </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="input-group  date datePicker" id="Div15">
                                                                                                <asp:TextBox ID="Txt_TL_Sanction_Date" runat="server" class="form-control"></asp:TextBox>
                                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_TL_Avail_Amount" CssClass="form-control" runat="server" onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" Enabled="True"
                                                                                                TargetControlID="Txt_TL_Avail_Amount" ValidChars="1234567890.">
                                                                                            </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="input-group  date datePicker" id="Div16">
                                                                                                <asp:TextBox ID="Txt_TL_Availed_Date" runat="server" class="form-control"></asp:TextBox>
                                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="LnkBtn_TL_Add_More" CssClass="btn btn-success btn-sm" runat="server"
                                                                                                OnClick="LnkBtn_TL_Add_More_Click" OnClientClick="return validateTermLoan();"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="9">
                                                                                            <asp:GridView ID="Grd_TL" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                                                                                ShowHeader="false">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_TL_Sl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="4%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_TL_Financial_Inst" runat="server" Text='<%# Eval("vchNameOfFinancialInst") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="13%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_TL_State" runat="server" Text='<%# Eval("vchState") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="13%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_TL_City" runat="server" Text='<%# Eval("vchCity") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="13%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_TL_Amount" runat="server" Text='<%# Eval("decLoanAmt") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="10%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_TL_Sanction_Date" runat="server" Text='<%# Eval("dtmSanctionDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_TL_Avail_Amt" runat="server" Text='<%# Eval("decAvailedAmt") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="10%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_TL_Avail_Date" runat="server" Text='<%# Eval("dtmAvailedDate" , "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="ImgBtn_Delete_TL" runat="server" ImageUrl="~/Portal/images/deleteIcon.png"
                                                                                                                CommandArgument='<%# Container.DataItemIndex %>' OnClick="ImgBtn_Delete_TL_Click" />
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="5%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-12 ">
                                                                                <strong>Working Capital Loan Details</strong></label>
                                                                            <div class="col-sm-12">
                                                                                <table class="table table-bordered">
                                                                                    <tr>
                                                                                        <th rowspan="2" width="4%">
                                                                                            SlNo
                                                                                        </th>
                                                                                        <th rowspan="2">
                                                                                            Name of Financial Institution
                                                                                        </th>
                                                                                        <th colspan="2" width="26%" style="text-align: center;">
                                                                                            Location
                                                                                        </th>
                                                                                        <th rowspan="2" width="10%">
                                                                                            Loan Amount
                                                                                        </th>
                                                                                        <th rowspan="2">
                                                                                            Sanction Date
                                                                                        </th>
                                                                                        <th rowspan="2" width="10%">
                                                                                            Availed Amount
                                                                                        </th>
                                                                                        <th rowspan="2">
                                                                                            Availed Date
                                                                                        </th>
                                                                                        <th rowspan="2" width="5%">
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
                                                                                            <asp:TextBox ID="Txt_WC_Financial_Institution" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_WC_State" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_WC_City" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_WC_Amount" CssClass="form-control" runat="server" onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" Enabled="True"
                                                                                                TargetControlID="Txt_WC_Amount" ValidChars="1234567890.">
                                                                                            </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="input-group  date datePicker" id="Div3">
                                                                                                <asp:TextBox ID="Txt_WC_Sanction_Date" runat="server" class="form-control"></asp:TextBox>
                                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_WC_Avail_Amount" CssClass="form-control" runat="server" onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" Enabled="True"
                                                                                                TargetControlID="Txt_WC_Avail_Amount" ValidChars="1234567890.">
                                                                                            </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="input-group  date datePicker" id="Div4">
                                                                                                <asp:TextBox ID="Txt_WC_Availed_Date" runat="server" class="form-control"></asp:TextBox>
                                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="LnkBtn_WC_Add_More" CssClass="btn btn-success btn-sm" runat="server"
                                                                                                OnClick="LnkBtn_WC_Add_More_Click" OnClientClick="return validateWCLoan();"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="9">
                                                                                            <asp:GridView ID="Grd_WC" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                                                                                ShowHeader="false">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_WC_Sl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="4%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_WC_Financial_Inst" runat="server" Text='<%# Eval("vchNameOfFinancialInst") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="13%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_WC_State" runat="server" Text='<%# Eval("vchState") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="13%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_WC_City" runat="server" Text='<%# Eval("vchCity") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="13%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_WC_Amount" runat="server" Text='<%# Eval("decLoanAmt") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="10%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_WC_Sanction_Date" runat="server" Text='<%# Eval("dtmSanctionDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_WC_Avail_Amt" runat="server" Text='<%# Eval("decAvailedAmt") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="10%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_WC_Avail_Date" runat="server" Text='<%# Eval("dtmAvailedDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="ImgBtn_Delete_WC" runat="server" ImageUrl="~/Portal/images/deleteIcon.png"
                                                                                                                CommandArgument='<%# Container.DataItemIndex %>' OnClick="ImgBtn_Delete_WC_Click" />
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="5%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label class="col-sm-4">
                                                                                <asp:Label ID="Lbl_Term_Loan_Doc_Name" runat="server" Text=""></asp:Label>
                                                                                <asp:HiddenField ID="Hid_Term_Loan_Doc_Code" runat="server" />
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <div class="input-group">
                                                                                    <asp:FileUpload ID="FU_Term_Loan" CssClass="form-control" runat="server" />
                                                                                    <asp:HiddenField ID="Hid_Term_Loan_File_Name" runat="server" />
                                                                                    <asp:LinkButton ID="LnkBtn_Upload_Term_Loan_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                        OnClick="LnkBtn_Add_Doc_Click" ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                    <asp:LinkButton ID="LnkBtn_Delete_Term_Loan_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                        OnClick="LnkBtn_Delete_Doc_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                    <asp:HyperLink ID="Hyp_View_Term_Loan_Doc" runat="server" Target="_blank" Visible="false"
                                                                                        CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    <%--    <a href="#" data-toggle="tooltip" class="fieldinfo" title="Term loan sanction letter issued from the bank">
                                                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>--%>
                                                                                </div>
                                                                                <asp:Label ID="Lbl_Msg_Term_Loan_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                                    Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <%--
      <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>--%>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-4 ">
                                                                            FDI (If any)
                                                                        </label>
                                                                        <div class="col-sm-8">
                                                                            <span class="colon">:</span>
                                                                            <asp:TextBox ID="Txt_FDI_Componet" CssClass="form-control" runat="server" Text="0"
                                                                                onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                            <a href="#" data-toggle="tooltip" class="fieldinfo" title="Foreign direct investment (FDI) is an investment made by a company or individual in one country in business interests in another country, in the form of either establishing business operations or acquiring business assets in the other country, such as ownership or controlling interest in a foreign company.">
                                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" TargetControlID="Txt_FDI_Componet"
                                                                                FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789.">
                                                                            </cc1:FilteredTextBoxExtender>
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
                                    <div class="form-footer">
                                        <div class="row">
                                            <div class="col-sm-12 text-center">
                                                <asp:Button ID="BtnApply" runat="server" Text="Next" CssClass="btn btn-success" OnClick="BtnApply_Click"
                                                    OnClientClick="return validateBasicDetails();" ToolTip="Click Here to Save and Proceed" />
                                                <asp:HiddenField ID="Hid_Is_Exist_Before" runat="server" />
                                                <asp:HiddenField ID="Hid_Is_Exist_After" runat="server" />
                                                <asp:HiddenField ID="Hid_Data_Source" runat="server" />
                                                <asp:HiddenField ID="Hid_PC_Status" runat="server" />
                                                <asp:HiddenField ID="Hid_Project_Type" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-footer">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 style="color: #777;">
                                                    General Instructions (To fill up subsequent incentive applications)</h4>
                                                <div class="listdiv">
                                                    <ol>
                                                        <li>All fields in the Basic Unit details on this page will be pre-populated in the subsequent
                                                            applications. If you wish to make any changes to the same while applying, you will
                                                            have to first come back to basic unit details and edit your profile information</li>
                                                        <li>The Go-SWIFT application will logout after a defined period of time. In order to
                                                            ensure that the your application details are not lost in this event, it is advisable
                                                            to click on <b>“Save as Draft”</b> periodically</li>
                                                        <li>To view your application status in details you can click on the count and drill
                                                            down to view details in the <b>“Application History”</b> dashboard on top of this
                                                            page</li>
                                                        <li>Incentive application is based on a time frame; hence if you have crossed the time
                                                            frame, the system will not allow you to apply. You have to then take a certification
                                                            of condonation of delay from the empowered committee to proceed with incentive application.
                                                            (To know the time frame for applying for each incentive, click on the <b>“View Details”</b>
                                                            link under the <b>“Operational Guidelines”</b> column in the <b>“Applicable Incentives”</b>
                                                            table after you <b>“Save and Proceed”</b> to the next page )</li>
                                                    </ol>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="LnkBtn_Upload_Org_Doc" />
            <%--  <asp:PostBackTrigger ControlID="LnkBtn_Upload_Prod_Comm_Before_Doc" />--%>
            <%--  <asp:PostBackTrigger ControlID="LnkBtn_Upload_Prod_Comm_After_Doc" />--%>
            <asp:PostBackTrigger ControlID="LnkBtn_Upload_Unit_Type_Doc" />
            <asp:PostBackTrigger ControlID="LnkBtn_Upload_Direct_Emp_Before_Doc" />
            <asp:PostBackTrigger ControlID="LnkBtn_Upload_Direct_Emp_After_Doc" />
            <asp:PostBackTrigger ControlID="LnkBtn_Upload_FFCI_Before_Doc" />
            <asp:PostBackTrigger ControlID="LnkBtn_Upload_FFCI_After_Doc" />
            <asp:PostBackTrigger ControlID="LnkBtn_Upload_Approved_DPR_Before_Doc" />
            <asp:PostBackTrigger ControlID="LnkBtn_Upload_Approved_DPR_After_Doc" />
            <asp:PostBackTrigger ControlID="LnkBtn_Upload_Term_Loan_Doc" />
            <asp:PostBackTrigger ControlID="LnkBtn_Upload_Pioneer_Doc" />
            <asp:PostBackTrigger ControlID="LnkBtn_Upload_Ancillary_Doc" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="Hid_Pop" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
        TargetControlID="Hid_Pop" BackgroundCssClass="modalBackground" CancelControlID="Btn_Close">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalfade" Style="display: none;">
        <div id="undertakingipr2015">
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header bg-purpul">
                        <h4 class="modal-title">
                            Please provide undertaking stating that your unit is not a part of the negative
                            unit list under <strong>IPR 2015</strong></h4>
                    </div>
                    <div class="modal-body">
                        <h4>
                            UNDERTAKING</h4>
                        <p>
                            I hereby declare that my Unit/Enterprise does not fall under the following ineligible
                            unit.
                        </p>
                        <%--   <p>
                            In my application, I will produce required documents for the same.</p>--%>
                        <h4>
                            List of Ineligible Unit Types</h4>
                        <h5 class="text-red">
                            Reference IPR-2015 Point-16,Annexure II</h5>
                        <div class="listdiv">
                            <ul type="I">
                                <li>B. Industrial Unit will not include non-manufacturing/servicing industries
                                    <ol>
                                        <li>General workshops including repair workshops having investment less than 50 Lakhs
                                            and running with power </li>
                                        <li>Cold storage and seafood freezing unit having investment less than Rs. 25 Lakhs
                                        </li>
                                        <li>Electronics repair and maintenance unit for professional grade equipment and computer
                                            software ITES/BPO and related with invest less than Rs. 25 lakhs </li>
                                        <li>Technology Development Laboratory/Prototype Development Centre/Research & Development
                                            with investment less than Rs. 25 Lakh </li>
                                        <li>Printing press with investment in plant and machinery/equipment of less than Rs.
                                            50 Lakhs </li>
                                        <li>Laundry/Dry Cleaning with investment in plant and machinery/equipment of less than
                                            Rs.25 Lakh </li>
                                    </ol>
                                </li>
                                <li>C. The following units shall neither be eligible for fiscal incentives specified
                                    under this IPR nor for allotment of land at concessional rates in the State, but
                                    shall be eligible for investment facilitation, allotment of land under normal rules
                                    at benchmark value/market rate and recommendation to the financial institutions
                                    for term loan and working capital and for recommendation, if necessary to the Power
                                    Distribution Companies:
                                    <ol>
                                        <li>Hullers and Rice mills with investment in plant and machinery of less than Rs.25
                                            Lakhs for industrially backward districts and less than one crore rupees for other
                                            areas </li>
                                        <li>Flour mills including manufacture of besan, pulse mills and chuda mills expect investment
                                            in plant and machinery of more than Rs. 25 Lakhs for industrially backward districts
                                            and less than 1 crore for other areas (Excluding Roller Flour mills) </li>
                                        <li>
                                            <ol>
                                                1. Processing of spices with investment in plant and machinery with less than Rs
                                                10 lakhs for industrially backward districts and less than 2 crore rupees for other
                                                areas.
                                            </ol>
                                            <ol>
                                                2. Units without Spice-mark or Agmark
                                            </ol>
                                        </li>
                                        <li>Confectionary with investment in plant and machinery with less than Rs.10 Lakhs
                                            for industrially backward districts and less than two crore rupees for other areas
                                        </li>
                                        <li>Oil mills with expellers including oil processing, filtering , de-coloring ,coloring
                                            ,refining of edible oils and hydro-generation thereof except investment in plant
                                            and machinery of RS. 10 Lakhs in industrial backward areas. </li>
                                        <li>Preparation of sweets and savories etc. excluding units using mechanized process
                                            with an investment in plant and machinery </li>
                                        <li>Bread making(excluding mechanized bakery) </li>
                                        <li>Mixture.Bhujia and chanachur preparation units </li>
                                        <li>Manufacture of ice candy </li>
                                        <li>Manufacture and processing of betel nuts </li>
                                        <li>Hatcheries, Piggeries, rabbit or Broiler farming </li>
                                        <li>Standalone sponge iron plants </li>
                                        <li>Iron and steel processors, such as cutting of sheets,bars,angles,coils,M.S. sheets,
                                            recoiling, straightening,corrugating,drophammer units etc with low value addition
                                        </li>
                                        <li>Cracker-making units </li>
                                        <li>Tyre retreading units with investment in plant and machinery of less Rs.20 Lakhs
                                        </li>
                                        <li>Stone crushing units </li>
                                        <li>Coal/coke screening coal /coke Briquetting </li>
                                        <li>Production of firewood and charcoal </li>
                                        <li>Painting and spray-painting units with investment in plant and machinery of less
                                            than Rs. 20 Lakhs </li>
                                        <li>Units for physical mixing of fertilizers. </li>
                                        <li>Brick- making units (except units making refractory bricks and those making bricks
                                            from flyash, red mud and similar industrial waste not less than 25% as base martial)
                                        </li>
                                        <li>Manufacturing of tarpaulin out of canvas cloth with investment in plant and machinery
                                            of less than Rs. 20 Lakhs. </li>
                                        <li>Saw mills, sawing of timber. </li>
                                        <li>Carpentry, joinery and wooden furniture making except when part of a wood based
                                            cluster of at least 20 units. </li>
                                        <li>Drilling rigs, Bore-wells and Tube-wells </li>
                                        <li>Units for mixing or blending/packaging of tea. </li>
                                        <li>Units for cutting raw tobacco and sprinkling jiggery for chewing purpose and Gudakhu
                                            manufacturing units. </li>
                                        <li>Units for bottling of medicines. </li>
                                        <li>Bookbinding/Rubber stamp making/making notebooks, exercise notebook s and envelopes.
                                        </li>
                                        <li>Distilled water units </li>
                                        <li>Tailoring (other than readymade garment manufacturing units) </li>
                                        <li>Repacking /stitching/printing of woven sacks out of woven fabrics. </li>
                                        <li>Pre-Processing of oil seeds-Decorticating, expelling, crushing, parching and frying.
                                        </li>
                                        <li>Aerated water and soft drinks units </li>
                                        <li>Bottling units or any activity in respect of IMFL or liquor of any kind </li>
                                        <li>Size reducing/size separating units/ Grinding / mixing units with investment in
                                            plant and machinery of less than ten crore rupees except manufacturing of cement
                                            with clinker. </li>
                                        <li>Polythene less than 40 micron in thickness /recycling of plastic materials.
                                        </li>
                                        <li>Thermal power plants. </li>
                                        <li>Repackaging units. </li>
                                        <%--  <li>Industries falling within the purview of the following Boards and public Agencies.
                                    <ol>
                                        <li>Coir Board </li>
                                        <li>Silk Board</li>
                                        <li>All India handloom and Handicraft Board</li>
                                        <li>Khadi and village industries Commission/Board</li>
                                        <li>Any other Agency con situation by Government for industrial department.</li>
                                    </ol>
                                </li>--%>
                                    </ol>
                                    <small class="text-red">Note: List of industrial units indicated above may be modified
                                        by the Government from time to time.</small> </li>
                            </ul>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="row">
                            <div class="col-sm-6 text-left">
                                <asp:CheckBox ID="ChkBx_Agree" runat="server" Text="I agree that provided information is correct." /></div>
                            <div class="col-sm-6">
                                <asp:Button ID="Btn_Submit" runat="server" Text="Submit" OnClick="Btn_Submit_Click"
                                    class="btn btn-success" OnClientClick="return validate_checkbox();" />
                                <asp:Button ID="Btn_Close" runat="server" Text="Close" class="btn btn-danger" />
                            </div>
                        </div>
                        <%--   <a data-toggle="modal" class="btn btn-success" data-dismiss="modal" data-target="#undertakingfoodprocessing">
                                Submit</a>
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>--%>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
    </asp:Panel>
    <uc3:footer ID="footer" runat="server" />
    <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function pageLoad() {
            $(function () {
                $('.datePicker').datepicker({
                    minDate: new Date(),
                    autoclose: true,
                    format: "dd-M-yyyy"
                });
            });

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });
            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #ProductionEmploymentDetails, #IndustryDetails").removeClass('in');
                $(hdn).addClass("in");
            }

            $(".panel-title > a").on("click", function () {
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });
        }

        function validate_checkbox() {
            if (document.getElementById('<%= ChkBx_Agree.ClientID %>').checked == false) {
                jAlert('<strong>Please Click on CheckBox to Agree !!</strong>', projname);
                document.getElementById('<%= ChkBx_Agree.ClientID %>').focus();
                return false;
            }
        }

    </script>
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.6;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 900px;
            height: 550px;
        }
    </style>
    </form>
</body>
</html>
