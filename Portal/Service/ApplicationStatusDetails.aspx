<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationStatusDetails.aspx.cs"
    Inherits="Portal_Service_ApplicationStatusDetails" MasterPageFile="~/MasterPage/Application.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .bindlabel
        {
            border: 1px solid #cccccc;
            padding: 3px 10px;
            margin-top: 0px !important;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 0px;
            border-style: none;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 600px;
            height: 400px;
        }
        .Querysec
        {
            background: #eee;
            padding: 10px;
        }
        .Querysec h4
        {
            margin: 0px;
            margin-bottom: 10px;
            font-size: 20px;
            border-bottom: 2px solid #bdbdbd;
            padding-bottom: 8px;
            font-weight: 700;
        }
        .Querysec .table
        {
            background: #fff;
        }
        .Querysec .table-bordered > tbody > tr > td, .Querysec .table-bordered > tbody > tr > th, .Querysec .table-bordered > tfoot > tr > td, .Querysec .table-bordered > tfoot > tr > th, .Querysec .table-bordered > thead > tr > td, .table-bordered > thead > tr > th
        {
            border: 1px solid #d0d0d0;
        }
    </style>
    <script>

        $(document).ready(function () {

            $('.menuservices').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });
        });
   
    </script>
    <script type="text/javascript">
     
        $(document).ready(function () {
            $("a").click(function (event) {
                debugger;
                var href = $(this).attr('href');
                //$(this).attr('href', '#');
                var Filename = href.split('/');
                if (Filename[4].indexOf('.pdf') > -1) {
                    $('#ContentPlaceHolder1_hdnFileNames').val(Filename[4]);
                    document.getElementById('<%= btnDownload.ClientID %>').click();
                }
                event.preventDefault();
            });
        });
    </script>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>View Query Details</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Mange users</a></li><li><a>View Query Details</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                           <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ServiceViewAndTakeAction.aspx"> 
                              <i class="fa fa-plus"></i>View Query Details</a>
                               
                           </div>
                            
                        </div>
                        <div class="panel-body">
                           <div class="form-group">
                            <div class="row">
                            <div class="col-sm-12">
                                <div class="ibox">
                                    <%--                                    <div class="ibox-title">
                                        <h5>
                                            Application Details</h5>
                                    </div>--%>
                                    <div class="ibox-content">
                                        <div class="form-group row">
                                            <label class="col-sm-2">
                                                Department Name</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:Label ID="lblDepartmntName" CssClass="form-control-static" runat="server"></asp:Label>
                                            </div>
                                            <label class="col-sm-2">
                                                Service Name</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:Label ID="lblServiceName" CssClass="form-control-static" runat="server"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2">
                                                Applicant Name</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:Label ID="lblApplicantName" CssClass="form-control-static" runat="server"></asp:Label>
                                            </div>
                                            <label class="col-sm-2">
                                                Application No.</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:Label ID="lblApplicationNo" CssClass="form-control-static" runat="server"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2">
                                                Application Status</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <label class="form-control-static ">
                                                    <asp:Label ID="lblApplicationStatus" CssClass="label label-primary" runat="server"></asp:Label>
                                                </label>
                                            </div>
                                            <label class="col-sm-2">
                                                Download Certificate</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:Label ID="Label7" CssClass="form-control-static" runat="server" Text="NA"></asp:Label>
                                                  <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12" id="dvQueryMain" runat="server">
                                <div class="ibox">
                                    <div class="ibox-title">
                                        <h5>
                                            Query</h5>
                                        <div class="pull-right">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td class="">
                                                            <div class="legendColorBox green">
                                                            </div>
                                                        </td>
                                                        <td class="legendLabel">
                                                            Reverted
                                                        </td>
                                                        <td width="10">
                                                            &nbsp;
                                                        </td>
                                                        <td class=" ">
                                                            <div class="legendColorBox blue">
                                                            </div>
                                                        </td>
                                                        <td class="legendLabel">
                                                            Replay
                                                        </td>
                                                        <td width="10">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="form-group row">
                                            <label class="col-sm-3">
                                                Query Status</label>
                                            <div class="col-sm-8">
                                                <span class="colon">:</span>
                                                <label class="form-control-static">
                                                    <asp:Label ID="lblQueryStatus" CssClass="label label-primary" runat="server"></asp:Label></label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        
                                        
                                        <div class="form-group row">
                                            <div class="col-sm-12">
                                                <h4>
                                                    Query Details</h4>
                                            </div>
                                            <div class="col-sm-12" id="QueryHist" runat="server">
                                            </div>
                                            <asp:Button ID="btnDownload" runat="server" Text="Download" Style="display: none"
                                                OnClick="btnDownload_Click" />
                                            <asp:HiddenField ID="hdnFileNames" runat="server" />
                                            <%--<div class="col-sm-12">
                                                <table style="margin-left: 60px;">
                                                    <tbody>
                                                        <tr>
                                                            <td class="">
                                                                <div class="legendColorBox green">
                                                                </div>
                                                            </td>
                                                            <td class="legendLabel">
                                                                Reverted
                                                            </td>
                                                            <td width="10">
                                                                &nbsp;
                                                            </td>
                                                            <td class=" ">
                                                                <div class="legendColorBox blue">
                                                                </div>
                                                            </td>
                                                            <td class="legendLabel">
                                                                Replay
                                                            </td>
                                                            <td width="10">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="10">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td class="legendColorBox">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td class="legendColorBox">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <div class="messagebox">
                                                    <div class="itemdiv dialogdiv">
                                                        <div class="user">
                                                            <img src="images/user.png" alt="user img">
                                                        </div>
                                                        <div class="body">
                                                            <div class="time">
                                                                <i class="ace-icon fa fa-calendar"></i>12-Sep-2017
                                                            </div>
                                                            <div class="name">
                                                                <a href="#">Er Smt Archana Dash</a>
                                                            </div>
                                                            <div class="form-sec ">
                                                                <div class="form-header">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <div class="legendColorBox blue">
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                Replay
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="form-body">
                                                                    response details
                                                                </div>
                                                            </div>
                                                            <div class="text-right">
                                                                <a href="#" class="btn btn-info btn-sm"><i class="fa fa-download"></i></a>
                                                            </div>
                                                            <!--<div class="text">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque commodo massa sed ipsum porttitor facilisis.</div>-->
                                                        </div>
                                                    </div>
                                                    <div class="itemdiv dialogdiv">
                                                        <div class="user">
                                                            <img src="images/user.png" alt="user img">
                                                        </div>
                                                        <div class="body">
                                                            <div class="time">
                                                                <i class="ace-icon fa fa-calendar"></i>12-Sep-2017
                                                            </div>
                                                            <div class="name">
                                                                <a href="#">Sanghamitra Kumari Samal</a>
                                                            </div>
                                                            <div class="form-sec ">
                                                                <div class="form-header">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <div class="legendColorBox green">
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                Replay
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="form-body">
                                                                    response details
                                                                </div>
                                                            </div>
                                                            <div class="text-right">
                                                                <a href="#" class="btn btn-info btn-sm"><i class="fa fa-download"></i></a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="itemdiv dialogdiv">
                                                        <div class="user">
                                                            <img src="images/user.png" alt="user img">
                                                        </div>
                                                        <div class="body">
                                                            <div class="time">
                                                                <i class="ace-icon fa fa-calendar"></i>12-Sep-2017
                                                            </div>
                                                            <div class="name">
                                                                <a href="#">Er Smt Archana Dash</a>
                                                            </div>
                                                            <div class="form-sec ">
                                                                <div class="form-header">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <div class="legendColorBox blue">
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                Reverted
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="form-body">
                                                                    response details
                                                                </div>
                                                            </div>
                                                            <div class="text-right">
                                                                <a href="#" class="btn btn-info btn-sm"><i class="fa fa-download"></i></a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--Modified By Pranay Kumar on 12-Sept-2017 for Revert Query Management--%>
                                <%--<div class="Querysec">
                                        <h4>
                                            Query</h4>

                                    </div>--%>
                                <%--Modified By Pranay Kumar on 12-Sept-2017 for Revert Query Management--%>
                            </div>
                            <div class="col-sm-12" id="dvQueryMain1" runat="server">
                                <h4 class="nodata" style="margin:8% 0%!important">
                                    No Query</h4>
                            </div>
                        </div>
                        </div>
                     </div>
                  </div>
               </div>
                 <!-- customer Modal1 -->
                
               <!-- /.modal -->
               <!-- Modal -->    
            </section>
        <!-- /.content -->
    </div>
</asp:Content>
