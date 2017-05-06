using System;
using System.Collections.Generic;
using Assets.script.core.audio;
using Assets.script.core.monoBehaviour;
using UnityEngine;

namespace Assets.script.core.@event
{
    public abstract class EventManager : SingletonMonoBehaviour<EventManager>
    {
        protected Dictionary<int, EventTask> eventDct = new Dictionary<int, EventTask>();
        protected List<int> eventList;
        protected int currentEvent;
        protected Mode eventMode;

        protected enum Mode
        {
            Loading,
            WaitTrigger,
            ExecuteEvent,
            WaitEvent,
            End
        }

        protected float time;

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
            eventList.Add(eventId);
        }

        public void NextTask()
        {
            eventDct[currentEvent].StopFlg = false;
        }

        public void SkipEvent(int skipNum)
        {
            eventDct[currentEvent].CurrentIndex += skipNum;
        }

        protected abstract void Load();

        protected virtual void OnWaitTrigger()
        {
            // TODO 元のソースから条件を再取得すること！！！！
            if ((!AudioManager.Exist() && !AudioManager.Instance.IsLoadComplete())
//                        ||
//                        (ciInstance != null && !ciInstance.isLoadComplete()) ||
//                        (oiInstance != null && !oiInstance.isLoadComplete())
            ) return;
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
                currentEvent = 0;
                eventList.RemoveAt(0);
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
