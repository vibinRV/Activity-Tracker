using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activity_Tracker.Models
{
    public class TeamLead
    {
        public int sno {  get; set; }
        public int project_no {  get; set; }
        public string project_name {  get; set; }
        public DateTime project_start_date {  get; set; }
        public DateTime project_end_date { get; set; }
        public int person_no {  get; set; }
        public string person_name { get; set; }
        public string person_role { get; set; }
        public string project_lead { get; set;}

    }
}