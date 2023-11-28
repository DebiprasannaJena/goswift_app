<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpowermentAction.aspx.cs" MasterPageFile="~/MasterPage/Application.master"  Inherits="Portal_Incentive_EmpowermentAction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"
    ClientIDMode="Static">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Incentive</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>View Incentive</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidisable">
                     
                        <div class="panel-body">
                        <div class="search-sec">
                         
                    <asp:updatepanel runat="server">
    <contenttemplate>
    <div class="ibox-content">
                    <div class="clearfix">
                    </div>
        <div class="form-group">
            <div class="row">
                <label class="col-sm-2">
                    Application No.</label>
                <div class="col-sm-4">
                    <label  id="lblAppNo" runat="server">
                    </label>

                </div>
           
            </div>
        </div>

        <div class="form-group">
            <div class="row">
                <label class="col-sm-2">
                    Application Details</label>
                <div class="col-sm-4">
                        Click to view
                        <asp:HyperLink ID="hypView" runat="server" CssClass="btn btn-primary" Target="_blank"  class="form-control"><i class="fa fa-file-text-o" ></i></asp:HyperLink>
                      <%-- <asp:LinkButton ID="LinkButton1" runat="server"  
                        style="margin-top:5px;" onclick="LinkButton1_Click" ></asp:LinkButton>--%>
                </div>
                
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <label class="col-sm-2">
                    Unit Name</label>
                    
                <div class="col-sm-4">
                    <label id="lblUnitName" runat="server">
                    </label>
                </div>
            </div>
        </div>
       
        <div class="form-group">
            <div class="row">
                <label class="col-sm-2">
                    Incentive Name</label>
                <div class="col-sm-4">
                    <label id="lblIncentive" runat="server">
                    </label>
                </div>
            </div>
        </div>
            <div class="form-group">
                <div class="row">
                    <label class="col-sm-2">
                        PC Date</label>
                    <div class="col-sm-4">
                        <div class="input-group  date datePicker" id="Div5">
                                        <input name="txtPCDate" type="text" id="txtPCDate" class="form-control" runat="server" disabled="disabled" />
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                    </div>
                </div>
            </div>    
            <div class="form-group">
                <div class="row">
                    <label class="col-sm-2">
                        Reason For Delay</label>
                    <div class="col-sm-4">
                        <div class="">
                             <asp:TextBox ID="txtReason" Rows="5"  Enabled="false"   style="overflow:scroll;"
                                TextMode="MultiLine" MaxLength="250" CssClass="form-control" runat="server" ></asp:TextBox>                          
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group" id="dvSanctionUp" runat="server">
            <div class="row">
            <label class="col-sm-2">                
                <div id="DivStTypeText" runat="server">View Document(in Support of Delay)</div>     
            </label>
            <div class="col-sm-4">                         
                 <asp:HyperLink ID="hypReason" runat="server" CssClass="btn btn-primary" Target="_blank"  class="form-control"><i class="fa fa-file-text-o" ></i></asp:HyperLink>
            </div>
            <div class="clearfix">
            </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <label class="col-sm-2">
                    Status <span style="color:Red;" >*</span></label>
                <div class="col-sm-4">      
                              
                            <asp:DropDownList ID="ddlStatusPop" CssClass="form-control" runat="server">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Approve" Value="2" />
                                <asp:ListItem Text="Reject" Value="3" />
                            </asp:DropDownList>        
                            <asp:HiddenField ID="hdnDocText" runat="server" />   
                            <asp:HiddenField ID="hdnAvailType" runat="server" />   
                            <asp:HiddenField ID="hdnProvision" runat="server" />          
                </div>
            </div>
        </div>
        <div class="form-group">
                <div class="row">
                    <label class="col-sm-2">
                        Remark  <span style="color:Red;" >*</span></label>
                    <div class="col-sm-4">
                        <div class="">
                            <asp:TextBox ID="txtRemark" Rows="5" onKeyUp="limitText(this,this.form.count,250);"
                                TextMode="MultiLine" MaxLength="250" CssClass="form-control" runat="server" onKeyDown="limitText(this,this.form.count,250);"></asp:TextBox>
                            <a href="#" data-toggle="tooltip" class="fieldinfo" title="It can accept both alphabets and numbers.Special Characters like !  # $ % & ( )  + , - . / : ; = ? @ [ \ ] are allowed">
                                <i class="fa fa-question-circle" aria-hidden="true"></i></a><small><small>Maximum
                                    <input name="count" class="inputCss" readonly="readonly" style="font-weight: bold;
                                        color: red; width: 26px;" type="text" value="250" />
                                    Characters Left</small>
                                    <cc1:filteredtextboxextender id="FilteredTextBoxExtender9" runat="server" enabled="True"
                                        filtertype="Custom,UppercaseLetters,LowercaseLetters,Numbers" validchars="!# $ % & ( ) + , - . / : ; = ? @ [ \ ]  "
                                        filtermode="ValidChars" targetcontrolid="txtRemark">
                                    </cc1:filteredtextboxextender>
                        </div>
                    </div>
                </div>
            </div>
        <div class="form-group">
            <div class="row">
                <div class="col-sm-9 col-sm-offset-2">
                    <div class="row">
                        <div class="col-sm-9 col-sm-offset-2">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnSubmit_Click"
                                OnClientClick="return validation();" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" 
                                onclick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </contenttemplate>
