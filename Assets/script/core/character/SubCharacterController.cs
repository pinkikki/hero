using script.core.@event;
using UnityEngine;

namespace script.core.character
{
	public class SubCharacterController : MonoBehaviour {

		void Start () {
	
		}
	
		void Update () {
	
		}

		void OnCollisionEnter2D(Collision2D other)
		{
			EventManager.Instance.Register(gameObject.name);
		}
	}
}
