<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Dashboard_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <script>

        $(document).ready(function (e) {
            //  $('.counter').counterUp({ delay: 10, time: 2000 });
            $('.searchfilter,.spmgfilter').click(function () {
                $(this).find(".fa").toggleClass('fa-filter fa-times-circle');

            });
            $('#filterpealsearch').click(function () {
                $('#pealsearch').slideToggle();

            });

            $('#filterIncentivesearh').click(function () {
                $('#Incentivesearh').slideToggle();

            });
            $('#linkDepartmentServices').click(function () {
                $('#DepartmentServices').slideToggle();

            }); $('#linkAPAAStatus').click(function () {
                $('#APAAStatus').slideToggle();

            }); $('#linkCSRActivities').click(function () {
                $('#CSRActivities').slideToggle();

            }); $('#linkCIF').click(function () {
                $('#CIF').slideToggle();

            });
            $('#linkUnitREg').click(function () {
                $('#UnitREg').slideToggle();
            });
            $('#linkSpmg').click(function () {
                $('#SpmgContent').slideToggle();
            });

            $('#Queryfilter').click(function () {
                $('#Querysearch').slideToggle();
            });
        });
        function openpoupwin(ctrl) {
            debugger;
            $.ajax({
                type: "POST",
                url: "Default.aspx/PealDetailsBind",

                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    tempHTML = "";
                    $("#tblPeal").html('')
                    tempHTML += '<thead><tr class="persist-header">'
                    tempHTML += '<th rowspan="1" valign="middle" width="20px" bgcolor="#e4e4e4">Sl#</th>'
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Company Name</th>'
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Fee</th>'
                    tempHTML += '</tr></thead><tbody>'
                    $('#tblPeal').append(tempHTML);
                    tempHTML = "";
                    var serialNo = 0;
                    $.each(r.d, function (index, value) {
                        serialNo++;
                        if (r.d.length > 0) {
                            tempHTML += '<tr>';
                            tempHTML += '<td align="left">' + serialNo + '</td>';
                            tempHTML += '<td >' + value.strComapnyName + '</td>';
                            tempHTML += '<td >' + value.decFee + '</td>';
                            tempHTML += '</tr>';
                        }
                    });
                    if (r.d.length == 0) {
                        $("#tblPeal").html('')
                        tempHTML += '<tr><td>No Records Found...</td></tr>';
                        $("#tblPeal").append(tempHTML);
                    }
                    else {
                        $("#tblPeal").append(tempHTML); //APPEND THE DYNAMIC VALUE IN ROW
                        $("#tblPeal").append("</tbody>");
                    }
                },
                error: function (response) {
                    alert('y');
                    var msg = jQuery.parseJSON(response.responseText);
                    alert("Message: " + msg.Message + "<br /> StackTrace:" + msg.StackTrace + "<br /> ExceptionType:" + msg.ExceptionType);
                }
            });
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
              <%-- <div class="header-icon">
                  <img src="../images/naveen-patnaik.jpg" style="height: 53px;" class="img img-responsive img-thumbnail" />
               </div>
               <div class="header-title">
                  <h1>Chief Minister's DashBoard</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a> DashBoard</a></li><li><a></a></li></ul>
               </div>--%>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">


                  <div class="Mastertracker">
             <h4>Master Tracker <small>(for current year)</small></h4>
            <div class="row">
             <div class="col-sm-6 col-md-4">
             <div class="masterportletsec minheight-170">
             <h4 class="text-center">Inspection Status </h4>

             <div class="two-sectons">
             <p><span id="unattInsdash" runat="server"></span> Unattended Inspection </p>
             </div>
             <div class="two-sectons magin-right0">
             <p><span id="reportPending" runat="server"></span> Reports Pending </p>
             </div>

             </div>
             </div>
             <div class="col-sm-6 col-md-4">
             <div class="masterportletsec">
             <h4>Approvals Status </h4>
             <p>Total Approvals Pending <span>44</span></p>
             
             </div>
             </div>
             <div class="col-sm-6 col-md-4">
             <div class="masterportletsec">
             <h4>Post Allotment Applications to IDCO Status </h4>
             <p>No. change requests pending <span id="spAPAAPending" runat="server"></span></p>
             

             </div>
             </div>
             <div class="col-sm-6 col-md-4">
             <div class="masterportletsec">
             <h4>SPMG Portal </h4>
             <p>Issues pending<span id="spSpmgpnd" runat="server"></span></p>
            

             </div>
             </div>
             <div class="col-sm-6 col-md-4">
             <div class="masterportletsec">
             <h4>Queries</h4>
             <p>Issues pending <span>44</span></p>
             

             </div>
             </div>
             </div>
                  </div>
                 
                      <div class="grphs-sec">
                  <div class="row">
                  <div class="col-sm-6 col-md-4">
                       <div class="investordashboard-sec incentive-sec ">
                      
                       <h4>Approval Status 
                       <a class="pull-right spmgfilter" id="linkDepartmentServices">
                       <i class="fa fa-filter"></i>
                       </a></h4>
                        <asp:updatepanel runat="server" ID="upd1"  UpdateMode="Conditional" ChildrenAsTriggers="true">
    <contenttemplate>
                       <div class="portletcontainer cmdashbordportlet">
                     <ul>
                     <li>Application Received<span  id="hdApplied" runat="server"><asp:Literal ID="ltlServiceApplied" runat="server"></asp:Literal></span></li>
                      <li>Total Approvals Provided<span  id="hdApprove" runat="server"><asp:Literal ID="ltlApprove" runat="server"></asp:Literal></span></li>
                      
                      <li>Approval Pending<span  id="hdPending" runat="server"><asp:Literal ID="ltlServicepending" runat="server"></asp:Literal></span></li>
                      <%--<li><a href="#DsModal" data-toggle="modal" data-target="#DsModal"  title="Approvals past ORTPSA timeline">Approvals past ORTPSA timeline<span class="bgdisbursed">12</span></a></li>--%>
                         
                  
                     </ul>
                     </div>
                          <div class="portletsearch" id="DepartmentServices">
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-6 col-md-4">Department</label>
                  <div class="col-sm-8">
                   <span class="colon">:</span>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control dpt"   AutoPostBack="True">
                <asp:ListItem Value="0">---Select---</asp:ListItem>
                </asp:DropDownList>
                    </ContentTemplate>
                                  
                </asp:UpdatePanel>
                  </div>
                  </div>
                  </div>
                   <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                   <ContentTemplate>
                     <div class="form-group">
                  <div class="row">
                  <label class="col-sm-6 col-md-4">Service</label>
                   <div class="col-sm-8">
                  <span class="colon">:</span>
                 
                <asp:DropDownList CssClass="form-control" ID="ddlService" runat="server">
                <asp:ListItem Value="0">---Select---</asp:ListItem>
                </asp:DropDownList>
                
                </div>
                  </div>
                  
                  </div>
           
                    <div class="form-group">
                  <div class="row">
                 <div class="col-sm-8 col-sm-offset-4">
                  <asp:Button ID="btnStatusOfApproval" CssClass="btn btn-success" runat="server" Text="Submit" ></asp:Button>
                  </div>
               
                  </div>
                  
                  </div>
                  </ContentTemplate>
                  <triggers>
            
                                  
                </triggers>
                </asp:UpdatePanel>
                  </div>
             
               </contenttemplate>
              <Triggers>
                   <asp:asyncpostbacktrigger controlid="ddldept"  />
              <asp:asyncpostbacktrigger controlid="btnStatusOfApproval" />  
              </Triggers>
