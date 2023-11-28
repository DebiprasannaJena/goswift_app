<%--'*******************************************************************************************************************
' File Name         : Feedback_Rating.aspx
' Description       : To display feedback rating in bar chart.
' Created by        : Sushant Kumar Jena
' Created On        : 01-Mar-2018
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Feedback_Rating.aspx.cs"
    Inherits="Feedback_Rating" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script src="Portal/js/jQuery-2.1.3.min.js" type="text/javascript"></script>
    <script src="Portal/js/highcharts.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc2:header ID="header" runat="server" />
        <div class="container wrapper">
            <div class="registration-div investors-bg">
                <div id="exTab1">
                    <div class="investrs-tab">
                        <uc4:investoemenu ID="ineste" runat="server" />
                    </div>
                    <div class="tab-content clearfix">
                        <div class="tab-pane active" id="1a">
                            <div class="form-sec">
                                <div class="form-header">
                                    <h2>Feedback Rating</h2>
                                </div>
                                <div class="form-body minheight350">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div id="Div_Question_1" style="height: 380px; width: 500px">
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div id="Div_Question_2" style="height: 380px; width: 500px">
                                                </div>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div id="Div_Question_3" style="height: 380px; width: 500px">
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div id="Div_Question_4" style="height: 380px; width: 500px">
                                                </div>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-12 text-right">
                                                <asp:Button ID="Btn_Home" runat="server" Text="Go to Home" class="btn btn-success"
                                                    OnClick="Btn_Home_Click" />
                                            </div>
                                            <div class="clearfix">
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
        <uc3:footer ID="footer" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">

        function pageLoad() {
            GetFeedbackDetails('1', 'Div_Question_1');
            GetFeedbackDetails('2', 'Div_Question_2');
            GetFeedbackDetails('3', 'Div_Question_3');
            GetFeedbackDetails('4', 'Div_Question_4');
        }

        /*----------------------------------------------------------------*/
        ///// Gather data for preparation of Graph
        /*----------------------------------------------------------------*/
        function GetFeedbackDetails(intQuestionId, divToBind) {

            var strQuestionType = '';
            //debugger;

            //make the ajax call
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: 'Feedback_Rating.aspx/GetFeedbackCount',
                data: "{'intQuestionId':" + intQuestionId + "}",
                dataType: "json",
                success: function (Result) {
                    //debugger;
                    if (Result.d != null && Result.d != undefined) {

                        strQuestionType = Result.d[0].strQuestion;

                        var serie = new Array();
                        var arrCount = new Array();
                        var arrVacant = [];

                        var arrXAxis = [];
                        var ResLength = Result.d.length;
                        data = Result.d;

                        for (var i = 0; i < ResLength; i++) {

                            arrCount.push(Result.d[i].intAnswerCount);

                            if (intQuestionId == 1 || intQuestionId == 2) {

                                arrXAxis.push(Result.d[i].strAnswer + ' Star');
                                serie.push({ "name": Result.d[i].strAnswer + ' Star', "data": arrCount });
                            }
                            else {
                                arrXAxis.push(Result.d[i].strAnswer);
                                serie.push({ "name": Result.d[i].strAnswer, "data": arrCount });
                            }
                            arrCount = new Array();
                        }


                        //                        if (data.length > 0) {
                        //                            $.each(JSON.parse(JSON.stringify(data)), function (index, value) {
                        //                                var dataArr = [value.strAnswer, parseInt(value.intAnswerCount)];
                        //                                arrVacant.push(dataArr);

                        //                                if (intQuestionId == 1 || intQuestionId == 2) {

                        //                                    arrXAxis.push('Star ' + value.strAnswer);
                        //                                }
                        //                                else {
                        //                                    arrXAxis.push(value.strAnswer);
                        //                                }
                        //                            });
                        //                        }

                        DrawFeedbackCountChart(serie, divToBind, strQuestionType, 'Star Rated', 'No. Of Count', arrXAxis);
                    }
                    else {
                        alert("No data found");
                    }
                },
                error: function (Result) {
                    //alert("Error occured");
                }
            });
        }

        /*----------------------------------------------------------------*/
        ///// Draw Graph
        /*----------------------------------------------------------------*/
        function DrawFeedbackCountChart(dataCol, divToBind, titleOfChart, xAxisTitle, yAxisTitle, arrXAxis) {
            debugger;
            var chart = new Highcharts.Chart({
                chart: {
                    type: 'bar',
                    renderTo: divToBind
                },
                title: {
                    text: titleOfChart
                },
                xAxis: {

                    categories: [''],
                    title: {
                        text: xAxisTitle
                    }

                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: yAxisTitle
                    },
                    min: 0,
                    labels: {
                        overflow: 'justify'
                    }
                },

                plotOptions: {
                    bar: {
                        dataLabels: {
                            enabled: true
                        }
                    }
                    , grouping: false

                },

                tooltip: {
                    headerFormat: '<b>{point.x}</b><br/>',
                    pointFormat: '{series.name}: {point.y}'
                }
                , series: dataCol

                //    , series: [{
                //        type: 'bar',
                //        colorByPoint: true,
                //        name: 'Total Count',
                //        showInLegend: true,
                //        dataLabels: {
                //            enabled: true,
                //            rotation: 0,
                //            color: 'black',
                //            align: 'top',
                //            //format: '{point.y:.1f}', // one decimal
                //            y: 1, // 10 pixels down from the top
                //            style: {
                //                fontSize: '8px',
                //                fontFamily: 'Verdana, sans-serif'
                //            }
                //        },
                //        data: dataCol
                //    }]
            });
        }

    </script>
</body>
</html>