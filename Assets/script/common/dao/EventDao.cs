using System.Collections.Generic;
using System.Text;
using System.Threading;
using Plugins;
using script.common.entity;
using script.core.db;

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

    public static List<EventEntity> SelectBySceneId(string sceneId)
    {
        List<EventEntity> entityList = new List<EventEntity>();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM EVENT e1 where SCENE_ID = '")
            .Append(sceneId)
            .Append("'")
            .Append(" and COMPLETE_FLG <> 1 and NOT EXISTS (select 1 from EVENT e2 where SCENE_ID = '")
            .Append(sceneId)
            .Append("'")
            .Append(" and COMPLETE_FLG <> 1 and e2.PROCEDURE < e1.PROCEDURE)")
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
            .Append(entity.CompleteFlg ? 1 : 0)
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
            .Append("COMPLETE_FLG = ")
            .Append(entity.CompleteFlg ? 1 : 0)
            .Append(",")
            .Append("PROCEDURE = ")
            .Append(entity.Procedure)
            .Append(";");
        DbManager.ExecuteNonQuery(sb.ToString());
    }

    private static EventEntity CreateEntity(DataRow row)
    {
        EventEntity entity = new EventEntity();

        entity.EventId = DaoSupport.GetIntValue(row, "EventId");

        entity.SceneId = DaoSupport.GetStringValue(row, "SceneId");

        entity.CompleteFlg = DaoSupport.GetBoolValue(row, "CompleteFlg");

        entity.Procedure = DaoSupport.GetIntValue(row, "Procedure");

        return entity;
    }
}