using script.core.monoBehaviour;
using script.core.operation;
using script.core.scene;
using script.logic.school;
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
			if (other.gameObject.name == "yusuke" && !SceneStatus.IsCompletedQuizA && SceneStatus.Procedure == 3)
			{
				var pos = transform.position;
				if ((4.65f < pos.x && pos.x < 5.65f) && 4.15f < pos.y)
				{
					ClassroomDeskStatus.DeskX = transform.position.x;
					if (!registrationFlg)
					{
						registrationFlg = true;
						SearchButton.Instance.OnRegister(504);
					}
				}
				else
				{
					registrationFlg = false;
					SearchButton.Instance.OnDialog();					
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
