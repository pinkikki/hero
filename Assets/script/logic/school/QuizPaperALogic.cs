using script.core.monoBehaviour;
using script.core.operation;
using script.core.scene;
using UnityEngine;

namespace script.logic.school
{
	public class QuizPaperALogic : SingletonMonoBehaviour<QuizPaperALogic> {

		void Start() {
		}
	
		void Update() {
		}

		void OnCollisionEnter2D(Collision2D other) {
			if (other.gameObject.name == "yusuke" && !SceneStatus.HasQuizA && SceneStatus.Procedure == 3) {
				SearchButton.Instance.OnRegister(503);
			}
		}

		void OnCollisionExit2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke") {
				SearchButton.Instance.OnDialog();
			}
		}
	}
}
