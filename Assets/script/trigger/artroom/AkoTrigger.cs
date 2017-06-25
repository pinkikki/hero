using script.core.character;
using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.trigger.artroom
{
	public class AkoTrigger : MonoBehaviour
	{
		void Start()
		{
		}

		void Update()
		{
		}

		void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke" && SceneStatus.Procedure == 1)
			{
				if (SceneStatus.LastSearchedArtObject == SceneStatus.ArtObject.Smartball)
				{
					EventManager.Instance.Register(705);
				}
				else if (SceneStatus.LastSearchedArtObject == SceneStatus.ArtObject.None)
				{
					EventManager.Instance.Register(702);
				}
				else
				{
					EventManager.Instance.Register(704);
				}
				gameObject.GetComponent<AutoCharacterController>().FreezeFlg = true;
			}
		}
	}
}
