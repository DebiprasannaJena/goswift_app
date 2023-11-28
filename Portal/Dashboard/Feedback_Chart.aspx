<%--'*******************************************************************************************************************
' File Name         : Feedback_Chart.aspx
' Description       : To display a column chart as per the feedback provided.
' Created by        : Sushant Kumar Jena
' Created On        : 21-Feb-2018
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="Feedback_Chart.aspx.cs" Inherits="Portal_Dashboard_Feedback_Chart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jQuery-2.1.3.min.js" type="text/javascript"></script>
    <script src="../js/highcharts.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function pageLoad() {

            GetFeedbackDetails();

            $('#ContentPlaceHolder1_DrpDwn_Questions').change(function () {
                GetFeedbackDetails();
            });
        }

        /*----------------------------------------------------------------*/
        ///// Gather data for preparation of Graph
        /*----------------------------------------------------------------*/

        function GetFeedbackDetails() {

            var intQuestionId = $("#ContentPlaceHolder1_DrpDwn_Questions").val();

            var strQuestionType = '';
            //debugger;

            //make the ajax call
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: 'Feedback_Chart.aspx/GetFeedbackCount',
                data: "{'intQuestionId':" + intQuestionId + "}",
                dataType: "json",
                success: function (Result) {
                    //debugger;
                    if (Result.d != null && Result.d != undefined) {

                        strQuestionType = Result.d[0].strQuestion;

                        var serie = new Array();
                        var arrVacant = [];
                        var arrXAxis = new Array();
                        var ResLength = Result.d.length;
                        data = Result.d;

                        if (data.length > 0) {
                            $.each(JSON.parse(JSON.stringify(data)), function (index, value) {
                                var dataArr = [value.strAnswer, parseInt(value.intAnswerCount)];

                                arrVacant.push(dataArr);
                                arrXAxis.push(value.strAnswer);
                            });
                        }

                        DrawFeedbackCountChart(arrVacant, 'divFeedbackChart', strQuestionType, 'Star Rated', 'No. Of Count', arrXAxis);
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

            $('#' + divToBind).highcharts({
                //colors: ["Red", "Blue",  "#90ed7d","#90ed7d", "#434348", "#434348", "#f7a35c", "#f7a35c", "#8085e9", "#8085e9", "#f15c80", "#f15c80", "#e4d354", "#e4d354", "#2b908f", "#2b908f"],
                chart: {
                    type: 'column'
                },
                title: {
                    text: titleOfChart
                },
                xAxis: {
                    categories: arrXAxis
                             , title: {
                                 text: xAxisTitle
                             }
                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: yAxisTitle
                    },
                    min: 0,
                    stackLabels: {
                        enabled: true,
                        style: {
                            fontWeight: 'bold',
                            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                },

                plotOptions: {
                    column: {
                        pointRange: 100000,
                        pointPadding: 0.2,
                        groupPadding: 1,
                        pointWidth: 70
                    },
                    showInLegend: false
                },

                tooltip: {
                    headerFormat: '<b>{point.x}</b><br/>',
                    pointFormat: '{series.name}: {point.y}'
                }
                    , series: [{
                        type: 'column',
                        colorByPoint: true,
                        name: 'Total Count',
                        showInLegend: false,
                        dataLabels: {
                            enabled: true,
                            rotation: 0,
                            color: 'black',
                            align: 'top',
                            //format: '{point.y:.1f}', // one decimal
                            y: 1, // 10 pixels down from the top
                            style: {
                                fontSize: '8px',
                                fontFamily: 'Verdana, sans-serif'
                            }
                        },
                        data: dataCol
                    }]
            });
        }

    </script>
    <script type="text/javascript">

        //        function GetStaffDetails(intQuestionId) {


        //            var intMonth = 0;
        //            var intYear = 0;

        //            var designation = 0;
        //            var strActionCode = '';

        //            var strQuestionType = '';
        //            debugger;

        //            //make the ajax call
        //            $.ajax({
        //                type: "POST",
        //                contentType: "application/json; charset=utf-8",
        //                url: 'Feedback_Chart.aspx/GetStaffChart',
        //                data: "{'strActionCode':'sanc','intYear':" + intYear + ", 'intMonth':" + intMonth + ", 'designation':" + designation + "}",
        //                dataType: "json",
        //                success: function (Result) {
        //                    debugger;
        //                    if (Result.d != null && Result.d != undefined) {

        //                        strQuestionType = Result.d[0].strQuestion;

        //                        var ArrSers = [];
        //                        var ArrXAxis = new Array();
        //                        var ResLength = Result.d.length;
        //                        data = Result.d;
        //                        ////*************---------------------
        //                        if (data.length > 0) {
        //                            $.each(JSON.parse(JSON.stringify(data)), function (index, value) {
        //                                var dataArr = [value.strAnswer, parseInt(value.intAnswerCount)];

        //                                ArrSers.push(dataArr);
        //                            });
        //                        }
        //                        else {
        //                            var dataArr = ['Empty', parseInt(0)];
        //                            ArrSers.push(dataArr);
        //                        }

        //                        $('#dvStaffSelection').highcharts({

        //                            chart: {
        //                                plotBackgroundColor: null,
        //                                plotBorderWidth: null,
        //                                plotShadow: false,
        //                                type: 'column'
        //                            },
        //                            title: {
        //                                text: 'Monthly Collection'
        //                            },
        //                            tooltip: {
        //                                pointFormat: '{series.name}: <b>{point.y:.2f}</b>'
        //                            },
        //                            xAxis: {
        //                                title: {
        //                                    text: 'Collection (in Cr.)'
        //                                }
        //                            },
        //                            yAxis: {
        //                                title: {
        //                                    text: 'Collection (in Cr.)'
        //                                }
        //                            },
        //                            plotOptions: {
        //                                column: {
        //                                    allowPointSelect: true,
        //                                    cursor: 'pointer',
        //                                    dataLabels: {
        //                                        enabled: true
        //                                    },
        //                                    showInLegend: false
        //                                }
        //                            },
        //                            series: [{
        //                                type: 'column',
        //                                colorByPoint: true,
        //                                name: 'Collection',
        //                                showInLegend: false,
        //                                dataLabels: {
        //                                    enabled: false,
        //                                    rotation: 0,
        //                                    color: 'black',
        //                                    align: 'top',
        //                                    //                        format: '{point.y:.1f}', // one decimal
        //                                    y: 1, // 10 pixels down from the top
        //                                    style: {
        //                                        fontSize: '8px',
        //                                        fontFamily: 'Verdana, sans-serif'
        //                                    }
        //                                },
        //                                data: ArrSers
        //                            }]
        //                        });
        //                    }
        //                },
        //                error: function (Result) {
        //                }
        //            });
        //        }



    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    Feedback Report</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>MIS Report</a></li><li><a>View Feedback</a></li></ul>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <br />
                        <div class="form-group">
                            <label class="col-md-2 col-sm-3">
                                Questions
                            </label>
                            <div class="col-sm-5">
                                <span class="colon">:</span>
                                <asp:DropDownList ID="DrpDwn_Questions" runat="server" CssClass="form-control" ToolTip="Select Question to View Reports !!">
                                </asp:DropDownList>
                            </div>
                            <div class="clearfix">
                            </div>
                        </div>
                        <hr />
                        <div id="divFeedbackChart" class="graph-container" style="height: 380px; width: 600px">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
