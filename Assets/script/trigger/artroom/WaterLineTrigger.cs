using script.core.operation;
using script.core.scene;
using UnityEngine;

namespace script.trigger.artroom
{
	public class WaterLineTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke" && SceneStatus.CanCreateNerikeshi && !SceneStatus.IsFinishedWashingHands && SceneStatus.Procedure == 2)
			{
				SearchButton.Instance.OnRegister(717);
			}
		}

		void OnCollisionExit2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke")
			{
				SearchButton.Instance.OnDialog();
			}
		}
	}
}
