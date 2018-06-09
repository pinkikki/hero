using script.core.scene;
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
				if (gameObject.name == "door_artroom")
				{
					SceneStatus.EntranceNo = 1;
					SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "artroom", null);
				}
				else if (gameObject.name == "door_a")
				{
					SceneStatus.EntranceNo = 2;
					SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "classroom", null);
				}
				else if (gameObject.name == "door_b")
				{
					SceneStatus.EntranceNo = 1;
					SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "classroom", null);
				}
				else if (gameObject.name == "door_c_1")
				{
					SceneStatus.EntranceNo = 1;
					SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "schoolyard", null);
				}
				else if (gameObject.name == "door_c_2")
				{
					SceneStatus.EntranceNo = 2;
					SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "schoolyard", null);
				}
				else if (gameObject.name == "door_c_3")
				{
					SceneStatus.EntranceNo = 3;
					SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "schoolyard", null);
				}
			}
		}
	}
}
