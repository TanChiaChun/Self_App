using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Self_App.myClasses
{
    class Database
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        // Specific
        private const string DB_FOLDER = "content";
        private const string DB_PATH = DB_FOLDER + "/app.db";
        private const string CONNECTION_STR = "Data Source=" + DB_PATH;
        public readonly string DATE_FORMAT_DB = "yyyy-MM-dd";
        public readonly string DATETIME_FORMAT_DB = "yyyy-MM-ddThh:mm:ss";
        public readonly string SQL_COMMA = ", ";

        public Database()
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

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public MyTask Select_Task(int id)
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
                            task.UpdateTask_FromDb(res["task_name"].ToString(), res["is_done"].ToString(), res["project"].ToString(), res["section"].ToString(), res["due_date"].ToString(), res["do_date"].ToString(), res["start_date"].ToString(), res["priority"].ToString(), res["my_day"].ToString(), res["tags"].ToString(), res["steps"].ToString(), res["note"].ToString());
                        }
                    }
                }
            }
            return task;
        }

        public List<MyTask> Select_TodoAll()
        {
            List<MyTask> tasks = new List<MyTask>();
            string query = "SELECT id, task_name, is_done, project, section, due_date, do_date, start_date, priority, my_day FROM Task ORDER BY modify_date DESC";
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
                                tasks.Add(new MyTask(res["id"].ToString(), res["task_name"].ToString(), res["is_done"].ToString(), res["project"].ToString(), res["section"].ToString(), res["due_date"].ToString(), res["do_date"].ToString(), res["start_date"].ToString(), res["priority"].ToString(), res["my_day"].ToString()));
                            }
                        }
                    }
                }
            }
            return tasks;
        }

        public void WriteDb(string query)
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
    }
}
