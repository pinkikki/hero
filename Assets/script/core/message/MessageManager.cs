using System.Linq;
using script.core.assetbandle;
using script.core.monoBehaviour;
using UnityEngine;
using UnityEngine.UI;

namespace script.core.message
{
    public class MessageManager : SingletonMonoBehaviour<MessageManager>
    {
        public bool AutoFlg { get; private set; }
        public bool ManualFlg { get; private set; }

        GameObject msgDialog;
        Text contentText;
        Text titleText;
        GameObject nextButton;


        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        void Start()
        {
            msgDialog = (GameObject) Instantiate(LoadAssetBandles.Instance.LoadPrefab("prefab/msgdialog", "MsgDialog"),
                new Vector2(0, 0), Quaternion.identity);
            titleText = msgDialog.transform.FindChild("Body/TitleBox/TitleText").GetComponent<Text>();
            contentText = msgDialog.transform.FindChild("Body/ContentBox/ContentText").GetComponent<Text>();
            nextButton = msgDialog.transform.FindChild("Body/NextButton").gameObject;
            msgDialog.SetActive(false);
        }

        void Update()
        {
        }

        public void ChangeMessage(string titleMessage, string contentMessage, bool lastMsgFlg, bool autoFlg)
        {
            AutoFlg = autoFlg;
            ManualFlg = false;
            if (!msgDialog.activeSelf)
            {
                Show();
            }

            nextButton.SetActive(!lastMsgFlg);
            titleText.text = titleMessage;
            contentText.text = contentMessage;
        }

        public void Show()
        {
            msgDialog.SetActive(true);
        }

        public void Hide()
        {
            titleText.text = "";
            contentText.text = "";
            msgDialog.SetActive(false);
        }

        public void Destroy()
        {
            Destroy(msgDialog);
        }

        public void CreateSelectMessageDialog(int selectNum, string message)
        {
            var selectMsgDialog = (GameObject) Instantiate(
                LoadAssetBandles.Instance.LoadPrefab("prefab/msgdialog", "SelectMsgDialog"), new Vector2(0.0f, 0.0f),
                Quaternion.identity);
            selectMsgDialog.name = "SelectMsgDialog";
            var msgText = selectMsgDialog.transform.FindChild("Body/ContentBox/ContentText").GetComponent<Text>();
            msgText.text = message;
            CreateSelectMessageButton(selectMsgDialog.GetComponent<MessageSelectButton>(),
                selectMsgDialog.transform.FindChild("Body/SelectButtons").gameObject, selectNum);
        }

        void CreateSelectMessageButton(MessageSelectButton script, GameObject obj, int selectNum)
        {
            foreach (var i in Enumerable.Range(0, selectNum))
            {
                var buttonBase = (GameObject) Instantiate(
                    LoadAssetBandles.Instance.LoadPrefab("prefab/msgdialog", "ButtonBase"), new Vector2(0.0f, 0.0f),
                    Quaternion.identity);
                var text = buttonBase.transform.FindChild("Text").GetComponent<Text>();
                var button = buttonBase.GetComponent<Button>();
                switch (i)
                {
                    case 0:
                        text.text = "A";
                        button.onClick.AddListener(script.OnAClick);
                        break;
                    case 1:
                        text.text = "B";
                        button.onClick.AddListener(script.OnBClick);
                        break;
                    case 2:
                        text.text = "C";
                        button.onClick.AddListener(script.OnCClick);
                        break;
                    case 3:
                        text.text = "D";
                        button.onClick.AddListener(script.OnDClick);
                        break;
                    case 4:
                        text.text = "E";
                        button.onClick.AddListener(script.OnEClick);
                        break;
                }

                buttonBase.transform.SetParent(obj.transform);
            }
        }
    }
}
