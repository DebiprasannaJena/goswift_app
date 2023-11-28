<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mom.aspx.cs" Inherits="SingleWindow_Mom" %>

<!doctype html>
<html>
<head runat="server">
    <title></title>
    <link href="../../PortalCSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
   
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/overrid.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <style type="text/css" media="all">
        body
        {
            font-family: Arial !important;
            font-size:16px;
        }
        .ppul
        {
            margin: 0px !important;
        }
        .ppul li
        {
            list-style-type: disc;
            margin-left: 12px;
        }
        .rmn li
        {
            list-style-type: decimal;
        }
        .rmn
        {
            margin-left: 8px;
        }
        .print-table, .print-table tr, .print-table tr td, .print-table thead, .print-table tr th
        {
            border: 1px solid #000 !important;
        }
        .inner-table, .inner-table tr, .inner-table tr td, .pinner-table thead, .inner-table tr th
        {
            border: 0px solid #000 !important;
        }
/*        td span
        {
            line-height: 28px;
        }*/
        #lblProjTitle
        {
           font-size: 18px;
    line-height: 20px;
    text-transform: uppercase;
        }
        .listX ul li
        {
            list-style-type: disc;
            margin-left: 8px;
        }
        .listX ol
        {
            list-style-type: decimal;
        }
        .listX ol li
        {
            list-style-type: decimal;
            margin-left: 8px;
        }
        .listX li
        {
            list-style-type: upper-roman;
        }
        .listX
        {
            margin-left: 20px;
        }
        div.panel
        {
            margin: 6px 0px;
            padding: 0px;
            border: 1px solid #ddd;
        }
        div.panel .panel-heding
        {
            background: #f5f5f5;
            border-bottom: 1px solid #ddd;
            margin: 0px;
            padding: 6px;
        }
        div.panel .panel-content
        {
            margin: 0px;
            padding: 12px 10px;
        }
        .m-b-15
        {
            margin-bottom:5px;
        }
        table {
    border-collapse: collapse;
    border-spacing: 0;
    }
		/*.table-bordered th {background-color: #E3E3E3;}
            .table-bordered>thead>tr>th, .table-bordered>tbody>tr>th, .table-bordered>tfoot>tr>th, .table-bordered>thead>tr>td, .table-bordered>tbody>tr>td, .table-bordered>tfoot>tr>td {
        border: 1px solid #bdbdbd!IMPORTANT;
    }*/
		.panel-content table:last-child {margin-bottom: 0;}
    @media Print{
        .body{font-size:16px;}     
      .print-btn{display:none;}       
             }
