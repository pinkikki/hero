using System.Collections.Generic;
using System.Linq;
using script.common.dao;
using script.core.scene;

namespace script.core.@event
{
    public class BasicEventLoader : IEventLoader
    {
        Dictionary<string, int> objectMappingDic;

        public EventHolder Load()
        {
            var entityList = EventDao.SelectBySceneIdAndProcedure(SceneStatus.SceneId, SceneStatus.Procedure);
            entityList.Add(EventDao.SelectByPrimaryKey(99999));

            var holder = new EventHolder
            {
                ObjectMappingDic = entityList.ToDictionary(e => e.ObjectName, e => e.EventId),
                EventDic = entityList.ToDictionary(e => e.EventId,
                    e => new EventTask(EventDetailDao.SelectByEventId(e.EventId)))
            };
            return holder;

        }
    }
}
