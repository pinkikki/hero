using System;
using System.Collections.Generic;
using script.common.entity;
using UnityEngine;

namespace script.core.@event
{
    public class EventTask
    {
        bool stopFlg;

        public bool StopFlg
        {
            get { return stopFlg; }
            set { stopFlg = value; }
        }

        bool taskEndFlg;

        public bool TaskEndFlg
        {
            get { return taskEndFlg; }
            set { taskEndFlg = value; }
        }

        float sleepTime;


        public float SleepTime
        {
            get { return sleepTime; }
            set { sleepTime = value; }
        }

        int currentIndex;


        public int CurrentIndex
        {
            get { return currentIndex; }
            set { currentIndex = value; }
        }

        String eventName;
        List<EventDetailEntity> eventDetailList;

        public EventTask(List<EventDetailEntity> eventDetailList)
        {
            this.eventDetailList = eventDetailList;
        }

        public void Execute()
        {
            for (int i = currentIndex; i < eventDetailList.Count; i++)
            {
                if (stopFlg)
                {
                    break;
                }
                EventDetailEntity task = eventDetailList[i];

                switch (task.TypeId)
                {
                    // bgmの場合
                    case 1:
                        if (task.Attr1 == "start")
                        {
                            amInstance.playBgm(task.Attr2,
                                float.Parse(MusicDao.SelectByPrimaryKey(int.Parse(task.Attr2)).Time));
                        }
                        else
                        {
                            amInstance.stopBgm();
                        }
                        break;
                    // seの場合
                    case 2:
                        amInstance.playSe(task.Attr1);
                        break;
                    // msgの場合
                    case 3:
                        if (task.Attr1 == "del")
                        {
                            mmInstance.hide();
                        }
                        else
                        {
                            stopFlg = true;
                            EventDetailEntity nextTask = eventDetailList[currentIndex + 1];
                            bool lastMsgFlg = nextTask.Attr1 == "del";
                            mmInstance.changeMessage(nmInstance.convert(task.Attr1), nmInstance.convert(task.Attr2),
                                lastMsgFlg,
                                false);
                        }
                        break;
                    // msg_autoの場合
                    case 4:
                        mmInstance.changeMessage(nmInstance.convert(task.Attr1), nmInstance.convert(task.Attr2), true,
                            true);
                        break;
                    // msg_selectの場合
                    case 5:
                        stopFlg = true;
                        mmInstance.createSelectMessageDialog(task);
                        break;
                    // actionの場合
                    case 6:
                        stopFlg = true;
                        mmInstance.gameObject.SendMessage(task.TypeId + task.Attr1);
                        break;
                    // sleepの場合
                    case 7:
                        stopFlg = true;
                        sleepTime = float.Parse(task.Attr1);
                        break;
                    // endの場合
                    case 8:
                        currentIndex = 0;
                        taskEndFlg = true;
                        break;
                }

                currentIndex++;
            }
        }
    }
}
