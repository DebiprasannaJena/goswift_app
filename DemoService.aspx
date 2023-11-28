<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DemoService.aspx.cs" Inherits="DemoService" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SWP</title>
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="css/animate.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="js/jQuery.alert.js" type="text/javascript"></script>
    <link href="css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="js/WebValidation.js" type="text/javascript"></script>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="table-responsive" id="divGrd" style="margin-top: 15px;" align="center">
            <table class="table table-bordered bg-white">
                <tr>
                    <th>
                        Sl#.
                    </th>
                    <th>
                        Department
                    </th>
                    <th>
                        Services
                    </th>
                    <th>
                        Application Fee
                    </th>
                    <th>
                        Apply Now
                    </th>
                </tr>
                <tr>
                    <td>
                        1
                    </td>
                    <td>
                        Housing and Urban Development Department (H UD)
                    </td>
                    <td>
                        Trade licensing
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:HyperLink ID="hypTradeApply" runat="server" Target="_blank" NavigateUrl="http://117.240.239.40/or/ulb/citizen-services?p_p_id=eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF&p_p_lifecycle=1&p_p_state=normal&p_p_mode=view&p_p_col_id=column-1&p_p_col_count=1&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__spage=%2Fportlet_action%2FEmunicipality_portlet%2Ftradelicense-status%2Faction&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__sorig=%2Fportlet_action%2FEmunicipality_portlet%2Ftl%2Frender&ulbCode=ULB001&swpId=investor1&proposalId=201709002" Text="Apply" CssClass="btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                    </td>
                </tr>
                  <tr>
                    <td>
                        2
                    </td>
                    <td>
                       Housing and Urban Development Department (H UD)
                    </td>
                    <td>
                        Building Plan Approval System
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#"
                            Text="Apply" CssClass="btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        3
                    </td>
                    <td>
                        Health and Family Welfare Department (H and FW)
                    </td>
                    <td>
                        Retail / Bulk Drug License & renewal of Bulk Drug License
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:HyperLink ID="hypRetailApply" runat="server" Target="_blank" NavigateUrl="http://117.247.252.220:8484/dcodisha/dcservice.php?ServiceID=30&UserId=investor10&ProposalNo=201709002"
                            Text="Apply" CssClass="btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                       4
                    </td>
                    <td>
                        Health and Family Welfare Department (H and FW)
                    </td>
                    <td>
                        Drug license for setting up a pharmacy in State
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:HyperLink ID="hypPharmaApply" runat="server" Target="_blank" NavigateUrl="http://117.247.252.220:8484/dcodisha/dcservice.php?ServiceID=31&UserId=investor10&ProposalNo=201709002" Text="Apply" CssClass="btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        5
                    </td>
                    <td>
                        Health and Family Welfare Department (H and FW)
                    </td>
                    <td>
                        Wholesale drug license
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:HyperLink ID="hypWholesaleApply" runat="server" Target="_blank" NavigateUrl="http://117.247.252.220:8484/dcodisha/dcservice.php?ServiceID=32&UserId=investor10&ProposalNo=201709002" Text="Apply"
                            CssClass="btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        6
                    </td>
                    <td>
                        Forest and Environment Department
                    </td>
                    <td>
                        NOC for tree felling and tree transit from Tree Authority/ Appropriate Authority
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:HyperLink ID="hypNOCTreeFellingApply" runat="server" Target="_blank" NavigateUrl="http://117.247.252.221/ttpermit_subrat/users/swpLogin?ServiceID=25&UserId=investor10&ProposalNo=201709002" Text="Apply"
                            CssClass="btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                       7
                    </td>
                    <td>
                        Forest and Environment Department
                    </td>
                    <td>
                        Tree Transit permission
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:HyperLink ID="hypTreeTransit" runat="server" Target="_blank" NavigateUrl="http://117.247.252.221/ttpermit_subrat/users/swpLogin?ServiceID=26&UserId=investor10&ProposalNo=201709002" Text="Apply" CssClass="btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                       8
                    </td>
                    <td>
                        Forest and Environment Department
                    </td>
                    <td>
                        Inspection by AppropriateAuthority for felling trees NEW
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:HyperLink ID="hypTreeFelling" runat="server" Target="_blank" NavigateUrl="http://117.247.252.221/ttpermit_subrat/users/swpLogin?ServiceID=27&UserId=investor10&ProposalNo=201709002" Text="Apply" CssClass="btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                      9
                    </td>
                    <td>
                       Revenue and Disaster Management Department (R DM)
                    </td>
                    <td>
                       Registration of Partnership firms
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:HyperLink ID="hypPartnershipApply" runat="server" Target="_blank" NavigateUrl="http://igrodisha.gov.in/swp_firm/Admin/Firm/FirmDtl.aspx?UserId=investor10&ProposalId=201709001" Text="Apply" CssClass="btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                      10
                    </td>
                    <td>
                       Commercial Tax Organization
                    </td>
                    <td>
                       Registration for Professional Tax
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:LinkButton ID="hypProfessionalTaxApply" runat="server" Text="Apply" 
                            CssClass="btn  btn-success btn-sm" onclick="hypProfessionalTaxApply_Click"><i class="fa fa-check-square"></i></asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td>
                       11
                    </td>
                    <td>
                       Odisha State Pollution Control Board
                    </td>
                    <td>
                       Consent to Establish under Water Act, 1974 and Air Act, 1981
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:HyperLink ID="hypPollutionEUW" runat="server" Target="_blank" NavigateUrl="http://164.100.163.18/OSPCB/industryRegMaster/singleWindowIndustry?ServiceID=43&UserId=investor10&ProposalId=201709001" Text="Apply" CssClass="btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                       12
                    </td>
                    <td>
                       Odisha State Pollution Control Board
                    </td>
                    <td>
                       Consent to Operate under Water Act, 1974 and Air Act, 1981
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:HyperLink ID="hypPollutionEUA" runat="server" Target="_blank" NavigateUrl="http://164.100.163.18/OSPCB/industryRegMaster/singleWindowIndustry?ServiceID=45&UserId=investor10&ProposalId=201709001" Text="Apply" CssClass="btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                       13
                    </td>
                    <td>
                       Odisha State Pollution Control Board
                    </td>
                    <td>
                       Authorization under Hazardous Waste (Management and Handling) Rules, 1989
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:HyperLink ID="hypPollutionOUW" runat="server" Target="_blank" NavigateUrl="http://164.100.163.18/OSPCB/industryRegMaster/singleWindowIndustry?ServiceID=50&UserId=investor10&ProposalId=201709001" Text="Apply" CssClass="btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                    </td>
                </tr>
               <%-- <tr>
                    <td>
                       14
                    </td>
                    <td>
                       Odisha State Pollution Control Board
                    </td>
                    <td>
                       Consent to Operate under Air Act, 1981
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:HyperLink ID="hypPollutionOUA" runat="server" Target="_blank" NavigateUrl="http://164.100.163.18/OSPCB/industryRegMaster/create1?ServiceID=46&UserId=investor10&ProposalId=201709001" Text="Apply" CssClass="btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                       15
                    </td>
                    <td>
                      Odisha State Pollution Control Board
                    </td>
                    <td>
                       Authorization under Hazardous Waste (Management and Handling) Rules, 1989
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:HyperLink ID="hypPollutionHW" runat="server" Target="_blank" NavigateUrl="http://164.100.163.18/OSPCB/industryRegMaster/create1?ServiceID=50&UserId=investor10&ProposalId=201709001" Text="Apply" CssClass="btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                    </td>
                </tr>--%>
              <tr>
                    <td>
                       14
                    </td>
                    <td>
                      Industries Department (IDCO)
                    </td>
                    <td>
                       PEAL Integratin with IDCO ERP
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="#" Text="Apply" CssClass="btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                       15
                    </td>
                    <td>
                      Industries Department (IDCO)
                    </td>
                    <td>
                       Obtaining Water Connection
                    </td>
                    <td>
                        NA
                    </td>
                    <td>
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="#" Text="Apply" CssClass="btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
