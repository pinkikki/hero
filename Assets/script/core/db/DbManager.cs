using System;
using Assets.Plugins;

namespace Assets.script.core.db
{
    public class DbManager
    {
        private static SqliteDatabase mdb;
        public static SqliteDatabase Mdb
        {
            get { return mdb; }
        }

        public static void Init()
        {
            mdb = new SqliteDatabase("heroMaster.db");
        }

        public static DataTable ExecuteQuery(String query) {
            if (mdb == null)
            {
                Init();
            }
            DataTable dataTable = mdb.ExecuteQuery(query);
            return dataTable;
        }

        public static void ExecuteNonQuery(String query) {
            if (mdb == null)
            {
                Init();
            }
            mdb.ExecuteNonQuery(query);
        }
    }
}
