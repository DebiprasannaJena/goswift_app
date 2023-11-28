<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="IssueDetails.aspx.cs" Inherits="Portal_HelpDesk_IssueDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
        function inputLimiter(e, allow) {
            var AllowableCharacters = '';

            if (allow == 'NameCharacters') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'NameCharactersAndNumbers') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'Numbers') {
                AllowableCharacters = '1234567890';
            }
            if (allow == 'Decimal') {
                AllowableCharacters = '1234567890.';
            }
            if (allow == 'Email') {
                AllowableCharacters = '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@@._';
            }
            if (allow == 'Address') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#-,./;\'';
            }
            if (allow == 'DateFormat') {
                AllowableCharacters = '1234567890/-';
            }
            if (allow == 'NumbersSSN') {
                AllowableCharacters = '1234567890-';
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
        function valid() {

            if (document.getElementById('ContentPlaceHolder1_hdnNoofrecord').value == "0") {
                if (document.getElementById('ContentPlaceHolder1_txtq1').value == "") {
                    alert('Initial Query can not be left blank!');
                    document.getElementById('ContentPlaceHolder1_txtq1').focus();
                    return false;
                }
            }
            if (document.getElementById('ContentPlaceHolder1_hdnNoofrecord').value == "2") {
                if (document.getElementById('ContentPlaceHolder1_txtq2').value == "") {
                    alert('Second Set Of Query can not be left blank!');
                    document.getElementById('ContentPlaceHolder1_txtq2').focus();
                    return false;
                }
            }
        }

        function setvalue() {

            $('#charsLeft').html(1000 - $('#ContentPlaceHolder1_txtq1').val().length);
        }
        function setvalue1() {

            $('#charsLeft1').html(1000 - $('#ContentPlaceHolder1_txtq2').val().length);
        }
    </script>
    <style>
    .padding-lr10{padding:4px 15px;}
    @media print
{
 .panel-body{padding:0px 0px;margin:0px -15px}   
 .main-header,.content-header,.main-footer,.back-top{display:none;}   
 .dyformbody h2 { font-size: 15px;  margin: 0px 0px 4px;  padding: 0px 5px; border-bottom: 1px solid #e8e8e8;line-height: 26px;}
 .col-sm-6{ width: 50%;  float: left; }
 .dropdown{display:none;}
 .dyformheader {border: 1px solid #b5b5b5;
}
.dyformbody {
    
    border: 1px solid #b5b5b5;
   margin-top:-1px
}
a[href]:after {content: none !important;}
.btn{display:none;}
    }
    
    </style>
<div class="content-wrapper">
            <!-- Content Header (Page header) -->
            <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Manage Service</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Services</a></li><li><a>Add Service</a></li></ul>
               </div>
            </section>
            <!-- Main content -->
           
             <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidrag">
                     <div class="panel-heading ui-sortable-handle">
                        <div class="dropdown">
                        <ul class="dropdown-menu dropdown-menu-right">
                        <li>
                           <asp:HyperLink title="Save as Pdf" data-toggle="tooltip"    id="hplPdf" runat="server"><i class="panel-control-icon fa fa-file-pdf-o">
                        </i></asp:HyperLink>
                       <%-- <a  data-tooltip="Save as Pdf" data-toggle="tooltip" data-title="Save as Pdf" ><i class="panel-control-icon fa fa-file-pdf-o">
                        </i></a>--%>
                        </li>
                        <li><a  class="PrintBtn" data-tooltip="Print" data-toggle="tooltip" data-title="Print" ><i class="panel-control-icon fa fa-print"></i></a></li>
                      
                        </ul>
                        </div>
                      </div>
 <div class="panel-body">

 <div class="dyformheader">
 <div class="header-details" runat="server" id="divHeader">
<%-- <h2>Application Header</h2>
 <p>Government of Odisha</p>--%>
 </div>
 </div>
  <div  class="dyformbody">
   <div ID="frmContent" runat="server">
   
   </div>

    </div>
    </div>
                     
 </div>  

 
  
    <div class="col-sm-12" align="center" >
      <asp:Button ID="btnBack" runat="server" Text="Back" 
                                                OnClick="btnBack_Click" class="btn btn-add btn-sm" />
    <%--<asp:HyperLink ID="HyperLink1" NavigateUrl="~/Service/ServiceViewAndTakeAction.aspx" CssClass="btn btn-success" runat="server">Back</asp:HyperLink>--%>
<%--     <asp:LinkButton ID="LinkButton1" Text="Raise Query" runat="server" class="btn btn-danger btn-sm"
                data-toggle="modal" data-target="#customer1"></asp:LinkButton>--%>
    </div>
     </div>
                  </div>
</div>
          
            <!-- /.content -->
       <div class="modal fade" id='customer1' tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header modal-header-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×</button>
                    <h3>
                        <i class="fa fa-user m-r-5"></i>Raise Query</h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <form class="form-horizontal">
                            <fieldset>
                                <div class="panel panel-bd ">
                                    <div class="panel-heading">
                                        Raise Query
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-group" id="divQ1" runat="server">
                                            <label class="col-md-2">
                                              Initial Query</label>
                                            <div class="col-md-4">
                                                <%--  <textarea name="address" class="form-control" rows="3"></textarea>--%>
                                                   <asp:Label ID="lblq1" runat="server" class="bindlabel" Text="" Width="500px"></asp:Label>
                                                <asp:TextBox ID="txtq1" MaxLength="1000" TextMode="MultiLine" Rows="3" CssClass="form-control" Width="500px" onkeyup="setvalue();"
                                                    Onkeypress="return inputLimiter(event,'Address')"  runat="server"></asp:TextBox>
                                                    <div id="div1stcnt" runat="server" visible="false">
                                                     <i>Maximum  <span id="charsLeft" class="mandatoryspan">1000</span> characters left</i></div>
                                                <asp:HiddenField ID="hdnNoofrecord" runat="server" />
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group" id="divA1" runat="server">
                                            <label class="col-md-2">
                                          Initial Response From The Investor</label>
                                            <div class="col-md-4">
                                                <%--  <textarea name="address" class="form-control" rows="3"></textarea>--%>
                                                <asp:Label ID="lbla1" runat="server" class="bindlabel" Text="" Width="500px" ></asp:Label>
                                                <%--<asp:TextBox ID="txta1" MaxLength="1000" TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                    Onkeypress="return inputLimiter(event,'Address')" runat="server"></asp:TextBox>--%>
                                               
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group" id="divfile1" runat="server">
                                            <label class="col-md-2">
                                                Files</label>
                                            <div class="col-md-4">
                                                <asp:HyperLink ID="hlDoc1" runat="server" Target="_blank">
                                                    <asp:Image ID="pdficon1" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                        Width="25" Visible="false" />
                                                </asp:HyperLink>
                                                <asp:HyperLink ID="hlDoc2" runat="server" Target="_blank">
                                                    <asp:Image ID="pdficon2" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                        Width="25" Visible="false" /></asp:HyperLink>
                                                <asp:HyperLink ID="hlDoc3" runat="server" Target="_blank">
                                                    <asp:Image ID="pdficon3" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                        Width="25" Visible="false" /></asp:HyperLink>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group" id="divQ2" runat="server">
                                            <label class="col-md-2">
                                                Second Set Of Query</label>
                                            <div class="col-md-4">
                                                <%--  <textarea name="address" class="form-control" rows="3"></textarea>--%>
                                                    <asp:Label ID="lblq2" runat="server" class="bindlabel" Text="" Width="500px"></asp:Label>
                                                <asp:TextBox ID="txtq2" MaxLength="1000" TextMode="MultiLine" Rows="3" Width="500px" CssClass="form-control"
                                                    Onkeypress="return inputLimiter(event,'Address')" onkeyup="setvalue1();" runat="server"></asp:TextBox>
                                                        <div id="div2ndcnt" runat="server" visible="false">
                                                      <i>Maximum  <span id="charsLeft1" class="mandatoryspan">1000</span> characters left</i></div>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group" id="divA2" runat="server">
                                            <label class="col-md-2">
                                                Second Set Of Response From The Investor</label>
                                            <div class="col-md-4">
                                                <%--  <textarea name="address" class="form-control" rows="3"></textarea>--%>
                                                  <asp:Label ID="lbla2" runat="server" class="bindlabel" Text="" Width="500px"></asp:Label>
                                                <%--<asp:TextBox ID="txta2" MaxLength="1000" TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                    Onkeypress="return inputLimiter(event,'Address')" runat="server"></asp:TextBox>--%>
                                            </div>
                                        
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                         <div class="form-group" id="divfile2" runat="server">
                                            <label class="col-md-2">
                                                Files</label>
                                            <div class="col-md-4">
                                                <asp:HyperLink ID="hlDoc4" runat="server" Target="_blank">
                                                    <asp:Image ID="pdficon4" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                        Width="25" Visible="false" />
                                                </asp:HyperLink>
                                                <asp:HyperLink ID="hlDoc5" runat="server" Target="_blank">
                                                    <asp:Image ID="pdficon5" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                        Width="25" Visible="false" /></asp:HyperLink>
                                                <asp:HyperLink ID="hlDoc6" runat="server" Target="_blank">
                                                    <asp:Image ID="pdficon6" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                        Width="25" Visible="false" /></asp:HyperLink>
                                            </div>
                                           
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="col-md-10 col-sm-offset-2 form-group user-form-group">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClientClick="return  valid();"
                                                OnClick="btnSubmit_Click" class="btn btn-add btn-sm" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger btn-sm" style="display:none"
                                                data-dismiss="modal" />
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Text input-->
                            </fieldset>
                            </form>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" style="display:none" class="btn btn-danger pull-right" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>

