<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="EventLog_Exception_Report.aspx.cs" Inherits="EventLog_Exception_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"> 
    <style>
        .investordashboard-sec .portletsearch {
           top: 1px;
          }
    </style>
    <script>
        $(function () {
            $('#linkSpmg').click(function () {
                $('#SpmgContent').slideToggle();
            }); 
            $('#linkCSRActivities').click(function () {
                $('#CSRActivities').slideToggle();
            });
             $('#CifFilter').click(function () {
                $('#CifSearch').slideToggle();
            });
             $('#pealfilter').click(function () {
                $('#pealsearch').slideToggle();
            });
             $('#pealfilterdic').click(function () {
                $('#pealsearchdic').slideToggle();
            });
             $('#linkDepartmentServices').click(function () {
                 $('#DepartmentServices').slideToggle();
            });
            $('#linkDicDepartmentServices').click(function () {
                 $('#DicDepartmentServices').slideToggle();
            });
             $('#INCENTIVEfilter').click(function () {
                $('#INCENTIVEsearch').slideToggle();
            });
             $('#DicINCENTIVEfilter').click(function () {
                $('#DicINCENTIVEsearch').slideToggle();
            });
             $('#landFilter').click(function () {
                $('#landSearch').slideToggle();
            });
            $('#APAAfilter').click(function () {
                $('#APAAsearch').slideToggle();
            });
            $('#DicAPAAfilter').click(function () {
                $('#DicAPAAsearch').slideToggle();
            });
            $('#DiclandFilter').click(function () {
                $('#DiclandSearch').slideToggle();
            });
        });

 

    //On UpdatePanel Refresh.

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                 $('#linkSpmg').click(function () {
                $('#SpmgContent').slideToggle();
            }); 
            $('#linkCSRActivities').click(function () {
                $('#CSRActivities').slideToggle();
            });
             $('#CifFilter').click(function () {
                $('#CifSearch').slideToggle();
            });
             $('#pealfilter').click(function () {
                $('#pealsearch').slideToggle();
            });
             $('#pealfilterdic').click(function () {
                $('#pealsearchdic').slideToggle();
            });
             $('#linkDepartmentServices').click(function () {
                 $('#DepartmentServices').slideToggle();
            });
            $('#linkDicDepartmentServices').click(function () {
                 $('#DicDepartmentServices').slideToggle();
            });
             $('#INCENTIVEfilter').click(function () {
                $('#INCENTIVEsearch').slideToggle();
            });
             $('#DicINCENTIVEfilter').click(function () {
                $('#DicINCENTIVEsearch').slideToggle();
            });
             $('#landFilter').click(function () {
                $('#landSearch').slideToggle();
            });
            $('#APAAfilter').click(function () {
                $('#APAAsearch').slideToggle();
            });
            $('#DicAPAAfilter').click(function () {
                $('#DicAPAAsearch').slideToggle();
            });
            $('#DiclandFilter').click(function () {
                $('#DiclandSearch').slideToggle();
            });
            }
        });
    };
        function pageLoad() {
            var yr4 = $('#ContentPlaceHolder1_ddlspmgyear option:selected').text();
            $('#ContentPlaceHolder1_lbl4').html(yr4);         
            $('#btnspmg').click(function () {
                yr4 = $('#ContentPlaceHolder1_ddlspmgyear option:selected').text();
                $('#ContentPlaceHolder1_lbl4').html(yr4);                
            });
            var yr7 = $('#ContentPlaceHolder1_ddlCSRYear option:selected').text();
            $('#ContentPlaceHolder1_lbl7').html(yr7);           
            $('#btnCSRStatus').click(function () {
                yr7 = $('#ContentPlaceHolder1_ddlCSRYear option:selected').text();
                $('#ContentPlaceHolder1_lbl7').html(yr7);                
            });
            var yr5 = $('#ContentPlaceHolder1_ddlYearCICG option:selected').text();
            $('#ContentPlaceHolder1_lbl5').html(yr5);           
            $('#btnCICGStatus').click(function () {
                yr5 = $('#ContentPlaceHolder1_ddlYearCICG option:selected').text();
                $('#ContentPlaceHolder1_lbl5').html(yr5);               
            });
            var yr1 = $('#ContentPlaceHolder1_ddlPealYear').val();
            $('#ContentPlaceHolder1_lbl1').html(yr1);           
            $('#btnPealsubmit').click(function () {
                yr1 = $('#ContentPlaceHolder1_ddlPealYear').val();
                $('#ContentPlaceHolder1_lbl1').html(yr1);                
            });
            var yr10 = $('#ContentPlaceHolder1_ddlpealdicyear option:selected').text();
            $('#ContentPlaceHolder1_lbl10').html(yr10);          
            $('#btnpealdicsubmit').click(function () {
                yr10 = $('#ContentPlaceHolder1_ddlpealdicyear option:selected').text();
                $('#ContentPlaceHolder1_lbl10').html(yr10);               
            }); 

            var yr94 = $('#ContentPlaceHolder1_ddlserviceyear option:selected').text();
            $('#ContentPlaceHolder1_lbl444').html(yr94);            
            $('#btnDicStatusOfApproval').click(function () {
                yr94 = $('#ContentPlaceHolder1_ddlserviceyear option:selected').text();
                $('#ContentPlaceHolder1_lbl444').html(yr94);               
            });
            var yr8 = $('#ContentPlaceHolder1_ddlIncentiveYear option:selected').text();
            $('#ContentPlaceHolder1_lbl8').html(yr8);            
            $('#btnIncentiveSubmit').click(function () {
                yr8 = $('#ContentPlaceHolder1_ddlIncentiveYear option:selected').text();
                $('#ContentPlaceHolder1_lbl8').html(yr8);                
            });
            var yr44 = $('#ContentPlaceHolder1_ddldicincentiveyear option:selected').text();
            $('#ContentPlaceHolder1_lbl500').html(yr44);            
            $('#btnDicIncentiveSubmit').click(function () {
                yr44 = $('#ContentPlaceHolder1_ddldicincentiveyear option:selected').text();
                $('#ContentPlaceHolder1_lbl500').html(yr44);                
            });
            var yr9 = $('#ContentPlaceHolder1_ddlLandFinYear option:selected').text();
            $('#ContentPlaceHolder1_lbl9').html(yr9);          
            $('#btnLandSubmit').click(function () {
                yr9 = $('#ContentPlaceHolder1_ddlLandFinYear option:selected').text();
                $('#ContentPlaceHolder1_lbl9').html(yr9);               
            });
            var yr6 = $('#ContentPlaceHolder1_ddlAppaYear option:selected').text();
            $('#ContentPlaceHolder1_lbl6').html(yr6);           
            $('#btnAPAASubmit').click(function () {
                yr6 = $('#ContentPlaceHolder1_ddlAppaYear option:selected').text();
                $('#ContentPlaceHolder1_lbl6').html(yr6);               
            });
            var yr55 = $('#ContentPlaceHolder1_ddldicAppaYear option:selected').text();
            $('#ContentPlaceHolder1_Lbl55').html(yr55);           
            $('#btndicAPAASubmit').click(function () {
                yr55 = $('#ContentPlaceHolder1_ddldicAppaYear option:selected').text();
                $('#ContentPlaceHolder1_Lbl55').html(yr55);               
            });
            var yr50 = $('#ContentPlaceHolder1_ddldicLandFinYear option:selected').text();
            $('#ContentPlaceHolder1_Lbl50').html(yr50);          
            $('#btndicLandSubmit').click(function () {
                yr50 = $('#ContentPlaceHolder1_ddldicLandFinYear option:selected').text();
                $('#ContentPlaceHolder1_Lbl50').html(yr50);               
            });          
        }
        function ShowSearchpanel() {
            //dvservce
            $('[id*=DepartmentServices]').css("display", "block");
            return false;
        }
       
    </script>
   
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
       
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    Exception Report
                </h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Exception Report</a></li><li><a>View</a></li></ul>
            </div>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="dropdown">
                                <ul class="dropdown-menu dropdown-menu-right">
                                    <%--<li>
                                        <asp:LinkButton ID="lnkPdf" CssClass="back" runat="server" title="Export to PDF"
                                            OnClick="lnkPdf_Click"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                    <li><a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                                        <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" title="Export to Excel"
                                            OnClick="lnkExport_Click"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
                                    </a></li>
                                    <li><a class="PrintBtn" data-tooltip="Print" data-toggle="tooltip" data-title="Print">
                                        <i class="panel-control-icon fa fa-print"></i></a></li>
                                    <li><a href="javascript:history.back()" data-tooltip="Back" data-toggle="tooltip"
                                        data-title="Back"><i class="panel-control-icon fa  fa-chevron-circle-left"></i></a>
                                    </li>--%>
                                </ul>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="up1" runat="server">
                              <ContentTemplate>
                        <div class="panel-body">
                            <div class="search-sec NOPRINT">
                                <div class="form-group row NOPRINT">                                    
                                            <div class="col-sm-3">
                                                <label for="Department">
                                                    User</label>
                                                <asp:DropDownList ID="drpuser" runat="server" CssClass="form-control">
                                                   
                                                </asp:DropDownList>
                                            </div> 
                                            <div class="col-sm-3">
                                        <asp:Button ID="btnSearch" CssClass="btn btn btn-add btn-sm"
                                            runat="server" Text="Search" OnClick="btnSearch_Click" Style="margin-top:23px"></asp:Button>
                                             </div>
                                                                         
                                </div>
                                <div class="form-group row NOPRINT">
                                   
                                </div>
                            </div>
                            <section class="content">
                     <div class="row">
                    <!-- Form controls -->
                    <div class="col-sm-12">                       
                        <div class="grphs-sec">                           
                            <div class="row">


                                 <%-- PEAL FORM STATUS--%>
                                <div class="col-md-8 col-sm-8" id="SWASPANEL" runat="server" visible="false">
                                    <div class="investordashboard-sec incentive-sec" >
                                        <h4>
                                            Single Window Application Status(<asp:Label ID="lbl1" runat="server" Text="reerre"></asp:Label>)
                                            <a class="pull-right spmgfilter" data-toggle="tooltip" title="Search" id="pealfilter">
                                                <i class="fa fa-search"></i></a>
                                        </h4>
                                        <div class="portletcontainer cmdashbordportlet">
                                           <div class="scroll-prtlet">
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th rowspan="2" style="vertical-align: middle!important;">
                                                            Status
                                                        </th>
                                                        <th width="130px" style="vertical-align: middle!important;" rowspan="2">
                                                            State Level
                                                        </th>
                                                        <th width="130px" rowspan="2" style="vertical-align: middle!important;">
                                                            District Level
                                                        </th>
                                                        <th width="260px" style="text-align: center!important;" colspan="2">
                                                            Special Single Window
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th width="130px" colspan="1">
                                                            IT
                                                        </th>
                                                        <th width="130px" colspan="1">
                                                            Tourism
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Applied
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealApplied" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealdistApplied" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealITApplied" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealTourismApplied" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Approved
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealApproved" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealdistApproved" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <atitle="Applied">
                                                                <asp:Label ID="lblPealITApproved" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealTourismApproved" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Rejected
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealRejected" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealdistRejected" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealITRejected" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealTourismRejected" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Deferred
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealDeferred" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealdistDeferred" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealITDeferred" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealTourismDeferred" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Query In Progress
                                                        </td>
                                                        <td align="right">
                                                            <a title="Query In Progress">
                                                                <asp:Label ID="lblPealQueryRaise" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Query In Progress">
                                                                <asp:Label ID="lblPealdistQueryRaise" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Query In Progress">
                                                                <asp:Label ID="lblPealITQueryRaise" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Query In Progress">
                                                                <asp:Label ID="lblPealTourismQueryRaise" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Under Evalution
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealUnderEvalution" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealdistUnderEvalution" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealITUnderEvalution" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Applied">
                                                                <asp:Label ID="lblPealTourismUnderEvalution" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Application pending since last 30 days
                                                        </td>
                                                        <td align="right">
                                                            <a title="Under Evalution">
                                                                <asp:Label ID="Lbl_Peal_ORTPSA_State" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Under Evalution">
                                                                <asp:Label ID="Lbl_Peal_ORTPSA_Dist" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Under Evalution">
                                                                <asp:Label ID="Lbl_Peal_ORTPSA_IT" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Under Evalution">
                                                                <asp:Label ID="Lbl_Peal_ORTPSA_Tourism" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                             
                                            <div class="portletsearch" id="pealsearch">
                                            <div class="form-group">
                                                <div class="row" style="display: none">
                                                    <label class="col-sm-4">
                                                        Amount
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlPealQuarter" runat="server">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text=">50 Cr." Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="<50 Cr." Value="2"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Year
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlPealYear" runat="server">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        District</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlPEALDistrict" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-8 col-sm-offset-4">
                                                        <asp:Button ID="btnPealsubmit" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnPealsubmit_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>                                       
                                    </div>
                                </div>
                                </div>


                                 <%--PEAL FORM STATUS DIC--%>

                                <div class="col-sm-6  col-md-4"  id="SWASDICPANEL" runat="server" visible="false">
                                    <div class="investordashboard-sec incentive-sec">
                                        <h4>
                                            Single Window Application Status
                                            (<asp:Label ID="lbl10" runat="server" Text=""></asp:Label>)
                                            <a class="pull-right spmgfilter" data-toggle="tooltip" title="Search" id="pealfilterdic">
                                                <i class="fa fa-search"></i></a>
                                        </h4>
                                        <div class="portletcontainer cmdashbordportlet">
                                           <div class="scroll-prtlet">
                                                <ul>
                                                    <li><a title="Applied">
                                                        Applied<span>
                                                            <asp:Label ID="lbldicPealApplied" runat="server" Text=""></asp:Label></span></a>
                                                    </li>
                                                    <li><a title="Approved">
                                                        Approved<span>
                                                            <asp:Label ID="lbldicPealApproved" runat="server" Text=""></asp:Label></span></a>
                                                    </li>
                                                    <li><a title="Rejected">
                                                        Rejected<span>
                                                            <asp:Label ID="lbldicPealRejected" runat="server" Text=""></asp:Label></span></a>
                                                    </li>
                                                    <li><a title="Deferred">
                                                        Deferred<span>
                                                            <asp:Label ID="lbldicPealDeferred" runat="server" Text=""></asp:Label></span></a>
                                                    </li>
                                                    <li><a title="Query In Progress"
                                                        >Query In Progress<span>
                                                            <asp:Label ID="lbldicQueryInprogress" runat="server" Text=""></asp:Label></span></a>
                                                    </li>
                                                    <li><a title="Under Evalution"
                                                        >Under Evalution<span>
                                                            <asp:Label ID="lbldicPealUnderEvalution" runat="server" Text=""></asp:Label></span></a>
                                                    </li>
                                                    <li><a title=" Application pending since last 30 days"
                                                        >Application pending since last 30 days<span>
                                                            <asp:Label ID="Lbl_dic_Peal_ORTPSA" runat="server" Text=""></asp:Label></span></a>
                                                    </li>
                                                </ul>
                                            </div> 
                                            <div class="portletsearch" id="pealsearchdic">
                                                 <div class="form-group">
                                                <div class="row" style="display: none">
                                                    <label class="col-sm-4">
                                                        Amount
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddldicPealQuarter" runat="server">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text=">50 Cr." Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="<50 Cr." Value="2"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" >
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Year
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlpealdicyear" runat="server">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row" style="display: none">
                                                    <label class="col-sm-4">
                                                        District</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlpealdicdistrict" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-8 col-sm-offset-4">
                                                        <asp:Button ID="btnpealdicsubmit" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnpealdicsubmit_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        </div>                                        
                                    </div>
                                </div>



                                <%--Department Wise Approvals--%>
                                <div class="col-sm-6  col-md-4" id="DWAPANEL" runat="server" visible="false">
                                    <div class="investordashboard-sec incentive-sec">
                                        <h4>
                                            Department Wise Approvals
                                            <a class="pull-right spmgfilter" data-toggle="tooltip" title="Search"
                                                id="linkDepartmentServices"><i class="fa fa-search"></i></a>
                                        </h4>
                                        <div class="portletcontainer cmdashbordportlet">
                                            <ul>
                                                <li><a
                                                    title="Application Received">Application Received<span id="hdApplied" runat="server"><asp:Literal
                                                        ID="ltlServiceApplied" runat="server"></asp:Literal></span></a></li>
                                                <li><a 
                                                    title="Total Approvals Provided">Total Approvals Provided<span id="hdApprove" runat="server"><asp:Literal
                                                        ID="ltlApprove" runat="server"></asp:Literal></span></a></li>
                                                <li><a 
                                                    title="Approval Pending">Query In Progress<span id="hdnqueryRaised" runat="server"><asp:Literal
                                                        ID="Literal1" runat="server"></asp:Literal></span></a></li>
                                                <li><a 
                                                    title="Approval Pending">Approval Pending<span id="hdPending" runat="server"><asp:Literal
                                                        ID="ltlServicepending" runat="server"></asp:Literal></span></a></li>
                                                <li><a 
                                                    title="Total Rejected ">Total Rejected <span id="hdReject" runat="server">
                                                        <asp:Literal ID="ltlServiceReject" runat="server"></asp:Literal></span></a></li>
                                                <li><a 
                                                    title="Applications past ORTPSA timelines">Applications past ORTPSA timelines<span
                                                        class="bgdisbursed" id="hdExceed" runat="server"></span></a></li>
                                            </ul>
                                            <div class="portletsearch" id="DepartmentServices">                                                                                                 
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Department</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control dpt" OnSelectedIndexChanged="ddldept_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>                                                
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Service</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlService" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-8 col-sm-offset-4">
                                                        <asp:Button ID="btnStatusOfApproval" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnStatusOfApproval_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>                                        
                                    </div>
                                </div>
                                </div>



                                <%--Department Wise DIC Approvals--%>

                                <div class="col-sm-6  col-md-4" id="DWADICPANEL" runat="server" visible="false">
                                    <div class="investordashboard-sec incentive-sec">
                                        <h4>
                                            Department Wise Approvals(<asp:Label ID="lbl444" runat="server" Text=""></asp:Label>)
                                            <a class="pull-right spmgfilter" data-toggle="tooltip" title="Search"
                                                id="linkDicDepartmentServices"><i class="fa fa-search"></i></a>
                                        </h4>
                                        <div class="portletcontainer cmdashbordportlet">
                                           <ul>
                                                <li><a title="Application Received">Application Received<span id="dichdApplied" runat="server">
                                                    <asp:Literal ID="Literal2" runat="server"></asp:Literal></span></a></li>
                                                <li><a 
                                                    title="Total Approvals Provided">Total Approvals Provided<span id="dichdApprove" runat="server"><asp:Literal
                                                        ID="Literal3" runat="server"></asp:Literal></span></a></li>                                               
                                                <li><a 
                                                    title="Approval Pending">Query In Progress<span id="dichdnqueryRaised" runat="server"><asp:Literal
                                                        ID="Literal4" runat="server"></asp:Literal></span></a></li>
                                                <li><a 
                                                    title="Approval Pending">Rejected<span id="dichdReject" runat="server"></span></a></li>
                                                <li><a 
                                                    title="Approval Pending">Approval Pending<span id="dichdPending" runat="server"></span></a></li>
                                            </ul>
                                            <div class="portletsearch" id="DicDepartmentServices">
                                           <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Year</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlserviceyear" runat="server">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Month</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlServcMonth" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" style="display: none">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Districts</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlServcDistrict" runat="server">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-8 col-sm-offset-4">
                                                        <asp:Button ID="btnDicStatusOfApproval" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnDicStatusOfApproval_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>                                        
                                    </div>
                                </div>
                                </div>




                                  <%-- Incentive Details--%>
                                <div class="col-sm-6  col-md-4" id="INCENTIVEPANEL" runat="server" visible="false">
                                    <div class="investordashboard-sec incentive-sec" >
                                        <h4>
                                            Incentive Details
                                            (<asp:Label ID="lbl8" runat="server" Text=""></asp:Label>)<a class="pull-right spmgfilter"
                                                data-toggle="tooltip" title="Search" id="INCENTIVEfilter"><i class="fa fa-search"></i></a>
                                        </h4>
                                        <div class="portletcontainer cmdashbordportlet">
                                          <ul>
                                                <li>                                                    
                                                    <a title="Sanctioned"
                                                        >Sanctioned<span><asp:Label ID="lblIncsanctioed" runat="server"
                                                            Text=""></asp:Label></span></a> </li>                                               
                                                <li><a title="Scrutiny">Pending <span>
                                                        <asp:Label ID="lblIncpending" runat="server" Text=""></asp:Label></span></a>
                                                </li>                                               
                                                <li><a title="Rejected"
                                                    >Rejected<span><asp:Label ID="lblIncrejected" runat="server"
                                                        Text=""></asp:Label></span></a>                                                   
                                                </li>                                               
                                                <li><a title="Disbursed"
                                                    >Disbursed <span>
                                                        <asp:Label ID="lblIncApplied" runat="server" Text=""></asp:Label></span></a>
                                                </li>                                               
                                            </ul>
                                            <div class="portletsearch" id="INCENTIVEsearch">
                                            <div class="form-group">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            District</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlIncentiveDistrict" runat="server">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>                                                               
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Year
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlIncentiveYear" runat="server">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-8 col-sm-offset-4">
                                                        <asp:Button ID="btnIncentiveSubmit" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnIncentiveSubmit_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>                                      
                                    </div>
                                </div>
                                </div>




                                 <%-- DIC Incentive Details--%>
                                  <div class="col-sm-6  col-md-4" id="DICINCENTIVEPANEL" runat="server" visible="false">
                                    <div class="investordashboard-sec incentive-sec" >
                                        <h4>
                                            Incentive Details
                                            (<asp:Label ID="lbl500" runat="server" Text=""></asp:Label>)<a class="pull-right spmgfilter"
                                                data-toggle="tooltip" title="Search" id="DicINCENTIVEfilter"><i class="fa fa-search"></i></a>
                                        </h4>
                                        <div class="portletcontainer cmdashbordportlet">
                                           <ul>
                                                <li>                                                    
                                                    <a title="Sanctioned"
                                                        >Sanctioned<span><asp:Label ID="lbldicIncApplied" runat="server"
                                                            Text=""></asp:Label></span></a> </li>                                               
                                                <li><a title="Scrutiny">Pending <span>
                                                        <asp:Label ID="lbldicIncpending" runat="server" Text=""></asp:Label></span></a>
                                                </li>                                               
                                                <li><a title="Rejected">Rejected<span><asp:Label ID="lbldicIncrejected" runat="server"
                                                        Text=""></asp:Label></span></a>                                                    
                                                </li>                                                
                                                <li><a title="Disbursed">Disbursed <span>
                                                        <asp:Label ID="lbldicIncsanctioed" runat="server" Text=""></asp:Label></span></a>
                                                </li>                                              
                                            </ul>
                                            <div class="portletsearch" id="DicINCENTIVEsearch">
                                           <div class="form-group">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Quarterwise</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddldicIncentive" runat="server">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>                                                                
                                                                <asp:ListItem Text="Q1" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Q2" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Q3" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="Q4" Value="4"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Year
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddldicincentiveyear" runat="server">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" style="display: none">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        District</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddldicincentivedistrict" runat="server">                                                           
                                                            <asp:ListItem Text="Q1" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Q2" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Q3" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Q4" Value="4"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-8 col-sm-offset-4">
                                                        <asp:Button ID="btnDicIncentiveSubmit" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnDicIncentiveSubmit_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>                                      
                                    </div>
                                </div>
                                </div>



                                 <%--LAND ALLOTMENT DETAILS--%>
                                <div class="col-sm-6  col-md-4" id="LADPANEL" runat="server" visible="false">
                                    <div class="investordashboard-sec incentive-sec" >
                                        <h4>
                                            Land Allotment Details

                                            (<asp:Label ID="lbl9" runat="server" Text=""></asp:Label>)<a
                                                class="pull-right landfilter" data-toggle="tooltip" title="Search" id="landFilter"><i
                                                    class="fa fa-search"></i></a>
                                        </h4>
                                        <div class="portletcontainer cmdashbordportlet">
                                            <div class="scroll-prtlet">
                                             <ul>
                                                <li><a title="Land assessments"
                                                    >No. of Projects Requiring Land<span id="spLandAssesment"
                                                        runat="server"></span></a></li>
                                                <li><a title="proposals sent to IDCO for Land Allotment"
                                                    >No. of proposals sent to
                                                    <br />
                                                    IDCO for Land Allotment <span id="spPropIDCO" runat="server"></span></a></li>
                                                <li><a title="No. of proposal for which land allotted by IDCO"
                                                    >No. of proposal for which
                                                    <br />
                                                    land allotted by IDCO <span id="spLandAllotByIDCO" runat="server"></span></a>
                                                </li>
                                                <li><a title="Land allotted by IDCO (in Acres)"
                                                    >Area of Land allotted
                                                    <br />
                                                    by IDCO (in Acres) <span id="spLandAllot" runat="server"></span></a></li>
                                                <li><a title="No. of Applications where ORTPSA Timelines have exceeded"
                                                    >No. of Applications where ORTPSA
                                                    <br />
                                                    Timelines have exceeded <span id="spORTPSALAnd" runat="server"></span></a></li>
                                            </ul>
                                                </div>
                                            <div class="portletsearch" id="landSearch" style="top: -5px;">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Year</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlLandFinYear" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-8 col-sm-offset-4">
                                                            <asp:Button ID="btnLandSubmit" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnLandSubmit_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                        </div>
                                    </div>
                                </div>
                                  </div>



                                 <%--DIC LAND ALLOTMENT DETAILS--%>
                                <div class="col-sm-6  col-md-4" id="DICLADPANEL" runat="server" visible="false">
                                    <div class="investordashboard-sec incentive-sec">
                                        <h4>
                                            Land Allotment Details (<asp:Label ID="Lbl50" runat="server" Text=""></asp:Label>)<a
                                                class="pull-right landfilter" data-toggle="tooltip" title="Search" id="DiclandFilter"><i
                                                    class="fa fa-search"></i></a></h4>
                                        <div class="portletcontainer cmdashbordportlet">
                                            <div class="scroll-prtlet">
                                            <ul>
                                                <li><a title="Land assessments"
                                                    >No. of Projects Requiring Land<span id="dicspLandAssesment"
                                                        runat="server"></span></a></li>
                                                <li><a title="proposals sent to IDCO for Land Allotment"
                                                    >No. of proposals sent to
                                                    <br />
                                                    IDCO for Land Allotment<span id="dicspPropIDCO" runat="server"></span></a></li>
                                                <li><a title="No. of proposal for which land allotted by IDCO"
                                                    >No. of proposal for which
                                                    <br />
                                                    land allotted by IDCO<span id="dicspLandAllotByIDCO" runat="server"></span></a></li>
                                                <li><a title="Land allotted by IDCO (in Acres)"
                                                    >Area of Land allotted
                                                    <br />
                                                    by IDCO (in Acres)<span id="dicspLandAllot" runat="server"></span></a></li>
                                                <li><a title="No. of Applications where ORTPSA Timelines have exceeded"
                                                    >No. of Applications where ORTPSA
                                                    <br />
                                                    Timelines have exceeded<span id="dicspORTPSALAnd" runat="server"></span></a></li>
                                            </ul>
                                                </div>
                                            <div class="portletsearch" id="DiclandSearch" style="top: -5px;">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Year</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddldicLandFinYear" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-8 col-sm-offset-4">
                                                            <asp:Button ID="btndicLandSubmit" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btndicLandSubmit_Click"
                                                                ></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                              
                                <%--  SPMG--%>
                                <div class="col-sm-6  col-md-4" id="SPMGPANEL" runat="server" visible="false">
                                    <div class="investordashboard-sec incentive-sec ">
                                        <h4>
                                            STATE PROJECT MONITORING GROUP(<asp:Label ID="lbl4" runat="server" Text=""></asp:Label>)<a
                                                class="pull-right spmgfilter" data-toggle="tooltip" title="Search" id="linkSpmg"><i
                                                    class="fa fa-search"></i></a>
                                        </h4>
                                        <div class="portletcontainer cmdashbordportlet">
                                             <ul>
                                                <li><a title="Issues Received" id="spmgreceivedclr" runat="server">Issues Received<span id="spmgraised" runat="server"></span></a></li>
                                                <li><a title="Issues Resolved" id="spmgresolvedclr" runat="server">Issues Resolved<span id="spmgresolved" runat="server"></span></a></li>
                                                <li><a title="Issues Pending" id="spmgpendingclr" runat="server">Issues Pending<span id="spmgpending" runat="server"></span></a></li>
                                                <li><a title="Issues Pending > 30 days" id="spmgpending30clr" runat="server">Issues Pending > 30 days <span id="spmg30pending"
                                                        runat="server"></span></a></li>
                                            </ul>
                                            <div class="portletsearch" id="SpmgContent" style="top: -5px;">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Year</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlspmgyear" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-8 col-sm-offset-4">
                                                            <asp:Button ID="btnspmg" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnspmg_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div> 
                                

                                <%--  CSR Activities--%>
                                <div class="col-sm-6  col-md-4" id="CSRPANEL" runat="server" visible="false">                                       
                                    <div class="investordashboard-sec incentive-sec ">
                                        <h4>
                                            CSR ACTIVITIES(<asp:Label ID="lbl7" runat="server" Text=""></asp:Label>) <a class="pull-right spmgfilter"
                                                data-toggle="tooltip" title="Search" id="linkCSRActivities"><i class="fa fa-search">
                                                </i></a>
                                        </h4>
                                        <div class="portletcontainer cmdashbordportlet">
                                             <ul>
                                                <li><a title="Projects taken up" id="Spcorclr" runat="server">No. of recommended CSR Projects undertaken by corporates<span id="Spcor" runat="server"></span></a></li>
                                                <li><a title="Projects taken up" id="SPProjectclr" runat="server">Total Project<span id="SPProject" runat="server"></span></a></li>
                                                <li><a title="Projects taken up" id="SPRecommendedCouncilclr" runat="server">Recommended by Council<span id="SPRecommendedCouncil" runat="server"></span></a></li>
                                                <li><a title="Projects taken up" id="SPSpentclr" runat="server">Total Spending<span><i class="fa fa-rupee"></i>&nbsp;<label id="SPSpent" runat="server"></label>&nbsp;
                                                    Cr.</span></a></li>
                                            </ul>
                                            <div class="portletsearch" id="CSRActivities" style="top: -5px;">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            District</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlcsrdistrict" runat="server">                                                                
                                                            </asp:DropDownList>                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Year</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlCSRYear" runat="server">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-8 col-sm-offset-4">
                                                            <asp:Button ID="btnCSRStatus" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnCSRStatus_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                           
                                        </div>
                                    </div>
                                </div>
                                    </div>



                                <%--Central Inspection Framework--%>
                                <div class="col-sm-6  col-md-4" id="CIFPANEL" runat="server" visible="false">
                                    <div class="investordashboard-sec incentive-sec ">
                                        <h4>
                                            Central Inspection Framework
                                            <asp:Label ID="lbl5" runat="server" Text="" Style="display: none"></asp:Label><a
                                                class="pull-right spmgfilter" data-toggle="tooltip" title="Search" id="CifFilter"><i
                                                    class="fa fa-search"></i></a>
                                        </h4>
                                        <div class="portletcontainer cmdashbordportlet">
                                           <ul>
                                                <li><a title="Inspections Completed" id="SPcicgcompletedclr" runat="server">Inspections Completed<span id="SPcicgcompleted" runat="server"></span></a></li>
                                                <li><a title="Inspections Scheduled" id="SPcicgappliedclr" runat="server">Inspections Scheduled<span id="SPcicgapplied" runat="server"></span></a></li>
                                                <li><a title="Unattended Inspections" id="SPunattInsdashclr" runat="server">Unattended Inspections<span id="SPunattInsdash" runat="server"></span></a></li>
                                                <li><a title="Inspection reports not uploaded within 48 hours" id="SPReprtNotUploadedclr" runat="server">Inspection reports
                                                    not uploaded within 48 hours<span class="bgdisbursed" id="SPReprtNotUploaded" runat="server"></span></a></li>
                                            </ul>  
                                            <div class="portletsearch" id="CifSearch">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Department</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="ddldeptCIF" runat="server" CssClass="form-control dpt">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Year</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="ddlYearCICG" runat="server" CssClass="form-control dpt">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Month</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlCICGMonth" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-8 col-sm-offset-4">
                                                        <asp:Button ID="btnCICGStatus" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnCICGStatus_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>                                       
                                    </div>
                                </div>
                                </div>



                                <%--APAA--%>
                                <div class="col-sm-6  col-md-4" id="APPPANEL" runat="server" visible="false">
                                    <div class="investordashboard-sec incentive-sec" >
                                        <h4>
                                            IDCO POST ALLOTMENT APPLICATIONS
                                             (<asp:Label ID="lbl6" runat="server" Text=""></asp:Label>)
                                            <a class="pull-right spmgfilter" data-toggle="tooltip" title="Search" id="APAAfilter">
                                                <i class="fa fa-search"></i></a>
                                        </h4>
                                        <div class="portletcontainer cmdashbordportlet">
                                          <ul>
                                                <li><a title="Received" id="spchngrqstAppliedclr" runat="server">Change requests received<span id="spchngrqstApplied" runat="server"></span></a></li>
                                                <li><a title="Processed" id="spchngreqdisposeclr" runat="server">Change requests processed<span id="spchngreqdispose" runat="server"></span></a></li>
                                                <li><a title="Disposed" id="spchngreqPendAtIDCOclr" runat="server">Change requests pending to be disposed<span id="spchngreqPendAtIDCO"
                                                        runat="server"></span></a></li>
                                                <li><a title="Disposed" id="spnPendingatUnitclr" runat="server">Change requests pending at Unit<span id="spnPendingatUnit"
                                                        runat="server"></span></a></li>
                                                <li><a title="Approvals" id="spchngReqCrossThirtyclr" runat="server">Change requests which have crossed 30 days<span class="bgdisbursed"
                                                        id="spchngReqCrossThirty" runat="server"></span></a></li>
                                            </ul>
                                            <div class="portletsearch" id="APAAsearch">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Districts</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlAPAADistrict" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Year</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlAppaYear" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Month</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlAppaMonth" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-8 col-sm-offset-4">
                                                        <asp:Button ID="btnAPAASubmit" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnAPAASubmit_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>                                       
                                    </div>
                                </div>
                               </div>


                               


                                 <%--DIC IDCO POST ALLOTMENT APPLICATIONS Status--%>
                                 <div class="col-sm-6  col-md-4" id="DICAPPPANEL" runat="server" visible="false">
                                    <div class="investordashboard-sec incentive-sec" >
                                        <h4>
                                            IDCO POST ALLOTMENT APPLICATIONS
                                             (<asp:Label ID="Lbl55" runat="server" Text=""></asp:Label>)
                                            <a class="pull-right spmgfilter" data-toggle="tooltip" title="Search" id="DicAPAAfilter">
                                                <i class="fa fa-search"></i></a>
                                        </h4>
                                        <div class="portletcontainer cmdashbordportlet">
                                          <ul>
                                                <li><a title="Received" id="dicspchngrqstAppliedclr" runat="server">Change requests received<span id="dicspchngrqstApplied" runat="server"></span></a></li>
                                                <li><a title="Processed" id="dicspchngreqPendAtIDCOclr" runat="server">Change requests processed<span id="dicspchngreqPendAtIDCO" runat="server"></span></a></li>
                                                <li><a title="Disposed" id="dicspchngreqdisposeclr" runat="server">Change requests pending to be disposed<span id="dicspchngreqdispose"
                                                        runat="server"></span></a></li>
                                                <li><a title="Disposed" id="dicspnPendingatUnitclr" runat="server">Change requests pending at Unit<span id="dicspnPendingatUnit"
                                                        runat="server"></span></a></li>
                                                <li><a title="Approvals" id="dicspchngReqCrossThirtyclr" runat="server">Change requests which have crossed 30 days<span class="bgdisbursed"
                                                        id="dicspchngReqCrossThirty" runat="server"></span></a></li>
                                            </ul>
                                            <div class="portletsearch" id="DicAPAAsearch">
                                            <div class="form-group" style="display: none">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Districts</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddldicAPAADistrict" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Year</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddldicAppaYear" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Month</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList CssClass="form-control" ID="ddldicAppaMonth" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-8 col-sm-offset-4">
                                                        <asp:Button ID="btndicAPAASubmit" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btndicAPAASubmit_Click"
                                                            ></asp:Button>
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
                            </section>
                        </div>
                        </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSearch" />
                            </Triggers>
                          </asp:UpdatePanel>  
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