</asp:updatepanel>
                       </div>
                       </div>
                      
                        <div class="col-sm-6 col-md-4">

                       <div class="investordashboard-sec incentive-sec">
                       <h4>Central Inspection Framework 
                       <a class="pull-right spmgfilter" id="linkCIF"><i class="fa fa-filter"></i></a>
                       </h4>

                      
                       <div class="portletcontainer cmdashbordportlet">
                    <ul>
                     <li>Inspection Scheduled<span  id="cicgapplied" runat="server"><asp:Literal ID="Literal1" runat="server">120</asp:Literal></span></li>
                      <li>Inspection Completed<span  id="cicgcompleted" runat="server"><asp:Literal ID="Literal2" runat="server">98</asp:Literal></span></li>
                       <li>
                       <%--<a href="#CIFModal" data-toggle="modal" data-target="#CIFModal"  title="Unattended Inspection">--%>
                       Unattended Inspection<span class="bgdisbursed" id="cicgunattnd" runat="server"><asp:Literal ID="Literal3" runat="server">5</asp:Literal>
                       </span>
                       <%--</a>--%>
                       </li>
                      <li>
                      <%--<a href="#CIFModal" data-toggle="modal" data-target="#CIFModal"  title="Inspection reports Pending">--%>
                      Inspection reports Pending<span  id="cicgpending" runat="server"><asp:Literal ID="Literal4" runat="server">8</asp:Literal></span>
                     <%-- </a>--%>
                      </li>
                      <%-- <li>Approvals past ORTPSA timeline<span class="bgdisbursed">12</span></li>--%>
                         
                  
                     </ul>
                     </div>
                       <div>
                      <div class="portletsearch" id="CIF">
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">Year</label>
                  <div class="col-sm-8">
                  <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="DropDownList14" runat="server">
                <asp:ListItem>---Select---</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  </div>
                     <div class="form-group">
                  <div class="row">
                  <label class="col-sm-4">Month</label>
                   <div class="col-sm-8">
                  <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="DropDownList15" runat="server">
                <asp:ListItem>---Select---</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  
                  </div>
           
                    <div class="form-group">
                  <div class="row">
                 <div class="col-sm-8 col-sm-offset-4">
                  <asp:Button ID="Button8" CssClass="btn btn-success" runat="server" Text="Submit"></asp:Button>
                  </div>
               
                  </div>
                  
                  </div>
                  </div>
                       </div>   
                     
                       </div>
                       </div>
 
                     <div class="col-sm-6 col-md-4">
                        <div class="investordashboard-sec incentive-sec">
                       <h4>Post Allotment Applications to IDCO STATUS  <a class="pull-right spmgfilter" id="linkAPAAStatus">
                       <i class="fa fa-filter"></i>
                       </a>
                       </h4>
                    <div id="ApaaStatus" style="height:280px;display:none"  ></div>
                  <div class="portletcontainer cmdashbordportlet">
                     <ul>
                     <li>Change requests applied<span ><asp:Literal ID="Literal6" runat="server"></asp:Literal></span></li>
                      <li>Change requests processed<span ><asp:Literal ID="Literal7" runat="server"></asp:Literal></span></li>
                       <li>Change requests pending<span ><asp:Literal ID="Literal8" runat="server"></asp:Literal></span></li>
                      <li><a data-toggle="modal" data-target="#ApAAModal"  title="Change requests which have crossed >30 days" style="color:#374767;">Change requests which have<br /><small>crossed >30 days</small> <span class="bgdisbursed" style="margin-top: -18px;"><asp:Literal ID="Literal9" runat="server"></asp:Literal></span></a></li>
                      <%-- <li>Approvals past ORTPSA timeline<span class="bgdisbursed">12</span></li>--%>
                         
                     </ul>
                     </div>
                            <div  id="aaa" style="display:none"> 
                            <div id="dvtest" runat="server" ></div>
                            </div>
                           <div class="portletsearch" id="APAAStatus"  >


                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">District</label>
                  <div class="col-sm-8">
                   <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="ddlDistrict" runat="server">
                <asp:ListItem>---Select---</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  </div>

                     <div class="form-group">
                  <div class="row">
                  <label class="col-sm-4">Month</label>
                   <div class="col-sm-8">
                  <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="ddlAppaMonth" runat="server">
                <asp:ListItem>---Select---</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  
                  </div>
           
           <div class="form-group">
                  <div class="row">
                  <label class="col-sm-4">Year</label>
                   <div class="col-sm-8">
                  <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="ddlAppaYear" runat="server">
                <asp:ListItem>---Select---</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  
                  </div>
                    <div class="form-group">
                  <div class="row">
                 <div class="col-sm-8 col-sm-offset-4">
                  <asp:Button ID="Button4" CssClass="btn btn-success" runat="server" Text="Submit"></asp:Button>
                  </div>
               
                  </div>
                  
                  </div>
                  </div> 
                  </div>
                       </div>
                       </div>
                     </div>
                    
                  <div class="grphs-sec">
                  <div class="row">
                   <div class="col-sm-6 col-md-4">
                       <div class="investordashboard-sec incentive-sec ">
                       <h4> Query <a class="pull-right Queryfilter" id="Queryfilter"><i class="fa fa-filter"></i></a></h4>
                       <div class="portletcontainer cmdashbordportlet">
                  <ul>
                      <li>Queries Raised<span  id="spRaised" runat="server"></span></li>
                      <li>Queries Reverted<span  id="spRevert" runat="server"></span></li>
                       <li>Queries Resolved<span  id="spResolved" runat="server"></span></li>
                         <li><a href="#QueryModal" data-toggle="modal" data-target="#QueryModal"  title="Issues Pending > 30 days">Queries past their prescribed Timelines<span class="bgdisbursed" id="spPastQuery" runat="server"></span></a></li>
                     </ul>

               <div class="portletsearch" id="Querysearch" style="top: -0px;">
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-2">Year</label>
                  <div class="col-sm-6">
                   <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="DropDownList16" runat="server">
                <asp:ListItem>---Select---</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  <div class="col-sm-2">
                  <asp:Button ID="Button10" CssClass="btn btn-success" runat="server" Text="Submit"></asp:Button>
                  </div>
                  </div>
                  </div>

           
                   
                  </div>
               
                       </div>
                       </div></div>
                         
                           <div class="col-sm-6 col-md-4">
                       <div class="investordashboard-sec incentive-sec ">
                       <h4>SPMG 
                       <a class="pull-right spmgfilter" id="linkSpmg"><i class="fa fa-filter"></i></a>
                       </h4>
                       <div class="portletcontainer cmdashbordportlet">
                  <ul>
                      <li>Issues Received<span  id="spmgraised" runat="server"></span></li>
                      <li>Issues Resolved<span  id="spmgresolved" runat="server"></span></li>
                       <li>Issues Pending<span   id="spmgpending" runat="server"></span></li>
                         <li>
                         <a href="#spmgmodal" data-toggle="modal" data-target="#spmgmodal"  title="Issues Pending  > 30 days">
                         Issues Pending > 30 days <span  id="spmg30pending" runat="server" ></span>
                         </a>
                         </li>
                     </ul>

               <div class="portletsearch" id="SpmgContent" style="top: -0px;">
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-2">Year</label>
                  <div class="col-sm-6">
                   <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="ddlspmgyear" runat="server"  AutoPostBack="True">
                <asp:ListItem Value="0">---Select---</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  <div class="col-sm-2">
                  <asp:Button ID="btnspmg" CssClass="btn btn-success" runat="server" Text="Submit"></asp:Button>
                  </div>
                  </div>
                  </div>

           
                   
                  </div>
               
                       </div>
                       </div></div>

                     
                     </div>
                     </div>
                    
                    <div class="grphs-sec" style="display:none">
                  <div class="row">
                  <div class="col-sm-6">
                 <div class="bg-vaolet margin-bottom20 groupmastreportlet">
                   
                      <h4>Proposals <span class="searchfilter pull-right" id="filterpealsearch" title="Search">
                      <i class="fa fa-filter"></i>
                      </span></h4>

                       
                   <div class="portletdivider">
                   <div class="fontsec">
                   <span class="counter"><small>Applied</small><br />
                   <%--<a href="#" class="projectdtls"> 200</a>--%>
                  <%-- <asp:Label ID="lblApplied" runat="server" Text="" class="projectdtls"></asp:Label>--%>
                     <asp:Literal ID="lblApplied" runat="server"></asp:Literal>
                   </span> 
                   <span class="counter" title="Proposed Investment">
                  <i class="fa fa-inr"></i> 
                  <asp:Literal ID="lblProposalApplied" runat="server"></asp:Literal>
                  
                   <small>Cr.</small>
                   </span>
                   <span class="counter" title="Proposed Employment">
                   <i class="fa fa-user"></i>
                    <%--<asp:Label ID="lblProposalNoApplied" runat="server" Text="" class="fa fa-user"></asp:Label>--%>
                    <asp:Literal ID="lblProposalNoApplied" runat="server"></asp:Literal>
                   <small> Nos. </small></span>
                   <div class="clearfix"></div>
                   </div>
                   <%--<h4>Applied</h4>--%>
                   </div> 
                    <div class="portletdivider margin-right0"><div class="fontsec">
                    <span class="counter"><small>Approved</small><br />
                   <%-- <a href="#"> 10</a>--%>
                   <%--<asp:Label ID="lblApproved" runat="server" Text="" ></asp:Label>--%>
                    <asp:Literal ID="lblApproved" runat="server"></asp:Literal>
                    </span> 
                    <span class="counter">
                    <i class="fa fa-inr"></i> 
             
                     <%--<asp:Label ID="lblProposalApproved" runat="server" Text=""  class="fa fa-inr"></asp:Label>--%>
                     <asp:Literal ID="lblProposalApproved" runat="server"></asp:Literal>
                    <small>Cr.</small></span>
                    <span class="counter" title="Proposed Employment">
                   <i class="fa fa-user"></i>
                  <%-- <asp:Label ID="lblProposalNoApproved" runat="server" Text="" class="fa fa-user"></asp:Label>--%>
                    <asp:Literal ID="lblProposalNoApproved" runat="server"></asp:Literal>
                    <small> Nos. </small></span>
                    <div class="clearfix"></div>
                    </div>
                   <%--<h4>Approved</h4>--%>
                   </div> 
                    <div class="clearfix"></div>
                   <div class="portletdivider noamount"><div class="fontsec">
                  <%-- <span class="counter">10</span> --%>
                  <asp:Label ID="lblrejected" runat="server" class="counter" Text=""></asp:Label>
                   </div>
                   <h4>Rejected
