using Assets.script.core.scene;
using UnityEngine;

namespace script.trigger.corridor
{
	public class DoorTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other) {
			if (other.gameObject.name == "yusuke")
			{
				SceneStatus.EntranceNo = 1;
				SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "artroom", null);
			}
		}
	}
}
