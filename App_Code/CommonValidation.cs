using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for CommonValidation
/// </summary>
public class CommonValidation
{
    string _Output = string.Empty;
    string str = "";
    string GetHindiLanguage = string.Empty;
   
    public string ValidateTextbox_Mandatory_Alphabets(string txtValue, string ctlname)
    {
        try
        {
            if (txtValue.Substring(0, 1) == "!" | txtValue.Substring(0, 1) == "@" | txtValue.Substring(0, 1) == "#" | txtValue.Substring(0, 1) == "$" | txtValue.Substring(0, 1) == "%" | txtValue.Substring(0, 1) == "^" | txtValue.Substring(0, 1) == "&" | txtValue.Substring(0, 1) == "*" | txtValue.Substring(0, 1) == "(" | txtValue.Substring(0, 1) == ")" | txtValue.Substring(0, 1) == "-" | txtValue.Substring(0, 1) == "_" | txtValue.Substring(0, 1) == "+" | txtValue.Substring(0, 1) == "=" | txtValue.Substring(0, 1) == "{" | txtValue.Substring(0, 1) == "}" | txtValue.Substring(0, 1) == "[" | txtValue.Substring(0, 1) == "]" | txtValue.Substring(0, 1) == "|" | txtValue.Substring(0, 1) == ";" | txtValue.Substring(0, 1) == ":" | txtValue.Substring(0, 1) == "<" | txtValue.Substring(0, 1) == ">" | txtValue.Substring(0, 1) == "?" | txtValue.Substring(0, 1) == "." | txtValue.Substring(0, 1) == "," | txtValue.Substring(0, 1) == "/" | txtValue.Substring(0, 1) == "\\" | txtValue.Substring(0, 1) == "~" | txtValue.Substring(0, 1) == "`" | txtValue.Substring(0, 1) == "\"" | txtValue.Substring(0, 1) == "\'" | txtValue.Substring(0, 1) == "&")
            {
                str = ctlname + " " + "does not allow Special Characters in first place";

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
    public string ValidateTextbox_Mandatory_Numbers(string txtValue, string ctlname)
    {
        try
        {
            Int64 n;
            bool isNumeric = Int64.TryParse(txtValue, out n);
            if (txtValue.Substring(0, 1) == "!" | txtValue.Substring(0, 1) == "@" | txtValue.Substring(0, 1) == "#" | txtValue.Substring(0, 1) == "$" | txtValue.Substring(0, 1) == "%" | txtValue.Substring(0, 1) == "^" | txtValue.Substring(0, 1) == "&" | txtValue.Substring(0, 1) == "*" | txtValue.Substring(0, 1) == "(" | txtValue.Substring(0, 1) == ")" | txtValue.Substring(0, 1) == "-" | txtValue.Substring(0, 1) == "_" | txtValue.Substring(0, 1) == "+" | txtValue.Substring(0, 1) == "=" | txtValue.Substring(0, 1) == "{" | txtValue.Substring(0, 1) == "}" | txtValue.Substring(0, 1) == "[" | txtValue.Substring(0, 1) == "]" | txtValue.Substring(0, 1) == "|" | txtValue.Substring(0, 1) == ";" | txtValue.Substring(0, 1) == ":" | txtValue.Substring(0, 1) == "<" | txtValue.Substring(0, 1) == ">" | txtValue.Substring(0, 1) == "?" | txtValue.Substring(0, 1) == "." | txtValue.Substring(0, 1) == "," | txtValue.Substring(0, 1) == "/" | txtValue.Substring(0, 1) == "\\" | txtValue.Substring(0, 1) == "~" | txtValue.Substring(0, 1) == "`" | txtValue.Substring(0, 1) == "\"" | txtValue.Substring(0, 1) == "\'" | txtValue.Substring(0, 1) == "&")
            {
                str = ctlname + " " + "does not allow Special Characters in first place";
            }
            else if ((txtValue == "'"))
            {
                str = ctlname + " " + "does not allow Single Quote";

            }
            else if ((txtValue.Contains('\'')) | (txtValue.Contains('!')) | (txtValue.Contains('$')) | (txtValue.Contains('%')) | (txtValue.Contains('^')) | (txtValue.Contains('*')) | (txtValue.Contains('<')) | (txtValue.Contains('>')) | (txtValue.Contains('~')) | (txtValue.Contains('+')) | (txtValue.Contains('=')) | (txtValue.Contains('{')) | (txtValue.Contains('}')) | (txtValue.Contains('[')) | (txtValue.Contains(']')) | (txtValue.Contains('|')) | (txtValue.Contains(';')) | (txtValue.Contains('`')) | (txtValue.Contains('-')) | (txtValue.Contains('_')) | (txtValue.Contains('"')) | (txtValue.Contains('\\')) | (txtValue.Contains('/')) | (txtValue.Contains('&')))
            {
                str = ctlname + " " + "does not allow Special characters";
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
    public string ValidateEmailTextBox(string txtValue, string ctlname)
    {
        try
        {
            if (txtValue != "")
            {
                if (txtValue.Substring(0, 1) == "!" | txtValue.Substring(0, 1) == "@" | txtValue.Substring(0, 1) == "#" | txtValue.Substring(0, 1) == "$" | txtValue.Substring(0, 1) == "%" | txtValue.Substring(0, 1) == "^" | txtValue.Substring(0, 1) == "&" | txtValue.Substring(0, 1) == "*" | txtValue.Substring(0, 1) == "(" | txtValue.Substring(0, 1) == ")" | txtValue.Substring(0, 1) == "-" | txtValue.Substring(0, 1) == "_" | txtValue.Substring(0, 1) == "+_" | txtValue.Substring(0, 1) == "=" | txtValue.Substring(0, 1) == "{" | txtValue.Substring(0, 1) == "}" | txtValue.Substring(0, 1) == "[" | txtValue.Substring(0, 1) == "]" | txtValue.Substring(0, 1) == "|" | txtValue.Substring(0, 1) == ";" | txtValue.Substring(0, 1) == ":" | txtValue.Substring(0, 1) == "<" | txtValue.Substring(0, 1) == ">" | txtValue.Substring(0, 1) == "?" | txtValue.Substring(0, 1) == "." | txtValue.Substring(0, 1) == ",")
                {
                    str = ctlname + " " + "Special characters not allowed in first place";

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
    public string ValidateTextbox_Mandatory_Alphabets_Num(string txtValue, string ctlname)
    {
        try
        {
            if (txtValue.Substring(0, 1) == "!" | txtValue.Substring(0, 1) == "@" | txtValue.Substring(0, 1) == "#" | txtValue.Substring(0, 1) == "$" | txtValue.Substring(0, 1) == "%" | txtValue.Substring(0, 1) == "^" | txtValue.Substring(0, 1) == "&" | txtValue.Substring(0, 1) == "*" | txtValue.Substring(0, 1) == "(" | txtValue.Substring(0, 1) == ")" | txtValue.Substring(0, 1) == "-" | txtValue.Substring(0, 1) == "_" | txtValue.Substring(0, 1) == "+" | txtValue.Substring(0, 1) == "=" | txtValue.Substring(0, 1) == "{" | txtValue.Substring(0, 1) == "}" | txtValue.Substring(0, 1) == "[" | txtValue.Substring(0, 1) == "]" | txtValue.Substring(0, 1) == "|" | txtValue.Substring(0, 1) == ";" | txtValue.Substring(0, 1) == ":" | txtValue.Substring(0, 1) == "<" | txtValue.Substring(0, 1) == ">" | txtValue.Substring(0, 1) == "?" | txtValue.Substring(0, 1) == "." | txtValue.Substring(0, 1) == "," | txtValue.Substring(0, 1) == "/" | txtValue.Substring(0, 1) == "\\" | txtValue.Substring(0, 1) == "~" | txtValue.Substring(0, 1) == "`" | txtValue.Substring(0, 1) == "\"" | txtValue.Substring(0, 1) == "\'" | txtValue.Substring(0, 1) == "&")
            {
                str = ctlname + " " + "does not allow Special Characters in first place";

            }
            else if ((txtValue == "'"))
            {
                str = ctlname + " " + "does not allow Single Quote";
            }

            else if ((txtValue.Contains('!')) | (txtValue.Contains('%')) | (txtValue.Contains('<')) | (txtValue.Contains('>')) | (txtValue.Contains('=')) | (txtValue.Contains("'")))
            {
                str = ctlname + " " + "does not allow Special characters";
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
}