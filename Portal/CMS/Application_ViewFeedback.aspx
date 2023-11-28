<%@ Page Language="C#"  MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="Application_ViewFeedback.aspx.cs" Inherits="Application_ViewFeedback" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Manage Feedback</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Feedback</a></li><li><a>Application Feedback</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                          <%-- <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="AddCMS.aspx"> 
                              <i class="fa fa-plus"></i>  Add </a>  
                           </div>--%>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="Application_viewFeedback.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> "">  
                              <i class="fa fa-file"></i> Application Feedback </a>  
                           </div>
                           
                        </div>
                        <div class="panel-body">               
                      
                                <div class="row">
                                        <label class="col-md-1 col-sm-1">
                                            Type</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlType" runat="server" class="form-control" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="--SELECT--">
                                                </asp:ListItem>
                                                <asp:ListItem Value="1" Text="Service">
                                                </asp:ListItem>
                                                <asp:ListItem Value="2" Text="Peal">
                                                </asp:ListItem>
                                                <asp:ListItem Value="3" Text="Incentive">
                                                </asp:ListItem>
                                                <asp:ListItem Value="4" Text="PC">
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="table-responsive" id="div1" style="margin-top: 15px;">
                                        <asp:GridView ID="grdRating" runat="server" OnRowDataBound="grdRating_RowDataBound"
                                            AutoGenerateColumns="false" CssClass="table table-bordered">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SI.NO">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblServiceName" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label>
                                                        <asp:Label ID="lblServiceId" runat="server" Visible="false" Text='<%#Eval("INT_SERVICEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="How smooth was the application process?">
                                                    <ItemTemplate>
                                                        <cc1:Rating ID="Rating1" ReadOnly="true"  runat="server" StarCssClass="ratingStar" WaitingStarCssClass = "savedRatingStar" FilledStarCssClass = "filledRatingStar" EmptyStarCssClass = "emptyRatingStar">
                                                        </cc1:Rating>
                                                        <asp:LinkButton ID="lblq1" runat="server"  data-toggle="modal" data-target='<%# "#myModal1"+Eval("INT_SERVICEID") %>' Visible="false" ></asp:LinkButton>
                                                      <%--   <div class="modal fade" id='<%# "myModal1"+Eval("INT_SERVICEID") %>' tabindex="-1" role="dialog" aria-hidden="true">
                                                            <div class="modal-dialog modal-lg">
                                                                <!-- Modal content-->
                                                                <div class="modal-content modal-primary ">
                                                                    <div class="modal-header">
                                                                        <button type="button" class="close" data-dismiss="modal">
                                                                            &times;</button>
                                                                        <h4 class="modal-title">
                                                                            Rating Details</h4>
                                                                    </div>
                                                                    <div class="modal-body">
                                                                        <div class="row">
                                                                            <div class="col-sm-6">
                                                                                <div class="panel panel-default">
                                                                                    <div class="panel-heading">
                                                                                        Successfull Transaction</div>
                                                                                    <div class="panel-body">
                                                                                       
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="panel panel-default">
                                                                                    <div class="panel-heading">
                                                                                        Failure Transaction</div>
                                                                                    <div class="panel-body">
                                                                                       
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <button type="button" class="btn btn-default" data-dismiss="modal">
                                                                            Close</button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="How was the overall GO – SWIFT Experience?">
                                                    <ItemTemplate>
                                                    <cc1:Rating ID="Rating2" runat="server" ReadOnly="true" StarCssClass="ratingStar" WaitingStarCssClass = "savedRatingStar" FilledStarCssClass = "filledRatingStar" EmptyStarCssClass = "emptyRatingStar">
                                                        </cc1:Rating>
                                                        <asp:LinkButton ID="lblq2" runat="server" data-toggle="modal" data-target='<%# "#myModal2"+Eval("INT_SERVICEID") %>' Visible="false"></asp:LinkButton>
                                                             
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Have you used the other features of GO SWIFT ?">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblq3" runat="server" data-toggle="modal" data-target='<%# "#myModal3"+Eval("INT_SERVICEID") %>'></asp:Label>
                                                         <asp:Label ID="lblSPMG" runat="server" CssClass="text-success" style="font-weight:600"></asp:Label><br />
                                                         <asp:Label ID="lblAPAA" runat="server" CssClass="text-primary" style="font-weight:600"></asp:Label><br />
                                                        <asp:Label ID="lblGOCARE" runat="server" CssClass="text-warning" style="font-weight:600"></asp:Label><br />
                                                        <asp:Label ID="lblGOSMILE" runat="server" CssClass="text-info" style="font-weight:600" ></asp:Label><br />
                                                        <asp:Label ID="lblINFOWIZARD" runat="server" CssClass="text-success" style="font-weight:600"></asp:Label>

                                                      
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Would you recommend GO – SWIFT to another investor/friend?">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblq4" runat="server" data-toggle="modal" data-target='<%# "#myModal4"+Eval("INT_SERVICEID") %>'></asp:Label>
                                                        <asp:Label ID="lblYes" runat="server" CssClass="text-success" style="font-weight:600"></asp:Label><br />
                                                        <asp:Label ID="lblNo" runat="server"  CssClass="text-warning" style="font-weight:600"></asp:Label>

                                                      
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="How can we improve GO – SWIFT experience? Please share your ideas and suggestions" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblq5" runat="server" data-toggle="modal" data-target='<%# "#myModal5"+Eval("INT_SERVICEID") %>'></asp:LinkButton>
                                                        <div class="modal fade" id='<%# "myModal5"+Eval("INT_SERVICEID") %>' tabindex="-1" role="dialog" aria-hidden="true">
                                                            <div class="modal-dialog modal-lg">
                                                               
                                                                
                                                            </div>
                                                        </div>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div> 
                           </div>                       
                     </div>
                  </div>
   
    </div> </section>
    <!-- /.content -->
    </div> 
</asp:Content>