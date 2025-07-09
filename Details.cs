using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activity_Tracker.Models
{
    public class Details
    {
        private int project_no;

        public void set(int value)
        {
            this.project_no = value;
        }
        public int get()
        {
            return project_no;
        }
    }
}