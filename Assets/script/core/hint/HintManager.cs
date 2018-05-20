using script.core.character;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.core.hint
{
	public class HintManager : MonoBehaviour {

		[SerializeField] string zoneID = "rewardedVideo";
		[SerializeField] string gameID_iOS = "";
		[SerializeField] string gameID_Android = "";

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
			if (Advertisement.isSupported && !Advertisement.isInitialized)
			{
#if UNITY_ANDROID
				Advertisement.Initialize(gameID_Android);
#elif UNITY_IOS
			    Advertisement.Initialize(gameID_iOS);
#endif
			}

			if (baseButton == null)
			{
				baseButton = gameObject.transform.Find("Body/BaseButton").gameObject;
			}
			if (contentBoxA == null)
			{
				contentBoxA = gameObject.transform.Find("Body/ContentBoxA").gameObject;
			}
			if (contentBoxB == null)
			{
				contentBoxB = gameObject.transform.Find("Body/ContentBoxB").gameObject;
			}
			if (yellowButtonTextOfcontentBoxA == null)
			{
				yellowButtonTextOfcontentBoxA = gameObject.transform.Find("Body/ContentBoxA/YellowButton/ContentText").GetComponent<Text>();
			}
			if (yellowButtonTextOfcontentBoxB == null)
			{
				yellowButtonTextOfcontentBoxB = gameObject.transform.Find("Body/ContentBoxB/YellowButton/ContentText").GetComponent<Text>();
			}
			if (contentOfcontentBoxB == null)
			{
				contentOfcontentBoxB = gameObject.transform.Find("Body/ContentBoxB/Content").GetComponent<Text>();
			}

			contentBoxA.SetActive(false);
			contentBoxB.SetActive(false);
			
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

		void OnSkipped()
		{
			OnSkippedAds.Invoke();
			ClickBaseButton();
		}

		void OnFailed()
		{
			OnFailedAds.Invoke();
		}

		public void ClickBaseButton()
		{
			baseButton.SetActive(false);
			contentBoxA.SetActive(true);
			contentBoxB.SetActive(false);
			GameObject.Find("yusuke").GetComponent<MainCharacterController>().FreezeFlg = true;
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
			GameObject.Find("yusuke").GetComponent<MainCharacterController>().FreezeFlg = false;
			yellowButtonOfcontentBoxA.interactable = true;
			yellowButtonOfcontentBoxB.interactable = true;


		}
		
		public void ClickYellowButtonContentBox()
		{
			baseButton.SetActive(false);
			contentBoxA.SetActive(false);
			contentBoxB.SetActive(false);
			ShowUnityAds();
			Debug.Log("kita");
		}
		
	}
}
