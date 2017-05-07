using System.Linq;
using Assets.script.core.asset;
using Assets.script.core.monoBehaviour;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.script.core.message
{
    public class MessageManager : SingletonMonoBehaviour<MessageManager>
    {
        public bool AutoFlg { get; private set; }

        Text contentText;
        Text titleText;
        GameObject nextButton;


        void Awake()
        {
            DontDestroyOnLoad(this);
        }

        void Start()
        {
            titleText = transform.FindChild("Body/TitleBox/TitleText").GetComponent<Text>();
            contentText = transform.FindChild("Body/ContentBox/ContentText").GetComponent<Text>();
            nextButton = transform.FindChild("Body/NextButton").gameObject;
            // 非アクティブ状態だとインスタンスを取得できなくなるので、ここで取得しておく
            var msg = Instance;
            gameObject.SetActive(false);
        }

        void Update()
        {
        }

        public void ChangeMessage(string titleMessage, string contentMessage, bool lastMsgFlg, bool autoFlg)
        {
            AutoFlg = autoFlg;
            if (!gameObject.activeSelf)
            {
                Show();
            }

            nextButton.SetActive(!lastMsgFlg);
            titleText.text = titleMessage;
            contentText.text = contentMessage;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            titleText.text = "";
            contentText.text = "";
            gameObject.SetActive(false);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void CreateSelectMessageDialog(int selectNum, string message)
        {
            var selectMsgDialog = (GameObject) Instantiate(
                AssetLoader.Instance.LoadPrefab("prefab/common/", "SelectMsgDialog"), new Vector2(0.0f, 0.0f),
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
                    AssetLoader.Instance.LoadPrefab("prefab/common/", "ButtonBase"), new Vector2(0.0f, 0.0f),
                    Quaternion.identity);
                var text = buttonBase.transform.FindChild("ButtonText").GetComponent<Text>();
                var button = buttonBase.GetComponent<Button>();
                switch (i)
                {
                    case 0:
                        text.text = "Ａ";
                        button.onClick.AddListener(script.OnAClick);
                        break;
                    case 1:
                        text.text = "Ｂ";
                        button.onClick.AddListener(script.OnBClick);
                        break;
                    case 2:
                        text.text = "Ｃ";
                        button.onClick.AddListener(script.OnCClick);
                        break;
                    case 3:
                        text.text = "Ｄ";
                        button.onClick.AddListener(script.OnDClick);
                        break;
                    case 4:
                        text.text = "Ｅ";
                        button.onClick.AddListener(script.OnEClick);
                        break;
                }

                buttonBase.transform.SetParent(obj.transform);
            }
        }
    }
}
