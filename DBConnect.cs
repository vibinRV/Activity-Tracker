using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Activity_Tracker.Models
{
    public class DBConnect
    {
        string name;
        string pname;
        int project_no_value;
        
        public List<employeeDatabase> DailyemployeesTask(string a)
        {
            List<employeeDatabase> taskDetails = new List<employeeDatabase>();
            DateTime todayDate = DateTime.Today;
            string t = todayDate.ToString("yyyy-MM-dd");


            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker; select task_no,task_name,task_role,task_description,task_start_timing,task_end_timing,task_Date,project_name,status,[file],total_timing,duration from team_task_assignment WHERE CONVERT(DATE, task_Date) = @t and emp_name = '"+a+"';";

            using (SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@t", t);

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        taskDetails.Add(new employeeDatabase
                        {
                            task_no = (int)sqlDataReader["task_no"],
                            task_name = sqlDataReader["task_name"].ToString(),
                            task_role = sqlDataReader["task_role"].ToString(),
                            task_description = sqlDataReader["task_description"].ToString(),
                            task_date = todayDate,
                            task_start_time = (TimeSpan)sqlDataReader["task_start_timing"],
                            task_end_time = (TimeSpan)sqlDataReader["task_end_timing"],
                            project_name = sqlDataReader["project_name"].ToString(),
                            status = sqlDataReader["status"].ToString(),
                            file = sqlDataReader["file"].ToString(),
                            total_timing = (TimeSpan)sqlDataReader["total_timing"],
                            duration = (TimeSpan)sqlDataReader["duration"]
                        });
                    }
                }
            }

            return taskDetails;
        }

        public List<employeeDatabase> employees(string emp_name)
        {
            
            List<employeeDatabase> taskDetails = new List<employeeDatabase>();
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "select task_no,task_name,task_role,task_start_timing,task_end_timing,task_Date,project_name,status,total_timing,duration from team_task_assignment where emp_name = '" + emp_name+"';";
            
            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection); 
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand1.ExecuteReader();

            while(sqlDataReader.Read())
            {
                taskDetails.Add(new employeeDatabase
                {
                    task_no = (int)sqlDataReader["task_no"],
                    task_name = sqlDataReader["task_name"].ToString(),
                    task_role = sqlDataReader["task_role"].ToString(),
                    project_name = sqlDataReader["project_name"].ToString(),
                    task_date = (DateTime)sqlDataReader["task_Date"],
                    task_start_time = (TimeSpan)sqlDataReader["task_start_timing"],
                    task_end_time = (TimeSpan)sqlDataReader["task_end_timing"],
                    status = sqlDataReader["status"].ToString(),
                    total_timing = (TimeSpan)sqlDataReader["total_timing"],
                    duration = (TimeSpan)sqlDataReader["duration"]
                }) ;
            }
            return taskDetails ;
        }

        public List<employeeDatabase> listTasks(int task_no)
        {

            List<employeeDatabase> taskDetails = new List<employeeDatabase>();
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "select task_no,task_name,task_role,task_description,task_start_timing,task_end_timing,task_Date,project_name,status,[file],total_timing,duration from team_task_assignment where task_no = '" + task_no + "';";

            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand1.ExecuteReader();

            while (sqlDataReader.Read())
            {

                taskDetails.Add(new employeeDatabase
                {
                    task_no = (int)sqlDataReader["task_no"],
                    task_name = sqlDataReader["task_name"].ToString(),
                    task_role = sqlDataReader["task_role"].ToString(),
                    task_description = sqlDataReader["task_description"].ToString(),
                    task_start_time = (TimeSpan)sqlDataReader["task_start_timing"],
                    task_end_time = (TimeSpan)sqlDataReader["task_end_timing"],
                    project_name = sqlDataReader["project_name"].ToString(),
                    status = sqlDataReader["status"].ToString(),
                    file = sqlDataReader["file"].ToString(),
                    total_timing = (TimeSpan)sqlDataReader["total_timing"],
                    duration = (TimeSpan)sqlDataReader["duration"]
                });
            }
            return taskDetails;
        }

        public List<LoginPage> LoginVerify(string u_name,string pass)
        {
            List<LoginPage> a1 = new List<LoginPage>();
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "select * from LoginDB where username= '"+u_name+"'and password='"+pass+"';";

            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand1.ExecuteReader();

            while (sqlDataReader.Read())
            {
                a1.Add(new LoginPage
                {
                    sno = (int)sqlDataReader["sno"],
                    username = sqlDataReader["username"].ToString()
                });

            }
        return a1;
        }

        public string AddProjects(string project_name,string project_lead,string project_description,DateTime project_start_date,DateTime project_end_date)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "INSERT INTO project_info (project_name,project_lead,project_start_date,project_end_date,project_description) VALUES (@project_name,@project_lead,@project_start_date,@project_end_date,@project_description);";

            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);
            sqlCommand1.Parameters.AddWithValue("@project_name", project_name);
            sqlCommand1.Parameters.AddWithValue("@project_lead", project_lead);
            sqlCommand1.Parameters.AddWithValue("@project_start_date", project_start_date);
            sqlCommand1.Parameters.AddWithValue("@project_end_date", project_end_date);
            sqlCommand1.Parameters.AddWithValue("@project_description", project_description);
            int result = sqlCommand1.ExecuteNonQuery();
            if (result > 0)
            {
                return "1";
            }
            return "0";
        }

        
        public string AddPersons(string person_name, string person_role,int ans)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "INSERT INTO project_lead (person_name,person_role,project_no) VALUES (@person_name,@person_role,@project_no);";

            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);
            sqlCommand1.Parameters.AddWithValue("@person_name", person_name);
            sqlCommand1.Parameters.AddWithValue("@person_role", person_role);
            sqlCommand1.Parameters.AddWithValue("@project_no", ans);
            int result = sqlCommand1.ExecuteNonQuery();
            if (result > 0)
            {
                return "1";
            }
            return "0";
        }

        public List<AdminprojectList> projectItems()
        {
            List<AdminprojectList> projectLists = new List<AdminprojectList>();
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "select project_no,project_name,project_lead,project_start_date,project_end_date from project_info;";

            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand1.ExecuteReader();

            while (sqlDataReader.Read())
            {
                projectLists.Add(new AdminprojectList
                {
                    
                    project_no = (int)sqlDataReader["project_no"],
                    project_name = sqlDataReader["project_name"].ToString(),
                    project_lead = sqlDataReader["project_lead"].ToString(),
                    project_start_date = Convert.ToDateTime(sqlDataReader["project_start_date"]),
                    project_end_date = Convert.ToDateTime(sqlDataReader["project_end_date"])
                }) ;
            }
            return projectLists;
        }

        public List<TeamLead> TeamLead(string name)
        {
            pname = name;
            List<TeamLead> TeamList = new List<TeamLead>();
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "select project_lead,project_no,project_name,project_start_date,project_end_date from project_info where project_lead = '"+name+"';";
            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand1.ExecuteReader();

            while (sqlDataReader.Read())
            {
                TeamList.Add(new TeamLead
                {
                    project_lead = sqlDataReader["project_lead"].ToString() ,
                    project_no = (int)sqlDataReader["project_no"],
                    project_name = sqlDataReader["project_name"].ToString(),
                    project_start_date = Convert.ToDateTime(sqlDataReader["project_start_date"]),
                    project_end_date = Convert.ToDateTime(sqlDataReader["project_end_date"])
                });
            }
            return TeamList;
        }

        public List<TeamLead> TeamLeaddetails(int value,string s)
        {
            project_no_value = value;
            List<TeamLead> TeamList = new List<TeamLead>();
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "select pl.project_no,pl.person_no,pl.person_name,pl.person_role from project_lead as pl inner join project_info as pIn on pl.project_no = pIn.project_no where project_lead = '"+s+"' and pl.project_no = '"+ project_no_value + "';";
            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand1.ExecuteReader();

            while (sqlDataReader.Read())
            {
                TeamList.Add(new TeamLead
                {
                    project_no = (int)sqlDataReader["project_no"],   
                    person_no = (int)sqlDataReader["person_no"],
                    person_name = sqlDataReader["person_name"].ToString(),
                    person_role = sqlDataReader["person_role"].ToString()
                });
            }
            return TeamList;
        }

        public string AddTaskDetails(string task_name, string task_role, string task_description, string task_start_time, string task_end_time,int p_no,string emp_name,string d)
        {
            DateTime date = DateTime.Today;

            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            TimeSpan t1 = TimeSpan.Parse(task_start_time);
            TimeSpan t2 = TimeSpan.Parse(task_end_time);
            TimeSpan total_timing = t2.Subtract(t1);

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "INSERT INTO team_task_assignment (task_name, task_role, task_description, task_start_timing, task_end_timing,task_Date,project_no,emp_name,project_name,total_timing) VALUES (@task_name,@task_role,@task_description,@task_start_time,@task_end_time,@task_Date,@p_no,@emp_name,@project_name,@total_timing);";

            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);
            sqlCommand1.Parameters.AddWithValue("@task_name", task_name);
            sqlCommand1.Parameters.AddWithValue("@task_role", task_role);
            sqlCommand1.Parameters.AddWithValue("@task_description", task_description);
            sqlCommand1.Parameters.AddWithValue("@task_start_time", t1);
            sqlCommand1.Parameters.AddWithValue("@task_end_time", t2);
            sqlCommand1.Parameters.AddWithValue("@task_Date", date);
            sqlCommand1.Parameters.AddWithValue("@p_no", p_no);
            sqlCommand1.Parameters.AddWithValue("@emp_name", emp_name);
            sqlCommand1.Parameters.AddWithValue("@project_name", d);
            sqlCommand1.Parameters.AddWithValue("@total_timing", total_timing);
            int result = sqlCommand1.ExecuteNonQuery();
            if (result > 0)
            {
                return "1";
            }
            return "0";
        }

        public List<employeeDatabase> showAllTasks(string val, int d, string u_name)
        {

            List<employeeDatabase> TeamList = new List<employeeDatabase>();
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "select task_name,task_description,task_Date,status,total_timing,duration from team_task_assignment where emp_name = '" + val+"';";
            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand1.ExecuteReader();

            while (sqlDataReader.Read())
            {
                TeamList.Add(new employeeDatabase
                {
                    task_name = sqlDataReader["task_name"].ToString(),
                    task_description = sqlDataReader["task_description"].ToString(),
                    task_date = (DateTime)sqlDataReader["task_Date"],
                    status = sqlDataReader["status"].ToString(),
                    total_timing = (TimeSpan)sqlDataReader["total_timing"],
                    duration = (TimeSpan)sqlDataReader["duration"]
                });
            }
            return TeamList;
        }

        public List<TeamLead> TeamLeadMembers()
        {
            List<TeamLead> TeamList = new List<TeamLead>();
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "select project_lead,project_no from project_info;";
            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand1.ExecuteReader();

            while (sqlDataReader.Read())
            {
                TeamList.Add(new TeamLead
                {
                    project_lead = sqlDataReader["project_lead"].ToString(),
                    project_no = (int)sqlDataReader["project_no"],
                });
            }
            return TeamList;
        }

        

        public string AddEmployeeMembers(string emp_name,string emp_role)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "INSERT INTO emp_details (emp_name,emp_role) VALUES (@emp_name,@emp_role);";

            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);
            sqlCommand1.Parameters.AddWithValue("@emp_name", emp_name);
            sqlCommand1.Parameters.AddWithValue("@emp_role", emp_role);
            int result = sqlCommand1.ExecuteNonQuery();
            if (result > 0)
            {
                return "1";
            }
            return "0";
        }

        public string AddTeamLeadMembers(string team_lead_members, string team_lead_role)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "INSERT INTO team_lead_names (team_lead_members,team_lead_role) VALUES (@team_lead_members,@team_lead_role);";

            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);
            sqlCommand1.Parameters.AddWithValue("@team_lead_members", team_lead_members);
            sqlCommand1.Parameters.AddWithValue("@team_lead_role", team_lead_role);
            int result = sqlCommand1.ExecuteNonQuery();
            if (result > 0)
            {
                return "1";
            }
            return "0";
        }

        public List<AdminprojectList> ViewTeamLeadMembers()
        {
            List<AdminprojectList> TeamMembers = new List<AdminprojectList>();
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "select team_lead_members,team_lead_role from team_lead_names;";
            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand1.ExecuteReader();

            while (sqlDataReader.Read())
            {
                TeamMembers.Add(new AdminprojectList
                {
                    team_lead_members = sqlDataReader["team_lead_members"].ToString(),
                    team_lead_role = sqlDataReader["team_lead_role"].ToString(),
                });
            }
            return TeamMembers;
        }

        public List<AdminprojectList> ViewEmployeeMembers()
        {
            List<AdminprojectList> TeamMembers = new List<AdminprojectList>();
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "select emp_name,emp_role from emp_details;";
            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand1.ExecuteReader();

            while (sqlDataReader.Read())
            {
                TeamMembers.Add(new AdminprojectList
                {
                    emp_name = sqlDataReader["emp_name"].ToString(),
                    emp_role = sqlDataReader["emp_role"].ToString(),
                });
            }
            return TeamMembers;
        }

        public List<LoginPage> ViewLoginMembers()
        {
            List<LoginPage> LoginList = new List<LoginPage>();
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "select sno,username,password from loginDB;";
            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand1.ExecuteReader();

            while (sqlDataReader.Read())
            {
                LoginList.Add(new LoginPage
                {
                    sno = (int)sqlDataReader["sno"],
                    username = sqlDataReader["username"].ToString(),
                    password = sqlDataReader["password"].ToString(),
                });
            }
            return LoginList;
        }

        public string AddLoginMembers(string user_role, string user_name, string password)
        {
            int sno = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "INSERT INTO loginDB (sno,username,password) VALUES (@sno,@username,@password);";

            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);
            if (user_role.Equals("team lead"))
            {
                sno = 2;
            }
            else
            {
                sno = 3;
            }
            sqlCommand1.Parameters.AddWithValue("@sno", sno);
            sqlCommand1.Parameters.AddWithValue("@username", user_name);
            sqlCommand1.Parameters.AddWithValue("@password", password);
            int result = sqlCommand1.ExecuteNonQuery();
            if (result > 0)
            {
                return "1";
            }
            return "0";
        }


        public string AddFiles(string files,int task_no)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";

            TimeSpan t1 = new TimeSpan(00, 00, 00);
            TimeSpan t2 = new TimeSpan(00, 00, 00);

            string cmd2 = "select workStartTime,workEndTime from team_task_assignment WHERE task_no = '" + task_no + "'";
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand1.ExecuteReader();
            while (sqlDataReader.Read())
            {
                t1 = (TimeSpan)sqlDataReader["workStartTime"];
                t2 = (TimeSpan)sqlDataReader["workEndTime"];
            }
            TimeSpan duration = t2.Subtract(t1);
            sqlDataReader.Close();

            string cmd3 = "UPDATE team_task_assignment SET [file] = '"+files+ "' , duration = '" + duration + "' WHERE task_no = '" + task_no+"';";
            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand2 = new SqlCommand(cmd3, sqlConnection);
            int result = sqlCommand2.ExecuteNonQuery();
            if (result > 0)
            {
                return "1";
            }
            return "0";
        }

        public string ModifyStatus(string button,int task_no)
        {
            if (Equals(button, "START"))
            {
                button = "started";
            }
            else if(Equals(button,"STOP"))
            {
                button = "stopped";
            }
            else if (Equals(button, "UPLOAD"))
            {
                button = "completed";
            }
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "UPDATE team_task_assignment SET status = '" + button + "' WHERE task_no = '" + task_no + "';";
            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);
            int result = sqlCommand1.ExecuteNonQuery();
            if (result > 0)
            {
                return "1";
            }
            return "0";
        }

        public void timeStore(int task_no,int n)
        {
            string currtime = DateTime.Now.ToString("HH:mm");
            TimeSpan ts = TimeSpan.Parse(currtime);
            string connectionString = ConfigurationManager.ConnectionStrings["employee_task_details"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd1 = "use ActivityTracker;";
            string cmd2 = "";
            if(n == 1)
            {
                cmd2 = "UPDATE team_task_assignment SET workStartTime = '" + ts + "'  WHERE task_no = '" + task_no + "';";
            }
            else if (n == 2)
            {
                cmd2 = "UPDATE team_task_assignment SET workEndTime = '" + ts + "'  WHERE task_no = '" + task_no + "';";
            }

            SqlCommand sqlCommand = new SqlCommand(cmd1, sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(cmd2, sqlConnection);
            int result = sqlCommand1.ExecuteNonQuery();
        }
    }  
}