using script.core.operation;
using script.core.scene;
using UnityEngine;

namespace script.trigger.artroom
{
	public class DusterTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke" && SceneStatus.CanCreateNerikeshi && !SceneStatus.HasDuster && SceneStatus.Procedure == 3)
			{
				SearchButton.Instance.OnRegister(718);
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
