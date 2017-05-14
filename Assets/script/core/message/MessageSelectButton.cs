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
            Destroy(selectMessageDialogObj);
            systemObj.SendMessage("SelectAButton");
        }

        public void OnBClick()
        {
            Destroy(selectMessageDialogObj);
            systemObj.SendMessage("SelectBButton");
        }

        public void OnCClick()
        {
            Destroy(selectMessageDialogObj);
            systemObj.SendMessage("SelectCButton");
        }

        public void OnDClick()
        {
            Destroy(selectMessageDialogObj);
            systemObj.SendMessage("SelectDButton");
        }

        public void OnEClick()
        {
            Destroy(selectMessageDialogObj);
            systemObj.SendMessage("SelectEButton");
        }
    }
}
