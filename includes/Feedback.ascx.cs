using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;

public partial class includes_Feedback : System.Web.UI.UserControl
{
    private static string m_ServiceId = string.Empty;

    private static string m_ApplicationUniqueID = string.Empty;

    # region Properties

    public string ServiceId
    {

        get { return m_ServiceId; }

        set { m_ServiceId = value; }

    }

    public string ApplicationUniqueID
    {

        get { return m_ApplicationUniqueID; }

        set { m_ApplicationUniqueID = value; }

    }
    # endregion
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ApplicationUniqueID = ((this.Parent.Page).FindControl("hdApplicationUniqueID") as HiddenField).Value;
            ServiceId = ((this.Parent.Page).FindControl("hdServiceId") as HiddenField).Value;
        }
        BindControlAndQuestion();
    }

    public void BindControlAndQuestion()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_FeedBackBind";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Action", "B");
        cmd.Connection = conn;
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                HtmlTableRow tr = new HtmlTableRow();
                HtmlTableCell td = new HtmlTableCell();

                //td.InnerText = (i + 1).ToString() + ".";
                //tr.Cells.Add(td);

                td = new HtmlTableCell();
                td.InnerText = (i + 1).ToString() + ". " + ds.Tables[0].Rows[i]["VCH_QUESTION"].ToString();
                td.Style.Equals("width:50%;");
                //tr.Cells.Add(td);

                string qustType = ds.Tables[0].Rows[i]["VCH_QUESTION_TYPE"].ToString();
                if (qustType == "Rating")
                {
                    // td = new HtmlTableCell();
                    Rating rt = new Rating();
                    HiddenField hd = new HiddenField();
                    hd.ID = "hdf" + ds.Tables[0].Rows[i]["INT_QUESTION_ID"].ToString();
                    rt.ID = "rt_" + ds.Tables[0].Rows[i]["INT_QUESTION_ID"].ToString();
                    rt.StarCssClass = "ratingStar";
                    rt.WaitingStarCssClass = "savedRatingStar";
                    rt.FilledStarCssClass = "filledRatingStar";
                    rt.EmptyStarCssClass = "emptyRatingStar";
                    rt.RatingDirection = RatingDirection.LeftToRightTopToBottom;
                    rt.Attributes.Add("onclick", "cal(this)");
                    //rt.AutoPostBack = true;
                    // rt.Changed += new RatingEventHandler(OnRatingChanged);
                    // rt.Changed += new RatingEventHandler(OnRatingChanged);
                    td.Controls.Add(rt);
                    td.Controls.Add(hd);
                    tr.Cells.Add(td);
                    tbl.Rows.Add(tr);
                }
                else if (qustType == "TextBox")
                {
                    // td = new HtmlTableCell();
                    TextBox txt = new TextBox();
                    txt.ID = "txt" + ds.Tables[0].Rows[i]["INT_QUESTION_ID"].ToString();
                    txt.TextMode = TextBoxMode.MultiLine;
                    txt.CssClass = "form-control";
                    td.Controls.Add(txt);
                    tr.Cells.Add(td);
                    tbl.Rows.Add(tr);
                }
                else if (qustType == "Checkbox")
                {
                    // td = new HtmlTableCell();

                    string strQuestionId = ds.Tables[0].Rows[i]["INT_QUESTION_ID"].ToString();

                    CheckBoxList chk = new CheckBoxList();
                    chk.ID = "chk" + strQuestionId;
                    chk.RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Vertical;
                    string val = ds.Tables[0].Rows[i]["VCH_VALUE"].ToString();
                    string[] itmlist = (val).Split(',');
                    for (int j = 0; j < itmlist.Count(); j++)
                    {
                        if (strQuestionId == "3")
                        {
                            string strItem = itmlist[j].ToString().Trim();

                            DataView dataView = ds.Tables[1].DefaultView;
                            dataView.RowFilter = "VCH_VALUE = '" + strItem + "'";

                            chk.Items.Add(new ListItem(dataView[0]["VCH_FULL_FORM"].ToString(), strItem));
                        }
                        else
                        {
                            chk.Items.Add(itmlist[j].ToString());
                        }
                    }
                    td.Controls.Add(chk);

                    tr.Cells.Add(td);
                    tbl.Rows.Add(tr);
                }
                else if (qustType == "Radiobuttonlist")
                {
                    //td = new HtmlTableCell();

                    RadioButtonList rdb = new RadioButtonList();
                    rdb.ID = "rdb" + ds.Tables[0].Rows[i]["INT_QUESTION_ID"].ToString();
                    rdb.RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal;
                    string val = ds.Tables[0].Rows[i]["VCH_VALUE"].ToString();
                    string[] itmlist = (val).Split(',');
                    for (int j = 0; j < itmlist.Count(); j++)
                    {
                        rdb.Items.Add(itmlist[j].ToString());
                    }
                    td.Controls.Add(rdb);

                    tr.Cells.Add(td);
                    tbl.Rows.Add(tr);
                }
            }
        }
    }

    protected void OnRatingChanged(object sender, RatingEventArgs e)
    {
        Rating rt = sender as Rating;
        int rtn = rt.CurrentRating;

    }
    public void RaiseCallbackEvent(String eventArgument)
    {
        string rt = eventArgument;
    }

    public string GetCallbackResult()
    {
        string strr = "";
        return strr;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        feedbackValue objfd = new feedbackValue();
        List<feedbackValue> lst = new List<feedbackValue>();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string ans = "";
                string qustType = ds.Tables[0].Rows[i]["VCH_QUESTION_TYPE"].ToString();
                if (qustType == "Rating")
                {
                    ans = ((HiddenField)tbl.FindControl("hdf" + ds.Tables[0].Rows[i]["INT_QUESTION_ID"].ToString())).Value;
                    ((HiddenField)tbl.FindControl("hdf" + ds.Tables[0].Rows[i]["INT_QUESTION_ID"].ToString())).Value = "";
                }
                else if (qustType == "TextBox")
                {
                    ans = ((TextBox)tbl.FindControl("txt" + ds.Tables[0].Rows[i]["INT_QUESTION_ID"].ToString())).Text;
                    ((TextBox)tbl.FindControl("txt" + ds.Tables[0].Rows[i]["INT_QUESTION_ID"].ToString())).Text = "";
                }
                else if (qustType == "Checkbox")
                {
                    foreach (ListItem itm in ((CheckBoxList)tbl.FindControl("chk" + ds.Tables[0].Rows[i]["INT_QUESTION_ID"].ToString())).Items)
                    {
                        if (itm.Selected == true)
                        {
                            ans = ans + itm.Value + ",";
                            itm.Selected = false;
                        }
                    }
                }
                else if (qustType == "Radiobuttonlist")
                {
                    foreach (ListItem itm in ((RadioButtonList)tbl.FindControl("rdb" + ds.Tables[0].Rows[i]["INT_QUESTION_ID"].ToString())).Items)
                    {
                        if (itm.Selected == true)
                        {
                            ans = ans + itm.Text + ",";
                            itm.Selected = false;
                        }
                    }
                }

                objfd.QuestionID = ds.Tables[0].Rows[i]["INT_QUESTION_ID"].ToString();
                objfd.ans = ans;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "USP_FeedBackBind";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "I");
                cmd.Parameters.AddWithValue("@INT_QUESTION_ID", objfd.QuestionID);
                cmd.Parameters.AddWithValue("@VCH_ANS", objfd.ans);
                //cmd.Parameters.AddWithValue("@INT_CREATEDBY", "");
                cmd.Parameters.AddWithValue("@INT_SERVICE_ID", ServiceId);
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNIQUEID", ApplicationUniqueID);
                cmd.Parameters.AddWithValue("@returnval", SqlDbType.Int);
                cmd.Parameters["@returnval"].Direction = ParameterDirection.Output;
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@returnval"].Value.ToString();
            }
        }

        string strMessage = "Thank you for your feedback";
        if (ServiceId == "500" || ServiceId == "501" || ServiceId == "502")////---- sent from Incentive Forms type OR PC page --------
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + strMessage + "','" + ServiceId + "');</script>", false);
        }
        else if (ServiceId == "1053") /////--- PEAL
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirectPEAL('" + strMessage + "');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirectService('" + strMessage + "');</script>", false);
        }
    }

    public class feedbackValue
    {
        public string QuestionID { get; set; }
        public string ans { get; set; }
    }
}