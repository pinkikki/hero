using script.common.dao;
using script.common.entity;
using script.core.audio;
using script.core.@event;
using UnityEngine;

namespace script.core.message
{
    public class MessageButton : MonoBehaviour {

        MusicEntity entity;
        
        void Start () {
	
        }
	
        void Update () {
	
        }

        public void OnClick() {
            if (!MessageManager.Instance.AutoFlg)
            {
                if (entity == null)
                {
                    entity = MusicDao.SelectByPrimaryKey(7);
                }
                AudioManager.Instance.PlaySe(entity.MusicName);
                EventManager.Instance.NextTask();
            }
        }
    }
}
