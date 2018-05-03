using System.Collections.Generic;
using script.common.dao;
using script.common.entity;
using script.core.audio;
using script.core.message;
using script.core.operation;

namespace script.core.@event
{
    public class EventTask
    {
        public bool StopFlg { get; set; }
        public bool TaskEndFlg { get; set; }
        public float SleepTime { get; set; }
        public int CurrentIndex { get; set; }
        public int CurrentTaskTypeId  { get; set; }

        string eventName;
        readonly List<EventDetailEntity> eventDetailList;

        public EventTask(List<EventDetailEntity> eventDetailList)
        {
            this.eventDetailList = eventDetailList;
        }

        public void Execute()
        {
            SearchButton.Instance.OnNop();
            for (var i = CurrentIndex; i < eventDetailList.Count; i++)
            {
                if (StopFlg)
                {
                    break;
                }
                var task = eventDetailList[i];
                CurrentTaskTypeId = task.TypeId;
                MusicEntity entity;
                switch (task.TypeId)
                {
                    // bgmの場合
                    case 1:
                        if (task.Attr1 == "start")
                        {
                            entity = MusicDao.SelectByPrimaryKey(int.Parse(task.Attr2));
                            AudioManager.Instance.PlayBgm(entity.MusicName, float.Parse(entity.Time));
                        }
                        else
                        {
                            AudioManager.Instance.StopBgm();
                        }
                        break;
                    // seの場合
                    case 2:
                        entity = MusicDao.SelectByPrimaryKey(int.Parse(task.Attr1));
                        AudioManager.Instance.PlaySe(entity.MusicName, bool.Parse(task.Attr2));
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
            if (0 < eventDetailList.Count && clearOmittedEventId.Contains(eventDetailList[0].EventId))
            {
                SearchButton.Instance.OnRegister(eventDetailList[0].EventId);
            } else {
                SearchButton.Instance.OnDialog();
            }
        }

        private readonly List<int> clearOmittedEventId = new List<int>
        {
            602, 603, 604, 605, 606, 607, 721, 811, 812, 813, 814, 815, 816, 817, 818, 819, 820
        };
    }
}
