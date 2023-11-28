<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptwiseServiceslist.aspx.cs"
    Inherits="DeptwiseServiceslist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <uc2:header ID="header" runat="server" />
    <div class="pagenavigator">
        <h2>
            <a class="" href="javascript:history.back()"><i class="fa fa-chevron-circle-left"></i>
            </a>Department Wise Services List</h2>
    </div>
    <div class="registration-div">
        <div class="container">
           <div class="form-sec"  >
           <div class="form-header">
                        
                            <h2>
                                Department of Labour, Employment and Skill Development</h2>
                           </div>
           <div class="form-body">
            <div class="table-responsive" id="divGrd" style="">
                <table class="table table-bordered" cellspacing="0" cellpadding="4" id="gvDeptClearance"
                    style="width: 100%; border-collapse: collapse;">
                    
                        <tr>
                            <th scope="col">
                                SlNo.
                            </th>
                            <th scope="col">
                                Department
                            </th>
                            <th scope="col">
                                Services
                            </th>
                           
                            <th scope="col">
                                Prcedure
                            </th> 
                            <th scope="col">
                                Apply Now
                            </th>
                        </tr>
                        <tr>
                            <td>
                                1
                            </td>
                            <td>
                                <span id="gvDeptClearance_lblDepartment2_0">Directorate of Labour</span>
                            </td>
                            <td>
                                <span id="gvDeptClearance_lblDepartment1_0">Registration under The Building and Other
                                    Construction Workers Act, 1996</span>
                            </td>
                            <td>
                               <a title="Download"><i class="fa fa-file-pdf-o"></i></a>
                            </td>
                            <td>
                                <a id="gvDeptClearance_hypApply_0" class=" btn  bg-drpurpol " href="inestorlogin.aspx"
                                    style="color: White;">Apply</a>
                            </td>
                            
                        </tr>
                        <tr>
                            <td>
                                2
                            </td>
                            <td>
                                <span id="gvDeptClearance_lblDepartment2_1">Directorate of Labour</span>
                            </td>
                            <td>
                                <span id="gvDeptClearance_lblDepartment1_1">License for contractors &amp; renewal of
                                    license under provision of The Contracts Labour Act, 1970</span>
                            </td>
                              <td>
                                <a title="Download"><i class="fa fa-file-pdf-o"></i></a>
                            </td>
                            <td>
                                <a id="gvDeptClearance_hypApply_1" class=" btn  bg-drpurpol " href="inestorlogin.aspx"
                                    style="color: White;">Apply</a>
                            </td>
                          
                        </tr>
                        <tr>
                            <td>
                                3
                            </td>
                            <td>
                                <span id="gvDeptClearance_lblDepartment2_2">Directorate of Labour</span>
                            </td>
                            <td>
                                <span id="gvDeptClearance_lblDepartment1_2">Registration certificate of Establishment
                                    Inter State Migrant Workmen(RE&amp;CS)Act,1979 </span>
                            </td>
                              <td>
                               <a title="Download"><i class="fa fa-file-pdf-o"></i></a>
                            </td>
                            <td>
                                <a id="gvDeptClearance_hypApply_2" class=" btn  bg-drpurpol " href="inestorlogin.aspx"
                                    style="color: White;">Apply</a>
                            </td>
                          
                        </tr>
                        <tr>
                            <td>
                                4
                            </td>
                            <td>
                                <span id="gvDeptClearance_lblDepartment2_3">Directorate of Labour</span>
                            </td>
                            <td>
                                <span id="gvDeptClearance_lblDepartment1_3">Application for Licence </span>
                            </td>
                               <td>
                                   <a title="Download"><i class="fa fa-file-pdf-o"></i></a>
                            </td>
                            <td>
                                <a id="gvDeptClearance_hypApply_3" class=" btn  bg-drpurpol " href="inestorlogin.aspx"
                                    style="color: White;">Apply</a>
                            </td>
                         
                        </tr>
                        <tr>
                            <td>
                                5
                            </td>
                            <td>
                                <span id="gvDeptClearance_lblDepartment2_4">Directorate of Labour</span>
                            </td>
                            <td>
                                <span id="gvDeptClearance_lblDepartment1_4">Application for renewal of licences
                                </span>
                            </td>
                             <td>
                            <a title="Download"><i class="fa fa-file-pdf-o"></i></a>
                                
                            </td>
                            <td>
                                <a id="gvDeptClearance_hypApply_4" class=" btn  bg-drpurpol " href="inestorlogin.aspx"
                                    style="color: White;">Apply</a>
                            </td>
                           
                        </tr>
                  
                </table>
            </div>
            </div>
            <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
