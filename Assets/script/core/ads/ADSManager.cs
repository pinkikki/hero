using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

namespace script.core.ads
{
    public class ADSManager : MonoBehaviour
    {
        [SerializeField] string zoneID = "rewardedVideo";
        [SerializeField] string gameID_iOS = "";
        [SerializeField] string gameID_Android = "";

        [Header("OnFinished Callback")] public UnityEvent OnFinishedAds;
        [Header("OnSkipped Callback")] public UnityEvent OnSkippedAds;
        [Header("OnFailed Callback")] public UnityEvent OnFailedAds;


        void Start()
        {
            if (Advertisement.isSupported && !Advertisement.isInitialized)
            {
#if UNITY_ANDROID
                Advertisement.Initialize(gameID_Android);
#elif UNITY_IOS
			    Advertisement.Initialize(gameID_iOS);
#endif
            }
        }

        void Update()
        {
        }

        public void ShowUnityAds()
        {
            if (Advertisement.IsReady(zoneID))
            {
                var options = new ShowOptions {resultCallback = HandleShowResult};
                Advertisement.Show(zoneID, options);
            }
        }

        private void HandleShowResult(ShowResult result)
        {
            switch (result)
            {
                case ShowResult.Finished:
                    Debug.Log("The ad was successfully shown.");
                    OnFinished();
                    break;
                case ShowResult.Skipped:
                    Debug.Log("The ad was skipped before reaching the end.");
                    OnSkipped();
                    break;
                case ShowResult.Failed:
                    Debug.LogError("The ad failed to be shown.");
                    OnFailed();
                    break;
            }
        }

        void OnFinished()
        {
            OnFinishedAds.Invoke();
        }

        void OnSkipped()
        {
            OnSkippedAds.Invoke();
        }

        void OnFailed()
        {
            OnFailedAds.Invoke();
        }
    }
}