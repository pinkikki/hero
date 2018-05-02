using script.core.operation;
using UnityEngine;

namespace script.trigger.schoolyard
{
	public class TileAntTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		void OnTriggerEnter2D(Collider2D other) {
			if (other.gameObject.name == "yusuke") {
				SearchButton.Instance.OnRegister(811);
			}
		}

		void OnTriggerExit2D(Collider2D other)
		{
			if (other.gameObject.name == "yusuke") {
				SearchButton.Instance.OnDialog();
			}
		}
	}
}
