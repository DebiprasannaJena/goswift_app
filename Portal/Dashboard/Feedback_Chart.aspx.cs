//******************************************************************************************************************
// File Name             :   Feedback_Chart.aspx.cs
// Description           :   To display a column chart as per the feedback provided.
// Created by            :   Sushant Kumar Jena
// Created on            :   21-Feb-2018
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
using System.Data;
using EntityLayer.Chart;

public partial class Portal_Dashboard_Feedback_Chart : System.Web.UI.Page
{
    ////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //GetStaffChart("A", 4, 5, 0);
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }
        if (!IsPostBack)
        {
            fillFeedbackQuestion();
        }

    }

    ////// Fill Questions
    private void fillFeedbackQuestion()
    {
        ChartBusinessLayer objBAL = new ChartBusinessLayer();
        Feedback_Chart_Entity objEntity = new Feedback_Chart_Entity();
        DataSet ds = new DataSet();
        try
        {
            objEntity.strAction = "B";
            ds = objBAL.Feedback_Chart_View(objEntity);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DrpDwn_Questions.DataTextField = "VCH_QUESTION";
                DrpDwn_Questions.DataValueField = "INT_QUESTION_ID";
                DrpDwn_Questions.DataSource = ds.Tables[0];
                DrpDwn_Questions.DataBind();
                //DrpDwn_Questions.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            objBAL = null;
            objEntity = null;
            ds = null;
        }
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
}