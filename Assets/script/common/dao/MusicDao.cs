using System.Collections.Generic;
using System.Text;
using Assets.Plugins;
using Assets.script.common.entity;
using Assets.script.core.db;

namespace Assets.script.common.dao
{
    public static class MusicDao
    {
        public static List<MusicEntity> SelectAll()
        {
            List<MusicEntity> entityList = new List<MusicEntity>();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM MUSIC;");
            DataTable dataTable = DbManager.ExecuteQuery(sb.ToString());
            dataTable.Rows.ForEach(r => entityList.Add(CreateEntity(r)));
            return entityList;
        }

        public static MusicEntity SelectByPrimaryKey(int musicId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM MUSIC WHERE MUSIC_ID = ")
                .Append(musicId)
                .Append(";");
            DataTable dataTable = DbManager.ExecuteQuery(sb.ToString());
            return dataTable.Rows.Count == 0 ? null : CreateEntity(dataTable[0]);
        }

        public static void Insert(MusicEntity entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO MUSIC VALUES (")
        
            
                .Append(entity.MusicId)
            
                .Append(",")
        
                .Append("'")
                .Append(entity.MusicName)
                .Append("'")
                .Append(",")
        
                .Append("'")
                .Append(entity.Time)
                .Append("'")
            
        
                .Append(");");
            DbManager.ExecuteNonQuery(sb.ToString());
        }

        public static void Update(MusicEntity entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE MUSIC SET ")
        
                .Append("MUSIC_ID = ")
            
                .Append(entity.MusicId)
            
                .Append(",")
        
                .Append("MUSIC_NAME = ")
                .Append("'")
                .Append(entity.MusicName)
                .Append("'")
                .Append(",")
        
                .Append("TIME = ")
                .Append("'")
                .Append(entity.Time)
                .Append("'")
            
        
                .Append(";");
            DbManager.ExecuteNonQuery(sb.ToString());
        }

        private static MusicEntity CreateEntity(DataRow row)
        {
            MusicEntity entity = new MusicEntity();
        
            entity.MusicId = DaoSupport.GetIntValue(row, "MUSIC_ID");
        
            entity.MusicName = DaoSupport.GetStringValue(row, "MUSIC_NAME");
        
            entity.Time = DaoSupport.GetStringValue(row, "TIME");
        
            return entity;
        }
    }
}