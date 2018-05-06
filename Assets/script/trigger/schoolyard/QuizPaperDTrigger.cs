using script.core.character;
using script.core.@event;
using script.core.operation;
using script.core.scene;
using UnityEngine;

namespace script.trigger.schoolyard
{
	public class QuizPaperDTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.name == "yusuke" && SceneStatus.HasQuizC && SceneStatus.Procedure == 3 && !SceneStatus.HasQuizD)
			{
				SearchButton.Instance.OnRegister(803);
			}
		}
		
		void OnTriggerExit2D(Collider2D other)
		{
			if (other.gameObject.name == "yusuke")
			{
				SearchButton.Instance.OnDialog();
			}
		}
	}
}
