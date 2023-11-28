<%@ Page Language="C#" AutoEventWireup="true" CodeFile="unitdetailsforEINIEM.aspx.cs"
    Inherits="incentives_incentiveoffered" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            });
        });
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:header ID="header" runat="server" />
    <div class="registration-div investors-bg">
        <div id="exTab1" class="container">
            <div class="investrs-tab">
                <ul class="nav nav-pills">
                    <li class="menudashboard"><a href="javascript:void(0)"><i class="fa fa-tachometer"></i>
                        Dashboard</a> </li>
                    <li class="menuprofile"><a href="../InvesterProfile.aspx"><i class="fa fa-user"></i>
                        Investor Profile</a> </li>
                    <li class="menuproposal"><a href="../Proposals.aspx"><i class="fa fa-briefcase"></i>
                        Proposals</a> </li>
                    <li class="menuservices"><a href="../DepartmentClearance.aspx"><i class="fa fa-wrench">
                    </i>Services</a> </li>
                    <%--  <li><a href="IncentiveCalculator.aspx">Incentive Calculator</a> </li>--%>
                    <li class="menuincentive"><a href="incentiveoffered.aspx"><i class="fa fa-money"></i>
                        Incentive</a> </li>
                </ul>
            </div>
            <div class="tab-content clearfix">
                <div class="tab-pane active" id="1a">
                    <div class="form-sec">
                        <div class="innertabs">
                            <ul class="nav nav-pills pull-right">
                                <li ><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                                <li class="active"><a href="appliedindustrylist.aspx">Apply For incentive</a></li>
                                <li><a href="javscript:void(0);">View Application Status</a></li>
                            </ul>
                            <div class="clearfix">
                            </div>
                        </div>
                     <div class="form-header">
                            <div class="iconsdiv">
                                <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                    <i class="fa fa-print"></i></a><a href="javascript:void(0);" title="Export to Excel"
                                        id="A1" class="pull-right printbtn"><i class="fa fa-file-excel-o"></i></a>
                                        <a href="javascript:void(0);" title="Delete" id="A2" class="pull-right printbtn">
                                    <i class="fa fa-trash-o"></i></a>
                                      <p class="pull-right">
                                    <i class="fa fa-check-circle-o text-green"></i>&nbsp; Complete information Available</p>
                                     <p class="pull-right">
                                    <i class="fa fa-times-circle-o text-red"></i>&nbsp;Incomplete Information, Pre-Requisite to Check Eligibility</p>
                            </div>
                            <h2>
                               Units Details</h2>
                        </div>
                        <div class="form-body">
                          <div class="form-group">
                                    <div class="row">
                                    <label class="col-sm-2">Industry Code</label>
                                    <div class="col-sm-4"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Text="12-56-23-56-99999" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-2">Industry Name</label>
                                    <div class="col-sm-4"><span class="colon">:</span><asp:TextBox ID="TextBox2" CssClass="form-control" runat="server" Text="JRD Farma &amp; Research2" ReadOnly="true"></asp:TextBox></div>
                                    </div>
                                    </div>

                         <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                              
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="headingTwo">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#PromoterInformation" aria-expanded="false" aria-controls="collapseTwo"><i
                                                    class="more-less fa  fa-minus"></i> <i class="fa fa-check-circle-o text-green"></i>&nbsp;Basic Details</a>
                                        </h4>
                                    </div>
                                    <div id="PromoterInformation" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingTwo">
                                        <div class="panel-body">
                                           <div class="form-group">
                                    <div class="row">
                                    <label class="col-sm-5">Date (or Proposed Date) of First Fixed Capital Investment</label>
                                    <div class="col-sm-4"><span class="colon">:</span>
                                         <div class="input-group  date datePicker" id="Div1">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="Text1" class="form-control">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                    </div>
                                 
                                    </div>
                                    </div>
                                             <div class="form-group">
                                    <div class="row">
                                  
                                    <label class="col-sm-5">Date (or Proposed Date) of Commnecement of Production <input type="radio" checked="checked" name="optradio"></label>
                                    <div class="col-sm-4"><span class="colon">:</span> <div class="input-group  date datePicker" id="Div4">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="Text4" class="form-control">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div></div>
                                    </div>
                                    </div>
                                       <div class="form-group">
                                    <div class="row">
                                  
                                    <label class="col-sm-2">Sector</label>
                                    <div class="col-sm-4"><span class="colon">:</span>
                                        <asp:DropDownList ID="DropDownList1" CssClass="form-control" readonly="true" runat="server">
                                        <asp:ListItem>Pharmaceuticals</asp:ListItem>
                                        </asp:DropDownList>
                                     </div>
                                       <label class="col-sm-2"><input type="checkbox" value="" checked="checked" readonly="true"> Is Prority</label>
                                   
                                       
  

                                     </div>
                                   
                                    </div>
                                       <div class="form-group">
                                    <div class="row">
                                  
                                    <label class="col-sm-2">District</label>
                                    <div class="col-sm-4"><span class="colon">:</span>
                                        <asp:DropDownList ID="DropDownList2" CssClass="form-control" readonly="true" runat="server">
                                        <asp:ListItem>Khurda</asp:ListItem>
                                        </asp:DropDownList>
                                     </div>
                                     
                                    </div>

                                    </div>
                                       <div class="form-group">
                                    <div class="row">
                                  
                                    <label class="col-sm-2">Unit Type</label>
                                    <div class="col-sm-4"><span class="colon">:</span>
                                        <asp:DropDownList ID="DropDownList3" CssClass="form-control" readonly="true" runat="server">
                                        <asp:ListItem>Large Scale </asp:ListItem>
                                        </asp:DropDownList>
                                     </div>
                                       <label class="col-sm-2"><input type="radio" value="" checked="checked" readonly="true"> Manufacturing</label>
                                    </div>

                                    </div>
                                      <div class="form-group">
                                    <div class="row">
                                  
                                    <label class="col-sm-2">Unit Category</label>
                                    <div class="col-sm-4"><span class="colon">:</span>
                                        <asp:DropDownList ID="DropDownList4" CssClass="form-control" readonly="true" runat="server">
                                        <asp:ListItem>New Unit</asp:ListItem>
                                        </asp:DropDownList>
                                     </div>
                                      <label class="col-sm-2"><input type="radio" value="" checked="checked" readonly="true"> Services</label>
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
                                                </i><i class="fa fa-times-circle-o text-red"></i>&nbsp;Employment Details </a>
                                        </h4>
                                    </div>
                                    <div id="IndustryDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            <div class="form-group">
                                    <div class="row">
                                  
                                    <label class="col-sm-3">Total no. of employees</label>
                                    <div class="col-sm-3"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>
                                     <label class="col-sm-3">Total no. of state domicial employees</label>
                                    <div class="col-sm-3"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>
                                    </div>

                                    </div>
                                      <div class="form-group">
                                    <div class="row">
                                  
                                    <label class="col-sm-3">Total no. of labour</label>
                                    <div class="col-sm-3"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>
                                     <label class="col-sm-3">Count  of state domicial labour</label>
                                    <div class="col-sm-3"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>
                                    </div>

                                    </div>
                                      <div class="form-group">
                                    <div class="row">
                                  
                                    <label class="col-sm-3">Total no. of skilled labour</label>
                                    <div class="col-sm-3"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>
                                     <label class="col-sm-3">Count  of state domicial skilled labour</label>
                                    <div class="col-sm-3"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>
                                    </div>

                                    </div>
                                      <div class="form-group">
                                    <div class="row">
                                  
                                    <label class="col-sm-3">Total no. of unskilled labour</label>
                                    <div class="col-sm-3"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>
                                     <label class="col-sm-3">Count  of state domicial unskilled labour</label>
                                    <div class="col-sm-3"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>
                                    </div>

                                    </div>
                                      <div class="form-group">
                                    <div class="row">
                                  
                                    <label class="col-sm-3">Total no. of Managerial/Admin </label>
                                    <div class="col-sm-3"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>
                                     <label class="col-sm-3">Count  of state domicial Managerial/Admin</label>
                                    <div class="col-sm-3"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                href="#InterestSubsidyDetails" aria-expanded="false" aria-controls="collapseThree">
                                                <i class="more-less fa  fa-plus"></i><i class="fa fa-check-circle-o text-green"></i>&nbsp;Investment / Porposed Investment Details </a>
                                        </h4>
                                    </div>
                                    <div id="InterestSubsidyDetails" class="panel-collapse collapse" role="tabpanel"
                                        aria-labelledby="headingThree">
                                        <div class="panel-body">
                                             <div class="form-group">
                                    <div class="row">
                                  
                                    <label class="col-sm-4">First Fixed Capital Investment (FFCI) <small>(In Cr.)</small></label>
                                    <div class="col-sm-2"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control" Text="90" ReadOnly="true"></asp:TextBox>
                                     </div>
                                     <label class="col-sm-4">Total investment in Fixed Assets (The above field is inclusive of FFCI) <small>(In Cr.)</small></label>
                                    <div class="col-sm-2"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control" Text="130" ReadOnly="true"></asp:TextBox>
                                     </div>
                                    </div>

                                    </div>
                                      <div class="form-group">
                                    <div class="row">
                                  
                                    <label class="col-sm-4">Capital Investment in Plant &amp; Mechinery</label>
                                    <div class="col-sm-2"><span class="colon">:</span>
                                        <asp:TextBox ID="TextBox15" runat="server" CssClass="form-control" Text="90" ReadOnly="true"></asp:TextBox>
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
                                    <asp:Button ID="btnEdit" runat="server" 
                                        Text="Save & Proceed for Eligibility Check" CssClass="btn btn-success" 
                                        onclick="btnEdit_Click" />
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
