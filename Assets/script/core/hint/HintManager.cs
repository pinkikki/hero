using script.core.character;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.core.hint
{
    public class HintManager : AdsManager<HintManager>
    {
        [SerializeField] GameObject baseButton;
        [SerializeField] GameObject contentBoxA;
        [SerializeField] GameObject contentBoxB;
        [SerializeField] Button yellowButtonOfcontentBoxA;
        [SerializeField] Button yellowButtonOfcontentBoxB;
        [SerializeField] Text yellowButtonTextOfcontentBoxA;
        [SerializeField] Text yellowButtonTextOfcontentBoxB;
        [SerializeField] Text contentOfcontentBoxB;

        [Header("OnFinished Callback")] public UnityEvent OnFinishedAds;
        [Header("OnSkipped Callback")] public UnityEvent OnSkippedAds;
        [Header("OnFailed Callback")] public UnityEvent OnFailedAds;


        void Start()
        {
            var obj = Instance;
            Initialize();

            if (baseButton == null)
            {
                baseButton = gameObject.transform.Find("BaseButton").gameObject;
            }

            if (contentBoxA == null)
            {
                contentBoxA = gameObject.transform.Find("ContentBoxA").gameObject;
            }

            if (contentBoxB == null)
            {
                contentBoxB = gameObject.transform.Find("ContentBoxB").gameObject;
            }

            if (yellowButtonOfcontentBoxA == null)
            {
                yellowButtonOfcontentBoxA = gameObject.transform.Find("ContentBoxA/YellowButton")
                    .GetComponent<Button>();
            }

            if (yellowButtonOfcontentBoxB == null)
            {
                yellowButtonOfcontentBoxB = gameObject.transform.Find("ContentBoxB/YellowButton")
                    .GetComponent<Button>();
            }
            
            if (yellowButtonTextOfcontentBoxA == null)
            {
                yellowButtonTextOfcontentBoxA = gameObject.transform.Find("ContentBoxA/YellowButton/ContentText")
                    .GetComponent<Text>();
            }

            if (yellowButtonTextOfcontentBoxB == null)
            {
                yellowButtonTextOfcontentBoxB = gameObject.transform.Find("ContentBoxB/YellowButton/ContentText")
                    .GetComponent<Text>();
            }

            if (contentOfcontentBoxB == null)
            {
                contentOfcontentBoxB = gameObject.transform.Find("ContentBoxB/Content").GetComponent<Text>();
            }

            contentBoxA.SetActive(false);
            contentBoxB.SetActive(false);
        }

        void Update()
        {
        }

        protected override void OnFinished()
        {
            OnFinishedAds.Invoke();
            baseButton.SetActive(false);
            contentBoxA.SetActive(false);
            contentBoxB.SetActive(true);
            contentOfcontentBoxB.text = HintRepository.Instance.GetNextHint();
            if (HintRepository.Instance.HasNext())
            {
                yellowButtonTextOfcontentBoxB.text = "動画を見て次のヒントをもらう";
            }
            else
            {
                yellowButtonTextOfcontentBoxB.text = "今見れるヒントはもうないよ";
                yellowButtonOfcontentBoxB.interactable = false;
            }
        }

        protected override void OnSkipped()
        {
            OnSkippedAds.Invoke();
            ClickBaseButton();
        }

        protected override void OnFailed()
        {
            OnFailedAds.Invoke();
        }

        public void ClickBaseButton()
        {
            baseButton.SetActive(false);
            contentBoxA.SetActive(true);
            contentBoxB.SetActive(false);
            var yusuke = GameObject.Find("yusuke");
            if (yusuke != null)
            {
                var mainCharacterController = yusuke.GetComponent<MainCharacterController>();
                if (mainCharacterController != null)
                {
                    mainCharacterController.FreezeFlg = true;
                }
            }

            if (HintRepository.Instance.HasNext())
            {
                yellowButtonTextOfcontentBoxA.text = "動画を見てヒントをもらう";
            }
            else
            {
                yellowButtonTextOfcontentBoxA.text = "今見れるヒントはもうないよ";
                yellowButtonOfcontentBoxA.interactable = false;
            }
        }

        public void ClickGreenButtonContentBox()
        {
            baseButton.SetActive(true);
            contentBoxA.SetActive(false);
            contentBoxB.SetActive(false);
            var yusuke = GameObject.Find("yusuke");
            if (yusuke != null)
            {
                var mainCharacterController = yusuke.GetComponent<MainCharacterController>();
                if (mainCharacterController != null)
                {
                    mainCharacterController.FreezeFlg = false;
                }
            }

            yellowButtonOfcontentBoxA.interactable = true;
            yellowButtonOfcontentBoxB.interactable = true;
        }

        public void ClickYellowButtonContentBox()
        {
            baseButton.SetActive(false);
            contentBoxA.SetActive(false);
            contentBoxB.SetActive(false);
            ShowUnityAds();
        }
    }
}