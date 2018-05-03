using script.core.character;
using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.trigger.artroom
{
	public class NerikeshiGameTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke" && SceneStatus.Procedure == 3)
			{
				if (SceneStatus.HasGlue)
				{
					EventManager.Instance.Register(708);
				}
				else
				{
					EventManager.Instance.Register(709);
				}
			}
		}
	}
}
