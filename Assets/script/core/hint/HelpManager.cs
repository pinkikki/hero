using script.core.character;
using script.core.operation;
using script.core.quiz;
using UnityEngine;
using UnityEngine.Events;

namespace script.core.hint
{
    public class HelpManager : AdsManager<HelpManager>
    {
        [SerializeField] GameObject baseButton;
        [SerializeField] GameObject contentBoxA;

        [Header("OnFinished Callback")] public UnityEvent OnFinishedAds;

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

            contentBoxA.SetActive(false);
        }

        void Update()
        {
        }

        protected override void OnFinished()
        {
            OnFinishedAds.Invoke();
            gameObject.SetActive(false);

            SearchButton.Instance.Show();
            QuizManager.Instance.Show();
            var yusuke = GameObject.Find("yusuke");
            if (yusuke != null)
            {
                var mainCharacterController = yusuke.GetComponent<MainCharacterController>();
                if (mainCharacterController != null)
                {
                    mainCharacterController.FreezeFlg = false;
                }
            }
        }

        protected override void OnSkipped()
        {
            ClickBaseButton();
        }

        protected override void OnFailed()
        {
            // nop
        }

        public void ClickBaseButton()
        {
            baseButton.SetActive(false);
            contentBoxA.SetActive(true);
            SearchButton.Instance.Hide();
            QuizManager.Instance.Hide();


            var yusuke = GameObject.Find("yusuke");
            if (yusuke != null)
            {
                var mainCharacterController = yusuke.GetComponent<MainCharacterController>();
                if (mainCharacterController != null)
                {
                    mainCharacterController.FreezeFlg = true;
                }
            }
            PlaySe();
        }

        public void ClickGreenButtonContentBox()
        {
            baseButton.SetActive(true);
            contentBoxA.SetActive(false);
            SearchButton.Instance.Show();
            QuizManager.Instance.Show();

            var yusuke = GameObject.Find("yusuke");
            if (yusuke != null)
            {
                var mainCharacterController = yusuke.GetComponent<MainCharacterController>();
                if (mainCharacterController != null)
                {
                    mainCharacterController.FreezeFlg = false;
                }
            }
            PlaySe();
        }

        public void ClickYellowButtonContentBox()
        {
            baseButton.SetActive(false);
            contentBoxA.SetActive(false);
            ShowUnityAds();
            PlaySe();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}