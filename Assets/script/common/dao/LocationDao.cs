using System.Collections.Generic;
using System.Text;
using Plugins;
using script.common.entity;
using script.core.db;

namespace script.common.dao
{
    public static class LocationDao
    {
        public static List<LocationEntity> SelectAll()
        {
            List<LocationEntity> entityList = new List<LocationEntity>();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM LOCATION;");
            DataTable dataTable = DbManager.ExecuteQuery(sb.ToString());
            dataTable.Rows.ForEach(r => entityList.Add(CreateEntity(r)));
            return entityList;
        }

        public static LocationEntity SelectByPrimaryKey(int locationId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM LOCATION WHERE LOCATION_ID = ")
                .Append(locationId)
                .Append(";");
            DataTable dataTable = DbManager.ExecuteQuery(sb.ToString());
            return dataTable.Rows.Count == 0 ? null : CreateEntity(dataTable[0]);
        }

        public static List<LocationEntity> SelectBySceneStatus(string sceneId, int entranceNo, int procedure)
        {
            List<LocationEntity> entityList = new List<LocationEntity>();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM LOCATION WHERE SCENE_ID = '")
                .Append(sceneId)
                .Append("' AND ENTRANCE_NO = ")
                .Append(entranceNo)
                .Append(" AND PROCEDURE = ")
                .Append(procedure)
                .Append("");
            DataTable dataTable = DbManager.ExecuteQuery(sb.ToString());
            dataTable.Rows.ForEach(r => entityList.Add(CreateEntity(r)));
            return entityList;
        }

        public static void Insert(LocationEntity entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO LOCATION VALUES (")
                .Append(entity.LocationId)
                .Append(",")
                .Append("'")
                .Append(entity.SceneId)
                .Append("'")
                .Append(",")
                .Append(entity.EntranceNo)
                .Append(",")
                .Append("'")
                .Append(entity.AssetBandlesName)
                .Append("'")
                .Append(",")
                .Append("'")
                .Append(entity.AssetName)
                .Append("'")
                .Append(",")
                .Append("'")
                .Append(entity.ObjectName)
                .Append("'")
                .Append(",")
                .Append("'")
                .Append(entity.PositionX)
                .Append("'")
                .Append(",")
                .Append("'")
                .Append(entity.PositionY)
                .Append("'")
                .Append(",")
                .Append(entity.Direction)
                .Append(",")
                .Append(entity.Procedure)
                .Append(");");
            DbManager.ExecuteNonQuery(sb.ToString());
        }

        public static void Update(LocationEntity entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE LOCATION SET ")
                .Append("LOCATION_ID = ")
                .Append(entity.LocationId)
                .Append(",")
                .Append("SCENE_ID = ")
                .Append("'")
                .Append(entity.SceneId)
                .Append("'")
                .Append(",")
                .Append("ENTRANCE_NO = ")
                .Append(entity.EntranceNo)
                .Append(",")
                .Append("ASSET_BANDLES_NAME = ")
                .Append("'")
                .Append(entity.AssetBandlesName)
                .Append("'")
                .Append(",")
                .Append("ASSET_NAME = ")
                .Append("'")
                .Append(entity.AssetName)
                .Append("'")
                .Append(",")
                .Append("OBJECT_NAME = ")
                .Append("'")
                .Append(entity.ObjectName)
                .Append("'")
                .Append(",")
                .Append("POSITION_X = ")
                .Append("'")
                .Append(entity.PositionX)
                .Append("'")
                .Append(",")
                .Append("POSITION_Y = ")
                .Append("'")
                .Append(entity.PositionY)
                .Append("'")
                .Append(",")
                .Append("DIRECTION = ")
                .Append(entity.Direction)
                .Append(",")
                .Append("PROCEDURE = ")
                .Append(entity.Procedure)
                .Append(";");
            DbManager.ExecuteNonQuery(sb.ToString());
        }

        private static LocationEntity CreateEntity(DataRow row)
        {
            LocationEntity entity = new LocationEntity();

            entity.LocationId = DaoSupport.GetIntValue(row, "LOCATION_ID");

            entity.SceneId = DaoSupport.GetStringValue(row, "SCENE_ID");

            entity.EntranceNo = DaoSupport.GetIntValue(row, "ENTRANCE_NO");

            entity.AssetBandlesName = DaoSupport.GetStringValue(row, "ASSET_BANDLES_NAME");

            entity.AssetName = DaoSupport.GetStringValue(row, "ASSET_NAME");

            entity.ObjectName = DaoSupport.GetStringValue(row, "OBJECT_NAME");

            entity.PositionX = DaoSupport.GetStringValue(row, "POSITION_X");

            entity.PositionY = DaoSupport.GetStringValue(row, "POSITION_Y");

            entity.Direction = DaoSupport.GetIntValue(row, "DIRECTION");

            entity.Procedure = DaoSupport.GetIntValue(row, "PROCEDURE");

            return entity;
        }
    }
}