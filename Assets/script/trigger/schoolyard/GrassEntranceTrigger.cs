using script.core.@event;
using script.core.operation;
using script.core.scene;
using UnityEngine;

namespace script.trigger.schoolyard
{
	public class GrassEntranceTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other) {
			if (other.gameObject.name == "yusuke" && SceneStatus.HasQuizE)
			{
				SearchButton.Instance.OnRegister(810);
			}
		}
		
		void OnCollisionExit2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke")
			{
				SceneStatus.EntranceNo = 1;
				SearchButton.Instance.OnDialog();
			}
		}
	}
}
