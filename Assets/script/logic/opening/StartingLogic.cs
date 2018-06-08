using System.Collections;
using System.Collections.Generic;
using script.common.dao;
using script.core.audio;
using script.core.@event;
using script.core.scene;
using UnityEngine;
using UnityEngine.UI;

namespace script.logic.opening
{
	public class StartingLogic : MonoBehaviour {

		[SerializeField] GameObject ContinueButton;
		[SerializeField] GameObject StartSelect;
		
		void Start ()
		{
			if (ContinueButton == null)
			{
				ContinueButton = GameObject.Find("ContinueButton");
			}

			ContinueButton.SetActive(false);
			
			if (StartSelect == null)
			{
				StartSelect = GameObject.Find("StartSelect");
			}

			StartSelect.SetActive(false);
			
			EventManager.Instance.Register(5001);
		}
	
		void Update () {
		
		}

		private bool starting;
		public void StartGame()
		{
			if (!starting)
			{
				starting = true;
				AudioManager.Instance.PlaySe(MusicDao.SelectByPrimaryKey(7).MusicName);
				var saveEntity = SaveDao.SelectAll();
				if (saveEntity.SceneId != "starting")
				{
					StartSelect.SetActive(true);
				}
				else
				{
					Advance();

				}
				starting = false;
			}
		}
		
		public void Yes()
		{
			if (!starting)
			{
				starting = true;
				AudioManager.Instance.PlaySe(MusicDao.SelectByPrimaryKey(7).MusicName);
				Advance();
			}
			
		}

		void Advance()
		{
			SaveDao.Delete();
			SaveDao.Insert();
			SceneStatus.Starting = true;
			SceneStatus.Procedure = 1;
			SceneStatus.EntranceNo = 1;
			SceneLoadManager.Instance.LoadLevelInLoading(1.0f, 5.0f, "classroom", null);
		
		}
		
		public void No()
		{
			StartSelect.SetActive(false);
			AudioManager.Instance.PlaySe(MusicDao.SelectByPrimaryKey(7).MusicName);
		}
		
		public void ContinueGame()
		{
			if (!starting)
			{
				starting = true;
				SceneStatus.Continue = true;
				AudioManager.Instance.PlaySe(MusicDao.SelectByPrimaryKey(7).MusicName);
				var saveEntity = SaveDao.SelectAll();
				saveEntity.reflect();
				SceneStatus.EntranceNo = 1;
				SceneLoadManager.Instance.LoadLevelInLoading(1.0f, 5.0f, saveEntity.SceneId, null);
			}
		}
		
		public void Action001()
		{
			StartCoroutine(Action001Coroutine());
		}

		IEnumerator Action001Coroutine()
		{
			var saveEntity = SaveDao.SelectAll();
			if (saveEntity.SceneId != "starting")
			{
				ContinueButton.SetActive(true);
			}
			yield return new WaitForSeconds(3.0f);
			yield return SpriteIn(GameObject.Find("opening_illust").GetComponent<SpriteRenderer>());
			yield return new WaitForSeconds(2.0f);
			yield return SpriteIn(GameObject.Find("opening_title").GetComponent<SpriteRenderer>());
			List<Text> texts = new List<Text>();
			texts.Add(GameObject.Find("StartButton/Text").GetComponent<Text>());
			if (saveEntity.SceneId != "starting")
			{
				texts.Add(ContinueButton.transform.Find("Text").GetComponent<Text>());
			}
			yield return TextsIn(texts);
			EventManager.Instance.NextTask();
		}
		
		IEnumerator SpriteIn(SpriteRenderer sprite)
		{
			var time = 0.0f;
			var fadeOutInterval = 6.0f;
			while (time <= fadeOutInterval)
			{
				sprite.color = new Color(255, 255, 255, Mathf.Lerp(0f, 1f, time / fadeOutInterval));
				time += Time.deltaTime;
				yield return null;
			}
		}
		
		IEnumerator TextsIn(List<Text> texts)
		{
			var time = 0.0f;
			var fadeOutInterval = 2.0f;
			while (time <= fadeOutInterval)
			{
				foreach (var text in texts)
				{
					text.color = new Color(255, 255, 255, Mathf.Lerp(0f, 1f, time / fadeOutInterval));
					time += Time.deltaTime;
					yield return null;
				}
			}
		}
	}
}
