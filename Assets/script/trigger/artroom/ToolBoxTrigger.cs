using script.core.character;
using script.core.@event;
using script.core.operation;
using script.core.scene;
using UnityEngine;

namespace script.trigger.artroom
{
	public class ToolBoxTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke" && SceneStatus.CanCreateNerikeshi && !SceneStatus.HasGlue && SceneStatus.Procedure == 3)
			{
				SearchButton.Instance.OnRegister(707);
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
