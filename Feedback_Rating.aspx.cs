
//******************************************************************************************************************
// File Name             :   Feedback_Rating.aspx.cs
// Description           :   To display Feedback rating in bar chart after each feedback submission
// Created by            :   Sushant Kumar Jena
// Created on            :   01-Mar-2018
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using EntityLayer.Chart;
using System.Data;

public partial class Feedback_Rating : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Manoj Kumar Behera Code Begin
        if (!IsPostBack)
        {
            
        }
        //Manoj Kumar Behera Code End
    }

    ////// WebMethod used to populate data from tables
    [WebMethod]
    public static List<Feedback_Chart_Entity> GetFeedbackCount(int intQuestionId)
    {
        ChartBusinessLayer objBAL = new ChartBusinessLayer();
        Feedback_Chart_Entity objEntity = new Feedback_Chart_Entity();

        objEntity.intQuestionId = intQuestionId;
        objEntity.strAction = "A";

        List<Feedback_Chart_Entity> lstBarChart = new List<Feedback_Chart_Entity>();

        DataSet ds = new DataSet();
        ds = objBAL.Feedback_Chart_View(objEntity);

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Feedback_Chart_Entity objPieChart = new Feedback_Chart_Entity();
                objPieChart.intQuestionId = Convert.ToInt32(ds.Tables[0].Rows[i]["INT_QUESTION_ID"].ToString());
                objPieChart.intAnswerCount = Convert.ToInt32(ds.Tables[0].Rows[i]["INT_ANS_COUNT"].ToString());
                objPieChart.strAnswer = ds.Tables[0].Rows[i]["VCH_ANS"].ToString();
                objPieChart.strQuestion = ds.Tables[0].Rows[i]["VCH_QUESTION"].ToString();
                lstBarChart.Add(objPieChart);
            }
        }
        return lstBarChart;
    }

    ////// Button click event
    protected void Btn_Home_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            string strId = Request.QueryString["id"].ToString();

            if (strId == "500")
            {
                Response.Redirect("incentives/appliedlistwithdetails.aspx");
            }
            else if (strId == "501")
            {
                Response.Redirect("pcViewPage.aspx");
            }
            else if (strId == "502")
            {
                Response.Redirect("incentives/Incentiveoffered.aspx");
            }
            else if (strId == "PEAL")
            {
                Response.Redirect("Proposals.aspx");
            }
            else if (strId == "SERVICE")
            {
                Response.Redirect("DepartmentClearance.aspx");
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
}