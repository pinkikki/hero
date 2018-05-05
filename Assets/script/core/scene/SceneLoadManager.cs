using System.Collections;
using System.Collections.Generic;
using script.core.asset;
using script.core.audio;
using script.core.character;
using script.core.initialization;
using script.core.monoBehaviour;
using script.core.operation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace script.core.scene
{
	public class SceneLoadManager : SingletonMonoBehaviour<SceneLoadManager>
	{
		GameObject baseLayer;
		readonly float defaultInterval = 1.0f;
		private bool isDuring;

		enum TransType
		{
			Default,
			Loading,
			None
		}

		void Awake()
		{
			DontDestroyOnLoad(this);
		}

		void Destroy()
		{
			if (baseLayer != null)
			{
				Destroy(baseLayer);
			}
		}

		public void FadeIn(float fadeInInterval) {
			StartCoroutine(FadeInScene(fadeInInterval));
		}

		public void FadeOut(float fadeOutInterval) {
			StartCoroutine(FadeOutScene(fadeOutInterval));
		}

		public void Fade()
		{
			StartCoroutine(TransScene(defaultInterval, defaultInterval, TransType.None, null, null));
		}

		public void Fade(float interval)
		{
			StartCoroutine(TransScene(interval, interval, TransType.None, null, null));
		}

		public void Fade(float fadeInInterval, float fadeOutInterval) {
			StartCoroutine(TransScene(fadeInInterval, fadeOutInterval, TransType.None, null, null));
		}

		public void LoadLevel(string nextLevelName, Dictionary<string, int> assetBundleInfoDic) {
			StartCoroutine(TransScene(defaultInterval, defaultInterval, TransType.Default, nextLevelName, assetBundleInfoDic));
		}

		public void LoadLevel(float interval, string nextLevelName, Dictionary<string, int> assetBundleInfoDic) {
			StartCoroutine(TransScene(interval, interval, TransType.Default,nextLevelName, assetBundleInfoDic));
		}

		public void LoadLevelInLoading(string nextLevelName, Dictionary<string, int> assetBundleInfoDic) {
			StartCoroutine(TransScene(defaultInterval, defaultInterval, TransType.Loading, nextLevelName, assetBundleInfoDic));
		}

		public void LoadLevelInLoading(float interval, string nextLevelName, Dictionary<string, int> assetBundleInfoDic) {
			StartCoroutine(TransScene(interval, interval, TransType.Loading,nextLevelName, assetBundleInfoDic));
		}

		public void LoadLevelInLoading(float fadeInInterval, float fadeOutInterval, string nextLevelName, Dictionary<string, int> assetBundleInfoDic) {
			StartCoroutine(TransScene(fadeInInterval, fadeOutInterval, TransType.Loading,nextLevelName, assetBundleInfoDic));
		}

		IEnumerator TransScene(float fadeInInterval, float fadeOutInterval,
			TransType transType, string nextLevelName,
			Dictionary<string, int> assetBundleInfoDic)
		{

			while (isDuring)
			{
				yield return null;
			}
			try
			{
				isDuring = true;
				
				SearchButton.Instance.Hide();
				var raw = CreateLayer();

				var time = 0.0f;
				while (time <= fadeOutInterval)
				{
					raw.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, time / fadeOutInterval));
					time += Time.deltaTime;
					yield return null;
				}

				if (transType == TransType.Default || transType == TransType.Loading)
				{
					if (transType == TransType.Loading)
					{
						SceneManager.LoadScene("loading");
						while (SceneManager.GetActiveScene().name != "loading") yield return null;
					}

					if (AudioManager.Exist() && !AudioManager.Instance.DestructionFlg)
					{
						AssetLoader.Instance.UnloadExcludingAudios();
					}
					else
					{
						AssetLoader.Instance.Unload();
					}

					AssetLoader.Instance.AssetBundleInfoDic = assetBundleInfoDic;
					var ao = SceneManager.LoadSceneAsync(nextLevelName);
					ao.allowSceneActivation = false;
					AssetLoader.Instance.Load();
					while (ao.progress < 0.9f) yield return null;
					while (AssetLoader.Instance.CurrentLoadStatus != AssetLoader.LoadStatus.LoadComplete) yield return null;
					ao.allowSceneActivation = true;
					while (SceneManager.GetActiveScene().name != nextLevelName) yield return null;
				}
				else
				{
					Destroy();
				}

				time = 0;
				raw = CreateLayer();
				raw.color = new Color(0, 0, 0, 1);

				if (AudioManager.Exist())
				{
					while (!AudioManager.Instance.IsLoadComplete()) yield return null;
				}

				if (CharacterInitializer.Exist())
				{
					while (!CharacterInitializer.Instance.IsLoadComplete()) yield return null;
				}
				while (time <= fadeInInterval)
				{
					raw.color = new Color(0, 0, 0, Mathf.Lerp(1f, 0f, time / fadeInInterval));
					time += Time.deltaTime;
					yield return null;
				}

				Destroy();
				SearchButton.Instance.Show();
			}
			finally
			{
				isDuring = false;
			}
		}

		public IEnumerator FadeInScene(float fadeInInterval) {
			return Trans(fadeInInterval, 1f ,0f, true);
		}

		public IEnumerator FadeOutScene(float fadeOutInterval) {
			return Trans(fadeOutInterval, 0f ,1f, false);
		}

		IEnumerator Trans(float interval, float startTransVal, float endTransVal, bool destructionFlg) {
			Destroy();
			var raw= CreateLayer();

			var time = 0.0f;
			while (time <= interval) {
				raw.color = new Color(0, 0, 0, Mathf.Lerp(startTransVal, endTransVal, time / interval));
				time += Time.deltaTime;
				yield return null;
			}

			if (destructionFlg) {
				Destroy();
			}
		}

		RawImage CreateLayer()
		{
			baseLayer = new GameObject
			{
				name = "BaseLayer",
				layer = 5
			};
			var canvas = baseLayer.AddComponent<Canvas>();
			canvas.renderMode = 0;

			var childLayer = new GameObject {name = "ChildLayer"};
			childLayer.transform.SetParent(baseLayer.transform);
			var rect = childLayer.AddComponent<RectTransform>();
			rect.anchorMax = new Vector2(1f, 1f);
			rect.anchorMin = new Vector2(0f, 0f);
			rect.anchoredPosition = new Vector2(0, 0);
			return childLayer.AddComponent<RawImage>();
		}
	}
}
