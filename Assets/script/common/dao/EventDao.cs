using System.Collections.Generic;
using System.Text;
using Assets.Plugins;
using Assets.script.common.entity;
using Assets.script.core.db;

namespace Assets.script.common.dao
{
    public static class EventDao
    {
        public static List<EventEntity> SelectAll()
        {
            List<EventEntity> entityList = new List<EventEntity>();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM EVENT;");
            DataTable dataTable = DbManager.ExecuteQuery(sb.ToString());
            dataTable.Rows.ForEach(r => entityList.Add(CreateEntity(r)));
            return entityList;
        }

        public static EventEntity SelectByPrimaryKey(int eventId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM EVENT WHERE EVENT_ID = ")
                .Append(eventId)
                .Append(";");
            DataTable dataTable = DbManager.ExecuteQuery(sb.ToString());
            return dataTable.Rows.Count == 0 ? null : CreateEntity(dataTable[0]);
        }

        public static List<EventEntity> SelectBySceneIdAndProcedure(string sceneId, int procedure)
        {
            List<EventEntity> entityList = new List<EventEntity>();
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

        public static void Insert(EventEntity entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO EVENT VALUES (")
                .Append(entity.EventId)
                .Append(",")
                .Append("'")
                .Append(entity.SceneId)
                .Append("'")
                .Append(",")
                .Append("'")
                .Append(entity.ObjectName)
                .Append("'")
                .Append(",")
                .Append(entity.Procedure)
                .Append(");");
            DbManager.ExecuteNonQuery(sb.ToString());
        }

        public static void Update(EventEntity entity)
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
                .Append("OBJECT_NAME = ")
                .Append("'")
                .Append(entity.ObjectName)
                .Append("'")
                .Append(",")
                .Append("PROCEDURE = ")
                .Append(entity.Procedure)
                .Append(";");
            DbManager.ExecuteNonQuery(sb.ToString());
        }

        private static EventEntity CreateEntity(DataRow row)
        {
            EventEntity entity = new EventEntity();

            entity.EventId = DaoSupport.GetIntValue(row, "EVENT_ID");

            entity.SceneId = DaoSupport.GetStringValue(row, "SCENE_ID");

            entity.ObjectName = DaoSupport.GetStringValue(row, "OBJECT_NAME");

            entity.Procedure = DaoSupport.GetIntValue(row, "PROCEDURE");

            return entity;
        }
    }
}