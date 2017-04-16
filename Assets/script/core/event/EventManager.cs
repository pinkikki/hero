using System;
using System.Collections.Generic;
using System.Linq;
using script.common.entity;
using script.core.monoBehaviour;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace script.core.@event
{
    public class EventManager : SingletonMonoBehaviour<EventManager> {

        [SerializeField]
        String eventId;
        List<int> eventList;
        Dictionary<int, EventTask> eventDct = new Dictionary<int, EventTask>();
        int currentEvent;
        Mode eventMode;
        enum Mode {
            Loading,
            WaitTrigger,
            ExecuteEvent,
            WaitEvent,
            End
        }
        float time;
        void Start () {
            eventMode = Mode.Loading;
            Load();
        }

        void Update () {
            if(Mode.WaitTrigger == eventMode) {
                if ((amInstance == null || amInstance.isLoadComplete()) &&
                    (ciInstance == null || ciInstance.isLoadComplete()) &&
                    (oiInstance == null || oiInstance.isLoadComplete())) {
                    if(eventList.Count > 0) {
                        eventMode = Mode.ExecuteEvent;
                        currentEvent = eventList[0];
                    }
                }

            } else if(Mode.ExecuteEvent == eventMode) {
                Execute();
                eventMode = Mode.WaitEvent;

            } else if(Mode.WaitEvent == eventMode) {
                if(eventDct[currentEvent].TaskEndFlg) {
                    currentEvent = 0;
                    eventList.RemoveAt(0);
                    eventMode = Mode.WaitTrigger;
                } else if(!eventDct[currentEvent].StopFlg) {
                    eventMode = Mode.ExecuteEvent;
                }
            }
        }

        void FixedUpdate() {
            if(Mode.WaitEvent == eventMode) {
                if(eventDct[currentEvent].StopFlg) {
                    float sleepTime = eventDct[currentEvent].SleepTime;
                    if (0.0f < sleepTime) {
                        time += Time.deltaTime;
                        if (sleepTime < time) {
                            time = 0.0f;
                            eventDct[currentEvent].SleepTime = 0.0f;
                            eventDct[currentEvent].StopFlg = false;
                        }
                    }
                }
            }
        }

        void Load() {

            String sceneId = SceneManager.GetActiveScene().name;
            List<EventEntity> entityList = EventDao.SelectBySceneId(sceneId);

            entityList.ForEach(e => eventDct[e.EventId] = new EventTask(EventDetailDao.SelectByEventId(e.EventId)));
            eventList = entityList.Select(e => e.EventId).ToList();

            eventMode = Mode.WaitTrigger;
        }

        void Execute() {
            eventDct[currentEvent].Execute();
        }

        public void NextTask() {
            eventDct[currentEvent].StopFlg = false;
        }

        public void SkipEvent(int skipNum) {
            eventDct[currentEvent].CurrentIndex += skipNum;
        }
    }
}
