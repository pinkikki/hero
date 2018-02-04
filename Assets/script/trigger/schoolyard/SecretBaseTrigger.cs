using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.trigger.schoolyard
{
	public class SecretBaseTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
	
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.name == "yusuke" && SceneStatus.CanFlowEndRoll)
			{
				EventManager.Instance.Register(1002);
			}
			else if (other.gameObject.name == "yusuke" && SceneStatus.HasQuizE)
			{
				EventManager.Instance.Register(1001);				
			}
		}
	}
}
