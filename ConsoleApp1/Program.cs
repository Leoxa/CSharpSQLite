using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=db.db;Version=3");
            conn.Open();

            SQLiteCommand letrehoz = conn.CreateCommand();
            letrehoz.CommandText = @"CREATE TABLE IF NOT EXISTS asd
                                     (id INTEGER PRIMARY KEY AUTOINCREMENT,
                                     nev VARCHAR(128) NOT NULL,
                                     merete INTEGER NOT NULL);";
            letrehoz.ExecuteNonQuery();


        }
    }
}
