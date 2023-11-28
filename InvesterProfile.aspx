<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvesterProfile.aspx.cs"
    Inherits="InvesterProfile" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/jquery.mCustomScrollbar.min.css" rel="stylesheet" type="text/css" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <style></style>
    <script>

        $(document).ready(function () {

            $('.menuprofile').addClass('active');
        });
        //        function EditFunc() {
        //            location.replace("InvestorRegistration.aspx");
        //            return false;
        //        }
    </script>
    <script src="js/jQuery.alert.js" type="text/javascript"></script>
    <link href="css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="js/WebValidation.js" type="text/javascript"></script>
</head>
<body>
    <form id="form2" runat="server">
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="registration-div investors-bg">
            <div id="exTab1" class="">
                <div class="investrs-tab">
                    <div class="row">
                        <div class="col-sm-12">
                            <uc4:investoemenu ID="ineste" runat="server" />
                        </div>
                        <!--div class="col-sm-4">
                            <span class="pull-right" style="margin-top: 10px;"><b>Last Login:</b>
                                <asp:Label ID="lblLastlogin" runat="server" CssClass="lablespan text-primary"></asp:Label>
                            </span>
                        </div-->
                    </div>
                </div>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="1a">
                        <div class="form-sec">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="ibox">
                                        <div class="ibox-title">
                                            <h5>
                                                <i class="fa fa-user"></i>&nbsp; Investor Details</h5>
                                            <div class="ibox-tools">
                                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-success btn-sm "
                                                    OnClientClick="return EditFunc();" OnClick="btnEdit_Click" />
                                                <%-- <a href="#" ID="A1" runat="server"  OnClientClick="return EditFunc();" OnClick="btnEdit_Click" class="btn btn-success btn-sm pull-right"><i class="fa fa-pencil"></i> Edit</a>--%>
                                            </div>
                                        </div>
                                        <div class="ibox-content">
                                            <div class="investorprofile">
                                                <div class="form-horizental">
                                                    <!--<div class="col-sm-2 padding-left-0">
                      <div class="img-prev">-->
                                                    <%--<img src="images/default_user.png" alt="user-demo" />--%>
                                                    <!--<asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
                        <img runat="server" id="imgInvestor" class="img-shadow" alt="Upload Image"
                                        /> </div>
                    </div>-->
                                                    <div class="form-group row">
                                                        <label class="col-sm-4">
                                                            Unit Name</label>
                                                        <div class="col-sm-7">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblName" runat="server" CssClass="lablespan" Text='<%#Eval("VCH_INV_NAME") %>'></asp:Label>
                                                        </div>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-sm-4">
                                                            Email Id</label>
                                                        <div class="col-sm-7">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblEmail" runat="server" CssClass="lablespan" Text='<%#Eval("VCH_EMAIL") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-sm-2" style="display: none;">
                                                            Country</label>
                                                        <div class="col-sm-4" style="display: none;">
                                                            <%--<label for="fname" class="bindlabel">
                                            India
                                        </label>--%>
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblCountry" runat="server" CssClass="lablespan" Text='<%#Eval("INT_COUNTRY") %>'></asp:Label>
                                                        </div>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-sm-4">
                                                            Contact Person</label>
                                                        <div class="col-sm-7" style="padding-right: 0px">
                                                            <%--<label for="fname" class="bindlabel">
                                            Mr
                                        </label>--%>
                                                            <span class="colon">:</span>
                                                            <%--<asp:Label ID="lblSalutation" runat="server" CssClass="lablespan" Text='<%#Eval("INT_SALUTATION") %>'></asp:Label>--%>
                                                            <asp:Label ID="lblFirstName" runat="server" CssClass="lablespan" Text='<%#Eval("VCH_CONTACT_FIRSTNAME") %>'></asp:Label>
                                                            <asp:Label ID="lblMiddleName" runat="server" CssClass="lablespan" Text='<%#Eval("VCH_CONTACT_MIDDLENAME") %>'></asp:Label>
                                                            <asp:Label ID="lblLastName" runat="server" CssClass="lablespan" Text='<%#Eval("VCH_CONTACT_LASTNAME") %>'></asp:Label>
                                                        </div>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-sm-4">
                                                            Mobile Number</label>
                                                        <div class="col-sm-7">
                                                            <%--<label for="fname" class="bindlabel">
                                            9341957393</label>--%>
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblMobile" runat="server" CssClass="lablespan" Text='<%#Eval("VCH_OFF_MOBILE") %>'></asp:Label>
                                                        </div>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-sm-4">
                                                            Address</label>
                                                        <div class="col-sm-7">
                                                            <%--<label for="fname" class="bindlabel">
                                            N1/231,Mumbai</label>--%>
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblAddress" runat="server" CssClass="lablespan" Text='<%#Eval("VCH_ADDRESS") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-sm-5" style="display: none;">
                                                            User Name</label>
                                                        <div class="col-sm-7" style="display: none;">
                                                            <%--label for="fname" class="bindlabel">
                                            alexhales</label>--%>
                                                            <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("VCH_INV_USERID") %>'></asp:Label>
                                                        </div>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-sm-4">
                                                            Registration Date</label>
                                                        <div class="col-sm-7">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblRegDate" runat="server" CssClass="lablespan"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                        <!--<div class="ibox-footer">
                                            <div class="row">
                                                <div class="col-sm-12 text-center">
                                                    
                                                </div>
                                            </div>                                        
                                        </div>-->
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="ibox" style="margin-top: 0;">
                                        <div class="ibox-title">
                                            <h5>
                                                <i class="fa fa-briefcase"></i>&nbsp; Approved Proposal</h5>
                                            <div class="ibox-tools">
                                                <a href="PEAL/PromoterDetails.aspx" class="btn btn-success btn-sm ">Create Proposal</a>
                                                <a href="DraftedProposals.aspx" class="btn btn-danger btn-sm ">Drafted Proposals</a>
                                            </div>
                                        </div>
                                        <div class="ibox-content">
                                            <div class="contentbox">
                                                <div class="form-horizental">
                                                    <div class="form-group row">
                                                        <label class="col-sm-4">
                                                            Proposal No.</label>
                                                        <div class="col-sm-7">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Label1" runat="server" CssClass="lablespan">201709001	</asp:Label>
                                                        </div>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-sm-4">
                                                            Name of Unit</label>
                                                        <div class="col-sm-7">
                                                            <span class="colon">:</span>
                                                            <asp:Label runat="server" CssClass="lablespan">Sanjay Birla Group</asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-sm-4">
                                                            Industries Type</label>
                                                        <div class="col-sm-7">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Label2" runat="server" CssClass="lablespan">Large</asp:Label>
                                                        </div>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-sm-4">
                                                            Unit Location</label>
                                                        <div class="col-sm-7">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Label8" runat="server" CssClass="lablespan">BBSR</asp:Label>
                                                        </div>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-sm-4">
                                                            Corporate Office</label>
                                                        <div class="col-sm-7">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Label9" runat="server" CssClass="lablespan">Bhubaneswar</asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-sm-4">
                                                            Promoter</label>
                                                        <div class="col-sm-7">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Label11" runat="server" CssClass="lablespan">Sekhar Vijlani</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="ibox" style="margin: 0;">
                                        <div class="ibox-title">
                                            <h5>
                                                <i class="fa fa-wrench"></i>&nbsp; Availed Services</h5>
                                            <div class="ibox-tools">
                                                <a href="ApplicationDetails.aspx" class="btn btn-success btn-sm">Application Details</a>
                                            </div>
                                        </div>
                                        <!--<div>-->
                                        <div class="ibox-content">
                                            <div class="contentbox">
                                                <table class="table table-striped" style="margin: 0;">
                                                    <tbody>
                                                        <tr>
                                                            <th>
                                                                Department
                                                            </th>
                                                            <th>
                                                                Services
                                                            </th>
                                                            <th>
                                                                Renewal Date
                                                            </th>
                                                            <th width="40">
                                                                View
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Department of Energy
                                                            </td>
                                                            <td>
                                                                <a href="#">Power Connection Application</a>
                                                            </td>
                                                            <td>
                                                                10-Jan-2017
                                                            </td>
                                                            <td>
                                                                <a href="#" class="btn btn-info btn-xs"><i class="fa fa-eye"></i></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Food Supplies and Consumer ...
                                                            </td>
                                                            <td>
                                                                <a href="#">Licences as manufature of ...</a>
                                                            </td>
                                                            <td>
                                                                10-Jan-2017
                                                            </td>
                                                            <td>
                                                                <a href="#" class="btn btn-info btn-xs"><i class="fa fa-eye"></i></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Housing and Urban ...
                                                            </td>
                                                            <td>
                                                                <a href="#">Trade licensing</a>
                                                            </td>
                                                            <td>
                                                                10-Jan-2017
                                                            </td>
                                                            <td>
                                                                <a href="#" class="btn btn-info btn-xs"><i class="fa fa-eye"></i></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Home Department
                                                            </td>
                                                            <td>
                                                                <a href="#">Application for Fire Safety ...</a>
                                                            </td>
                                                            <td>
                                                                10-Jan-2017
                                                            </td>
                                                            <td>
                                                                <a href="#" class="btn btn-info btn-xs"><i class="fa fa-eye"></i></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Forest and Environment ...
                                                            </td>
                                                            <td>
                                                                <a href="#">NOC for tree felling and tree ...</a>
                                                            </td>
                                                            <td>
                                                                10-Jan-2017
                                                            </td>
                                                            <td>
                                                                <a href="#" class="btn btn-info btn-xs"><i class="fa fa-eye"></i></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Forest and Environment ...
                                                            </td>
                                                            <td>
                                                                <a href="#">Tree Transit permission</a>
                                                            </td>
                                                            <td>
                                                                10-Jan-2017
                                                            </td>
                                                            <td>
                                                                <a href="#" class="btn btn-info btn-xs"><i class="fa fa-eye"></i></a>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="ibox">
                                        <div class="ibox-title">
                                            <h5>
                                                <i class="fa fa-money"></i>&nbsp; Incentive</h5>
                                            <div class="ibox-tools">
                                                <a href="#" class="btn btn-success btn-sm ">View Details</a>
                                            </div>
                                        </div>
                                        <div class="ibox-content">
                                            <div class="contentbox">
                                                <h2 class="nodata">
                                                    No Data Available</h2>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--<div class="fixedslide-btn">
			    <ul>
                    <li > <a href="javascript:void(0);" title="twitter" ><i class="fa fa-user"></i> <span>Investor Profile</span></a></li>
                    <li > <a href="javascript:void(0);" title="facebook" ><i class="fa fa-briefcase"></i> <span> Proposals</span></a></li>
                    <li ><a href="javascript:void(0);" title="linkedin" ><i class="fa fa-wrench"></i> <span> Services</span> </a></li>
                    <li ><a href="javascript:void(0);" title="youtube" ><i class="fa fa-money"></i> <span>Incentive</span> </a> </li>
              </ul>
        </div>-->
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
    <script>
        (function ($) {
            $(window).on("load", function () {
                $(".contentbox").mCustomScrollbar();
            });
        })(jQuery);

        $(document).ready(function (e) {


        });		
    </script>
</body>
</html>
