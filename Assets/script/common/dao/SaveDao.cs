using System.Collections.Generic;
using System.Text;
using Plugins;
using script.common.entity;
using script.core.db;
using UnityEngine;

namespace script.common.dao
{
    public class SaveDao
    {
        public static SaveEntity SelectAll()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM SAVE;");
            DataTable dataTable = DbManager.ExecuteQuery(sb.ToString());
            return dataTable.Rows.Count == 0 ? null : CreateEntity(dataTable[0]);
        }

        public static void Insert()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO SAVE (SAVE_ID, SCENE_ID) VALUES (1, 'starting');");
            DbManager.ExecuteNonQuery(sb.ToString());
        }
        
        public static void Update(List<string> columnNames)
        {
            var sb = new StringBuilder();
            sb.Append("UPDATE SAVE SET ");
            columnNames.ForEach(s =>
                {
                    sb.Append(s)
                        .Append(" = ")
                        .Append(1)
                        .Append(",");
                });

            sb.Remove(sb.Length - 2, sb.Length - 1);
            sb.Append(";");
            DbManager.ExecuteNonQuery(sb.ToString());
        }
        
        public static void Update(string sceneId, int classroomProcedure,
            int corridorProcedure, int artroomProcedure, int schoolyardProcedure, List<string> statusColumnNames)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE SAVE SET ")
                .Append("SCENE_ID")
                .Append(" = ")
                .Append("'")
                .Append(sceneId)
                .Append("', ")
                .Append("CLASSROOM_PROCEDURE")
                .Append(" = ")
                .Append(classroomProcedure)
                .Append(", ")
                .Append("CORRIDOR_PROCEDURE")
                .Append(" = ")
                .Append(corridorProcedure)
                .Append(", ")
                .Append("ARTROOM_PROCEDURE")
                .Append(" = ")
                .Append(artroomProcedure)
                .Append(", ")
                .Append("SCHOOLYARD_PROCEDURE")
                .Append(" = ")
                .Append(schoolyardProcedure);
            
            statusColumnNames.ForEach(s =>
            {
                sb.Append(",")
                    .Append(s)
                    .Append(" = ")
                    .Append(1);
            });
            sb.Append(";");
            DbManager.ExecuteNonQuery(sb.ToString());
        }
        
        public static void Delete()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM SAVE;");
            DbManager.ExecuteNonQuery(sb.ToString());
        }

        private static SaveEntity CreateEntity(DataRow row)
        {
            return new SaveEntity
            {
                SaveId = DaoSupport.GetIntValue(row, "SAVE_ID"),
                SceneId = DaoSupport.GetStringValue(row, "SCENE_ID"),
                ClassroomProcedure = DaoSupport.GetIntValue(row, "CLASSROOM_PROCEDURE"),
                CorridorProcedure = DaoSupport.GetIntValue(row, "CORRIDOR_PROCEDURE"),
                ArtroomProcedure = DaoSupport.GetIntValue(row, "ARTROOM_PROCEDURE"),
                SchoolyardProcedure = DaoSupport.GetIntValue(row, "SCHOOLYARD_PROCEDURE"),
                Starting = DaoSupport.GetIntValue(row, "STARTING"),
                Cancomeinclassroom = DaoSupport.GetIntValue(row, "CANCOMEINCLASSROOM"),
                Hasquiza = DaoSupport.GetIntValue(row, "HASQUIZA"),
                Hascicada = DaoSupport.GetIntValue(row, "HASCICADA"),
                Hasbroom = DaoSupport.GetIntValue(row, "HASBROOM"),
                Iscompletedquiza = DaoSupport.GetIntValue(row, "ISCOMPLETEDQUIZA"),
                Hasquizb = DaoSupport.GetIntValue(row, "HASQUIZB"),
                Cansearchmarble = DaoSupport.GetIntValue(row, "CANSEARCHMARBLE"),
                Cansearchmatomari = DaoSupport.GetIntValue(row, "CANSEARCHMATOMARI"),
                Hasgraveroada = DaoSupport.GetIntValue(row, "HASGRAVEROADA"),
                Cangetgraveroadb = DaoSupport.GetIntValue(row, "CANGETGRAVEROADB"),
                Hasgraveroadb = DaoSupport.GetIntValue(row, "HASGRAVEROADB"),
                Hasmatomari = DaoSupport.GetIntValue(row, "HASMATOMARI"),
                Cancreatenerikeshi = DaoSupport.GetIntValue(row, "CANCREATENERIKESHI"),
                Hasglue = DaoSupport.GetIntValue(row, "HASGLUE"),
                Isfinishedwashinghands = DaoSupport.GetIntValue(row, "ISFINISHEDWASHINGHANDS"),
                Hasduster = DaoSupport.GetIntValue(row, "HASDUSTER"),
                Hasnerikeshi = DaoSupport.GetIntValue(row, "HASNERIKESHI"),
                Cangetmuddumplings = DaoSupport.GetIntValue(row, "CANGETMUDDUMPLINGS"),
                Hasmuddumplings = DaoSupport.GetIntValue(row, "HASMUDDUMPLINGS"),
                Hasmarble = DaoSupport.GetIntValue(row, "HASMARBLE"),
                Hasquizc = DaoSupport.GetIntValue(row, "HASQUIZC"),
                Hasquizd = DaoSupport.GetIntValue(row, "HASQUIZD"),
                Isfinishedfirstunlocking = DaoSupport.GetIntValue(row, "ISFINISHEDFIRSTUNLOCKING"),
                Isfinishedsecondunlocking = DaoSupport.GetIntValue(row, "ISFINISHEDSECONDUNLOCKING"),
                Hasquize = DaoSupport.GetIntValue(row, "HASQUIZE"),
                Canflowendroll = DaoSupport.GetIntValue(row, "CANFLOWENDROLL"),
                Iscompletedshinoburooma = DaoSupport.GetIntValue(row, "ISCOMPLETEDSHINOBUROOMA")
            };
        }
    }
}