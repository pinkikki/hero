using Assets.script.common.dao;
using Assets.script.core.scene;

namespace Assets.script.core.@event
{
    public class ManualEventManager : EventManager
    {
        protected override void Load()
        {
            var entityList = ManualEventDao.SelectBySceneIdAndProcedure(SceneStatus.SceneId, SceneStatus.Procedure);

            entityList.ForEach(e => eventDct[e.EventId] = new EventTask(EventDetailDao.SelectByEventId(e.EventId)));

            eventMode = Mode.WaitTrigger;
        }

        protected override void OnWaitEvent()
        {
            if (eventDct[currentEvent].TaskEndFlg)
            {
                currentEvent = 0;
                eventMode = Mode.WaitTrigger;
            }
            else if (!eventDct[currentEvent].StopFlg)
            {
                eventMode = Mode.ExecuteEvent;
            }
        }
    }
}
