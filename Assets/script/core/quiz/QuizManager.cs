using script.core.asset;
using script.core.monoBehaviour;
using script.core.scene;
using UnityEngine;

namespace script.core.quiz
{
	public class QuizManager : SingletonMonoBehaviour<QuizManager> {

		void Awake()
		{
			DontDestroyOnLoad(this);
		}
		
		void Start () {
			// 非アクティブ状態だとインスタンスを取得できなくなるので、ここで取得しておく
			var obj = Instance;
			gameObject.SetActive(false);
		}

	
		void Update () {
		
		}

		public void OnClick()
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
			
			var obj = (GameObject) Instantiate(AssetLoader.Instance.LoadPrefab("prefab/common/", quizName),
				new Vector2(0.0f, 0.0f), Quaternion.identity);
			obj.name = quizName;
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}
	}
}
