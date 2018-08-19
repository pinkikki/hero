using System.Collections.Generic;
using System.Text;
using Plugins;
using script.common.entity;
using script.core.db;

namespace script.common.dao
{
	public class HintDao {

		public static List<HintEntity> SelectAll()
		{
			List<HintEntity> entityList = new List<HintEntity>();
			StringBuilder sb = new StringBuilder();
			sb.Append("SELECT * FROM HINT;");
			DataTable dataTable = DbManager.Instance.ExecuteQuery(sb.ToString());
			dataTable.Rows.ForEach(r => entityList.Add(CreateEntity(r)));
			return entityList;
		}
	
		private static HintEntity CreateEntity(DataRow row)
		{
			HintEntity entity = new HintEntity();
        
			entity.HintId = DaoSupport.GetIntValue(row, "HINT_ID");
        
			entity.Message= DaoSupport.GetStringValue(row, "MESSAGE");
        
			return entity;
		}
	}
}