.table-bordered th {background-color: #E3E3E3 ;}
.table-bordered>thead>tr>th,.table-bordered>tbody>tr>th,.table-bordered>tfoot>tr>th,.table-bordered>thead>tr>td,.table-bordered>tbody>tr>td,.table-bordered>tfoot>tr>td {border: 1px solid #bdbdbd!IMPORTANT; }		
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
    
        <div class="m-b-15">
            <strong>
                <asp:Label ID="lblProjTitle" runat="server" Text=""></asp:Label></strong>
                <a class="btn btn-primary print-btn  pull-right btn-sm" id="A1" href="#" title="Click here to Print"
                onclick="window.print();"><i class="fa fa-print"></i>
            </a>
                <div class="clearfix"></div>
                </div>
        <div class="panel" >
            <div class="panel-heding">
                <label>
                    SECTOR :</label>
                <asp:Label ID="lblSector" runat="server" Text="" Font-Bold="true"></asp:Label></div>
        </div>
        <div class="panel">
            <div class="panel-heding">
                <label>
                    1. Name of the Applicant</label></div>
            <div class="panel-content">
                <asp:Label ID="lblProjNm" runat="server" Text=""></asp:Label></div>
        </div>
        <div class="panel">
            <div class="panel-heding">
                <label>
                    2. Date of submission of Preliminary Project assessment Application</label></div>
            <div class="panel-content">
                <asp:Label ID="lblApplDate" runat="server" Text=""></asp:Label></div>
        </div>
        <div class="panel">
            <div class="panel-heding">
                <label>
                    3. Location of the Project</label></div>
            <div class="panel-content">
                <div id="placeholder" runat="server">
                </div>
            </div>
        </div>
        <div class="panel">
            <div class="panel-heding">
                <label>
                    4. Profile of the project</label></div>
            <div class="panel-content">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="55%" valign="top">
                            <asp:Repeater ID="rptCapacity" runat="server">
                                <HeaderTemplate>
                                    <table id="tblSample" width="100%" class="table printable table-bordered " border="0">
                                        <tr>
                                            <th>
                                                Product
                                            </th>
                                            <th>
                                                Capacity
                                            </th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#Eval("VCHPRODUCT")%>
                                        </td>
                                        <td>
                                            <%#Eval("vchCapacity")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                        <td width="5%">
                        </td>
                        <td width="45%" valign="top">
                            <table width="100%" class="table table-bordered">
                                <tr>
                                    <th>
                                        Type
                                    </th>
                                    <th>
                                      <asp:Label ID="lblNew" runat="server" Text=""></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        Environment Category
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="panel">
            <div class="panel-heding">
                <label>
                    5. Proposal in brief</label></div>
            <div class="panel-content">
                <asp:Repeater ID="RptrProposal" runat="server">
                    <HeaderTemplate>
                        <ul class="listX">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%-- <li>--%>
                        <%# Server.HtmlDecode( (string) Eval( "ProposalDtl" ) ) %>
                        <%-- </li>--%>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="panel">
            <div class="panel-heding">
                <label>
                    6. Promoters</label></div>
            <div class="panel-content">
                <table cellpadding="2" cellspacing="0" width="100%">
                    <tr>
                        <td width="50%" valign="top">
                            Board of Directors:-
                            <br />
                            <asp:Repeater ID="RptrPromoterDirectors" runat="server">
                                <HeaderTemplate>
                                    <ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li>
                                        <%#Container.ItemIndex + 1%>.
                                        <%#Eval("VCHPROMOTOR") %>
                                    </li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                        <td width="4%" class="bdr-left">
                        </td>
                        <td valign="top">
                            Existing Business Interest of the company:-
                            <asp:Repeater ID="RptrPromoterBusiness" runat="server">
                                <HeaderTemplate>
                                    <ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li>
                                        <%#Container.ItemIndex + 1%>.
                                        <%#Eval("VCHPROMOTOR") %>
                                    </li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="panel">
            <div class="panel-heding">
                <label>
                    7. Financial Performance of company(Rs in crores)</label></div>
            <div class="panel-content">
                <asp:GridView ID="GrdFinanace" runat="server" Width="100%" AutoGenerateColumns="False"
                    DataKeyNames="FinanceId,ComapnyName,KeyId" OnDataBound="OnDataBound" CssClass="table table-bordered table-condensed">
                    <Columns>
                        <asp:BoundField DataField="ComapnyName" HeaderText="Company Name">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Particulars" HeaderText="Particulars">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FinYear1" HeaderText="Finance Year">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FinYear2" HeaderText="Finance Year">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FinYear3" HeaderText="Finance Year">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="panel">
            <div class="panel-heding">
                <label>
                    8. Project Cost(Rs in Crores)</label>
                :
                <asp:Label ID="lblProjCost" runat="server" Text=""></asp:Label></div>
            <div class="panel-content">
                <h3>
                    a) Project Cost details</h3>
                <asp:GridView ID="GrdProjectCostDtls" runat="server" Width="100%" AutoGenerateColumns="False"
                    ShowFooter="true" GridLines="None" OnRowDataBound="GrdProjectCostDtls_RowDataBound"
                    CssClass="table table-bordered table-condensed m-b-15">
                    <Columns>
                        <asp:TemplateField HeaderText="Details Description">
                            <ItemTemplate>
                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("VCH_COST_DTLS_DESC") %>'></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle Width="65%" />
                            <ItemStyle Width="65%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cost (Rs in Crores)" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblCost" runat="server" Text='<%# Eval("DEC_COST") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGrandTotal" runat="server"></asp:Label>
                            </FooterTemplate>
                             <HeaderStyle Width="34%"/>
                            <ItemStyle Width="34%" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                </asp:GridView>
                <h3>
                    b) Financing details(Rs. in Crores)</h3>
                <asp:GridView ID="GrdFinDtls" runat="server" Width="100%" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-condensed m-b-15" ShowFooter="true" GridLines="None"
                    OnRowDataBound="GrdFinDtls_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("VCH_FIN_DTLS_DESC") %>'></asp:Label>
                            </ItemTemplate>
                             <%--<HeaderStyle Width="50%" />--%>
                            <ItemStyle Width="33%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("DEC_FIN_COST") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGTotal" runat="server"></asp:Label>
                            </FooterTemplate>
                             <%--<HeaderStyle Width="25%" />--%>
                            <ItemStyle Width="33%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% of Total Project Cost" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblPerCost" runat="server" Text='<%# Eval("VCH_PERCENTAGE") %>'></asp:Label>
                            </ItemTemplate>
                             <%--<HeaderStyle Width="25%" />--%>
                            <ItemStyle Width="34%" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                </asp:GridView>
                <div id="trFin" runat="server">
                    <h3>
                        c) Financing Description</h3>
                    <asp:Label ID="lblFinDesc" runat="server"></asp:Label></div>
            </div>
        </div>
        <div class="panel">
            <div class="panel-heding">
                <label>
                    9. Infrastructure requirement</label></div>
            <div class="panel-content">
                <table class="table table-bordered table-condensed">
                    <tr>
                        <td width="150px">
                            i. Land
                        </td>
                        <td>
                            <asp:Label ID="lblLand" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ii. Water
                        </td>
                        <td>
                            <asp:Label ID="lblWater" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            iii. Power
                        </td>
                        <td>
                            <asp:Label ID="lblPower" runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="lblCPP" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="panel">
            <div class="panel-heding">
                <label>
                    10. Raw Materials Source</label></div>
            <div class="panel-content">
                <asp:GridView ID="GrdSource" runat="server" class="table table-bordered" Width="100%"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Materials">
                            <ItemTemplate>
                                <asp:Label ID="lblMaterials" runat="server" Text='<%# Eval("Materials") %>'></asp:Label>
                            </ItemTemplate>
                              <ItemStyle Width="50%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Source">
                            <ItemTemplate>
                                <asp:Label ID="lblSource" runat="server" Text='<%# Eval("Source") %>'></asp:Label>
                            </ItemTemplate>
                              <ItemStyle Width="50%" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="panel">
            <div class="panel-heding">
                <label>
                    11. Implementation Period</label></div>
            <div class="panel-content">
                <asp:Label ID="lblImple" runat="server"></asp:Label></div>
        </div>
        <div class="panel">
            <div class="panel-heding">
                <label>
                    12. Employment Potential</label></div>
            <div class="panel-content">
                <table width="100%" cellpadding="0" cellspacing="0" class="table table-bordered">
                    <tr>
                        <td>
                            Direct
                        </td>
                        <td>
                            <asp:Label ID="lblDirect" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Contractual
                        </td>
                        <td>
                            <asp:Label ID="lblContra" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Total</label>
                        </td>
                        <td>
                            <asp:Label ID="lblEmTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="panel">
            <div class="panel-heding">
                <label>
                    13. Views of Line Department represented in State Level Facilitation Cell.</label></div>
            <div class="panel-content">
                <asp:Repeater ID="RptrComments" runat="server">
                    <HeaderTemplate>
                        <table style="width: 100%" class="inner-table">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%--<asp:Label ID="lblName" runat="server" Text='<%#  Eval("VCH_FULLNAME") %>' Font-Bold="true" />--%>
                                <br />
                                "
                                <asp:Label ID="lblComment" runat="server" Text='<%#Eval("VCHCOMMENT") %>' />
                                "
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <br />
                                sd/- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <br />
                                <asp:Label ID="lblCName" runat="server" Text='<%# Eval("VCH_FULLNAME") %>'></asp:Label>
                                <br />
                                <asp:Label ID="lblDesigName" runat="server" Text='<%# Eval("nvchDesigName") %>'></asp:Label>
                                ,
                                <asp:Label ID="lblDeptName" runat="server" Text='<%# "(" + Eval("DEPTNAME") + ") " %>'></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="panel">
            <div class="panel-heding">
                <label>
                    14. Decision of State Level Facilitation Cell.</label></div>
            <div class="panel-content">
                <asp:Repeater ID="RptrDecision" runat="server">
                    <HeaderTemplate>
                        <%= DecisionText%>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <span style="float: left; width: 4%;">
                            <%#Container.ItemIndex + 1%>.</span>
                        <li style="float: left; width: 96%;">
                            <%#Eval("CHECKLISTPOINT")%>
                        </li>
                        <div class="clearfix">
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
                <table width="100%" cellpadding="2" cellspacing="0">
                    <tr>
                        <td height="100px" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" align="center">
                            sd/<br />
                            Prepared By-
                            <asp:Literal ID="litPreapre" runat="server"></asp:Literal>
                        </td>
                        <td align="center">
                            sd/
                            <br />
                            Checked By-
                            <asp:Literal ID="litChecked" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="100px" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" align="center">
                        </td>
                        <td align="center">
                            sd/<br />
                            Chairman-cum-Managing Director<br />
                            IPICOL
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#tblSample').each(function () {
                var Column_number_to_Merge = 1;

                // Previous_TD holds the first instance of same td. Initially first TD=null.
                var Previous_TD = null;
                var i = 1;
                $("tbody", this).find('tr').each(function () {
                    // find the correct td of the correct column
                    // we are considering the table column 1, You can apply on any table column
                    var Current_td = $(this).find('td:nth-child(' + Column_number_to_Merge + ')');

                    if (Previous_TD == null) {
                        // for first row
                        Previous_TD = Current_td;
                        i = 1;
                    }
                    else if (Current_td.text() == Previous_TD.text()) {
                        // the current td is identical to the previous row td
                        // remove the current td
                        Current_td.remove();
                        // increment the rowspan attribute of the first row td instance
                        Previous_TD.attr('rowspan', i + 1);
                        i = i + 1;
                    }
                    else {
                        // means new value found in current td. So initialize counter variable i
                        Previous_TD = Current_td;
                        i = 1;
                    }
                });
            });
        });
    </script>
</body>
</html>
