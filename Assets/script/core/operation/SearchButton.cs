using System;
using script.common.dao;
using script.common.entity;
using script.core.audio;
using script.core.@event;
using script.core.monoBehaviour;
using script.core.scene;
using UnityEngine;
using UnityEngine.UI;

namespace script.core.operation
{
	public class SearchButton : SingletonMonoBehaviour<SearchButton>
	{
		Button button;
		MusicEntity entity;
		
		void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

		void Start () {
			// 非アクティブ状態だとインスタンスを取得できなくなるので、ここで取得しておく
			var obj = Instance;
			button = gameObject.GetComponent<Button>();
			gameObject.SetActive(false);
			OnDialog();
		}

		void Update () {

		}

		public void Show()
		{
			if (!SceneStatus.CanFlowEndRoll && SceneStatus.Starting)
			{
				gameObject.SetActive(true);	
			}
		}

		public void Hide()
		{
			if (button == null)
			{
				button = gameObject.GetComponent<Button>();
			}
			gameObject.SetActive(false);
		}

		public void OnDialog()
		{
			if (button == null) return;
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(Dialog);
		}

		public void Dialog()
		{
			PlaySe();
			EventManager.Instance.Register(99999);
		}

		public void OnRegister(int eventId)
		{
			if (button == null) return;
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => Register(eventId));
		}

		public void OnRegister(Action action)
		{
			if (button == null) return;
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() =>
			{
				PlaySe();
				action();
			});
		}
		
		public void Register(int eventId)
		{
			PlaySe();
			EventManager.Instance.Register(eventId);
		}

		public void OnNop()
		{
			if (button == null) return;
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(Nop);
		}

		public void Nop()
		{
			PlaySe();
		}
		
		void PlaySe()
		{
			if (entity == null)
			{
				entity = MusicDao.SelectByPrimaryKey(7);
			}
			AudioManager.Instance.PlaySe(entity.MusicName);
		}
	}
}
