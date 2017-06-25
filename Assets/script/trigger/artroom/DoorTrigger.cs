using script.core.scene;
using UnityEngine;

namespace script.trigger.artroom
{
	public class DoorTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other) {
			if (other.gameObject.name == "yusuke")
			{
				SceneStatus.EntranceNo = 3;
				SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "corridor", null);
			}
		}
	}
}
