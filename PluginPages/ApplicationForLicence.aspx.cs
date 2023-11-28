using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;


public partial class PluginPages_ApplicationForLicence : System.Web.UI.Page
{
    DataTable dtable;
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FirstGridViewRow();
        }
    }
    private void FirstGridViewRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("NameAddress", typeof(string)));
        dt.Columns.Add(new DataColumn("Cerification", typeof(string)));
        dt.Columns.Add(new DataColumn("NameOfOperation", typeof(string)));
        dt.Columns.Add(new DataColumn("NatureOfEstablishment", typeof(string)));
        dt.Columns.Add(new DataColumn("ContractWork", typeof(string)));
        dt.Columns.Add(new DataColumn("NameManager", typeof(string)));
        dt.Columns.Add(new DataColumn("MaximumNoOfEmp", typeof(string)));
        

        dr = dt.NewRow();

        dr["NameAddress"] = string.Empty;
        dr["Cerification"] = string.Empty;
        dr["NameOfOperation"] = string.Empty;
        dr["NatureOfEstablishment"] = string.Empty;
        dr["ContractWork"] = string.Empty;
        dr["NameManager"] = string.Empty;
        dr["MaximumNoOfEmp"] = string.Empty;



        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;

        grvAddmore.DataSource = dt;
        grvAddmore.DataBind();


    }
    private void AddNewRow()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {

                    TextBox txtAddress =
                      (TextBox)grvAddmore.Rows[rowIndex].Cells[0].FindControl("txtAddress");
                    TextBox txtCerification =
                     (TextBox)grvAddmore.Rows[rowIndex].Cells[1].FindControl("txtCerification");
                    TextBox txtOpeEstab =
                     (TextBox)grvAddmore.Rows[rowIndex].Cells[2].FindControl("txtOpeEstab");
                    TextBox txtNatureEsta =
                     (TextBox)grvAddmore.Rows[rowIndex].Cells[3].FindControl("txtNatureEsta");
                    TextBox txtContWrk =
                    (TextBox)grvAddmore.Rows[rowIndex].Cells[3].FindControl("txtContWrk");
                    TextBox txtMangName =
                    (TextBox)grvAddmore.Rows[rowIndex].Cells[3].FindControl("txtMangName");
                    TextBox txtMaxEmp =
                    (TextBox)grvAddmore.Rows[rowIndex].Cells[3].FindControl("txtMaxEmp");

                    drCurrentRow = dtCurrentTable.NewRow();


                    dtCurrentTable.Rows[i - 1]["NameAddress"] = txtAddress.Text;
                    dtCurrentTable.Rows[i - 1]["Cerification"] = txtCerification.Text;
                    dtCurrentTable.Rows[i - 1]["NameOfOperation"] = txtOpeEstab.Text;
                    dtCurrentTable.Rows[i - 1]["NatureOfEstablishment"] = txtNatureEsta.Text;
                    dtCurrentTable.Rows[i - 1]["ContractWork"] = txtContWrk.Text;
                    dtCurrentTable.Rows[i - 1]["NameManager"] = txtMangName.Text;
                    dtCurrentTable.Rows[i - 1]["MaximumNoOfEmp"] = txtMaxEmp.Text;



                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);

                ViewState["CurrentTable"] = dtCurrentTable;

                grvAddmore.DataSource = dtCurrentTable;
                grvAddmore.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData();
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TextBox txtAddress =
                      (TextBox)grvAddmore.Rows[rowIndex].Cells[0].FindControl("txtAddress");
                    TextBox txtCerification =
                     (TextBox)grvAddmore.Rows[rowIndex].Cells[1].FindControl("txtCerification");
                    TextBox txtOpeEstab =
                     (TextBox)grvAddmore.Rows[rowIndex].Cells[2].FindControl("txtOpeEstab");
                    TextBox txtNatureEsta =
                     (TextBox)grvAddmore.Rows[rowIndex].Cells[3].FindControl("txtNatureEsta");
                    TextBox txtContWrk =
                    (TextBox)grvAddmore.Rows[rowIndex].Cells[4].FindControl("txtContWrk");
                    TextBox txtMangName =
                    (TextBox)grvAddmore.Rows[rowIndex].Cells[5].FindControl("txtMangName");
                    TextBox txtMaxEmp =
                    (TextBox)grvAddmore.Rows[rowIndex].Cells[6].FindControl("txtMaxEmp");

                    txtAddress.Text = dt.Rows[i]["NameAddress"].ToString();

                    txtCerification.Text = dt.Rows[i]["Cerification"].ToString();
                    txtOpeEstab.Text = dt.Rows[i]["NameOfOperation"].ToString();
                    txtNatureEsta.Text = dt.Rows[i]["NatureOfEstablishment"].ToString();
                    txtContWrk.Text = dt.Rows[i]["ContractWork"].ToString();
                    txtMangName.Text = dt.Rows[i]["NameManager"].ToString();
                    txtMaxEmp.Text = dt.Rows[i]["MaximumNoOfEmp"].ToString();



                    rowIndex++;
                }
            }
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRow();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        //DataTable DynamicForm = new DataTable();
        //DynamicForm.TableName = "MyTable";
        ////DynamicForm.Columns.Add(new DataColumn("INT_ITEMID"));
        //DynamicForm.Columns.Add(new DataColumn("INT_APPLICATIONID"));
        //DynamicForm.Columns.Add(new DataColumn("VCH_NATUREOFDEMAND"));
        //DynamicForm.Columns.Add(new DataColumn("INT_NUMBER"));
        //DynamicForm.Columns.Add(new DataColumn("INT_CONNECTEDLOADEACH"));
        //DynamicForm.Columns.Add(new DataColumn("INT_TOTALLOAD"));

        //DynamicForm.Columns.Add(new DataColumn("INT_DELETED_FLAG"));
        //foreach (GridViewRow gv in grvAddmore.Rows)
        //{
        //    string rating = string.Empty;
        //    TextBox txtNature = gv.FindControl("txtNature") as TextBox;
        //    TextBox txtNumber = gv.FindControl("txtNumber") as TextBox;
        //    TextBox txtConnLoad = gv.FindControl("txtConnLoad") as TextBox;
        //    TextBox txtTotalConnLoad = gv.FindControl("txtTotalConnLoad") as TextBox;



        //    DataRow dr = DynamicForm.NewRow();
        //    //dr["INT_ITEMID"] = 1;
        //    dr["INT_APPLICATIONID"] = 1;
        //    dr["VCH_NATUREOFDEMAND"] = txtNature.Text.ToString();
        //    dr["INT_NUMBER"] = Convert.ToInt32(txtNumber.Text.ToString());
        //    dr["INT_CONNECTEDLOADEACH"] = Convert.ToInt32(txtTotalConnLoad.Text.ToString());
        //    dr["INT_TOTALLOAD"] = Convert.ToInt32(txtConnLoad.Text.ToString());

        //    dr["INT_DELETED_FLAG"] = 0;
        //    DynamicForm.Rows.Add(dr);
        //}


        //string xmltable = GetSTRXMLResult(DynamicForm);
        //SqlConnection con = new SqlConnection(connectionString);
        //con.Open();
        //SqlCommand cmd = new SqlCommand("USP_GENERAL_ELECTRICALINFO", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@P_XMLTABLE", xmltable);
        //int status = cmd.ExecuteNonQuery();
        //con.Close();
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Saved Successfully.');", true);
    }
    public static string GetSTRXMLResult(DataTable dtTable)
    {
        string strXMLResult = string.Empty;
        if (dtTable.Rows.Count > 0)
        {
            StringWriter sw = new StringWriter();
            dtTable.WriteXml(sw);
            strXMLResult = sw.ToString();
            sw.Close();
            sw.Dispose();
        }

        return strXMLResult;
    }
    protected void grvAddmore_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowData();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];

            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);

                ViewState["CurrentTable"] = dt;
                grvAddmore.DataSource = dt;
                grvAddmore.DataBind();


            }
            SetPreviousData();
        }
    }
    private void SetRowData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TextBox txtAddress =
                      (TextBox)grvAddmore.Rows[rowIndex].Cells[0].FindControl("txtAddress");
                    TextBox txtCerification =
                     (TextBox)grvAddmore.Rows[rowIndex].Cells[1].FindControl("txtCerification");
                    TextBox txtOpeEstab =
                     (TextBox)grvAddmore.Rows[rowIndex].Cells[2].FindControl("txtOpeEstab");
                    TextBox txtNatureEsta =
                     (TextBox)grvAddmore.Rows[rowIndex].Cells[3].FindControl("txtNatureEsta");
                    TextBox txtContWrk =
                    (TextBox)grvAddmore.Rows[rowIndex].Cells[4].FindControl("txtContWrk");
                    TextBox txtMangName =
                    (TextBox)grvAddmore.Rows[rowIndex].Cells[5].FindControl("txtMangName");
                    TextBox txtMaxEmp =
                    (TextBox)grvAddmore.Rows[rowIndex].Cells[6].FindControl("txtMaxEmp");

                    txtAddress.Text = dt.Rows[i]["NameAddress"].ToString();

                    txtCerification.Text = dt.Rows[i]["Cerification"].ToString();
                    txtOpeEstab.Text = dt.Rows[i]["NameOfOperation"].ToString();
                    txtNatureEsta.Text = dt.Rows[i]["NatureOfEstablishment"].ToString();
                    txtContWrk.Text = dt.Rows[i]["ContractWork"].ToString();
                    txtMangName.Text = dt.Rows[i]["NameManager"].ToString();
                    txtMaxEmp.Text = dt.Rows[i]["MaximumNoOfEmp"].ToString();



                    rowIndex++;
                }
            }
        }
        //SetPreviousData();
    }
    public string PushData()
    {

        return "";
    }
}