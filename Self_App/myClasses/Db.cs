using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Self_App.myClasses
{
    static class Db
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        // Specific
        private const string DB_FOLDER = "content";
        private const string DB_PATH = DB_FOLDER + "/app.db";
        private const string CONNECTION_STR = "Data Source=" + DB_PATH;
        
        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public static void InitDb()
        {
            if (!File.Exists(DB_PATH))
            {
                Directory.CreateDirectory(DB_FOLDER);
                SQLiteConnection.CreateFile(DB_PATH);

                string query = "CREATE TABLE 'Task' ('id' INTEGER, 'task_name' TEXT NOT NULL DEFAULT '', 'is_done' INTEGER NOT NULL DEFAULT 0 CHECK(is_done >= 0 AND is_done <= 1), 'project' TEXT NOT NULL DEFAULT 'General', 'section' TEXT NOT NULL DEFAULT 'General', 'due_date' TEXT NOT NULL DEFAULT '0001-01-01', 'do_date' TEXT NOT NULL DEFAULT '0001-01-01', 'start_date' TEXT NOT NULL DEFAULT '0001-01-01', 'priority' INTEGER NOT NULL DEFAULT 2 CHECK(priority >= 0 AND priority <= 3), 'my_day' INTEGER NOT NULL DEFAULT 4 CHECK(my_day >= 0 AND my_day <= 4), 'tags' TEXT NOT NULL DEFAULT '', 'steps' TEXT NOT NULL DEFAULT '', 'note' TEXT NOT NULL DEFAULT '', 'create_date' TEXT NOT NULL DEFAULT '0001-01-01T12:00:00', 'modify_date' TEXT NOT NULL DEFAULT '0001-01-01T12:00:00', 'complete_date' TEXT NOT NULL DEFAULT '0001-01-01T12:00:00', 'external_id' INTEGER NOT NULL DEFAULT -1, PRIMARY KEY('id' AUTOINCREMENT))";
                using (SQLiteConnection connect = new SQLiteConnection(CONNECTION_STR))
                {
                    connect.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, connect))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static MyTask Select_Task(int id)
        {
            MyTask task = new MyTask(id);
            string query = $"SELECT project, section, task_name, is_done, due_date, do_date, start_date, priority, my_day, tags, steps, note FROM Task WHERE id={id}";
            using (SQLiteConnection connect = new SQLiteConnection(CONNECTION_STR))
            {
                connect.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, connect))
                {
                    using (SQLiteDataReader res = cmd.ExecuteReader())
                    {
                        if (res.HasRows)
                        {
                            res.Read();
                            string taskName = res["task_name"].ToString();
                            bool isDone = Convert.ToBoolean(Int32.Parse(res["is_done"].ToString()));
                            string project = res["project"].ToString();
                            string section = res["section"].ToString();
                            DateTime dueDate = DateTime.ParseExact(res["due_date"].ToString(), MyCls.DATE_FORMAT_DB, null);
                            DateTime doDate = DateTime.ParseExact(res["do_date"].ToString(), MyCls.DATE_FORMAT_DB, null);
                            DateTime startDate = DateTime.ParseExact(res["start_date"].ToString(), MyCls.DATE_FORMAT_DB, null);
                            MyCls.Priority _priority = (MyCls.Priority)Int32.Parse(res["priority"].ToString());
                            MyCls.MyDay _myDay = (MyCls.MyDay)Int32.Parse(res["my_day"].ToString());
                            string tags = res["tags"].ToString();
                            string steps = res["steps"].ToString();
                            string note = res["note"].ToString();
                            task.UpdateTask_FromDb(taskName, isDone, project, section, dueDate, doDate, startDate, _priority, _myDay, tags, steps, note);
                        }
                    }
                }
            }
            return task;
        }

        public static List<string> Select_Projects()
        {
            List<string> projects = new List<string>();
            string query = "SELECT DISTINCT project FROM Task ORDER BY project ASC";
            using (SQLiteConnection connect = new SQLiteConnection(CONNECTION_STR))
            {
                connect.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, connect))
                {
                    using (SQLiteDataReader res = cmd.ExecuteReader())
                    {
                        if (res.HasRows)
                        {
                            while (res.Read())
                            {
                                projects.Add(res["project"].ToString());
                            }
                        }
                    }
                }
            }
            return projects;
        }

        public static List<string> Select_Sections(string project)
        {
            List<string> sections = new List<string>();
            string query = $"SELECT DISTINCT section FROM Task WHERE project='{project}' ORDER BY section ASC";
            using (SQLiteConnection connect = new SQLiteConnection(CONNECTION_STR))
            {
                connect.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, connect))
                {
                    using (SQLiteDataReader res = cmd.ExecuteReader())
                    {
                        if (res.HasRows)
                        {
                            while (res.Read())
                            {
                                sections.Add(res["section"].ToString());
                            }
                        }
                    }
                }
            }
            return sections;
        }

        public static HashSet<string> Select_Tags()
        {
            HashSet<string> tags = new HashSet<string>();
            string query = "WITH RECURSIVE neat (id1, tag1, etc) AS (SELECT id, '', tags || ';' FROM Task WHERE id UNION ALL SELECT id1, substr(etc, 0, instr(etc, ';')), substr(etc, instr(etc, ';') + 1) FROM neat WHERE etc <> '') SELECT DISTINCT tag1 FROM neat WHERE tag1 <> '' ORDER BY tag1 ASC";
            using (SQLiteConnection connect = new SQLiteConnection(CONNECTION_STR))
            {
                connect.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, connect))
                {
                    using (SQLiteDataReader res = cmd.ExecuteReader())
                    {
                        if (res.HasRows)
                        {
                            while (res.Read())
                            {
                                tags.Add(res["tag1"].ToString());
                            }
                        }
                    }
                }
            }
            return tags;
        }

        public static List<MyTask> Select_TodoMyDay(int myDay)
        {
            List<MyTask> tasks = new List<MyTask>();
            string query = $"SELECT id, task_name, is_done, due_date FROM Task WHERE my_day={myDay} AND (is_done=0 OR complete_date>'{DateTime.Now.ToString(MyCls.DATE_FORMAT_DB)}') ORDER BY is_done ASC, modify_date DESC";
            using (SQLiteConnection connect = new SQLiteConnection(CONNECTION_STR))
            {
                connect.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, connect))
                {
                    using (SQLiteDataReader res = cmd.ExecuteReader())
                    {
                        if (res.HasRows)
                        {
                            while (res.Read())
                            {
                                int id = Int32.Parse(res["id"].ToString());
                                string taskName = res["task_name"].ToString();
                                bool isDone = Convert.ToBoolean(Int32.Parse(res["is_done"].ToString()));
                                DateTime dueDate = DateTime.ParseExact(res["due_date"].ToString(), MyCls.DATE_FORMAT_DB, null);
                                tasks.Add(new MyTask(id, taskName, isDone, dueDate));
                            }
                        }
                    }
                }
            }
            return tasks;
        }

        public static List<MyTask> Select_TodoDate(string type, MyCls.DateRange dateRng)
        {
            string optr = "";
            string sort = "";
            switch (dateRng)
            {
                case MyCls.DateRange.Earlier:
                    optr = "<";
                    sort = "DESC";
                    break;
                case MyCls.DateRange.Today:
                    optr = "=";
                    sort = "ASC";
                    break;
                case MyCls.DateRange.Upcoming:
                    optr = ">";
                    sort = "ASC";
                    break;
            }

            List<MyTask> tasks = new List<MyTask>();
            string query = $"SELECT id, task_name, project, section, due_date, do_date, priority, my_day, (CASE WHEN my_day<4 THEN 1 ELSE 0 END) as my_day_not_done FROM Task WHERE is_done=0 AND {type}_date!='0001-01-01' AND {type}_date{optr}'{DateTime.Now.ToString(MyCls.DATE_FORMAT_DB)}' ORDER BY {type}_date {sort}, priority ASC, modify_date DESC";
            using (SQLiteConnection connect = new SQLiteConnection(CONNECTION_STR))
            {
                connect.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, connect))
                {
                    using (SQLiteDataReader res = cmd.ExecuteReader())
                    {
                        if (res.HasRows)
                        {
                            while (res.Read())
                            {
                                int id = Int32.Parse(res["id"].ToString());
                                string taskName = res["task_name"].ToString();
                                string project = res["project"].ToString();
                                string section = res["section"].ToString();
                                DateTime dueDate = DateTime.ParseExact(res["due_date"].ToString(), MyCls.DATE_FORMAT_DB, null);
                                DateTime doDate = DateTime.ParseExact(res["do_date"].ToString(), MyCls.DATE_FORMAT_DB, null);
                                MyCls.Priority _priority = (MyCls.Priority)Int32.Parse(res["priority"].ToString());
                                MyCls.MyDay _myDay = (MyCls.MyDay)Int32.Parse(res["my_day"].ToString());
                                bool myDay_notDone = Convert.ToBoolean(Int32.Parse(res["my_day_not_done"].ToString()));
                                tasks.Add(new MyTask(id, taskName, project, section, dueDate, doDate, _priority, _myDay, myDay_notDone));
                            }
                        }
                    }
                }
            }
            return tasks;
        }

        public static List<MyTask> Select_TodoPriority()
        {
            List<MyTask> tasks = new List<MyTask>();
            string query = "SELECT id, task_name, project, section, due_date, do_date, priority, my_day, (CASE WHEN my_day<4 THEN 1 ELSE 0 END) as my_day_not_done FROM Task WHERE is_done=0 ORDER BY priority ASC, due_date ASC, modify_date DESC";
            using (SQLiteConnection connect = new SQLiteConnection(CONNECTION_STR))
            {
                connect.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, connect))
                {
                    using (SQLiteDataReader res = cmd.ExecuteReader())
                    {
                        if (res.HasRows)
                        {
                            while (res.Read())
                            {
                                int id = Int32.Parse(res["id"].ToString());
                                string taskName = res["task_name"].ToString();
                                string project = res["project"].ToString();
                                string section = res["section"].ToString();
                                DateTime dueDate = DateTime.ParseExact(res["due_date"].ToString(), MyCls.DATE_FORMAT_DB, null);
                                DateTime doDate = DateTime.ParseExact(res["do_date"].ToString(), MyCls.DATE_FORMAT_DB, null);
                                MyCls.Priority _priority = (MyCls.Priority)Int32.Parse(res["priority"].ToString());
                                MyCls.MyDay _myDay = (MyCls.MyDay)Int32.Parse(res["my_day"].ToString());
                                bool myDay_notDone = Convert.ToBoolean(Int32.Parse(res["my_day_not_done"].ToString()));
                                tasks.Add(new MyTask(id, taskName, project, section, dueDate, doDate, _priority, _myDay, myDay_notDone));
                            }
                        }
                    }
                }
            }
            return tasks;
        }

        public static List<MyTask> Select_TodoBlank()
        {
            List<MyTask> tasks = new List<MyTask>();
            string query = "SELECT id, task_name, project, section, start_date, priority FROM Task WHERE is_done=0 AND due_date='0001-01-01' AND do_date='0001-01-01' AND priority>0 AND my_day=4 ORDER BY project ASC, section ASC";
            using (SQLiteConnection connect = new SQLiteConnection(CONNECTION_STR))
            {
                connect.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, connect))
                {
                    using (SQLiteDataReader res = cmd.ExecuteReader())
                    {
                        if (res.HasRows)
                        {
                            while (res.Read())
                            {
                                int id = Int32.Parse(res["id"].ToString());
                                string taskName = res["task_name"].ToString();
                                string project = res["project"].ToString();
                                string section = res["section"].ToString();
                                DateTime startDate = DateTime.ParseExact(res["start_date"].ToString(), MyCls.DATE_FORMAT_DB, null);
                                MyCls.Priority _priority = (MyCls.Priority)Int32.Parse(res["priority"].ToString());
                                tasks.Add(new MyTask(id, taskName, project, section, startDate, _priority));
                            }
                        }
                    }
                }
            }
            return tasks;
        }

        public static List<MyTask> Select_TodoAll()
        {
            List<MyTask> tasks = new List<MyTask>();
            string query = "SELECT id, task_name, is_done, project, section, due_date, do_date, start_date, priority, my_day, (CASE WHEN my_day<4 THEN 1 ELSE 0 END) as my_day_not_done FROM Task ORDER BY modify_date DESC";
            using (SQLiteConnection connect = new SQLiteConnection(CONNECTION_STR))
            {
                connect.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, connect))
                {
                    using (SQLiteDataReader res = cmd.ExecuteReader())
                    {
                        if (res.HasRows)
                        {
                            while (res.Read())
                            {
                                int id = Int32.Parse(res["id"].ToString());
                                string taskName = res["task_name"].ToString();
                                bool isDone = Convert.ToBoolean(Int32.Parse(res["is_done"].ToString()));
                                string project = res["project"].ToString();
                                string section = res["section"].ToString();
                                DateTime dueDate = DateTime.ParseExact(res["due_date"].ToString(), MyCls.DATE_FORMAT_DB, null);
                                DateTime doDate = DateTime.ParseExact(res["do_date"].ToString(), MyCls.DATE_FORMAT_DB, null);
                                DateTime startDate = DateTime.ParseExact(res["start_date"].ToString(), MyCls.DATE_FORMAT_DB, null);
                                MyCls.Priority _priority = (MyCls.Priority)Int32.Parse(res["priority"].ToString());
                                MyCls.MyDay _myDay = (MyCls.MyDay)Int32.Parse(res["my_day"].ToString());
                                bool myDay_notDone = Convert.ToBoolean(Int32.Parse(res["my_day_not_done"].ToString()));
                                tasks.Add(new MyTask(id, taskName, isDone, project, section, dueDate, doDate, startDate, _priority, _myDay, myDay_notDone));
                            }
                        }
                    }
                }
            }
            return tasks;
        }

        public static List<MyTask> Select_SectionTasks(string proj, string sect, bool includeDone)
        {
            string doneFltr = includeDone ? "" : " AND is_done=0";
            List<MyTask> tasks = new List<MyTask>();
            string query = $"SELECT id, task_name, is_done, due_date, do_date, start_date, (CASE WHEN is_done=0 AND my_day<4 THEN 1 ELSE 0 END) AS my_day_not_done, (CASE WHEN steps!='' THEN 1 ELSE 0 END) AS has_steps, (CASE WHEN note!='' THEN 1 ELSE 0 END) AS has_note FROM Task WHERE project='{proj}' AND section='{sect}'{doneFltr} ORDER BY is_done ASC, task_name ASC";
            using (SQLiteConnection connect = new SQLiteConnection(CONNECTION_STR))
            {
                connect.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, connect))
                {
                    using (SQLiteDataReader res = cmd.ExecuteReader())
                    {
                        if (res.HasRows)
                        {
                            while (res.Read())
                            {
                                int id = Int32.Parse(res["id"].ToString());
                                string taskName = res["task_name"].ToString();
                                bool isDone = Convert.ToBoolean(Int32.Parse(res["is_done"].ToString()));
                                DateTime dueDate = DateTime.ParseExact(res["due_date"].ToString(), MyCls.DATE_FORMAT_DB, null);
                                DateTime doDate = DateTime.ParseExact(res["do_date"].ToString(), MyCls.DATE_FORMAT_DB, null);
                                DateTime startDate = DateTime.ParseExact(res["start_date"].ToString(), MyCls.DATE_FORMAT_DB, null);
                                bool myDay_notDone = Convert.ToBoolean(Int32.Parse(res["my_day_not_done"].ToString()));
                                bool hasSteps = Convert.ToBoolean(Int32.Parse(res["has_steps"].ToString()));
                                bool hasNote = Convert.ToBoolean(Int32.Parse(res["has_note"].ToString()));
                                tasks.Add(new MyTask(id, taskName, isDone, dueDate, doDate, startDate, myDay_notDone, hasSteps, hasNote));
                            }
                        }
                    }
                }
            }
            return tasks;
        }

        public static void WriteDb(string query)
        {
            using (SQLiteConnection connect = new SQLiteConnection(CONNECTION_STR))
            {
                connect.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, connect))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteTask(int id)
        {
            string query = $"DELETE FROM Task WHERE id={id}";
            using (SQLiteConnection connect = new SQLiteConnection(CONNECTION_STR))
            {
                connect.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, connect))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
