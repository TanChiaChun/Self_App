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

                string query = "CREATE TABLE 'Task' ('id' INTEGER, 'task_name' TEXT NOT NULL DEFAULT '', 'is_completed' INTEGER NOT NULL DEFAULT 0 CHECK(is_completed >= 0 AND is_completed <= 1), 'project' TEXT NOT NULL DEFAULT 'General', 'section' TEXT NOT NULL DEFAULT 'General', 'due_date' TEXT NOT NULL DEFAULT '0001-01-01', 'do_date' TEXT NOT NULL DEFAULT '0001-01-01', 'start_date' TEXT NOT NULL DEFAULT '0001-01-01', 'my_day' INTEGER NOT NULL DEFAULT 4 CHECK(my_day >= 0 AND my_day <= 4), 'priority' INTEGER NOT NULL DEFAULT 2 CHECK(priority >= 0 AND priority <= 3), 'tags' TEXT NOT NULL DEFAULT '', 'steps' TEXT NOT NULL DEFAULT '', 'note' TEXT NOT NULL DEFAULT '', 'create_date' TEXT NOT NULL DEFAULT '0001-01-01T12:00:00', 'modify_date' TEXT NOT NULL DEFAULT '0001-01-01T12:00:00', 'complete_date' TEXT NOT NULL DEFAULT '0001-01-01T12:00:00', 'external_id' INTEGER NOT NULL DEFAULT -1, PRIMARY KEY('id' AUTOINCREMENT))";
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
        public List<MyTask> Select_TodoAll()
        {
            List<MyTask> tasks = new List<MyTask>();
            string query = "SELECT id, project, section, task_name, is_completed, due_date, do_date, start_date, my_day, priority FROM Task ORDER BY modify_date DESC";
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
                                tasks.Add(new MyTask(Convert.ToInt32(res["id"]), res["project"].ToString(), res["section"].ToString(), res["task_name"].ToString(), Convert.ToBoolean(res["is_completed"]), DateTime.ParseExact(res["due_date"].ToString(), DATE_FORMAT_DB, null), DateTime.ParseExact(res["do_date"].ToString(), DATE_FORMAT_DB, null), DateTime.ParseExact(res["start_date"].ToString(), DATE_FORMAT_DB, null), Convert.ToInt32(res["my_day"]), Convert.ToInt32(res["priority"])));
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
