using script.core.@event;
using UnityEngine;

namespace script.trigger.artroom
{
	public class SmartballSuccessTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		void OnCollisionEnter(Collision other) {
			EventManager.Instance.Register(720);
		}
	}
}
