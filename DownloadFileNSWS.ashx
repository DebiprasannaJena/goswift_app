<%@ WebHandler Language="C#" Class="DownloadFileNSWS" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class DownloadFileNSWS : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }

        try
        {
            string strAppNo = context.Request.QueryString["AppNo"];

            if (strAppNo != "" && strAppNo != null)
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_NSWS_FETCH_DATA";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_VCH_ACTION", "C");
                cmd.Parameters.AddWithValue("@P_VCH_APP_NO", strAppNo);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();

                if (dt.Rows.Count > 0)
                {
                    string strCertFileName = Convert.ToString(dt.Rows[0]["VCH_CERTIFICATE_FILENAME"]);
                    if (strCertFileName != "")
                    {
                        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("Portal/ApprovalDocs/" + strCertFileName)))
                        {
                            response.ContentType = "text/plain";
                            response.AddHeader("Content-Disposition", "attachment; filename=" + strCertFileName + ";");
                            response.TransmitFile(HttpContext.Current.Server.MapPath("Portal/ApprovalDocs/" + strCertFileName));
                        }
                        else
                        {
                            context.Response.Clear();
                            context.Response.Write("No file found for download.Please contact to administrator.");
                        }
                    }
                    else
                    {
                        context.Response.Clear();
                        context.Response.Write("No file found for download.");
                    }
                }
                else
                {
                    context.Response.Clear();
                    context.Response.Write("No file found for download.");
                }

                response.Flush();
                response.End();
            }
            else
            {
                context.Response.Clear();
                context.Response.Write("Invalid File Path.");
            }
        }
        catch (System.Threading.ThreadAbortException)
        {
            // ignore it
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "NSWSApprovalDoc");
        }
        finally
        {
            cmd = null;
            conn.Close();
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}