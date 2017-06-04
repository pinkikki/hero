using Assets.script.core.operation;
using Assets.script.core.scene;
using UnityEngine;

namespace Assets.script.trigger.classroom
{
	public class BroomTrigger : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
		
		}

		void OnCollisionEnter2D(Collision2D other) {
			if (other.gameObject.name == "yusuke" && !SceneStatus.HasBroom && SceneStatus.Procedure == 3) {
				SearchButton.Instance.OnRegister(505);
			}
		}

		void OnCollisionExit2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke") {
				SearchButton.Instance.OnDialog();
			}
		}
	}
}
