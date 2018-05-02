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
			if (other.gameObject.name == "yusuke")
			{
				EventManager.Instance.Register(gameObject.name);
			}
		}
	}
}
