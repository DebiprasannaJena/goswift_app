using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AvailedIncentiveEarlier
/// </summary>
public class AvailedIncentiveEarlier
 {
    public int AVAILEarlierID { get; set; }
    public int INCUNQUEID { get; set; }
    public List<AvailedEarlierDetails> ListAvailedEarlierDetails { get; set; }
 }
 public class AvailedEarlierDetails
 {
     public int AvailedEarlierDetailsId { get; set; }
     public int IncentiveTypeID { get; set; }
     public decimal QuantumValue { get; set; }
     public DateTime AvailedEarlierFrom { get; set; }
     public DateTime AvailedEarlierTo { get; set; }
     public int ApplicabilityId { get; set; }
 }