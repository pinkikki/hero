using script.core.operation;
using UnityEngine;

namespace script.trigger.schoolyard
{
	public class TreeZoneTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke")
			{
				Debug.Log(gameObject.name);
				switch (gameObject.name)
				{
					case "azalea":
						SearchButton.Instance.OnRegister(812);
						break;
					case "cherry_tree":
						SearchButton.Instance.OnRegister(813);
						break;
					case "ginkgo":
						SearchButton.Instance.OnRegister(814);
						break;
					case "hydrangea":
						SearchButton.Instance.OnRegister(815);
						break;
					case "magnolia_kobus":
						SearchButton.Instance.OnRegister(816);
						break;
					case "pine":
						SearchButton.Instance.OnRegister(817);
						break;
					case "platan":
						SearchButton.Instance.OnRegister(818);
						break;
					case "thuja":
						SearchButton.Instance.OnRegister(819);
						break;
					case "ichii":
						SearchButton.Instance.OnRegister(820);
						break;
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
