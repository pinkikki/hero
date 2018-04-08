﻿using System;
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
			button.onClick.AddListener(() => action());
		}
		
		public void Register(int eventId)
		{
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

		}
	}
}
