<%--'*******************************************************************************************************************
' File Name         : StatusSearch.aspx
' Description       : Status Search
' Created by        : Radhika Rani Patri
' Created On        : 03 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatusDetails.aspx.cs" Inherits="StatusDetails" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <style>
        
    </style>
    <script language="javascript">
        function OTPValidation() {

            debugger;
            if ($('#txtToken').val() == "") {
                alert('Plaese Enter the application No')
                $('#txtToken').focus();
                return false;
            }



        }
    </script>
</head>
<body>
    <form id="form2" runat="server">
    <uc2:header ID="header" runat="server" />
    <div class="pagenavigator">
        <h2>
            <a class="" href="javascript:history.back()"><i class="fa fa-chevron-circle-left"></i>
            </a>Search Status</h2>
    </div>
    <div class="registration-div">
        <div class="container">
            <div class="col-sm-12">
                <div class="panel panel-bd lobidrag">
                    <div class="dy-formview">
                        <div class="dyformheader">
                            <div class="header-details" runat="server" id="divHeader">
                                <%-- <h2>Application Header</h2>
 <p>Government of Odisha</p>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="dyformbody">
                                <div class="panel-heading">
                                    <label for="Iname" class="col-sm-3">
                                        Application Number</label>
                                    <div class="col-sm-3">
                                        <span class="colon">:</span>
                                        <asp:Label ID="Lbl_Application_No" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <label for="Iname" class="col-sm-3">
                                        Current Status</label>
                                    <div class="col-sm-3">
                                        <span class="colon">:</span>
                                        <asp:Label ID="Lbl_Current_Status" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="dyformbody">
                            <div id="frmContent" runat="server">
                            </div>
                            <div>
                                <h4 class="modal-title">
                                    Transactional Details</h4>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Successfull Transaction</div>
                                            <div class="panel-body">
                                                <div id="OrderList" runat="server">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Failure Transaction</div>
                                            <div class="panel-body">
                                                <div id="OrderList1" runat="server">
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
        <div class="col-sm-12" align="center">
            <%--            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" class="btn btn-primary btn-sm" />--%>
            <asp:HyperLink ID="HyperLink1" NavigateUrl="~/StatusSearch.aspx" CssClass="btn btn-success"
                runat="server">Back</asp:HyperLink>
            <%--     <asp:LinkButton ID="LinkButton1" Text="Raise Query" runat="server" class="btn btn-danger btn-sm"
                data-toggle="modal" data-target="#customer1"></asp:LinkButton>--%>
        </div>
        <uc3:footer ID="footer" runat="server" />
        <div class="modal fade" id='customer1' tabindex="-1" role="dialog" aria-hidden="true"
            style="display: none">
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
                                                    <asp:TextBox ID="txtq1" MaxLength="1000" TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                        Width="500px" onkeyup="setvalue();" Onkeypress="return inputLimiter(event,'Address')"
                                                        runat="server"></asp:TextBox>
                                                    <div id="div1stcnt" runat="server" visible="false">
                                                        <i>Maximum <span id="charsLeft" class="mandatoryspan">1000</span> characters left</i></div>
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
                                                    <asp:Label ID="lbla1" runat="server" class="bindlabel" Text="" Width="500px"></asp:Label>
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
                                                    <asp:TextBox ID="txtq2" MaxLength="1000" TextMode="MultiLine" Rows="3" Width="500px"
                                                        CssClass="form-control" Onkeypress="return inputLimiter(event,'Address')" onkeyup="setvalue1();"
                                                        runat="server"></asp:TextBox>
                                                    <div id="div2ndcnt" runat="server" visible="false">
                                                        <i>Maximum <span id="charsLeft1" class="mandatoryspan">1000</span> characters left</i></div>
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
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger btn-sm"
                                                    Style="display: none" data-dismiss="modal" />
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
                        <button type="button" style="display: none" class="btn btn-danger pull-right" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <asp:HyperLink title="Save as Pdf" data-toggle="tooltip" class="pull-right printbtn btn-primary"
            Visible="false" ID="hplPdf" runat="server"><i class="fa fa-file-pdf-o"></i></asp:HyperLink>
    </form>
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="js/loadComponent.js" type="text/javascript"></script>
    <script src="js/CSMValidation.js" type="text/javascript"></script>
    <script src="js/jQuery.alert.js" type="text/javascript"></script>
    <script src="js/jQuery.js" type="text/javascript"></script>
</body>
</html>
