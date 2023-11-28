using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using AjaxControlToolkit;

public partial class Application_ViewFeedback : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
        
    }

    public void BindRatingGrid(string action)
    { 
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_FeedBackBind";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Action", action);
        cmd.Connection = conn;
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            grdRating.DataSource = ds.Tables[0];
            grdRating.DataBind();
        }
    }

    
    protected void grdRating_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Rating rt1 = (Rating)e.Row.FindControl("Rating1");
               
                Rating rt2 = (Rating)e.Row.FindControl("Rating2");
               

                Label lblServiceId = (Label)e.Row.FindControl("lblServiceId");
                LinkButton lblq1 = (LinkButton)e.Row.FindControl("lblq1");
                LinkButton lblq2 = (LinkButton)e.Row.FindControl("lblq2");
                Label lblq3 = (Label)e.Row.FindControl("lblq3");
                Label lblq4 = (Label)e.Row.FindControl("lblq4");
                LinkButton lblq5 = (LinkButton)e.Row.FindControl("lblq5");
                Label lblSPMG = (Label)e.Row.FindControl("lblSPMG");
                Label lblAPAA = (Label)e.Row.FindControl("lblAPAA");
                Label lblGOCARE = (Label)e.Row.FindControl("lblGOCARE");
                Label lblGOSMILE = (Label)e.Row.FindControl("lblGOSMILE");
                Label lblINFOWIZARD = (Label)e.Row.FindControl("lblINFOWIZARD");
                Label lblYes = (Label)e.Row.FindControl("lblYes");
                Label lblNo = (Label)e.Row.FindControl("lblNo");


                int serviceId = Convert.ToInt32(((Label)e.Row.FindControl("lblServiceId")).Text);
                decimal totappused = 0;
                for (int i = 1; i <= 5; i++)
                {
                    decimal yes = 0; decimal No = 0;
                    decimal total = 0;
                    decimal SPMG=0, APAA=0, GOCARE=0, GOSMILE=0, INFOWIZARD = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "USP_FeedBackBind";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "R" + i);
                    cmd.Parameters.AddWithValue("@INT_SERVICE_ID", lblServiceId.Text);
                   
                    cmd.Connection = conn;
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    ds = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            if (i == 1)
                            {
                                lblq1.Text = ds.Tables[0].Rows[j]["ANS"].ToString();
                                rt1.CurrentRating = Convert.ToInt32(ds.Tables[0].Rows[j]["ANS"].ToString());
                               

                            }
                            else if (i == 2)
                            {
                                lblq2.Text = ds.Tables[0].Rows[j]["ANS"].ToString();
                                rt2.CurrentRating = Convert.ToInt32(ds.Tables[0].Rows[j]["ANS"].ToString());
                            }
                            else if (i == 3)
                            {
                                string[] arr = ds.Tables[0].Rows[j]["ANS"].ToString().Split(',').ToArray();
                                totappused = totappused + 1;

                                for (int k = 0; k < arr.Count(); k++)
                                {
                                    if (arr[k].ToString().Trim() == "SPMG")
                                    {
                                        SPMG = SPMG + 1;
                                    }
                                    else if (arr[k].ToString().Trim() == "APAA")
                                    {
                                        APAA = APAA + 1;
                                        

                                    }
                                    else if (arr[k].ToString().Trim() == "GO CARE")
                                    {
                                        GOCARE = GOCARE + 1;
                                        

                                    }
                                    else if (arr[k].ToString().Trim() == "GO SMILE")
                                    {
                                        GOSMILE = GOSMILE + 1;
                                       

                                    }
                                    else if (arr[k].ToString().Trim() == "INFO WIZARD")
                                    {
                                        INFOWIZARD = INFOWIZARD + 1;
                                       

                                    }
                                }
                                decimal SPMGperc = (SPMG / totappused) * 100;
                                decimal APAAperc = (APAA / totappused) * 100;
                                decimal GOCAREperc = (GOCARE / totappused) * 100;
                                decimal GOSMILEperc = (GOSMILE / totappused) * 100;
                                decimal INFOWIZARDperc = (INFOWIZARD / totappused) * 100;

                               // lblq3.Text = "SPMG:" + SPMGperc.ToString("0.00") + "%, APAA:" + APAAperc.ToString("0.00") + "%, GO CARE:" + GOCAREperc.ToString("0.00") + "%, GO SMILE:" + GOSMILEperc.ToString("0.00") + "%, INFO WIZARD:" + INFOWIZARDperc.ToString("0.00")+"%";
                                lblSPMG.Text = "SPMG:" + SPMGperc.ToString("0.00") + "%";
                                lblAPAA.Text = "APAA:" + APAAperc.ToString("0.00") + "%";
                                lblGOCARE.Text = "GO CARE:" + GOCAREperc.ToString("0.00") + "%";
                                lblGOSMILE.Text = "GO SMILE:" + GOSMILEperc.ToString("0.00") + "%";
                                lblINFOWIZARD.Text = "INFO WIZARD:" + INFOWIZARDperc.ToString("0.00") + "%";
                            }
                            else if (i == 4)
                            {
                                
                                if (ds.Tables[0].Rows[j]["ANS"].ToString() == "Yes,")
                                {
                                    yes = yes + 1;
                                    total = total + 1;
                                }
                                else if (ds.Tables[0].Rows[j]["ANS"].ToString() == "No,")
                                {
                                    No = No + 1;
                                    total = total + 1;

                                }
                                decimal perc = (yes / total )* 100;
                                decimal perc1 = (No / total) * 100;
                                //lblq4.Text = "YES:" + perc.ToString("0.00") + "% ,  NO:" + perc1.ToString("0.00") + "%";
                                lblYes.Text = "YES:" + perc.ToString("0.00") + "%";
                                lblNo.Text = "NO:" + perc1.ToString("0.00") + "%";
                            }
                            else if (i == 5)
                            {
                                lblq5.Text += ds.Tables[0].Rows[j]["ANS"].ToString()+',';
                            }
                        }
                    }
                }
            }
        }
        catch(Exception ex)
        {
        
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedItem.Value == "1")
        {
            BindRatingGrid("S");
        }
        else if (ddlType.SelectedItem.Value == "2")
        {
            BindRatingGrid("P");
        }
        else if (ddlType.SelectedItem.Value == "3")
        {
            BindRatingGrid("IV");
        }
    }
}