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
            SQLiteConnection conn = new SQLiteConnection("Data Source=db.db;Version=3");//adatbázis létrehozás
            conn.Open();

            SQLiteCommand letrehoz = conn.CreateCommand();//tábla létrehozás
            letrehoz.CommandText = @"CREATE TABLE IF NOT EXISTS asd
                                     (id INTEGER PRIMARY KEY AUTOINCREMENT,
                                     nev VARCHAR(128) NOT NULL,
                                     merete INTEGER NOT NULL);";
            letrehoz.ExecuteNonQuery();

            //------Adatbevite---------------

            //SQLiteCommand comInsert = conn.CreateCommand();
            //comInsert.CommandText = @"INSERT INTO asd (nev, merete)
            //                         VALUES ('Cirmi', 75), ('Bolyhos', 98), ('Garfiend', 300);";
            //comInsert.ExecuteNonQuery();


            SQLiteCommand comSzamol = conn.CreateCommand();
            comSzamol.CommandText = "SELECT count(*) FROM asd;";
            long db = (long) comSzamol.ExecuteScalar();
            Console.WriteLine($"{db} db rekord van");

            //-----paraméteres insert-----------
            //var comParameteresInsert = conn.CreateCommand();
            //comParameteresInsert.CommandText = @"INSERT INTO asd (nev, merete)
            //                                     VALUES (@nev, @merete);";
            //comParameteresInsert.Parameters.AddWithValue("@nev", "ASD");
            //comParameteresInsert.Parameters.AddWithValue("@merete", 98);
            //comParameteresInsert.ExecuteNonQuery();

            //------Adatok lekérdezése-------------
            SQLiteCommand comSelect = conn.CreateCommand();
            comSelect.CommandText = @"SELECT id, nev, merete FROM asd;";
            using (SQLiteDataReader reader = comSelect.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string nev = reader.GetString(1);
                    int merete = reader.GetInt32(2);
                    Console.WriteLine($"{id}, {nev}, {merete}");
                }
            }

            //-------paraméter lekérdezés--------------
            //Console.WriteLine("Kérem a méretet: ");
            //int a;
            //if (!int.TryParse(Console.ReadLine(), out a))
            //{
            //    return;
            //}
            //SQLiteCommand comLekerdez = conn.CreateCommand();
            //comLekerdez.CommandText = @"SELECT id, nev, merete FROM asd
            //                            WHERE merete > @meret;";
            //comLekerdez.Parameters.AddWithValue("@meret", a);
            //using (var reader = comLekerdez.ExecuteReader())
            //{
            //    while (reader.Read())
            //    {
            //        int id = reader.GetInt32(0);
            //        string nev = reader.GetString(1);
            //        int merete = reader.GetInt32(2);
            //        Console.WriteLine($"{nev} ({id}) \t{merete}");
            //    }
            //}

            
            //-------id be adatok ki--------------
            Console.WriteLine("Kérem az id-t: ");
            int i;
            if (!int.TryParse(Console.ReadLine(), out i))
            {
                return;
            }
            SQLiteCommand comid = conn.CreateCommand();
            comid.CommandText = @"SELECT id, nev, merete FROM asd
                                        WHERE id = @id;";
            comid.Parameters.AddWithValue("@id", i);
            using (var reader = comid.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string nev = reader.GetString(1);
                    int merete = reader.GetInt32(2);
                    Console.WriteLine($"{nev} ({id}) \t{merete}");
                }
            }

            //-------nev be adatok ki--------------
            Console.WriteLine("Kérem a nevet: ");
            string n = Console.ReadLine();
            SQLiteCommand comnev = conn.CreateCommand();
            comnev.CommandText = @"SELECT id, nev, merete FROM asd
                                        WHERE nev = @nev;";
            comnev.Parameters.AddWithValue("@nev", n);
            using (var reader = comnev.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string nev = reader.GetString(1);
                    int merete = reader.GetInt32(2);
                    Console.WriteLine($"{nev} ({id}) \t{merete}");
                }
            }

            //-------szam be , ki db kissebb van--------------
            Console.WriteLine("Kérem a számot: ");
            int szam;
            if (!int.TryParse(Console.ReadLine(), out szam))
            {
                return;
            }
            SQLiteCommand comszam = conn.CreateCommand();
            comszam.CommandText = @"SELECT count(id) FROM asd
                                        WHERE merete < @merete;";
            comszam.Parameters.AddWithValue("@merete", szam);
            using (var reader = comszam.ExecuteReader())
            {
                while (reader.Read())
                {
                    int merete = reader.GetInt32(0);
                    Console.WriteLine($"{merete}db");
                }
            }
            
            //-------nev valtoztatas--------------
            Console.WriteLine("Kérem az id-t:");
            int ix;
            if (!int.TryParse(Console.ReadLine(), out ix))
            {
                return;
            }

            Console.WriteLine("Kérem az új nevet:");
            string ujnev = Console.ReadLine();

            SQLiteCommand comnnev = conn.CreateCommand();
            comnnev.CommandText = @"UPDATE asd 
                                  SET nev = @ujnev
                                  WHERE id = @id;";
            comnnev.Parameters.AddWithValue("@id", ix);
            comnnev.Parameters.AddWithValue("@ujnev", ujnev);


            Console.WriteLine("-------");
            SQLiteCommand comSorrend = conn.CreateCommand();
            comSorrend.CommandText = @"SELECT merete, COUNT(id) FROM asd GROUP BY merete ORDER BY merete";
            using (var reader = comSorrend.ExecuteReader())
            {
                while (reader.Read())
                {
                    int merete = reader.GetInt32(0);
                    int darab = reader.GetInt32(1);
                    Console.WriteLine($"{merete}cm: {darab}db");
                }
            }

            Console.ReadKey();
        }
    }
}