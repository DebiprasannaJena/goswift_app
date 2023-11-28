using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for NSWSAPIReponseCls
/// </summary>
public class NSWSAPIReponseCls
{
    public NSWSAPIReponseCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }   

    public class SubField
    {
        public string name { get; set; }
        public string inputValue { get; set; }
        public string fieldKey { get; set; }
    }

    public class Field
    {
        public string name { get; set; }
        public string inputValue { get; set; }
        public string fieldKey { get; set; }
        public string serialNumber { get; set; }
        public List<SubField> subFields { get; set; }
    }

    public class Section
    {
        public string name { get; set; }
        public List<Field> fields { get; set; }
        public string sectionKey { get; set; }
        public string serialNumber { get; set; }
    }

    public class Data
    {
        public string investorSWSId { get; set; }
        public long dateOfInitiation { get; set; }
        public List<Section> sections { get; set; }
    }

    public class NSWSCAFResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}