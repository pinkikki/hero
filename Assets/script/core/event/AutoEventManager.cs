using Assets.script.common.dao;
using Assets.script.core.scene;

namespace Assets.script.core.@event
{
    public class AutoEventManager : EventManager
    {
        protected override void Load()
        {
            var entityList = AutoEventDao.SelectBySceneId(SceneStatus.SceneId, SceneStatus.Procedure);

            entityList.ForEach(e => eventDct[e.EventId] = new EventTask(EventDetailDao.SelectByEventId(e.EventId)));

            eventMode = Mode.WaitTrigger;
        }
    }
}
