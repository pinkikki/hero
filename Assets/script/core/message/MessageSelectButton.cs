using UnityEngine;

namespace script.core.message
{
    public class MessageSelectButton : MonoBehaviour
    {
        GameObject selectMessageDialogObj;
        GameObject systemObj;

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
            systemObj.SendMessage("SelectAButton");
            Destroy(selectMessageDialogObj);
        }

        public void OnBClick()
        {
            systemObj.SendMessage("SelectBButton");
            Destroy(selectMessageDialogObj);
        }

        public void OnCClick()
        {
            systemObj.SendMessage("SelectCButton");
            Destroy(selectMessageDialogObj);
        }

        public void OnDClick()
        {
            systemObj.SendMessage("SelectDButton");
            Destroy(selectMessageDialogObj);
        }

        public void OnEClick()
        {
            systemObj.SendMessage("SelectEButton");
            Destroy(selectMessageDialogObj);
        }
    }
}
