using System;
using System.Collections.Generic;
using System.Data.SQLite;
using PerpustakaanAppMVC.Model.Entity;
using PerpustakaanAppMVC.Model.Context;

namespace PerpustakaanAppMVC.Model.Repository
{
    public class MahasiswaRepository
    {
        private SQLiteConnection _conn;

        public MahasiswaRepository(DbContext context)
        {
            _conn = context.Conn;
        }

        public int Create(Mahasiswa mhs)
        {
            int result = 0;
            string sql = @"insert into mahasiswa (npm, nama, angkatan) values (@npm, @nama, @angkatan)";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@npm", mhs.Npm);
                cmd.Parameters.AddWithValue("@nama", mhs.Nama);
                cmd.Parameters.AddWithValue("@angkatan", mhs.Angkatan);

                try
                {
                    _conn.Open();
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create Error: {0}", ex.Message);
                }
                finally
                {
                    _conn.Close();
                }
            }
            return result;
        }

        // Other methods (Update, Delete) remain the same as provided in the previous code snippet...

        public List<Mahasiswa> ReadAll()
        {
            List<Mahasiswa> list = new List<Mahasiswa>();
            try
            {
                string sql = @"select npm, nama, angkatan from mahasiswa order by nama";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Mahasiswa mhs = new Mahasiswa();
                            mhs.Npm = dtr["npm"].ToString();
                            mhs.Nama = dtr["nama"].ToString();
                            mhs.Angkatan = dtr["angkatan"].ToString();
                            list.Add(mhs);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }
            return list;
        }

        public List<Mahasiswa> ReadByNama(string nama)
        {
            List<Mahasiswa> list = new List<Mahasiswa>();
            try
            {
                string sql = @"select npm, nama, angkatan from mahasiswa where nama like @nama order by nama";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@nama", string.Format("%{0}%", nama));
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Mahasiswa mhs = new Mahasiswa();
                            mhs.Npm = dtr["npm"].ToString();
                            mhs.Nama = dtr["nama"].ToString();
                            mhs.Angkatan = dtr["angkatan"].ToString();
                            list.Add(mhs);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByNama error: {0}", ex.Message);
            }
            return list;
        }
    }
}
