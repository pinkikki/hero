using script.core.operation;
using UnityEngine;

namespace script.trigger.schoolyard
{
	public class BasketCloseTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.name == "chicken_target")
			{
				Debug.Log("test");
//				SearchButton.Instance.OnRegister(801);
			}
		}

		void OnCollisionExit2D(Collision2D other)
		{
			if (other.gameObject.name == "chicken_target")
			{
				SearchButton.Instance.OnDialog();
			}
		}
	}
}
