using System.Collections.Generic;
using System.Text;
using Assets.Plugins;
using Assets.script.common.entity;
using Assets.script.core.db;

namespace Assets.script.common.dao
{
    public static class EventDetailDao
    {
        public static List<EventDetailEntity> SelectAll()
        {
            List<EventDetailEntity> entityList = new List<EventDetailEntity>();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM EVENT_DETAIL;");
            DataTable dataTable = DbManager.ExecuteQuery(sb.ToString());
            dataTable.Rows.ForEach(r => entityList.Add(CreateEntity(r)));
            return entityList;
        }

        public static EventDetailEntity SelectByPrimaryKey(int eventId, int seq)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM EVENT_DETAIL WHERE EVENT_ID = ")
                .Append(eventId)
                .Append(" SEQ = ")
                .Append(seq)
                .Append(";");
            DataTable dataTable = DbManager.ExecuteQuery(sb.ToString());
            return dataTable.Rows.Count == 0 ? null : CreateEntity(dataTable[0]);
        }

        public static List<EventDetailEntity> SelectByEventId(int eventId)
        {
            List<EventDetailEntity> entityList = new List<EventDetailEntity>();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM EVENT_DETAIL WHERE EVENT_ID = ")
                .Append(eventId)
                .Append(";");
            DataTable dataTable = DbManager.ExecuteQuery(sb.ToString());
            dataTable.Rows.ForEach(r => entityList.Add(CreateEntity(r)));
            return entityList;
        }

        public static void Insert(EventDetailEntity entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO EVENT_DETAIL VALUES (")
                .Append(entity.EventId)
                .Append(",")
                .Append(entity.Seq)
                .Append(",")
                .Append(entity.TypeId)
                .Append(",")
                .Append("'")
                .Append(entity.Attr1)
                .Append("'")
                .Append(",")
                .Append("'")
                .Append(entity.Attr2)
                .Append("'")
                .Append(");");
            DbManager.ExecuteNonQuery(sb.ToString());
        }

        public static void Update(EventDetailEntity entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE EVENT_DETAIL SET ")
                .Append("EVENT_ID = ")
                .Append(entity.EventId)
                .Append(",")
                .Append("SEQ = ")
                .Append(entity.Seq)
                .Append(",")
                .Append("TYPE_ID = ")
                .Append(entity.TypeId)
                .Append(",")
                .Append("ATTR1 = ")
                .Append("'")
                .Append(entity.Attr1)
                .Append("'")
                .Append(",")
                .Append("ATTR2 = ")
                .Append("'")
                .Append(entity.Attr2)
                .Append("'")
                .Append(";");
            DbManager.ExecuteNonQuery(sb.ToString());
        }

        private static EventDetailEntity CreateEntity(DataRow row)
        {
            EventDetailEntity entity = new EventDetailEntity();

            entity.EventId = DaoSupport.GetIntValue(row, "EVENT_ID");

            entity.Seq = DaoSupport.GetIntValue(row, "SEQ");

            entity.TypeId = DaoSupport.GetIntValue(row, "TYPE_ID");

            entity.Attr1 = DaoSupport.GetStringValue(row, "ATTR1");

            entity.Attr2 = DaoSupport.GetStringValue(row, "ATTR2");

            return entity;
        }
    }
}