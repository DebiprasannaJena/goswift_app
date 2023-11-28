<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="UpdateQueryDate.aspx.cs" Inherits="Portal_SuperAdmin_UpdateQueryDate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
      <script src="../js/WebValidation.js" type="text/javascript"></script>
     
      <script type="text/javascript">

          var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

          function Validate() {

              if (blankFieldValidation('ContentPlaceHolder1_Txt_Proposal_No', 'Proposal No', projname) == false) {
                  $("#ContentPlaceHolder1_Txt_Proposal_No").focus();
                  return false;
              };
              if (WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_Proposal_No', 'Proposal No', projname) == false) {
                  $("#ContentPlaceHolder1_Txt_Proposal_No").focus();
                  return false;
              };

              if (WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_UpdatePeal_Query_Date', 'Query Date Update', projname) == false) {
                  $("#ContentPlaceHolder1_Txt_UpdatePeal_Query_Date").focus();
                  return false;
              };
              // alert('hello');

            

          function ValidateTo() {
              if (blankFieldValidation('ContentPlaceHolder1_Txt_Service_No', 'Service No', projname) == false) {
                  $("#ContentPlaceHolder1_Txt_Service_No").focus();
                  return false;
              };
              if (WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_Service_No', 'Service No', projname) == false) {
                  $("#ContentPlaceHolder1_Txt_Service_No").focus();
                  return false;
              };

          }
       
      </script>
    <script type="text/javascript">
              function inputLimiter(e, allow) {
                  var AllowableCharacters = '';

                  if (allow == 'Numbers') {
                      AllowableCharacters = '1234567890';
                  }

                  var k;
                  k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
                  if (k != 13 && k != 8 && k != 0) {
                      if ((e.ctrlKey == false) && (e.altKey == false)) {
                          return (AllowableCharacters.indexOf(String.fromCharCode(k)) != -1);
                      }
                      else {
                          return true;
                      }
                  }
                  else {
                      return true;
                  }
              }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datePicker').datepicker({
                format: 'dd-mm-yyyy 00:00:00',
                autoclose: true,
               
            });
        })
        
    </script>

    <link href="../../css/bootstrap-datetimepicker.css" rel="stylesheet" />
    <script src="../../js/bootstrap-datetimepicker.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">       
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>Update Query Date</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Admin Privileges</a></li>
                    <li><a>Update Query Date</a></li>
                </ul>
            </div>
        </div>
        <div class="content">
            <div class="row">                
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable" id="DivSelect">
                        <div class="panel-body">
                                                      
                                   <div id="DivSelectType" runat="server">
                                         <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="Type4">
                                                    Select Query Date Update <span class="text-red">*</span></label>
                                            </div>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                 <asp:RadioButtonList ID="Rdbtn_Select_Type" runat="server" OnSelectedIndexChanged="Rdbtn_Select_Type_SelectedIndexChanged" RepeatLayout="Flow" RepeatDirection="Horizontal" AutoPostBack="true">
                                               <asp:ListItem class="radio-inline" Text="Peal Type" Value="1" />
                                                <asp:ListItem class="radio-inline" Text="Service Type" Value="2" />
                                                </asp:RadioButtonList>
                                               
                                            </div>
                                        </div>
                                       </div>
                                  </div>
                                 <div>
                        </div>
                    </div>
                    <div id="DivPeal" runat="server">
                    <div class="panel panel-bd lobidisable"  visible="true">
                        <div class="panel-body">


                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                Enter Proposal Number</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Proposal_No" MaxLength="10" runat="server" Onkeypress="return inputLimiter(event,'Numbers')" placeholder="Type Proposal No. Here" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:Button ID="Btn_Search" runat="server"  CssClass="btn btn-primary" Text="Search" OnClick="Btn_Search_Click" OnClientClick="return Validate();" /> 
                                            <asp:Button ID="Btn_Reset" runat="server" CssClass="btn btn-warning" Text="Reset" OnClick="Btn_Reset_Click"/>
                                            <asp:Label ID="Lbl_Msg" runat="server" CssClass="text-danger"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div id="divNorecordPeal" runat="server" visible="false">
                                       <asp:Label ID="txt_Pealnorecord" runat="server" CssClass="text-danger"></asp:Label>
                                    </div>

                                    <div id="DivQueryUpdateDetails" runat="server">

                                        <div class="form-group row">
                                            <div class="col-sm-12">
                                                <h4 class="h4-header">Applicant Details
                                                </h4>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="User">
                                                    Proposal No</label>
                                            </div>
                                            <div class="col-sm-10">
                                                <span class="colon">:</span>
                                                <asp:Label ID="Lbl_Proposal_No" runat="server" CssClass="form-control-static" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="User">
                                                    Query Raise Date</label>
                                            </div>
                                            <div class="col-sm-10">
                                                <span class="colon">:</span>
                                                <asp:Label ID="Lbl_Query_Date" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="User">
                                                    No. Of Times Query Raise</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:Label ID="Lbl_timesofquery_Raise" runat="server" CssClass="form-control-static" Font-Bold="true" ForeColor="Red"></asp:Label>
                                            </div>
                                            <div class="col-sm-7">
                                                 <label for="User">                                             
                                                     Before updating <span style="color: Red; font-weight: bold;">Peal Query Date </span> take consent(approval) from Department through email.
                                               </label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-sm-12">
                                                <h4 class="h4-header">Update Query Date
                                                </h4>
                                            </div>
                                        </div>
                                    
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="Type4">
                                                    Update Query Date<span class="text-red">*</span></label>
                                            </div>

                                            <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <div class="input-group  date datePicker" id="datePicker1">
                                                        <asp:TextBox ID="Txt_Peal_Query_Date" CssClass="form-control" runat="server" AutoCompleteType="None"
                                                            autoComplete="off"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    </div>
                                                </div>

                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                            </div>
                                            <div class="col-sm-10">
                                                <asp:Button ID="Btn_Update" runat="server" Text="Update" CssClass="btn btn-success" OnClick="Btn_Update_Click"  onClientClick=" return confirm('Are you sure Want To Update?')"/>
                                                <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="Btn_Cancel_Click"  OnClientClick=" return confirm('Are you sure Want To cancel?')"/>                                               
                                            </div>
                                        </div>
                                    </div>
                                 </div>                       
                        </div>
                        <div>

                        </div>
                    </div>
                    </div>
                    <div id="DivService" runat="server">
                     <div class="panel panel-bd lobidisable"  visible="true" >
                        <div class="panel-body">
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                Enter Service Application  Number</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Service_No" MaxLength="16"  Onkeypress="return inputLimiter(event,'Numbers')" runat="server" placeholder="Type Service Application No. Here" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:Button ID="Btn_Search_Service" runat="server" CssClass="btn btn-primary" OnClick="Btn_Search_Service_Click" Text="Search" OnClientClick="return ValidateTo();"/>
                                            <asp:Button ID="Btn_Reset_Service" runat="server" CssClass="btn btn-warning" OnClick="Btn_Reset_Service_Click" Text="Reset"/>
                                            <asp:Label ID="Label1" runat="server" CssClass="text-danger"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                       <div id="divNorecordService" runat="server" visible="false">
                                       <asp:Label ID="Txt_ServicenoRecord" runat="server" CssClass="text-danger"></asp:Label>
                                    </div>

                                    <div id="Div_PealDetails" runat="server">

                                        <div class="form-group row">
                                            <div class="col-sm-12">
                                                <h4 class="h4-header">Applicant Details
                                                </h4>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="User">
                                                    Service Application No</label>
                                            </div>
                                            <div class="col-sm-10">
                                                <span class="colon">:</span>
                                                <asp:Label ID="Lbl_Sevice_No" runat="server" CssClass="form-control-static" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="User">
                                                    Query Raise Date</label>
                                            </div>
                                            <div class="col-sm-10">
                                                <span class="colon">:</span>
                                                <asp:Label ID="Lbl_Service_Query_Raise_Date" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="User">
                                                    No. Of Times Query Raise</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:Label ID="Lbl_Service_Times_Query" runat="server" CssClass="form-control-static" Font-Bold="true" ForeColor="Red"></asp:Label>
                                            </div>
                                            <div class="col-sm-7">
                                                 <label for="User">                                             
                                                     Before updating <span style="color: Red; font-weight: bold;">Query Date of Service</span> take consent(approval) from Department through email.
                                               </label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-sm-12">
                                                <h4 class="h4-header">Update Query Date
                                                </h4>
                                            </div>
                                        </div>
                                     
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="Type4">
                                                    Update Query Date<span class="text-red">*</span></label>
                                            </div>
                                            <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <div class="input-group  date datePicker" id="datePicker2">
                                                        <asp:TextBox ID="Txt_Service_Query_Date" CssClass="form-control" runat="server" AutoCompleteType="None"
                                                            autoComplete="off"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    </div>
                                                </div>

                                   

                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                            </div>
                                            <div class="col-sm-10">
                                                <asp:Button ID="Btn_Service_Update" runat="server" Text="Update" OnClick="Btn_Service_Update_Click" CssClass="btn btn-success" OnClientClick="return confirm('Are you sure Want To Update?')" />
                                                <asp:Button ID="Btn_Service_Cancel" runat="server" Text="Cancel" OnClick="Btn_Service_Cancel_Click" CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure Want To Cancel?')"  />                                               
                                            </div>
                                        </div>
                                    </div>
                                        </div>

                        </div>
                        <div>

                        </div>
                    </div>
                    </div>
                    
                </div>
            </div>
        </div>
    </div>
</asp:Content>
 
