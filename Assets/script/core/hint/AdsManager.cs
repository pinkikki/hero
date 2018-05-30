using script.common.dao;
using script.common.entity;
using script.core.audio;
using script.core.monoBehaviour;
using UnityEngine;
using UnityEngine.Advertisements;

namespace script.core.hint
{
	public abstract class AdsManager<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour {

		[SerializeField] protected string zoneID = "rewardedVideo";
		[SerializeField] protected string gameID_iOS = "";
		[SerializeField] protected string gameID_Android = "";
		MusicEntity entity;


		protected void Initialize()
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
		
				
		public void ShowUnityAds()
		{
			if (Advertisement.IsReady(zoneID))
			{
				var options = new ShowOptions {resultCallback = HandleShowResult};
				Advertisement.Show(zoneID, options);
			}
			else
			{
				Debug.Log("The ad is not ready");
				OnFinished();
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

		protected abstract void OnFinished();
		protected abstract void OnFailed();
		protected abstract void OnSkipped();
		
		protected void PlaySe()
		{
			if (entity == null)
			{
				entity = MusicDao.SelectByPrimaryKey(7);
			}
			AudioManager.Instance.PlaySe(entity.MusicName);
		}

	}
}
