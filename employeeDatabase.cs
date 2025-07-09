using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activity_Tracker.Models
{
    public class employeeDatabase
    {
        public int task_no {  get; set; }
        public string task_name {  get; set; }
        public string task_role {  get; set; }
        public string task_description { get; set; }
        public DateTime task_date {  get; set; }
        public TimeSpan task_start_time {  get; set; }
        public TimeSpan task_end_time { get;set; }
        public string project_name {  get; set; }
        public string status {  get; set; }
        public string file { get; set; }
        public TimeSpan total_timing { get; set; }
        public TimeSpan duration { get; set; }
    }
}