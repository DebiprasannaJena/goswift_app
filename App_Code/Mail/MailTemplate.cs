using System.Collections;
namespace MailEngine
{
    /// <summary>
    /// Holds the parameter for Mail Template
    /// </summary>
    /// <createdby>Manas Bej</createdby>
    /// <createdon>03-Feb-2015</createdon>
    public class MailTemplate
    {
        /// <summary>
        /// Template Path
        /// </summary>
        public string TemplatePath { get; set; }
        /// <summary>
        /// Variable collections for the template
        /// </summary>
        public Hashtable Verbs { get; set; }
    } 
}