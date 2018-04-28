using script.core.scene;
using UnityEngine;

namespace script.trigger.schoolyard
{
	public class GrassExitTrigger : MonoBehaviour {

		void Start () {
		
		}

		void Update () {
		
		}

		void OnCollisionEnter2D(Collision2D other) {
			if (other.gameObject.name == "yusuke")
			{
				SceneStatus.EntranceNo = 4;
				SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "schoolyard", null);
			}
		}
	}
}
