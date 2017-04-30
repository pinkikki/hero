using System;
using System.Collections.Generic;
using System.Linq;
using script.common.dao;
using script.core.audio;
using script.core.monoBehaviour;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace script.core.@event
{
    public class EventManager : SingletonMonoBehaviour<EventManager>
    {
        List<int> eventList;
        Dictionary<int, EventTask> eventDct = new Dictionary<int, EventTask>();
        int currentEvent;
        Mode eventMode;

        enum Mode
        {
            Loading,
            WaitTrigger,
            ExecuteEvent,
            WaitEvent,
            End
        }

        float time;

        void Start()
        {
            eventMode = Mode.Loading;
            Load();
        }

        void Update()
        {
            switch (eventMode)
            {
                case Mode.WaitTrigger:
                    // TODO 元のソースから条件を再取得すること！！！！
                    if ((!AudioManager.Exist() && !AudioManager.Instance.IsLoadComplete())
//                        ||
//                        (ciInstance != null && !ciInstance.isLoadComplete()) ||
//                        (oiInstance != null && !oiInstance.isLoadComplete())
                    ) return;
                    if (eventList.Count <= 0) return;
                    eventMode = Mode.ExecuteEvent;
                    currentEvent = eventList[0];
                    break;
                case Mode.ExecuteEvent:
                    Execute();
                    eventMode = Mode.WaitEvent;
                    break;
                case Mode.WaitEvent:
                    if (eventDct[currentEvent].TaskEndFlg)
                    {
                        currentEvent = 0;
                        eventList.RemoveAt(0);
                        eventMode = Mode.WaitTrigger;
                    }
                    else if (!eventDct[currentEvent].StopFlg)
                    {
                        eventMode = Mode.ExecuteEvent;
                    }
                    break;
                case Mode.Loading:
                    break;
                case Mode.End:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void FixedUpdate()
        {
            if (Mode.WaitEvent != eventMode) return;
            if (!eventDct[currentEvent].StopFlg) return;
            var sleepTime = eventDct[currentEvent].SleepTime;
            if (!(0.0f < sleepTime)) return;
            time += Time.deltaTime;
            if (!(sleepTime < time)) return;
            time = 0.0f;
            eventDct[currentEvent].SleepTime = 0.0f;
            eventDct[currentEvent].StopFlg = false;
        }

        void Load()
        {
            var sceneId = SceneManager.GetActiveScene().name;
            var entityList = EventDao.SelectBySceneId(sceneId);

            entityList.ForEach(e => eventDct[e.EventId] = new EventTask(EventDetailDao.SelectByEventId(e.EventId)));
            eventList = entityList.Select(e => e.EventId).ToList();

            eventMode = Mode.WaitTrigger;
        }

        void Execute()
        {
            eventDct[currentEvent].Execute();
        }

        public void NextTask()
        {
            eventDct[currentEvent].StopFlg = false;
        }

        public void SkipEvent(int skipNum)
        {
            eventDct[currentEvent].CurrentIndex += skipNum;
        }
    }
}
