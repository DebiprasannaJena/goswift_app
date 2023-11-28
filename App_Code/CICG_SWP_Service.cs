using System;
using System.Linq;
using System.Web.Services;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Xml.Serialization;
using System.IO;

/// <summary>
/// Summary description for CICGService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class CICG_SWP_Service : System.Web.Services.WebService {

    public CICG_SWP_Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
    SqlCommand obj_cmd = new SqlCommand();

      [WebMethod]
      public List<EmployeeDirectory> GetEmployeeName(string SearchText)
      {          
          List<EmployeeDirectory> objList = new List<EmployeeDirectory>();
          try
          {

              DataTable dt = AMServices.ViewEmployeeDtls("FE",SearchText);
              if (dt.Rows.Count > 0)
              {
                  objList = (from DataRow row in dt.Rows

                             select new EmployeeDirectory()
                             {
                                 Id = Convert.ToInt32(row["INTUSERID"]),
                                 Name = row["FULLNAME"].ToString()                             

                             }).ToList();

              }
          }
          catch (Exception ex)
          {
              throw ex;
          }

          return objList;

      }

      [WebMethod]
      public string AddAgenda(string Proposal, string ProjLoc, string ProdCapcity,
                             string BrdOfDirectors, string BIOfCompny,
                             string ProjCostDtls, string RawMatSource,
                             string FinDetails, string FinPerformance, string FinancialDoc, string UId)
      {
          string strOut = "0";
          try
          {
              con.Open();
              obj_cmd = new SqlCommand("USP_SWP_AddAgenda", con);
              obj_cmd.CommandType = CommandType.StoredProcedure;
              obj_cmd.Parameters.AddWithValue("@PCH_ACTION", "I");
              obj_cmd.Parameters.AddWithValue("@P_XMLProposal", Proposal);
              obj_cmd.Parameters.AddWithValue("@PVCH_UID", UId);
              obj_cmd.Parameters.AddWithValue("@P_XMLProjLocation", ProjLoc);
              obj_cmd.Parameters.AddWithValue("@P_XMLProductCapacity", ProdCapcity);
              obj_cmd.Parameters.AddWithValue("@P_XMLBoardOfDirectors", BrdOfDirectors);
              obj_cmd.Parameters.AddWithValue("@P_XMLBusinessIntrestsOfCompny", BIOfCompny);
              obj_cmd.Parameters.AddWithValue("@P_XMLProjectCostDtls", ProjCostDtls);
              obj_cmd.Parameters.AddWithValue("@P_XMLRawMaterialsSource", RawMatSource);
              obj_cmd.Parameters.AddWithValue("@P_XMLFinancingDetails", FinDetails);
              obj_cmd.Parameters.AddWithValue("@P_XMLFinancialPerformance", FinPerformance);
              obj_cmd.Parameters.AddWithValue("@P_XMLFinancialDoc", FinancialDoc);
              obj_cmd.Parameters.AddWithValue("@vchMsgOut", "out");
              obj_cmd.Parameters["@vchMsgOut"].Direction = ParameterDirection.Output;
              obj_cmd.Parameters["@vchMsgOut"].DbType = System.Data.DbType.String;
              obj_cmd.Parameters["@vchMsgOut"].Size = 100;
              obj_cmd.ExecuteNonQuery();
              strOut = Convert.ToString(obj_cmd.Parameters["@vchMsgOut"].Value);
          }
          catch (Exception ex)
          {
              throw ex;
          }
          return strOut;
      }
      public class Proposal
      {
          public string ProjectTitle { get; set; }
          public string CompanyName { get; set; }
          public int sector { get; set; }
          public string DateofApplication { get; set; }
          public string Type { get; set; }
          public string EnvironmentCategory { get; set; }
          public string BriefProposal { get; set; }
          public string Land { get; set; }
          public string Water { get; set; }
          public string Power { get; set; }
          public int Source { get; set; }
          public int DirectEmployment { get; set; }
          public int ContractualEmployment { get; set; }
          public string ImplementationPeriod { get; set; }
          public string FinancingDescription { get; set; }
          public int NodId { get; set; }
      }
      public class ProjLocation
      {
          public int District { get; set; }
          public string Location { get; set; }
      }
      public class ProductCapacity
      {
          public string Product { get; set; }
          public string Capacity { get; set; }
      }
      public class BoardOfDirectors
      {
          public string DirectorsName { get; set; }  
      }
      public class BusinessIntrestsOfCompny
      {
          public string BusinessIntrest { get; set; }
      }
      public class ProjectCostDtls
      {
          public int Description { get; set; }
          public string Cost { get; set; }
      }
      public class RawMaterialsSource
      {
          public int Description { get; set; }
          public string Cost { get; set; }
      }
      public class FinancingDetails 
      {
          public int Description { get; set; }
          public string Amount { get; set; }
          public string Percentage { get; set; }
      }
      public class FinancialPerformance
      {
          public string CompanyName { get; set; }
          public string Particulars { get; set; }
          public string TurnOver { get; set; }
          public string ProfitAfterTax { get; set; }
          public string NetWorth { get; set; }
          public string FinancialInfo { get; set; }
      }
      public class FinancialDoc
      {
          public string document_Name { get; set; }
          public string document_Link { get; set; }
      }
      public class EmployeeDirectory
      {
          public int Id { get; set; }
          public string Name { get; set; }
          public string DesgName { get; set; }
          public int DeptId { get; set; }
          public string MailId { get; set; }
          public string Mobileno { get; set; }
          public string Profilename { get; set; }
          public string DeptName { get; set; }
      }
     public static string SerializeToXMLString<T>(T toSerialize)
     {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, toSerialize);
            return textWriter.ToString();
     }

     [WebMethod]
     public List<EmployeeDirectory> FillNodalOfficer()
     {
         List<EmployeeDirectory> objList = new List<EmployeeDirectory>();
         try
         {
             Agenda objA = new Agenda();

             objA.Action = "E";
             objA.OfficerType = 1;
             DataTable dt = new DataTable();
             dt = AMServices.ViewOfficers(objA);
             
             if (dt.Rows.Count > 0)
             {
                 objList = (from DataRow row in dt.Rows

                            select new EmployeeDirectory()
                            {
                                Id = Convert.ToInt32(row["intUserId"]),
                                Name = row["Fullname"].ToString()

                            }).ToList();

             }
         }
         catch (Exception ex)
         {
             throw ex;
         }

         return objList;

     }



}
