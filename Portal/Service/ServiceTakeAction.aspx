<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ServiceTakeAction.aspx.cs" Inherits="ServiceMaster_ServiceTakeAction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Add Take Action</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Mange users</a></li><li><a>Take Action</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                           <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="Addservicemaster.aspx"> 
                              <i class="fa fa-plus"></i>Take Action</a>  
                           </div>
                           <%-- <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="demoitems.aspx"> 
                              <i class="fa fa-file"></i> View List </a>  
                           </div>--%>
                           
                        </div>
                        <div class="panel-body">
                           
                            <div class="table-responsive">
                            <table id="dataTableExample1" class="table table-bordered table-hover">
                                 <thead>
                                  <tr >
                                       <th rowspan="2">Sl#</th>
                                       <th rowspan="2">Application No.</th>
                                       <th rowspan="2">Proposal No.</th>
                                       <th rowspan="2">Service Name</th>
                                       <th rowspan="2">Investor's Name</th>
                                       <th rowspan="2">Requested Date</th>
                                       <th colspan="3"  class="text-center">Action Details</th>
                                       <th rowspan="2">Payment Status</th>
                                       <th rowspan="2">Action</th>
                                    <tr>
                                      <th>Status</th>
                                       <th>Action Taken By</th>
                                       <th>Action to be Taken By</th>
                                    </tr>
                                 </thead>
                                 <tbody>
                                    <tr>
                                       <td>
                                          <div class="checkbox checkbox-info">
                                             <input id="checkbox1" type="checkbox">
                                             <label for="checkbox1">01</label>
                                          </div>
                                       </td>
                                       <td><asp:LinkButton ID="hrfno" runat="server" data-toggle="modal" data-target="#customer2">02112017</asp:LinkButton></td>
                                       <td>TRC 45654</td>
                                       <td>Service 1</td>
                                       <td>Girija Sankar Sahoo</td>
                                       <td>05/06/2017</td>
                                       <td>Pending</td>
                                       <td>Amit Sahoo</td>
                                       <td>Smruti Ranjan Nayak</td>
                                       <td><span class="label-default label label-default">Not Required</span>
                                       </td>
                                       <td>
                                           <button type="button" class="label-warning label label-default" data-toggle="modal" data-target="#customer1">Take Action</button>
                                       </td>
                                    </tr>

                                    <tr>
                                       <td>
                                          <div class="checkbox checkbox-info">
                                             <input id="checkbox2" type="checkbox">
                                             <label for="checkbox1">02</label>
                                          </div>
                                       </td>
                                       <td><asp:LinkButton ID="LinkButton1" runat="server" data-toggle="modal" data-target="#customer2">07112017</asp:LinkButton></td>
                                       <td>TRC 45554</td>
                                       <td>Service 2</td>
                                       <td>Radhika Rani Patri</td>
                                       <td>05/06/2017</td>
                                       <td>Pending</td>
                                       <td>Pradeep Sahoo</td>
                                       <td>Smruti Ranjan Nayak</td>
                                       <td><span class="label-custom label label-default">Required</span>
                                       </td>
                                       <td>
                                           <button type="button" class="label-warning label label-default" data-toggle="modal" data-target="#customer1">Take Action</button>
                                       </td>
                                    </tr>
                                 </tbody>
                              </table>
                           </div>
                        </div>
                     </div>
                  </div>
               </div>
                  <!-- customer Modal1 -->
               <div class="modal fade" id="customer1" tabindex="-1" role="dialog" aria-hidden="true">
                  <div class="modal-dialog modal-lg">
                     <div class="modal-content">
                        <div class="modal-header modal-header-primary">
                           <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                           <h3><i class="fa fa-user m-r-5"></i> Take Action</h3>
                        </div>
                        <div class="modal-body">
                           <div class="row">
                              <div class="col-md-12">
                                 <form class="form-horizontal">
                                    <fieldset>
                                    <div class="panel panel-bd ">
                        <div class="panel-heading">
                         Details
                        </div>
                        <div class="panel-body">
                           <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">Proposal No.</label>
                                    <div class="col-md-4">
                                   <label class="control-label"><span>TRC 45654</span></label>
                                    </div>
                                     <label class="col-md-2" style="color: #000000">Name Of Industries/Enterprises</label>
                                    <div class="col-md-4">
                                    <label class="control-label"><span>TRC 45654</span></label>
                                    </div>
                                    </div>
                                    </div>
                           <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">Investor's Name</label>
                                    <div class="col-md-4">
                                   <label class="control-label"><span>Fly Biman</span></label>
                                    </div>
                                     <label class="col-md-2" style="color: #000000">Industry Type</label>
                                    <div class="col-md-4">
                                     <label class="control-label"><span>Small industry</span></label>
                                    </div>
                                    </div>
                                    </div>
                           <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">Status</label>
                                    <div class="col-md-4">
                                   <label class="control-label"><span>Approved</span></label>
                                    </div>
                                     <label class="col-md-2" style="color: #000000">Details</label>
                                    <div class="col-md-4">
                                      <label class="control-label"><span>Approved</span></label>
                                    </div>
                                    </div>
                                    </div>
                           <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">Action to be Taken By</label>
                                    <div class="col-md-4">
                                   <label class="control-label"><span>Girija Sankar Sahoo</span></label>
                                    </div>
                                     <label class="col-md-2" style="color: #000000">Action Taken By</label>
                                    <div class="col-md-4">
                                     <label class="control-label"><span>Amit Sahoo</span></label>
                                    </div>
                                    </div>
                                    </div>  
                        </div>
                     </div>
                      <div class="panel panel-bd ">
                        <div class="panel-heading">
                        Take Action
                        </div>
                         <div class="panel-body">
                          <div class="form-group">
                                       <div class="row">
                                       <label class="col-md-2">Status</label>
                                          <div class="col-md-4"> 
                                           <select class="form-control">
                                              <option value="volvo">Approve</option>
                                              <option value="saab">Forward</option>
                                              <option value="mercedes">Reject</option>
                                              <option value="audi">Raised Query</option>
                                            </select>
                                            </div>
                                             </div>
                                       </div>
                                   
                                      <div class="form-group">
                                       <div class="row">
                                   <label class="col-md-2">Remark</label>
                                          <div class="col-md-4"> 
                                         <textarea name="address" class="form-control" rows="3"></textarea>
                                            </div>

                                       </div>
                                       </div>
                                   
                                      <div class="form-group">
                                       <div class="row">
                                       <label class="col-md-2">Upload Reference document</label>
                                          <div class="col-md-4"> 
                                           <asp:FileUpload  ID="fulLogo" CssClass="form-control" runat="server"/>
                                            </div>
                                       </div>
                                       </div>
                                       <div class="col-md-10 col-sm-offset-2 form-group user-form-group">
                                          <div class="row">
                                          <button type="submit" class="btn btn-add btn-sm">Save</button>
                                          <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Cancel</button>
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
                           <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">Close</button>
                        </div>
                     </div>
                     <!-- /.modal-content -->
                  </div>
                  <!-- /.modal-dialog -->
               </div>
               <!-- /.modal -->
               <!-- Modal -->    
               <!-- Customer Modal2 -->
               <div class="modal fade" id="customer2" tabindex="-1" role="dialog" aria-hidden="true">
                  <div class="modal-dialog modal-lg">
                     <div class="modal-content">
                        <div class="modal-header modal-header-primary">
                           <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                           <h3><i class="fa fa-user m-r-5"></i>Application Details</h3>
                        </div>
                        <div class="modal-body">
                           <div class="row">
                              <div class="col-md-12">
                                 <form class="form-horizontal">
                                    <fieldset>
                                       <div class="panel-body">
                           <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">Proposal No.</label>
                                    <div class="col-md-4">
                                   <label class="control-label"><span>TRC 45654</span></label>
                                    </div>
                                     <label class="col-md-2" style="color: #000000">Name Of Industries/Enterprises</label>
                                    <div class="col-md-4">
                                    <label class="control-label"><span>TRC 45654</span></label>
                                    </div>
                                    </div>
                                    </div>
                           <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">Investor's Name</label>
                                    <div class="col-md-4">
                                   <label class="control-label"><span>Fly Biman</span></label>
                                    </div>
                                     <label class="col-md-2" style="color: #000000">Industry Type</label>
                                    <div class="col-md-4">
                                     <label class="control-label"><span>Small industry</span></label>
                                    </div>
                                    </div>
                                    </div>
                           <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">Status</label>
                                    <div class="col-md-4">
                                   <label class="control-label"><span>Approved</span></label>
                                    </div>
                                     <label class="col-md-2" style="color: #000000">Details</label>
                                    <div class="col-md-4">
                                      <label class="control-label"><span>Approved</span></label>
                                    </div>
                                    </div>
                                    </div>
                           <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">Action to be Taken By</label>
                                    <div class="col-md-4">
                                   <label class="control-label"><span>Girija Sankar Sahoo</span></label>
                                    </div>
                                     <label class="col-md-2" style="color: #000000">Action Taken By</label>
                                    <div class="col-md-4">
                                     <label class="control-label"><span>Amit Sahoo</span></label>
                                    </div>
                                    </div>
                                    </div>  
                        </div>
                                    </fieldset>
                                 </form>
                              </div>
                           </div>
                        </div>
                        <div class="modal-footer">
                           <button type="button" class="btn btn-danger pull-left" data-dismiss="modal">Close</button>
                        </div>
                     </div>
                     <!-- /.modal-content -->
                  </div>
                  <!-- /.modal-dialog -->
               </div>
               <!-- /.modal -->
            </section>
        <!-- /.content -->
    </div>
</asp:Content>
