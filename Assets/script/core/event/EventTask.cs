using System.Collections.Generic;
using script.common.dao;
using script.common.entity;
using script.core.audio;

namespace script.core.@event
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
                            mmInstance.hide();
                        }
                        else
                        {
                            StopFlg = true;
                            EventDetailEntity nextTask = eventDetailList[CurrentIndex + 1];
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
                        StopFlg = true;
                        mmInstance.createSelectMessageDialog(task);
                        break;
                    // actionの場合
                    case 6:
                        StopFlg = true;
                        mmInstance.gameObject.SendMessage(task.TypeId + task.Attr1);
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
    }
}
