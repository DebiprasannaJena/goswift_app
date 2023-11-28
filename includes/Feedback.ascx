<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Feedback.ascx.cs" Inherits="includes_Feedback" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script type="text/javascript">

    var msgTitle = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

    function cal(e) {

        var idd = e.id;
        //alert(e.id);
        var arr = idd.split("_");
        //alert(arr);
        var hdfid = arr[arr.length - 1];
        //alert(hdfid);
        //            alert($('#' + e.id + '_RatingExtender_ClientState').val());
        $('#Feedback1_hdf' + hdfid).val($('#' + e.id + '_RatingExtender_ClientState').val());
    }

    /*--------------------------------------------------------------*/
    /////// Alert and Redirect for Incentive/PC/Certification Pages
    /*--------------------------------------------------------------*/
    function alertredirect(msg, serviceId) {
        jAlert(msg, msgTitle, function (r) {
            if (r) {

                location.href = "../Feedback_Rating.aspx?id=" + serviceId;
            }
            else {
                return false;
            }
        });
    }

    /*--------------------------------------------------------------*/
    /////// Alert and Redirect for PEAL
    /*--------------------------------------------------------------*/
    function alertredirectPEAL(msg) {
        jAlert(msg, msgTitle, function (r) {
            if (r) {
                location.href = "../Feedback_Rating.aspx?id=PEAL";
                return true;
            }
            else {
                return false;
            }
        });
    }

    /*--------------------------------------------------------------*/
    /////// Alert and Redirect for Services
    /*--------------------------------------------------------------*/
    function alertredirectService(msg) {
        jAlert(msg, msgTitle, function (r) {
            if (r) {
                location.href = "Feedback_Rating.aspx?id=SERVICE";
                return true;
            }
            else {
                return false;
            }
        });
    }

    $(document).ready(function () {
        $('#Feedback1_btnSubmit').on('click', function () {
            if ($('#Feedback1_hdf1').val() == '') {
                jAlert('<strong>Please Provide Rating for Question No.1 !!</strong>', msgTitle);
                return false;
            }
            if ($('#Feedback1_hdf2').val() == '') {
                jAlert('<strong>Please Provide Rating for Question No.2 !!</strong>', msgTitle);
                return false;
            }

            var checkcnt = $("#Feedback1_chk3 :input:checked").length;
            if (checkcnt == 0) {
                jAlert('<strong>Please Answer for Question No.3 !!</strong>', msgTitle);
                return false;
            }

            var radcnt = $("#Feedback1_rdb4 :radio:checked").length;
            if (radcnt == 0) {
                jAlert('<strong>Please Answer for Question No.4 !!</strong>', msgTitle);
                return false;
            }
        });
    });


</script>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
<div class="container">
    <div class="rating-sec">
        <h4>
            Feedback
            <asp:HiddenField ID="HiddenField1" runat="server" />
        </h4>
        <table id="tbl" runat="server" class="table border0">
        </table>
        <div class="row">
            <div class="col-md-6">
                <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>--%>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary"
                    OnClick="btnSubmit_Click" />
                <%--</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
</div>
