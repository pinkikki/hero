using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.trigger.schoolyard
{
	public class QuizPaperDTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.name == "yusuke" && SceneStatus.HasQuizC && SceneStatus.Procedure == 3)
			{
				EventManager.Instance.Register(803);
			}
		}
	}
}