</h4></div> 
                    <div class="portletdivider noamount margin-right0"><div class="fontsec">
                    <%--<span class="counter">4</span>--%>
                    <asp:Label ID="lblEvaluation" runat="server" class="counter" Text=""></asp:Label>
                    </div>
                   <h4>Under Evaluation</h4></div> 
                    <div class="clearfix"></div>
                  <div class="portletsearch" id="pealsearch">
                  <div class="form-group">
                  <div class="row">
                  <div class="col-sm-6">
                  <label>Financial Year</label>
                <asp:DropDownList CssClass="form-control" ID="DropDownList1" runat="server">
                <asp:ListItem>---Select---</asp:ListItem>
                </asp:DropDownList>
                  </div>

                  <asp:HiddenField ID="hdnTotApplied" runat="server"></asp:HiddenField>

                  <div class="col-sm-6">
                  <label>Department</label>
                <asp:DropDownList CssClass="form-control" ID="DropDownList2" runat="server">
                <asp:ListItem>---Select---</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  
                  </div>
                    <div class="form-group">
                  <div class="row">
                  <div class="col-sm-6">
                  <label>District</label>
                <asp:DropDownList CssClass="form-control" ID="DropDownList4" runat="server">
                <asp:ListItem>---Select---</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  <div class="col-sm-6">
                  <label>Project value</label>
                <asp:DropDownList CssClass="form-control" ID="DropDownList5" runat="server">
                <asp:ListItem>---Select---</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  
                  </div>
                    <div class="form-group">
                  <div class="row">
                  <div class="col-sm-12 text-center">
                  <asp:Button ID="Button1" CssClass="btn btn-success" runat="server" Text="Submit"></asp:Button>
                  </div>
               
                  </div>
                  
                  </div>
                  </div>
                    
                    </div>
                     </div>
                      <div class="col-sm-6">
                     <div class="bg-seagreen margin-bottom20 groupmastreportlet">
                   
                      <h4>INCENTIVE <span class="searchfilter pull-right" id="filterIncentivesearh" title="Search">
                      <i class="fa fa-filter"></i>
                      </span></h4>
                 <div class="portletdivider incentive">
                 <div class="fontsec">
                 <span class="counter">Applied <a href="#">50</a></span> 
                 <span class="counter"><i class="fa fa-inr"></i> 1000 <small>Cr.</small></span>
                 <div class="clearfix"></div>
                 </div>
                   <%--<h4>Applied</h4>--%>
                   </div>



                    <div class="portletdivider incentive margin-right0">
                    <div class="fontsec">
                    <span class="counter">Approved <a href="#"> 30</a></span> 
                    <span class="counter incentive"><i class="fa fa-inr"></i> 1000 <small>Cr.</small></span>
                    <div class="clearfix"></div>
                    </div>
                   <%--<h4>Approved</h4>--%></div> 
                    <div class="clearfix"></div>
                   <div class="portletdivider noamount"><div class="fontsec"><span class="counter">20</span></div>
                   <h4>Rejected</h4></div> 
                    <div class="portletdivider noamount margin-right0"><div class="fontsec"><span class="counter">10</span> </div>
                   <h4>Under Evaluation</h4></div> 
                    <div class="clearfix"></div>
                       <div class="portletsearch" id="Incentivesearh">
                  <div class="form-group">
                  <div class="row">
                  
                  <div class="col-sm-offset-2 col-sm-8">
                  <label >Select Policy</label>
                <asp:DropDownList CssClass="form-control" ID="DropDownList3" runat="server">
                <asp:ListItem>---Select---</asp:ListItem>
                </asp:DropDownList>
                  </div>
                 
                  </div>
                  
                  </div>
                    <div class="form-group">
                  <div class="row">
                  <div class="col-sm-12 text-center">
                  <asp:Button ID="Button2" CssClass="btn btn-success" runat="server" Text="Submit"></asp:Button>
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
      <div style="display:none">
                          <div class="col-lg-6 col-sm-6 col-xs-12">
                       <div class="investordashboard-sec incentive-sec ">
                       <h4>CSR ACTIVITIES
  <a class="pull-right spmgfilter" id="linkCSRActivities">
 <%-- <i class="fa fa-filter"></i>--%>
  </a></h4>
                       <div class="portletcontainer cmdashbordportlet">
                
                <ul>
                     <li>Total Project<span ><asp:Literal ID="ltlTotProject" runat="server"></asp:Literal></span></li>
                    <%--<li>Recommended by Council<span ><asp:Literal ID="ltlRecomCouncil" runat="server"></asp:Literal></span></li>--%>
                      <li>Total Spending<span ><i class="fa fa-rupee"></i>&nbsp;<asp:Literal ID="ltlTOTSpending" runat="server"></asp:Literal><small>Cr.</small></span></li> 
                     </ul>
                 <div class="portletsearch" id="CSRActivities" style="top: -5px;">
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">District</label>
                  <div class="col-sm-8">
                  
                <asp:DropDownList CssClass="form-control" ID="DropDownList10" runat="server">
                <asp:ListItem>---Select---</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  </div>
                     <div class="form-group">
                  <div class="row">
                  <label class="col-sm-4">Year</label>
                   <div class="col-sm-8">
                 
                <asp:DropDownList CssClass="form-control" ID="DropDownList11" runat="server">
                <asp:ListItem>---Select---</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  
                  </div>
           
                    <div class="form-group">
                  <div class="row">
                 <div class="col-sm-8 col-sm-offset-4">
                  <asp:Button ID="Button5" CssClass="btn btn-success" runat="server" Text="Submit"></asp:Button>
                  </div>
               
                  </div>
                  
                  </div>
                  </div>
               
                       </div>
                       </div>
                       </div>
                   <div class="col-lg-6 col-sm-6 col-xs-12">
                       <div class="investordashboard-sec incentive-sec ">
                       <h4>Units  Registered <a class="pull-right spmgfilter" id="linkUnitREg">
                      <%-- <i class="fa fa-filter"></i>--%>
                       </a></h4>
                       <div class="portletcontainer cmdashbordportlet">
                  <ul>
                      <li>Applied<span ><asp:Literal ID="ltlUnitApplied" runat="server"></asp:Literal></span></li>
                      <li>Approved<span ><asp:Literal ID="ltlUnitApproved" runat="server"></asp:Literal></span></li>
                       <li>Under Evaluation<span class="bgrejected"><asp:Literal ID="ltlUnderevalution" runat="server"></asp:Literal></span></li>
                        <%--  <li>Employment<span >330</span></li>
                       <li>Investment<span > <i class="fa fa-rupee"></i> 2200</span></li>--%>
                     </ul>

               <div class="portletsearch" id="UnitREg" style="top: -5px;">
                  
                     <div class="form-group">
                  <div class="row">
                  <label class="col-sm-4">Year</label>
                   <div class="col-sm-8">
                 
                <asp:DropDownList CssClass="form-control" ID="DropDownList18" runat="server">
                <asp:ListItem>---Select---</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  
                  </div>
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">District</label>
                  <div class="col-sm-8">
                  
                <asp:DropDownList CssClass="form-control" ID="DropDownList17" runat="server">
                <asp:ListItem>---Select---</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  </div>
           <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">Project Cost</label>
                  <div class="col-sm-8">
                 <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                  </div>
                  </div>
                  </div>
                    <div class="form-group">
                  <div class="row">
                 <div class="col-sm-8 col-sm-offset-4">
                  <asp:Button ID="Button7" CssClass="btn btn-success" runat="server" Text="Submit"></asp:Button>
                  </div>
               
                  </div>
                  
                  </div>
                  </div>
               
                       </div>
                       </div></div>
                      </div>
    <!----------Modal lists------------------------>
       <div id="CIFModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content modal-primary ">
                <div class="modal-header bg-red">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                       CENTRAL INSPECTION FRAMEWORK</h4>
                </div>
                <div class="modal-body">
                 <table class="table table-bordered">
                <tr><th width="40px">Sl#.</th><th>Name of the company</th><th>Days since the inspection was scheduled</th><th>Name of the assigned inspector</th></tr>
                <tr><td>1</td><td>ABC</td><td>30</td><td>xyz</td></tr>
                 </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
      <div id="DsModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content modal-primary ">
                <div class="modal-header bg-red">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                       Approval Status</h4>
                </div>
                <div class="modal-body">
                 <table class="table table-bordered">
                <tr><th width="40px">Sl#.</th><th>Name of the company</th><th>Days since the receipt of the approval application</th></tr>
                <tr><td>1</td><td>ABC</td><td>30</td></tr>
                 </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div> 
    <%--<div id="ApAAModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content modal-primary ">
                <div class="modal-header bg-red">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                       Post Allotment Applications to IDCO</h4>
                </div>
                <div class="modal-body">
                 <table class="table table-bordered">
                <tr><th width="40px">Sl#.</th><th>Company/ Unit applied</th><th>Change request</th><th>Days since the APPA request was applied</th></tr>
                <tr><td>1</td><td>ABC</td><td>30</td><td>24</td></tr>
                 </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>--%>
     <div id="spmgmodal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content modal-primary ">
                <div class="modal-header bg-red">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                       SPMG Details</h4>
                </div>
                <div class="modal-body">

                 <%--<table class="table table-bordered">
                <tr><th width="40px">Sl#.</th><th>Name of the unit</th><th>Type of the issues</th><th>Since the receipt of the issue(in Days)</th></tr>
                <tr><td>1</td><td>ABC</td><td>30</td><td>24</td></tr>
                 </table>--%>
                 <asp:GridView ID="grdSPMGDtl" runat="server" CssClass="table table-bordered" AllowPaging="true"
                        PageSize="10" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                         CellPadding="4" GridLines="None">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name of the unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("VCH_DEPT_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type of the issues">
                                <ItemTemplate>
                                    <asp:Label ID="lblIssues" runat="server" Text='<%# Eval("VCH_ISSUE_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Since the receipt of the issue(in Days)">
                                <ItemTemplate>
                                    <asp:Label ID="lblDays" runat="server" Text='<%# Eval("VCH_DAYS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pagination-grid no-print" />
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
      <div id="QueryModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content modal-primary ">
                <div class="modal-header bg-red">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                       Query Details</h4>
                </div>
                <div class="modal-body">
                 <table class="table table-bordered">
                <tr><th width="40px">Sl#.</th><th>Name of the unit</th><th>Type of the query</th><th>Since the receipt of the query(in Days)</th></tr>
                <tr><td>1</td><td>ABC</td><td>30</td><td>24</td></tr>
                 </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
     <!----------Modal lists------------------------>
    </section>
        <!-- /.content -->
    </div>
    <script src="../js/highcharts.js" type="text/javascript"></script>
    <script src="../js/exporting.js" type="text/javascript"></script>
    <script src="../js/data.js" type="text/javascript"></script>
    <script src="../js/drilldown.js" type="text/javascript"></script>
    <script>

        var location_detl_hdr = 'Proposal Details';
        var location_detl_body_cnt = 'ProposalPupUp.aspx';
        var frm_hit = 450;
        var location_detl_ftr = '<button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Cancel</button>';
        $('.projectdtls').click(function () {
            openPageModal(location_detl_hdr, location_detl_body_cnt, location_detl_ftr, frm_hit);
        });



        $(document).ready(function (e) {

            var winwidth = $(window).width();
            var dinnersize; var dinnerdepth; var yside;
            if (winwidth > 1050) {
                dinnersize = 110;
                dinnerdepth = 100;
                dinnersize2 = 85;
                dinnerdepth2 = 60;
                yside = -20;
            }
            else if ((winwidth <= 1050) && (winwidth > 900)) {
                dinnersize = 70;
                dinnerdepth = 40;
                yside = -14;
            } else if ((winwidth <= 901) && (winwidth > 480)) {
                dinnersize = 80;
                dinnerdepth = 50;
                yside = -14;
            }
            else {
                dinnersize = 60;
                dinnerdepth = 30;
                yside = -20;
            }
            // Create the chart



            var Arr = [];
            $('#aaa tr').each(function () {
                var cc = this.cells[1].innerHTML;
                var dataArr = ['Disposed', parseFloat(cc)];
                Arr.push(dataArr);
                cc = this.cells[3].innerHTML;
                dataArr = ['Pending Unit', parseFloat(cc)];
                Arr.push(dataArr);
                cc = this.cells[2].innerHTML;
                dataArr = ['Pending at IDCO', parseFloat(cc)];
                Arr.push(dataArr);
                cc = this.cells[4].innerHTML;
                dataArr = ['Major Pending at IDCO', parseFloat(cc)];
                Arr.push(dataArr);
                cc = this.cells[0].innerHTML;
                dataArr = ['Applied', parseFloat(cc)];
                Arr.push(dataArr);
            });

            ////            ////----------old------
            Highcharts.chart('ApaaStatus', {
                colors: ['#25c6da', '#5bae2a', '#cd1c24', '#2196F3', '#9c27b0'],

                chart: {
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 100

                    }
                },
                title: {
                    text: ''

                },
                subtitle: {
                    text: ''
                },
                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y}</b> <br/>'
                },
                plotOptions: {

                    pie: {
                        innerSize: dinnersize2,
                        depth: dinnerdepth2,
                        dataLabels: {
                            enabled: true, format: '{point.y} '
                        },
                        showInLegend: true

                    }
                },
                series: [{
                    name: 'PEAL Status',
                    colorByPoint: true,
                    data: Arr
                }]

            });
            ////            /////----*****************
            //            Highcharts.chart('ServicesStatus', {
            //                colors: ['#25c6da', '#5bae2a', '#cd1c24', '#d48803', '#9c27b0'],
            //                chart: {
            //                    type: 'column'
            //                },
            //                title: {
            //                    text: ''
            //                },
            //                subtitle: {
            //                    text: ''
            //                },
            //                xAxis: {
            //                    categories: [
            //            'Jan',
            //            'Feb',
            //            'Mar'

            //        ],
            //                    crosshair: true
            //                },
            //                yAxis: {
            //                    min: 0,
            //                    title: {
            //                        text: ''
            //                    }
            //                },
            //                tooltip: {
            //                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            //                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
            //            '<td style="padding:0"><b>{point.y}</b></td></tr>',
            //                    footerFormat: '</table>',
            //                    shared: true,
            //                    useHTML: true
            //                },
            //                plotOptions: {
            //                    column: {
            //                        pointPadding: 0.2,
            //                        borderWidth: 0, showInLegend: true
            //                    }
            //                },
            //                series: [{
            //                    name: 'Received',
            //                    data: [44, 55, 33]

            //                }, {
            //                    name: 'Disposed',
            //                    data: [22, 31, 12]

            //                },{
            //                    name: 'Pending at IDCO',
            //                    data: [12, 8, 3]

            //                }, {
            //                    name: 'Pending at UNIT',
            //                    data: [12, 8, 3]

            //                }, {
            //                    name: 'Pending > 30 days',
            //                    data: [10, 16, 18]

            //                }]
            //            });

            $("[id*=ddldept]").change(function () {


                return false;
            })

        });
        function ShowSearchpanel() {
           
            $("[id*=DepartmentServices]").show();
        }	
    </script>
</asp:Content>
