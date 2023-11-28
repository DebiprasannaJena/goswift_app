using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
/// <summary>Summary description for Util</summary>
/// <remarks>Created by Dolagovinda Acharya on 25-May-2015</remarks>
/// <remarks>Edited by Suman Gupta on 3-Feb-2017</remarks>
using System.Web.UI.WebControls;

public class clsUtil
{
    string _Output = string.Empty;
    string str = "";
    string GetHindiLanguage = string.Empty;

    #region Check the Field value for special characters
    public static string CheckValidValue(string Fieldvalue)
    {
        string strlist = @"\,',`,~,^,#,$,*,(,),?,{,},[,],|,\\,//,/,!,<,>,%,;,=,--,_,&,@,\\',+,CR,LF";
        string[] arrlist = null;
        int istart = 0;
        arrlist = strlist.Split(',');
        for (istart = 0; istart < arrlist.Length; istart++)
        {

            if (Fieldvalue.IndexOf(arrlist[istart]) >= 0)
            {
                Fieldvalue = Fieldvalue.Replace(arrlist[istart], "");
            }
        }
        return Fieldvalue;
    }
    #endregion
           

      

    /// <summary>
    /// Created by Dolagovinda on 18th-nov-2013
    /// Valid DropDown
    /// </summary>
    /// <param name="ctl"></param>
    /// <param name="ctlname"></param>
    /// <returns></returns>
    public string Validatedropdown(int selectedValue, string ctlname)
    {
        try
        {
            if ((selectedValue == 0))
            {
                str = "Please select" + " " + ctlname;                
            }
            else
            {
                str = "PASS";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return str;
    }
    /// <summary>
    /// Created by Dolagovinda on 18th-nov-2013
    /// Check Blank Text Box for aplhabets mandatory
    /// </summary>
    /// <param name="ctl"></param>
    /// <param name="ctlname"></param>
    /// <param name="sz"></param>
    /// <returns></returns>
    public string ValidateTextbox_Mandatory_Alphabets(string txtValue, string ctlname, int sz, int minSize = 0)
    {
        try
        {
            if ((string.IsNullOrEmpty(txtValue)))
            {
                str = ctlname + " " + "can not be left blank";                
            }
            else if (txtValue.Substring(0, 1) == " ")
            {
                str = ctlname + " " + "does not allow White Space(s) in first place";                
            }
            else if (txtValue.Substring(0, 1) == "!" | txtValue.Substring(0, 1) == "@" | txtValue.Substring(0, 1) == "#" | txtValue.Substring(0, 1) == "$" | txtValue.Substring(0, 1) == "%" | txtValue.Substring(0, 1) == "^" | txtValue.Substring(0, 1) == "&" | txtValue.Substring(0, 1) == "*" | txtValue.Substring(0, 1) == "(" | txtValue.Substring(0, 1) == ")" | txtValue.Substring(0, 1) == "-" | txtValue.Substring(0, 1) == "_" | txtValue.Substring(0, 1) == "+" | txtValue.Substring(0, 1) == "=" | txtValue.Substring(0, 1) == "{" | txtValue.Substring(0, 1) == "}" | txtValue.Substring(0, 1) == "[" | txtValue.Substring(0, 1) == "]" | txtValue.Substring(0, 1) == "|" | txtValue.Substring(0, 1) == ";" | txtValue.Substring(0, 1) == ":" | txtValue.Substring(0, 1) == "<" | txtValue.Substring(0, 1) == ">" | txtValue.Substring(0, 1) == "?" | txtValue.Substring(0, 1) == "." | txtValue.Substring(0, 1) == "," | txtValue.Substring(0, 1) == "/" | txtValue.Substring(0, 1) == "\\" | txtValue.Substring(0, 1) == "~" | txtValue.Substring(0, 1) == "`" | txtValue.Substring(0, 1) == "\"" | txtValue.Substring(0, 1) == "\'" | txtValue.Substring(0, 1) == "&")
            {
                str = ctlname + " " + "does not allow Special Characters in first place";                
                
            }
            else if (txtValue.Substring(txtValue.Length - 1, 1) == " ")
            {
                str = ctlname + " " + "does not allow White Space(s) in last place";              
            }
            else if ((txtValue == "'"))
            {
                str = ctlname + " " + "does not allow Single Quote";              
            }

            else if ((txtValue.Contains('!')) | (txtValue.Contains('%')) | (txtValue.Contains('<')) | (txtValue.Contains('>')) | (txtValue.Contains('=')) | (txtValue.Contains("'")))
            {
                str = ctlname + " " + "does not allow Special characters";               
            }
            else if (txtValue.ToString().Contains('1') || txtValue.ToString().Contains('2') || txtValue.ToString().Contains('3') || txtValue.ToString().Contains('4') || txtValue.ToString().Contains('5') || txtValue.ToString().Contains('6') || txtValue.ToString().Contains('7') || txtValue.ToString().Contains('8') || txtValue.ToString().Contains('9') || txtValue.ToString().Contains('0'))
            {
                str = ctlname + " " + "does not allow Numbers";               
            }
            else if ((minSize > 0) && txtValue.Length < minSize)
            {
                int cnt = 0;
                cnt = txtValue.Length;
                if ((cnt < minSize))
                {
                    str = ctlname + " " + "allows minimum" + " " + minSize.ToString() + " " + "character(s)";                    
                }
            }
            else if ((sz > 0) && txtValue.Length > sz)
            {
                int cnt = 0;
                cnt = txtValue.Length;
                if ((cnt > sz))
                {                   
                    str = ctlname + " " + "allows Maximum" + " " + sz.ToString() + " " + "character(s)";                    
                }
            }

            else
            {
                str = "PASS";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return str;
    }
    /// <summary>
    /// Created by Dolagovinda on 18th-nov-2013
    /// Check Blank Text Box aplhabets not mandatory
    /// </summary>
    /// <param name="ctl"></param>
    /// <param name="ctlname"></param>
    /// <param name="sz"></param>
    /// <returns></returns>
    public string ValidateTextbox_NonMandatory_Alphabets(string txtValue, string ctlname, int sz, int minSize = 0)
    {
        try
        {
            if (txtValue != "")
            {
                if (txtValue.Substring(0, 1) == " ")
                {
                    str = ctlname + " " + "does not allow White Space(s) in first place";                   
                }
                else if (txtValue.Substring(0, 1) == "!" | txtValue.Substring(0, 1) == "@" | txtValue.Substring(0, 1) == "#" | txtValue.Substring(0, 1) == "$" | txtValue.Substring(0, 1) == "%" | txtValue.Substring(0, 1) == "^" | txtValue.Substring(0, 1) == "&" | txtValue.Substring(0, 1) == "*" | txtValue.Substring(0, 1) == "(" | txtValue.Substring(0, 1) == ")" | txtValue.Substring(0, 1) == "-" | txtValue.Substring(0, 1) == "_" | txtValue.Substring(0, 1) == "+" | txtValue.Substring(0, 1) == "=" | txtValue.Substring(0, 1) == "{" | txtValue.Substring(0, 1) == "}" | txtValue.Substring(0, 1) == "[" | txtValue.Substring(0, 1) == "]" | txtValue.Substring(0, 1) == "|" | txtValue.Substring(0, 1) == ";" | txtValue.Substring(0, 1) == ":" | txtValue.Substring(0, 1) == "<" | txtValue.Substring(0, 1) == ">" | txtValue.Substring(0, 1) == "?" | txtValue.Substring(0, 1) == "." | txtValue.Substring(0, 1) == "," | txtValue.Substring(0, 1) == "/" | txtValue.Substring(0, 1) == "\\" | txtValue.Substring(0, 1) == "~" | txtValue.Substring(0, 1) == "`" | txtValue.Substring(0, 1) == "\"" | txtValue.Substring(0, 1) == "\'" | txtValue.Substring(0, 1) == "&")
                {
                    str = ctlname + " " + "does not allow Special Characters in first place";                   
                }
                else if (txtValue.Substring(txtValue.Length - 1, 1) == " ")
                {
                    str = ctlname + " " + "does not allow White Space(s) in last place";                    
                }
                else if ((txtValue == "'"))
                {
                    str = ctlname + " " + "does not allow Single Quote";                 
                }

                else if ((txtValue.Contains('!')) | (txtValue.Contains('%')) | (txtValue.Contains('<')) | (txtValue.Contains('>')) | (txtValue.Contains('=')) | (txtValue.Contains("'")))
                {
                    str = ctlname + " " + "does not allow Special characters";                    
                }
                else if (txtValue.ToString().Contains('1') || txtValue.ToString().Contains('2') || txtValue.ToString().Contains('3') || txtValue.ToString().Contains('4') || txtValue.ToString().Contains('5') || txtValue.ToString().Contains('6') || txtValue.ToString().Contains('7') || txtValue.ToString().Contains('8') || txtValue.ToString().Contains('9') || txtValue.ToString().Contains('0'))
                {
                    str = ctlname + " " + "Numbers are not allowed";                   
                }
                else if ((minSize > 0) && txtValue.Length < minSize)
                {
                    int cnt = 0;
                    cnt = txtValue.Length;
                    if ((cnt < minSize))
                    {
                        str = ctlname + " " + "allows minimum" + " " + minSize.ToString() + " " + "character(s)";                        
                    }
                }
                else if ((sz > 0) && txtValue.Length > sz)
                {
                    int cnt = 0;
                    cnt = txtValue.Length;
                    if ((cnt > sz))
                    {                        
                        str = "allows maximum" + " " + sz.ToString() + " " + "character(s)";                       
                    }
                }
                else
                {
                    str = "PASS";
                }
            }
            else
            {
                str = "PASS";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return str;
    }
    /// <summary>
    /// Created by Dolagovinda on 18th-nov-2013
    /// Check Blank Text Box for numbers mandatory
    /// </summary>
    /// <param name="ctl"></param>
    /// <param name="ctlname"></param>
    /// <param name="sz"></param>
    /// <returns></returns>
    public string ValidateTextbox_Mandatory_Numbers(string txtValue, string ctlname, int sz, int minSize = 0)
    {
        try
        {
            int n;

            bool isNumeric = int.TryParse(txtValue, out n);


            if ((string.IsNullOrEmpty(txtValue)))
            {
                str = ctlname + " " + "can not be left blank";                
            }
            else if (txtValue.Substring(0, 1) == " ")
            {
                str = ctlname + " " + "White Space(s) not allowed in first place";
                
            }
            else if (txtValue.Substring(0, 1) == "!" | txtValue.Substring(0, 1) == "@" | txtValue.Substring(0, 1) == "#" | txtValue.Substring(0, 1) == "$" | txtValue.Substring(0, 1) == "%" | txtValue.Substring(0, 1) == "^" | txtValue.Substring(0, 1) == "&" | txtValue.Substring(0, 1) == "*" | txtValue.Substring(0, 1) == "(" | txtValue.Substring(0, 1) == ")" | txtValue.Substring(0, 1) == "-" | txtValue.Substring(0, 1) == "_" | txtValue.Substring(0, 1) == "+" | txtValue.Substring(0, 1) == "=" | txtValue.Substring(0, 1) == "{" | txtValue.Substring(0, 1) == "}" | txtValue.Substring(0, 1) == "[" | txtValue.Substring(0, 1) == "]" | txtValue.Substring(0, 1) == "|" | txtValue.Substring(0, 1) == ";" | txtValue.Substring(0, 1) == ":" | txtValue.Substring(0, 1) == "<" | txtValue.Substring(0, 1) == ">" | txtValue.Substring(0, 1) == "?" | txtValue.Substring(0, 1) == "." | txtValue.Substring(0, 1) == "," | txtValue.Substring(0, 1) == "/" | txtValue.Substring(0, 1) == "\\" | txtValue.Substring(0, 1) == "~" | txtValue.Substring(0, 1) == "`" | txtValue.Substring(0, 1) == "\"" | txtValue.Substring(0, 1) == "\'" | txtValue.Substring(0, 1) == "&")
            {
                str = ctlname + " " + "does not allow Special Characters in first place";               
            }
            else if (txtValue.Substring(txtValue.Length - 1, 1) == " ")
            {
                str = ctlname + " " + "does not allow White Space(s) in last place";                
            }
            else if ((txtValue == "'"))
            {
                str = ctlname + " " + "does not allow Single Quote";
               
            }
            else if ((txtValue == "0"))
            {
                str = ctlname + " " + " value cannot be zero";

            }

            else if ((txtValue.Contains('\'')) | (txtValue.Contains('!')) | (txtValue.Contains('$')) | (txtValue.Contains('%')) | (txtValue.Contains('^')) | (txtValue.Contains('*')) | (txtValue.Contains('<')) | (txtValue.Contains('>')) | (txtValue.Contains('~')) | (txtValue.Contains('+')) | (txtValue.Contains('=')) | (txtValue.Contains('{')) | (txtValue.Contains('}')) | (txtValue.Contains('[')) | (txtValue.Contains(']')) | (txtValue.Contains('|')) | (txtValue.Contains(';')) | (txtValue.Contains('`')) | (txtValue.Contains('-')) | (txtValue.Contains('_')) | (txtValue.Contains('"')) | (txtValue.Contains('\\')) | (txtValue.Contains('/')) | (txtValue.Contains('&')))
            {
                str = ctlname + " " + "does not allow Special characters";               
            }
            else if ((minSize > 0) && txtValue.Length < minSize)
            {
                int cnt = 0;
                cnt = txtValue.Length;
                if ((cnt < minSize))
                {
                    str = ctlname + " " + "allows minimum" + " " + minSize.ToString() + " " + "character(s)";                    
                }
            }
            else if ((sz > 0) && txtValue.Length > sz)
            {
                int cnt = 0;
                cnt = txtValue.Length;
                if ((cnt > sz))
                {                    
                    str = ctlname + " " + "allows Maximum" + " " + sz.ToString() + " " + "character(s)";
                   
                }
            }


            else if (txtValue.Contains('.'))
            {
                decimal value;
                if (!Decimal.TryParse(txtValue, out value))
                {
                    str = ctlname + " " + "is not a numeric value";
                    
                }
                else
                {
                    str = "PASS";
                }
            }
            else if (isNumeric == false)
            {
                str = ctlname + " " + "is not a numeric value";
                

            }

            else
            {
                str = "PASS";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return str;
    }

    /// <summary>
    /// Created by Dolagovinda on 18th-nov-2013
    /// Check Blank Text Box numbers not mandatory
    /// </summary>
    /// <param name="ctl"></param>
    /// <param name="ctlname"></param>
    /// <param name="sz"></param>
    /// <returns></returns>
    public string ValidateTextbox_NonMandatory_Numbers(string txtValue, string ctlname, int sz, int minSize = 0)
    {
        try
        {
            if (txtValue != "")
            {
                int n;
                bool isNumeric = int.TryParse(txtValue, out n);

                if (txtValue.Substring(0, 1) == " ")
                {
                    str = ctlname + " " + "White Space(s) not allowed in first place";
                    
                }
                else if (txtValue.Substring(0, 1) == "!" | txtValue.Substring(0, 1) == "@" | txtValue.Substring(0, 1) == "#" | txtValue.Substring(0, 1) == "$" | txtValue.Substring(0, 1) == "%" | txtValue.Substring(0, 1) == "^" | txtValue.Substring(0, 1) == "&" | txtValue.Substring(0, 1) == "*" | txtValue.Substring(0, 1) == "(" | txtValue.Substring(0, 1) == ")" | txtValue.Substring(0, 1) == "-" | txtValue.Substring(0, 1) == "_" | txtValue.Substring(0, 1) == "+" | txtValue.Substring(0, 1) == "=" | txtValue.Substring(0, 1) == "{" | txtValue.Substring(0, 1) == "}" | txtValue.Substring(0, 1) == "[" | txtValue.Substring(0, 1) == "]" | txtValue.Substring(0, 1) == "|" | txtValue.Substring(0, 1) == ";" | txtValue.Substring(0, 1) == ":" | txtValue.Substring(0, 1) == "<" | txtValue.Substring(0, 1) == ">" | txtValue.Substring(0, 1) == "?" | txtValue.Substring(0, 1) == "." | txtValue.Substring(0, 1) == "," | txtValue.Substring(0, 1) == "/" | txtValue.Substring(0, 1) == "\\" | txtValue.Substring(0, 1) == "~" | txtValue.Substring(0, 1) == "`" | txtValue.Substring(0, 1) == "\"" | txtValue.Substring(0, 1) == "\'" | txtValue.Substring(0, 1) == "&")
                {
                    str = ctlname + " " + "does not allow Special Characters in first place";
                    //ctl.Clear();
                    
                }
                else if (txtValue.Substring(txtValue.Length - 1, 1) == " ")
                {
                    str = ctlname + " " + "does not allow White Space(s) in last place";
                    
                }
                else if ((txtValue == "'"))
                {
                    str = ctlname + " " + "does not allow Single Quote";
                    // ctl.Clear();
                    
                }

                else if ((txtValue.Contains('\'')) | (txtValue.Contains('!')) | (txtValue.Contains('$')) | (txtValue.Contains('%')) | (txtValue.Contains('^')) | (txtValue.Contains('*')) | (txtValue.Contains('<')) | (txtValue.Contains('>')) | (txtValue.Contains('~')) | (txtValue.Contains('+')) | (txtValue.Contains('=')) | (txtValue.Contains('{')) | (txtValue.Contains('}')) | (txtValue.Contains('[')) | (txtValue.Contains(']')) | (txtValue.Contains('|')) | (txtValue.Contains(';')) | (txtValue.Contains('`')) | (txtValue.Contains('-')) | (txtValue.Contains('_')) | (txtValue.Contains('"')) | (txtValue.Contains('\\')) | (txtValue.Contains('/')) | (txtValue.Contains('&')))
                {
                    str = ctlname + " " + "does not allow Special characters";
                    //ctl.Clear();
                    
                }
                else if ((minSize > 0) && txtValue.Length < minSize)
                {
                    int cnt = 0;
                    cnt = txtValue.Length;
                    if ((cnt < minSize))
                    {
                        str = ctlname + " " + "allows minimum" + " " + minSize.ToString() + " " + "character(s)";
                        
                    }
                }
                else if ((sz > 0) && txtValue.Length > sz)
                {
                    int cnt = 0;
                    cnt = txtValue.Length;
                    if ((cnt > sz))
                    {                        
                        str = ctlname + " " + "allows maximum" + " " + sz.ToString() + " " + "character(s)";
                        
                    }
                }

                else if (txtValue.Contains('.'))
                {
                    decimal value;
                    if (!Decimal.TryParse(txtValue, out value))
                    {
                        str = ctlname + " " + "is not a numeric value";
                        
                    }

                }
                else if (isNumeric == false)
                {
                    str = ctlname + " " + "is not a numeric value";
                    

                }
                else
                {
                    str = "PASS";
                }
            }
            else
            {
                str = "PASS";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return str;
    }

    /// <summary>
    /// Validate Email Text Box
    /// </summary>
    /// <param name="ctl"></param>
    /// <param name="ctlname"></param>
    /// <param name="sz"></param>
    /// <returns></returns>
    public string ValidateEmailTextBox(string txtValue, string ctlname, int sz)
    {
        try
        {
            if (txtValue != "")
            {
                if (txtValue.Substring(0, 1) == " ")
                {
                    str = "White Space(s) not allowed in first place";
                    
                }
                else if (txtValue.Substring(0, 1) == "!" | txtValue.Substring(0, 1) == "@" | txtValue.Substring(0, 1) == "#" | txtValue.Substring(0, 1) == "$" | txtValue.Substring(0, 1) == "%" | txtValue.Substring(0, 1) == "^" | txtValue.Substring(0, 1) == "&" | txtValue.Substring(0, 1) == "*" | txtValue.Substring(0, 1) == "(" | txtValue.Substring(0, 1) == ")" | txtValue.Substring(0, 1) == "-" | txtValue.Substring(0, 1) == "_" | txtValue.Substring(0, 1) == "+_" | txtValue.Substring(0, 1) == "=" | txtValue.Substring(0, 1) == "{" | txtValue.Substring(0, 1) == "}" | txtValue.Substring(0, 1) == "[" | txtValue.Substring(0, 1) == "]" | txtValue.Substring(0, 1) == "|" | txtValue.Substring(0, 1) == ";" | txtValue.Substring(0, 1) == ":" | txtValue.Substring(0, 1) == "<" | txtValue.Substring(0, 1) == ">" | txtValue.Substring(0, 1) == "?" | txtValue.Substring(0, 1) == "." | txtValue.Substring(0, 1) == ",")
                {
                    str = ctlname + " " + "Special characters not allowed in first place";
                    
                }
                else if (txtValue.Substring(txtValue.Length - 1, 1) == " ")
                {
                    str = ctlname + " " + "White Space(s) not allowed in last place";
                    
                }
                else if ((txtValue == "'"))
                {
                    str = ctlname + " " + "Single Quote not allowed";
                    
                }

                else if ((txtValue.Contains('\'')) | (txtValue.Contains('!')) | (txtValue.Contains('%')) | (txtValue.Contains('^')) | (txtValue.Contains('&')) | (txtValue.Contains('*')) | (txtValue.Contains('(')) | (txtValue.Contains(')')) | (txtValue.Contains('<')) | (txtValue.Contains('>')) | (txtValue.Contains('~')) | (txtValue.Contains(')')) | (txtValue.Contains('-')) | (txtValue.Contains('+')) | (txtValue.Contains('=')) | (txtValue.Contains('{')) | (txtValue.Contains('}')) | (txtValue.Contains(']')) | (txtValue.Contains('[')) | (txtValue.Contains('|')) | (txtValue.Contains(';')) | (txtValue.Contains(':')) | (txtValue.Contains('?')) | (txtValue.Contains(',')) | (txtValue.Contains('/')) | (txtValue.Contains('\\')) | (txtValue.Contains('"')) | (txtValue.Contains('`')) | (txtValue.Contains('^')) | (txtValue.Contains('~')) | (txtValue.Contains('&')))
                {
                    str = ctlname + " " + "Special character not allowed";
                    
                }
                else if (txtValue.Contains("@") == false)
                {
                    str = ctlname + " " + "is not valid";
                    
                }
                else if ((sz > 0) && txtValue.Length > sz)
                {
                    int cnt = 0;
                    cnt = txtValue.Length;
                    if ((cnt > sz))
                    {                        
                        str = ctlname + "Maximum" + " " + sz + " " + "character(s) allowed";
                        
                    }
                }
                else
                {
                    str = "PASS";
                }
            }
            else
            {
                str = "PASS";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return str;
    }

    /// <summary>
    /// Validate Mobile
    /// </summary>
    /// <param name="ctl"></param>
    /// <param name="ctlname"></param>
    /// <param name="sz"></param>
    /// <returns></returns>
    public string ValidateMobile(string txtValue, string ctlname, string strMandate)
    {
        try
        {
            if (strMandate.ToUpper() == "YES")
            {
                if ((string.IsNullOrEmpty(txtValue)))
                {
                    str = ctlname + " " + "can not be left blank";
                    
                }
                else if ((txtValue.Length < 10))
                {

                    str = "Mobile Number should not be less than 10 digits";
                    

                }
                else
                {
                    str = "PASS";
                }
            }
            else
            {
                if (txtValue != "")
                {
                    if ((txtValue.Length < 10))
                    {

                        str = "Mobile Number should not be less than 10 digits";
                        

                    }
                    else
                    {
                        str = "PASS";
                    }
                }
                else
                {
                    str = "PASS";
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return str;
    }
  

    /// <summary>
    /// Created By Dolagovinda Acharya on 8th Jun 2015
    /// </summary>
    /// <param name="HeaderCell"></param>
    /// <param name="strText"></param>
    /// <param name="intColSpan"></param>
    /// <param name="intRowSpan"></param>
    /// <param name="hAlign"></param>
    /// <param name="ColorCode"></param>
    public string ValidDateCurrentdate(string txtdateto, string ctlname)
    {
        try
        {
            if (string.IsNullOrEmpty(txtdateto))
            {
                str = ctlname + " " + "can not be left blank";                ;
            }

            else if (Convert.ToDateTime(txtdateto) > Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()))
            {
                str = ctlname + " " + "can not be greater than current date";                
            }
            else
            {
                str = "PASS";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return str;
    }
    /// <summary>
    /// Created By Dolagovinda Acharya on 8th Jun 2015
    /// </summary>
    /// <param name="HeaderCell"></param>
    /// <param name="strText"></param>
    /// <param name="intColSpan"></param>
    /// <param name="intRowSpan"></param>
    /// <param name="hAlign"></param>
    /// <param name="ColorCode"></param>
    public string CurrentDateLessValidator(string txtdateto, string ctlname)
    {
        try
        {
            if (string.IsNullOrEmpty(txtdateto))
            {
                str = ctlname + " " + "can not be left blank";               
            }

            else if (Convert.ToDateTime(txtdateto) < Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()))
            {
                str = ctlname + " " + "can not be before Current Date";               
            }
            else
            {
                str = "PASS";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return str;
    }
    /// <summary>
    /// Created by Gayatri Prasad Das on 8-Juy-2015
    /// Check Blank Text Box for aplhabets and Numeric which is mandatory
    /// </summary>
    /// <param name="ctl"></param>
    /// <param name="ctlname"></param>
    /// <param name="sz"></param>
    /// <returns></returns>
    public string ValidateTextbox_Mandatory_AlphaNumeric(string txtValue, string ctlname, int sz, int minSize = 0)
    {
        try
        {
            if ((string.IsNullOrEmpty(txtValue)))
            {
                str = ctlname + " " + "can not be left blank";
                
            }
            else if (txtValue.Substring(0, 1) == " ")
            {
                str = ctlname + " " + "does not allow White Space(s) in first place";
                
            }
            else if (txtValue.Substring(0, 1) == "!" | txtValue.Substring(0, 1) == "@" | txtValue.Substring(0, 1) == "#" | txtValue.Substring(0, 1) == "$" | txtValue.Substring(0, 1) == "%" | txtValue.Substring(0, 1) == "^" | txtValue.Substring(0, 1) == "&" | txtValue.Substring(0, 1) == "*" | txtValue.Substring(0, 1) == "(" | txtValue.Substring(0, 1) == ")" | txtValue.Substring(0, 1) == "-" | txtValue.Substring(0, 1) == "_" | txtValue.Substring(0, 1) == "+" | txtValue.Substring(0, 1) == "=" | txtValue.Substring(0, 1) == "{" | txtValue.Substring(0, 1) == "}" | txtValue.Substring(0, 1) == "[" | txtValue.Substring(0, 1) == "]" | txtValue.Substring(0, 1) == "|" | txtValue.Substring(0, 1) == ";" | txtValue.Substring(0, 1) == ":" | txtValue.Substring(0, 1) == "<" | txtValue.Substring(0, 1) == ">" | txtValue.Substring(0, 1) == "?" | txtValue.Substring(0, 1) == "." | txtValue.Substring(0, 1) == "," | txtValue.Substring(0, 1) == "/" | txtValue.Substring(0, 1) == "\\" | txtValue.Substring(0, 1) == "~" | txtValue.Substring(0, 1) == "`" | txtValue.Substring(0, 1) == "\"" | txtValue.Substring(0, 1) == "\'" | txtValue.Substring(0, 1) == "&")
            {
                str = ctlname + " " + "does not allow Special Characters in first place";
                //ctl.Clear();
                
            }
            else if (txtValue.Substring(txtValue.Length - 1, 1) == " ")
            {
                str = ctlname + " " + "does not allow White Space(s) in last place";
                
            }
            else if ((txtValue == "'"))
            {
                str = ctlname + " " + "does not allow Single Quote";
                // ctl.Clear();
                
            }

            else if ((txtValue.Contains('!')) | (txtValue.Contains('%')) | (txtValue.Contains('<')) | (txtValue.Contains('>')) | (txtValue.Contains('=')) | (txtValue.Contains("'")))
            {
                str = ctlname + " " + "does not allow Special character";
                //ctl.Clear();
                
            }
            else if ((minSize > 0) && txtValue.Length < minSize)
            {
                int cnt = 0;
                cnt = txtValue.Length;
                if ((cnt < minSize))
                {
                    str = ctlname + " " + "allows minimum" + " " + minSize.ToString() + " " + "character(s)";
                    
                }
            }
            else if ((sz > 0) && txtValue.Length > sz)
            {
                int cnt = 0;
                cnt = txtValue.Length;
                if ((cnt > sz))
                {                    
                    str = ctlname + " " + "allows Maximum" + " " + sz.ToString() + " " + "character(s)";
                    
                }
            }
            else
            {
                str = "PASS";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return str;
    }
    /// <summary>
    /// Created by Gayatri Prasad Das on 8-Juy-2015
    /// Check Blank Text Box for aplhabets and Numeric which is not mandatory
    /// </summary>
    /// <param name="ctl"></param>
    /// <param name="ctlname"></param>
    /// <param name="sz"></param>
    /// <returns></returns>
    public string ValidateTextbox_NonMandatory_AlphaNumeric(string txtValue, string ctlname, int sz, int minSize = 0)
    {
        try
        {
            if (txtValue != "")
            {
                if (txtValue.Substring(0, 1) == " ")
                {
                    str = ctlname + " " + "does not allow White Space(s) in first place";
                    
                }
                else if (txtValue.Substring(0, 1) == "!" | txtValue.Substring(0, 1) == "@" | txtValue.Substring(0, 1) == "#" | txtValue.Substring(0, 1) == "$" | txtValue.Substring(0, 1) == "%" | txtValue.Substring(0, 1) == "^" | txtValue.Substring(0, 1) == "&" | txtValue.Substring(0, 1) == "*" | txtValue.Substring(0, 1) == "(" | txtValue.Substring(0, 1) == ")" | txtValue.Substring(0, 1) == "-" | txtValue.Substring(0, 1) == "_" | txtValue.Substring(0, 1) == "+" | txtValue.Substring(0, 1) == "=" | txtValue.Substring(0, 1) == "{" | txtValue.Substring(0, 1) == "}" | txtValue.Substring(0, 1) == "[" | txtValue.Substring(0, 1) == "]" | txtValue.Substring(0, 1) == "|" | txtValue.Substring(0, 1) == ";" | txtValue.Substring(0, 1) == ":" | txtValue.Substring(0, 1) == "<" | txtValue.Substring(0, 1) == ">" | txtValue.Substring(0, 1) == "?" | txtValue.Substring(0, 1) == "." | txtValue.Substring(0, 1) == "," | txtValue.Substring(0, 1) == "/" | txtValue.Substring(0, 1) == "\\" | txtValue.Substring(0, 1) == "~" | txtValue.Substring(0, 1) == "`" | txtValue.Substring(0, 1) == "\"" | txtValue.Substring(0, 1) == "\'" | txtValue.Substring(0, 1) == "&")
                {
                    str = ctlname + " " + "does not allow Special Characters in first place";
                    //ctl.Clear();
                    
                }
                else if (txtValue.Substring(txtValue.Length - 1, 1) == " ")
                {
                    str = ctlname + " " + "does not allow White Space(s) in last place";
                    
                }
                else if ((txtValue == "'"))
                {
                    str = ctlname + " " + "does not allow Single Quote ";
                    // ctl.Clear();
                    
                }

                else if ((txtValue.Contains('!')) | (txtValue.Contains('%')) | (txtValue.Contains('<')) | (txtValue.Contains('>')) | (txtValue.Contains('=')) | (txtValue.Contains("'")))
                {
                    str = ctlname + " " + "does not allow Special character ";
                    //ctl.Clear();
                    
                }
                else if ((minSize > 0) && txtValue.Length < minSize)
                {
                    int cnt = 0;
                    cnt = txtValue.Length;
                    if ((cnt < minSize))
                    {
                        str = ctlname + " " + "allows minimum" + " " + minSize.ToString() + " " + "character(s)";
                        
                    }
                }
                else if ((sz > 0) && txtValue.Length > sz)
                {
                    int cnt = 0;
                    cnt = txtValue.Length;
                    if ((cnt > sz))
                    {
                        
                        str = ctlname + " " + "allows Maximum" + " " + sz.ToString() + " " + "character(s)";
                        
                    }
                }

                else
                {
                    str = "PASS";
                }
            }
            else
            {
                str = "PASS";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return str;
    }
   
    #region ""
    /// <summary>
    /// Created By Suman on 8th Jun 2017
    /// <param name="txtdateto">Text Box name</param>
    /// </summary>
    public string ValidDateCurrentMonthYear(string txtdateto, string ctlname)
    {
        try
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            if (string.IsNullOrEmpty(txtdateto))
            {
                str = ctlname + " " + "can not be left blank";                
            }

            else if ((Convert.ToDateTime(txtdateto.Trim()).Month != currentMonth) || (Convert.ToDateTime(txtdateto.Trim()).Year != currentYear))
            {
                str = "Previous month data can not be added";              
            }
            else
            {
                str = "PASS";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return str;
    }
    #endregion
    public static string ToShortGuid(Guid newGuid)
    {
        string modifiedBase64 = Convert.ToBase64String(newGuid.ToByteArray())
            .Replace('+', '-').Replace('/', '_') // avoid invalid URL characters
            .Substring(0, 22);
        return modifiedBase64;
    }
   
}


