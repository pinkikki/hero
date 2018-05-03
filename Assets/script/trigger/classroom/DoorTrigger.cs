using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.trigger.classroom
{
	public class DoorTrigger : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other) {
			if (other.gameObject.name == "yusuke") {
				if (SceneStatus.Procedure >= 3)
				{
					if(SceneStatus.IsCompletedQuizA){
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
					else if(SceneStatus.HasQuizA)
					{
						EventManager.Instance.Register(509);	
					}
					else
					{
						EventManager.Instance.Register(510);
					}
				}
				else if (SceneStatus.Procedure == 1)
				{
					EventManager.Instance.Register(508);
				}
			}
		}
	}
}
