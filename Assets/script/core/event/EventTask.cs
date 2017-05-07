using System.Collections.Generic;
using Assets.script.common.dao;
using Assets.script.common.entity;
using Assets.script.core.audio;
using Assets.script.core.message;

namespace Assets.script.core.@event
{
    public class EventTask
    {
        public bool StopFlg { get; set; }
        public bool TaskEndFlg { get; set; }
        public float SleepTime { get; set; }
        public int CurrentIndex { get; set; }

        string eventName;
        readonly List<EventDetailEntity> eventDetailList;

        public EventTask(List<EventDetailEntity> eventDetailList)
        {
            this.eventDetailList = eventDetailList;
        }

        public void Execute()
        {
            for (var i = CurrentIndex; i < eventDetailList.Count; i++)
            {
                if (StopFlg)
                {
                    break;
                }
                var task = eventDetailList[i];

                switch (task.TypeId)
                {
                    // bgmの場合
                    case 1:
                        if (task.Attr1 == "start")
                        {
                            AudioManager.Instance.PlayBgm(task.Attr2,
                                float.Parse(MusicDao.SelectByPrimaryKey(int.Parse(task.Attr2)).Time));
                        }
                        else
                        {
                            AudioManager.Instance.StopBgm();
                        }
                        break;
                    // seの場合
                    case 2:
                        AudioManager.Instance.PlaySe(task.Attr1);
                        break;
                    // msgの場合
                    case 3:
                        if (task.Attr1 == "del")
                        {
                            MessageManager.Instance.Hide();
                        }
                        else
                        {
                            StopFlg = true;
                            EventDetailEntity nextTask = eventDetailList[CurrentIndex + 1];
                            bool lastMsgFlg = nextTask.Attr1 == "del";
                            MessageManager.Instance.ChangeMessage(task.Attr1, task.Attr2,
                                lastMsgFlg,
                                false);
                        }
                        break;
                    // msg_autoの場合
                    case 4:
                        MessageManager.Instance.ChangeMessage(task.Attr1, task.Attr2, true,
                            true);
                        break;
                    // msg_selectの場合
                    case 5:
                        StopFlg = true;
                        MessageManager.Instance.CreateSelectMessageDialog(int.Parse(task.Attr1), task.Attr2);
                        break;
                    // actionの場合
                    case 6:
                        StopFlg = true;
                        EventManager.Instance.gameObject.SendMessage("Action" + task.Attr1);
                        break;
                    // sleepの場合
                    case 7:
                        StopFlg = true;
                        SleepTime = float.Parse(task.Attr1);
                        break;
                    // endの場合
                    case 8:
                        CurrentIndex = 0;
                        TaskEndFlg = true;
                        break;
                }

                CurrentIndex++;
            }
        }

        public void Clear()
        {
            StopFlg = false;
            TaskEndFlg = false;
            SleepTime = 0.0f;
            CurrentIndex = 0;
        }
    }
}
