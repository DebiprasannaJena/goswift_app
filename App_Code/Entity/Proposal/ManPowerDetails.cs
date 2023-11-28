using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Proposal
{
   public  class ManPowerDetails
    {
        public string Action                    {get;set;}
     public int    intManPowerId             {get;set;}
     public string  vchProposalNo                  {get;set;}
     public int    intNoOfWorkersDirect         {get;set;}
     public int    intNoOfWorkersIndirect       {get;set;}
     public int    intNoOfStaffDirect           {get;set;}
     public int intNoOfStaffIndirect            {get;set;}
     public int    intTotalDirect               {get;set;}
     public int    intTotalIndirect             {get;set;}
     public int    intNoOfWorkingHours          {get;set;}
     public int    intPersonalResiding          {get;set;}
     public string vchPersonalResiding          {get;set;}
     public int  intManegrialExsiting			{get;set;}
     public int  intManegrialProposed           {get;set;}
     public int  intSupervisorExsiting          {get;set;}
     public int  intSupervisorProposed          {get;set;}
     public int  intSkilledExsiting             {get;set;}
     public int  intSkilledProposed             {get;set;}
     public int  intUnskilledExsiting           {get;set;}
     public int  intUnskilledProposed           {get;set;}
     public int  intOtherExsiting               {get;set;}
     public int  intOtherProposed               {get;set;}
     public int  intTotalEmployExsiting         {get;set;}
     public int  intTotalEmployProposed         {get;set;}
     public string  vchOperatingPeriod          {get;set;}
     public string  vchSessionalPeriod          {get;set;}
     public int   intDaysPerAnnum               {get;set;}
     public int   intCreatedBy                  {get;set;}
         
        
    }
}
