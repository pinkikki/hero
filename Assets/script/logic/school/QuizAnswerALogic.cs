using script.core.monoBehaviour;
using script.core.operation;
using script.core.scene;
using UnityEngine;

namespace script.logic.school
{
	public class QuizAnswerALogic : SingletonMonoBehaviour<QuizAnswerALogic>
	{
		private bool registrationFlg;

		void Start () {
		
		}
	
		void Update () {
		
		}

		void OnCollisionEnter2D(Collision2D other) {
		}

		private void OnCollisionStay2D(Collision2D other)
		{
			if (!registrationFlg && other.gameObject.name == "yusuke" && !SceneStatus.IsCompletedQuizA && SceneStatus.Procedure == 3)
			{
				var pos = transform.position;
				if ((0.7f < pos.x || pos.x < 1.4f) && 1.4f < pos.y)
				{
					registrationFlg = true;
					SearchButton.Instance.OnRegister(504);
				}

			}
		}

		void OnCollisionExit2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke")
			{
				registrationFlg = false;
				SearchButton.Instance.OnDialog();
			}
		}
	}
}
