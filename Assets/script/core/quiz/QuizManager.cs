using System;
using script.common.dao;
using script.common.entity;
using script.core.asset;
using script.core.audio;
using script.core.monoBehaviour;
using script.core.scene;
using UnityEngine;

namespace script.core.quiz
{
	public class QuizManager : SingletonMonoBehaviour<QuizManager> {

		public bool Enabled;
		MusicEntity entity;
		
		void Awake()
		{
			DontDestroyOnLoad(this);
		}
		
		void Start () {
			// 非アクティブ状態だとインスタンスを取得できなくなるので、ここで取得しておく
			var obj = Instance;
			gameObject.SetActive(false);
			Enabled = true;
		}

	
		void Update () {
		
		}

		public void OnClick()
		{
			Debug.Log(Enabled);
			if (Enabled)
			{
				string quizName;
				
				if (SceneStatus.HasQuizE)
				{
					quizName = "ReQuizE";
				}
				else if (SceneStatus.HasQuizD)
				{
					quizName = "ReQuizD";
				}
				else if (SceneStatus.HasQuizC)
				{
					quizName = "ReQuizC";
				}
				else if (SceneStatus.HasQuizB)
				{
					quizName = "ReQuizB";
				}
				else if (SceneStatus.HasQuizA)
				{
					quizName = "ReQuizA";
				}
				else
				{
					return;
				}
				
				Debug.Log(quizName);
				
				var obj = (GameObject) Instantiate(AssetLoader.Instance.LoadPrefab("prefab/common/", quizName),
					new Vector2(0.0f, 0.0f), Quaternion.identity);
				obj.name = "ReQuiz";
			}
			PlaySe();
		}

		public void Show()
		{
			if (!SceneStatus.CanFlowEndRoll && SceneStatus.HasQuizA)
			{
				gameObject.SetActive(true);	
			}
		}
		
		public void Hide()
		{
			gameObject.SetActive(false);	
		}
		
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
