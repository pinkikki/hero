using script.core.scene;
using UnityEngine;

namespace script.trigger.schoolyard
{
	public class DoorTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other) {
			if (other.gameObject.name == "yusuke")
			{
				if (gameObject.name == "door_a")
				{
					SceneStatus.EntranceNo = 4;
					SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "corridor", null);
				}
				else if (gameObject.name == "door_b")
				{
					SceneStatus.EntranceNo = 5;
					SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "corridor", null);
				}
				else if (gameObject.name == "door_c")
				{
					SceneStatus.EntranceNo = 6;
					SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "corridor", null);
				}
			}
		}
	}
}
