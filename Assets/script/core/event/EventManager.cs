using System;
using System.Collections.Generic;
using script.core.monoBehaviour;
using UnityEngine;

namespace script.core.@event
{
    public class EventManager: SingletonMonoBehaviour<EventManager>
    {
        [SerializeField] private IEventLoader loader = new BasicEventLoader();
        Dictionary<int, EventTask> eventDct;
        Dictionary<string, int> objectMappingDic;
        readonly List<int> eventList = new List<int>();
        readonly HashSet<int> completeEventSet = new HashSet<int>();

        public HashSet<int> CompleteEventSet
        {
            get { return completeEventSet; }
        }

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
                    OnWaitTrigger();
                    break;
                case Mode.ExecuteEvent:
                    OnExecuteEvent();
                    break;
                case Mode.WaitEvent:
                    OnWaitEvent();
                    break;
                case Mode.Loading:
                    break;
                case Mode.End:
                    OnEnd();
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

        protected void Execute()
        {
            eventDct[currentEvent].Execute();
        }

        public void Register(int eventId)
        {
            if (eventMode == Mode.WaitTrigger || eventMode == Mode.Loading)
            {
                eventList.Add(eventId);
            }
        }

        public void Register(string objectName)
        {
            Register(objectMappingDic[objectName]);
        }

        public void RegisterByForce(int eventId)
        {
            eventList.Add(eventId);
        }

        public void RegisterByForce(string objectName)
        {
            RegisterByForce(objectMappingDic[objectName]);
        }

        public void NextTask()
        {
            eventDct[currentEvent].StopFlg = false;
        }

        public void SkipEvent(int skipNum)
        {
            eventDct[currentEvent].CurrentIndex += skipNum;
        }

        protected void Load()
        {
            EventHolder holder = loader.Load();
            eventDct = holder.EventDic;
            objectMappingDic = holder.ObjectMappingDic;
            eventMode = Mode.WaitTrigger;
        }

        protected virtual void OnWaitTrigger()
        {
            // TODO 元のソースから条件を再取得すること！！！！
//            if ((!AudioManager.Exist() && !AudioManager.Instance.IsLoadComplete())
//                        ||
//                        (ciInstance != null && !ciInstance.isLoadComplete()) ||
//                        (oiInstance != null && !oiInstance.isLoadComplete())
//            ) return;
            if (eventList.Count <= 0) return;
            eventMode = Mode.ExecuteEvent;
            currentEvent = eventList[0];
        }

        protected virtual void OnExecuteEvent()
        {
            Execute();
            eventMode = Mode.WaitEvent;
        }

        protected virtual void OnWaitEvent()
        {
            if (eventDct[currentEvent].TaskEndFlg)
            {
                completeEventSet.Add(currentEvent);
                eventList.RemoveAt(0);
                eventDct[currentEvent].Clear();
                currentEvent = 0;
                eventMode = Mode.WaitTrigger;
            }
            else if (!eventDct[currentEvent].StopFlg)
            {
                eventMode = Mode.ExecuteEvent;
            }
        }

        protected virtual void OnEnd()
        {
            // 処理なし
        }
    }
}
