using Assets.script.core.scene;
using UnityEngine;

namespace Assets.script.trigger.classroom
{
	public class DoorTrigger : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other) {
			if (other.gameObject.name == "yusuke" && SceneStatus.IsCompletedQuizA && SceneStatus.Procedure >= 3)
			{
				if (gameObject.name == "door_a")
				{
					SceneStatus.EntranceNo = 1;
				}
				else
				{
					SceneStatus.EntranceNo = 2;
				}
				SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "corridor", null);
			}
		}
	}
}
