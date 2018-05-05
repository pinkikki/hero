﻿using System.Collections;
using script.common.dao;
using script.core.audio;
using script.core.@event;
using script.core.scene;
using UnityEngine;
using UnityEngine.UI;

namespace script.logic.opening
{
	public class StartingLogic : MonoBehaviour {
		
		void Start () {
			EventManager.Instance.Register(5001);
		}
	
		void Update () {
		
		}

		private bool starting;
		public void Click()
		{
			if (!starting)
			{
				starting = true;
				AudioManager.Instance.PlaySe(MusicDao.SelectByPrimaryKey(7).MusicName);
				SceneStatus.Starting = true;
				SceneStatus.Procedure = 1;
				SceneStatus.EntranceNo = 1;
				SceneLoadManager.Instance.LoadLevelInLoading(0.0f, 0.0f, "classroom", null);
			}
		}
		
		public void Action001()
		{
			StartCoroutine(Action001Coroutine());
		}

		IEnumerator Action001Coroutine()
		{
			yield return new WaitForSeconds(0.1f);
			yield return SpriteIn(GameObject.Find("opening_illust").GetComponent<SpriteRenderer>());
			yield return new WaitForSeconds(0.1f);
			yield return SpriteIn(GameObject.Find("opening_title").GetComponent<SpriteRenderer>());
			yield return TextIn(GameObject.Find("Text").GetComponent<Text>());
		}
		
		IEnumerator SpriteIn(SpriteRenderer sprite)
		{
			var time = 0.0f;
			var fadeOutInterval = 0.1f;
			while (time <= fadeOutInterval)
			{
				sprite.color = new Color(255, 255, 255, Mathf.Lerp(0f, 1f, time / fadeOutInterval));
				time += Time.deltaTime;
				yield return null;
			}
		}
		
		IEnumerator TextIn(Text text)
		{
			var time = 0.0f;
			var fadeOutInterval = 0.1f;
			while (time <= fadeOutInterval)
			{
				text.color = new Color(255, 255, 255, Mathf.Lerp(0f, 1f, time / fadeOutInterval));
				time += Time.deltaTime;
				yield return null;
			}
		}
	}
}
