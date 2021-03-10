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
        private const string DB_FOLDER = "content";
        private const string DB_PATH = DB_FOLDER + "/app.db";
        private const string CONNECTION_STR = "Data Source=" + DB_PATH;

        public Database()
        {
            if (!File.Exists(DB_PATH))
            {
                Directory.CreateDirectory(DB_FOLDER);
                SQLiteConnection.CreateFile(DB_PATH);

                string query = "CREATE TABLE 'Task' ('id' INTEGER, 'task_name' TEXT NOT NULL DEFAULT '', 'is_completed' INTEGER NOT NULL DEFAULT 0 CHECK(is_completed >= 0 AND is_completed <= 1), 'project' TEXT NOT NULL DEFAULT 'General', 'section' TEXT NOT NULL DEFAULT 'General', 'due_date' TEXT NOT NULL DEFAULT '0001-01-01', 'do_date' TEXT NOT NULL DEFAULT '0001-01-01', 'start_date' TEXT NOT NULL DEFAULT '0001-01-01', 'my_day' INTEGER NOT NULL DEFAULT 4 CHECK(my_day >= 0 AND my_day <= 4), 'priority' INTEGER NOT NULL DEFAULT 2 CHECK(priority >= 0 AND priority <= 3), 'tags' TEXT NOT NULL DEFAULT '', 'steps' TEXT NOT NULL DEFAULT '', 'note' TEXT NOT NULL DEFAULT '', 'create_date' TEXT NOT NULL DEFAULT '0001-01-01', 'modify_date'	TEXT NOT NULL DEFAULT '0001-01-01', 'complete_date' TEXT NOT NULL DEFAULT '0001-01-01', 'external_id' INTEGER NOT NULL DEFAULT -1, PRIMARY KEY('id' AUTOINCREMENT))";
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
}
