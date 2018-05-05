using script.core.monoBehaviour;
using script.core.operation;
using script.core.scene;
using UnityEngine;

namespace script.trigger.classroom
{
	public class QuizAnswerATrigger : SingletonMonoBehaviour<QuizAnswerATrigger>
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
				var pos = transform.localPosition;
				if ((0.6f < pos.x && pos.x < 1.4f) && 1.4f < pos.y)
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
