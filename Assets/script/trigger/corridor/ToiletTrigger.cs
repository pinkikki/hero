using script.core.operation;
using UnityEngine;

namespace script.trigger.corridor
{
	public class ToiletTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}

		void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke")
			{
				if (gameObject.name == "door_toilet_girl")
				{
					SearchButton.Instance.OnRegister(608);
				}
				else
				{
					SearchButton.Instance.OnRegister(609);
				}
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
