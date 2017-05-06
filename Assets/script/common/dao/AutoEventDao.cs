using System.Collections.Generic;
using System.Text;
using Assets.Plugins;
using Assets.script.common.entity;
using Assets.script.core.db;

namespace Assets.script.common.dao
{
    public static class AutoEventDao
    {
        public static List<AutoEventEntity> SelectAll()
        {
            List<AutoEventEntity> entityList = new List<AutoEventEntity>();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM EVENT;");
            DataTable dataTable = DbManager.ExecuteQuery(sb.ToString());
            dataTable.Rows.ForEach(r => entityList.Add(CreateEntity(r)));
            return entityList;
        }

        public static AutoEventEntity SelectByPrimaryKey(int eventId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM EVENT WHERE EVENT_ID = ")
                .Append(eventId)
                .Append(";");
            DataTable dataTable = DbManager.ExecuteQuery(sb.ToString());
            return dataTable.Rows.Count == 0 ? null : CreateEntity(dataTable[0]);
        }

        public static List<AutoEventEntity> SelectBySceneId(string sceneId, int procedure)
        {
            List<AutoEventEntity> entityList = new List<AutoEventEntity>();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM EVENT e1 where SCENE_ID = '")
                .Append(sceneId)
                .Append("'")
                .Append(" and PROCEDURE = ")
                .Append(procedure)
                .Append(";");
            DataTable dataTable = DbManager.ExecuteQuery(sb.ToString());
            dataTable.Rows.ForEach(r => entityList.Add(CreateEntity(r)));
            return entityList;

        }

        public static void Insert(AutoEventEntity entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO EVENT VALUES (")
                .Append(entity.EventId)
                .Append(",")
                .Append("'")
                .Append(entity.SceneId)
                .Append("'")
                .Append(",")
                .Append(entity.Procedure)
                .Append(");");
            DbManager.ExecuteNonQuery(sb.ToString());
        }

        public static void Update(AutoEventEntity entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE EVENT SET ")
                .Append("EVENT_ID = ")
                .Append(entity.EventId)
                .Append(",")
                .Append("SCENE_ID = ")
                .Append("'")
                .Append(entity.SceneId)
                .Append("'")
                .Append(",")
                .Append("PROCEDURE = ")
                .Append(entity.Procedure)
                .Append(";");
            DbManager.ExecuteNonQuery(sb.ToString());
        }

        private static AutoEventEntity CreateEntity(DataRow row)
        {
            AutoEventEntity entity = new AutoEventEntity();

            entity.EventId = DaoSupport.GetIntValue(row, "EventId");

            entity.SceneId = DaoSupport.GetStringValue(row, "SceneId");

            entity.Procedure = DaoSupport.GetIntValue(row, "Procedure");

            return entity;
        }
    }
}