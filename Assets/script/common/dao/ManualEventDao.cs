using System.Collections.Generic;
using System.Text;
using Assets.Plugins;
using Assets.script.common.entity;
using Assets.script.core.db;

namespace Assets.script.common.dao
{
    public static class ManualEventDao
    {
        public static List<ManualEventEntity> SelectAll()
        {
            List<ManualEventEntity> entityList = new List<ManualEventEntity>();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM MANUAL_EVENT;");
            DataTable dataTable = DbManager.ExecuteQuery(sb.ToString());
            dataTable.Rows.ForEach(r => entityList.Add(CreateEntity(r)));
            return entityList;
        }

        public static ManualEventEntity SelectByPrimaryKey(int eventId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM MANUAL_EVENT WHERE EVENT_ID = ")
                .Append(eventId)
                .Append(";");
            DataTable dataTable = DbManager.ExecuteQuery(sb.ToString());
            return dataTable.Rows.Count == 0 ? null : CreateEntity(dataTable[0]);
        }

        public static List<ManualEventEntity> SelectBySceneIdAndProcedure(string sceneId, int procedure)
        {
            List<ManualEventEntity> entityList = new List<ManualEventEntity>();
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

        public static void Insert(ManualEventEntity entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO MANUAL_EVENT VALUES (")
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

        public static void Update(ManualEventEntity entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE MANUAL_EVENT SET ")
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

        private static ManualEventEntity CreateEntity(DataRow row)
        {
            ManualEventEntity entity = new ManualEventEntity();

            entity.EventId = DaoSupport.GetIntValue(row, "EventId");

            entity.SceneId = DaoSupport.GetStringValue(row, "SceneId");

            entity.ObjectName = DaoSupport.GetStringValue(row, "ObjectName");

            entity.Procedure = DaoSupport.GetIntValue(row, "Procedure");

            return entity;
        }
    }
}