</asp:updatepanel>
                                           
               
                              
                        </div>

                         
                            <div class="clearfix">
                            </div>    
                        </div>
                     </div>
                  </div>
               </div>
        </section>
        <!-- customer Modal1 -->
        <!-- /.modal-dialog -->
    </div>
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script type="text/javascript">

        function pageLoad() {
            var txtDesc = $("#txtRemark").val().length;
            $('.inputCss').val(250 - txtDesc);



        }




        $(window).load(function () {

            $('#btnSave').click(function () {
                var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';


                if (DropDownValidation('ddlStatusPop', '0', 'Status', projname) == false) {
                    $("#ddlStatusPop").focus();
                    return false;
                };
                var isprovisional = '<%=Request.QueryString["isprovisional"]%>';

                var IncCode = '<%=Request.QueryString["code"]%>';

                if (isprovisional == "0" || isprovisional == "3") {
                    if ($("#ddlStatusPop").val() == "2") {
                        if ($("#fupSanctionDoc").val() == "") {
                            //alert("Sanction Order Document cannot be left blank!");
                            jAlert('Sanction Order Document cannot be left blank.', 'SWP');

                            return false;
                        }
                    }

                    if ($("#ddlStatusPop").val() == "2") {
                        if ($("#fupSanctionDoc").val() != "") {
                            if (ValidFileExtentionAndSize('fupSanctionDoc', 'pdf,xls,doc,docx,xlsx', projname, 4, 'MB') == false) {
                                $("#fupSanctionDoc").focus();
                                return false;
                            }
                        }
                    }
                }


                if (isprovisional == "1" && IncCode == "10100110") {
                    if ($("#fupProvisional").val() == "") {
                        //alert("Provisional Certificate cannot be left blank!");
                        jAlert('Provisional Certificate cannot be left blank.', 'SWP');
                        return false;
                    }
                }

                if (blankFieldValidation('txtRemark', 'Remark', projname) == false) {
                    $("#txtRemark").focus();
                    return false;
                };
                if (WhiteSpaceValidation1st('txtRemark', 'Remark', projname) == false) {
                    $("#txtRemark").focus();
                    return false;
                };
                return ConfirmAction('btnSave');
            });
        });

        function limitText(limitField, limitCount, limitNum) {
            if (limitField.value.length > limitNum) {
                limitField.value = limitField.value.substring(0, limitNum);
            } else {
                limitCount.value = limitNum - limitField.value.length;
            }
        }

        function ConfirmAction(cntr) {
            var strValue = $('#' + cntr).val();
            if (strValue == 'Update') {

                if (!confirm('Are You Sure To Submit?')) {
                    return false;
                }
                else {
                    return true;
                }

            }
            else {
                if (!confirm('Are You Sure To Save?')) {
                    return false;
                }
                else {
                    return true;
                }
            }
        }


     

        function alertredirect(msg) {
            jAlert('Action Taken Successfully !', 'GO-SWIFT', function (r) {

                if (r) {

                    location.href = 'ViewEMPCommitteApproval.aspx' + msg;
                    return true;
                }
                else {
                    return false;
                }
            });
        }
    </script>
</asp:Content>
