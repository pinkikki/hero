using script.common.dao;
using script.common.entity;
using script.core.audio;
using UnityEngine;

namespace script.core.message
{
    public class MessageSelectButton : MonoBehaviour
    {
        GameObject selectMessageDialogObj;
        GameObject systemObj;
        MusicEntity entity;

        void Start()
        {
            selectMessageDialogObj = GameObject.Find("SelectMsgDialog");
            systemObj = GameObject.Find("System");
        }

        void Update()
        {
        }

        public void OnAClick()
        {
            Destroy(selectMessageDialogObj);
            PlaySe();
            systemObj.SendMessage("SelectAButton");
        }

        public void OnBClick()
        {
            Destroy(selectMessageDialogObj);
            PlaySe();
            systemObj.SendMessage("SelectBButton");
        }

        public void OnCClick()
        {
            Destroy(selectMessageDialogObj);
            PlaySe();
            systemObj.SendMessage("SelectCButton");
        }

        public void OnDClick()
        {
            Destroy(selectMessageDialogObj);
            PlaySe();
            systemObj.SendMessage("SelectDButton");
        }

        public void OnEClick()
        {
            Destroy(selectMessageDialogObj);
            PlaySe();
            systemObj.SendMessage("SelectEButton");
        }

        void PlaySe()
        {
            if (entity == null)
            {
                entity = MusicDao.SelectByPrimaryKey(7);
            }
            AudioManager.Instance.PlaySe(entity.MusicName);
        }
    }
}
