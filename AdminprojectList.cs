
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activity_Tracker.Models
{
    public class AdminprojectList
    {

        public int project_no {  get; set; }
        public string project_name { get; set; }
        public string project_lead {  get; set; }
        public DateTime project_start_date { get; set; }
        public DateTime project_end_date { get;set; }
        public string emp_name {  get; set; }
        public string emp_role {  get; set; }
        public string team_lead_members {  get; set; }
        public string team_lead_role {  get; set; }
        public TimeSpan total_timing { get; set; }
        public TimeSpan duration { get; set; }
        public string status { get; set; }
    }
}